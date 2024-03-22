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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterForm));
            toolStrip1 = new ToolStrip();
            FileButton = new ToolStripDropDownButton();
            button1 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            button2 = new Button();
            label1 = new Label();
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
            toolStrip1.Size = new Size(461, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // FileButton
            // 
            FileButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            FileButton.ForeColor = SystemColors.ControlText;
            FileButton.Image = (Image)resources.GetObject("FileButton.Image");
            FileButton.ImageTransparentColor = Color.Magenta;
            FileButton.Name = "FileButton";
            FileButton.ShowDropDownArrow = false;
            FileButton.Size = new Size(36, 24);
            FileButton.Text = "File";
            FileButton.ToolTipText = "File";
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(3, 4);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(429, 108);
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
            tableLayoutPanel1.Controls.Add(button2, 0, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 30);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(435, 253);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(3, 139);
            button2.Name = "button2";
            button2.Size = new Size(429, 111);
            button2.TabIndex = 2;
            button2.Text = "Songlist Manager";
            button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 116);
            label1.Name = "label1";
            label1.Size = new Size(429, 20);
            label1.TabIndex = 3;
            label1.Text = "PC Tools";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MasterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(461, 600);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 4, 3, 4);
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
        private Button button2;
        private Label label1;
    }
}
