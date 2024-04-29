namespace GH_Toolkit_GUI
{
    partial class ImportSGH
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
            tableLayoutPanel1 = new TableLayoutPanel();
            songList = new CheckedListBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            consoleLabel = new Label();
            ConsoleSelect = new ComboBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            importSghFile = new Button();
            convertButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(songList, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(419, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // songList
            // 
            songList.CheckOnClick = true;
            songList.Dock = DockStyle.Fill;
            songList.FormattingEnabled = true;
            songList.Location = new Point(3, 3);
            songList.Name = "songList";
            songList.ScrollAlwaysVisible = true;
            songList.Size = new Size(413, 194);
            songList.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(consoleLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(ConsoleSelect, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 203);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(413, 44);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // consoleLabel
            // 
            consoleLabel.Anchor = AnchorStyles.Left;
            consoleLabel.AutoSize = true;
            consoleLabel.Location = new Point(3, 14);
            consoleLabel.Name = "consoleLabel";
            consoleLabel.Size = new Size(53, 15);
            consoleLabel.TabIndex = 0;
            consoleLabel.Text = "Console:";
            // 
            // ConsoleSelect
            // 
            ConsoleSelect.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ConsoleSelect.FormattingEnabled = true;
            ConsoleSelect.Items.AddRange(new object[] { "PC", "Xbox 360", "PS3" });
            ConsoleSelect.Location = new Point(62, 10);
            ConsoleSelect.Name = "ConsoleSelect";
            ConsoleSelect.Size = new Size(348, 23);
            ConsoleSelect.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(importSghFile, 0, 0);
            tableLayoutPanel3.Controls.Add(convertButton, 0, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 253);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Size = new Size(413, 194);
            tableLayoutPanel3.TabIndex = 3;
            // 
            // importSghFile
            // 
            importSghFile.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            importSghFile.Location = new Point(3, 20);
            importSghFile.Name = "importSghFile";
            importSghFile.Size = new Size(407, 23);
            importSghFile.TabIndex = 1;
            importSghFile.Text = "Import SGH File";
            importSghFile.UseVisualStyleBackColor = true;
            importSghFile.Click += importSgh_Click;
            // 
            // convertButton
            // 
            convertButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            convertButton.BackColor = Color.OliveDrab;
            convertButton.ForeColor = SystemColors.ButtonFace;
            convertButton.Location = new Point(3, 149);
            convertButton.Name = "convertButton";
            convertButton.Size = new Size(407, 23);
            convertButton.TabIndex = 3;
            convertButton.Text = "Import!";
            convertButton.UseVisualStyleBackColor = false;
            convertButton.Click += convertButton_Click;
            // 
            // ImportSGH
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "ImportSGH";
            Text = "Import SGH Archive";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private CheckedListBox songList;
        private Button importSghFile;
        private TableLayoutPanel tableLayoutPanel2;
        private Label consoleLabel;
        private ComboBox ConsoleSelect;
        private TableLayoutPanel tableLayoutPanel3;
        private Button convertButton;
    }
}