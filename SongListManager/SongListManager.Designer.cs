namespace GH_Toolkit_GUI
{
    partial class SongListManager
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
            groupBox1 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            gh3Radio = new RadioButton();
            ghaRadio = new RadioButton();
            tableLayoutPanel3 = new TableLayoutPanel();
            deleteSelected = new Button();
            loadSetlist = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            selectAllButton = new Button();
            selectNoneButton = new Button();
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(songList, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel1.Location = new Point(0, 28);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 39.9995F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.999875F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0003147F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0003147F));
            tableLayoutPanel1.Size = new Size(442, 442);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // songList
            // 
            songList.CheckOnClick = true;
            songList.Dock = DockStyle.Fill;
            songList.FormattingEnabled = true;
            songList.Location = new Point(3, 3);
            songList.Name = "songList";
            songList.Size = new Size(436, 170);
            songList.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 223);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(436, 104);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Game";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(gh3Radio, 0, 0);
            tableLayoutPanel2.Controls.Add(ghaRadio, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(430, 82);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // gh3Radio
            // 
            gh3Radio.Anchor = AnchorStyles.Left;
            gh3Radio.AutoSize = true;
            gh3Radio.Location = new Point(3, 31);
            gh3Radio.Name = "gh3Radio";
            gh3Radio.Size = new Size(48, 19);
            gh3Radio.TabIndex = 0;
            gh3Radio.TabStop = true;
            gh3Radio.Text = "GH3";
            gh3Radio.UseVisualStyleBackColor = true;
            // 
            // ghaRadio
            // 
            ghaRadio.Anchor = AnchorStyles.Left;
            ghaRadio.AutoSize = true;
            ghaRadio.Location = new Point(218, 31);
            ghaRadio.Name = "ghaRadio";
            ghaRadio.Size = new Size(50, 19);
            ghaRadio.TabIndex = 1;
            ghaRadio.TabStop = true;
            ghaRadio.Text = "GHA";
            ghaRadio.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoScroll = true;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(deleteSelected, 0, 1);
            tableLayoutPanel3.Controls.Add(loadSetlist, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 333);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(436, 106);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // deleteSelected
            // 
            deleteSelected.BackColor = Color.Firebrick;
            deleteSelected.ForeColor = SystemColors.ButtonHighlight;
            deleteSelected.Location = new Point(3, 56);
            deleteSelected.Name = "deleteSelected";
            deleteSelected.Size = new Size(430, 23);
            deleteSelected.TabIndex = 1;
            deleteSelected.Text = "Delete Selected";
            deleteSelected.UseVisualStyleBackColor = false;
            deleteSelected.Click += deleteSelected_Click;
            // 
            // loadSetlist
            // 
            loadSetlist.Location = new Point(3, 3);
            loadSetlist.Name = "loadSetlist";
            loadSetlist.RightToLeft = RightToLeft.No;
            loadSetlist.Size = new Size(430, 23);
            loadSetlist.TabIndex = 0;
            loadSetlist.Text = "Load Setlist";
            loadSetlist.UseVisualStyleBackColor = true;
            loadSetlist.Click += loadSetlist_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(selectAllButton, 0, 0);
            tableLayoutPanel4.Controls.Add(selectNoneButton, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 179);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(436, 38);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // selectAllButton
            // 
            selectAllButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            selectAllButton.Location = new Point(3, 3);
            selectAllButton.Name = "selectAllButton";
            selectAllButton.Size = new Size(212, 23);
            selectAllButton.TabIndex = 0;
            selectAllButton.Text = "Select All";
            selectAllButton.UseVisualStyleBackColor = true;
            selectAllButton.Click += selectAllButton_Click;
            // 
            // selectNoneButton
            // 
            selectNoneButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            selectNoneButton.Location = new Point(221, 3);
            selectNoneButton.Name = "selectNoneButton";
            selectNoneButton.Size = new Size(212, 23);
            selectNoneButton.TabIndex = 1;
            selectNoneButton.Text = "Select None";
            selectNoneButton.UseVisualStyleBackColor = true;
            selectNoneButton.Click += selectNoneButton_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(442, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(49, 22);
            toolStripLabel1.Text = "Settings";
            toolStripLabel1.Click += toolStripLabel1_Click;
            // 
            // SongListManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(442, 482);
            Controls.Add(toolStrip1);
            Controls.Add(tableLayoutPanel1);
            Name = "SongListManager";
            Text = "Song List Manager";
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private CheckedListBox songList;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private RadioButton gh3Radio;
        private RadioButton ghaRadio;
        private TableLayoutPanel tableLayoutPanel3;
        private Button deleteSelected;
        private Button loadSetlist;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private TableLayoutPanel tableLayoutPanel4;
        private Button selectAllButton;
        private Button selectNoneButton;
    }
}