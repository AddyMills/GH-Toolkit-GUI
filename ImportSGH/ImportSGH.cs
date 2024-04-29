using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GH_Toolkit_Core.QB;
using GH_Toolkit_Core.Methods;

namespace GH_Toolkit_GUI
{
    public partial class ImportSGH : Form
    {
        private readonly string sghFileFilter = "SGH Files (*.sgh)|*.sgh";
        private Dictionary<string, QBStruct.QBStructData> MasterList = new Dictionary<string, QBStruct.QBStructData>();
        private string SghPath = "";
        private string SghFolder = "";
        public ImportSGH(string sghFile = "")
        {
            InitializeComponent();
            ConsoleSelect.SelectedIndex = 0;
            if (sghFile != "")
            {
                SghPath = sghFile;
                LoadSGH();
            }
        }

        private void LoadSGH()
        {
            MasterList.Clear();
            songList.Items.Clear();
            try
            {
                SghFolder = Path.Combine(Path.GetDirectoryName(SghPath), Path.GetFileNameWithoutExtension(SghPath));
                GHTCP.ExtractSongsFromSgh(SghPath, SghFolder, out bool isEncrypted);
                var songs = Path.Combine(SghFolder, "songs.info");
                var songsQb = Path.Combine(SghFolder, "songs.qb");
                if (isEncrypted)
                {
                    var sghSongData = File.ReadAllBytes(songs);
                    var decryptedSong = GHTCP.DecryptSongs(sghSongData);
                    File.WriteAllBytes(songs, decryptedSong);
                }
                File.Move(songs, songsQb, true);
                var songsQbList = QB.DecompileQbFromFile(songsQb);
                foreach (var song in songsQbList)
                {
                    if (song.Data is QBStruct.QBStructData songData)
                    {
                        var songName = songData["name"] as string;
                        var songTitle = songData["title"] as string;
                        var songArtist = songData["artist"] as string;
                        songList.Items.Add($"{songName} ({songTitle} - {songArtist})", true);
                        MasterList.Add(songName, songData);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading SGH file: {e.Message}");
            }
        }

        private void ConvertSongs()
        {
            try
            {

            }
            catch
            {

            }
            finally
            {

            }
        }

        private void importSgh_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                string sghPath = "";
                dialog.Filter = sghFileFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SghPath = dialog.FileName;
                }
                else
                {
                    return;
                }
                LoadSGH();
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            ConvertSongs();
        }
    }
}
