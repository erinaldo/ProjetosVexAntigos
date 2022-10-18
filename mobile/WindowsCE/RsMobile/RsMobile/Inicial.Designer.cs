namespace RsMobile
{
    partial class Inicial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicial));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConfimar = new System.Windows.Forms.Button();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.txtDt = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVersao = new System.Windows.Forms.Label();
            this.lblMenssagem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 17);
            this.label1.Text = "Empresa:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 17);
            this.label2.Text = "Placa:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 20);
            this.label3.Text = "Nº do Documento:";
            // 
            // btnConfimar
            // 
            this.btnConfimar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConfimar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnConfimar.ForeColor = System.Drawing.Color.Black;
            this.btnConfimar.Location = new System.Drawing.Point(134, 245);
            this.btnConfimar.Name = "btnConfimar";
            this.btnConfimar.Size = new System.Drawing.Size(103, 20);
            this.btnConfimar.TabIndex = 6;
            this.btnConfimar.Text = "SEGUINTE >>";
            this.btnConfimar.Click += new System.EventHandler(this.btnConfimar_Click);
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.txtEmpresa.Location = new System.Drawing.Point(3, 68);
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(234, 32);
            this.txtEmpresa.TabIndex = 7;
            this.txtEmpresa.Text = "2";
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtPlaca.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.txtPlaca.Location = new System.Drawing.Point(3, 123);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(234, 32);
            this.txtPlaca.TabIndex = 8;
            this.txtPlaca.Text = "7912";
            // 
            // txtDt
            // 
            this.txtDt.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.txtDt.Location = new System.Drawing.Point(3, 179);
            this.txtDt.Name = "txtDt";
            this.txtDt.Size = new System.Drawing.Size(234, 32);
            this.txtDt.TabIndex = 9;
            this.txtDt.Text = "1706";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.Text = "SINCRONIZAR";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "ENTREGAS";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "OCORRÊNCIAS";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "FOTOS x COMPROVANTES";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(108, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.Text = "Versão:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVersao
            // 
            this.lblVersao.Location = new System.Drawing.Point(191, 21);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(46, 20);
            // 
            // lblMenssagem
            // 
            this.lblMenssagem.Location = new System.Drawing.Point(4, 218);
            this.lblMenssagem.Name = "lblMenssagem";
            this.lblMenssagem.Size = new System.Drawing.Size(233, 20);
            this.lblMenssagem.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblMenssagem);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtDt);
            this.Controls.Add(this.txtPlaca);
            this.Controls.Add(this.txtEmpresa);
            this.Controls.Add(this.btnConfimar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Inicial";
            this.Text = "RsMobile";
            this.Load += new System.EventHandler(this.Inicial_Load);
            this.Activated += new System.EventHandler(this.Inicial_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfimar;
        private System.Windows.Forms.TextBox txtEmpresa;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.TextBox txtDt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.Label lblMenssagem;

    }
}

