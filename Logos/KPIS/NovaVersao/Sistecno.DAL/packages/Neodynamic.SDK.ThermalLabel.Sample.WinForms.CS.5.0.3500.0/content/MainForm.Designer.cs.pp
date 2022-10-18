namespace $rootnamespace$
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lstDemos = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDemoOverview = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.imgViewer = new ThermalLabelSdkSamplesCS.ImageViewer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboDpi = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportToPdf = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.btnToImagePng = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageJpeg = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageTiff = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageGif = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageBmp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnXmlTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDemos
            // 
            this.lstDemos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDemos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDemos.FormattingEnabled = true;
            this.lstDemos.ItemHeight = 25;
            this.lstDemos.Items.AddRange(new object[] {
            "Basic Label",
            "Advanced Label",
            "Data Binding",
            "Counters",
            "Data Masking"});
            this.lstDemos.Location = new System.Drawing.Point(12, 13);
            this.lstDemos.Name = "lstDemos";
            this.lstDemos.Size = new System.Drawing.Size(171, 125);
            this.lstDemos.TabIndex = 1;
            this.lstDemos.SelectedIndexChanged += new System.EventHandler(this.lstDemos_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblDemoOverview);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lstDemos);
            this.panel2.Location = new System.Drawing.Point(12, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(195, 455);
            this.panel2.TabIndex = 2;
            // 
            // lblDemoOverview
            // 
            this.lblDemoOverview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDemoOverview.AutoEllipsis = true;
            this.lblDemoOverview.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblDemoOverview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoOverview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDemoOverview.Location = new System.Drawing.Point(12, 183);
            this.lblDemoOverview.Name = "lblDemoOverview";
            this.lblDemoOverview.Size = new System.Drawing.Size(171, 259);
            this.lblDemoOverview.TabIndex = 4;
            this.lblDemoOverview.Text = "Overview";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Overview";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Font = new System.Drawing.Font("Wingdings", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ü";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.imgViewer);
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Location = new System.Drawing.Point(223, 89);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(671, 455);
            this.panel3.TabIndex = 3;
            // 
            // imgViewer
            // 
            this.imgViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgViewer.Location = new System.Drawing.Point(0, 53);
            this.imgViewer.Name = "imgViewer";
            this.imgViewer.Size = new System.Drawing.Size(669, 400);
            this.imgViewer.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboDpi,
            this.toolStripSeparator2,
            this.btnPrint,
            this.toolStripSeparator1,
            this.btnExportToPdf,
            this.toolStripSplitButton1,
            this.toolStripSeparator3,
            this.btnXmlTemplate,
            this.toolStripSeparator4,
            this.btnHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(669, 53);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(73, 50);
            this.toolStripLabel1.Text = "Preview DPI";
            // 
            // cboDpi
            // 
            this.cboDpi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDpi.DropDownWidth = 75;
            this.cboDpi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDpi.Items.AddRange(new object[] {
            "Screen",
            "203",
            "300",
            "600"});
            this.cboDpi.Name = "cboDpi";
            this.cboDpi.Size = new System.Drawing.Size(75, 53);
            this.cboDpi.SelectedIndexChanged += new System.EventHandler(this.cboDpi_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::ThermalLabelSdkSamplesCS.Properties.Resources.ThermalPrinterIcon32x32;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(36, 50);
            this.btnPrint.Text = "Print";
            this.btnPrint.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // btnExportToPdf
            // 
            this.btnExportToPdf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToPdf.Image = global::ThermalLabelSdkSamplesCS.Properties.Resources.PDF;
            this.btnExportToPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToPdf.Name = "btnExportToPdf";
            this.btnExportToPdf.Size = new System.Drawing.Size(88, 50);
            this.btnExportToPdf.Text = "Export to PDF";
            this.btnExportToPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportToPdf.Click += new System.EventHandler(this.btnExportToPdf_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnToImagePng,
            this.btnToImageJpeg,
            this.btnToImageTiff,
            this.btnToImageGif,
            this.btnToImageBmp});
            this.toolStripSplitButton1.Image = global::ThermalLabelSdkSamplesCS.Properties.Resources.ImageFile;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(113, 50);
            this.toolStripSplitButton1.Text = "Export to Image";
            this.toolStripSplitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnToImagePng
            // 
            this.btnToImagePng.Name = "btnToImagePng";
            this.btnToImagePng.Size = new System.Drawing.Size(101, 22);
            this.btnToImagePng.Text = "PNG";
            this.btnToImagePng.Click += new System.EventHandler(this.btnToImagePng_Click);
            // 
            // btnToImageJpeg
            // 
            this.btnToImageJpeg.Name = "btnToImageJpeg";
            this.btnToImageJpeg.Size = new System.Drawing.Size(101, 22);
            this.btnToImageJpeg.Text = "JPEG";
            this.btnToImageJpeg.Click += new System.EventHandler(this.btnToImageJpeg_Click);
            // 
            // btnToImageTiff
            // 
            this.btnToImageTiff.Name = "btnToImageTiff";
            this.btnToImageTiff.Size = new System.Drawing.Size(101, 22);
            this.btnToImageTiff.Text = "TIFF";
            this.btnToImageTiff.Click += new System.EventHandler(this.btnToImageTiff_Click);
            // 
            // btnToImageGif
            // 
            this.btnToImageGif.Name = "btnToImageGif";
            this.btnToImageGif.Size = new System.Drawing.Size(101, 22);
            this.btnToImageGif.Text = "GIF";
            this.btnToImageGif.Click += new System.EventHandler(this.btnToImageGif_Click);
            // 
            // btnToImageBmp
            // 
            this.btnToImageBmp.Name = "btnToImageBmp";
            this.btnToImageBmp.Size = new System.Drawing.Size(101, 22);
            this.btnToImageBmp.Text = "BMP";
            this.btnToImageBmp.Click += new System.EventHandler(this.btnToImageBmp_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 53);
            // 
            // btnXmlTemplate
            // 
            this.btnXmlTemplate.Image = global::ThermalLabelSdkSamplesCS.Properties.Resources.TOXML;
            this.btnXmlTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXmlTemplate.Name = "btnXmlTemplate";
            this.btnXmlTemplate.Size = new System.Drawing.Size(82, 50);
            this.btnXmlTemplate.Text = "Save To XML";
            this.btnXmlTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXmlTemplate.Click += new System.EventHandler(this.btnXmlTemplate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 53);
            // 
            // btnHelp
            // 
            this.btnHelp.Image = global::ThermalLabelSdkSamplesCS.Properties.Resources.help;
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(36, 50);
            this.btnHelp.Text = "Help";
            this.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 83);
            this.panel1.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(708, 64);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(189, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Questions? support@neodynamic.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(906, 556);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Neodynamic ThermalLabel SDK - C# Samples";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstDemos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDemoOverview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportToPdf;
        private ImageViewer imgViewer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox cboDpi;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem btnToImagePng;
        private System.Windows.Forms.ToolStripMenuItem btnToImageJpeg;
        private System.Windows.Forms.ToolStripMenuItem btnToImageTiff;
        private System.Windows.Forms.ToolStripMenuItem btnToImageGif;
        private System.Windows.Forms.ToolStripMenuItem btnToImageBmp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnXmlTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}