using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
//using Common;
using PocketSignature;

namespace SignCaptureV2
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel areaSignature;
		private System.Windows.Forms.Button butNew;
		private System.Windows.Forms.Button Save;
		private System.Windows.Forms.Button Load;

		SignatureControl signature = new SignatureControl();

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// add signature control to form
			signature.Location = areaSignature.Location;
			signature.Size = areaSignature.Size;
			signature.Background = "\\Program Files\\SignCaptureV2\\sign here.png";
			this.Controls.Add(signature);
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.areaSignature = new System.Windows.Forms.Panel();
			this.butNew = new System.Windows.Forms.Button();
			this.Save = new System.Windows.Forms.Button();
			this.Load = new System.Windows.Forms.Button();
			// 
			// areaSignature
			// 
			this.areaSignature.BackColor = System.Drawing.Color.Gainsboro;
			this.areaSignature.Location = new System.Drawing.Point(8, 8);
			this.areaSignature.Size = new System.Drawing.Size(224, 120);
			this.areaSignature.Visible = false;
			// 
			// butNew
			// 
			this.butNew.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
			this.butNew.Location = new System.Drawing.Point(0, 232);
			this.butNew.Size = new System.Drawing.Size(54, 24);
			this.butNew.Text = "Clear";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// Save
			// 
			this.Save.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
			this.Save.Location = new System.Drawing.Point(64, 232);
			this.Save.Size = new System.Drawing.Size(72, 24);
			this.Save.Text = "Save in File";
			this.Save.Click += new System.EventHandler(this.Save_Click);
			// 
			// Load
			// 
			this.Load.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
			this.Load.Location = new System.Drawing.Point(144, 232);
			this.Load.Size = new System.Drawing.Size(88, 24);
			this.Load.Text = "Load from file";
			this.Load.Click += new System.EventHandler(this.Load_Click);
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(258, 270);
			this.Controls.Add(this.Load);
			this.Controls.Add(this.Save);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.areaSignature);
			this.Text = "PocketSignature";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void butNew_Click(object sender, System.EventArgs e)
		{
			signature.Clear();
			this.Refresh();
		}

		private void Save_Click(object sender, System.EventArgs e)
		{
			signature.StoreSigData("SignFile.txt");

		}

		private void Load_Click(object sender, System.EventArgs e)
		{
			int baseX = 10;
			int baseY = 100;
			string signatureFile = "SignFile.txt";
			load_signature(baseX,baseY,signatureFile);
			
		}
		void load_signature(int baseX,int baseY,string signatureFile) 
		{
			System.IO.StreamReader streamReader =	new System.IO.StreamReader("SignFile.txt");
			string pointString = null;
           
			while ((pointString = streamReader.ReadLine())!= null) 
			{
				if(pointString.Trim().Length>0)
				{
					String[] points = new String[4];
					points = pointString.Split(new Char[]{' '});
					Pen pen = new Pen(Color.Black);
					this.CreateGraphics().DrawLine(pen, (baseX+int.Parse(points[0].ToString())), (baseY+int.Parse(points[1].ToString())), (baseX+int.Parse(points[2].ToString())), (baseY+int.Parse(points[3].ToString())));
				}
			}
			streamReader.Close();
		}
	}

}
