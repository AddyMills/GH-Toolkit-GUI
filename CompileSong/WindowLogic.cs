using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GH_Toolkit_GUI
{
    public partial class CompileSong
    {
        private const int SC_CLOSE = 0xF060;
        private const int MF_GRAYED = 0x1;
        private const int MF_ENABLED = 0x00000000;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        private void DisableCloseButton()
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            EnableMenuItem(hMenu, SC_CLOSE, MF_GRAYED);
        }

        private void EnableCloseButton()
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            EnableMenuItem(hMenu, SC_CLOSE, MF_ENABLED);
        }
    }
}
