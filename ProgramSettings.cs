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
        private bool hasUnsavedChanges = false;
        private string originalTitle;
        public ProgramSettings(TabType tabIndex = 0)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tabControl1.SelectedIndex = (int)tabIndex;
            PopulateData();
            AttachChangeHandlers();
            originalTitle = Text; // Store the original title
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
            OnyxCliFolder.Text = Pref.OnyxCliPath;

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
            Pref.OnyxCliPath = OnyxCliFolder.Text;

            Pref.SongManagerDeleteSongs = SongManagerDeleteSongs.Checked;
            // Do not save Gh3/Gha folder paths as they are read-only fields
            Pref.Save();
            hasUnsavedChanges = false; // Reset flag after saving
            UpdateTitle();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (hasUnsavedChanges)
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to close without saving?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancel the close operation
                }
            }
            base.OnFormClosing(e);
        }

        private void AttachChangeHandlers()
        {
            CompilePopup.CheckedChanged += (s, e) => SetUnsavedChanges(true);
            OverrideBeatLines.CheckedChanged += (s, e) => SetUnsavedChanges(true);
            PreviewFadeIn.ValueChanged += (s, e) => SetUnsavedChanges(true);
            PreviewFadeOut.ValueChanged += (s, e) => SetUnsavedChanges(true);
            EncryptAudio.CheckedChanged += (s, e) => SetUnsavedChanges(true);
            WtModsFolder.TextChanged += (s, e) => SetUnsavedChanges(true);
            Gh3FolderPath.TextChanged += (s, e) => SetUnsavedChanges(true);
            GhaFolderPath.TextChanged += (s, e) => SetUnsavedChanges(true);
            OnyxCliFolder.TextChanged += (s, e) => SetUnsavedChanges(true);
            SongManagerDeleteSongs.CheckedChanged += (s, e) => SetUnsavedChanges(true);
        }

        private void SetUnsavedChanges(bool value)
        {
            hasUnsavedChanges = value;
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            if (hasUnsavedChanges)
            {
                Text = $"{originalTitle}*";
            }
            else
            {
                Text = originalTitle;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveData();
            Close();
        }

    }
}
