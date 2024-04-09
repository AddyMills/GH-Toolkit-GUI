using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GH_Toolkit_GUI
{
    public partial class CompileSong
    {
        public class SaveData
        {
            public int GhprojVersion = 1;
            [DefaultValue("")]
            public string gameSelect { get; set; } = "";
            [DefaultValue("")]
            public string platformSelect { get; set; } = "";
            [DefaultValue("")]
            public string songName { get; set; } = "";
            [DefaultValue("")]
            public string chartAuthor { get; set; } = "";
            [DefaultValue("")]
            public string title { get; set; } = "";
            [DefaultValue("")]
            public string artist { get; set; } = "";
            [DefaultValue("")]
            public string artistTextCustom { get; set; } = "";
            [DefaultValue("")]
            public string coverArtist { get; set; } = "";
            [DefaultValue("")]
            public string kickPath { get; set; } = "";
            [DefaultValue("")]
            public string snarePath { get; set; } = "";
            [DefaultValue("")]
            public string cymbalsPath { get; set; } = "";
            [DefaultValue("")]
            public string tomsPath { get; set; } = "";
            [DefaultValue("")]
            public string guitarPath { get; set; } = "";
            [DefaultValue("")]
            public string bassPath { get; set; } = "";
            [DefaultValue("")]
            public string vocalsPath { get; set; } = "";
            [DefaultValue("")]
            public string backingPaths { get; set; } = "";
            [DefaultValue("")]
            public string crowdPath { get; set; } = "";
            [DefaultValue("")]
            public string previewAudioPath { get; set; } = "";
            [DefaultValue("")]
            public string guitarPathGh3 { get; set; } = "";
            [DefaultValue("")] 
            public string rhythmPathGh3 { get; set; } = "";
            [DefaultValue("")]
            public string backingPathsGh3 { get; set; } = "";
            [DefaultValue("")]
            public string coopGuitarPath { get; set; } = "";
            [DefaultValue("")]
            public string coopRhythmPath { get; set; } = "";
            [DefaultValue("")]
            public string coopBackingPaths { get; set; } = "";
            [DefaultValue("")]
            public string crowdPathGh3 { get; set; } = "";
            [DefaultValue("")]
            public string previewAudioPathGh3 { get; set; } = "";
            [DefaultValue("")]
            public string midiPathGh3 { get; set; } = "";
            [DefaultValue("")]
            public string perfPathGh3 { get; set; } = "";
            [DefaultValue("")]
            public string skaPathGh3 { get; set; } = "";
            [DefaultValue("")]
            public string songScriptPathGh3 { get; set; } = "";
            [DefaultValue("")]
            public string midiPath { get; set; } = "";
            [DefaultValue("")]
            public string perfPath { get; set; } = "";
            [DefaultValue("")]
            public string skaPath { get; set; } = "";
            [DefaultValue("")]
            public string songScriptPath { get; set; } = "";
            [DefaultValue("")]
            public string compilePath { get; set; } = "";
            [DefaultValue("")]
            public string projectPath { get; set; } = "";
            [DefaultValue("")]
            public string gameIcon { get; set; } = "";
            [DefaultValue("")]
            public string gameCategory { get; set; } = "";
            [DefaultValue("")]
            public string bandWtde { get; set; } = "";
            [DefaultValue("Default")]
            public string gSkeleton { get; set; } = "";
            [DefaultValue("Default")]
            public string bSkeleton { get; set; } = "";
            [DefaultValue("Default")]
            public string dSkeleton { get; set; } = "";
            [DefaultValue("Default")]
            public string vSkeleton { get; set; } = "";
            [DefaultValue(-1)]
            public int artistText { get; set; } = 0;
            public int songYear { get; set; } = 2024;
            public int coverYear { get; set; } = 2024;
            [DefaultValue(-1)]
            public int wtGenre { get; set; } = 0;
            [DefaultValue(-1)]
            public int gh5Genre { get; set; } = 0;
            [DefaultValue(-1)]
            public int worGenre { get; set; } = 0;
            [DefaultValue(30000)]
            public int previewStart { get; set; } = 30000;
            [DefaultValue(30000)]
            public int previewEnd { get; set; } = 30000;
            [DefaultValue(170)]
            public int hmxHopoVal { get; set; } = 170;
            public int skaSourceGh3 { get; set; } = 0;
            public int venueSourceGh3 { get; set; } = 0;
            public int countoffGh3 { get; set; } = 0;
            public int vocalGenderGh3 { get; set; } = 0;
            public int bassistSelect { get; set; } = 0;
            public int skaSource { get; set; } = 0;
            public int venueSource { get; set; } = 0;
            public int countoff { get; set; } = 0;
            public int vocalGender { get; set; } = 0;
            public int hopoMode { get; set; } = 0;
            [DefaultValue(1)]
            public int beat8thLow { get; set; } = 1;
            [DefaultValue(150)]
            public int beat8thHigh { get; set; } = 150;
            [DefaultValue(1)]
            public int beat16thLow { get; set; } = 1;
            [DefaultValue(120)]
            public int beat16thHigh { get; set; } = 120;
            public decimal gtrVolumeGh3 { get; set; } = 0;
            public decimal bandVolumeGh3 { get; set; } = 0;
            [DefaultValue(1)]
            public decimal vocalScrollSpeed { get; set; } = 1;
            public decimal vocalTuningCents { get; set; } = 0;
            [DefaultValue(0.5)]
            public decimal sustainThreshold { get; set; } = 0.5m;
            public decimal overallVolume { get; set; } = 0;
            public bool isCover { get; set; } = false;
            public bool isP2Rhythm { get; set; } = false;
            public bool isCoopAudio { get; set; } = false;
            public bool useRenderedPreview { get; set; } = false;
            public bool setEnd { get; set; } = false;
            public bool useBeatTrack { get; set; } = false;
            public bool guitarMic { get; set; } = false;
            public bool bassMic { get; set; } = false;
            public bool useNewClips { get; set; } = false;
            public bool modernStrobes { get; set; } = false;
        }
        private class SaveDataOld {             
            public string game { get; set; }
            public string title_input { get; set; }
            public string artist_text_select { get; set; }
            public string artist_text_other { get; set; }
            public string artist_input { get; set; }
            public int year_input { get; set; }
            public string cover_checkbox { get; set; }
            public int cover_year_input { get; set; }
            public string cover_artist_input { get; set; }
            public string genre_select { get; set; }
            public string checksum_input { get; set; }
            public string author_input { get; set; }
            public string ghwt_genre { get; set; }
            public string ghwor_genre { get; set; }
            public string kick_input { get; set; }
            public string snare_input { get; set; }
            public string cymbals_input { get; set; }
            public string toms_input { get; set; }
            public string guitar_input { get; set; }
            public string bass_input { get; set; }
            public string vocals_input { get; set; }
            public string backing_input { get; set; }
            public string crowd_input { get; set; }
            public int preview_minutes { get; set; }
            public int preview_seconds { get; set; }
            public int preview_mills { get; set; }
            public bool ghwt_set_end { get; set; }
            public int length_minutes { get; set; }
            public int length_seconds { get; set; }
            public int length_mills { get; set; }
            // Continue later

        }
        private SaveData makeSaveClass()
        {
            var data = new SaveData
            {
                // Metadata
                gameSelect = GetGame(),
                platformSelect = GetPlatform(),
                songName = song_checksum.Text,
                chartAuthor = chart_author_input.Text,
                title = title_input.Text,
                artist = artist_input.Text,
                artistTextCustom = artistTextCustom.Text,
                coverArtist = cover_artist_input.Text,

                // GHWT
                // Audio
                kickPath = kickInput.Text,
                snarePath = snareInput.Text,
                cymbalsPath = cymbalsInput.Text,
                tomsPath = tomsInput.Text,
                guitarPath = guitarInput.Text,
                bassPath = bassInput.Text,
                vocalsPath = vocalsInput.Text,
                backingPaths = string.Join(";", backingInput.Items.Cast<string>().ToArray()),
                crowdPath = crowdInput.Text,
                previewAudioPath = previewInput.Text,
                // Song Data
                midiPath = midiFileInput.Text,
                perfPath = perfOverrideInput.Text,
                skaPath = skaFilesInput.Text,
                songScriptPath = songScriptInput.Text,
                skaSource = skaFileSource.SelectedIndex,
                venueSource = venueSource.SelectedIndex,
                countoff = countoffSelect.SelectedIndex,
                vocalGender = vocalGenderSelect.SelectedIndex,
                vocalScrollSpeed = vocalScrollSpeed.Value,
                vocalTuningCents = vocalTuningCents.Value,
                sustainThreshold = sustainThreshold.Value,
                overallVolume = overallVolume.Value,
                guitarMic = guitarMicCheck.Checked,
                bassMic = bassMicCheck.Checked,
                // WTDE Settings
                gameIcon = gameIconInput.Text,
                gameCategory = gameCategoryInput.Text,
                bandWtde = bandInput.Text,
                gSkeleton = gSkeletonSelect.Text,
                bSkeleton = bSkeletonSelect.Text,
                dSkeleton = dSkeletonSelect.Text,
                vSkeleton = vSkeletonSelect.Text,
                useNewClips = useNewClipsCheck.Checked,
                modernStrobes = modernStrobesCheck.Checked,

                // GH3
                // Audio
                guitarPathGh3 = guitar_input_gh3.Text,
                rhythmPathGh3 = rhythm_input_gh3.Text,
                backingPathsGh3 = string.Join(";", backing_input_gh3.Items.Cast<string>().ToArray()),
                coopGuitarPath = coop_guitar_input_gh3.Text,
                coopRhythmPath = coop_rhythm_input_gh3.Text,
                coopBackingPaths = string.Join(";", coop_backing_input_gh3.Items.Cast<string>().ToArray()),
                crowdPathGh3 = crowd_input_gh3.Text,
                previewAudioPathGh3 = preview_audio_input_gh3.Text,
                gtrVolumeGh3 = gh3_gtr_vol.Value,
                bandVolumeGh3 = gh3_band_vol.Value,
                // Song Data
                midiPathGh3 = midi_file_input_gh3.Text,
                perfPathGh3 = perf_override_input_gh3.Text,
                skaPathGh3 = ska_files_input_gh3.Text,
                songScriptPathGh3 = song_script_input_gh3.Text,
                skaSourceGh3 = ska_file_source_gh3.SelectedIndex,
                venueSourceGh3 = venue_source_gh3.SelectedIndex,
                countoffGh3 = countoff_select_gh3.SelectedIndex,
                vocalGenderGh3 = vocal_gender_select_gh3.SelectedIndex,
                bassistSelect = bassist_select_gh3.SelectedIndex,
                isP2Rhythm = p2_rhythm_check.Checked,
                isCoopAudio = coop_audio_check.Checked,
                useRenderedPreview = gh3_rendered_preview_check.Checked,
                setEnd = gh3_set_end.Checked,
                // Other
                compilePath = compile_input.Text,
                projectPath = project_input.Text,
                // Metadata
                artistText = artist_text_select.SelectedIndex,
                songYear = (int)year_input.Value,
                coverYear = (int)cover_year_input.Value,
                wtGenre = gameSelectedGenres["GHWT"],
                gh5Genre = gameSelectedGenres["GH5"],
                worGenre = gameSelectedGenres["GHWoR"],
                previewStart = previewStartTime,
                previewEnd = previewEndTime,
                hmxHopoVal = (int)HmxHopoVal.Value,
                hopoMode = hopo_mode_select.SelectedIndex,
                beat8thLow = (int)beat8thLow.Value,
                beat8thHigh = (int)beat8thHigh.Value,
                beat16thLow = (int)beat16thLow.Value,
                beat16thHigh = (int)beat16thHigh.Value,
                isCover = isCover.Checked,
                useBeatTrack = use_beat_check.Checked
            };
            return data;
        }
        private void LoadSaveData(SaveData data)
        {
            isProgrammaticChange = true;
            ClearListBoxes();
            // Metadata
            game_layout.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == data.gameSelect).Checked = true;
            platform_layout.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == data.platformSelect).Checked = true;
            song_checksum.Text = data.songName;
            chart_author_input.Text = data.chartAuthor;
            title_input.Text = data.title;
            artist_input.Text = data.artist;
            artistTextCustom.Text = data.artistTextCustom;
            cover_artist_input.Text = data.coverArtist;


            // GH3 Audio
            guitar_input_gh3.Text = data.guitarPathGh3;
            rhythm_input_gh3.Text = data.rhythmPathGh3;
            if (data.backingPathsGh3 != "")
            {
                backing_input_gh3.Items.AddRange(data.backingPathsGh3.Split(';'));
            }
            coop_guitar_input_gh3.Text = data.coopGuitarPath;
            coop_rhythm_input_gh3.Text = data.coopRhythmPath;
            if (data.coopBackingPaths != "")
            {
                coop_backing_input_gh3.Items.AddRange(data.coopBackingPaths.Split(';'));
            }
            crowd_input_gh3.Text = data.crowdPathGh3;
            preview_audio_input_gh3.Text = data.previewAudioPathGh3;

            // GH3 Song Data
            midi_file_input_gh3.Text = data.midiPathGh3;
            perf_override_input_gh3.Text = data.perfPathGh3;
            ska_files_input_gh3.Text = data.skaPathGh3;
            song_script_input_gh3.Text = data.songScriptPathGh3;

            // Other Settings
            compile_input.Text = data.compilePath;
            project_input.Text = data.projectPath;

            // Metadata
            artist_text_select.SelectedIndex = data.artistText;
            year_input.Value = data.songYear;
            cover_year_input.Value = data.coverYear;
            gameSelectedGenres["GHWT"] = data.wtGenre;
            gameSelectedGenres["GH5"] = data.gh5Genre;
            gameSelectedGenres["WOR"] = data.worGenre;
           
            HmxHopoVal.Value = data.hmxHopoVal;
            hopo_mode_select.SelectedIndex = data.hopoMode;
            beat8thLow.Value = data.beat8thLow;
            beat8thHigh.Value = data.beat8thHigh;
            beat16thLow.Value = data.beat16thLow;
            beat16thHigh.Value = data.beat16thHigh;
            isCover.Checked = data.isCover;
            use_beat_check.Checked = data.useBeatTrack;

            // GH3 Additional Settings
            ska_file_source_gh3.SelectedIndex = data.skaSourceGh3;
            venue_source_gh3.SelectedIndex = data.venueSourceGh3;
            countoff_select_gh3.SelectedIndex = data.countoffGh3;
            vocal_gender_select_gh3.SelectedIndex = data.vocalGenderGh3;
            bassist_select_gh3.SelectedIndex = data.bassistSelect;
            gh3_gtr_vol.Value = data.gtrVolumeGh3;
            gh3_band_vol.Value = data.bandVolumeGh3;
            p2_rhythm_check.Checked = data.isP2Rhythm;
            coop_audio_check.Checked = data.isCoopAudio;
            gh3_rendered_preview_check.Checked = data.useRenderedPreview;
            gh3_set_end.Checked = data.setEnd;

            // GHWT Audio
            kickInput.Text = data.kickPath;
            snareInput.Text = data.snarePath;
            cymbalsInput.Text = data.cymbalsPath;
            tomsInput.Text = data.tomsPath;
            guitarInput.Text = data.guitarPath;
            bassInput.Text = data.bassPath;
            vocalsInput.Text = data.vocalsPath;
            if (data.backingPaths != "")
            {
                backingInput.Items.AddRange(data.backingPaths.Split(';'));
            }
            crowdInput.Text = data.crowdPath;
            previewInput.Text = data.previewAudioPath;

            // Song Data
            midiFileInput.Text = data.midiPath;
            perfOverrideInput.Text = data.perfPath;
            skaFilesInput.Text = data.skaPath;
            songScriptInput.Text = data.songScriptPath;
            skaFileSource.SelectedIndex = data.skaSource;
            venueSource.SelectedIndex = data.venueSource;
            countoffSelect.SelectedIndex = data.countoff;
            vocalGenderSelect.SelectedIndex = data.vocalGender;
            vocalScrollSpeed.Value = data.vocalScrollSpeed;
            vocalTuningCents.Value = data.vocalTuningCents;
            sustainThreshold.Value = data.sustainThreshold;
            overallVolume.Value = data.overallVolume;
            guitarMicCheck.Checked = data.guitarMic;
            bassMicCheck.Checked = data.bassMic;

            // WTDE Settings
            gameIconInput.Text = data.gameIcon;
            gameCategoryInput.Text = data.gameCategory;
            bandInput.Text = data.bandWtde;
            gSkeletonSelect.Text = data.gSkeleton;
            bSkeletonSelect.Text = data.bSkeleton;
            dSkeletonSelect.Text = data.dSkeleton;
            vSkeletonSelect.Text = data.vSkeleton;
            useNewClipsCheck.Checked = data.useNewClips;
            modernStrobesCheck.Checked = data.modernStrobes;
            previewStartTime = data.previewStart;
            previewEndTime = data.previewEnd;
            UpdatePreviewFields(); // This needs to be changed. Currently broken
            isProgrammaticChange = false;
            

            SetAll();
        }
        private void SaveProject()
        {
            if (File.Exists(projectFilePath))
            {
                var data = makeSaveClass();

                // Custom serializer settings to ignore default values
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(data, settings);
                File.WriteAllText(projectFilePath, json);
            }
            else
            {
                SaveProjectAs();
            }
        }

        private void SaveProjectAs()
        {
            
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = ghprojFileFilter; // Ensure this is defined somewhere in your code
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    project_input.Text = saveFileDialog.FileName;
                    var data = makeSaveClass();
                    // Custom serializer settings to ignore default values
                    var settings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    };

                    string json = JsonConvert.SerializeObject(data, settings);
                    File.WriteAllText(saveFileDialog.FileName, json); // Use the selected file name for saving
                }
            }
        }
        private void LoadProject(string filePath)
        {

            if (File.Exists(filePath))
            {
                isLoading = true;
                string json = File.ReadAllText(filePath);
                SaveData data = JsonConvert.DeserializeObject<SaveData>(json);

                LoadSaveData(data);
                isLoading = false;
            }
        }
    }
}
