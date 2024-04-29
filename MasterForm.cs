using System.Text;

namespace GH_Toolkit_GUI
{    public partial class MasterForm : Form
    {
        public MasterForm(string inputFile = "")
        {
            InitializeComponent();
            Console.SetOut(new TextBoxStreamWriter(consoleOutput));
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
            else if (Path.GetExtension(inputFile.ToLower()) == ".sgh")
            {
                ImportSGHForm(inputFile);
            }

        }

        private void OpenCompileSongForm(string inputFile = "")
        {
            CompileSong compileSongForm = new CompileSong(inputFile);
            compileSongForm.Show();
        }

        private void ImportSGHForm(string inputFile = "")
        {
            ImportSGH importSGHForm = new ImportSGH(inputFile);
            importSGHForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenCompileSongForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImportSGHForm();
        }
    }
    public class TextBoxStreamWriter : TextWriter
    {
        private TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            if (_output.InvokeRequired)
            {
                _output.Invoke(new MethodInvoker(delegate { _output.AppendText(value.ToString()); }));
            }
            else
            {
                _output.AppendText(value.ToString());
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
