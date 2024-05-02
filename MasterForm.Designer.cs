namespace GH_Toolkit_GUI
{
    partial class MasterForm
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
            toolStrip1 = new ToolStrip();
            FileButton = new ToolStripDropDownButton();
            button1 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            importSGH = new Button();
            label1 = new Label();
            songlistManagerButton = new Button();
            consoleOutput = new TextBox();
            toolStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { FileButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(403, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // FileButton
            // 
            FileButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            FileButton.ForeColor = SystemColors.ControlText;
            FileButton.ImageTransparentColor = Color.Magenta;
            FileButton.Name = "FileButton";
            FileButton.ShowDropDownArrow = false;
            FileButton.Size = new Size(29, 22);
            FileButton.Text = "File";
            FileButton.ToolTipText = "File";
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(375, 52);
            button1.TabIndex = 1;
            button1.Text = "Compile a Song";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(button1, 0, 0);
            tableLayoutPanel1.Controls.Add(importSGH, 0, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(songlistManagerButton, 0, 3);
            tableLayoutPanel1.Location = new Point(10, 22);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(381, 190);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // importSGH
            // 
            importSGH.Dock = DockStyle.Fill;
            importSGH.Location = new Point(3, 75);
            importSGH.Margin = new Padding(3, 2, 3, 2);
            importSGH.Name = "importSGH";
            importSGH.Size = new Size(375, 54);
            importSGH.TabIndex = 2;
            importSGH.Text = "Import SGH Archive";
            importSGH.UseVisualStyleBackColor = true;
            importSGH.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 58);
            label1.Name = "label1";
            label1.Size = new Size(375, 15);
            label1.TabIndex = 3;
            label1.Text = "Other Tools";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // songlistManagerButton
            // 
            songlistManagerButton.AutoSize = true;
            songlistManagerButton.Dock = DockStyle.Fill;
            songlistManagerButton.Location = new Point(3, 134);
            songlistManagerButton.Name = "songlistManagerButton";
            songlistManagerButton.Size = new Size(375, 53);
            songlistManagerButton.TabIndex = 4;
            songlistManagerButton.Text = "PC Songlist Manager";
            songlistManagerButton.UseVisualStyleBackColor = true;
            songlistManagerButton.Click += songlistManagerClick;
            // 
            // consoleOutput
            // 
            consoleOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            consoleOutput.Location = new Point(10, 217);
            consoleOutput.Multiline = true;
            consoleOutput.Name = "consoleOutput";
            consoleOutput.ReadOnly = true;
            consoleOutput.ScrollBars = ScrollBars.Vertical;
            consoleOutput.Size = new Size(381, 221);
            consoleOutput.TabIndex = 3;
            // 
            // MasterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 450);
            Controls.Add(consoleOutput);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(toolStrip1);
            Name = "MasterForm";
            Text = "Guitar Hero Toolkit";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton FileButton;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button importSGH;
        private TextBox consoleOutput;
        private Label label1;
        private Button songlistManagerButton;
    }
}
