using System.Diagnostics;

namespace ohaERP_EDI_Server.Invoices
{
    public class cOCRmyPDF
    {
        string m_file_name;
        string m_ocrmypdf_exe_path = "C:\\Python311\\Scripts";  // "C:\\Users\\DSluzki\\AppData\\Local\\Packages\\PythonSoftwareFoundation.Python.3.11_qbz5n2kfra8p0\\LocalCache\\local-packages\\Python311\\Scripts";  // "C:\\Python311\\Scripts";
        string input_file_name = "input_ocr.pdf";
        string output_file_name = "output_ocr.pdf";

        public cOCRmyPDF(string file_name, string ocrmypdf_exe_path)
        {
            m_file_name = file_name;
            m_ocrmypdf_exe_path = ocrmypdf_exe_path;
        }

        public string Start_OCRmyPDF()
        {
            try
            {
                var input_file = System.IO.Path.Combine(m_ocrmypdf_exe_path, input_file_name);
                if (File.Exists(input_file)) File.Delete(input_file);

                File.Copy(m_file_name, input_file, true);

                var output_file = System.IO.Path.Combine(m_ocrmypdf_exe_path, output_file_name);
                if (File.Exists(output_file)) File.Delete(output_file);

                string command =
                    m_ocrmypdf_exe_path + "\\ocrmypdf " +
                    m_ocrmypdf_exe_path + "\\" + input_file_name + " " +
                    m_ocrmypdf_exe_path + "\\" + output_file_name;

                ExecCommand(command);

                return output_file;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void ExecCommand(string command)  //string input_file, string output_file)
        {
            using (var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"cmd.exe",  // @"cmd.exe",
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    Arguments = "/C " + command   // "/C" - wichtig fuer Arguments
                }
            })
            {
                proc.Start();

                if (!proc.WaitForExit(30000))
                    throw new TimeoutException("Process failed to complete in time");
            }
        }

    }
}
