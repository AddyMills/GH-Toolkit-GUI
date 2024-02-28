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
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
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
            FileButton.Image = (Image)resources.GetObject("FileButton.Image");
            FileButton.ImageTransparentColor = Color.Magenta;
            FileButton.Name = "FileButton";
            FileButton.ShowDropDownArrow = false;
            FileButton.Size = new Size(29, 22);
            FileButton.Text = "File";
            FileButton.ToolTipText = "File";
            // 
            // button1
            // 
            button1.Location = new Point(12, 150);
            button1.Name = "button1";
            button1.Size = new Size(379, 23);
            button1.TabIndex = 1;
            button1.Text = "Compile a Song";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MasterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 450);
            Controls.Add(button1);
            Controls.Add(toolStrip1);
            Name = "MasterForm";
            Text = "Form1";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton FileButton;
        private Button button1;
    }
}
