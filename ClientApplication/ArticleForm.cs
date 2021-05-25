using BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class ArticleForm : Form
    {
        public ArticleForm(NewsArticle article)
        {
            InitializeComponent();
            TextBoxUrl.Text = article.URL;
            TextBoxDate.Text = article.Date.ToString("g");
            RichTextBoxName.Text = article.Name;
            RichTextBoxText.Text = article.Text;
        }
    }
}
