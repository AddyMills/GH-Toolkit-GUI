using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            public string gameSelect { get; set; } = "";
            public string platformSelect { get; set; } = "";
            public string songName { get; set; } = "";
            public string chartAuthor { get; set; } = "";
            public string title { get; set; } = "";
            public string artist { get; set; } = "";
            public string artistTextCustom { get; set; } = "";
            public string coverArtist { get; set; } = "";
            public string guitarPath { get; set; } = "";
            public string rhythmPath { get; set; } = "";
            public string backingPaths { get; set; } = "";
            public string coopGuitarPath { get; set; } = "";
            public string coopRhythmPath { get; set; } = "";
            public string coopBackingPaths { get; set; } = "";
            public string crowdPath { get; set; } = "";
            public string previewAudioPath { get; set; } = "";
            public string midiPath { get; set; } = "";
            public string perfPath { get; set; } = "";
            public string skaPath { get; set; } = "";
            public string songScriptPath { get; set; } = "";
            public string compilePath { get; set; } = "";
            public string projectPath { get; set; } = "";
            public int artistText { get; set; } = 0;
            public int songYear { get; set; } = 2024;
            public int coverYear { get; set; } = 2024;
            public int genre { get; set; } = 0;
            public int previewStart { get; set; } = 30000;
            public int previewEnd { get; set; } = 60000;
            public int hmxHopoVal { get; set; } = 170;
            public int skaSource { get; set; } = 0;
            public int venueSource { get; set; } = 0;
            public int countoff { get; set; } = 0;
            public int vocalGender { get; set; } = 0;
            public int bassistSelect { get; set; } = 0;
            public int hopoMode { get; set; } = 0;
            public int beat8thLow { get; set; } = 1;
            public int beat8thHigh { get; set; } = 180;
            public int beat16thLow { get; set; } = 1;
            public int beat16thHigh { get; set; } = 120;
            public decimal gtrVolume { get; set; } = 0;
            public decimal bandVolume { get; set; } = 0;
            public bool isCover { get; set; } = false;
            public bool isP2Rhythm { get; set; } = false;
            public bool isCoopAudio { get; set; } = false;
            public bool useGh3RenderedPreview { get; set; } = false;
            public bool setEnd { get; set; } = false;
            public bool useBeatTrack { get; set; } = false;
        }
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
                venueSource = venue_source_gh3.SelectedIndex,
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
            venue_source_gh3.SelectedIndex = data.venueSource;
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
        private void SaveProject()
        {
            if (File.Exists(projectFilePath))
            {
                var data = makeSaveClass();
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(projectFilePath, json);
            }
            else
            {
                SaveProjectAs();
            }
        }
        private void SaveProjectAs()
        {
            var data = makeSaveClass();
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
        private void LoadProject(string filePath)
        {

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
                LoadSaveData(data);
            }
        }
    }
}
