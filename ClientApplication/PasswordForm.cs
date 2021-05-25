using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class PasswordForm : Form
    {
        private readonly MainForm ParentMainForm;

        public PasswordForm(MainForm form)
        {
            InitializeComponent();
            ParentMainForm = form;
            LabelQuestion.Text = Localization.Question;
        }

        private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ParentMainForm.Password = TextBoxPassword.Text.Split(' ')[^1];
                this.Close();
            }
        }
    }
}
