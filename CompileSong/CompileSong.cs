using GH_Toolkit_Core.Audio;
using GH_Toolkit_Core.PAK;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using GH_Toolkit_Core.QB;
using static GH_Toolkit_Core.QB.QBConstants;
using static GH_Toolkit_Exceptions.Exceptions;
using static GH_Toolkit_Core.Methods.Exceptions;
using IniParser.Model;
using IniParser;
using IniParser.Model.Configuration;
using GH_Toolkit_Core.Methods;

namespace GH_Toolkit_GUI
{
    public partial class CompileSong : Form
    {
        private int ghprojVersion = 1;
        private static string ExeLocation = Assembly.GetExecutingAssembly().Location;
        private static string ExeDirectory = Path.GetDirectoryName(ExeLocation);
        private string DefaultTemplateFolder;
        private string DefaultTemplatePath;
        private string StartupProject;

        private static string DATAPath = "DATA";
        private static string PAKPath = Path.Combine(DATAPath, "PAK");
        private static string MUSICPath = Path.Combine(DATAPath, "MUSIC");
        private static string SONGSPath = Path.Combine(DATAPath, "SONGS");
        private string GH3Path;
        private string GHAPath;
        private string WtSongFolder;
        private string ContentFolder;
        private string MusicFolder;
        private static string OnyxFolder = Path.Combine(ExeDirectory, "Tools", "Onyx");

        // This is needed to force all numbers to use decimals with a period as the decimal separator
        private static CultureInfo Murica = new CultureInfo("en-US");

        private static string downloadRef = "scripts\\guitar\\guitar_download.qb";
        private static string gh3DownloadSongs = "gh3_download_songs";
        private static string songlistRef = "scripts\\guitar\\songlist.qb";
        private static string downloadSonglist = "download_songlist";
        private static string gh3Songlist = "gh3_songlist";
        private static string downloadProps = "download_songlist_props";
        private static string permanentProps = "permanent_songlist_props";

        private string CurrentGame;
        private string CurrentPlatform;

        // Dictionary to hold the selected genre for each game
        private Dictionary<string, int> gameSelectedGenres = new Dictionary<string, int>()
        {
            { "GHWT", 0 },
            { "GH5", 0 },
            { "GHWoR", 0 }
        };
        // Dictionary to hold the selected drum kit for each game
        private Dictionary<string, int> gameDrumKits = new Dictionary<string, int>()
        {
            { "GHWT", 0 },
            { "GH5", 0 },
            { "GHWoR", 0 }
        };
        // Dictionary to hold the compile folder path for each platform and each game
        private Dictionary<string, Dictionary<string, string>> gamePlatformCompilePaths = new Dictionary<string, Dictionary<string, string>>();


        private RadioButton lastCheckedRadioButton = null;
        private string audioFileFilter = "Audio files (*.mp3, *.ogg, *.flac, *.wav)|*.mp3;*.ogg;*.flac;*.wav|All files (*.*)|*.*";
        private string audioRegex = ".*\\.(mp3|ogg|flac|wav)$";
        private string midiFileFilter = "MIDI files (*.mid)|*.mid|All files (*.*)|*.*";
        private string midiRegexCh = ".*\\.mid$";
        private string qFileFilter = "Q files (*.q)|*.q|All files (*.*)|*.*";
        private string ghprojFileFilter = "GHProj files (*.ghproj)|*.ghproj|All files (*.*)|*.*";

        private Dictionary<string, TabPage> tabPageDict = new Dictionary<string, TabPage>();
        private int previewStartTime;
        private int previewEndTime;
        private decimal nsHopoThreshold;

        private string compileFolderPath = "";
        private string GhwtModsFolderPath = "";
        private string Ps2IsoFolderPath = "";
        private string projectFilePath = "";

        private bool isProgrammaticChange = false;
        private bool isLoading = false;
        private UserPreferences Pref = UserPreferences.Default;


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
            OneTimeSetup();
            SetAll();
            DefaultPaths();
            DefaultTemplateCheck();
            UpdateNsValue();
        }
        private void OneTimeSetup()
        {
            ska_file_source_gh3.SelectedIndex = 0;
            venue_source_gh3.SelectedIndex = 0;
            countoff_select_gh3.SelectedIndex = 0;
            vocal_gender_select_gh3.SelectedIndex = 0;
            bassist_select_gh3.SelectedIndex = 0;
            hopo_mode_select.SelectedIndex = 0;

            skaFileSource.SelectedIndex = 0;
            venueSource.SelectedIndex = 2;
            countoffSelect.SelectedIndex = 0;
            vocalGenderSelect.SelectedIndex = 0;


            string[] skeletons = LoadTextFile(Path.Combine(ExeDirectory, "List Files", "skeletons.txt"));
            if (skeletons.Length == 0)
            {
                skeletons = ["Default"];
            }

            gSkeletonSelect.Items.AddRange(skeletons);
            bSkeletonSelect.Items.AddRange(skeletons);
            dSkeletonSelect.Items.AddRange(skeletons);
            vSkeletonSelect.Items.AddRange(skeletons);

            gSkeletonSelect.SelectedIndex = 0;
            bSkeletonSelect.SelectedIndex = 0;
            dSkeletonSelect.SelectedIndex = 0;
            vSkeletonSelect.SelectedIndex = 0;

            string[] songCategories = LoadTextFile(Path.Combine(ExeDirectory, "List Files", "songcategories.txt"));
            if (songCategories.Length != 0)
            {
                gameCategoryInput.Items.AddRange(songCategories);
                foreach (string category in songCategories)
                {
                    gameIconInput.Items.Add($"gamelogo_{category}");
                }
            }

            previewMinutes.ValueChanged += allPreviewTimeChange;
            previewSeconds.ValueChanged += allPreviewTimeChange;
            previewMills.ValueChanged += allPreviewTimeChange;
            lengthMinutes.ValueChanged += allPreviewTimeChange;
            lengthSeconds.ValueChanged += allPreviewTimeChange;
            lengthMills.ValueChanged += allPreviewTimeChange;
            preview_minutes_gh3.ValueChanged += allPreviewTimeChange;
            preview_seconds_gh3.ValueChanged += allPreviewTimeChange;
            preview_mills_gh3.ValueChanged += allPreviewTimeChange;
            length_minutes_gh3.ValueChanged += allPreviewTimeChange;
            length_seconds_gh3.ValueChanged += allPreviewTimeChange;
            length_mills_gh3.ValueChanged += allPreviewTimeChange;


        }
        // Method to load a text file and return a list of string for each line in the file
        private string[] LoadTextFile(string filePath)
        {
            // Create a new list to hold the lines of the text file
            List<string> lines = new List<string>();

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read all lines from the text file and add them to the list
                lines = File.ReadAllLines(filePath).ToList();
            }

            // Return the list of lines
            return lines.ToArray();
        }
        private void Startup_Load(object sender, EventArgs e)
        {
            if (File.Exists(StartupProject))
            {
                LoadProject(StartupProject);
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

            DefaultTemplateFolder = Path.Combine(ExeDirectory, "Templates");
            DefaultTemplatePath = Path.Combine(DefaultTemplateFolder, "default.ghproj");
            CurrentGame = GetGame();
            CurrentPlatform = GetPlatform();
        }
        private void DefaultTemplateCheck()
        {
            if (File.Exists(DefaultTemplatePath))
            {
                LoadProject(DefaultTemplatePath);
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
                { "compile_tab", compile_tab },
                { "wtde_settings", wtde_settings }
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

            // GHWT+

            // Audio
            kickSelect.Tag = new Tuple<TextBox, string, string>(kickInput, "file", audioFileFilter);
            snareSelect.Tag = new Tuple<TextBox, string, string>(snareInput, "file", audioFileFilter);
            cymbalsSelect.Tag = new Tuple<TextBox, string, string>(cymbalsInput, "file", audioFileFilter);
            tomsSelect.Tag = new Tuple<TextBox, string, string>(tomsInput, "file", audioFileFilter);

            guitarSelect.Tag = new Tuple<TextBox, string, string>(guitarInput, "file", audioFileFilter);
            bassSelect.Tag = new Tuple<TextBox, string, string>(bassInput, "file", audioFileFilter);
            vocalsSelect.Tag = new Tuple<TextBox, string, string>(vocalsInput, "file", audioFileFilter);

            backingAdd.Tag = backingInput;
            backingDelete.Tag = backingInput;

            crowdSelect.Tag = new Tuple<TextBox, string, string>(crowdInput, "file", audioFileFilter);
            previewSelect.Tag = new Tuple<TextBox, string, string>(previewInput, "file", audioFileFilter);

            // Attach the event handlers to all GHWT+ Audio tab buttons
            kickSelect.Click += SelectFileFolder;
            snareSelect.Click += SelectFileFolder;
            cymbalsSelect.Click += SelectFileFolder;
            tomsSelect.Click += SelectFileFolder;

            guitarSelect.Click += SelectFileFolder;
            bassSelect.Click += SelectFileFolder;
            vocalsSelect.Click += SelectFileFolder;

            backingAdd.Click += AddToListBox;
            backingDelete.Click += DeleteFromListBox;

            crowdSelect.Click += SelectFileFolder;
            previewSelect.Click += SelectFileFolder;

            // Song Data
            midiFileSelect.Tag = new Tuple<TextBox, string, string>(midiFileInput, "file", midiFileFilter);
            perfOverrideSelect.Tag = new Tuple<TextBox, string, string>(perfOverrideInput, "file", qFileFilter);
            skaFilesSelect.Tag = new Tuple<TextBox, string, string>(skaFilesInput, "folder", "");
            songScriptSelect.Tag = new Tuple<TextBox, string, string>(songScriptInput, "file", qFileFilter);

            // Attach the event handlers to all GHWT+ Song Data tab buttons
            midiFileSelect.Click += SelectFileFolder;
            perfOverrideSelect.Click += SelectFileFolder;
            skaFilesSelect.Click += SelectFileFolder;
            songScriptSelect.Click += SelectFileFolder;
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
            SetDrumkit();
            SetBeatLines();
            // artist_text_select.SelectedIndex = 0;
            if (isOld)
            {

            }
            else
            {

            }
        }
        private void SetBeatLines()
        {
            beat8thLow.Enabled = Pref.OverrideBeatLines;
            beat8thHigh.Enabled = Pref.OverrideBeatLines;
            beat16thLow.Enabled = Pref.OverrideBeatLines;
            beat16thHigh.Enabled = Pref.OverrideBeatLines;
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
                    genre_input.SelectedIndex = gameSelectedGenres[game];
                }
                else if (!isLoading)
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
        private void SetDrumkit()
        {
            string game = CurrentGame;
            if (game == GAME_GH3 || game == GAME_GHA)
            {
                return;
            }
            List<string> drumKitBase = new List<string> { "Classic Rock", "Electro", "Fusion", "Heavy Rock", "Hip Hop", "Modern Rock" };
            List<string> drumKitWt = new List<string> { "Blip Hop", "Cheesy", "Computight", "Conga", "Dub", "Eightys", "Gunshot",
                                       "House", "India", "Jazzy", "Old School", "Orchestral", "Scratch", "Scratch_Electro" };
            List<string> drumKitGh5 = new List<string> { "Bigroom Rock", "Dance", "Metal", "Noise", "Standard Rock" };

            if (CurrentGame == "GHWT")
            {
                drumKitBase.AddRange(drumKitWt);
            }
            else
            {
                drumKitBase.AddRange(drumKitGh5);
            }

            drumKitBase.Sort();

            // Clear drumKitSelect ComboBox entries
            drumKitSelect.Items.Clear();

            // Add the drum kits to the drumKitSelect ComboBox
            drumKitSelect.Items.AddRange(drumKitBase.ToArray());
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
            if (CurrentGame == GAME_GHWT && CurrentPlatform == "PC")
            {
                compiler_tabs.TabPages.Add(wtde_settings);
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
            return GetSkaSource(ska_file_source_gh3.SelectedIndex);

        }
        public string GetSkaSourceGhwt()
        {
            return GetSkaSource(skaFileSource.SelectedIndex);
        }
        private string GetSkaSource(int skaSource)
        {
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
                    var selectedGenre = genre_input.SelectedIndex; // Get the currently selected genre

                    if (!string.IsNullOrEmpty(gameName))
                    {
                        // Add or update the dictionary entry for the game
                        gameSelectedGenres[gameName] = selectedGenre;
                    }

                    // Reset lastCheckedRadioButton since we've handled the unchecked event
                    lastCheckedRadioButton = null;
                }
                else if (radioButton.Checked)
                {
                    // Update the Current Game field
                    CurrentGame = radioButton.Text;

                    // Radio button is checked, update fields based on the selected game
                    SetGameFields();

                    // Remember this radio button as the last one checked
                    lastCheckedRadioButton = radioButton;


                }
            }
        }
        private void PlatformSelect_CheckChanged(object sender, EventArgs e)
        {
            string game = CurrentGame;
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
                    // Update the Current Platform field
                    CurrentPlatform = radioButton.Text;

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
            if (isProgrammaticChange)
            {
                return;
            }
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
        private void UpdatePreviewFields()
        {
            preview_minutes_gh3.Value = previewStartTime / 60000;
            preview_seconds_gh3.Value = (previewStartTime % 60000) / 1000;
            preview_mills_gh3.Value = (previewStartTime % 60000) % 1000;
            length_minutes_gh3.Value = previewEndTime / 60000;
            length_seconds_gh3.Value = (previewEndTime % 60000) / 1000;
            length_mills_gh3.Value = (previewEndTime % 60000) % 1000;
            previewMinutes.Value = preview_minutes_gh3.Value;
            previewSeconds.Value = preview_seconds_gh3.Value;
            previewMills.Value = preview_mills_gh3.Value;
            lengthMinutes.Value = length_minutes_gh3.Value;
            lengthSeconds.Value = length_seconds_gh3.Value;
            lengthMills.Value = length_mills_gh3.Value;
        }
        private void UpdatePreviewLengthFields()
        {
            length_minutes_gh3.Value = previewEndTime / 60000;
            length_seconds_gh3.Value = (previewEndTime % 60000) / 1000;
            length_mills_gh3.Value = (previewEndTime % 60000) % 1000;
            lengthMinutes.Value = length_minutes_gh3.Value;
            lengthSeconds.Value = length_seconds_gh3.Value;
            lengthMills.Value = length_mills_gh3.Value;
        }
        private void allPreviewTimeChange(object sender, EventArgs e)
        {

            // Sender is the NumericUpDown that triggered the event
            NumericUpDown changed = sender as NumericUpDown;
            if (changed != null)
            {
                var actionMap = new Dictionary<NumericUpDown, Action<NumericUpDown>>()
                {
                    { preview_minutes_gh3, (c) => { previewMinutes.Value = c.Value; updatePreviewStartTime(); } },
                    { preview_seconds_gh3, (c) => { previewSeconds.Value = c.Value; updatePreviewStartTime(); } },
                    { preview_mills_gh3, (c) => { previewMills.Value = c.Value; updatePreviewStartTime(); } },
                    { length_minutes_gh3, (c) => { lengthMinutes.Value = c.Value; updatePreviewEndTime(); } },
                    { length_seconds_gh3, (c) => { lengthSeconds.Value = c.Value; updatePreviewEndTime(); } },
                    { length_mills_gh3, (c) => { lengthMills.Value = c.Value; updatePreviewEndTime(); } },
                    { previewMinutes, (c) => { preview_minutes_gh3.Value = c.Value; updatePreviewStartTime(); } },
                    { previewSeconds, (c) => { preview_seconds_gh3.Value = c.Value; updatePreviewStartTime(); } },
                    { previewMills, (c) => { preview_mills_gh3.Value = c.Value; updatePreviewStartTime(); } },
                    { lengthMinutes, (c) => { length_minutes_gh3.Value = c.Value; updatePreviewEndTime(); } },
                    { lengthSeconds, (c) => { length_seconds_gh3.Value = c.Value; updatePreviewEndTime(); } },
                    { lengthMills, (c) => { length_mills_gh3.Value = c.Value; updatePreviewEndTime(); } },
                };

                if (actionMap.TryGetValue(changed, out var action))
                {
                    if (isProgrammaticChange)
                    {
                        return;
                    }
                    isProgrammaticChange = true;
                    action(changed);
                    isProgrammaticChange = false;
                }
            }

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
            setEndTime.Checked = gh3_set_end.Checked;
            isProgrammaticChange = false;
        }

        private void UpdateGh3FilePreference(string pakPath, string pabPath, string folderPath)
        {
            Pref.Gh3QbPak = pakPath;
            Pref.Gh3QbPab = pabPath;
            Pref.Gh3FolderPath = folderPath;
            Pref.Save();
        }
        private void UpdateGhaFilePreference(string pakPath, string pabPath, string folderPath)
        {
            Pref.GhaQbPak = pakPath;
            Pref.GhaQbPab = pabPath;
            Pref.GhaFolderPath = folderPath;
            Pref.Save();
        }
        private void UpdateGhwtModsFolder(string folderPath)
        {
            Pref.WtModsFolder = folderPath;
            Pref.Save();
        }
        // Compiling Logic
        private void PreCompileCheck()
        {
            if (compile_input.Text == "")
            {
                compile_input.Text = Path.GetDirectoryName(project_input.Text);
            }
            if (song_checksum.Text == "")
            {
                CreateChecksum();
            }
            string game = CurrentGame;
            if (game == "GH3" || game == "GHA")
            {
                string platform = CurrentPlatform;
                if (platform == "PC")
                {
                    string backupLocation = Path.Combine(ExeDirectory, "Backups", game);
                    string qbPakLocation = Path.Combine(backupLocation, "qb");
                    if (!File.Exists(qbPakLocation + DOT_PAK_XEN))
                    {
                        var regLookup = new RegistryLookup();
                        string regFolder = game == "GH3" ? "Guitar Hero III" : "Guitar Hero Aerosmith";
                        string regPath = $@"SOFTWARE\WOW6432Node\Aspyr\{regFolder}";
                        string regValue = "Path";
                        string ghPath = regLookup.GetRegistryValue(regPath, regValue);

                        try
                        {
                            // Check if ghPath is null or an empty string
                            if (string.IsNullOrEmpty(ghPath))
                            {
                                // Call the method that pops up a window asking for the game path
                                ghPath = AskForGamePath();
                                string exeName = CurrentGame == GAME_GH3 ? "GH3" : "Guitar Hero Aerosmith";
                                string ghExePath = Path.Combine(ghPath, $"{exeName}.exe");
                                if (!File.Exists(ghExePath))
                                {
                                    throw new Exception($"The game executable was not found at the specified path: {ghExePath}");
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            MessageBox.Show($"{regFolder}'s game path is required to proceed.\n\nCancelling compilation.", "Path Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            throw;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while trying to get the game path.\n\n{ex.Message}\n\nCancelling compilation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }

                        Directory.CreateDirectory(backupLocation);
                        string ghQbPakPath = Path.Combine(ghPath, PAKPath, "qb.pak.xen");
                        string ghQbPabPath = Path.Combine(ghPath, PAKPath, "qb.pab.xen");
                        try
                        {
                            File.Copy(ghQbPakPath, qbPakLocation + DOT_PAK_XEN);
                            File.Copy(ghQbPabPath, qbPakLocation + DOT_PAB_XEN);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while trying to backup {regFolder}'s QB.PAK file.\n\n{ex.Message}\n\nCancelling compilation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }

                        if (game == "GH3")
                        {
                            UpdateGh3FilePreference(ghQbPakPath, ghQbPabPath, ghPath);
                        }
                        else
                        {
                            UpdateGhaFilePreference(ghQbPakPath, ghQbPabPath, ghPath);
                        }
                        ReplaceGh3PakFiles();

                        MessageBox.Show($"A backup of {regFolder}'s QB file has been created.\nIt can be copied back to your GH folder at any time in the settings menu.", "Backup Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            if (game == GAME_GHWT)
            {
                if (CurrentPlatform == "PC")
                {
                    if (!Directory.Exists(Pref.WtModsFolder))
                    {
                        MessageBox.Show("Your GHWT mods folder has not been set. Please select your MODS folder now.", "Folder Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateGhwtModsFolder(AskForGamePath());
                    }
                }
            }
            if (CurrentPlatform == "PS2")
            {
                if (Ps2IsoFolderPath == "")
                {
                    MessageBox.Show("Your PS2 ISO folder has not been set. Please select your ISO folder now.", "Folder Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Ps2IsoFolderPath = AskForGamePath();
                }
            }
            else if (CurrentPlatform != "PC")
            {
                var onyxExe = Path.Combine(OnyxFolder, "Onyx.exe");
                while (!Directory.Exists(OnyxFolder) || !File.Exists(onyxExe))
                {
                    MessageBox.Show("Onyx has not been set. Please select your Onyx CLI folder now.", "Folder Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var tempOnyxFolder = AskForGamePath();
                    var tempOnyxExe = Path.Combine(tempOnyxFolder, "Onyx.exe");
                    if (!File.Exists(tempOnyxExe))
                    {
                        MessageBox.Show("Onyx.exe was not found in the selected folder. Please select the correct folder.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ReadWrite.CopyDirectory(tempOnyxFolder, OnyxFolder, true);
                    }
                }
            }
        }
        public static string AskForGamePath()
        {

            string folderPath = "";
            while (true) // Keep showing the dialog until a valid path is selected or the user cancels
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.ShowNewFolderButton = false;
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    {
                        if (Directory.Exists(dialog.SelectedPath))
                        {
                            return dialog.SelectedPath; // Return the selected path if it's valid
                        }
                        else
                        {
                            MessageBox.Show("The selected path does not exist. Please select a valid path.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        throw new OperationCanceledException("User cancelled the path selection."); // Throw an exception if the user cancels
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid path.", "Path Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void ReplaceGh3PakFiles()
        {
            string qbPakLocation = GetGh3PakFile();
            // Might make this use the backup instead in the future...
            string replaceLocation = Path.Combine(ExeDirectory, "Replacements", CurrentPlatform, CurrentGame, "QB");

            if (Directory.Exists(replaceLocation))
            {
                var pakCompiler = new PAK.PakCompiler(CurrentGame, CurrentPlatform, split: true);
                var replaceFiles = Directory.GetFiles(replaceLocation, "*.qb", SearchOption.AllDirectories);
                var qbPak = PAK.PakEntryDictFromFile(qbPakLocation);
                foreach (var file in replaceFiles)
                {
                    var relPath = Path.GetRelativePath(replaceLocation, file);
                    if (qbPak.TryGetValue(relPath, out var entry))
                    {
                        var qbData = File.ReadAllBytes(file);
                        entry.OverwriteData(qbData);
                    }
                }
                var (pakData, pabData) = pakCompiler.CompilePakFromDictionary(qbPak);
                OverwriteGh3Pak(pakData, pabData!);
            }
        }
        private void OverwriteGh3Pak(byte[] pakData, byte[] pabData)
        {
            string qbPakLocation = GetGh3PakFile();
            string qbPabLocation = qbPakLocation.Replace(DOT_PAK_XEN, DOT_PAB_XEN);
            File.WriteAllBytes(qbPakLocation, pakData);
            File.WriteAllBytes(qbPabLocation, pabData);
        }
        private void FirstTimeDownloadSonglist()
        {
            string qbPakLocation = GetGh3PakFile();
            var pakCompiler = new PAK.PakCompiler(CurrentGame, CurrentPlatform, split: true);
            var qbPak = PAK.PakEntryDictFromFile(qbPakLocation);
            var downloadQb = qbPak[downloadRef];
            var downloadQbEntries = QB.QbEntryDictFromBytes(downloadQb.EntryData, "big");
            var downloadSonglist = downloadQbEntries[gh3DownloadSongs].Data as QBStruct.QBStructData;
            var tier1 = downloadSonglist["tier1"] as QBStruct.QBStructData;
            tier1.DeleteItem("defaultunlocked");
            tier1.AddFlagToStruct("unlockall", QBKEY);
            byte[] downloadQbBytes = QB.CompileQbFromDict(downloadQbEntries, downloadRef, CurrentGame, CurrentPlatform);
            downloadQb.EntryData = downloadQbBytes;
            var (pakData, pabData) = pakCompiler.CompilePakFromDictionary(qbPak);
            OverwriteGh3Pak(pakData, pabData!);
        }
        private void CreateChecksum()
        {
            // Normalize the string to get the diacritics separated
            string formD = title_input.Text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char ch in formD)
            {
                // Keep the char if it is a letter and not a diacritic
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }
            // Remove non-alphabetic characters
            string alphanumericOnly = Regex.Replace(sb.ToString(), "[^A-Za-z]", "").ToLower();

            // Return the normalized string without diacritics and non-alphabetic characters
            song_checksum.Text = alphanumericOnly;
        }
        private string GetGh3PakFile()
        {
            if (CurrentGame == "GH3")
            {
                return UserPreferences.Default.Gh3QbPak;
            }
            else
            {
                return UserPreferences.Default.GhaQbPak;
            }
        }
        private string GetGh3Folder()
        {
            if (CurrentGame == "GH3")
            {
                return UserPreferences.Default.Gh3FolderPath;
            }
            else
            {
                return UserPreferences.Default.GhaFolderPath;
            }
        }
        private string GetVenue(int venueType)
        {
            string venue;
            switch (venueType)
            {
                case 0:
                    venue = "GH3";
                    break;
                case 1:
                    venue = "GHA";
                    break;
                default:
                    venue = "GHWT";
                    break;
            }
            return venue;
        }
        private void CompileGh3PakFile()
        {

            string venue = GetVenue(venue_source_gh3.SelectedIndex);

            string pakFile = PAK.CreateSongPackage(
                midiPath: midi_file_input_gh3.Text,
                savePath: compile_input.Text,
                songName: song_checksum.Text,
                game: CurrentGame,
                gameConsole: CurrentPlatform,
                hopoThreshold: (int)HmxHopoVal.Value,
                skaPath: ska_files_input_gh3.Text,
                perfOverride: perf_override_input_gh3.Text,
                songScripts: song_script_input_gh3.Text,
                skaSource: GetSkaSourceGh3(),
                venueSource: venue,
                rhythmTrack: p2_rhythm_check.Checked,
                overrideBeat: use_beat_check.Checked,
                hopoType: hopo_mode_select.SelectedIndex,
                isSteven: vocal_gender_select_gh3.Text == "Steven Tyler");

            if (CurrentPlatform == "PC")
            {
                AddToPCSetlist();
                MoveToGh3SongsFolder(pakFile);
            }
            else if (CurrentPlatform == "PS2")
            {
                Console.WriteLine("How'd you get here?");
            }
            else
            {

            }
            // Add code to delete the folder after processing eventually
        }
        private void CompileGhwtPakFile()
        {

            string venue = GetVenue(venueSource.SelectedIndex);

            string pakFile = PAK.CreateSongPackage(
                midiPath: midiFileInput.Text,
                savePath: compile_input.Text,
                songName: song_checksum.Text,
                game: CurrentGame,
                gameConsole: CurrentPlatform,
                hopoThreshold: (int)HmxHopoVal.Value,
                skaPath: skaFilesInput.Text,
                perfOverride: perfOverrideInput.Text,
                songScripts: songScriptInput.Text,
                skaSource: GetSkaSourceGhwt(),
                venueSource: venue,
                overrideBeat: use_beat_check.Checked,
                hopoType: hopo_mode_select.SelectedIndex);

            if (CurrentPlatform == "PC")
            {
                WtSongFolder = Path.Combine(Pref.WtModsFolder, song_checksum.Text);
                ContentFolder = Path.Combine(WtSongFolder, "Content");
                MusicFolder = Path.Combine(ContentFolder, "Music");
                string pakName = Path.GetFileName(pakFile);

                Directory.CreateDirectory(MusicFolder);

                File.Move(pakFile, Path.Combine(ContentFolder, $"a{pakName}"), true);
                WriteWtdeIni(WtSongFolder);
            }
            // Add code to delete the folder after processing eventually
        }
        private void WriteWtdeIni(string saveFolder)
        {
            var config = new IniParserConfiguration();
            config.AssigmentSpacer = "";
            var noSpaceParser = new IniParser.Parser.IniDataParser(config);

            var parser = new FileIniDataParser(noSpaceParser);
            var ini = GenerateWtdeIni();
            var iniPath = Path.Combine(saveFolder, "song.ini");
            parser.WriteFile(iniPath, ini);
        }
        private void MoveToGh3SongsFolder(string pakPath)
        {
            string gameFolder = GetGh3Folder();
            string saveFolder = Path.Combine(gameFolder, SONGSPath);
            string savePath = Path.Combine(saveFolder, Path.GetFileName(pakPath));
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            File.Move(pakPath, savePath, true);
        }
        private void MoveToGh3MusicFolder(string audioPath)
        {
            string gameFolder = GetGh3Folder();
            string saveFolder = Path.Combine(gameFolder, MUSICPath);
            string savePath = Path.Combine(saveFolder, Path.GetFileName(audioPath));
            if (!savePath.EndsWith(".xen"))
            {
                savePath += ".xen";
            }
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            File.Move(audioPath, savePath, true);
        }

        private QBStruct.QBStructData GenerateGh3SongListEntry()
        {
            string STEVEN = "Steven Tyler";

            var entry = new QBStruct.QBStructData();
            string pString = CurrentPlatform == CONSOLE_PS2 ? STRING : WIDESTRING; // Depends on the platform
            bool artistIsOther = artist_text_select.Text == "Other";
            string artistText = !artistIsOther ? $"artist_text_{artist_text_select.Text.ToLower().Replace(" ", "_")}" : artistTextCustom.Text;
            string artistType = artistIsOther ? pString : POINTER;
            string bassist = BassistName();
            string singer = SingerName();
            string gender = (singer == STEVEN) ? "male" : singer;

            entry.AddVarToStruct("checksum", song_checksum.Text, QBKEY);
            entry.AddVarToStruct("name", song_checksum.Text, STRING);
            entry.AddVarToStruct("title", title_input.Text, pString);
            entry.AddVarToStruct("artist", artist_input.Text, pString);
            entry.AddVarToStruct("year", $", {year_input.Value}", pString);
            entry.AddVarToStruct("artist_text", artistText, artistType);
            entry.AddIntToStruct("original_artist", IsArtistFamousBy() ? 0 : 1);
            entry.AddVarToStruct("version", "gh3", QBKEY);
            entry.AddIntToStruct("leaderboard", 1);
            entry.AddIntToStruct("gem_offset", 0);
            entry.AddIntToStruct("input_offset", 0);
            entry.AddVarToStruct("singer", gender, QBKEY);
            if (CurrentGame == "GHA")
            {
                string band = singer == STEVEN ? aerosmithBand.Text : "default_band";
                entry.AddVarToStruct("band", band, QBKEY);
                if (singer == STEVEN)
                {
                    entry.AddVarToStruct("guitarist_checksum", "aerosmith", QBKEY);
                }
                if (Pref.OverrideBeatLines)
                {
                    var low8 = (int)beat8thLow.Value;
                    var high8 = (int)beat8thHigh.Value;
                    var low16 = (int)beat16thLow.Value;
                    var high16 = (int)beat16thHigh.Value;
                    if (low8 != 1)
                    {
                        entry.AddIntToStruct("thin_fretbar_8note_params_low_bpm", low8);
                    }
                    if (high8 != 180)
                    {
                        entry.AddIntToStruct("thin_fretbar_8note_params_high_bpm", high8);
                    }
                    if (low16 != 1)
                    {
                        entry.AddIntToStruct("thin_fretbar_16note_params_low_bpm", low16);
                    }
                    if (high16 != 120)
                    {
                        entry.AddIntToStruct("thin_fretbar_16note_params_high_bpm", high16);
                    }
                }
            }
            entry.AddVarToStruct("keyboard", "false", QBKEY);
            entry.AddFloatToStruct("band_playback_volume", (float)gh3_band_vol.Value);
            entry.AddFloatToStruct("guitar_playback_volume", (float)gh3_gtr_vol.Value);
            entry.AddVarToStruct("countoff", countoff_select_gh3.Text, STRING);
            entry.AddIntToStruct("rhythm_track", p2_rhythm_check.Checked ? 1 : 0);
            if (coop_audio_check.Checked)
            {
                entry.AddFlagToStruct("use_coop_notetracks", QBKEY);
            }
            entry.AddFloatToStruct("hammer_on_measure_scale", (float)nsHopoThreshold);
            if (bassist != "Default")
            {
                entry.AddVarToStruct("bassist", bassist, QBKEY);
            }
            return entry;
        }
        private string GetArtistText()
        {
            bool artistIsOther = artist_text_select.Text == "Other";
            string artistText = !artistIsOther ? $"artist_text_{artist_text_select.Text.ToLower().Replace(" ", "_")}" : artistTextCustom.Text;
            return artistText;
        }
        private bool IsArtistFamousBy()
        {
            return artist_text_select.Text == "As Made Famous By";
        }
        private IniData GenerateWtdeIni()
        {
            var config = new IniParserConfiguration();
            config.AssigmentSpacer = "";

            IniData wtdeIni = new IniData();
            wtdeIni.Configuration = config;
            var modInfo = new SectionData("ModInfo");
            modInfo.Keys.AddKey("Name", song_checksum.Text);
            modInfo.Keys.AddKey("Description", "Generated with Addy's Song Compiler");
            modInfo.Keys.AddKey("Author", chart_author_input.Text);
            modInfo.Keys.AddKey("Version", "1");


            var songInfo = new SectionData("SongInfo");
            songInfo.Keys.AddKey("Checksum", song_checksum.Text);
            songInfo.Keys.AddKey("Title", title_input.Text);
            songInfo.Keys.AddKey("Artist", artist_input.Text);
            songInfo.Keys.AddKey("Year", year_input.Value.ToString("G0", Murica));
            songInfo.Keys.AddKey("ArtistText", GetArtistText());
            songInfo.Keys.AddKey("OriginalArtist", IsArtistFamousBy() ? "0" : "1");
            songInfo.Keys.AddKey("Leaderboard", "1");
            songInfo.Keys.AddKey("Singer", vocalGenderSelect.Text);
            songInfo.Keys.AddKey("Genre", genre_input.Text);
            songInfo.Keys.AddKey("Countoff", countoffSelect.Text);
            songInfo.Keys.AddKey("Volume", overallVolume.Value.ToString("G2", Murica));

            if (!string.IsNullOrEmpty(gameIconInput.Text))
            {
                songInfo.Keys.AddKey("GameIcon", gameIconInput.Text);
            }
            if (!string.IsNullOrEmpty(gameCategoryInput.Text))
            {
                songInfo.Keys.AddKey("SongIcon", gameCategoryInput.Text);
            }
            if (!string.IsNullOrEmpty(bandInput.Text))
            {
                songInfo.Keys.AddKey("Band", bandInput.Text);
            }
            if (useNewClipsCheck.Checked)
            {
                songInfo.Keys.AddKey("UseNewClips", "1");
            }
            if (bSkeletonSelect.Text.ToLower() != DEFAULT && !string.IsNullOrEmpty(bSkeletonSelect.Text))
            {
                songInfo.Keys.AddKey("SkeletonTypeB", bSkeletonSelect.Text);
            }
            if (dSkeletonSelect.Text.ToLower() != DEFAULT && !string.IsNullOrEmpty(dSkeletonSelect.Text))
            {
                songInfo.Keys.AddKey("SkeletonTypeD", dSkeletonSelect.Text);
            }
            if (gSkeletonSelect.Text.ToLower() != DEFAULT && !string.IsNullOrEmpty(gSkeletonSelect.Text))
            {
                songInfo.Keys.AddKey("SkeletonTypeG", gSkeletonSelect.Text);
            }
            if (vSkeletonSelect.Text.ToLower() != DEFAULT && !string.IsNullOrEmpty(vSkeletonSelect.Text))
            {
                songInfo.Keys.AddKey("SkeletonTypeV", vSkeletonSelect.Text);
            }
            if (bassMicCheck.Checked)
            {
                songInfo.Keys.AddKey("MicForBassist", "1");
            }
            if (guitarMicCheck.Checked)
            {
                songInfo.Keys.AddKey("MicForGuitarist", "1");
            }
            if (Pref.OverrideBeatLines)
            {
                songInfo.Keys.AddKey("Low8Bars", beat8thLow.Value.ToString());
                songInfo.Keys.AddKey("High8Bars", beat8thHigh.Value.ToString());
                songInfo.Keys.AddKey("Low16Bars", beat16thLow.Value.ToString());
                songInfo.Keys.AddKey("High16Bars", beat16thHigh.Value.ToString());
            }
            songInfo.Keys.AddKey("Cents", ((int)vocalTuningCents.Value).ToString("G0", Murica));
            songInfo.Keys.AddKey("WhammyCutoff", sustainThreshold.Value.ToString("G2", Murica));
            songInfo.Keys.AddKey("VocalsScrollSpeed", vocalScrollSpeed.Value.ToString("G2", Murica));

            if (modernStrobesCheck.Checked)
            {
                songInfo.Keys.AddKey("ModernStrobes", "1");
            }

            wtdeIni.Sections.Add(modInfo);
            wtdeIni.Sections.Add(songInfo);

            return wtdeIni;
        }
        private string BassistName()
        {
            if (CurrentGame == GAME_GHA)
            {
                return "Default";
            }
            string bassist = bassist_select_gh3.Text;
            if (bassist == "Tom Morello")
            {
                return "Morello";
            }
            else if (bassist == "Lou")
            {
                return "Satan";
            }
            else if (bassist == "God of Rock/Metalhead")
            {
                return CurrentPlatform == CONSOLE_PS2 ? "Metalhead" : "RockGod";
            }
            else if (bassist == "Grim Ripper/Elroy")
            {
                return CurrentPlatform == CONSOLE_PS2 ? "Elroy" : "Ripper";
            }
            return bassist;
        }
        private string SingerName()
        {
            string singer = vocal_gender_select_gh3.Text;
            if (singer == "Bret Michaels")
            {
                return "Bret";
            }
            return singer;
        }
        private void AddToPCSetlist()
        {
            //ReplaceGh3PakFiles();
            string qbPakLocation = GetGh3PakFile();
            var pakCompiler = new PAK.PakCompiler(CurrentGame, CurrentPlatform, split: true);
            var qbPak = PAK.PakEntryDictFromFile(qbPakLocation);
            var songList = qbPak[songlistRef];
            var songListEntries = QB.QbEntryDictFromBytes(songList.EntryData, "big");
            var dlSongList = songListEntries[gh3Songlist].Data as QBArray.QBArrayNode;
            var dlSongListProps = songListEntries[permanentProps].Data as QBStruct.QBStructData;
            var songPropsTest = songListEntries["permanent_songlist_props"].Data as QBStruct.QBStructData;
            var songListEntry = GenerateGh3SongListEntry();
            var songIndex = dlSongList.GetItemIndex(song_checksum.Text, QBKEY);
            if (songIndex == -1)
            {
                dlSongList.AddQbkeyToArray(song_checksum.Text);
                dlSongListProps.AddStructToStruct(song_checksum.Text, songListEntry);
            }
            else
            {
                dlSongListProps[song_checksum.Text] = songListEntry;
            }
            byte[] songlistBytes = QB.CompileQbFromDict(songListEntries, songlistRef, CurrentGame, CurrentPlatform);
            songList.OverwriteData(songlistBytes);

            var downloadQb = qbPak[downloadRef];
            var downloadQbEntries = QB.QbEntryDictFromBytes(downloadQb.EntryData, "big");
            var downloadlist = downloadQbEntries[gh3DownloadSongs].Data as QBStruct.QBStructData;
            var tier1 = downloadlist["tier1"] as QBStruct.QBStructData;
            var songArray = tier1["songs"] as QBArray.QBArrayNode;


            if (songArray.GetItemIndex(song_checksum.Text, QBKEY) == -1)
            {
                songArray.AddQbkeyToArray(song_checksum.Text);
                tier1["defaultunlocked"] = songArray.Items.Count;
            }
            byte[] downloadQbBytes = QB.CompileQbFromDict(downloadQbEntries, downloadRef, CurrentGame, CurrentPlatform);
            downloadQb.OverwriteData(downloadQbBytes);

            var (pakData, pabData) = pakCompiler.CompilePakFromDictionary(qbPak);
            OverwriteGh3Pak(pakData, pabData!);
        }
        private async Task CompileGh3Audio()
        {
            string gtrOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_guitar.mp3");
            string rhythmOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_rhythm.mp3");
            string backingOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_song.mp3");
            string coopGtrOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_coop_guitar.mp3");
            string coopRhythmOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_coop_rhythm.mp3");
            string coopBackingOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_coop_song.mp3");
            string crowdOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_crowd.mp3");
            string previewOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_preview.mp3");
            string[] spFiles = { gtrOutput, rhythmOutput, backingOutput };
            string[] coopFiles = { coopGtrOutput, coopRhythmOutput, coopBackingOutput };
            var filesToProcess = new List<string>();
            filesToProcess.AddRange(spFiles);
            filesToProcess.AddRange(coopFiles);
            filesToProcess.Add(crowdOutput);
            filesToProcess.Add(previewOutput);
            string fsbOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}");
            try
            {
                Console.WriteLine("Compiling Audio...");
                string[] backingPaths = backing_input_gh3.Items.Cast<string>().ToArray();

                string[] coopBackingPaths = coop_backing_input_gh3.Items.Cast<string>().ToArray();

                FSB fsb = new FSB();

                Task gtrStem = fsb.ConvertToMp3(guitar_input_gh3.Text, gtrOutput);
                Task rhythmStem = fsb.ConvertToMp3(rhythm_input_gh3.Text, rhythmOutput);
                Task backingStem = fsb.MixFiles(backingPaths, backingOutput);

                var tasksToAwait = new List<Task> { gtrStem, rhythmStem, backingStem };
                if (CurrentGame == GAME_GHA && File.Exists(crowd_input_gh3.Text))
                {
                    Task crowdStem = fsb.ConvertToMp3(crowd_input_gh3.Text, crowdOutput);
                    tasksToAwait.Add(crowdStem);
                }
                if (coop_audio_check.Checked)
                {
                    Task coopGtrStem = fsb.ConvertToMp3(coop_guitar_input_gh3.Text, coopGtrOutput);
                    Task coopRhythmStem = fsb.ConvertToMp3(coop_rhythm_input_gh3.Text, coopRhythmOutput);
                    Task coopBackingStem = fsb.MixFiles(coopBackingPaths, coopBackingOutput);
                    tasksToAwait.AddRange(new List<Task> { coopGtrStem, coopRhythmStem, coopBackingStem });
                }


                // Await all started tasks. This ensures all conversions are completed before moving on.
                await Task.WhenAll(tasksToAwait.ToArray());

                // Create the preview audio
                if (gh3_rendered_preview_check.Checked)
                {
                    Task previewStem = fsb.ConvertToMp3(gtrOutput, previewOutput);
                    await previewStem;
                }
                else
                {
                    decimal previewStart = previewStartTime / 1000m;
                    decimal previewLength = previewEndTime / 1000m;
                    if (gh3_set_end.Checked)
                    {
                        previewLength -= previewStart;
                    }

                    decimal fadeIn = UserPreferences.Default.PreviewFadeIn;
                    decimal fadeOut = UserPreferences.Default.PreviewFadeOut;
                    Task previewStem = fsb.MakePreview(spFiles, previewOutput, previewStart, previewLength, fadeIn, fadeOut, previewVolumeGh3.Value);
                    await previewStem;
                }
                Console.WriteLine("Combining Audio...");
                var (fsbOut, datOut) = fsb.CombineFSB3File(filesToProcess, fsbOutput);
                if (CurrentPlatform == "PC")
                {
                    MoveToGh3MusicFolder(fsbOut);
                    MoveToGh3MusicFolder(datOut);
                }
                Console.WriteLine("Audio Compilation Complete!");
            }
            catch (Exception ex)
            {
                HandleException(ex, "Audio Compilation Failed!");
                throw;
            }
            finally
            {

                foreach (string file in filesToProcess)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
        }
        private async Task CompileGhwtAudio()
        {
            string drums1Output = Path.Combine(compile_input.Text, $"{song_checksum.Text}_drums1.mp3");
            string drums2Output = Path.Combine(compile_input.Text, $"{song_checksum.Text}_drums2.mp3");
            string drums3Output = Path.Combine(compile_input.Text, $"{song_checksum.Text}_drums3.mp3");
            string drums4Output = Path.Combine(compile_input.Text, $"{song_checksum.Text}_drums4.mp3");

            string guitarOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_guitar.mp3");
            string rhythmOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_rhythm.mp3");
            string vocalsOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_vocals.mp3");

            string crowdOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_crowd.mp3");
            string backingOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_song.mp3");

            string previewOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}_preview.mp3");

            string[] drumFiles = { drums1Output, drums2Output, drums3Output, drums4Output };
            string[] otherFiles = { guitarOutput, rhythmOutput, vocalsOutput };
            string[] backingFiles = { backingOutput, crowdOutput };

            var filesToProcess = new List<string>();
            filesToProcess.AddRange(drumFiles);
            filesToProcess.AddRange(otherFiles);
            filesToProcess.AddRange(backingFiles);
            filesToProcess.Add(previewOutput);

            string fsbOutput = Path.Combine(compile_input.Text, $"{song_checksum.Text}");
            try
            {
                Console.WriteLine("Compiling Audio...");
                string[] backingPaths = backingInput.Items.Cast<string>().ToArray();

                FSB fsb = new FSB();

                Task drums1Stem = fsb.ConvertToMp3(kickInput.Text, drums1Output);
                Task drums2Stem = fsb.ConvertToMp3(snareInput.Text, drums2Output);
                Task drums3Stem = fsb.ConvertToMp3(cymbalsInput.Text, drums3Output);
                Task drums4Stem = fsb.ConvertToMp3(tomsInput.Text, drums4Output);

                Task guitarStem = fsb.ConvertToMp3(guitarInput.Text, guitarOutput);
                Task rhythmStem = fsb.ConvertToMp3(bassInput.Text, rhythmOutput);
                Task vocalsStem = fsb.ConvertToMp3(vocalsInput.Text, vocalsOutput);

                Task backingStem = fsb.MixFiles(backingPaths, backingOutput);
                Task crowdStem = fsb.ConvertToMp3(crowdInput.Text, crowdOutput);

                var tasksToAwait = new List<Task> { drums1Stem, drums2Stem, drums3Stem, drums4Stem, guitarStem, rhythmStem, vocalsStem, backingStem, crowdStem };

                // Await all started tasks. This ensures all conversions are completed before moving on.
                await Task.WhenAll(tasksToAwait.ToArray());

                // Create the preview audio
                if (renderedPreviewCheck.Checked)
                {
                    Task previewStem = fsb.ConvertToMp3(guitarOutput, previewOutput);
                    await previewStem;
                }
                else
                {
                    string[] previewFiles = { drums1Output, drums2Output, drums3Output, drums4Output, guitarOutput, rhythmOutput, vocalsOutput, backingOutput };
                    decimal previewStart = previewStartTime / 1000m;
                    decimal previewLength = previewEndTime / 1000m;
                    if (setEndTime.Checked)
                    {
                        previewLength -= previewStart;
                    }
                    decimal fadeIn = UserPreferences.Default.PreviewFadeIn;
                    decimal fadeOut = UserPreferences.Default.PreviewFadeOut;
                    Task previewStem = fsb.MakePreview(previewFiles, previewOutput, previewStart, previewLength, fadeIn, fadeOut, previewVolume.Value);
                    await previewStem;
                }
                Console.WriteLine("Combining Audio...");
                var fsbList = fsb.CombineFSB4File(drumFiles, otherFiles, backingFiles, [previewOutput], fsbOutput);

                if (CurrentPlatform == "PC")
                {
                    foreach (string file in fsbList)
                    {
                        File.Move(file, Path.Combine(MusicFolder, $"{Path.GetFileName(file)}.xen"), true);
                    }
                }
                Console.WriteLine("Audio Compilation Complete!");
            }
            catch (Exception ex)
            {
                HandleException(ex, "Audio Compilation Failed!");
                throw;
            }
            finally
            {
                foreach (string file in filesToProcess)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        private bool CompilePakGh3()
        {
            bool success = false;
            try
            {
                SaveProject();
                PreCompileCheck();
                CompileGh3PakFile();
                success = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                string errorMessage = "Compilation has failed due to one or more files having \"Read-Only\" permission. " +
                    "To be able to compile, the Toolkit needs to run a separate tool which requires Administrator permissions.\n\n" +
                    "Pressing OK will launch this tool, and will ask for your permission to run it in Administrator mode.\n\n" +
                    "Otherwise, click Cancel, go to the file path the next Error Box mentions and remove Read-Only mode from it.\n\n" +
                    "You will need to re-compile afterwards for either method used.";
                var nextSteps = MessageBox.Show(errorMessage, "Could not access file", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (nextSteps == DialogResult.OK)
                {
                    string removePath = Path.Combine(ExeDirectory, "Tools", "RemoveReadOnly", "RemoveReadOnly.exe");
                    string gamePath = GetGh3Folder();
                    ProcessStartInfo startInfo = new ProcessStartInfo(removePath);
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = true;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.ArgumentList.Add(gamePath);
                    try
                    {
                        // Start the process with the info we specified.
                        // Call WaitForExit and then the using statement will close.
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                        MessageBox.Show("Read-Only permissions have been removed from the game folder. Please re-compile the song.", "RemoveReadOnly Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show(ex2.Message, "RemoveReadOnly Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    HandleException(ex, "Compile Failed!");
                }
            }
            catch (MidiCompileException ex)
            {
                MidiFailException(ex);
            }
            catch (Exception ex)
            {
                HandleException(ex, "Compile Failed!");
            }
            return success;
        }
        private bool CompilePakGhwt()
        {
            bool success = false;
            try
            {
                SaveProject();
                PreCompileCheck();
                CompileGhwtPakFile();
                success = true;
            }
            catch (MidiCompileException ex)
            {
                MidiFailException(ex);
            }
            catch (Exception ex)
            {
                HandleException(ex, "Compile Failed!");
            }
            return success;
        }
        private void compile_pak_button_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Compiling song for {CurrentGame}");
            var time1 = DateTime.Now;
            if (CurrentGame == GAME_GH3 || CurrentGame == GAME_GHA)
            {
                CompilePakGh3();
            }
            else if (CurrentGame == GAME_GHWT)
            {
                CompilePakGhwt();
            }
            var time2 = DateTime.Now;
            // Calculate the time it took to compile the song
            var timeDiff = time2 - time1;
            Console.WriteLine($"Chart compilation took {timeDiff.TotalSeconds.ToString("G3")} seconds");
        }
        private async void compile_all_button_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Compiling chart and audio for {CurrentGame}");
            string compileText = compile_all_button.Text;
            compile_all_button.Text = "Compiling...";
            DisableCloseButton();
            bool compileSuccess = false;
            bool pakSuccess = false;
            try
            {
                var time1 = DateTime.Now;
                if (CurrentGame == GAME_GH3 || CurrentGame == GAME_GHA)
                {
                    pakSuccess = CompilePakGh3();
                    if (pakSuccess)
                    {
                        await CompileGh3Audio();
                        compileSuccess = true;
                    }
                }
                else if (CurrentGame == GAME_GHWT)
                {
                    pakSuccess = CompilePakGhwt();
                    if (pakSuccess)
                    {
                        await CompileGhwtAudio();
                        compileSuccess = true;
                    }
                }
                var time2 = DateTime.Now;

                var timeDiff = time2 - time1;
                Console.WriteLine($"Total compilation took {timeDiff.TotalSeconds.ToString("G3")} seconds");
            }
            catch (Exception ex)
            {
                // Errors are handled in the CompilePakGh3 and CompileGh3All methods
            }
            finally
            {
                EnableCloseButton();
                compile_all_button.Text = compileText;
                if (Pref.ShowPostCompile && compileSuccess)
                {
                    ShowPostCompile();
                }
            }

        }

        private void ShowPostCompile()
        {
            if (CurrentPlatform == platform_pc.Text)
            {
                MessageBox.Show("Compilation has completed successfully!\n\nYour song has been added to the game and can be played immediately.", "Compilation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // Toolstrip Logic

        private void ClearListBoxes()
        {
            backing_input_gh3.Items.Clear();
            coop_backing_input_gh3.Items.Clear();
            backingInput.Items.Clear();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProjectAs();
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
                    LoadProject(project_input.Text);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProject(DefaultTemplatePath);
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
                    LoadProject(filePath);
                    project_input.Text = "";
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a modal window for the preferences panel
            var prefWindow = new ProgramSettings(TabType.CompileSong);
            prefWindow.ShowDialog();
            SetBeatLines();
        }

        private void artist_text_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (artist_text_select.Text == "Other")
            {
                artistTextCustom.Enabled = true;
            }
            else
            {
                artistTextCustom.Enabled = false;
            }
        }
        private void UpdateNsValue()
        {
            nsHopoThreshold = 1920 / HmxHopoVal.Value / 4;
            NsHopoVal.Text = Math.Round(nsHopoThreshold, 5).ToString();
        }
        private void HmxHopoVal_ValueChanged(object sender, EventArgs e)
        {

            UpdateNsValue();
        }

        private void renderedPreviewCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange)
            {
                return;
            }

            previewAudioLabel.Enabled = renderedPreviewCheck.Checked;
            previewInput.Enabled = renderedPreviewCheck.Checked;
            previewSelect.Enabled = renderedPreviewCheck.Checked;

            previewLabel.Enabled = !renderedPreviewCheck.Checked;
            previewMinutes.Enabled = !renderedPreviewCheck.Checked;
            previewSeconds.Enabled = !renderedPreviewCheck.Checked;
            previewMills.Enabled = !renderedPreviewCheck.Checked;
            lengthLabel.Enabled = !renderedPreviewCheck.Checked;
            lengthMinutes.Enabled = !renderedPreviewCheck.Checked;
            lengthSeconds.Enabled = !renderedPreviewCheck.Checked;
            lengthMills.Enabled = !renderedPreviewCheck.Checked;
            setEndTime.Enabled = !renderedPreviewCheck.Checked;
        }

        private void setEndTime_CheckedChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange)
            {
                return;
            }
            gh3_set_end.Checked = setEndTime.Checked;
        }

        private void genre_input_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange || isLoading)
            {
                return;
            }
            gameSelectedGenres[CurrentGame] = genre_input.SelectedIndex;
        }

        private void drumKitSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange || isLoading)
            {
                return;
            }
            gameDrumKits[CurrentGame] = drumKitSelect.SelectedIndex;
        }

        private void import_from_other_Click(object sender, EventArgs e)
        {
            // Open a folder choosing dialog to select a folder
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select the Clone Hero song folder you want to import.";
                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    LoadFromChFolder(folderPath);
                }
            }
        }

        private void vocal_gender_select_gh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vocal_gender_select_gh3.SelectedIndex == 4)
            {
                aerosmithBand.Enabled = true;
                if (aerosmithBand.SelectedIndex == -1)
                {
                    aerosmithBand.SelectedIndex = 0;
                }
            }
            else
            {
                aerosmithBand.Enabled = false;
            }
        }
    }
}
