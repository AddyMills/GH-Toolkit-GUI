namespace GH_Toolkit_GUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            var inputFile = "";
            if (args.Length > 0)
            { 
                inputFile = args[0];
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MasterForm(inputFile));
        }
    }
}