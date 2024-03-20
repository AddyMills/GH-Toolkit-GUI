


using System.Diagnostics;

namespace GH_Toolkit_GUI
{
    public partial class CompileSong
    {
        public static void HandleException(Exception ex, string errorInfo = "Error")
        {
            var st = new StackTrace(ex, true);
            StackFrame targetFrame = null;

            foreach (var frame in st.GetFrames())
            {
                var method = frame.GetMethod();
                if (method != null &&
                    !method.DeclaringType.Assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase) &&
                    !method.DeclaringType.Assembly.FullName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase))
                {
                    targetFrame = frame;
                    break;
                }
            }

            if (targetFrame != null)
            {
                var fileName = Path.GetFileName(targetFrame.GetFileName()) ?? "Not available";
                var methodName = targetFrame.GetMethod().Name;
                var line = targetFrame.GetFileLineNumber();
                var column = targetFrame.GetFileColumnNumber();

                string errorMessage = $"Exception: {ex.Message}\nMethod: {methodName}\nFile: {fileName}\nLine: {line}, Column: {column}";
                MessageBox.Show(errorMessage, errorInfo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Fallback message if no suitable frame is found
                MessageBox.Show($"Exception: {ex.Message}", "errorMessage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }
    }
}