using System.Data;
using System.Diagnostics;
using System.Text;
using Tabula;
using Tabula.Extractors;
using UglyToad.PdfPig;

namespace ohaGetTableFromPDF
{
    public partial class frmTabula : Form
    {
        public enum enumShowMessageMode
        {
            Success, Break, OKCancel, YesNo, YesNoCancel, Done, Info,
            YesNoPlus
        }

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
            Read(null, null);  // new List<string> { "851", "854" }, 11);
        }

        private void Read(List<string>? InputPrefixArr, int? InputLenght)
        {
            try
            {
                OpenFileDialog open_file = new OpenFileDialog();
                open_file.Filter = "PDF-files (*.pdf)|*.pdf";
                if (open_file.ShowDialog() == DialogResult.OK)
                {
                    tableLayoutPanel1.Controls.Clear();

                    List<string> output_arr_all = new List<string>();
                    using (PdfDocument _document = PdfDocument.Open(open_file.FileName, new ParsingOptions() { ClipPaths = true }))
                    {
                        int row_no = 0;
                        var _pages = _document.GetPages().ToList();

                        for (int i = 1; i <= _pages.Count; i++)
                        {
                            var page = ObjectExtractor.Extract(_document, i);
                            var rulings = page.GetRulings().ToList();
                            var detector = new Tabula.Detectors.SimpleNurminenDetectionAlgorithm();
                            var regions = detector.Detect(page);
                            for (int r = 0; r<regions.Count; r++)
                            {
                                var ea = new BasicExtractionAlgorithm();
                                var area = page.GetArea(regions[r].BoundingBox);
                                IReadOnlyList<float> _verticalRulingPositions = null;
                                var tables = ea.Extract(area); // take first candidate area
                                for (int t = 0; t<tables.Count; t++)
                                {
                                    var table_name = "Table_" + i.ToString() + "_" + r.ToString() + "_" + t;
                                    DataTable _dataTable = new DataTable(table_name);
                                    InitTable(_dataTable);

                                    StringBuilder sb = new StringBuilder();
                                    var table = tables[t];

                                    var rows = table.Rows;

                                    var csv_writer = new Tabula.Writers.CSVWriter();
                                    //var csv_writer = new Tabula.Writers.JSONWriter();

                                    using (var stream = new MemoryStream())
                                    using (var sw = new StreamWriter(stream) { AutoFlush = true })
                                    {
                                        csv_writer.Write(sb, table);
                                        var output_str = sb.ToString();
                                        var _output_arr = output_str.Split('\n');

                                        output_arr_all.AddRange(_output_arr);

                                        foreach (var row in _output_arr)
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

                                        tableLayoutPanel1.Controls.Add(dgv, 0, row_no);
                                        row_no++;
                                    }
                                }
                            }
                        }
                    }

                    string? header_first_word = null;
                    string[] header_arr = null;
                    List<string[]> output_arr = new List<string[]>();
                    string[] row_arr = null;
                    string[]? tmp_row = null;
                    bool is_new_row = false;
                    bool is_header = false;
                    string[]? tmp_header_arr = null;
                    foreach (string row in output_arr_all)
                    {
                        //var _row = row.Replace(","," ");
                        row_arr = row.Split(",");
                        var row_arr_1 = row_arr[0];
                        //if (string.IsNullOrEmpty(row_arr_1))
                        //    continue;
                        if (string.IsNullOrEmpty(header_first_word))
                        {
                            header_first_word = row_arr_1;
                            header_arr = row_arr;
                            is_header=true;
                        }
                        else
                        {
                            if (is_header)
                            {
                                if (tmp_row != null && tmp_header_arr!= null && tmp_row[0] == tmp_header_arr[0])
                                    continue;
                                //ConcatWithPreviouse(header_arr, row_arr);
                                is_header=false;
                            }
                            if (row_arr_1==header_first_word)
                                continue;  // Ignore header
                            else
                            {
                                is_new_row = IsNewRow(row_arr_1, InputPrefixArr, InputLenght);
                                if (is_new_row)
                                {
                                    tmp_row = row_arr;
                                    output_arr.Add(row_arr);
                                }
                                else
                                {
                                    int last_ind = output_arr.LastIndexOf(tmp_row);
                                    if (last_ind != -1)
                                        ConcatWithPreviouse(output_arr[last_ind], row_arr);
                                }
                            }
                        }
                    }

                    string output_line = "";
                    List<string> output_arr_str = new List<string>();

                    // Header
                    output_line = string.Join(";", header_arr);
                    output_arr_str.Add(output_line.Trim().Replace("\n", ""));

                    // Rows
                    foreach (var row in output_arr)
                    {
                        output_line = string.Join(";", row);
                        output_arr_str.Add(output_line.Trim().Replace("\n", ""));
                    }

                    // var output_str = sb.ToString();

                    var file_name = Path.Combine(Path.GetTempPath(), "pdf_to_table_output.csv");
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(file_name))
                    //{
                    //    file.WriteLine(sb.ToString());
                    //}

                    if (File.Exists(file_name))
                        File.Delete(file_name);
                    System.Text.Encoding encoding = Encoding.UTF8;
                    File.WriteAllLines(file_name, output_arr_str, encoding);
                    Process.Start(new ProcessStartInfo
                    {
                        Arguments=file_name,
                        FileName = "notepad.exe",
                        UseShellExecute = true
                    });
                    Process.Start(new ProcessStartInfo
                    {
                        FileName=Path.GetDirectoryName(file_name),
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(enumShowMessageMode.Break, ex.Message);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void ConcatWithPreviouse(string[] header_arr, string[] row_arr)
        {
            int bound = row_arr.Length <header_arr.Length ? row_arr.Length : header_arr.Length;

            for (int i = 0; i < bound; i++)
            {
                var row_arr_item = row_arr[i];

                header_arr[i] += !string.IsNullOrEmpty(row_arr_item) ? " " + row_arr_item : "";
            }
        }

        private bool IsNewRow(string row_arr_1, List<string>? inputPrefixArr, int? inputLenght)
        {
            bool isInputLenOK = inputLenght != null && row_arr_1.Length == inputLenght;
            if (!string.IsNullOrEmpty(row_arr_1)) return true;
            if (inputPrefixArr == null || inputPrefixArr.Count == 0) return true;

            if (isInputLenOK)
            {
                foreach (string inputPrefixItem in inputPrefixArr)
                {
                    if (row_arr_1.Contains(inputPrefixItem))
                        return true;
                }
            }
            else
            {
                if (inputLenght == null)
                {
                    foreach (string inputPrefixItem in inputPrefixArr)
                    {
                        if (row_arr_1.Contains(inputPrefixItem))
                            return true;
                    }
                }
            }
            return false;
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
