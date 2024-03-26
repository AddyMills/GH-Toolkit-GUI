namespace GH_Toolkit_GUI
{
    partial class ProgramSettings
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            CompilePopup = new CheckBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            label6 = new Label();
            label7 = new Label();
            PreviewFadeIn = new NumericUpDown();
            PreviewFadeOut = new NumericUpDown();
            WtModsFolder = new TextBox();
            Gh3FolderPath = new TextBox();
            GhaFolderPath = new TextBox();
            tabPage2 = new TabPage();
            button1 = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PreviewFadeIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PreviewFadeOut).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Top;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 506);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 473);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Compile a Song";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.Controls.Add(label2, 0, 4);
            tableLayoutPanel1.Controls.Add(label1, 0, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 0);
            tableLayoutPanel1.Controls.Add(CompilePopup, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 1);
            tableLayoutPanel1.Controls.Add(WtModsFolder, 1, 2);
            tableLayoutPanel1.Controls.Add(Gh3FolderPath, 1, 3);
            tableLayoutPanel1.Controls.Add(GhaFolderPath, 1, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6673622F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6673622F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6673622F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6673622F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6673622F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6631966F));
            tableLayoutPanel1.Size = new Size(786, 467);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 308);
            label2.Name = "label2";
            label2.Size = new Size(269, 77);
            label2.TabIndex = 5;
            label2.Text = "GHA Folder Path (PC)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 231);
            label1.Name = "label1";
            label1.Size = new Size(269, 77);
            label1.TabIndex = 4;
            label1.Text = "GH3 Folder Path (PC)";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 154);
            label3.Name = "label3";
            label3.Size = new Size(269, 77);
            label3.TabIndex = 3;
            label3.Text = "GHWT MODS Folder Path";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 77);
            label4.Name = "label4";
            label4.Size = new Size(269, 77);
            label4.TabIndex = 2;
            label4.Text = "Preview Fade Values";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(269, 77);
            label5.TabIndex = 6;
            label5.Text = "Show Compile Success Popup";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CompilePopup
            // 
            CompilePopup.AutoSize = true;
            CompilePopup.Dock = DockStyle.Fill;
            CompilePopup.Location = new Point(278, 3);
            CompilePopup.Name = "CompilePopup";
            CompilePopup.Size = new Size(505, 71);
            CompilePopup.TabIndex = 1;
            CompilePopup.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(label6, 0, 0);
            tableLayoutPanel2.Controls.Add(label7, 2, 0);
            tableLayoutPanel2.Controls.Add(PreviewFadeOut, 3, 0);
            tableLayoutPanel2.Controls.Add(PreviewFadeIn, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(275, 77);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(511, 77);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(121, 77);
            label6.TabIndex = 0;
            label6.Text = "Fade In:";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(257, 0);
            label7.Name = "label7";
            label7.Size = new Size(121, 77);
            label7.TabIndex = 1;
            label7.Text = "Fade Out:";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PreviewFadeIn
            // 
            PreviewFadeIn.Anchor = AnchorStyles.Left;
            PreviewFadeIn.DecimalPlaces = 2;
            PreviewFadeIn.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            PreviewFadeIn.Location = new Point(130, 25);
            PreviewFadeIn.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            PreviewFadeIn.Name = "PreviewFadeIn";
            PreviewFadeIn.Size = new Size(121, 27);
            PreviewFadeIn.TabIndex = 2;
            PreviewFadeIn.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // PreviewFadeOut
            // 
            PreviewFadeOut.Anchor = AnchorStyles.Left;
            PreviewFadeOut.DecimalPlaces = 2;
            PreviewFadeOut.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            PreviewFadeOut.Location = new Point(384, 25);
            PreviewFadeOut.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            PreviewFadeOut.Name = "PreviewFadeOut";
            PreviewFadeOut.Size = new Size(124, 27);
            PreviewFadeOut.TabIndex = 3;
            PreviewFadeOut.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // WtModsFolder
            // 
            WtModsFolder.Dock = DockStyle.Fill;
            WtModsFolder.Location = new Point(278, 177);
            WtModsFolder.Margin = new Padding(3, 23, 3, 3);
            WtModsFolder.Name = "WtModsFolder";
            WtModsFolder.Size = new Size(505, 27);
            WtModsFolder.TabIndex = 8;
            // 
            // Gh3FolderPath
            // 
            Gh3FolderPath.Dock = DockStyle.Fill;
            Gh3FolderPath.Location = new Point(278, 254);
            Gh3FolderPath.Margin = new Padding(3, 23, 3, 3);
            Gh3FolderPath.Name = "Gh3FolderPath";
            Gh3FolderPath.ReadOnly = true;
            Gh3FolderPath.Size = new Size(505, 27);
            Gh3FolderPath.TabIndex = 9;
            // 
            // GhaFolderPath
            // 
            GhaFolderPath.Dock = DockStyle.Fill;
            GhaFolderPath.Location = new Point(278, 331);
            GhaFolderPath.Margin = new Padding(3, 23, 3, 3);
            GhaFolderPath.Name = "GhaFolderPath";
            GhaFolderPath.ReadOnly = true;
            GhaFolderPath.Size = new Size(505, 27);
            GhaFolderPath.TabIndex = 10;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 473);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Songlist Manager";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(666, 512);
            button1.Name = "button1";
            button1.Size = new Size(122, 29);
            button1.TabIndex = 1;
            button1.Text = "Save & Close";
            button1.UseMnemonic = false;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ProgramSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 553);
            Controls.Add(button1);
            Controls.Add(tabControl1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgramSettings";
            ShowIcon = false;
            Text = "Settings & Information";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PreviewFadeIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)PreviewFadeOut).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private CheckBox CompilePopup;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label6;
        private Label label7;
        private NumericUpDown PreviewFadeIn;
        private NumericUpDown PreviewFadeOut;
        private TextBox WtModsFolder;
        private TextBox Gh3FolderPath;
        private TextBox GhaFolderPath;
        private Button button1;
    }
}