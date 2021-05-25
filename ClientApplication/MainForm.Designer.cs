
namespace ClientApplication
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ComboBoxSites = new System.Windows.Forms.ComboBox();
            this.ComboBoxCount = new System.Windows.Forms.ComboBox();
            this.ButtonDownloadList = new System.Windows.Forms.Button();
            this.DataGridViewNews = new System.Windows.Forms.DataGridView();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimerLoadSites = new System.Windows.Forms.Timer(this.components);
            this.LabelConnection = new System.Windows.Forms.Label();
            this.LabelConnectionState = new System.Windows.Forms.Label();
            this.TimerWaitDots = new System.Windows.Forms.Timer(this.components);
            this.LabelSites = new System.Windows.Forms.Label();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.NumericUpDownSave = new System.Windows.Forms.NumericUpDown();
            this.LabelSaveState = new System.Windows.Forms.Label();
            this.LabelSaveNews = new System.Windows.Forms.Label();
            this.LabelArchiveNews = new System.Windows.Forms.Label();
            this.DateTimePickerLeft = new System.Windows.Forms.DateTimePicker();
            this.DateTimePickerRight = new System.Windows.Forms.DateTimePicker();
            this.TextBoxKeywords = new System.Windows.Forms.TextBox();
            this.TextBoxEntities = new System.Windows.Forms.TextBox();
            this.ButtonNextPage = new System.Windows.Forms.Button();
            this.TextBoxPage = new System.Windows.Forms.TextBox();
            this.ButtonPrevPage = new System.Windows.Forms.Button();
            this.CheckBoxOldNews = new System.Windows.Forms.CheckBox();
            this.LabelOldNews = new System.Windows.Forms.Label();
            this.LabelCount = new System.Windows.Forms.Label();
            this.LabelCurrentPage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewNews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownSave)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboBoxSites
            // 
            this.ComboBoxSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxSites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSites.FormattingEnabled = true;
            this.ComboBoxSites.Location = new System.Drawing.Point(693, 103);
            this.ComboBoxSites.Name = "ComboBoxSites";
            this.ComboBoxSites.Size = new System.Drawing.Size(266, 28);
            this.ComboBoxSites.Sorted = true;
            this.ComboBoxSites.TabIndex = 0;
            // 
            // ComboBoxCount
            // 
            this.ComboBoxCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxCount.FormattingEnabled = true;
            this.ComboBoxCount.Items.AddRange(new object[] {
            "10",
            "25",
            "50"});
            this.ComboBoxCount.Location = new System.Drawing.Point(822, 499);
            this.ComboBoxCount.MaxLength = 2;
            this.ComboBoxCount.Name = "ComboBoxCount";
            this.ComboBoxCount.Size = new System.Drawing.Size(45, 28);
            this.ComboBoxCount.Sorted = true;
            this.ComboBoxCount.TabIndex = 1;
            this.ComboBoxCount.Text = "10";
            this.ComboBoxCount.TextChanged += new System.EventHandler(this.ComboBoxCount_TextChanged);
            // 
            // ButtonDownloadList
            // 
            this.ButtonDownloadList.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ButtonDownloadList.Location = new System.Drawing.Point(784, 384);
            this.ButtonDownloadList.Name = "ButtonDownloadList";
            this.ButtonDownloadList.Size = new System.Drawing.Size(94, 29);
            this.ButtonDownloadList.TabIndex = 3;
            this.ButtonDownloadList.Text = "Поиск";
            this.ButtonDownloadList.UseVisualStyleBackColor = true;
            this.ButtonDownloadList.Click += new System.EventHandler(this.ButtonDownload_Click);
            // 
            // DataGridViewNews
            // 
            this.DataGridViewNews.AllowUserToAddRows = false;
            this.DataGridViewNews.AllowUserToDeleteRows = false;
            this.DataGridViewNews.AllowUserToOrderColumns = true;
            this.DataGridViewNews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewNews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridViewNews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewNews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDate,
            this.ColumnName,
            this.ColumnUrl});
            this.DataGridViewNews.Location = new System.Drawing.Point(12, 12);
            this.DataGridViewNews.Name = "DataGridViewNews";
            this.DataGridViewNews.ReadOnly = true;
            this.DataGridViewNews.RowHeadersVisible = false;
            this.DataGridViewNews.RowHeadersWidth = 51;
            this.DataGridViewNews.RowTemplate.Height = 29;
            this.DataGridViewNews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewNews.Size = new System.Drawing.Size(641, 531);
            this.DataGridViewNews.TabIndex = 4;
            this.DataGridViewNews.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataGridViewNews_MouseDoubleClick);
            // 
            // ColumnDate
            // 
            this.ColumnDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnDate.FillWeight = 150F;
            this.ColumnDate.HeaderText = "Дата публикации";
            this.ColumnDate.MinimumWidth = 6;
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.ReadOnly = true;
            this.ColumnDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnDate.Width = 160;
            // 
            // ColumnName
            // 
            this.ColumnName.FillWeight = 300F;
            this.ColumnName.HeaderText = "Название статьи";
            this.ColumnName.MinimumWidth = 6;
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnUrl
            // 
            this.ColumnUrl.FillWeight = 150F;
            this.ColumnUrl.HeaderText = "Ссылка";
            this.ColumnUrl.MinimumWidth = 6;
            this.ColumnUrl.Name = "ColumnUrl";
            this.ColumnUrl.ReadOnly = true;
            this.ColumnUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TimerLoadSites
            // 
            this.TimerLoadSites.Enabled = true;
            this.TimerLoadSites.Interval = 500;
            this.TimerLoadSites.Tick += new System.EventHandler(this.TimerLoadSites_Tick);
            // 
            // LabelConnection
            // 
            this.LabelConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelConnection.AutoSize = true;
            this.LabelConnection.Location = new System.Drawing.Point(693, 26);
            this.LabelConnection.Name = "LabelConnection";
            this.LabelConnection.Size = new System.Drawing.Size(185, 20);
            this.LabelConnection.TabIndex = 5;
            this.LabelConnection.Text = "Состояние подключения:";
            // 
            // LabelConnectionState
            // 
            this.LabelConnectionState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelConnectionState.AutoSize = true;
            this.LabelConnectionState.Location = new System.Drawing.Point(884, 26);
            this.LabelConnectionState.Name = "LabelConnectionState";
            this.LabelConnectionState.Size = new System.Drawing.Size(54, 20);
            this.LabelConnectionState.TabIndex = 6;
            this.LabelConnectionState.Text = "запуск";
            // 
            // TimerWaitDots
            // 
            this.TimerWaitDots.Enabled = true;
            this.TimerWaitDots.Interval = 500;
            this.TimerWaitDots.Tick += new System.EventHandler(this.TimerLoadSitesDots_Tick);
            // 
            // LabelSites
            // 
            this.LabelSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSites.AutoSize = true;
            this.LabelSites.Location = new System.Drawing.Point(693, 72);
            this.LabelSites.Name = "LabelSites";
            this.LabelSites.Size = new System.Drawing.Size(123, 20);
            this.LabelSites.TabIndex = 7;
            this.LabelSites.Text = "Новостной сайт:";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.Location = new System.Drawing.Point(784, 185);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(94, 29);
            this.ButtonSave.TabIndex = 8;
            this.ButtonSave.Text = "Загрузить";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // NumericUpDownSave
            // 
            this.NumericUpDownSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericUpDownSave.Location = new System.Drawing.Point(693, 185);
            this.NumericUpDownSave.Maximum = new decimal(new int[] {
            42,
            0,
            0,
            0});
            this.NumericUpDownSave.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDownSave.Name = "NumericUpDownSave";
            this.NumericUpDownSave.Size = new System.Drawing.Size(58, 27);
            this.NumericUpDownSave.TabIndex = 9;
            this.NumericUpDownSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumericUpDownSave.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LabelSaveState
            // 
            this.LabelSaveState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSaveState.AutoSize = true;
            this.LabelSaveState.Location = new System.Drawing.Point(911, 189);
            this.LabelSaveState.Name = "LabelSaveState";
            this.LabelSaveState.Size = new System.Drawing.Size(27, 20);
            this.LabelSaveState.TabIndex = 10;
            this.LabelSaveState.Text = "---";
            // 
            // LabelSaveNews
            // 
            this.LabelSaveNews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSaveNews.AutoSize = true;
            this.LabelSaveNews.Location = new System.Drawing.Point(756, 153);
            this.LabelSaveNews.Name = "LabelSaveNews";
            this.LabelSaveNews.Size = new System.Drawing.Size(147, 20);
            this.LabelSaveNews.TabIndex = 11;
            this.LabelSaveNews.Text = "Последние новости";
            // 
            // LabelArchiveNews
            // 
            this.LabelArchiveNews.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LabelArchiveNews.AutoSize = true;
            this.LabelArchiveNews.Location = new System.Drawing.Point(756, 253);
            this.LabelArchiveNews.Name = "LabelArchiveNews";
            this.LabelArchiveNews.Size = new System.Drawing.Size(141, 20);
            this.LabelArchiveNews.TabIndex = 12;
            this.LabelArchiveNews.Text = "Архивные новости";
            // 
            // DateTimePickerLeft
            // 
            this.DateTimePickerLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DateTimePickerLeft.Checked = false;
            this.DateTimePickerLeft.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerLeft.Location = new System.Drawing.Point(693, 285);
            this.DateTimePickerLeft.Name = "DateTimePickerLeft";
            this.DateTimePickerLeft.ShowCheckBox = true;
            this.DateTimePickerLeft.Size = new System.Drawing.Size(123, 27);
            this.DateTimePickerLeft.TabIndex = 13;
            this.DateTimePickerLeft.Value = new System.DateTime(2020, 8, 9, 0, 0, 0, 0);
            // 
            // DateTimePickerRight
            // 
            this.DateTimePickerRight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DateTimePickerRight.Checked = false;
            this.DateTimePickerRight.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerRight.Location = new System.Drawing.Point(836, 285);
            this.DateTimePickerRight.Name = "DateTimePickerRight";
            this.DateTimePickerRight.ShowCheckBox = true;
            this.DateTimePickerRight.Size = new System.Drawing.Size(123, 27);
            this.DateTimePickerRight.TabIndex = 14;
            this.DateTimePickerRight.Value = new System.DateTime(2334, 1, 1, 0, 0, 0, 0);
            // 
            // TextBoxKeywords
            // 
            this.TextBoxKeywords.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TextBoxKeywords.Location = new System.Drawing.Point(693, 318);
            this.TextBoxKeywords.Name = "TextBoxKeywords";
            this.TextBoxKeywords.PlaceholderText = "Поиск по ключевым словам";
            this.TextBoxKeywords.Size = new System.Drawing.Size(266, 27);
            this.TextBoxKeywords.TabIndex = 15;
            // 
            // TextBoxEntities
            // 
            this.TextBoxEntities.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TextBoxEntities.Location = new System.Drawing.Point(693, 351);
            this.TextBoxEntities.Name = "TextBoxEntities";
            this.TextBoxEntities.PlaceholderText = "Поиск по сущностям";
            this.TextBoxEntities.Size = new System.Drawing.Size(266, 27);
            this.TextBoxEntities.TabIndex = 16;
            // 
            // ButtonNextPage
            // 
            this.ButtonNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonNextPage.Location = new System.Drawing.Point(768, 499);
            this.ButtonNextPage.Name = "ButtonNextPage";
            this.ButtonNextPage.Size = new System.Drawing.Size(34, 29);
            this.ButtonNextPage.TabIndex = 17;
            this.ButtonNextPage.Text = "▷";
            this.ButtonNextPage.UseVisualStyleBackColor = true;
            this.ButtonNextPage.Click += new System.EventHandler(this.ButtonNextPage_Click);
            // 
            // TextBoxPage
            // 
            this.TextBoxPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxPage.Location = new System.Drawing.Point(733, 500);
            this.TextBoxPage.MaxLength = 2;
            this.TextBoxPage.Name = "TextBoxPage";
            this.TextBoxPage.ReadOnly = true;
            this.TextBoxPage.Size = new System.Drawing.Size(29, 27);
            this.TextBoxPage.TabIndex = 18;
            this.TextBoxPage.Text = "1";
            this.TextBoxPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ButtonPrevPage
            // 
            this.ButtonPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPrevPage.Location = new System.Drawing.Point(693, 499);
            this.ButtonPrevPage.Name = "ButtonPrevPage";
            this.ButtonPrevPage.Size = new System.Drawing.Size(34, 29);
            this.ButtonPrevPage.TabIndex = 19;
            this.ButtonPrevPage.Text = "◁";
            this.ButtonPrevPage.UseVisualStyleBackColor = true;
            this.ButtonPrevPage.Click += new System.EventHandler(this.ButtonPrevPage_Click);
            // 
            // CheckBoxOldNews
            // 
            this.CheckBoxOldNews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxOldNews.AutoSize = true;
            this.CheckBoxOldNews.Checked = true;
            this.CheckBoxOldNews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxOldNews.Location = new System.Drawing.Point(915, 505);
            this.CheckBoxOldNews.Name = "CheckBoxOldNews";
            this.CheckBoxOldNews.Size = new System.Drawing.Size(18, 17);
            this.CheckBoxOldNews.TabIndex = 20;
            this.CheckBoxOldNews.UseVisualStyleBackColor = true;
            this.CheckBoxOldNews.CheckedChanged += new System.EventHandler(this.CheckBoxOldNews_CheckedChanged);
            // 
            // LabelOldNews
            // 
            this.LabelOldNews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelOldNews.AutoSize = true;
            this.LabelOldNews.Location = new System.Drawing.Point(895, 447);
            this.LabelOldNews.Name = "LabelOldNews";
            this.LabelOldNews.Size = new System.Drawing.Size(67, 40);
            this.LabelOldNews.TabIndex = 21;
            this.LabelOldNews.Text = "Сначала\r\nстарые";
            this.LabelOldNews.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelCount
            // 
            this.LabelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelCount.AutoSize = true;
            this.LabelCount.Location = new System.Drawing.Point(805, 447);
            this.LabelCount.Name = "LabelCount";
            this.LabelCount.Size = new System.Drawing.Size(77, 40);
            this.LabelCount.TabIndex = 22;
            this.LabelCount.Text = "Размер\r\nстраницы";
            this.LabelCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelCurrentPage
            // 
            this.LabelCurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelCurrentPage.AutoSize = true;
            this.LabelCurrentPage.Location = new System.Drawing.Point(713, 447);
            this.LabelCurrentPage.Name = "LabelCurrentPage";
            this.LabelCurrentPage.Size = new System.Drawing.Size(74, 40);
            this.LabelCurrentPage.TabIndex = 23;
            this.LabelCurrentPage.Text = "Текущая\r\nстраница";
            this.LabelCurrentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.LabelCurrentPage);
            this.Controls.Add(this.LabelCount);
            this.Controls.Add(this.LabelOldNews);
            this.Controls.Add(this.CheckBoxOldNews);
            this.Controls.Add(this.ButtonPrevPage);
            this.Controls.Add(this.TextBoxPage);
            this.Controls.Add(this.ButtonNextPage);
            this.Controls.Add(this.TextBoxEntities);
            this.Controls.Add(this.TextBoxKeywords);
            this.Controls.Add(this.DateTimePickerRight);
            this.Controls.Add(this.DateTimePickerLeft);
            this.Controls.Add(this.LabelArchiveNews);
            this.Controls.Add(this.LabelSaveNews);
            this.Controls.Add(this.LabelSaveState);
            this.Controls.Add(this.NumericUpDownSave);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.LabelSites);
            this.Controls.Add(this.LabelConnectionState);
            this.Controls.Add(this.LabelConnection);
            this.Controls.Add(this.DataGridViewNews);
            this.Controls.Add(this.ButtonDownloadList);
            this.Controls.Add(this.ComboBoxCount);
            this.Controls.Add(this.ComboBoxSites);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Новостной архив";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewNews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownSave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxSites;
        private System.Windows.Forms.ComboBox ComboBoxCount;
        private System.Windows.Forms.Button ButtonDownloadList;
        private System.Windows.Forms.DataGridView DataGridViewNews;
        private System.Windows.Forms.Timer TimerLoadSites;
        private System.Windows.Forms.Label LabelConnection;
        private System.Windows.Forms.Label LabelConnectionState;
        private System.Windows.Forms.Timer TimerWaitDots;
        private System.Windows.Forms.Label LabelSites;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.NumericUpDown NumericUpDownSave;
        private System.Windows.Forms.Label LabelSaveState;
        private System.Windows.Forms.Label LabelSaveNews;
        private System.Windows.Forms.Label LabelArchiveNews;
        private System.Windows.Forms.DateTimePicker DateTimePickerLeft;
        private System.Windows.Forms.DateTimePicker DateTimePickerRight;
        private System.Windows.Forms.TextBox TextBoxKeywords;
        private System.Windows.Forms.TextBox TextBoxEntities;
        private System.Windows.Forms.Button ButtonNextPage;
        private System.Windows.Forms.TextBox TextBoxPage;
        private System.Windows.Forms.Button ButtonPrevPage;
        private System.Windows.Forms.CheckBox CheckBoxOldNews;
        private System.Windows.Forms.Label LabelOldNews;
        private System.Windows.Forms.Label LabelCount;
        private System.Windows.Forms.Label LabelCurrentPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUrl;
    }
}

