namespace DownloadXMLNFeExemploBasico
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.Web1 = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLoteProcessamento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.EditChave = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EditCaptcha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Img1 = new System.Windows.Forms.PictureBox();
            this.chkManual = new System.Windows.Forms.CheckBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ButBaixar2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblContador = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnIncluirChave = new System.Windows.Forms.Button();
            this.txtIncluirChave = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.grdLotesPendentes = new System.Windows.Forms.DataGridView();
            this.btnExcluirLote = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Img1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotesPendentes)).BeginInit();
            this.SuspendLayout();
            // 
            // Web1
            // 
            this.Web1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Web1.Location = new System.Drawing.Point(13, 57);
            this.Web1.MinimumSize = new System.Drawing.Size(20, 20);
            this.Web1.Name = "Web1";
            this.Web1.Size = new System.Drawing.Size(547, 165);
            this.Web1.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtLoteProcessamento);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.EditChave);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.EditCaptcha);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Img1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 187);
            this.panel1.TabIndex = 18;
            // 
            // txtLoteProcessamento
            // 
            this.txtLoteProcessamento.Location = new System.Drawing.Point(275, 111);
            this.txtLoteProcessamento.Name = "txtLoteProcessamento";
            this.txtLoteProcessamento.Size = new System.Drawing.Size(292, 20);
            this.txtLoteProcessamento.TabIndex = 46;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Lote de Processamento";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Location = new System.Drawing.Point(6, 138);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(569, 39);
            this.textBox1.TabIndex = 44;
            // 
            // EditChave
            // 
            this.EditChave.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.EditChave.Enabled = false;
            this.EditChave.Location = new System.Drawing.Point(275, 26);
            this.EditChave.Name = "EditChave";
            this.EditChave.Size = new System.Drawing.Size(292, 20);
            this.EditChave.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Chave";
            // 
            // EditCaptcha
            // 
            this.EditCaptcha.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.EditCaptcha.Enabled = false;
            this.EditCaptcha.Location = new System.Drawing.Point(275, 69);
            this.EditCaptcha.Name = "EditCaptcha";
            this.EditCaptcha.Size = new System.Drawing.Size(292, 20);
            this.EditCaptcha.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Captcha";
            // 
            // Img1
            // 
            this.Img1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Img1.Location = new System.Drawing.Point(4, 19);
            this.Img1.Name = "Img1";
            this.Img1.Size = new System.Drawing.Size(250, 112);
            this.Img1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Img1.TabIndex = 37;
            this.Img1.TabStop = false;
            // 
            // chkManual
            // 
            this.chkManual.AutoSize = true;
            this.chkManual.Location = new System.Drawing.Point(282, 21);
            this.chkManual.Name = "chkManual";
            this.chkManual.Size = new System.Drawing.Size(131, 17);
            this.chkManual.TabIndex = 43;
            this.chkManual.Text = "Informar Manualmente";
            this.chkManual.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(354, 334);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(207, 53);
            this.webBrowser1.TabIndex = 33;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Location = new System.Drawing.Point(356, 389);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(205, 49);
            this.webBrowser2.TabIndex = 34;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 205);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(582, 288);
            this.tabControl1.TabIndex = 36;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.webBrowser2);
            this.tabPage1.Controls.Add(this.chkManual);
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Controls.Add(this.Web1);
            this.tabPage1.Controls.Add(this.ButBaixar2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(574, 262);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Importação Automatica";
            // 
            // ButBaixar2
            // 
            this.ButBaixar2.Location = new System.Drawing.Point(429, 15);
            this.ButBaixar2.Name = "ButBaixar2";
            this.ButBaixar2.Size = new System.Drawing.Size(131, 23);
            this.ButBaixar2.TabIndex = 45;
            this.ButBaixar2.Text = "Iniciar/Parar";
            this.ButBaixar2.UseVisualStyleBackColor = true;
            this.ButBaixar2.Click += new System.EventHandler(this.ButBaixar2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.lblContador);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnIncluirChave);
            this.tabPage2.Controls.Add(this.txtIncluirChave);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(574, 262);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Incluir Chaves";
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Location = new System.Drawing.Point(16, 72);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(0, 13);
            this.lblContador.TabIndex = 46;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Processar Agora ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnIncluirChave
            // 
            this.btnIncluirChave.Location = new System.Drawing.Point(354, 39);
            this.btnIncluirChave.Name = "btnIncluirChave";
            this.btnIncluirChave.Size = new System.Drawing.Size(75, 23);
            this.btnIncluirChave.TabIndex = 2;
            this.btnIncluirChave.Text = "Incluir Chave";
            this.btnIncluirChave.UseVisualStyleBackColor = true;
            this.btnIncluirChave.Click += new System.EventHandler(this.btnIncluirChave_Click);
            // 
            // txtIncluirChave
            // 
            this.txtIncluirChave.Location = new System.Drawing.Point(19, 39);
            this.txtIncluirChave.MaxLength = 44;
            this.txtIncluirChave.Name = "txtIncluirChave";
            this.txtIncluirChave.Size = new System.Drawing.Size(329, 20);
            this.txtIncluirChave.TabIndex = 1;
            this.txtIncluirChave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIncluirChave_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chave";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(574, 262);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Lotes Pendentes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(568, 256);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnExcluirLote);
            this.tabPage4.Controls.Add(this.grdLotesPendentes);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(574, 262);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Lotes Pendentes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // grdLotesPendentes
            // 
            this.grdLotesPendentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLotesPendentes.Location = new System.Drawing.Point(3, 3);
            this.grdLotesPendentes.Name = "grdLotesPendentes";
            this.grdLotesPendentes.Size = new System.Drawing.Size(568, 227);
            this.grdLotesPendentes.TabIndex = 1;
            this.grdLotesPendentes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdLotesPendentes_CellClick);
            // 
            // btnExcluirLote
            // 
            this.btnExcluirLote.Location = new System.Drawing.Point(496, 236);
            this.btnExcluirLote.Name = "btnExcluirLote";
            this.btnExcluirLote.Size = new System.Drawing.Size(75, 23);
            this.btnExcluirLote.TabIndex = 2;
            this.btnExcluirLote.Text = "Excluir Lote";
            this.btnExcluirLote.UseVisualStyleBackColor = true;
            this.btnExcluirLote.Click += new System.EventHandler(this.btnExcluirLote_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 503);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baixar XML (v1.2 - 22/06/2017)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Img1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLotesPendentes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser Web1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnIncluirChave;
        private System.Windows.Forms.TextBox txtIncluirChave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox EditChave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EditCaptcha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox Img1;
        private System.Windows.Forms.Button ButBaixar2;
        private System.Windows.Forms.CheckBox chkManual;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtLoteProcessamento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnExcluirLote;
        private System.Windows.Forms.DataGridView grdLotesPendentes;
    }
}

