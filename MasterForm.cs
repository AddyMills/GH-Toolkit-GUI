namespace GH_Toolkit_GUI
{
    public partial class MasterForm : Form
    {
        public MasterForm(string inputFile = "")
        {
            InitializeComponent();
            if (inputFile != "")
            {
                ReadInputFile(inputFile);
            }
        }

        private void ReadInputFile(string inputFile)
        {
            if (Path.GetExtension(inputFile.ToLower()) == ".ghproj")
            {
                CompileSong compileSongForm = new CompileSong(inputFile);
                compileSongForm.ShowDialog(); // Use Show() if you don't want it to be modal
            }

        }

        private void OpenCompileSongForm()
        {
            CompileSong compileSongForm = new CompileSong();
            compileSongForm.ShowDialog(); // Use Show() if you don't want it to be modal
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenCompileSongForm();
        }
    }
}
