using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Neodynamic.SDK.Printing;


namespace $rootnamespace$
{
    public partial class MainForm : Form
    {

        const string LABEL_NOTE_4x3 = "IMPORTANT: This sample label was designed considering a label size of 4\" x 3\" (width & height). Please change the source code provided with these samples to accommodate them to your own label size before continuing. Do you want to continue?";
        const string LABEL_NOTE_3x2 = "IMPORTANT: This sample label was designed considering a label size of 3\" x 2\" (width & height). Please change the source code provided with these samples to accommodate them to your own label size before continuing. Do you want to continue?";
        

        double _dpi = 96;
        ThermalLabel _currentThermalLabel = null;
        int _currentDemoIndex = -1;
        ImageSettings _imgSettings = new ImageSettings();


        public MainForm()
        {
            InitializeComponent();
        }

        



        private void MainForm_Load(object sender, EventArgs e)
        {
            

            this.lstDemos.SelectedIndex = 0;
            this.cboDpi.SelectedIndex = 0;

            
            
        }


        

        private void DisplayThermalLabel()
        {

            int copies = 1;
                                    
            if (_currentDemoIndex == 0)
            {
                //Basic ThermalLabel
                _currentThermalLabel = this.GenerateBasicThermalLabel();
            }
            else if (_currentDemoIndex == 1)
            {
                //Advanced ThermalLabel
                _currentThermalLabel = this.GenerateAdvancedThermalLabel();
            }
            else if (_currentDemoIndex == 2)
            {
                //Data Binding with ThermalLabel
                _currentThermalLabel = this.GenerateThermalLabelDataBinding();
            }
            else if (_currentDemoIndex == 3)
            {
                //Counters with ThermalLabel
                _currentThermalLabel = this.GenerateThermalLabelCounters();
                copies = 5; //set copies to the num of labels to generate with counters to 5... 
            }
            else if (_currentDemoIndex == 4)
            {
                //Data Masking with ThermalLabel
                _currentThermalLabel = this.GenerateThermalLabelDataMasking();
                copies = 5; //set copies to the num of labels to generate with data masking to 5...
            }
                
            

            
            //Display ThermalLabel as a TIFF image
            if (_currentThermalLabel != null)
            {
                using (PrintJob pj = new PrintJob())
                {
                    pj.ThermalLabel = _currentThermalLabel;
                    pj.Copies = copies;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pj.ExportToImage(ms, new ImageSettings(ImageFormat.Tiff), _dpi);

                    

                    this.imgViewer.LoadImage(ms);
                }
            }

        }

        private void lstDemos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentDemoIndex != this.lstDemos.SelectedIndex)
            {
                _currentDemoIndex = this.lstDemos.SelectedIndex;
                this.DisplayThermalLabel();
            }
        
            //demo overview
            if (_currentDemoIndex == 0)
                lblDemoOverview.Text = "This is a simple 4in x 3in label featuring a TextItem and a BarcodeItem.";
            else if (_currentDemoIndex == 1)
                lblDemoOverview.Text = "This is an advanced 4in x 3in label featuring ImageItem TextItem and BarcodeItem objects with border settings, text sizing and white text on black background.";
            else if (_currentDemoIndex == 2)
                lblDemoOverview.Text = "ThermalLabel SDK supports .NET Data Binding scenarios allowing you to print thermal labels bound to a data source such as custom .NET objects, XML files, Databases, ADO.NET, etc.\n\nThe following sample features a class called Product with two basic properties: Id and Name. A list of Product and a ThermalLabel objects are used to perform data binding scenario generating a set of thermal labels for each product.";
            else if (_currentDemoIndex == 3)
                lblDemoOverview.Text = "Counters allow you to index data items by a selected increment or decrement value, making the data items to increase or decrease by a specified value each time a label is printed. Counters can be used with TextItem as well as BarcodeItem objects.\n\nThe following sample features a Counter scenario generating 5 labels with a Barcode Code-128 for values ranging from \"ABC1\" to \"ABC5\" and a text decreasing from 50 to 46.";
            else if (_currentDemoIndex == 4)
                lblDemoOverview.Text = "Data Masking allows you to serialize data items by specifying a Mask string as well as an increment string. Data Masking can be used with TextItem as well as BarcodeItem.\n\nIn the following sample, each label features a TextItem which serializes a product model ranging from \"MDL-001/X\" to \"MDL-005/X\" (i.e. the sequence is MDL-001/X, MDL-002/X, MDL-003/X, ..., MDL-005/X) and a BarcodeItem which serializes a product ID ranging from \"PRD000-A\" to \"PRD400-E\" (i.e. the sequence is \"PRD000-A\", \"PRD100-B\", ..., \"PRD400-E\") in Code 128 symbology.";

            
        }

        private void cboDpi_SelectedIndexChanged(object sender, EventArgs e)
        {
            double tmpDPI = 96;
            
            if(cboDpi.SelectedItem.ToString() != "Screen")
                tmpDPI = double.Parse(cboDpi.SelectedItem.ToString());

            if (tmpDPI != _dpi)
            {
                _dpi = tmpDPI;
                this.DisplayThermalLabel();
            }
        }



        private ThermalLabel GenerateBasicThermalLabel()
        {
            //Define a ThermalLabel object and set unit to inch and label size
            ThermalLabel tLabel = new ThermalLabel(UnitType.Inch, 4, 3);
            tLabel.GapLength = 0.2;

            //Define a TextItem object
            TextItem txtItem = new TextItem(0.2, 0.2, 2.5, 0.5, "Thermal Label Test");

            //Define a BarcodeItem object
            BarcodeItem bcItem = new BarcodeItem(0.2, 1, 2, 1, BarcodeSymbology.Code128, "ABC 12345");
            //Set bars height to .75inch
            bcItem.BarHeight = 0.75;
            //Set bars width to 0.0104inch
            bcItem.BarWidth = 0.0104;

            //Add items to ThermalLabel object...
            tLabel.Items.Add(txtItem);
            tLabel.Items.Add(bcItem);

            
            return tLabel;
        }


        private ThermalLabel GenerateAdvancedThermalLabel()
        {

            //Define a ThermalLabel object and set unit to inch and label size
            ThermalLabel tLabel = new ThermalLabel(UnitType.Inch, 4, 3);
            tLabel.GapLength = 0.2;


            //get ThermalLabel SDk install dir and get the sample images
            string imgFolder = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Neodynamic\\SDK\\ThermalLabel SDK 5.0 for .NET\\InstallDir").GetValue(null).ToString() + "\\Samples\\Images\\";


            //Define an ImageItem for AdventureWorks logo
            ImageItem awLogo = new ImageItem(0.1, 0.1);
            awLogo.SourceFile = imgFolder + "adventureworks.jpg";
            awLogo.Width = 1.5;
            awLogo.LockAspectRatio = LockAspectRatio.WidthBased;
            awLogo.MonochromeSettings.DitherMethod = DitherMethod.Threshold;
            awLogo.MonochromeSettings.Threshold = 80;

            //Define a TextItem for 'AW' id
            TextItem txtAW = new TextItem(2.8, 0.1, 1.1, 0.5, "AW");
            //font settings
            txtAW.Font.Name = "Arial";
            txtAW.Font.Bold = true;
            //stretch text
            txtAW.Sizing = TextSizing.Stretch;
            //border settings
            txtAW.BorderThickness = new FrameThickness(0.02);
            txtAW.CornerRadius = new RectangleCornerRadius(0.05);
            txtAW.TextPadding = new FrameThickness(0);

            //Define a TextItem for 'Model Name'
            TextItem txtModelName = new TextItem(0.1, 0.75, 3.8, 0.25, "Model Name: ROAD 150");
            //font settings
            txtModelName.Font.Name = "Arial";
            txtModelName.Font.Unit = FontUnit.Point;
            txtModelName.Font.Size = 12;
            //white text on black background
            txtModelName.BackColor = Neodynamic.SDK.Printing.Color.Black;
            txtModelName.ForeColor = Neodynamic.SDK.Printing.Color.White;
            //padding
            txtModelName.TextPadding = new FrameThickness(0.075, 0.03, 0, 0);

            //Define a TextItem for 'Model Code' (random code)
            TextItem txtModelCode = new TextItem(0.1, 1, 3.8, 0.25, "Model Code: " + Guid.NewGuid().ToString().ToUpper().Substring(0, 17));
            //font settings
            txtModelCode.Font.Name = "Arial";
            txtModelCode.Font.Unit = FontUnit.Point;
            txtModelCode.Font.Size = 12;
            //white text on black background
            txtModelCode.BackColor = Neodynamic.SDK.Printing.Color.Black;
            txtModelCode.ForeColor = Neodynamic.SDK.Printing.Color.White;
            //padding
            txtModelCode.TextPadding = new FrameThickness(0.075, 0.03, 0, 0);

            //Define a BarcodeItem for a random 'Serial Number'
            string serialNum = Guid.NewGuid().ToString().ToUpper().Substring(0, 8);
            BarcodeItem serialBarcode = new BarcodeItem(0.1, 1.25, 3.8, 0.5, BarcodeSymbology.Code39, serialNum);
            //Set bars height to .3inch
            serialBarcode.BarHeight = 0.3;
            //Set bars width to 0.02inch
            serialBarcode.BarWidth = 0.02;
            //disable checksum
            serialBarcode.AddChecksum = false;
            //hide human readable text
            serialBarcode.DisplayCode = false;
            //set border
            serialBarcode.BorderThickness = new FrameThickness(0.02);
            //align barcode
            serialBarcode.BarcodeAlignment = BarcodeAlignment.MiddleCenter;


            //Define a TextItem for 'Serial Num'
            TextItem txtSN = new TextItem(0.1, 1.75 - serialBarcode.BorderThickness.Bottom, 1.25, 0.3, "S/N: " + serialNum);
            //font settings
            txtSN.Font.Name = "Arial Narrow";
            txtSN.Font.Bold = true;
            txtSN.Font.Unit = FontUnit.Point;
            txtSN.Font.Size = 12;
            //padding
            txtSN.TextPadding = new FrameThickness(0.03);
            //set border
            txtSN.BorderThickness = new FrameThickness(0.02);
            txtSN.TextAlignment = TextAlignment.Center;

            //Define a TextItem for legend
            TextItem txtLegend = new TextItem(txtSN.X + txtSN.Width - txtSN.BorderThickness.Right, txtSN.Y, 3.8 - txtSN.Width + txtSN.BorderThickness.Right, txtSN.Height, "This bike is ridden by race winners! Brought to you by Adventure Works Cycles professional race team.");
            //font settings
            txtLegend.Font.Name = "Arial";
            txtLegend.Font.Unit = FontUnit.Point;
            txtLegend.Font.Size = 7.5;
            //padding
            txtLegend.TextPadding = new FrameThickness(0.03, 0, 0, 0);
            //set border
            txtLegend.BorderThickness = new FrameThickness(0.02);

            //Define another BarcodeItem for EAN-13 symbology
            BarcodeItem eanBarcode = new BarcodeItem(0.1, 2.1, 3, 0.9, BarcodeSymbology.Ean13, "0729507704739");
            //Set barcode dimensions...
            eanBarcode.BarHeight = 0.5;
            eanBarcode.BarWidth = 0.02;
            eanBarcode.EanUpcGuardBarHeight = 0.55;
            //human readable text font settings
            eanBarcode.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            eanBarcode.Font.Unit = FontUnit.Point;
            eanBarcode.Font.Size = 5;
            
            //Define an ImageItem for NBDA logo
            ImageItem nbdaLogo = new ImageItem(2.9, 2.1);
            nbdaLogo.SourceFile = imgFolder + "nbda.jpg";
            nbdaLogo.Width = 1;
            nbdaLogo.LockAspectRatio = LockAspectRatio.WidthBased;
            nbdaLogo.MonochromeSettings.DitherMethod = DitherMethod.Threshold;
            nbdaLogo.MonochromeSettings.Threshold = 50;
            nbdaLogo.MonochromeSettings.ReverseEffect = true;

            //Define a LineShapeItem
            LineShapeItem line = new LineShapeItem(0.1, 2.8, 3.8, 0.03);
            line.Orientation = LineOrientation.Horizontal;
            line.StrokeThickness = 0.03;


            //Add items to ThermalLabel object...
            tLabel.Items.Add(awLogo);
            tLabel.Items.Add(txtAW);
            tLabel.Items.Add(txtModelName);
            tLabel.Items.Add(txtModelCode);
            tLabel.Items.Add(serialBarcode);
            tLabel.Items.Add(txtSN);
            tLabel.Items.Add(txtLegend);
            tLabel.Items.Add(eanBarcode);
            tLabel.Items.Add(nbdaLogo);
            tLabel.Items.Add(line);

            return tLabel;
        }

        private ThermalLabel GenerateThermalLabelDataBinding()
        {
            //Define a ThermalLabel object and set unit to inch and label size
            ThermalLabel tLabel = new ThermalLabel(UnitType.Inch, 3, 2);
            tLabel.GapLength = 0.2;

            //Define a TextItem object for product name
            TextItem txt = new TextItem(0.1, 0.1, 2.8, 0.5, "");
            //set data field
            txt.DataField = "Name";
            //set font
            txt.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            txt.Font.Unit = FontUnit.Point;
            txt.Font.Size = 10;
            //set border
            txt.BorderThickness = new FrameThickness(0.03);
            //set alignment
            txt.TextAlignment = TextAlignment.Center;
            txt.TextPadding = new FrameThickness(0, 0.1, 0, 0);

            //Define a BarcodeItem object for encoding product id with a Code 128 symbology
            BarcodeItem bc = new BarcodeItem(0.1, 0.57, 2.8, 1.3, BarcodeSymbology.Code128, "");
            //set data field
            bc.DataField = "Id";
            //set barcode size
            bc.BarWidth = 0.01;
            bc.BarHeight = 0.75;
            //set barcode alignment
            bc.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
            //set text alignment
            bc.CodeAlignment = BarcodeTextAlignment.BelowCenter;
            //set border
            bc.BorderThickness = new FrameThickness(0.03);

            //Add items to ThermalLabel object...
            tLabel.Items.Add(txt);
            tLabel.Items.Add(bc);

            //Create data source...
            List<Product> products = new List<Product>();
            products.Add(new Product("OO2935", "Olive Oil"));
            products.Add(new Product("CS4948", "Curry Sauce"));
            products.Add(new Product("CH0094", "Chocolate"));
            products.Add(new Product("MZ1027", "Mozzarella"));

            //set data source...
            tLabel.DataSource = products;

            
            return tLabel;
        
        }


        private ThermalLabel GenerateThermalLabelCounters()
        {
            //Define a ThermalLabel object and set unit to inch and label size
            ThermalLabel tLabel = new ThermalLabel(UnitType.Inch, 3, 2);
            tLabel.GapLength = 0.2;

            //Define a TextItem object 
            TextItem txt = new TextItem(0.1, 0.1, 2.8, 0.5, "Decreasing 50");
            //set counter step for decreasing by 1
            txt.CounterStep = -1;
            //set font
            txt.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            txt.Font.Unit = FontUnit.Point;
            txt.Font.Size = 10;

            //Define a BarcodeItem object
            BarcodeItem bc = new BarcodeItem(0.1, 0.57, 2.8, 1.3, BarcodeSymbology.Code128, "ABC01");
            //set counter step for increasing by 1
            bc.CounterStep = 1;
            //set barcode size
            bc.BarWidth = 0.02;
            bc.BarHeight = 0.75;
            //set barcode alignment
            bc.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
            //set font
            bc.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            bc.Font.Unit = FontUnit.Point;
            bc.Font.Size = 10;

            //Add items to ThermalLabel object...
            tLabel.Items.Add(txt);
            tLabel.Items.Add(bc);
            
            return tLabel;
        }


        private ThermalLabel GenerateThermalLabelDataMasking()
        {
            //Define a ThermalLabel object and set unit to inch and label size
            ThermalLabel tLabel = new ThermalLabel(UnitType.Inch, 3, 2);
            tLabel.GapLength = 0.2;

            //Define a TextItem object 
            TextItem txt = new TextItem(0.1, 0.1, 2.8, 0.5, "MDL-001/X");
            //set Mask info...
            txt.Mask = "%%%%ddd%%";
            txt.MaskIncrement = "1%%";
            //set font
            txt.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            txt.Font.Unit = FontUnit.Point;
            txt.Font.Size = 10;

            //Define a BarcodeItem object
            BarcodeItem bc = new BarcodeItem(0.1, 0.57, 2.8, 1.3, BarcodeSymbology.Code128, "PRD000-A");
            //set Mask info...
            bc.Mask = "%%%d%%%A";
            bc.MaskIncrement = "1%%%B";
            //set barcode size
            bc.BarWidth = 0.02;
            bc.BarHeight = 0.75;
            //set barcode alignment
            bc.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
            //set font
            bc.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            bc.Font.Unit = FontUnit.Point;
            bc.Font.Size = 10;

            //Add items to ThermalLabel object...
            tLabel.Items.Add(txt);
            tLabel.Items.Add(bc);

            return tLabel;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            //warning about label size
            string msg = LABEL_NOTE_3x2;
            if (_currentDemoIndex == 0 ||
                _currentDemoIndex == 1 ||
                _currentDemoIndex == 5)
            {
                msg = LABEL_NOTE_4x3;
            }

            if (MessageBox.Show(msg, "NOTE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            //Display Print Job dialog...           
            PrintJobDialog frmPrintJob = new PrintJobDialog();
            if (frmPrintJob.ShowDialog() == DialogResult.OK)
            {
                //create a PrintJob object
                using (PrintJob pj = new PrintJob(frmPrintJob.PrinterSettings))
                {
                    pj.Copies = frmPrintJob.Copies; // set copies
                    pj.PrintOrientation = frmPrintJob.PrintOrientation; //set orientation
                    pj.ThermalLabel = _currentThermalLabel; // set the ThermalLabel object
                    pj.Print(); // print the ThermalLabel object                    
                }
            }
        }

        private void btnExportToPdf_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Adobe PDF|*.pdf";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //create a PrintJob object
                using (PrintJob pj = new PrintJob())
                {
                    pj.ThermalLabel = _currentThermalLabel; // set the ThermalLabel object

                    //set num of copies if Counters or Data Masking demos
                    if (_currentDemoIndex == 3 || _currentDemoIndex == 4)
                        pj.Copies = 5;

                    pj.ExportToPdf(sfd.FileName, _dpi); //export to pdf
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
        }

        private void btnXmlTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Template|*.xml";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //save ThermalLabel to XML template
                System.IO.File.WriteAllText(sfd.FileName, _currentThermalLabel.GetXmlTemplate());

                if (MessageBox.Show("XML Template saved! Do you want to open it?", "ThermalLabel SDK", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                } 
                
            }
        }

        private void btnToImagePng_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Png;
            this.ExportToImage();
        }

        private void btnToImageJpeg_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Jpeg;
            this.ExportToImage();
        }

        private void btnToImageTiff_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Tiff;
            this.ExportToImage();
        }

        private void btnToImageGif_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Gif;
            this.ExportToImage();
        }

        private void btnToImageBmp_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Bmp;
            this.ExportToImage();
        }

        private void ExportToImage()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if(_imgSettings.ImageFormat == ImageFormat.Png)
                sfd.Filter = "PNG|*.png";
            else if (_imgSettings.ImageFormat == ImageFormat.Gif)
                sfd.Filter = "GIF|*.gif";
            else if (_imgSettings.ImageFormat == ImageFormat.Jpeg)
                sfd.Filter = "JPEG|*.jpg";
            else if (_imgSettings.ImageFormat == ImageFormat.Tiff)
                sfd.Filter = "TIFF|*.tif";
            else if (_imgSettings.ImageFormat == ImageFormat.Bmp)
                sfd.Filter = "BMP|*.bmp";
            
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //create a PrintJob object
                using (PrintJob pj = new PrintJob())
                {
                    pj.ThermalLabel = _currentThermalLabel; // set the ThermalLabel object

                    //set num of copies if Counters or Data Masking demos
                    if (_currentDemoIndex == 3 || _currentDemoIndex == 4)
                        pj.Copies = 5;

                    pj.ExportToImage(sfd.FileName, _imgSettings, _dpi); //export to image file
                    
                    
                    //Open folder where image file was created
                    System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(sfd.FileName));
                    
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {

            try
            {
                string helpPath = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Neodynamic\\SDK\\ThermalLabel SDK 5.0 for .NET\\InstallDir").GetValue(null).ToString() + "\\Help\\ThermalLabelSdkHelp.chm";
                if (System.IO.File.Exists(helpPath))
                    System.Diagnostics.Process.Start(helpPath);
            }
            catch 
            {
                MessageBox.Show("Cannot find the ThermalLabel SDK help docs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.neodynamic.com/Support/Support.aspx");
        }

        
    }
}
