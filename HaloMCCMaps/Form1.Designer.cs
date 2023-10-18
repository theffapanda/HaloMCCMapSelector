namespace HaloMCCMaps
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            checkedListBox1 = new CheckedListBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            ceLabel = new Label();
            tabPage2 = new TabPage();
            h2Label = new Label();
            tabPage3 = new TabPage();
            h3Label = new Label();
            pathLabel = new Label();
            installPath = new Label();
            pictureBox1 = new PictureBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(214, 36);
            button1.TabIndex = 0;
            button1.Text = "Browse for Installation Folder";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(12, 71);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(214, 760);
            checkedListBox1.TabIndex = 1;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(278, 34);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(246, 797);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(ceLabel);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(238, 769);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // ceLabel
            // 
            ceLabel.AutoSize = true;
            ceLabel.Location = new Point(6, 3);
            ceLabel.Name = "ceLabel";
            ceLabel.Size = new Size(38, 15);
            ceLabel.TabIndex = 0;
            ceLabel.Text = "label1";
            ceLabel.Click += label1_Click;
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(h2Label);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(238, 769);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // h2Label
            // 
            h2Label.AutoSize = true;
            h2Label.Location = new Point(6, 3);
            h2Label.Name = "h2Label";
            h2Label.Size = new Size(38, 15);
            h2Label.TabIndex = 0;
            h2Label.Text = "label1";
            // 
            // tabPage3
            // 
            tabPage3.AutoScroll = true;
            tabPage3.Controls.Add(h3Label);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(238, 769);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // h3Label
            // 
            h3Label.AutoSize = true;
            h3Label.Location = new Point(6, 3);
            h3Label.Name = "h3Label";
            h3Label.Size = new Size(32, 15);
            h3Label.TabIndex = 0;
            h3Label.Text = "label";
            h3Label.Click += label1_Click_1;
            // 
            // pathLabel
            // 
            pathLabel.AutoSize = true;
            pathLabel.Location = new Point(288, 654);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new Size(0, 15);
            pathLabel.TabIndex = 3;
            pathLabel.Visible = false;
            // 
            // installPath
            // 
            installPath.AutoSize = true;
            installPath.Location = new Point(278, 14);
            installPath.Name = "installPath";
            installPath.Size = new Size(62, 15);
            installPath.TabIndex = 4;
            installPath.Text = "installPath";
            installPath.Click += label1_Click_2;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.brave_0ETtJE95R5;
            pictureBox1.InitialImage = Properties.Resources.brave_0ETtJE95R5;
            pictureBox1.Location = new Point(550, 130);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(632, 593);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1204, 856);
            Controls.Add(pictureBox1);
            Controls.Add(installPath);
            Controls.Add(pathLabel);
            Controls.Add(tabControl1);
            Controls.Add(checkedListBox1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "HaloMCCMapSelector";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private CheckedListBox checkedListBox1;
        private TextBox textBox1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label ceLabel;
        private Label h2Label;
        private Label h3Label;
        private Label pathLabel;
        private Label installPath;
        private PictureBox pictureBox1;
    }
}