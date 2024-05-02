using GH_Toolkit_Core.QB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GH_Toolkit_Exceptions.Exceptions;
using static GH_Toolkit_Core.Checksum.CRC;
using static GH_Toolkit_GUI.PreCompileChecks;
using static GH_Toolkit_Core.Methods.CreateForGame;
using static GH_Toolkit_Core.QB.QBConstants;
using static GH_Toolkit_Core.QB.QBArray;
using static GH_Toolkit_Core.QB.QBStruct;
using static GH_Toolkit_Core.PAK.PAK;
using static GH_Toolkit_Core.QB.QB;
using GH_Toolkit_Core.PAK;

namespace GH_Toolkit_GUI
{
    public partial class SongListManager : Form
    {
        private Dictionary<string, QBStruct.QBStructData> MasterList = new Dictionary<string, QBStruct.QBStructData>();
        private static UserPreferences Pref = UserPreferences.Default;
        private string Game;
        private string PakFile;
        private Dictionary<string, PakEntry> QbPak;
        private PakCompiler Compiler;
        private PakEntry Songlist;
        private Dictionary<string, QBItem> SongListEntries;
        private QBArrayNode DlSongList;
        private QBStructData DlSongListProps;
        private PakEntry DownloadQb;
        private Dictionary<string, QBItem> DownloadQbEntries;
        private QBStructData DownloadList;
        private QBStructData Tier1;
        private QBArrayNode SongArray;

        public SongListManager()
        {
            InitializeComponent();
            gh3Radio.Checked = true;
        }
        private void LoadSetlist()
        {
            Gh3PcCheck(Game);
            MasterList.Clear();
            songList.Items.Clear();
            PakFile = GetGh3PakFile(Game);
            Compiler = new PakCompiler(GAME_GH3, CONSOLE_PC, split: true);
            QbPak = PakEntryDictFromFile(PakFile);
            (Songlist, SongListEntries, DlSongList, DlSongListProps) = GetSongListPak(QbPak);
            (DownloadQb, DownloadQbEntries, DownloadList) = GetDownloadPak(QbPak);
            Tier1 = DownloadList["tier1"] as QBStructData;
            SongArray = Tier1["songs"] as QBArrayNode;
            foreach (string songHash in SongArray.Items)
            {
                var song = DlSongListProps[songHash] as QBStructData;
                var songName = song["name"] as string;
                var songTitle = song["title"] as string;
                var songArtist = song["artist"] as string;
                songList.Items.Add($"{songName} ({songTitle} - {songArtist})");
            }
        }
        private void DeleteSongs()
        {
            if (songList.CheckedItems.Count == 0)
            {
                MessageBox.Show("No songs selected to delete.", "No Songs Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<int> songArrayIndeces = new List<int>();
            List<int> dlSonglistIndeces = new List<int>();
            List<int> dlSongPropsIndeces = new List<int>();

            var musicFolder = Path.Combine(GetGh3Folder(Game), "DATA", "MUSIC");
            var songsFolder = Path.Combine(GetGh3Folder(Game), "DATA", "SONGS");

            foreach (string song in songList.CheckedItems)
            {
                var songName = song.Split(' ')[0];
                songArrayIndeces.Add(SongArray.GetItemIndex(songName, QBKEY));
                dlSonglistIndeces.Add(DlSongList.GetItemIndex(songName, QBKEY));
                for (int i = 0; i < DlSongListProps.Items.Count; i++)
                {
                    var item = DlSongListProps.Items[i] as QBStructItem;
                    var itemData = item.Data as QBStructData;

                    if (itemData["name"] as string == songName)
                    {
                        dlSongPropsIndeces.Add(i);
                        break;
                    }
                }
                if (Pref.SongManagerDeleteSongs)
                {
                    var fsbPath = Path.Combine(musicFolder, $"{songName}.fsb.xen");
                    var datPath = Path.Combine(musicFolder, $"{songName}.dat.xen");
                    var songPath = Path.Combine(songsFolder, $"{songName}_song.pak.xen");
                    if (File.Exists(fsbPath))
                    {
                        File.Delete(fsbPath);
                    }
                    if (File.Exists(datPath))
                    {
                        File.Delete(datPath);
                    }
                    if (File.Exists(songPath))
                    {
                        File.Delete(songPath);
                    }
                }

            }
            songArrayIndeces.Sort();
            dlSonglistIndeces.Sort();
            dlSongPropsIndeces.Sort();
            for (int i = songArrayIndeces.Count - 1; i >= 0; i--)
            {
                SongArray.Items.RemoveAt(songArrayIndeces[i]);
            }
            for (int i = dlSonglistIndeces.Count - 1; i >= 0; i--)
            {
                DlSongList.Items.RemoveAt(dlSonglistIndeces[i]);
            }
            for (int i = dlSongPropsIndeces.Count - 1; i >= 0; i--)
            {
                DlSongListProps.Items.RemoveAt(dlSongPropsIndeces[i]);
            }
            byte[] songlistBytes = CompileQbFromDict(SongListEntries, songlistRef, GAME_GH3, CONSOLE_PC);
            Songlist.OverwriteData(songlistBytes);

            byte[] dlSonglistBytes = CompileQbFromDict(DownloadQbEntries, downloadRef, GAME_GH3, CONSOLE_PC);
            DownloadQb.OverwriteData(dlSonglistBytes);

            var (pakData, pabData) = Compiler.CompilePakFromDictionary(QbPak);
            OverwriteGh3Pak(pakData, pabData!, Game);
            MasterList.Clear();
            songList.Items.Clear();
        }
        private void SelectAll()
        {
            for (int i = 0; i < songList.Items.Count; i++)
            {
                songList.SetItemChecked(i, true);
            }
        }
        private void SelectNone()
        {
            for (int i = 0; i < songList.Items.Count; i++)
            {
                songList.SetItemChecked(i, false);
            }
        }
        private void loadSetlist_Click(object sender, EventArgs e)
        {
            if (gh3Radio.Checked)
            {
                Game = GAME_GH3;
            }
            else
            {
                Game = GAME_GHA;
            }
            LoadSetlist();
        }

        private void deleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSongs();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            // Create a modal window for the preferences panel
            var prefWindow = new ProgramSettings(TabType.SonglistManager);
            prefWindow.ShowDialog();
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void selectNoneButton_Click(object sender, EventArgs e)
        {
            SelectNone();
        }
    }
}
