using Microsoft.Win32;

namespace GH_Toolkit_GUI
{
    internal class RegistryLookup
    {
        public string GetRegistryValue(string key, string value)
        {
            string result = string.Empty;
            try
            {
                using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key))
                {
                    if (registryKey != null)
                    {
                        result = registryKey.GetValue(value).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
