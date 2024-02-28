using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace GH_Toolkit_Core.Registry
{
    internal class RegistryLookup
    {
        public static string GetRegistryValue(string key, string value)
        {
            string result = string.Empty;
            try
            {
                using (RegistryKey registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key))
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
