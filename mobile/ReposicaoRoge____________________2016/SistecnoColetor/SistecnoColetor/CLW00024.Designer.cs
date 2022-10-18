namespace SistecnoColetor
{
    partial class CLW00024
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlNovoRomaneio = new System.Windows.Forms.Panel();
            this.chkDevolucao = new System.Windows.Forms.CheckBox();
            this.btnConfirmarRomaneio = new System.Windows.Forms.Button();
            this.dataGrid2 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle2 = new System.Windows.Forms.DataGridTableStyle();
            this.Chave = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.txtNovoRomanioChave = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.ROMANEIO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnNovoRomaneio = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblSolicitaValidade = new System.Windows.Forms.Label();
            this.lblSolicitaLote = new System.Windows.Forms.Label();
            this.lblIdProdutoEmbalagem = new System.Windows.Forms.Label();
            this.lblIdProdutoCliente = new System.Windows.Forms.Label();
            this.txtValidade = new System.Windows.Forms.TextBox();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.txtExecCodigoDeBarras = new System.Windows.Forms.TextBox();
            this.txtExecUa = new System.Windows.Forms.TextBox();
            this.btnConfirmarExecucao = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grdLancamentos = new System.Windows.Forms.DataGrid();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.dataGridTableStyle6 = new System.Windows.Forms.DataGridTableStyle();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.grdDivergencias = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle3 = new System.Windows.Forms.DataGridTableStyle();
            this.cb = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QUANTIDADEBASE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QUANTIDADELIDO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlNovoRomaneio.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(238, 16);
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.None;
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 252);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGrid1);
            this.tabPage1.Controls.Add(this.btnNovoRomaneio);
            this.tabPage1.Controls.Add(this.pnlNovoRomaneio);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(234, 229);
            this.tabPage1.Text = "Romaneios";
            // 
            // pnlNovoRomaneio
            // 
            this.pnlNovoRomaneio.Controls.Add(this.button1);
            this.pnlNovoRomaneio.Controls.Add(this.chkDevolucao);
            this.pnlNovoRomaneio.Controls.Add(this.btnConfirmarRomaneio);
            this.pnlNovoRomaneio.Controls.Add(this.dataGrid2);
            this.pnlNovoRomaneio.Controls.Add(this.txtNovoRomanioChave);
            this.pnlNovoRomaneio.Controls.Add(this.label2);
            this.pnlNovoRomaneio.Controls.Add(this.label1);
            this.pnlNovoRomaneio.Location = new System.Drawing.Point(3, 0);
            this.pnlNovoRomaneio.Name = "pnlNovoRomaneio";
            this.pnlNovoRomaneio.Size = new System.Drawing.Size(228, 226);
            this.pnlNovoRomaneio.Visible = false;
            // 
            // chkDevolucao
            // 
            this.chkDevolucao.Location = new System.Drawing.Point(1, 24);
            this.chkDevolucao.Name = "chkDevolucao";
            this.chkDevolucao.Size = new System.Drawing.Size(100, 20);
            this.chkDevolucao.TabIndex = 9;
            this.chkDevolucao.Text = "Devolução";
            // 
            // btnConfirmarRomaneio
            // 
            this.btnConfirmarRomaneio.Location = new System.Drawing.Point(87, 204);
            this.btnConfirmarRomaneio.Name = "btnConfirmarRomaneio";
            this.btnConfirmarRomaneio.Size = new System.Drawing.Size(137, 20);
            this.btnConfirmarRomaneio.TabIndex = 6;
            this.btnConfirmarRomaneio.Text = "Confirmar Romaneio";
            this.btnConfirmarRomaneio.Click += new System.EventHandler(this.btnConfirmarRomaneio_Click);
            // 
            // dataGrid2
            // 
            this.dataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2.Location = new System.Drawing.Point(4, 92);
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.Size = new System.Drawing.Size(220, 105);
            this.dataGrid2.TabIndex = 4;
            this.dataGrid2.TableStyles.Add(this.dataGridTableStyle2);
            // 
            // dataGridTableStyle2
            // 
            this.dataGridTableStyle2.GridColumnStyles.Add(this.Chave);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            // 
            // Chave
            // 
            this.Chave.Format = "";
            this.Chave.FormatInfo = null;
            this.Chave.HeaderText = "CHAVE";
            this.Chave.MappingName = "CHAVE";
            this.Chave.Width = 200;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "IDDOCUMENTO";
            this.dataGridTextBoxColumn1.MappingName = "IDDOCUMENTO";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // txtNovoRomanioChave
            // 
            this.txtNovoRomanioChave.Location = new System.Drawing.Point(4, 66);
            this.txtNovoRomanioChave.Name = "txtNovoRomanioChave";
            this.txtNovoRomanioChave.Size = new System.Drawing.Size(220, 21);
            this.txtNovoRomanioChave.TabIndex = 3;
            this.txtNovoRomanioChave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNovoRomanioChave_KeyPress);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.Text = "Chave:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(125, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.Text = "Novo Romaneio";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(5, 3);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(225, 199);
            this.dataGrid1.TabIndex = 7;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.ROMANEIO);
            // 
            // ROMANEIO
            // 
            this.ROMANEIO.Format = "";
            this.ROMANEIO.FormatInfo = null;
            this.ROMANEIO.HeaderText = "ROMANEIO";
            this.ROMANEIO.MappingName = "IDROMANEIO";
            this.ROMANEIO.NullText = "-";
            this.ROMANEIO.Width = 250;
            // 
            // btnNovoRomaneio
            // 
            this.btnNovoRomaneio.Location = new System.Drawing.Point(107, 205);
            this.btnNovoRomaneio.Name = "btnNovoRomaneio";
            this.btnNovoRomaneio.Size = new System.Drawing.Size(122, 20);
            this.btnNovoRomaneio.TabIndex = 0;
            this.btnNovoRomaneio.Text = "Novo Romaneio";
            this.btnNovoRomaneio.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblSolicitaValidade);
            this.tabPage2.Controls.Add(this.lblSolicitaLote);
            this.tabPage2.Controls.Add(this.lblIdProdutoEmbalagem);
            this.tabPage2.Controls.Add(this.lblIdProdutoCliente);
            this.tabPage2.Controls.Add(this.txtValidade);
            this.tabPage2.Controls.Add(this.txtLote);
            this.tabPage2.Controls.Add(this.txtQuantidade);
            this.tabPage2.Controls.Add(this.txtExecCodigoDeBarras);
            this.tabPage2.Controls.Add(this.txtExecUa);
            this.tabPage2.Controls.Add(this.btnConfirmarExecucao);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(234, 229);
            this.tabPage2.Text = "Exec.";
            // 
            // lblSolicitaValidade
            // 
            this.lblSolicitaValidade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblSolicitaValidade.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSolicitaValidade.Location = new System.Drawing.Point(93, 5);
            this.lblSolicitaValidade.Name = "lblSolicitaValidade";
            this.lblSolicitaValidade.Size = new System.Drawing.Size(39, 15);
            this.lblSolicitaValidade.Visible = false;
            // 
            // lblSolicitaLote
            // 
            this.lblSolicitaLote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblSolicitaLote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSolicitaLote.Location = new System.Drawing.Point(181, 55);
            this.lblSolicitaLote.Name = "lblSolicitaLote";
            this.lblSolicitaLote.Size = new System.Drawing.Size(39, 15);
            this.lblSolicitaLote.Visible = false;
            // 
            // lblIdProdutoEmbalagem
            // 
            this.lblIdProdutoEmbalagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblIdProdutoEmbalagem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblIdProdutoEmbalagem.Location = new System.Drawing.Point(136, 55);
            this.lblIdProdutoEmbalagem.Name = "lblIdProdutoEmbalagem";
            this.lblIdProdutoEmbalagem.Size = new System.Drawing.Size(39, 15);
            this.lblIdProdutoEmbalagem.Visible = false;
            // 
            // lblIdProdutoCliente
            // 
            this.lblIdProdutoCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblIdProdutoCliente.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblIdProdutoCliente.Location = new System.Drawing.Point(93, 55);
            this.lblIdProdutoCliente.Name = "lblIdProdutoCliente";
            this.lblIdProdutoCliente.Size = new System.Drawing.Size(39, 15);
            this.lblIdProdutoCliente.Visible = false;
            // 
            // txtValidade
            // 
            this.txtValidade.Enabled = false;
            this.txtValidade.Location = new System.Drawing.Point(117, 171);
            this.txtValidade.Name = "txtValidade";
            this.txtValidade.Size = new System.Drawing.Size(112, 21);
            this.txtValidade.TabIndex = 15;
            this.txtValidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValidade_KeyPress);
            // 
            // txtLote
            // 
            this.txtLote.Enabled = false;
            this.txtLote.Location = new System.Drawing.Point(5, 171);
            this.txtLote.Name = "txtLote";
            this.txtLote.Size = new System.Drawing.Size(99, 21);
            this.txtLote.TabIndex = 14;
            this.txtLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLote_KeyPress);
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Enabled = false;
            this.txtQuantidade.Location = new System.Drawing.Point(5, 121);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(225, 21);
            this.txtQuantidade.TabIndex = 13;
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            // 
            // txtExecCodigoDeBarras
            // 
            this.txtExecCodigoDeBarras.Enabled = false;
            this.txtExecCodigoDeBarras.Location = new System.Drawing.Point(5, 73);
            this.txtExecCodigoDeBarras.Name = "txtExecCodigoDeBarras";
            this.txtExecCodigoDeBarras.Size = new System.Drawing.Size(225, 21);
            this.txtExecCodigoDeBarras.TabIndex = 12;
            this.txtExecCodigoDeBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExecCodigoDeBarras_KeyPress);
            // 
            // txtExecUa
            // 
            this.txtExecUa.Location = new System.Drawing.Point(4, 23);
            this.txtExecUa.Name = "txtExecUa";
            this.txtExecUa.Size = new System.Drawing.Size(225, 21);
            this.txtExecUa.TabIndex = 11;
            this.txtExecUa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExecUa_KeyPress);
            // 
            // btnConfirmarExecucao
            // 
            this.btnConfirmarExecucao.Enabled = false;
            this.btnConfirmarExecucao.Location = new System.Drawing.Point(155, 206);
            this.btnConfirmarExecucao.Name = "btnConfirmarExecucao";
            this.btnConfirmarExecucao.Size = new System.Drawing.Size(72, 20);
            this.btnConfirmarExecucao.TabIndex = 10;
            this.btnConfirmarExecucao.Text = "Confirmar";
            this.btnConfirmarExecucao.Click += new System.EventHandler(this.btnConfirmarExecucao_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(114, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 15);
            this.label7.Text = "Validade:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(3, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 15);
            this.label6.Text = "Lote:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 15);
            this.label5.Text = "Quantidade:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(3, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 15);
            this.label4.Text = "Cód. Barras:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 15);
            this.label3.Text = "UA:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grdLancamentos);
            this.tabPage3.Location = new System.Drawing.Point(0, 0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(234, 229);
            this.tabPage3.Text = "Lanc.";
            // 
            // grdLancamentos
            // 
            this.grdLancamentos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdLancamentos.ContextMenu = this.contextMenu1;
            this.grdLancamentos.Location = new System.Drawing.Point(5, 0);
            this.grdLancamentos.Name = "grdLancamentos";
            this.grdLancamentos.Size = new System.Drawing.Size(225, 226);
            this.grdLancamentos.TabIndex = 0;
            this.grdLancamentos.TableStyles.Add(this.dataGridTableStyle6);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Excluir";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnFinalizar);
            this.tabPage4.Controls.Add(this.grdDivergencias);
            this.tabPage4.Location = new System.Drawing.Point(0, 0);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(234, 229);
            this.tabPage4.Text = "Divergencias";
            // 
            // grdDivergencias
            // 
            this.grdDivergencias.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdDivergencias.ContextMenu = this.contextMenu1;
            this.grdDivergencias.Location = new System.Drawing.Point(5, 1);
            this.grdDivergencias.Name = "grdDivergencias";
            this.grdDivergencias.Size = new System.Drawing.Size(225, 199);
            this.grdDivergencias.TabIndex = 1;
            this.grdDivergencias.TableStyles.Add(this.dataGridTableStyle3);
            // 
            // dataGridTableStyle3
            // 
            this.dataGridTableStyle3.GridColumnStyles.Add(this.cb);
            this.dataGridTableStyle3.GridColumnStyles.Add(this.CODIGO);
            this.dataGridTableStyle3.GridColumnStyles.Add(this.DESCRICAO);
            this.dataGridTableStyle3.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle3.GridColumnStyles.Add(this.QUANTIDADEBASE);
            this.dataGridTableStyle3.GridColumnStyles.Add(this.QUANTIDADELIDO);
            // 
            // cb
            // 
            this.cb.Format = "";
            this.cb.FormatInfo = null;
            this.cb.HeaderText = "CÓD. BARRAS";
            this.cb.MappingName = "CODIGODEBARRAS";
            this.cb.Width = 100;
            // 
            // CODIGO
            // 
            this.CODIGO.Format = "";
            this.CODIGO.FormatInfo = null;
            this.CODIGO.HeaderText = "CODIGO";
            this.CODIGO.MappingName = "CODIGO";
            this.CODIGO.Width = 70;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.Format = "";
            this.DESCRICAO.FormatInfo = null;
            this.DESCRICAO.HeaderText = "DESCRICAO";
            this.DESCRICAO.MappingName = "DESCRICAO";
            this.DESCRICAO.Width = 150;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            // 
            // QUANTIDADEBASE
            // 
            this.QUANTIDADEBASE.Format = "";
            this.QUANTIDADEBASE.FormatInfo = null;
            this.QUANTIDADEBASE.HeaderText = "SOL.";
            this.QUANTIDADEBASE.MappingName = "QUANTIDADEBASE";
            // 
            // QUANTIDADELIDO
            // 
            this.QUANTIDADELIDO.Format = "";
            this.QUANTIDADELIDO.FormatInfo = null;
            this.QUANTIDADELIDO.HeaderText = "CONFERIDO";
            this.QUANTIDADELIDO.MappingName = "QUANTIDADELIDO";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 12;
            this.button1.Text = "Cancelar";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(159, 206);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(72, 20);
            this.btnFinalizar.TabIndex = 2;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // CLW00024
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.statusBar1);
            this.Name = "CLW00024";
            this.Text = "WEB00024";
            this.Load += new System.EventHandler(this.WEB00024_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlNovoRomaneio.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn ROMANEIO;
        private System.Windows.Forms.Button btnNovoRomaneio;
        private System.Windows.Forms.Panel pnlNovoRomaneio;
        private System.Windows.Forms.TextBox txtNovoRomanioChave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfirmarRomaneio;
        private System.Windows.Forms.DataGrid dataGrid2;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle2;
        private System.Windows.Forms.DataGridTextBoxColumn Chave;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.CheckBox chkDevolucao;
        private System.Windows.Forms.Button btnConfirmarExecucao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValidade;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.TextBox txtExecCodigoDeBarras;
        private System.Windows.Forms.TextBox txtExecUa;
        private System.Windows.Forms.Label lblIdProdutoEmbalagem;
        private System.Windows.Forms.Label lblIdProdutoCliente;
        private System.Windows.Forms.Label lblSolicitaValidade;
        private System.Windows.Forms.Label lblSolicitaLote;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGrid grdLancamentos;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle6;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGrid grdDivergencias;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle3;
        private System.Windows.Forms.DataGridTextBoxColumn cb;
        private System.Windows.Forms.DataGridTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
        private System.Windows.Forms.DataGridTextBoxColumn QUANTIDADEBASE;
        private System.Windows.Forms.DataGridTextBoxColumn QUANTIDADELIDO;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFinalizar;
    }
}