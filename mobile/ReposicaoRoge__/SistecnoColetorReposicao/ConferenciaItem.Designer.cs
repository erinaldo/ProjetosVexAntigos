namespace SistecnoColetor
{
    partial class ConferenciaItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConferenciaItem));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlConfere = new System.Windows.Forms.Panel();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.CODIGODEBARRAS = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QUANTIDADE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirmarVolume = new System.Windows.Forms.Button();
            this.txtCbVolume = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDiferencas = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnConfirmarEEnviar = new System.Windows.Forms.Button();
            this.grdConsolidar = new System.Windows.Forms.DataGrid();
            this.DGS = new System.Windows.Forms.DataGridTableStyle();
            this.CODIGODEBARRAS1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.FALTA = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VLUNI = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridTextBoxColumn();
            this.pnlConfere.SuspendLayout();
            this.pnlDiferencas.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // pnlConfere
            // 
            this.pnlConfere.Controls.Add(this.dataGrid1);
            this.pnlConfere.Controls.Add(this.txtQuantidade);
            this.pnlConfere.Controls.Add(this.label2);
            this.pnlConfere.Controls.Add(this.btnConfirmarVolume);
            this.pnlConfere.Controls.Add(this.txtCbVolume);
            this.pnlConfere.Controls.Add(this.label1);
            this.pnlConfere.Location = new System.Drawing.Point(4, 22);
            this.pnlConfere.Name = "pnlConfere";
            this.pnlConfere.Size = new System.Drawing.Size(230, 246);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(6, 48);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(219, 168);
            this.dataGrid1.TabIndex = 42;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.CODIGODEBARRAS);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.QUANTIDADE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.ID);
            // 
            // CODIGODEBARRAS
            // 
            this.CODIGODEBARRAS.Format = "";
            this.CODIGODEBARRAS.FormatInfo = null;
            this.CODIGODEBARRAS.HeaderText = "CÓD. BARRAS";
            this.CODIGODEBARRAS.MappingName = "CODIGODEBARRAS";
            this.CODIGODEBARRAS.Width = 100;
            // 
            // QUANTIDADE
            // 
            this.QUANTIDADE.Format = "";
            this.QUANTIDADE.FormatInfo = null;
            this.QUANTIDADE.HeaderText = "QUANTIDADE";
            this.QUANTIDADE.MappingName = "QUANTIDADE";
            // 
            // ID
            // 
            this.ID.Format = "";
            this.ID.FormatInfo = null;
            this.ID.HeaderText = "CÓDIGO";
            this.ID.MappingName = "ID";
            this.ID.Width = 0;
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Location = new System.Drawing.Point(163, 22);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(60, 21);
            this.txtQuantidade.TabIndex = 41;
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(164, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 18);
            this.label2.Text = "Quant.:";
            // 
            // btnConfirmarVolume
            // 
            this.btnConfirmarVolume.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmarVolume.Location = new System.Drawing.Point(8, 219);
            this.btnConfirmarVolume.Name = "btnConfirmarVolume";
            this.btnConfirmarVolume.Size = new System.Drawing.Size(217, 24);
            this.btnConfirmarVolume.TabIndex = 28;
            this.btnConfirmarVolume.Text = "Apurar Diferenças";
            this.btnConfirmarVolume.Click += new System.EventHandler(this.btnConfirmarVolume_Click_1);
            // 
            // txtCbVolume
            // 
            this.txtCbVolume.Location = new System.Drawing.Point(8, 22);
            this.txtCbVolume.Name = "txtCbVolume";
            this.txtCbVolume.Size = new System.Drawing.Size(149, 21);
            this.txtCbVolume.TabIndex = 40;
            this.txtCbVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCbVolume_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 14);
            this.label1.Text = "Codigo de Barras:";
            // 
            // pnlDiferencas
            // 
            this.pnlDiferencas.Controls.Add(this.btnVoltar);
            this.pnlDiferencas.Controls.Add(this.btnConfirmarEEnviar);
            this.pnlDiferencas.Controls.Add(this.grdConsolidar);
            this.pnlDiferencas.Location = new System.Drawing.Point(2, 16);
            this.pnlDiferencas.Name = "pnlDiferencas";
            this.pnlDiferencas.Size = new System.Drawing.Size(232, 256);
            this.pnlDiferencas.Visible = false;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnVoltar.Location = new System.Drawing.Point(8, 229);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(92, 24);
            this.btnVoltar.TabIndex = 45;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnConfirmarEEnviar
            // 
            this.btnConfirmarEEnviar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmarEEnviar.Location = new System.Drawing.Point(136, 229);
            this.btnConfirmarEEnviar.Name = "btnConfirmarEEnviar";
            this.btnConfirmarEEnviar.Size = new System.Drawing.Size(89, 24);
            this.btnConfirmarEEnviar.TabIndex = 44;
            this.btnConfirmarEEnviar.Text = "Confirmar";
            this.btnConfirmarEEnviar.Click += new System.EventHandler(this.btnConfirmarEEnviar_Click);
            // 
            // grdConsolidar
            // 
            this.grdConsolidar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdConsolidar.Location = new System.Drawing.Point(6, 7);
            this.grdConsolidar.Name = "grdConsolidar";
            this.grdConsolidar.Size = new System.Drawing.Size(220, 215);
            this.grdConsolidar.TabIndex = 43;
            this.grdConsolidar.TableStyles.Add(this.DGS);
            // 
            // DGS
            // 
            this.DGS.GridColumnStyles.Add(this.CODIGODEBARRAS1);
            this.DGS.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.DGS.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.DGS.GridColumnStyles.Add(this.FALTA);
            this.DGS.GridColumnStyles.Add(this.VLUNI);
            this.DGS.GridColumnStyles.Add(this.TOTAL);
            // 
            // CODIGODEBARRAS1
            // 
            this.CODIGODEBARRAS1.Format = "";
            this.CODIGODEBARRAS1.FormatInfo = null;
            this.CODIGODEBARRAS1.HeaderText = "CÓD. BARRAS";
            this.CODIGODEBARRAS1.MappingName = "CODIGODEBARRAS";
            this.CODIGODEBARRAS1.Width = 100;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "CONFERIDO";
            this.dataGridTextBoxColumn3.MappingName = "QUANTIDADEAFERIDA";
            this.dataGridTextBoxColumn3.Width = 100;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "QUANTIDADE";
            this.dataGridTextBoxColumn2.MappingName = "QUANTIDADE";
            this.dataGridTextBoxColumn2.Width = 0;
            // 
            // FALTA
            // 
            this.FALTA.Format = "";
            this.FALTA.FormatInfo = null;
            this.FALTA.HeaderText = "FALTA";
            this.FALTA.MappingName = "FALTA";
            this.FALTA.Width = 0;
            // 
            // VLUNI
            // 
            this.VLUNI.Format = "";
            this.VLUNI.FormatInfo = null;
            this.VLUNI.HeaderText = "VL. UNIT.";
            this.VLUNI.MappingName = "VALORUNITARIO";
            this.VLUNI.Width = 0;
            // 
            // TOTAL
            // 
            this.TOTAL.Format = "";
            this.TOTAL.FormatInfo = null;
            this.TOTAL.HeaderText = "TOTAL";
            this.TOTAL.MappingName = "TOTALITEM";
            this.TOTAL.Width = 0;
            // 
            // ConferenciaItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.pnlConfere);
            this.Controls.Add(this.pnlDiferencas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConferenciaItem";
            this.Text = "Conferencia De Itens";
            this.Load += new System.EventHandler(this.ConfVolume_Load);
            this.pnlConfere.ResumeLayout(false);
            this.pnlDiferencas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlConfere;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCbVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlDiferencas;
        private System.Windows.Forms.DataGrid grdConsolidar;
        private System.Windows.Forms.Button btnConfirmarVolume;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnConfirmarEEnviar;
        private System.Windows.Forms.DataGridTextBoxColumn ID;
        private System.Windows.Forms.DataGridTextBoxColumn CODIGODEBARRAS;
        private System.Windows.Forms.DataGridTextBoxColumn QUANTIDADE;
        private System.Windows.Forms.DataGridTableStyle DGS;
        private System.Windows.Forms.DataGridTextBoxColumn CODIGODEBARRAS1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn FALTA;
        private System.Windows.Forms.DataGridTextBoxColumn VLUNI;
        private System.Windows.Forms.DataGridTextBoxColumn TOTAL;
    }
}