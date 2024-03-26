using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GH_Toolkit_GUI
{
    public enum TabType
    {
        CompileSong,
        SonglistManager
    }
    public partial class ProgramSettings : Form
    {
        private UserPreferences Pref = UserPreferences.Default;
        public ProgramSettings(TabType tabIndex = 0)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tabControl1.SelectedIndex = (int)tabIndex;
            PopulateData();
        }
        private void PopulateData()
        {
            CompilePopup.Checked = Pref.ShowPostCompile;
            PreviewFadeIn.Value = Pref.PreviewFadeIn;
            PreviewFadeOut.Value = Pref.PreviewFadeOut;
            WtModsFolder.Text = Pref.WtModsFolder;
            Gh3FolderPath.Text = Pref.Gh3FolderPath;
            GhaFolderPath.Text = Pref.GhaFolderPath;
        }
        private void SaveData()
        {
            Pref.ShowPostCompile = CompilePopup.Checked;
            Pref.PreviewFadeIn = PreviewFadeIn.Value;
            Pref.PreviewFadeOut = PreviewFadeOut.Value;
            Pref.WtModsFolder = WtModsFolder.Text;
            Pref.Gh3FolderPath = Gh3FolderPath.Text;
            Pref.GhaFolderPath = GhaFolderPath.Text;
            Pref.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveData();
            Close();
        }
    }
}
