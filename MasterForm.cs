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
                OpenCompileSongForm(inputFile);
            }

        }

        private void OpenCompileSongForm(string inputFile = "")
        {
            CompileSong compileSongForm = new CompileSong(inputFile);
            compileSongForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenCompileSongForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
