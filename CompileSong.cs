using GH_Toolkit_Core.PAK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GH_Toolkit_GUI.Ghproj;
using static GH_Toolkit_Core.QB.QBConstants;
using GH_Toolkit_Core.Audio;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Policy;

namespace GH_Toolkit_GUI
{
    public partial class CompileSong : Form
    {
        private int ghprojVersion = 1;
        private string ExeLocation;
        private string ExeDirectory;
        private string DefaultTemplateFolder;
        private string DefaultTemplatePath;
        private string StartupProject;

        // Dictionary to hold the selected genre for each game
        private Dictionary<string, string> gameSelectedGenres = new Dictionary<string, string>();
        // Dictionary to hold the compile folder path for each platform and each game
        private Dictionary<string, Dictionary<string, string>> gamePlatformCompilePaths = new Dictionary<string, Dictionary<string, string>>();

        private RadioButton lastCheckedRadioButton = null;
        private string audioFileFilter = "Audio files (*.mp3, *.ogg, *.flac, *.wav)|*.mp3;*.ogg;*.flac;*.wav|All files (*.*)|*.*";
        private string midiFileFilter = "MIDI files (*.mid)|*.mid|All files (*.*)|*.*";
        private string qFileFilter = "Q files (*.q)|*.q|All files (*.*)|*.*";
        private string ghprojFileFilter = "GHProj files (*.ghproj)|*.ghproj|All files (*.*)|*.*";

        private Dictionary<string, TabPage> tabPageDict = new Dictionary<string, TabPage>();
        private int previewStartTime;
        private int previewEndTime;

        private string compileFolderPath = "";
        private string GhwtModsFolderPath = "";
        private string Ps2IsoFolderPath = "";
        private string projectFilePath = "";

        private bool isProgrammaticChange = false;


        public CompileSong(string ghproj = "")
        {
            InitializeComponent();

            year_input.Value = DateTime.Now.Year;

            this.Load += new EventHandler(MainForm_Load);
            if (ghproj != "")
            {
                StartupProject = ghproj;
                this.Load += new EventHandler(Startup_Load);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Iterate through all controls in the TableLayoutPanel
            foreach (Control nestedControl in game_layout.Controls)
            {
                // Check if the nested control is a RadioButton
                if (nestedControl is RadioButton radioButton)
                {
                    // Attach the CheckedChanged event handler to the RadioButton
                    radioButton.CheckedChanged += GameSelect_CheckChanged;
                }
            }
            foreach (Control nestedControl in platform_layout.Controls)
            {
                // Check if the nested control is a RadioButton
                if (nestedControl is RadioButton radioButton)
                {
                    // Attach the CheckedChanged event handler to the RadioButton
                    radioButton.CheckedChanged += PlatformSelect_CheckChanged;
                }
            }
            InitializeTabDict();
            SetButtons();
            SetAll();
            DefaultPaths();
            DefaultTemplateCheck();
        }
        private void Startup_Load(object sender, EventArgs e)
        {
            if (File.Exists(StartupProject))
            {
                LoadGhproj(StartupProject);
            }

        }
        private void SetAll()
        {
            SetGameFields();
            updatePreviewStartTime();
            updatePreviewEndTime();
        }
        private void DefaultPaths()
        {
            ExeLocation = Assembly.GetExecutingAssembly().Location;
            ExeDirectory = Path.GetDirectoryName(ExeLocation);
            DefaultTemplateFolder = Path.Combine(ExeDirectory, "Templates");
            DefaultTemplatePath = Path.Combine(DefaultTemplateFolder, "default.ghproj");
        }
        private void DefaultTemplateCheck()
        {
            if (File.Exists(DefaultTemplatePath))
            {
                LoadGhproj(DefaultTemplatePath);
            }
            else
            {
                Directory.CreateDirectory(DefaultTemplateFolder);
                SaveData data = makeSaveClass();
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(DefaultTemplatePath, json);
            }
        }
        private void textBox_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Data format of the file(s) can be accepted
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Modify the DragDropEffects to provide a visual feedback to the user
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // Reject the drop
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // Optionally, you can handle multiple files or directories here
            // For this example, let's handle only the first path
            if (fileList != null && fileList.Length > 0)
            {
                TextBox textBox = sender as TextBox; // Cast the sender back to a TextBox
                if (textBox != null)
                {
                    textBox.Text = fileList[0];
                }
            }
        }
        private void InitializeTabDict()
        {
            tabPageDict = new Dictionary<string, TabPage>
            {
                { "metadata_tab", metadata_tab },
                { "audio_tab_wt", audio_tab_wt },
                { "song_data_wt", song_data_tab_wt },
                { "audio_tab_gh3", audio_tab_gh3 },
                { "song_data_gh3", song_data_tab_gh3 },
                { "compile_tab", compile_tab }
            };
        }
        private void SetButtons()
        {
            // Set the Tag property with both the target TextBox and the dialog type

            // General

            compile_select.Tag = new Tuple<TextBox, string, string>(compile_input, "folder", "");
            project_select.Tag = new Tuple<TextBox, string, string>(project_input, "file", ghprojFileFilter);

            // Attach the event handlers to all general buttons
            compile_select.Click += SelectFileFolder;
            compile_select.Click += SaveCompileString;
            project_select.Click += SelectFileFolder;
            project_input.TextChanged += SaveProjectString;

            // GH3/Aerosmith

            // Audio
            guitar_select_gh3.Tag = new Tuple<TextBox, string, string>(guitar_input_gh3, "file", audioFileFilter);
            rhythm_select_gh3.Tag = new Tuple<TextBox, string, string>(rhythm_input_gh3, "file", audioFileFilter);
            backing_add_gh3.Tag = backing_input_gh3;
            backing_delete_gh3.Tag = backing_input_gh3;

            coop_guitar_select_gh3.Tag = new Tuple<TextBox, string, string>(coop_guitar_input_gh3, "file", audioFileFilter);
            coop_rhythm_select_gh3.Tag = new Tuple<TextBox, string, string>(coop_rhythm_input_gh3, "file", audioFileFilter);
            coop_backing_add_gh3.Tag = coop_backing_input_gh3;
            coop_backing_delete_gh3.Tag = coop_backing_input_gh3;

            crowd_select_gh3.Tag = new Tuple<TextBox, string, string>(crowd_input_gh3, "file", audioFileFilter);
            preview_audio_select_gh3.Tag = new Tuple<TextBox, string, string>(preview_audio_input_gh3, "file", audioFileFilter);

            // Attach the event handlers to all GH3/A Audio tab buttons
            guitar_select_gh3.Click += SelectFileFolder;
            rhythm_select_gh3.Click += SelectFileFolder;
            backing_add_gh3.Click += AddToListBox;
            backing_delete_gh3.Click += DeleteFromListBox;

            coop_guitar_select_gh3.Click += SelectFileFolder;
            coop_rhythm_select_gh3.Click += SelectFileFolder;
            coop_backing_add_gh3.Click += AddToListBox;
            coop_backing_delete_gh3.Click += DeleteFromListBox;

            crowd_select_gh3.Click += SelectFileFolder;
            preview_audio_select_gh3.Click += SelectFileFolder;

            // Song Data
            midi_file_select_gh3.Tag = new Tuple<TextBox, string, string>(midi_file_input_gh3, "file", midiFileFilter);
            perf_override_select_gh3.Tag = new Tuple<TextBox, string, string>(perf_override_input_gh3, "file", qFileFilter);
            ska_files_select_gh3.Tag = new Tuple<TextBox, string, string>(ska_files_input_gh3, "folder", "");
            song_script_select_gh3.Tag = new Tuple<TextBox, string, string>(song_script_input_gh3, "file", qFileFilter);

            // Attach the event handlers to all GH3/A Song Data tab buttons
            midi_file_select_gh3.Click += SelectFileFolder;
            perf_override_select_gh3.Click += SelectFileFolder;
            ska_files_select_gh3.Click += SelectFileFolder;
            song_script_select_gh3.Click += SelectFileFolder;
        }
        public void SetGameFields()
        {
            var game = GetGame();
            if (game == "")
            {
                return;
            }
            bool isOld = game == "GH3" || game == "GHA";
            SetTabs(isOld);
            SetGenres(game);
            artist_text_select.SelectedIndex = 0;
            if (isOld)
            {
                SetGh3Fields(game);
                ska_file_source_gh3.SelectedIndex = 0;
                countoff_select_gh3.SelectedIndex = 0;
                vocal_gender_select_gh3.SelectedIndex = 0;
                bassist_select_gh3.SelectedIndex = 0;
                hopo_mode_select.SelectedIndex = 0;
            }
            else
            {

            }
        }
        private void SetGenres(string game)
        {
            List<string> genreBase = new List<string> { "Rock", "Punk", "Glam Rock", "Black Metal", "Classic Rock", "Pop" };
            List<string> genreWt = new List<string> { "Heavy Metal", "Goth" };
            List<string> genreGh5 = new List<string> { "Alternative", "Big Band", "Blues", "Blues Rock", "Country", "Dance", "Death Metal", "Disco",
                     "Electronic", "Experimental", "Funk", "Grunge", "Hard Rock", "Hardcore", "Hip Hop", "Indie Rock",
                     "Industrial", "International", "Jazz", "Metal", "Modern Rock", "New Wave", "Nu Metal", "Pop Punk",
                     "Pop Rock", "Prog Rock", "R&B", "Reggae", "Rockabilly", "Ska Punk", "Southern Rock", "Speed Metal",
                     "Surf Rock" };
            List<string> genreGh6 = new List<string> { "Hardcore Punk", "Heavy Metal", "Progressive Rock" };

            // Create a merged list based on the game
            List<string> mergedGenres = new List<string>(genreBase); // Start with the base
            if (game == "GHWT")
            {
                mergedGenres.AddRange(genreWt);
            }
            else if (game == "GH5")
            {
                mergedGenres.AddRange(genreGh5);
            }
            else if (game == "GHWoR")
            {
                mergedGenres.AddRange(genreGh5);
                mergedGenres.AddRange(genreGh6);
            }

            mergedGenres.Sort();
            mergedGenres.Add("Other");

            // Set the genre list
            genre_input.Items.Clear();
            if (game != "GH3" && game != "GHA")
            {
                genre_input.Enabled = true;
                genre_input.Items.AddRange(mergedGenres.ToArray());
                if (gameSelectedGenres.ContainsKey(game))
                {
                    genre_input.Text = gameSelectedGenres[game];
                }
                else
                {
                    genre_input.SelectedIndex = 0;
                }
            }
            else
            {
                genre_input.Enabled = false;
                genre_input.Text = "";
            }
        }
        private void SetGh3Fields(string game)
        {
            bool isGha = game == "GHA";
            crowd_label_gh3.Enabled = isGha;
            crowd_input_gh3.Enabled = isGha;
            crowd_select_gh3.Enabled = isGha;
        }
        private void SetTabs(bool isOld)
        {
            compiler_tabs.TabPages.Clear();
            List<(string tab, string tabName)> tabNames;
            if (isOld)
            {
                tabNames = new List<(string tab, string tabName)>
                {
                    ("metadata_tab", "Metadata"),
                    ("audio_tab_gh3", "Audio"),
                    ("song_data_gh3", "Song Data"),
                    ("compile_tab", "Compile")
                };
            }
            else
            {
                tabNames = new List<(string tab, string tabName)>
                {
                    ("metadata_tab", "Metadata"),
                    ("audio_tab_wt", "Audio"),
                    ("song_data_wt", "Song Data"),
                    ("compile_tab", "Compile")
                };
            }
            foreach (var (tab, tabName) in tabNames)
            {
                var newTab = tabPageDict[tab];
                newTab.Text = tabName;
                compiler_tabs.TabPages.Add(newTab);
            }
        }
        public string GetGame()
        {
            return GetSelectedFromTable(game_layout);
        }
        public string GetPlatform()
        {
            return GetSelectedFromTable(platform_layout);
        }
        public string GetSkaSourceGh3()
        {
            int skaSource = ska_file_source_gh3.SelectedIndex;
            if (skaSource == 2)
            {
                return "GH3";
            }
            else if (skaSource == 1)
            {
                return "GHA";
            }
            else
            {
                return "GHWT";
            }
        }
        public string GetSelectedFromTable(TableLayoutPanel table)
        {
            foreach (Control control in table.Controls)
            {
                // Check if the control is a RadioButton
                if (control is RadioButton radioButton)
                {
                    // Check if the radio button is checked
                    if (radioButton.Checked)
                    {
                        return radioButton.Text;
                    }
                }
            }
            return "";
        }
        private void GameSelect_CheckChanged(object sender, EventArgs e)
        {
            // Sender is the radio button that triggered the event
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                if (!radioButton.Checked && lastCheckedRadioButton == radioButton)
                {
                    // Radio button is unchecked, add or update the entry in the gameSelectedGenres dictionary
                    string gameName = lastCheckedRadioButton.Text;
                    string selectedGenre = genre_input.Text; // Get the currently selected genre

                    if (!string.IsNullOrEmpty(gameName) && !string.IsNullOrEmpty(selectedGenre))
                    {
                        // Add or update the dictionary entry for the game
                        gameSelectedGenres[gameName] = selectedGenre;
                    }

                    // Reset lastCheckedRadioButton since we've handled the unchecked event
                    lastCheckedRadioButton = null;
                }
                else if (radioButton.Checked)
                {
                    // Radio button is checked, update fields based on the selected game
                    SetGameFields();

                    // Remember this radio button as the last one checked
                    lastCheckedRadioButton = radioButton;
                }
            }
        }
        private void PlatformSelect_CheckChanged(object sender, EventArgs e)
        {
            string game = GetGame();
            // Sender is the radio button that triggered the event
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                if (radioButton.Checked)
                {
                    if (game == "GH3" || game == "GHA")
                    {
                        if (radioButton.Text == "PS2")
                        {
                            compile_label.Text = "Extracted ISO Folder:";
                            compile_input.Text = Ps2IsoFolderPath;
                        }
                        else
                        {
                            compile_label.Text = "Compile Folder:";
                            compile_input.Text = compileFolderPath;
                        }
                    }

                }
            }
        }
        private void SelectFileFolder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Tuple<TextBox, string, string>)
            {
                var (linkedTextBox, dialogType, fileFilter) = (Tuple<TextBox, string, string>)btn.Tag;

                if (dialogType == "file")
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = fileFilter;
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.RestoreDirectory = true;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Get the path of specified file
                            string filePath = openFileDialog.FileName;

                            // Update the linked TextBox with the selected file path
                            linkedTextBox.Text = filePath;
                        }
                    }
                }
                else if (dialogType == "folder")
                {
                    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                    {
                        folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Get the path of selected folder
                            string folderPath = folderBrowserDialog.SelectedPath;

                            // Update the linked TextBox with the selected folder path
                            linkedTextBox.Text = folderPath;
                        }
                    }
                }
            }
        }
        private void SaveCompileString(object sender, EventArgs e)
        {
            string platform = GetPlatform();
            if (platform == "PS2")
            {
                Ps2IsoFolderPath = compile_input.Text;
            }
            else
            {
                compileFolderPath = compile_input.Text;
            }
        }
        private void SaveProjectString(object sender, EventArgs e)
        {
            projectFilePath = project_input.Text;
        }
        private void AddToListBox(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is ListBox)
            {
                ListBox linkedTextBox = (ListBox)btn.Tag;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = audioFileFilter;
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.Multiselect = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in openFileDialog.FileNames)
                        {
                            linkedTextBox.Items.Add(file);
                        }
                    }
                }
            }
        }
        private void DeleteFromListBox(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is ListBox)
            {
                ListBox linkedTextBox = (ListBox)btn.Tag;
                if (linkedTextBox.SelectedIndex != -1)
                {
                    linkedTextBox.Items.RemoveAt(linkedTextBox.SelectedIndex);
                }
            }
        }
        private void isCover_CheckedChanged(object sender, EventArgs e)
        {
            coverLabel.Enabled = isCover.Checked;
            cover_artist_input.Enabled = isCover.Checked;
            cover_year_input.Enabled = isCover.Checked;
        }
        // Guitar Hero 3/Aerosmith Logic
        private void p2_rhythm_check_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked)
            {
                coop_audio_check.Enabled = true;
                rhythm_label_gh3.Text = "Rhythm";
            }
            else
            {
                coop_audio_check.Enabled = false;
                coop_audio_check.Checked = false;
                rhythm_label_gh3.Text = "Bass";
            }
        }

        private void coop_audio_check_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            coop_guitar_label_gh3.Enabled = checkBox.Checked;
            coop_guitar_input_gh3.Enabled = checkBox.Checked;
            coop_rhythm_label_gh3.Enabled = checkBox.Checked;
            coop_rhythm_input_gh3.Enabled = checkBox.Checked;
            coop_backing_label_gh3.Enabled = checkBox.Checked;
            coop_backing_input_gh3.Enabled = checkBox.Checked;
            coop_guitar_select_gh3.Enabled = checkBox.Checked;
            coop_rhythm_select_gh3.Enabled = checkBox.Checked;
            coop_backing_add_gh3.Enabled = checkBox.Checked;
            coop_backing_delete_gh3.Enabled = checkBox.Checked;
        }

        private void gh3_rendered_preview_check_CheckedChanged(object sender, EventArgs e)
        {
            gh3_preview_audio_label.Enabled = gh3_rendered_preview_check.Checked;
            preview_audio_input_gh3.Enabled = gh3_rendered_preview_check.Checked;
            preview_audio_select_gh3.Enabled = gh3_rendered_preview_check.Checked;

            preview_label_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            preview_minutes_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            preview_seconds_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            preview_mills_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            length_label_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            length_minutes_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            length_seconds_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            length_mills_gh3.Enabled = !gh3_rendered_preview_check.Checked;
            gh3_set_end.Enabled = !gh3_rendered_preview_check.Checked;
        }
        private void updatePreviewStartTime()
        {
            int minutes = (int)preview_minutes_gh3.Value;
            int seconds = (int)preview_seconds_gh3.Value;
            int mills = (int)preview_mills_gh3.Value;
            previewStartTime = (minutes * 60000) + (seconds * 1000) + mills;
        }
        private void updatePreviewEndTime()
        {
            int minutes = (int)length_minutes_gh3.Value;
            int seconds = (int)length_seconds_gh3.Value;
            int mills = (int)length_mills_gh3.Value;
            previewEndTime = (minutes * 60000) + (seconds * 1000) + mills;
        }
        private void previewLengthEndSwap()
        {
            if (gh3_set_end.Checked)
            {
                previewEndTime = previewStartTime + previewEndTime;
            }
            else
            {
                previewEndTime = previewEndTime - previewStartTime;
            }
            UpdatePreviewLengthFields();
        }
        private void UpdatePreviewLengthFields()
        {
            length_minutes_gh3.Value = previewEndTime / 60000;
            length_seconds_gh3.Value = (previewEndTime % 60000) / 1000;
            length_mills_gh3.Value = (previewEndTime % 60000) % 1000;
        }
        private void gh3_preview_ValueChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange)
            {
                return;
            }
            updatePreviewStartTime();
        }
        private void gh3_preview_length_ValueChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange)
            {
                return;
            }
            updatePreviewEndTime();
        }
        private void gh3_set_end_CheckedChanged(object sender, EventArgs e)
        {
            isProgrammaticChange = true;
            previewLengthEndSwap();
            isProgrammaticChange = false;
        }

        // Compiling Logic
        private void CompileFolderCheck()
        {
            if (compile_input.Text == "")
            {
                compile_input.Text = Path.GetDirectoryName(midi_file_input_gh3.Text);
            }
        }
        private void CompileGh3PakFile()
        {
            string pakFolder = PAK.CreateSongPackageGh3(midi_file_input_gh3.Text, compile_input.Text, song_checksum.Text, GetGame(), GetPlatform(), (int)HmxHopoVal.Value, ska_files_input_gh3.Text, perf_override_input_gh3.Text, song_script_input_gh3.Text, GetSkaSourceGh3());

            // Add code to delete the folder after processing eventually
        }
        private void compile_pak_button_Click(object sender, EventArgs e)
        {
            CompileFolderCheck();
            CompileGh3PakFile();
        }
        private async void compile_all_button_Click(object sender, EventArgs e)
        {
            CompileFolderCheck();
            //CompileGh3PakFile();
            string gtrPath = Path.Combine(compile_input.Text, "guitar.mp3");
            string rhythmPath = Path.Combine(compile_input.Text, "rhythm.mp3");
            string[] backingPaths = backing_input_gh3.Items.Cast<string>().ToArray();
            string coopGtrPath = Path.Combine(compile_input.Text, "coop_guitar.mp3");
            string coopRhythmPath = Path.Combine(compile_input.Text, "coop_rhythm.mp3");
            string[] coopBackingPaths = coop_backing_input_gh3.Items.Cast<string>().ToArray();


            FSB fsb = new FSB();
            fsb.MixFiles(backingPaths, Path.Combine(compile_input.Text, "backing.mp3"));
            Task gtrStem = fsb.ConvertToMp3(guitar_input_gh3.Text, gtrPath);
            Task rhythmStem = fsb.ConvertToMp3(rhythm_input_gh3.Text, rhythmPath);

            var tasksToAwait = new List<Task> { gtrStem, rhythmStem };



            // Await all started tasks. This ensures all conversions are completed before moving on.
            await Task.WhenAll(tasksToAwait.ToArray());

        }

        // Toolstrip Logic
        private SaveData makeSaveClass()
        {
            var data = new SaveData
            {
                gameSelect = GetGame(),
                platformSelect = GetPlatform(),
                songName = song_checksum.Text,
                chartAuthor = chart_author_input.Text,
                title = title_input.Text,
                artist = artist_input.Text,
                artistTextCustom = artistTextCustom.Text,
                coverArtist = cover_artist_input.Text,
                guitarPath = guitar_input_gh3.Text,
                rhythmPath = rhythm_input_gh3.Text,
                backingPaths = string.Join(";", backing_input_gh3.Items.Cast<string>().ToArray()),
                coopGuitarPath = coop_guitar_input_gh3.Text,
                coopRhythmPath = coop_rhythm_input_gh3.Text,
                coopBackingPaths = string.Join(";", coop_backing_input_gh3.Items.Cast<string>().ToArray()),
                crowdPath = crowd_input_gh3.Text,
                previewAudioPath = preview_audio_input_gh3.Text,
                midiPath = midi_file_input_gh3.Text,
                perfPath = perf_override_input_gh3.Text,
                skaPath = ska_files_input_gh3.Text,
                songScriptPath = song_script_input_gh3.Text,
                compilePath = compile_input.Text,
                projectPath = project_input.Text,
                artistText = artist_text_select.SelectedIndex,
                songYear = (int)year_input.Value,
                coverYear = (int)cover_year_input.Value,
                genre = genre_input.SelectedIndex,
                previewStart = previewStartTime,
                previewEnd = previewEndTime,
                hmxHopoVal = (int)HmxHopoVal.Value,
                skaSource = ska_file_source_gh3.SelectedIndex,
                countoff = countoff_select_gh3.SelectedIndex,
                vocalGender = vocal_gender_select_gh3.SelectedIndex,
                bassistSelect = bassist_select_gh3.SelectedIndex,
                hopoMode = hopo_mode_select.SelectedIndex,
                beat8thLow = (int)beat8thLow.Value,
                beat8thHigh = (int)beat8thHigh.Value,
                beat16thLow = (int)beat16thLow.Value,
                beat16thHigh = (int)beat16thHigh.Value,
                gtrVolume = gh3_gtr_vol.Value,
                bandVolume = gh3_band_vol.Value,
                isCover = isCover.Checked,
                isP2Rhythm = p2_rhythm_check.Checked,
                isCoopAudio = coop_audio_check.Checked,
                useGh3RenderedPreview = gh3_rendered_preview_check.Checked,
                setEnd = gh3_set_end.Checked,
                useBeatTrack = use_beat_check.Checked
            };
            return data;
        }
        private void ClearListBoxes()
        {
            backing_input_gh3.Items.Clear();
            coop_backing_input_gh3.Items.Clear();
        }
        private void LoadSaveData(SaveData data)
        {
            isProgrammaticChange = true;
            ClearListBoxes();
            game_layout.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == data.gameSelect).Checked = true;
            platform_layout.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == data.platformSelect).Checked = true;
            song_checksum.Text = data.songName;
            chart_author_input.Text = data.chartAuthor;
            title_input.Text = data.title;
            artist_input.Text = data.artist;
            artistTextCustom.Text = data.artistTextCustom;
            cover_artist_input.Text = data.coverArtist;
            guitar_input_gh3.Text = data.guitarPath;
            rhythm_input_gh3.Text = data.rhythmPath;
            if (data.backingPaths != "")
            {
                backing_input_gh3.Items.AddRange(data.backingPaths.Split(';'));
            }
            coop_guitar_input_gh3.Text = data.coopGuitarPath;
            coop_rhythm_input_gh3.Text = data.coopRhythmPath;
            if (data.coopBackingPaths != "")
            {
                coop_backing_input_gh3.Items.AddRange(data.coopBackingPaths.Split(';'));
            }
            crowd_input_gh3.Text = data.crowdPath;
            preview_audio_input_gh3.Text = data.previewAudioPath;
            midi_file_input_gh3.Text = data.midiPath;
            perf_override_input_gh3.Text = data.perfPath;
            ska_files_input_gh3.Text = data.skaPath;
            song_script_input_gh3.Text = data.songScriptPath;
            compile_input.Text = data.compilePath;
            project_input.Text = data.projectPath;
            artist_text_select.SelectedIndex = data.artistText;
            year_input.Value = data.songYear;
            cover_year_input.Value = data.coverYear;
            genre_input.SelectedIndex = data.genre;
            HmxHopoVal.Value = data.hmxHopoVal;
            ska_file_source_gh3.SelectedIndex = data.skaSource;
            countoff_select_gh3.SelectedIndex = data.countoff;
            vocal_gender_select_gh3.SelectedIndex = data.vocalGender;
            bassist_select_gh3.SelectedIndex = data.bassistSelect;
            hopo_mode_select.SelectedIndex = data.hopoMode;
            beat8thLow.Value = data.beat8thLow;
            beat8thHigh.Value = data.beat8thHigh;
            beat16thLow.Value = data.beat16thLow;
            beat16thHigh.Value = data.beat16thHigh;
            gh3_gtr_vol.Value = data.gtrVolume;
            gh3_band_vol.Value = data.bandVolume;
            isCover.Checked = data.isCover;
            p2_rhythm_check.Checked = data.isP2Rhythm;
            coop_audio_check.Checked = data.isCoopAudio;
            gh3_rendered_preview_check.Checked = data.useGh3RenderedPreview;
            gh3_set_end.Checked = data.setEnd;
            use_beat_check.Checked = data.useBeatTrack;
            isProgrammaticChange = false;
            SetAll();
        }
        private void SaveProject(SaveData data)
        {
            if (File.Exists(projectFilePath))
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(projectFilePath, json);
            }
            else
            {
                SaveProjectAs(data);
            }
        }
        private void SaveProjectAs(SaveData data)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = ghprojFileFilter;
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    project_input.Text = saveFileDialog.FileName;
                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(projectFilePath, json);
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = makeSaveClass();
            SaveProject(data);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = makeSaveClass();
            SaveProjectAs(data);
        }
        private void LoadGhproj(string filePath)
        {

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
                LoadSaveData(data);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = ghprojFileFilter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    project_input.Text = openFileDialog.FileName;
                    LoadGhproj(project_input.Text);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadGhproj(DefaultTemplatePath);
        }

        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = ghprojFileFilter;
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = false;
                saveFileDialog.InitialDirectory = DefaultTemplateFolder;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = saveFileDialog.FileName;
                    var data = makeSaveClass();
                    data.projectPath = "";
                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                }
            }
        }

        private void loadTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = ghprojFileFilter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.InitialDirectory = DefaultTemplateFolder;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;
                    LoadGhproj(filePath);
                    project_input.Text = "";
                }
            }
        }
    }
}
