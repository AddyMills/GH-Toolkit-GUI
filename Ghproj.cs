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
            public string gameSelect { get; set; }
            public string platformSelect { get; set; }
            public string songName { get; set; }
            public string chartAuthor { get; set; }
            public string title { get; set; }
            public string artist { get; set; }
            public string artistTextCustom { get; set; }
            public string coverArtist { get; set; }
            public string guitarPath { get; set; }
            public string rhythmPath { get; set; }
            public string backingPaths { get; set; }
            public string coopGuitarPath { get; set; }
            public string coopRhythmPath { get; set; }
            public string coopBackingPaths { get; set; }
            public string crowdPath { get; set; }
            public string previewAudioPath { get; set; }
            public string midiPath { get; set; }
            public string perfPath { get; set; }
            public string skaPath { get; set; }
            public string songScriptPath { get; set; }
            public string compilePath { get; set; }
            public string projectPath { get; set; }
            public int artistText { get; set; }
            public int songYear { get; set; }
            public int coverYear { get; set; }
            public int genre { get; set; }
            public int previewStart { get; set; }
            public int previewEnd { get; set; }
            public int hmxHopoVal { get; set; }
            public int skaSource { get; set; }
            public int countoff { get; set; }
            public int vocalGender { get; set; }
            public int bassistSelect { get; set; }
            public int hopoMode { get; set; }
            public int beat8thLow { get; set; }
            public int beat8thHigh { get; set; }
            public int beat16thLow { get; set; }
            public int beat16thHigh { get; set; }
            public decimal gtrVolume { get; set; }
            public decimal bandVolume { get; set; }
            public bool isCover { get; set; }
            public bool isP2Rhythm { get; set; }
            public bool isCoopAudio { get; set; }
            public bool useGh3RenderedPreview { get; set; }
            public bool setEnd { get; set; }
            public bool useBeatTrack { get; set; }
        }
    }
}
