using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GH_Toolkit_GUI
{
    internal class Ghproj
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
            public int beat8thHigh { get; set; } = 150;
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
    }
}
