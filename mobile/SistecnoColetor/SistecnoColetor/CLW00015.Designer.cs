namespace SistecnoColetor
{
    partial class CLW00015
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
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Itens = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle4 = new System.Windows.Forms.DataGridTableStyle();
            this.IdRomaneio = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Emissao = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Doca = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Observacao = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Conf = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.lblIdConferenciaPallet = new System.Windows.Forms.Label();
            this.lblIdProdutoEmbalagem = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigoDeBarras = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Lanc = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.grdLancamentos = new System.Windows.Forms.DataGrid();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.UA = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CODIGODEBARRAS = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QUANTIDADE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.IDCONFERENCIAPALLETPRODUTO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Divergencias = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.grdDivergencias = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle2 = new System.Windows.Forms.DataGridTableStyle();
            this.Prod = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dESC = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QTDSEPARADA = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QTDCONFEREIDA = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QTDBASE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.Itens.SuspendLayout();
            this.Conf.SuspendLayout();
            this.Lanc.SuspendLayout();
            this.Divergencias.SuspendLayout();
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
            this.lblTitulo.Location = new System.Drawing.Point(1, -1);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Itens);
            this.tabControl1.Controls.Add(this.Conf);
            this.tabControl1.Controls.Add(this.Lanc);
            this.tabControl1.Controls.Add(this.Divergencias);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.None;
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 253);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Itens
            // 
            this.Itens.Controls.Add(this.label5);
            this.Itens.Controls.Add(this.lblObservacao);
            this.Itens.Controls.Add(this.dataGrid1);
            this.Itens.Location = new System.Drawing.Point(0, 0);
            this.Itens.Name = "Itens";
            this.Itens.Size = new System.Drawing.Size(234, 230);
            this.Itens.Text = "Itens";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(8, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 16);
            this.label5.Text = "Romaneio Para Conferencia";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblObservacao
            // 
            this.lblObservacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblObservacao.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblObservacao.Location = new System.Drawing.Point(8, 188);
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Size = new System.Drawing.Size(219, 38);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(8, 21);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(221, 161);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle4);
            this.dataGrid1.DoubleClick += new System.EventHandler(this.dataGrid1_DoubleClick);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // dataGridTableStyle4
            // 
            this.dataGridTableStyle4.GridColumnStyles.Add(this.IdRomaneio);
            this.dataGridTableStyle4.GridColumnStyles.Add(this.Emissao);
            this.dataGridTableStyle4.GridColumnStyles.Add(this.Doca);
            this.dataGridTableStyle4.GridColumnStyles.Add(this.Observacao);
            // 
            // IdRomaneio
            // 
            this.IdRomaneio.Format = "";
            this.IdRomaneio.FormatInfo = null;
            this.IdRomaneio.HeaderText = "Romaneio";
            this.IdRomaneio.MappingName = "IdRomaneio";
            this.IdRomaneio.Width = 70;
            // 
            // Emissao
            // 
            this.Emissao.Format = "";
            this.Emissao.FormatInfo = null;
            this.Emissao.HeaderText = "Emissão";
            this.Emissao.MappingName = "Emissao";
            // 
            // Doca
            // 
            this.Doca.Format = "";
            this.Doca.FormatInfo = null;
            this.Doca.HeaderText = "Doca";
            this.Doca.MappingName = "Doca";
            this.Doca.Width = 70;
            // 
            // Observacao
            // 
            this.Observacao.Format = "";
            this.Observacao.FormatInfo = null;
            this.Observacao.HeaderText = "Observação";
            this.Observacao.MappingName = "Observacao";
            this.Observacao.Width = 0;
            // 
            // Conf
            // 
            this.Conf.Controls.Add(this.label6);
            this.Conf.Controls.Add(this.lblIdConferenciaPallet);
            this.Conf.Controls.Add(this.lblIdProdutoEmbalagem);
            this.Conf.Controls.Add(this.btnConfirmar);
            this.Conf.Controls.Add(this.txtQuantidade);
            this.Conf.Controls.Add(this.label3);
            this.Conf.Controls.Add(this.txtCodigoDeBarras);
            this.Conf.Controls.Add(this.label1);
            this.Conf.Controls.Add(this.txtUa);
            this.Conf.Controls.Add(this.label2);
            this.Conf.Location = new System.Drawing.Point(0, 0);
            this.Conf.Name = "Conf";
            this.Conf.Size = new System.Drawing.Size(226, 227);
            this.Conf.Text = "Conf.";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(6, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(219, 16);
            this.label6.Text = "Conferindo Itens";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblIdConferenciaPallet
            // 
            this.lblIdConferenciaPallet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblIdConferenciaPallet.Location = new System.Drawing.Point(7, 201);
            this.lblIdConferenciaPallet.Name = "lblIdConferenciaPallet";
            this.lblIdConferenciaPallet.Size = new System.Drawing.Size(100, 20);
            this.lblIdConferenciaPallet.Visible = false;
            // 
            // lblIdProdutoEmbalagem
            // 
            this.lblIdProdutoEmbalagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblIdProdutoEmbalagem.Location = new System.Drawing.Point(7, 179);
            this.lblIdProdutoEmbalagem.Name = "lblIdProdutoEmbalagem";
            this.lblIdProdutoEmbalagem.Size = new System.Drawing.Size(100, 20);
            this.lblIdProdutoEmbalagem.Visible = false;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(148, 203);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(83, 24);
            this.btnConfirmar.TabIndex = 28;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Enabled = false;
            this.txtQuantidade.Location = new System.Drawing.Point(5, 148);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(225, 21);
            this.txtQuantidade.TabIndex = 26;
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(5, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 20);
            this.label3.Text = "Quantidade";
            // 
            // txtCodigoDeBarras
            // 
            this.txtCodigoDeBarras.Enabled = false;
            this.txtCodigoDeBarras.Location = new System.Drawing.Point(6, 96);
            this.txtCodigoDeBarras.Name = "txtCodigoDeBarras";
            this.txtCodigoDeBarras.Size = new System.Drawing.Size(225, 21);
            this.txtCodigoDeBarras.TabIndex = 22;
            this.txtCodigoDeBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoDeBarras_KeyPress_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 20);
            this.label1.Text = "Código de Barras do Produto:";
            // 
            // txtUa
            // 
            this.txtUa.Location = new System.Drawing.Point(6, 53);
            this.txtUa.Name = "txtUa";
            this.txtUa.Size = new System.Drawing.Size(225, 21);
            this.txtUa.TabIndex = 21;
            this.txtUa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUa_KeyPress_1);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.Text = "UA:";
            // 
            // Lanc
            // 
            this.Lanc.Controls.Add(this.label7);
            this.Lanc.Controls.Add(this.grdLancamentos);
            this.Lanc.Location = new System.Drawing.Point(0, 0);
            this.Lanc.Name = "Lanc";
            this.Lanc.Size = new System.Drawing.Size(234, 230);
            this.Lanc.Text = "Lanc.";
            this.Lanc.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(7, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(219, 16);
            this.label7.Text = "Itens Conferidos";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grdLancamentos
            // 
            this.grdLancamentos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdLancamentos.ContextMenu = this.contextMenu1;
            this.grdLancamentos.Location = new System.Drawing.Point(4, 28);
            this.grdLancamentos.Name = "grdLancamentos";
            this.grdLancamentos.Size = new System.Drawing.Size(226, 195);
            this.grdLancamentos.TabIndex = 0;
            this.grdLancamentos.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.Add(this.menuItem1);
            this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
            // 
            // menuItem1
            // 
            this.menuItem1.Checked = true;
            this.menuItem1.Text = "Excluir Lancamento";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.UA);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.CODIGODEBARRAS);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.QUANTIDADE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.IDCONFERENCIAPALLETPRODUTO);
            // 
            // UA
            // 
            this.UA.Format = "";
            this.UA.FormatInfo = null;
            this.UA.HeaderText = "UA";
            this.UA.MappingName = "UA";
            // 
            // CODIGODEBARRAS
            // 
            this.CODIGODEBARRAS.Format = "";
            this.CODIGODEBARRAS.FormatInfo = null;
            this.CODIGODEBARRAS.HeaderText = "CODIGO DE BARRAS";
            this.CODIGODEBARRAS.MappingName = "CODIGO DE BARRAS";
            this.CODIGODEBARRAS.Width = 100;
            // 
            // QUANTIDADE
            // 
            this.QUANTIDADE.Format = "";
            this.QUANTIDADE.FormatInfo = null;
            this.QUANTIDADE.HeaderText = "QUANTIDADE";
            this.QUANTIDADE.MappingName = "QUANTIDADE";
            // 
            // IDCONFERENCIAPALLETPRODUTO
            // 
            this.IDCONFERENCIAPALLETPRODUTO.Format = "";
            this.IDCONFERENCIAPALLETPRODUTO.FormatInfo = null;
            this.IDCONFERENCIAPALLETPRODUTO.HeaderText = "IDCONFERENCIAPALLETPRODUTO";
            this.IDCONFERENCIAPALLETPRODUTO.MappingName = "IDCONFERENCIAPALLETPRODUTO";
            this.IDCONFERENCIAPALLETPRODUTO.Width = 0;
            // 
            // Divergencias
            // 
            this.Divergencias.Controls.Add(this.button1);
            this.Divergencias.Controls.Add(this.label8);
            this.Divergencias.Controls.Add(this.grdDivergencias);
            this.Divergencias.Location = new System.Drawing.Point(0, 0);
            this.Divergencias.Name = "Divergencias";
            this.Divergencias.Size = new System.Drawing.Size(226, 227);
            this.Divergencias.Text = "Diverg.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 20);
            this.button1.TabIndex = 3;
            this.button1.Text = "Fechar Conferencia";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(8, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(219, 16);
            this.label8.Text = "Ajustar Divergências";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grdDivergencias
            // 
            this.grdDivergencias.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdDivergencias.Location = new System.Drawing.Point(4, 25);
            this.grdDivergencias.Name = "grdDivergencias";
            this.grdDivergencias.Size = new System.Drawing.Size(226, 160);
            this.grdDivergencias.TabIndex = 0;
            this.grdDivergencias.TableStyles.Add(this.dataGridTableStyle2);
            // 
            // dataGridTableStyle2
            // 
            this.dataGridTableStyle2.GridColumnStyles.Add(this.Prod);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.dESC);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.QTDSEPARADA);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.QTDCONFEREIDA);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.QTDBASE);
            // 
            // Prod
            // 
            this.Prod.Format = "";
            this.Prod.FormatInfo = null;
            this.Prod.HeaderText = "COD. PROD.";
            this.Prod.MappingName = "CODIGO";
            this.Prod.Width = 100;
            // 
            // dESC
            // 
            this.dESC.Format = "";
            this.dESC.FormatInfo = null;
            this.dESC.HeaderText = "DESCRIÇÃO";
            this.dESC.MappingName = "PRODUTO";
            this.dESC.Width = 350;
            // 
            // QTDSEPARADA
            // 
            this.QTDSEPARADA.Format = "";
            this.QTDSEPARADA.FormatInfo = null;
            this.QTDSEPARADA.HeaderText = "SEPARAÇÃO";
            this.QTDSEPARADA.MappingName = "QTDSEPARADA";
            this.QTDSEPARADA.Width = 0;
            // 
            // QTDCONFEREIDA
            // 
            this.QTDCONFEREIDA.Format = "";
            this.QTDCONFEREIDA.FormatInfo = null;
            this.QTDCONFEREIDA.HeaderText = "CONF";
            this.QTDCONFEREIDA.MappingName = "QTDCONFEREIDA";
            this.QTDCONFEREIDA.Width = 0;
            // 
            // QTDBASE
            // 
            this.QTDBASE.Format = "";
            this.QTDBASE.FormatInfo = null;
            this.QTDBASE.HeaderText = "SOLIC.";
            this.QTDBASE.MappingName = "QTDBASE";
            this.QTDBASE.Width = 0;
            // 
            // CLW00015
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.statusBar1);
            this.Name = "CLW00015";
            this.Text = "CLW00015";
            this.Load += new System.EventHandler(this.CLW00015_Load);
            this.tabControl1.ResumeLayout(false);
            this.Itens.ResumeLayout(false);
            this.Conf.ResumeLayout(false);
            this.Lanc.ResumeLayout(false);
            this.Divergencias.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Itens;
        private System.Windows.Forms.TabPage Conf;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle4;
        private System.Windows.Forms.DataGridTextBoxColumn IdRomaneio;
        private System.Windows.Forms.DataGridTextBoxColumn Emissao;
        private System.Windows.Forms.DataGridTextBoxColumn Doca;
        private System.Windows.Forms.DataGridTextBoxColumn Observacao;
        private System.Windows.Forms.TabPage Lanc;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigoDeBarras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label lblIdProdutoEmbalagem;
        private System.Windows.Forms.Label lblIdConferenciaPallet;
        private System.Windows.Forms.DataGrid grdLancamentos;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn UA;
        private System.Windows.Forms.DataGridTextBoxColumn CODIGODEBARRAS;
        private System.Windows.Forms.DataGridTextBoxColumn QUANTIDADE;
        private System.Windows.Forms.DataGridTextBoxColumn IDCONFERENCIAPALLETPRODUTO;
        private System.Windows.Forms.TabPage Divergencias;
        private System.Windows.Forms.DataGrid grdDivergencias;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle2;
        private System.Windows.Forms.DataGridTextBoxColumn QTDSEPARADA;
        private System.Windows.Forms.DataGridTextBoxColumn QTDCONFEREIDA;
        private System.Windows.Forms.DataGridTextBoxColumn Prod;
        private System.Windows.Forms.DataGridTextBoxColumn dESC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridTextBoxColumn QTDBASE;

    }
}