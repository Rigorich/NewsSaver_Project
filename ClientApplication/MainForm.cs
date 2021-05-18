using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseClasses;
using ServerService;
using ServiceStack;

namespace ClientApplication
{
    public partial class MainForm : Form
    {
        JsonServiceClient server = new JsonServiceClient("http://localhost:5000");

        public MainForm()
        {
            InitializeComponent();

            //DataGridViewNews.DataSource = new NewsArticle("url", "html", "name", "text", DateTime.Now);
            string[][] rows = new string[][] {
                new string[] {DateTime.Now.ToString("g"), "Тестовая статья", "http://test.by/" },
                new string[] {DateTime.UnixEpoch.ToString("g"), "UNIX запустило время!", "http://unix.com/" },
                new string[] {DateTime.MinValue.ToString("g"), "Учёные изобрели машину времени", "https://time.net" }
            };
            foreach (var row in rows)
            {
                DataGridViewNews.Rows.Add(row);
            }
        }

        private void LostConnection()
        {
            WaitLabels.Remove(LabelConnectionState);
            LabelConnectionState.Text = Localization.Error;
            LabelConnectionState.Refresh();
            TimerLoadSites.Interval = 10000;
            TimerLoadSites.Start();
        }

        private async void TimerLoadSites_Tick(object sender, EventArgs e)
        {
            TimerLoadSites.Stop();
            LabelConnectionState.Text = Localization.WaitText;
            LabelConnectionState.Refresh();
            WaitLabels.Add(LabelConnectionState);
            ComboBoxSites.Items.Clear();
            try
            {
                var request = new AvailableSitesRequest();
                var response = await server.GetAsync(request);
                var result = response.Result;

                ComboBoxSites.Items.Add(Localization.NoSite);
                ComboBoxSites.Items.AddRange(result);
                ComboBoxSites.SelectedIndex = 0;
                WaitLabels.Remove(LabelConnectionState);
                LabelConnectionState.Text = Localization.Okay;
                LabelConnectionState.Refresh();
            }
            catch (Exception ex) when (
                ex is HttpError
                || ex is WebServiceException
                || ex is System.Net.WebException)
            {
                LostConnection();
                //MessageBox.Show(he.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Label> WaitLabels = new List<Label>();
        private void TimerLoadSitesDots_Tick(object sender, EventArgs e)
        {
            foreach (Label label in WaitLabels)
            {
                string text = label.Text;
                if (text.EndsWith("..."))
                {
                    label.Text = text.Substring(0, text.Length - 3);
                }
                else
                {
                    label.Text += ".";
                }
                label.Refresh();
            }
        }

        void AddToDataGridView(IEnumerable<NewsArticle> news)
        {
            foreach (NewsArticle article in news)
            {
                string[] row = new string[] {
                    article.Date.ToString("g"),
                    article.Name,
                    article.URL
                };
                DataGridViewNews.Rows.Add(row);
            }
        }

        private void RefreshNewsList()
        {
            if (LabelConnectionState.Text != Localization.Okay) return;
            var request = new ListRequest()
            {
                Url = ComboBoxSites.Text != Localization.NoSite ? ComboBoxSites.Text : null,
                LeftBoundDate = DateTimePickerLeft.Checked ? DateTimePickerLeft.Value : DateTime.MinValue,
                RightBoundDate = DateTimePickerRight.Checked ? DateTimePickerRight.Value : DateTime.MaxValue,
                Keywords = null,
                Entitities = null,

                OldestFirst = CheckBoxOldNews.Checked,
                Count = int.Parse(ComboBoxCount.Text),
                Skip = (int.Parse(TextBoxPage.Text) - 1) * int.Parse(ComboBoxCount.Text),
            };
            NewsArticle[] result;
            try
            {
                var response = server.Get(request);
                result = response.Result;
            }
            catch (Exception ex) when (
                ex is HttpError
                || ex is WebServiceException
                || ex is System.Net.WebException)
            {
                return;
            }
            DataGridViewNews.Rows.Clear();
            AddToDataGridView(result);
        }

        private void ButtonDownload_Click(object sender, EventArgs e)
        {
            TextBoxPage.Text = "1";
            RefreshNewsList();
        }

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            if (LabelConnectionState.Text != Localization.Okay) return;
            if (LabelSaveState.Text.StartsWith(Localization.WaitText)) return;
            LabelSaveState.Text = Localization.WaitText;
            LabelSaveState.Refresh();
            WaitLabels.Add(LabelSaveState);

            NewsRequest request = new NewsRequest();
            if (ComboBoxSites.Text != Localization.NoSite)
            {
                request.Url = ComboBoxSites.Text;
                request.Count = (int)NumericUpDownSave.Value;
            }
            else
            {
                request.Url = "";
                request.Count = 1;
            }

            NewsResponse response;
            try
            {
                response = await server.GetAsync(request);
            }
            catch (Exception ex) when (
                ex is HttpError
                || ex is WebServiceException
                || ex is System.Net.WebException)
            {
                WaitLabels.Remove(LabelSaveState);
                LabelSaveState.Text = Localization.Error;
                LabelSaveState.Refresh();
                //MessageBox.Show(he.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NewsArticle[] news = response.ResultNews;
            bool[] saved = response.ResultIsSaved;

            WaitLabels.Remove(LabelSaveState);
            LabelSaveState.Text = Localization.Okay;
            LabelSaveState.Refresh();

            DataGridViewNews.Rows.Clear();
            AddToDataGridView(news);
            for (int i = 0; i < saved.Length; i++)
            {
                if (!saved[i])
                {
                    DataGridViewNews.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                }
            }
        }

        private void ButtonPrevPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(TextBoxPage.Text) > 1)
            {
                TextBoxPage.Text = (int.Parse(TextBoxPage.Text) - 1).ToString();
                RefreshNewsList();
            }
        }

        private void ButtonNextPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(TextBoxPage.Text) < 10)
            {
                TextBoxPage.Text = (int.Parse(TextBoxPage.Text) + 1).ToString();
                RefreshNewsList();
            }
        }

        private void ComboBoxCount_TextChanged(object sender, EventArgs e)
        {
            TextBoxPage.Text = "1";
            RefreshNewsList();
        }

        private void CheckBoxOldNews_CheckedChanged(object sender, EventArgs e)
        {
            RefreshNewsList();
        }
    }
}
