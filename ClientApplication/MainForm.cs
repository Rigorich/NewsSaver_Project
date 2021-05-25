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
        public string Password = null;

        public MainForm()
        {
            InitializeComponent();
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
            string[] sites;
            try
            {
                var request = new AvailableSitesRequest();
                var response = await server.GetAsync(request);
                var result = response.Result;
                sites = result;
            }
            catch (Exception ex) when (
                ex is HttpError
                || ex is WebServiceException
                || ex is System.Net.WebException)
            {
                LostConnection();
                return;
            }
            ComboBoxSites.Items.Add(Localization.NoSite);
            ComboBoxSites.Items.AddRange(sites);
            ComboBoxSites.SelectedIndex = 0;
            WaitLabels.Remove(LabelConnectionState);
            LabelConnectionState.Text = Localization.Okay;
            LabelConnectionState.Refresh();
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
            if (LabelSaveState.Text.StartsWith(Localization.WaitText)) return;

            ListRequest request;
            try
            {
                request = new ListRequest()
                {
                    Url = ComboBoxSites.Text != Localization.NoSite ? ComboBoxSites.Text : null,
                    LeftBoundDate = DateTimePickerLeft.Checked ? DateTimePickerLeft.Value.Date : DateTime.UnixEpoch.AddYears(-1),
                    RightBoundDate = DateTimePickerRight.Checked ? DateTimePickerRight.Value.Date.AddDays(1).AddSeconds(-1) : DateTime.MaxValue,
                    Keywords = TextBoxKeywords.Text.Split(",;. |".ToCharArray()),
                    Entitities = TextBoxEntities.Text.Split(",;. |".ToCharArray()),

                    OldestFirst = CheckBoxOldNews.Checked,
                    Count = int.Parse(ComboBoxCount.Text),
                    Skip = (int.Parse(TextBoxPage.Text) - 1) * int.Parse(ComboBoxCount.Text),
                };
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверное число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                LostConnection();
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

        private void DataGridViewNews_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (DataGridViewNews.SelectedRows.Count == 1)
            {
                if (Password.IsNullOrEmpty())
                {
                    using (var form = new PasswordForm(this))
                    {
                        form.ShowDialog();
                    }
                }
                if (!Password.IsNullOrEmpty())
                {
                    int index = DataGridViewNews.SelectedRows[0].Index;
                    string url = DataGridViewNews.Rows[index].Cells[2].Value as string;
                    ArticlePasswordedRequest request = new ArticlePasswordedRequest() { Url = url, Password = Password };

                    ArticlePasswordedResponse response;
                    try
                    {
                        response = server.Get(request);
                    }
                    catch (WebServiceException ex) when (ex.StatusCode == 403)
                    {
                        Password = null;
                        MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception ex) when (
                        ex is HttpError
                        || ex is WebServiceException
                        || ex is System.Net.WebException)
                    {
                        LostConnection();
                        return;
                    }

                    NewsArticle article = response.Result;
                    using (var form = new ArticleForm(article))
                    {
                        form.ShowDialog();
                    }
                }
            }
        }
    }
}
