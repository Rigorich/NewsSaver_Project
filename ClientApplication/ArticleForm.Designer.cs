
namespace ClientApplication
{
    partial class ArticleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RichTextBoxText = new System.Windows.Forms.RichTextBox();
            this.RichTextBoxName = new System.Windows.Forms.RichTextBox();
            this.TextBoxUrl = new System.Windows.Forms.TextBox();
            this.TextBoxDate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RichTextBoxText
            // 
            this.RichTextBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBoxText.Location = new System.Drawing.Point(12, 104);
            this.RichTextBoxText.Name = "RichTextBoxText";
            this.RichTextBoxText.ReadOnly = true;
            this.RichTextBoxText.Size = new System.Drawing.Size(776, 334);
            this.RichTextBoxText.TabIndex = 0;
            this.RichTextBoxText.Text = "";
            // 
            // RichTextBoxName
            // 
            this.RichTextBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBoxName.Location = new System.Drawing.Point(12, 46);
            this.RichTextBoxName.Name = "RichTextBoxName";
            this.RichTextBoxName.ReadOnly = true;
            this.RichTextBoxName.Size = new System.Drawing.Size(776, 52);
            this.RichTextBoxName.TabIndex = 1;
            this.RichTextBoxName.Text = "";
            // 
            // TextBoxUrl
            // 
            this.TextBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxUrl.Location = new System.Drawing.Point(12, 13);
            this.TextBoxUrl.Name = "TextBoxUrl";
            this.TextBoxUrl.ReadOnly = true;
            this.TextBoxUrl.Size = new System.Drawing.Size(651, 27);
            this.TextBoxUrl.TabIndex = 2;
            // 
            // TextBoxDate
            // 
            this.TextBoxDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDate.Location = new System.Drawing.Point(669, 13);
            this.TextBoxDate.Name = "TextBoxDate";
            this.TextBoxDate.ReadOnly = true;
            this.TextBoxDate.Size = new System.Drawing.Size(119, 27);
            this.TextBoxDate.TabIndex = 3;
            // 
            // ArticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TextBoxDate);
            this.Controls.Add(this.TextBoxUrl);
            this.Controls.Add(this.RichTextBoxName);
            this.Controls.Add(this.RichTextBoxText);
            this.Name = "ArticleForm";
            this.Text = "Архивная статья";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextBoxText;
        private System.Windows.Forms.RichTextBox RichTextBoxName;
        private System.Windows.Forms.TextBox TextBoxUrl;
        private System.Windows.Forms.TextBox TextBoxDate;
    }
}