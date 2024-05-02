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
            OverrideBeatLines.Checked = Pref.OverrideBeatLines;
            PreviewFadeIn.Value = Pref.PreviewFadeIn;
            PreviewFadeOut.Value = Pref.PreviewFadeOut;
            EncryptAudio.Checked = Pref.EncryptAudio;
            WtModsFolder.Text = Pref.WtModsFolder;
            Gh3FolderPath.Text = Pref.Gh3FolderPath;
            GhaFolderPath.Text = Pref.GhaFolderPath;
            SongManagerDeleteSongs.Checked = Pref.SongManagerDeleteSongs;
        }
        private void SaveData()
        {
            Pref.ShowPostCompile = CompilePopup.Checked;
            Pref.OverrideBeatLines = OverrideBeatLines.Checked;
            Pref.PreviewFadeIn = PreviewFadeIn.Value;
            Pref.PreviewFadeOut = PreviewFadeOut.Value;
            Pref.EncryptAudio = EncryptAudio.Checked;
            Pref.WtModsFolder = WtModsFolder.Text;
            Pref.SongManagerDeleteSongs = SongManagerDeleteSongs.Checked;
            // Do not save Gh3/Gha folder paths as they are read-only fields
            Pref.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveData();
            Close();
        }
    }
}
