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
using static GH_Toolkit_Core.QB.QBConstants;
using GH_Toolkit_Core.Methods;
using System.Reflection;
using static GH_Toolkit_GUI.PreCompileChecks;
using static GH_Toolkit_Exceptions.Exceptions;
using GH_Toolkit_Core.PS360;
using GH_Toolkit_GUI.Properties;

namespace GH_Toolkit_GUI
{
    public partial class ImportSGH : Form
    {
        private readonly string sghFileFilter = "SGH Files (*.sgh)|*.sgh";
        private Dictionary<string, QBStruct.QBStructData> MasterList = new Dictionary<string, QBStruct.QBStructData>();
        private string SghPath = "";
        private string SghFolder = "";
        private static UserPreferences Pref = UserPreferences.Default;
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
                else
                {
                    throw new NotImplementedException("SGH file is not encrypted.");
                }
                File.Move(songs, songsQb, true);
                var songsQbList = QB.DecompileQbFromFile(songsQb);
                File.Delete(songsQb);
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
                HandleException(e);
            }
        }

        private void ConvertSongs()
        {
            if (MasterList.Count == 0)
            {
                MessageBox.Show("No songs loaded!\n\nPlease import an SGH file first.");
                return;
            }
            var toImport = new List<QBStruct.QBStructData>();
            string compilePath;
            string game = GAME_GH3;
            try
            {
                OnyxCheck();
                compilePath = SghFolder;
                Directory.CreateDirectory(compilePath);
                Console.WriteLine("Extracting all songs from SGH file...");
                GHTCP.ExtractSghZip(SghPath, compilePath, out bool isEncrypted);

                DeleteTempFiles(compilePath);

                foreach (string song in songList.CheckedItems)
                {
                    string songName = song.Split(' ')[0];
                    toImport.Add(MasterList[songName]);
                }

                PrepareFileNames(compilePath);

                var first = toImport[0];
                string[] checksumStrings = [(string)first["checksum"], (string)first["Title"], (string)first["Artist"], "123456"];
                var checksum = CreateForGame.MakeConsoleChecksum(checksumStrings);
                var platform = ConsoleSelect.Text;
                if (platform == CONSOLE_PC)
                {

                }
                else
                {
                    string sghName = $"{SghFolder}_{platform}";
                    string sghFolderName = CreateForGame.ReplaceNonAlphanumeric(Path.GetFileName(SghFolder));
                    CreateForGame.CreateConsoleDownloadFiles(checksum, GAME_GH3, platform, compilePath, ResourcePath, toImport);
                    string[] onyxArgs;
                    if (platform == CONSOLE_PS3)
                    {
                        string gameFiles = Path.Combine(compilePath, "USRDIR", sghFolderName.ToUpper());
                        Directory.CreateDirectory(gameFiles);
                        string ps3Resources = Path.Combine(ResourcePath, "PS3");
                        string currGameResources = Path.Combine(ps3Resources, game);
                        string vramFile = Path.Combine(ps3Resources, $"VRAM_{game}");
                        if (!Directory.Exists(ps3Resources) || !Directory.Exists(currGameResources))
                        {
                            throw new Exception("Cannot find PS3 Resource folder.\n\nThis should be included with your toolkit.\nPlease re-download the toolkit.");
                        }

                        string contentID = FileCreation.GetPs3Key(GAME_GH3) + $"-{checksum.ToString().PadLeft(16, '0')}";
                        foreach(var file in Directory.GetFiles(compilePath))
                        {
                            File.Move(file, Path.Combine(gameFiles, $"{Path.GetFileName(file)}.PS3".ToUpper()), true);
                            string fileExtension = Path.GetExtension(file);
                            string fileNoExt = Path.GetFileNameWithoutExtension(file).ToLower();
                            bool localeFile = fileNoExt.Contains("_text") && !fileNoExt.EndsWith("_text");
                            if (fileExtension.ToLower() == ".pak" && !localeFile)
                            {
                                File.Copy(vramFile, Path.Combine(gameFiles, $"{fileNoExt}_VRAM.PAK.PS3").ToUpper(), true);
                            }
                        }
                        foreach (string file in Directory.GetFiles(currGameResources))
                        {
                            File.Copy(file, Path.Combine(compilePath, Path.GetFileName(file)), true);
                        }
                        onyxArgs = ["pkg", contentID, compilePath, "--to", sghName + ".PKG"];
                    }
                    else
                    {
                        onyxArgs = ["stfs", compilePath, "--to", sghName];
                        AddExtension(compilePath, ".xen");
                    }
                    Console.WriteLine("Creating package file...");
                    CreateForGame.CompileWithOnyx(Pref.OnyxCliPath, onyxArgs);
                }
            }
                
            catch(Exception e)
            {
                HandleException(e);
            }
            finally
            {

            }
        }
        private static void DeleteTempFiles(string compilePath)
        {
            string songsInfo = Path.Combine(compilePath, "songs.info");
            string setlistInfo = Path.Combine(compilePath, "setlist.info");
            if (File.Exists(songsInfo))
            {
                File.Delete(songsInfo);
            }
            if (File.Exists(setlistInfo))
            {
                File.Delete(setlistInfo);
            }
        }
        private static void PrepareFileNames(string compilePath)
        {
            // Go through each file in compilePath, check for the extension .xen and remove it
            foreach (string file in Directory.GetFiles(compilePath))
            {
                if (Path.GetExtension(file) == ".xen")
                {
                    File.Move(file, Path.ChangeExtension(file, null), true);
                }
            }
        }
        private static void AddExtension(string compilePath, string extension)
        {
            foreach (string file in Directory.GetFiles(compilePath))
            {
                File.Move(file, Path.ChangeExtension(file, extension), true);
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
