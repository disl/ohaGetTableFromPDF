using System.Data;
using System.Text;
using System.Windows.Forms;
using Tabula;
using Tabula.Detectors;
using Tabula.Extractors;
using UglyToad.PdfPig;
using static ohaGetTableFromPDF.Form1;

namespace ohaGetTableFromPDF
{
    public partial class frmTabula : Form
    {     
        public frmTabula()
        {
            InitializeComponent();
        }

        private void frmTabula_Load(object sender, EventArgs e)
        {
            

        }

        private void InitTable(DataTable data_table)
        {
            DataColumn dataColumn = new DataColumn();
            dataColumn.DataType = typeof(string);
            data_table.Columns.Add(dataColumn);
        }

        private void readTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open_file = new OpenFileDialog();
                open_file.Filter = "PDF-files (*.pdf)|*.pdf";
                if (open_file.ShowDialog() == DialogResult.OK)
                {
                    tableLayoutPanel1.Controls.Clear();

                    using (PdfDocument _document = PdfDocument.Open(open_file.FileName, new ParsingOptions() { ClipPaths = true }))
                    {
                        int row_no=0;
                        var _pages = _document.GetPages().ToList();
                        for (int i = 1; i <= _pages.Count; i++)
                        {
                            var page = ObjectExtractor.Extract(_document, i);
                            // detect canditate table zones
                            SimpleNurminenDetectionAlgorithm detector = new SimpleNurminenDetectionAlgorithm();
                            var regions = detector.Detect(page);
                            for (int r = 0; r<regions.Count; r++)
                            {
                                IExtractionAlgorithm ea = new BasicExtractionAlgorithm(); //SpreadsheetExtractionAlgorithm(); //BasicExtractionAlgorithm();
                                var tables = ea.Extract(page.GetArea(regions[r].BoundingBox)); // take first candidate area
                                for (int t = 0; t<tables.Count; t++)
                                {
                                    var table_name = "Table_" + i.ToString() + "_" + r.ToString() + "_" + t;
                                    DataTable _dataTable = new DataTable(table_name);
                                    InitTable(_dataTable);

                                    StringBuilder sb = new StringBuilder();
                                    var table = tables[t];
                                    var rows = table.Rows;
                                    var csv_writer = new Tabula.Writers.CSVWriter(";");

                                    using (var stream = new MemoryStream())
                                    using (var sw = new StreamWriter(stream) { AutoFlush = true })
                                    {
                                        csv_writer.Write(sb, table);
                                        var output_str = sb.ToString();
                                        var output_arr = output_str.Split('\n');
                                        foreach (var row in output_arr)
                                        {
                                            _dataTable.Rows.Add(row);
                                        }
                                        DataGridView dgv = new DataGridView();
                                        dgv.Dock = DockStyle.Fill;
                                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                        dgv.DataSource= _dataTable;
                                        dgv.ScrollBars = ScrollBars.Both;
                                        dgv.ReadOnly = true;
                                        dgv.AllowUserToAddRows = false;
                                        dgv.AllowUserToDeleteRows = false;
                                        
                                        tableLayoutPanel1.Controls.Add(dgv,0,row_no);
                                        row_no++;
                                    }
                                }
                            }
                        }
                    }

                    

                    //var output_str = sb.ToString();

                    //var file_name = Path.Combine(Path.GetTempPath(), "pdf_to_table_output.csv");
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(file_name))
                    //{
                    //    file.WriteLine(sb.ToString());
                    //}

                    //if (File.Exists(file_name))
                    //    File.Delete(file_name);
                    //System.Text.Encoding encoding = Encoding.UTF8;
                    //File.WriteAllText(file_name, output_str, encoding);
                    //Process.Start(new ProcessStartInfo
                    //{
                    //    Arguments=file_name,
                    //    FileName = "notepad.exe",
                    //    UseShellExecute = true
                    //});
                    //Process.Start(new ProcessStartInfo
                    //{
                    //    FileName=Path.GetDirectoryName(file_name),
                    //    UseShellExecute = true
                    //});
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(enumShowMessageMode.Break, ex.Message);
            }
            finally { Cursor = Cursors.Default; }
        }

        public DialogResult ShowMessageBox(
                   enumShowMessageMode Mode,
                   string Message = null,
                   MessageBoxDefaultButton DefaultButton = MessageBoxDefaultButton.Button2)
        {
            string _message = null;
            MessageBoxIcon _icon = MessageBoxIcon.Exclamation;
            MessageBoxButtons _buttons = MessageBoxButtons.OK;
            MessageBoxDefaultButton _default_button = MessageBoxDefaultButton.Button1;
            string _caption = Properties.Resources.msgCaptionInformation;

            switch (Mode)
            {
                case enumShowMessageMode.Success:
                    _message = Properties.Resources.msg1235_01;
                    break;
                case enumShowMessageMode.Break:
                    _message = Properties.Resources.msg1175_01;
                    break;
                case enumShowMessageMode.OKCancel:
                    _caption = Properties.Resources.msgQuestion;
                    _message = Properties.Resources.msg1215_01;
                    _icon = MessageBoxIcon.Question;
                    _buttons = MessageBoxButtons.OKCancel;
                    _default_button = DefaultButton;
                    break;
                case enumShowMessageMode.YesNo:
                    _caption = Properties.Resources.msgQuestion;
                    _message = Properties.Resources.msg1215_01;
                    _icon = MessageBoxIcon.Question;
                    _buttons = MessageBoxButtons.YesNo;
                    _default_button = DefaultButton;
                    break;
                case enumShowMessageMode.YesNoPlus:
                    _caption = Properties.Resources.msgQuestion;
                    _message = Properties.Resources.msg1216_01;
                    _icon = MessageBoxIcon.Question;
                    _buttons = MessageBoxButtons.YesNo;
                    _default_button = DefaultButton;
                    break;
                case enumShowMessageMode.YesNoCancel:
                    _caption = Properties.Resources.msgQuestion;
                    _message = Properties.Resources.msg1215_01;
                    _icon = MessageBoxIcon.Question;
                    _buttons = MessageBoxButtons.YesNoCancel;
                    _default_button = DefaultButton;
                    break;
                case enumShowMessageMode.Info:
                    _message = null;
                    _icon = MessageBoxIcon.Information;
                    _buttons = MessageBoxButtons.OK;
                    _default_button = DefaultButton;
                    break;
                case enumShowMessageMode.Done:
                    _message = Properties.Resources.msg1210_01;
                    break;
            }

            if (!string.IsNullOrEmpty(Message))
            {
                _message = Message;
            }

            return MessageBox.Show(_message, _caption, _buttons, _icon, _default_button);
        }

        
    }
}
