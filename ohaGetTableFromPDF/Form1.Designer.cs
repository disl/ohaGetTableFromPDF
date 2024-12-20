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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            xColumn = new DataGridViewTextBoxColumn();
            yColumn = new DataGridViewTextBoxColumn();
            bindingSource1 = new BindingSource(components);
            dataSet1 = new DataSet1();
            ohaBindingNavigator1 = new ohaERP_Library.DataGridView.ohaBindingNavigator();
            imageBox1 = new Emgu.CV.UI.ImageBox();
            menuStrip1 = new MenuStrip();
            startToolStripMenuItem = new ToolStripMenuItem();
            getCsvToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            tesseractPathToolStripMenuItem = new ToolStripMenuItem();
            tesseract_pathToolStripTextBox = new ToolStripTextBox();
            spracheToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripComboBox = new ToolStripComboBox();
            seiteToolStripMenuItem = new ToolStripMenuItem();
            siteToolStripComboBox = new ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ohaBindingNavigator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.Controls.Add(ohaBindingNavigator1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(imageBox1);
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Description, xColumn, yColumn });
            dataGridView1.DataSource = bindingSource1;
            resources.ApplyResources(dataGridView1, "dataGridView1");
            dataGridView1.Name = "dataGridView1";
            // 
            // Column1
            // 
            Column1.DataPropertyName = "ColumnNo";
            resources.ApplyResources(Column1, "Column1");
            Column1.Name = "Column1";
            // 
            // Description
            // 
            Description.DataPropertyName = "Description";
            resources.ApplyResources(Description, "Description");
            Description.Name = "Description";
            // 
            // xColumn
            // 
            xColumn.DataPropertyName = "X";
            resources.ApplyResources(xColumn, "xColumn");
            xColumn.Name = "xColumn";
            // 
            // yColumn
            // 
            yColumn.DataPropertyName = "Y";
            resources.ApplyResources(yColumn, "yColumn");
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
            // ohaBindingNavigator1
            // 
            ohaBindingNavigator1.BindingSource = bindingSource1;
            resources.ApplyResources(ohaBindingNavigator1, "ohaBindingNavigator1");
            ohaBindingNavigator1.Name = "ohaBindingNavigator1";
            ohaBindingNavigator1.ShowMoveControls = true;
            ohaBindingNavigator1.ShowOnlyMoveControls = false;
            // 
            // imageBox1
            // 
            resources.ApplyResources(imageBox1, "imageBox1");
            imageBox1.Name = "imageBox1";
            imageBox1.TabStop = false;
            imageBox1.MouseClick += imageBox1_MouseClick;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { startToolStripMenuItem, getCsvToolStripMenuItem, toolStripMenuItem1, tesseractPathToolStripMenuItem, tesseract_pathToolStripTextBox, spracheToolStripMenuItem, languageToolStripComboBox, seiteToolStripMenuItem, siteToolStripComboBox });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            resources.ApplyResources(startToolStripMenuItem, "startToolStripMenuItem");
            startToolStripMenuItem.Click += startToolStripMenuItem_Click;
            // 
            // getCsvToolStripMenuItem
            // 
            getCsvToolStripMenuItem.Name = "getCsvToolStripMenuItem";
            resources.ApplyResources(getCsvToolStripMenuItem, "getCsvToolStripMenuItem");
            getCsvToolStripMenuItem.Click += getCsvToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // tesseractPathToolStripMenuItem
            // 
            tesseractPathToolStripMenuItem.Name = "tesseractPathToolStripMenuItem";
            resources.ApplyResources(tesseractPathToolStripMenuItem, "tesseractPathToolStripMenuItem");
            // 
            // tesseract_pathToolStripTextBox
            // 
            tesseract_pathToolStripTextBox.Name = "tesseract_pathToolStripTextBox";
            resources.ApplyResources(tesseract_pathToolStripTextBox, "tesseract_pathToolStripTextBox");
            // 
            // spracheToolStripMenuItem
            // 
            spracheToolStripMenuItem.Name = "spracheToolStripMenuItem";
            resources.ApplyResources(spracheToolStripMenuItem, "spracheToolStripMenuItem");
            // 
            // languageToolStripComboBox
            // 
            languageToolStripComboBox.Name = "languageToolStripComboBox";
            resources.ApplyResources(languageToolStripComboBox, "languageToolStripComboBox");
            // 
            // seiteToolStripMenuItem
            // 
            seiteToolStripMenuItem.Name = "seiteToolStripMenuItem";
            resources.ApplyResources(seiteToolStripMenuItem, "seiteToolStripMenuItem");
            // 
            // siteToolStripComboBox
            // 
            siteToolStripComboBox.Name = "siteToolStripComboBox";
            resources.ApplyResources(siteToolStripComboBox, "siteToolStripComboBox");
            siteToolStripComboBox.TextChanged += siteToolStripComboBox_TextChanged;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ohaBindingNavigator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private DataSet1 dataSet1;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private BindingSource bindingSource1;
        private ohaERP_Library.DataGridView.ohaBindingNavigator ohaBindingNavigator1;
        private ToolStripMenuItem getCsvToolStripMenuItem;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn xColumn;
        private DataGridViewTextBoxColumn yColumn;
    }
}
