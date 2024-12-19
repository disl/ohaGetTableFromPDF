using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Ghostscript.NET.Rasterizer;
using Microsoft.Extensions.Logging;
using ohaERP_EDI_Server.Invoices;
using System.Data;
using Tesseract;
using TextOCR_PDF;

namespace ohaGetTableFromPDF
{
    public partial class Form1 : Form
    {
        private string? m_start_pdf_path;
        string m_gswin_path = "C:\\Python311\\Scripts";
        WordsArray? m_match_rect_NumberLabel_obj = null;
        WordsArray? m_match_rect_SumLabel_obj = null;
        private const Tesseract.PageIteratorLevel pageIteratorLevel = Tesseract.PageIteratorLevel.Word;
        private const string m_c_output_png_file = "output_png.png";
        string m_tessdata_dir;
        List<WordsArray> WordsArray = new List<WordsArray>();
        List<System.Drawing.Image> ImageArray = new();
        private int m_page_count;
        private int m_supplier_setupsysid;
        private int? m_docu_archive_invoicesysid = null;
        private bool m_is_check_existing_invoices = false;
        private string? m_purchase_email = null;
        private bool m_isFromCOMServer;
        private string m_nation_iso_code;
        private ZUGFeRD_InfoType? m_zUGFeRD_Info = null;
        private string m_companyname1 = string.Empty;
        private int? m_suppliersysid;
        DataTable OrderDetailArrivalSysid_Table = new DataTable();
        DataColumn OrderDetailArrivalSysid_column;
        DataRow OrderDetailArrivalSysid_row;
        string m_file_for_email;
        //string m_file_for_email_template = "OCR_Model_file_for_email_%1.png";
        public enum enumLanguage { deu, ita, unknown };
        enumLanguage m_language_mode = enumLanguage.unknown;

        // TEST ???
        private bool m_test_ok = false;
        private string? m_pdf_text;
        private int m_search_counter;

        public string Model { get; set; }

        private Bitmap m_memoryImage;
        private bool m_is_ocr_dgv_active;
        private ILogger<Form1> m_logger;
        //private InvoiceDescriptor? m_invoiceDescriptor = null;
        private string m_error_message;
        private bool m_adding_new;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            languageToolStripComboBox.Text = null;

            switch (m_language_mode)
            {
                case enumLanguage.deu:
                    languageToolStripComboBox.Text = "deu";
                    break;
                case enumLanguage.ita:
                    languageToolStripComboBox.Text = "ita";
                    break;
                default:
                    languageToolStripComboBox.Text = "deu";
                    break;
            }

            react_by_mouse_clickToolStripComboBox.Text = m_is_check_existing_invoices ? "Nein" : "Ja";
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_page_count = 0;
            m_search_counter = 0;
            DocumentElement textLocationDataTable;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //var supplier_setup_obj = supplier_setupTableAdapter.GetDataBySysid(m_supplier_setupsysid);
                //supplier_setup_notesTextBox.Text = supplier_setup_obj[0].IsnotesNull() ? null : supplier_setup_obj[0].notes;

                m_error_message += !string.IsNullOrEmpty(m_companyname1) ? "Supplier/SupplierID='" + m_companyname1 + "'" : "Suppliersysid=" + m_suppliersysid;

                if (OpenPDFAndCreateRectangles())
                {
                    //if (supplier_setup_obj == null || supplier_setup_obj.Count == 0)
                    //    return;

                    //if (supplier_setup_obj[0].Isocr_modelNull() || string.IsNullOrEmpty(supplier_setup_obj[0].ocr_model))
                    //{
                    //    if (!m_isFromCOMServer)
                    //        ShowMessageBox(enumShowMessageMode.Break, "OCR-Model wurde noch nicht definiert!");
                    //    return;
                    //}

                    //var serializer = new XmlSerializer(typeof(DocumentElement));
                    //using (var stringReader = new System.IO.StringReader(supplier_setup_obj[0].ocr_model))
                    //{
                    //    textLocationDataTable = serializer.Deserialize(stringReader) as DocumentElement;
                    //}

                    //if (textLocationDataTable != null && textLocationDataTable.TextLocation.Count > 0)
                    //{
                    //    dataSet1.TextLocation.Rows.Clear();

                    //    foreach (var item in textLocationDataTable.TextLocation)
                    //    {
                    //        dataSet1.TextLocation.AddTextLocationRow(
                    //            item.X,
                    //            item.Y,
                    //            item.Field.Contains("Value") ? null : item.Text,
                    //            item.Field);
                    //    }

                    //    GetValues();

                    //    string? _invoice_no = null;
                    //    DateTime? _invoice_date = null;
                    //    decimal _invoice_net_amount = 0;

                    //    if (dataSet1.TextLocation.FirstOrDefault(x => x.Field == "NumberValue") != null && !dataSet1.TextLocation.FirstOrDefault(x => x.Field == "NumberValue").IsTextNull())
                    //        _invoice_no = dataSet1.TextLocation.FirstOrDefault(x => x.Field == "NumberValue").Text;
                    //    if (dataSet1.TextLocation.FirstOrDefault(x => x.Field == "DateValue") != null && !dataSet1.TextLocation.FirstOrDefault(x => x.Field == "DateValue").IsTextNull())
                    //        _invoice_date = Convert.ToDateTime(dataSet1.TextLocation.FirstOrDefault(x => x.Field == "DateValue").Text);
                    //    if (dataSet1.TextLocation.FirstOrDefault(x => x.Field == "SumValue") != null && !dataSet1.TextLocation.FirstOrDefault(x => x.Field == "SumValue").IsTextNull())
                    //    {
                    //        // ?????????
                    //        //m_culture = CultureInfo.CreateSpecificCulture("de-DE");

                    //        var _invoice_net_amount_local = Convert.ToDecimal(dataSet1.TextLocation.FirstOrDefault(x => x.Field == "SumValue").Text, m_culture);
                    //        _invoice_net_amount = Convert.ToDecimal(dataSet1.TextLocation.FirstOrDefault(x => x.Field == "SumValue").Text);
                    //        if (_invoice_net_amount_local < _invoice_net_amount)
                    //        {
                    //            _invoice_net_amount = _invoice_net_amount_local;
                    //        }
                    //    }
                    //    //_invoice_net_amount = Convert.ToDecimal(dataSet1.TextLocation.FirstOrDefault(x => x.Field == "SumValue").Text);

                    //    if (!string.IsNullOrEmpty(_invoice_no))
                    //        m_error_message += "; Invoice-No. = " + _invoice_no;
                    //    if (_invoice_date != null)
                    //        m_error_message += "; Invoice-Date = " + ((DateTime)_invoice_date).ToShortDateString();

                    //    AdditionalCheck(_invoice_no, _invoice_date, _invoice_net_amount);
                    //}
                }
            }
            catch (TesseractException ex)
            {
                //g_logger.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                if (!m_isFromCOMServer)
                    MessageBox.Show(m_error_message + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                else
                    //gErrors.SendErrorPerEmail(ToString(), ex, ohaERP_Library.Properties.Resources.msgCaptionError + " in: " + ToString(), m_start_pdf_path, m_error_message);
                    ShowMessageBox(enumShowMessageMode.Break, ex.Message);
            }
            catch (Exception ex)
            {
                //g_logger.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                if (!m_isFromCOMServer)
                    MessageBox.Show(m_error_message + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                else
                    //gErrors.SendErrorPerEmail(ToString(), ex, ohaERP_Library.Properties.Resources.msgCaptionError + " in: " + ToString(), m_start_pdf_path, m_error_message);
                    ShowMessageBox(enumShowMessageMode.Break, ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool OpenPDFAndCreateRectangles()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(m_start_pdf_path) && File.Exists(m_start_pdf_path))
                {
                    return ForOpenPDFAndCreateRectangles(m_start_pdf_path);
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        return ForOpenPDFAndCreateRectangles(openFileDialog.FileName);
                    }
                }
                return false;
            }
            finally { Cursor = Cursors.Default; }
        }

        private bool ForOpenPDFAndCreateRectangles(string FileName)
        {
            Cursor = Cursors.WaitCursor;

            imageBox1.Image = null;
            //dataSet1.TextLocation.Rows.Clear();

            cOCRmyPDF m_ocr = new cOCRmyPDF(FileName, m_gswin_path);
            var new_pdf = m_ocr.Start_OCRmyPDF();
            if (!string.IsNullOrEmpty(new_pdf) && File.Exists(new_pdf))
            {
                Thread.Sleep(3000);
                FileName = new_pdf;
            }

            var image_path = PDFToPNG(FileName);

            return DrawRectangles(image_path);
        }

        bool DrawRectangles(string image_path)
        {
            try
            {
                if (string.IsNullOrEmpty(image_path) || !File.Exists(image_path))
                {
                    var tmp_path = string.IsNullOrEmpty(image_path) ? "??????" : image_path;
                    throw new Exception("File  '" + tmp_path + "' not exists!" + Environment.NewLine + m_error_message);
                }

                if (string.IsNullOrEmpty(tesseract_pathToolStripTextBox.Text) || !Directory.Exists(tesseract_pathToolStripTextBox.Text))
                {
                    if (!Directory.Exists("C:\\Tesseract"))
                    {
                        if (!m_isFromCOMServer)
                            ShowMessageBox(enumShowMessageMode.Break, "Tesseract-Pfad (z.B. 'C:\\Tesseract') existiert nicht! Bitte installieren.");
                        throw new Exception("Tesseract-Pfad existiert nicht! Bitte installieren." + Environment.NewLine + m_error_message);
                    }
                    tesseract_pathToolStripTextBox.Text = "C:\\Tesseract";
                }

                TesseractEnviornment.CustomSearchPath = tesseract_pathToolStripTextBox.Text;

                Tesseract.Pix image_pix = Tesseract.Pix.LoadFromFile(image_path);
                var train_data_deu = tesseract_pathToolStripTextBox.Text; //System.IO.Path.Combine(GetTmpPath(), "tessdata");

                var engine_mode = EngineMode.Default;
                if (m_supplier_setupsysid == 58      // Emil Stahl
                    || m_supplier_setupsysid == 16   // Otto Roth
                    )
                {
                    engine_mode = EngineMode.TesseractOnly;
                }

                using (var engine = new TesseractEngine(train_data_deu, languageToolStripComboBox.Text, engine_mode))
                {
                    //engine.SetVariable("preserve_interword_spaces", "0");

                    using (var page = engine.Process(image_pix))
                    {
                        using (Mat image_cv = new Mat(image_path))
                        {
                            DrawRects(image_pix, page, image_cv);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string error_msg = ex.Message + Environment.NewLine + ex.StackTrace;
                if (ex.InnerException != null)
                    error_msg += Environment.NewLine + ex.InnerException.Message;
                if (!m_isFromCOMServer)
                    MessageBox.Show(error_msg);
                else
                    //gErrors.SendErrorPerEmail(ToString(), ex, ohaERP_Library.Properties.Resources.msgCaptionError + " in: " + ToString(), m_start_pdf_path, m_error_message);
                    ShowMessageBox(enumShowMessageMode.Break, ex.Message);
                //g_logger.Error(error_msg);
                return false;
            }
        }

        private void DrawRects(Tesseract.Pix image_pix, Page page, Mat image_cv)
        {
            using (var iter = page.GetIterator())
            {
                iter.Begin();
                Rect symbolBounds;
                WordsArray = new List<WordsArray>();

                do
                {
                    if (iter.TryGetBoundingBox(pageIteratorLevel, out symbolBounds))
                    {
                        // Draw rectangle
                        Rectangle bounds = new Rectangle(symbolBounds.X1, symbolBounds.Y1, symbolBounds.Width, symbolBounds.Height);
                        CvInvoke.Rectangle(image_cv, bounds, new MCvScalar(0, 255, 0), 1, LineType.FourConnected, 0);
                        // Rectangles text
                        var iter_text = iter.GetText(pageIteratorLevel);
                        // Fill bounds-text-array
                        WordsArray.Add(new WordsArray(bounds, iter_text));
                    }
                } while (iter.Next(pageIteratorLevel));

                imageBox1.Image = image_cv;
            }
        }

        private string PDFToPNG(string inputFile)
        {
            int ignoreLastNPages = 0;

            if (string.IsNullOrEmpty(inputFile) || !System.IO.Path.Exists(inputFile))
            {
                throw new Exception("PDFToPNG: " + "Path '" + inputFile + "' not exists!" + Environment.NewLine + m_error_message);
            }

            ImageArray = new List<System.Drawing.Image>();

            //if (dataSetSupplier.ocr_model_details.Count > 0)
            //{
            //    var ignoreLastNPages_obj = dataSetSupplier.ocr_model_details.FirstOrDefault(x => x.Field == "IgnoreLastNPages");
            //    if (ignoreLastNPages_obj != null)
            //        ignoreLastNPages = ignoreLastNPages_obj.IsTextNull() ? 0 : Convert.ToInt32(ignoreLastNPages_obj.Text);
            //}

            try
            {
                using (var rasterizer = new GhostscriptRasterizer())
                {
                    rasterizer.CustomSwitches.Add("-dNEWPDF=false");  // !!!!!!!!!!!!

                    if (rasterizer == null)
                    {
                        throw new Exception("PDFToPNG: A rasteriser could not create! " + Environment.NewLine + m_error_message);
                    }

                    rasterizer.Open(inputFile);
                    var outputPNGPath = System.IO.Path.Combine(GetTmpPath(), m_c_output_png_file);

                    if (m_page_count == 0)
                    {
                        m_page_count = rasterizer.PageCount - ignoreLastNPages;
                        m_search_counter = rasterizer.PageCount - ignoreLastNPages;
                    }

                    if (m_page_count == 0)
                        return null;

                    if (siteToolStripComboBox == null || siteToolStripComboBox.Items == null)
                    {
                        throw new Exception("PDFToPNG: 'siteToolStripComboBox' was not filled correctly! " + Environment.NewLine + m_error_message);
                    }

                    siteToolStripComboBox.Items.Clear();

                    for (int i = 1; i <= rasterizer.PageCount - ignoreLastNPages; i++)
                        siteToolStripComboBox.Items.Add(i);

                    for (int i = 1; i <= rasterizer.PageCount - ignoreLastNPages; i++)
                    {
                        var img = rasterizer.GetPage(300, i);
                        if (img == null)
                        {
                            return null;
                        }
                        ImageArray.Add(img);
                    }

                    if (ImageArray == null)
                    {
                        return null;
                    }
                    if (ImageArray.Count == 0)
                    {
                        return null;
                    }
                    if (ImageArray[0] == null)
                    {
                        return null;
                    }
                    if (string.IsNullOrEmpty(outputPNGPath))
                    {
                        return null;
                    }

                    ImageArray[0].Save(outputPNGPath);

                    siteToolStripComboBox.Text = siteToolStripComboBox.Items.Count.ToString();

                    return outputPNGPath;
                }
            }
            catch (Exception ex)
            {
                if (!m_isFromCOMServer)
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                else
                    //gErrors.SendErrorPerEmail(ToString(), ex, ohaERP_Library.Properties.Resources.msgCaptionError + " in: " + ToString(), m_start_pdf_path, m_error_message);
                    ShowMessageBox(enumShowMessageMode.Break, ex.Message);
                return null;
            }
        }

        private static string GetTmpPath()
        {
            return System.IO.Path.GetTempPath();
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

        private void siteToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            if (ImageArray == null || ImageArray.Count == 0) return;

            var outputPNGPath = System.IO.Path.Combine(GetTmpPath(), m_c_output_png_file);
            ImageArray[Convert.ToInt32(siteToolStripComboBox.Text) - 1].Save(outputPNGPath);
            DrawRectangles(outputPNGPath);

        }

        private void imageBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(react_by_mouse_clickToolStripComboBox.Text) || react_by_mouse_clickToolStripComboBox.Text != "Ja")
                return;

            var ib = (ImageBox)sender;
            var x_offset = ib.HorizontalScrollBar.Value;
            var y_offset = ib.VerticalScrollBar.Value;
            var x = e.Location.X;
            var y = e.Location.Y;
            var zoom = ib.ZoomScale;
            int pixelMousePosX = (int)((x / zoom) + x_offset);
            int pixelMousePosY = (int)((y / zoom) + y_offset);

            Point point = new Point(pixelMousePosX, pixelMousePosY);

            var match_rect_tmp_obj = WordsArray.FirstOrDefault(a => a.Rectangle.Contains(point) && !string.IsNullOrWhiteSpace(a.Text));
            var match_rect_tmp_obj_arr = WordsArray.Where(a => a.Rectangle.Contains(point) && !string.IsNullOrWhiteSpace(a.Text));

            if (match_rect_tmp_obj != null)
            {
                Rectangle match_rect = match_rect_tmp_obj.Rectangle;
                var text_tmp = WordsArray.FirstOrDefault(a => a.Rectangle == match_rect);
                var text = text_tmp == null ? "" : text_tmp.Text;

                var pts_X = match_rect.X + (match_rect.Width / 2);
                var pts_Y = match_rect.Y + (match_rect.Height / 2);

                if (m_adding_new)
                {
                //    var dgv_row = dataGridView1.CurrentRow;
                //if (dgv_row == null)
                //{
                    bindingSource1.AddNew();
                    var dgv_row = dataGridView1.CurrentRow;
                    dgv_row.Cells[xColumn.Index].Value = pts_X;
                    dgv_row.Cells[yColumn.Index].Value = pts_Y;
                    //}
                    //else
                    //{
                    //    if (dgv_row.Cells[xColumn.Index].Value == DBNull.Value)
                    //    {

                    //    }

                    m_adding_new=false;
                }
                else
                {
                    var dgv_row = dataGridView1.CurrentRow;
                    dgv_row.Cells[xColumn.Index].Value = pts_X;
                    dgv_row.Cells[yColumn.Index].Value = pts_Y;
                }

                //if (dgv_row.Cells[TextColumn.Index].Value != null && !string.IsNullOrEmpty(dgv_row.Cells[TextColumn.Index].Value.ToString()))
                //    dgv_row.Cells[TextColumn.Index].Value = text;  //+= "," + text;
                //else
                //dgv_row.Cells[TextColumn.Index].Value = text;
                //dgv_row.Cells[xColumn.Index].Value = pts_X;
                //dgv_row.Cells[yColumn.Index].Value = pts_Y;

                //bindingSource1.MoveNext();
                //}
                //else
                //{
                //    var dgv_row = ocr_model_detailsDataGridView.CurrentRow;
                //    ocr_model_detailsBindingSource.AddNew();
                //    dgv_row = ocr_model_detailsDataGridView.CurrentRow;
                //    dgv_row.Cells[ocr_model_details_Text_Column.Index].Value = text;
                //    dgv_row.Cells[ocr_model_details_X_Column.Index].Value = pts_X;
                //    dgv_row.Cells[ocr_model_details_Y_Column.Index].Value = pts_Y;
                //}
            }
        }

        private void bindingSource1_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            m_adding_new=true;
        }
    }



    public class ZUGFeRD_InfoType
    {
        int? Docu_archive_invoicesysid;
        List<ZUGFeRD_invoice_details_TypeTable>? ZUGFeRD_invoice_details_TypeTable;      // Positionen
        public string? InvoiceNo;                                                        // Rechungsnummer
        public DateTime? InvoiceDate;                                                    // Rechnungsdatum
        public decimal? TaxBasisAmount;                                                  // Netto Rechnungsbetrag
        decimal? TaxPercent;                                                             // MWST Prozentsatz
        public decimal? TaxTotalAmount;                                                  // MWST Betrag
        public decimal? GrandTotalAmount;                                                // Gesamtbetrag
        public string? Currency;                                                         // W�hrung

        public ZUGFeRD_InfoType(
            int? docu_archive_invoicesysid,
            List<ZUGFeRD_invoice_details_TypeTable>? zUGFeRD_invoice_details_TypeTable,
            string? invoiceNo,
            DateTime? invoiceDate,
            decimal? taxBasisAmount,
            decimal? taxPercent,
            decimal? taxTotalAmount,
            decimal? grandTotalAmount,
            string? currency)
        {
            Docu_archive_invoicesysid = docu_archive_invoicesysid;
            ZUGFeRD_invoice_details_TypeTable = zUGFeRD_invoice_details_TypeTable;
            InvoiceNo = invoiceNo;
            InvoiceDate = invoiceDate;
            TaxBasisAmount = taxBasisAmount;
            TaxPercent = taxPercent;
            TaxTotalAmount = taxTotalAmount;
            GrandTotalAmount = grandTotalAmount;
            Currency = currency;
        }
    }

    public class ZUGFeRD_invoice_details_TypeTable
    {
        public decimal? BilledQuantity;
        public string? SellerAssignedID;
        public decimal? GrossUnitPrice;
        public decimal? LineTotalAmount;

        public ZUGFeRD_invoice_details_TypeTable(
            decimal billedQuantity,
            string sellerAssignedID,
            decimal grossUnitPrice,
            decimal lineTotalAmount)
        {
            BilledQuantity = billedQuantity;
            SellerAssignedID = sellerAssignedID;
            GrossUnitPrice = grossUnitPrice;
            LineTotalAmount = lineTotalAmount;
        }
    }

    public enum enumShowMessageMode
    {
        Success, Break, OKCancel, YesNo, YesNoCancel, Done, Info,
        YesNoPlus
    }

    public class WordsArray
    {
        public Rectangle Rectangle;
        public string Text;

        public WordsArray(Rectangle rectangle, string text)
        {
            Rectangle = rectangle;
            Text = text;
        }
    }


}