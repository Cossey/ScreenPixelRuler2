namespace ScreenPixelRuler2
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            logoPictureBox = new System.Windows.Forms.PictureBox();
            labelProductName = new System.Windows.Forms.Label();
            labelVersion = new System.Windows.Forms.Label();
            textBoxLicense = new System.Windows.Forms.TextBox();
            okButton = new System.Windows.Forms.Button();
            linkLinks = new System.Windows.Forms.LinkLabel();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(textBoxLicense, 1, 3);
            tableLayoutPanel.Controls.Add(okButton, 1, 4);
            tableLayoutPanel.Controls.Add(linkLinks, 1, 2);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(11, 13);
            tableLayoutPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.Size = new System.Drawing.Size(557, 410);
            tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            logoPictureBox.Image = (System.Drawing.Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new System.Drawing.Point(5, 4);
            logoPictureBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 5);
            logoPictureBox.Size = new System.Drawing.Size(40, 402);
            logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProductName.Location = new System.Drawing.Point(58, 0);
            labelProductName.Margin = new System.Windows.Forms.Padding(8, 0, 5, 0);
            labelProductName.MaximumSize = new System.Drawing.Size(0, 27);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new System.Drawing.Size(494, 27);
            labelProductName.TabIndex = 19;
            labelProductName.Text = "Product Name";
            labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVersion.Location = new System.Drawing.Point(58, 32);
            labelVersion.Margin = new System.Windows.Forms.Padding(8, 0, 5, 0);
            labelVersion.MaximumSize = new System.Drawing.Size(0, 27);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new System.Drawing.Size(494, 27);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "Version";
            labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxLicense
            // 
            textBoxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxLicense.Location = new System.Drawing.Point(58, 100);
            textBoxLicense.Margin = new System.Windows.Forms.Padding(8, 4, 5, 4);
            textBoxLicense.Multiline = true;
            textBoxLicense.Name = "textBoxLicense";
            textBoxLicense.ReadOnly = true;
            textBoxLicense.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxLicense.Size = new System.Drawing.Size(494, 262);
            textBoxLicense.TabIndex = 23;
            textBoxLicense.TabStop = false;
            textBoxLicense.Text = "License";
            // 
            // okButton
            // 
            okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            okButton.Location = new System.Drawing.Point(451, 370);
            okButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(101, 36);
            okButton.TabIndex = 24;
            okButton.Text = "&OK";
            // 
            // linkLinks
            // 
            linkLinks.AutoSize = true;
            linkLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            linkLinks.Location = new System.Drawing.Point(58, 64);
            linkLinks.Margin = new System.Windows.Forms.Padding(8, 0, 5, 0);
            linkLinks.MaximumSize = new System.Drawing.Size(0, 27);
            linkLinks.Name = "linkLinks";
            linkLinks.Size = new System.Drawing.Size(494, 27);
            linkLinks.TabIndex = 25;
            linkLinks.TabStop = true;
            linkLinks.Text = "Links";
            // 
            // About
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(579, 436);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About";
            Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "AboutBox1";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxLicense;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.LinkLabel linkLinks;
    }
}
