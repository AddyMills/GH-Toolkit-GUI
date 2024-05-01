using GH_Toolkit_Core.PAK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static GH_Toolkit_Core.QB.QBConstants;
using static GH_Toolkit_Core.Methods.CreateForGame;

namespace GH_Toolkit_GUI
{
    public class PreCompileChecks
    {
        public static string ExeLocation = Assembly.GetExecutingAssembly().Location;
        public static string ExeDirectory = Path.GetDirectoryName(ExeLocation);
        public static string ResourcePath = Path.Combine(ExeDirectory, "Resources");
        public static string DATAPath = "DATA";
        public static string PAKPath = Path.Combine(DATAPath, "PAK");
        public static string MUSICPath = Path.Combine(DATAPath, "MUSIC");
        public static string SONGSPath = Path.Combine(DATAPath, "SONGS");

        private static UserPreferences Pref = UserPreferences.Default;
        public static void Gh3PcCheck(string game)
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
                        string exeName = game == GAME_GH3 ? "GH3" : "Guitar Hero Aerosmith";
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
                ReplaceGh3PakFiles(game, "PC");

                MessageBox.Show($"A backup of {regFolder}'s QB file has been created.\nIt can be copied back to your GH folder at any time in the settings menu.", "Backup Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private static void UpdateGh3FilePreference(string pakPath, string pabPath, string folderPath)
        {
            Pref.Gh3QbPak = pakPath;
            Pref.Gh3QbPab = pabPath;
            Pref.Gh3FolderPath = folderPath;
            Pref.Save();
        }
        private static void UpdateGhaFilePreference(string pakPath, string pabPath, string folderPath)
        {
            Pref.GhaQbPak = pakPath;
            Pref.GhaQbPab = pabPath;
            Pref.GhaFolderPath = folderPath;
            Pref.Save();
        }
        public static string GetGh3PakFile(string game)
        {
            if (game == "GH3")
            {
                return UserPreferences.Default.Gh3QbPak;
            }
            else
            {
                return UserPreferences.Default.GhaQbPak;
            }
        }
        public static string GetGh3Folder(string game)
        {
            if (game == "GH3")
            {
                return UserPreferences.Default.Gh3FolderPath;
            }
            else
            {
                return UserPreferences.Default.GhaFolderPath;
            }
        }
        public static void OverwriteGh3Pak(byte[] pakData, byte[] pabData, string game)
        {
            OverwriteSplitPak(GetGh3PakFile(game), pakData, pabData, DOTXEN);
        }
        public static void ReplaceGh3PakFiles(string game, string platform)
        {
            string qbPakLocation = GetGh3PakFile(game);
            // Might make this use the backup instead in the future...
            string replaceLocation = Path.Combine(ExeDirectory, "Replacements", platform, game, "QB");

            if (Directory.Exists(replaceLocation))
            {
                var pakCompiler = new PAK.PakCompiler(game, platform, split: true);
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
                OverwriteGh3Pak(pakData, pabData!, game);
            }
        }
        public static void OnyxCheck()
        {
            string onyxPath = Pref.OnyxCliPath;
            var onyxExe = Path.Combine(onyxPath, "onyx.exe");
            bool showWarning = true;
            while (!Directory.Exists(onyxPath) || !File.Exists(onyxExe))
            {
                if (showWarning)
                {
                    MessageBox.Show("Onyx has not been found. Please select your Onyx CLI folder now.", "Folder Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showWarning = false;
                }
                var tempOnyxFolder = AskForGamePath();
                var tempOnyxExe = Path.Combine(tempOnyxFolder, "onyx.exe");
                if (!File.Exists(tempOnyxExe))
                {
                    MessageBox.Show("Onyx.exe was not found in the selected folder. Please select the correct folder.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Pref.OnyxCliPath = tempOnyxFolder;
                    Pref.Save();
                    onyxPath = tempOnyxFolder;
                    onyxExe = tempOnyxExe;
                }
            }
        }
        public static string AskForGamePath()
        {
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
    }
}
