namespace ohaGetTableFromPDF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            startToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            tesseractPathToolStripMenuItem = new ToolStripMenuItem();
            tesseract_pathToolStripTextBox = new ToolStripTextBox();
            spracheToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripComboBox = new ToolStripComboBox();
            seiteToolStripMenuItem = new ToolStripMenuItem();
            siteToolStripComboBox = new ToolStripComboBox();
            aufMausKlickReagierenToolStripMenuItem = new ToolStripMenuItem();
            react_by_mouse_clickToolStripComboBox = new ToolStripComboBox();
            imageBox1 = new Emgu.CV.UI.ImageBox();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            xColumn = new DataGridViewTextBoxColumn();
            yColumn = new DataGridViewTextBoxColumn();
            bindingSource1 = new BindingSource(components);
            dataSet1 = new DataSet1();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { startToolStripMenuItem, toolStripMenuItem1, tesseractPathToolStripMenuItem, tesseract_pathToolStripTextBox, spracheToolStripMenuItem, languageToolStripComboBox, seiteToolStripMenuItem, siteToolStripComboBox, aufMausKlickReagierenToolStripMenuItem, react_by_mouse_clickToolStripComboBox });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1350, 27);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.Size = new Size(43, 23);
            startToolStripMenuItem.Text = "Start";
            startToolStripMenuItem.Click += startToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(22, 23);
            toolStripMenuItem1.Text = "|";
            // 
            // tesseractPathToolStripMenuItem
            // 
            tesseractPathToolStripMenuItem.Name = "tesseractPathToolStripMenuItem";
            tesseractPathToolStripMenuItem.Size = new Size(98, 23);
            tesseractPathToolStripMenuItem.Text = "Tesseract-Path:";
            // 
            // tesseract_pathToolStripTextBox
            // 
            tesseract_pathToolStripTextBox.Name = "tesseract_pathToolStripTextBox";
            tesseract_pathToolStripTextBox.Size = new Size(100, 23);
            tesseract_pathToolStripTextBox.Text = "C:\\Tesseract";
            // 
            // spracheToolStripMenuItem
            // 
            spracheToolStripMenuItem.Name = "spracheToolStripMenuItem";
            spracheToolStripMenuItem.Size = new Size(64, 23);
            spracheToolStripMenuItem.Text = "Sprache:";
            // 
            // languageToolStripComboBox
            // 
            languageToolStripComboBox.Name = "languageToolStripComboBox";
            languageToolStripComboBox.Size = new Size(121, 23);
            // 
            // seiteToolStripMenuItem
            // 
            seiteToolStripMenuItem.Name = "seiteToolStripMenuItem";
            seiteToolStripMenuItem.Size = new Size(47, 23);
            seiteToolStripMenuItem.Text = "Seite:";
            // 
            // siteToolStripComboBox
            // 
            siteToolStripComboBox.Name = "siteToolStripComboBox";
            siteToolStripComboBox.Size = new Size(121, 23);
            siteToolStripComboBox.TextChanged += siteToolStripComboBox_TextChanged;
            // 
            // aufMausKlickReagierenToolStripMenuItem
            // 
            aufMausKlickReagierenToolStripMenuItem.Name = "aufMausKlickReagierenToolStripMenuItem";
            aufMausKlickReagierenToolStripMenuItem.Size = new Size(148, 23);
            aufMausKlickReagierenToolStripMenuItem.Text = "auf Maus Klick reagieren";
            // 
            // react_by_mouse_clickToolStripComboBox
            // 
            react_by_mouse_clickToolStripComboBox.Items.AddRange(new object[] { "Ja", "Nein" });
            react_by_mouse_clickToolStripComboBox.Name = "react_by_mouse_clickToolStripComboBox";
            react_by_mouse_clickToolStripComboBox.Size = new Size(121, 23);
            // 
            // imageBox1
            // 
            imageBox1.Dock = DockStyle.Fill;
            imageBox1.Location = new Point(0, 0);
            imageBox1.Name = "imageBox1";
            imageBox1.Size = new Size(1001, 695);
            imageBox1.TabIndex = 2;
            imageBox1.TabStop = false;
            imageBox1.MouseClick += imageBox1_MouseClick;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 27);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(imageBox1);
            splitContainer1.Size = new Size(1350, 695);
            splitContainer1.SplitterDistance = 345;
            splitContainer1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, xColumn, yColumn });
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(345, 695);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.DataPropertyName = "ColumnNo";
            Column1.HeaderText = "ColumnNo";
            Column1.Name = "Column1";
            // 
            // xColumn
            // 
            xColumn.DataPropertyName = "X";
            xColumn.HeaderText = "X";
            xColumn.Name = "xColumn";
            // 
            // yColumn
            // 
            yColumn.DataPropertyName = "Y";
            yColumn.HeaderText = "Y";
            yColumn.Name = "yColumn";
            // 
            // bindingSource1
            // 
            bindingSource1.DataMember = "Columns";
            bindingSource1.DataSource = dataSet1;
            bindingSource1.AddingNew += bindingSource1_AddingNew;
            // 
            // dataSet1
            // 
            dataSet1.DataSetName = "DataSet1";
            dataSet1.Namespace = "http://tempuri.org/DataSet1.xsd";
            dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 722);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem startToolStripMenuItem;
        private Emgu.CV.UI.ImageBox imageBox1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem tesseractPathToolStripMenuItem;
        private ToolStripTextBox tesseract_pathToolStripTextBox;
        private ToolStripMenuItem spracheToolStripMenuItem;
        private ToolStripComboBox languageToolStripComboBox;
        private ToolStripMenuItem seiteToolStripMenuItem;
        private ToolStripComboBox siteToolStripComboBox;
        private ToolStripMenuItem aufMausKlickReagierenToolStripMenuItem;
        private ToolStripComboBox react_by_mouse_clickToolStripComboBox;
        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private DataSet1 dataSet1;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private BindingSource bindingSource1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn xColumn;
        private DataGridViewTextBoxColumn yColumn;
    }
}
