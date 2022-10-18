namespace RsMobile
{
    partial class ListarStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListarStatus));
            this.btnPendentes = new System.Windows.Forms.Button();
            this.btnATransmitir = new System.Windows.Forms.Button();
            this.btnEfetuados = new System.Windows.Forms.Button();
            this.pnlBotoes = new System.Windows.Forms.Panel();
            this.btnAlterarDt = new System.Windows.Forms.Button();
            this.grd = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.Numero = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Remetente = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Destinatario = new System.Windows.Forms.DataGridTextBoxColumn();
            this.IDDocumento = new System.Windows.Forms.DataGridTextBoxColumn();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.btnVoltarListaStatus = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.tmEnvio = new System.Windows.Forms.Timer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblStatusEnvio = new System.Windows.Forms.Label();
            this.pnlBotoes.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPendentes
            // 
            this.btnPendentes.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPendentes.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnPendentes.Location = new System.Drawing.Point(3, 3);
            this.btnPendentes.Name = "btnPendentes";
            this.btnPendentes.Size = new System.Drawing.Size(228, 38);
            this.btnPendentes.TabIndex = 0;
            this.btnPendentes.Text = "Pendentes";
            this.btnPendentes.Click += new System.EventHandler(this.btnPendentes_Click);
            // 
            // btnATransmitir
            // 
            this.btnATransmitir.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnATransmitir.Enabled = false;
            this.btnATransmitir.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnATransmitir.Location = new System.Drawing.Point(3, 61);
            this.btnATransmitir.Name = "btnATransmitir";
            this.btnATransmitir.Size = new System.Drawing.Size(228, 38);
            this.btnATransmitir.TabIndex = 1;
            this.btnATransmitir.Text = "A Transferir";
            this.btnATransmitir.Click += new System.EventHandler(this.btnATransmitir_Click);
            // 
            // btnEfetuados
            // 
            this.btnEfetuados.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEfetuados.Enabled = false;
            this.btnEfetuados.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnEfetuados.Location = new System.Drawing.Point(3, 120);
            this.btnEfetuados.Name = "btnEfetuados";
            this.btnEfetuados.Size = new System.Drawing.Size(228, 38);
            this.btnEfetuados.TabIndex = 2;
            this.btnEfetuados.Text = "Feito";
            this.btnEfetuados.Click += new System.EventHandler(this.btnEfetuados_Click);
            // 
            // pnlBotoes
            // 
            this.pnlBotoes.Controls.Add(this.btnAlterarDt);
            this.pnlBotoes.Controls.Add(this.btnPendentes);
            this.pnlBotoes.Controls.Add(this.btnATransmitir);
            this.pnlBotoes.Controls.Add(this.btnEfetuados);
            this.pnlBotoes.Location = new System.Drawing.Point(2, 57);
            this.pnlBotoes.Name = "pnlBotoes";
            this.pnlBotoes.Size = new System.Drawing.Size(234, 231);
            // 
            // btnAlterarDt
            // 
            this.btnAlterarDt.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAlterarDt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAlterarDt.Location = new System.Drawing.Point(136, 201);
            this.btnAlterarDt.Name = "btnAlterarDt";
            this.btnAlterarDt.Size = new System.Drawing.Size(94, 27);
            this.btnAlterarDt.TabIndex = 3;
            this.btnAlterarDt.Text = "VOLTAR";
            this.btnAlterarDt.Click += new System.EventHandler(this.btnAlterarDt_Click);
            // 
            // grd
            // 
            this.grd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grd.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.grd.Location = new System.Drawing.Point(3, 0);
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(231, 219);
            this.grd.TabIndex = 7;
            this.grd.TableStyles.Add(this.dataGridTableStyle1);
            this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
            this.grd.CurrentCellChanged += new System.EventHandler(this.grd_CurrentCellChanged);
            this.grd.Click += new System.EventHandler(this.grd_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Numero);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Remetente);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Destinatario);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.IDDocumento);
            // 
            // Numero
            // 
            this.Numero.Format = "";
            this.Numero.FormatInfo = null;
            this.Numero.HeaderText = "Documento";
            this.Numero.MappingName = "NUMERODOCUMENTO";
            this.Numero.Width = 60;
            // 
            // Remetente
            // 
            this.Remetente.Format = "";
            this.Remetente.FormatInfo = null;
            this.Remetente.HeaderText = "Remetente";
            this.Remetente.MappingName = "Remetente";
            this.Remetente.Width = 150;
            // 
            // Destinatario
            // 
            this.Destinatario.Format = "";
            this.Destinatario.FormatInfo = null;
            this.Destinatario.HeaderText = "Destinátario";
            this.Destinatario.MappingName = "Destinatario";
            this.Destinatario.Width = 150;
            // 
            // IDDocumento
            // 
            this.IDDocumento.Format = "";
            this.IDDocumento.FormatInfo = null;
            this.IDDocumento.HeaderText = "IdDocumento";
            this.IDDocumento.MappingName = "IdDocumento";
            this.IDDocumento.Width = 1;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.btnVoltarListaStatus);
            this.pnlGrid.Controls.Add(this.grd);
            this.pnlGrid.Location = new System.Drawing.Point(2, 48);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(235, 240);
            this.pnlGrid.Visible = false;
            // 
            // btnVoltarListaStatus
            // 
            this.btnVoltarListaStatus.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnVoltarListaStatus.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btnVoltarListaStatus.ForeColor = System.Drawing.Color.Red;
            this.btnVoltarListaStatus.Location = new System.Drawing.Point(130, 220);
            this.btnVoltarListaStatus.Name = "btnVoltarListaStatus";
            this.btnVoltarListaStatus.Size = new System.Drawing.Size(103, 20);
            this.btnVoltarListaStatus.TabIndex = 8;
            this.btnVoltarListaStatus.Text = "VOLTAR";
            this.btnVoltarListaStatus.Click += new System.EventHandler(this.btnVoltarListaStatus_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // tmEnvio
            // 
            this.tmEnvio.Interval = 300000;
            this.tmEnvio.Tick += new System.EventHandler(this.tmEnvio_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblStatusEnvio
            // 
            this.lblStatusEnvio.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lblStatusEnvio.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblStatusEnvio.Location = new System.Drawing.Point(109, 3);
            this.lblStatusEnvio.Name = "lblStatusEnvio";
            this.lblStatusEnvio.Size = new System.Drawing.Size(123, 38);
            // 
            // ListarStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblStatusEnvio);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlBotoes);
            this.Name = "ListarStatus";
            this.Text = "ListarStatus";
            this.Deactivate += new System.EventHandler(this.ListarStatus_Deactivate);
            this.Load += new System.EventHandler(this.ListarStatus_Load);
            this.Closed += new System.EventHandler(this.ListarStatus_Closed);
            this.Activated += new System.EventHandler(this.ListarStatus_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ListarStatus_Closing);
            this.pnlBotoes.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPendentes;
        private System.Windows.Forms.Button btnATransmitir;
        private System.Windows.Forms.Button btnEfetuados;
        private System.Windows.Forms.Panel pnlBotoes;
        private System.Windows.Forms.DataGrid grd;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn Remetente;
        private System.Windows.Forms.DataGridTextBoxColumn IDDocumento;
        private System.Windows.Forms.DataGridTextBoxColumn Numero;
        private System.Windows.Forms.DataGridTextBoxColumn Destinatario;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Button btnVoltarListaStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tmEnvio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAlterarDt;
        private System.Windows.Forms.Label lblStatusEnvio;


    }
}