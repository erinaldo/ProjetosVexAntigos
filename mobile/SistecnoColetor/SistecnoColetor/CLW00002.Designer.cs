namespace SistecnoColetor
{
    partial class CLW00002
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLW00002));
            this.label1 = new System.Windows.Forms.Label();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.txtEnderecoProduto = new System.Windows.Forms.TextBox();
            this.grd = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.IDPRODUTOCLIENTE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.METODODEMOVIMENTACAO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Endereco = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblEndecoOrigem = new System.Windows.Forms.Label();
            this.lblEnderecoPicking = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.lblUaOrigem = new System.Windows.Forms.Label();
            this.txtLeituraDaUa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLeituraEndereco = new System.Windows.Forms.TextBox();
            this.lblIdProdutoCliente = new System.Windows.Forms.Label();
            this.lblMetodoMovimento = new System.Windows.Forms.Label();
            this.lblIdEnderecoPicking = new System.Windows.Forms.Label();
            this.lblIdEnderecoOrigem = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.Text = "Endereço Picking:";
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // txtEnderecoProduto
            // 
            this.txtEnderecoProduto.Location = new System.Drawing.Point(124, 23);
            this.txtEnderecoProduto.Name = "txtEnderecoProduto";
            this.txtEnderecoProduto.Size = new System.Drawing.Size(114, 21);
            this.txtEnderecoProduto.TabIndex = 3;
            this.txtEnderecoProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEnderecoProduto_KeyPress);
            // 
            // grd
            // 
            this.grd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grd.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.grd.Location = new System.Drawing.Point(3, 50);
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(235, 71);
            this.grd.TabIndex = 9;
            this.grd.TableStyles.Add(this.dataGridTableStyle1);
            this.grd.Click += new System.EventHandler(this.grd_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.IDPRODUTOCLIENTE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.DESCRICAO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.METODODEMOVIMENTACAO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Endereco);
            // 
            // IDPRODUTOCLIENTE
            // 
            this.IDPRODUTOCLIENTE.Format = "";
            this.IDPRODUTOCLIENTE.FormatInfo = null;
            this.IDPRODUTOCLIENTE.HeaderText = "Cod. Produto";
            this.IDPRODUTOCLIENTE.MappingName = "IDPRODUTOCLIENTE";
            this.IDPRODUTOCLIENTE.Width = 0;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Codigo";
            this.dataGridTextBoxColumn1.MappingName = "CODIGO";
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.Format = "";
            this.DESCRICAO.FormatInfo = null;
            this.DESCRICAO.HeaderText = "Descrição";
            this.DESCRICAO.MappingName = "DESCRICAO";
            this.DESCRICAO.Width = 146;
            // 
            // METODODEMOVIMENTACAO
            // 
            this.METODODEMOVIMENTACAO.Format = "";
            this.METODODEMOVIMENTACAO.FormatInfo = null;
            this.METODODEMOVIMENTACAO.HeaderText = "Metodo de Movimento";
            this.METODODEMOVIMENTACAO.MappingName = "METODODEMOVIMENTACAO";
            this.METODODEMOVIMENTACAO.Width = 0;
            // 
            // Endereco
            // 
            this.Endereco.Format = "";
            this.Endereco.FormatInfo = null;
            this.Endereco.HeaderText = "Picking";
            this.Endereco.MappingName = "ENDERECO";
            this.Endereco.Width = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(5, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 1);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(0, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(230, 1);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel4.Location = new System.Drawing.Point(0, 68);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(230, 1);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel2.Location = new System.Drawing.Point(0, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 1);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(4, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.Text = "ORIGEM";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(5, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 19);
            this.label7.Text = "Endereço";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(74, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 19);
            this.label8.Text = "UA";
            // 
            // lblEndecoOrigem
            // 
            this.lblEndecoOrigem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblEndecoOrigem.Location = new System.Drawing.Point(5, 162);
            this.lblEndecoOrigem.Name = "lblEndecoOrigem";
            this.lblEndecoOrigem.Size = new System.Drawing.Size(65, 20);
            // 
            // lblEnderecoPicking
            // 
            this.lblEnderecoPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblEnderecoPicking.Location = new System.Drawing.Point(3, 220);
            this.lblEnderecoPicking.Name = "lblEnderecoPicking";
            this.lblEnderecoPicking.Size = new System.Drawing.Size(119, 20);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(126, 250);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(109, 21);
            this.btnConfirmar.TabIndex = 24;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // lblUaOrigem
            // 
            this.lblUaOrigem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblUaOrigem.Location = new System.Drawing.Point(72, 162);
            this.lblUaOrigem.Name = "lblUaOrigem";
            this.lblUaOrigem.Size = new System.Drawing.Size(50, 20);
            // 
            // txtLeituraDaUa
            // 
            this.txtLeituraDaUa.Enabled = false;
            this.txtLeituraDaUa.Location = new System.Drawing.Point(124, 162);
            this.txtLeituraDaUa.Name = "txtLeituraDaUa";
            this.txtLeituraDaUa.Size = new System.Drawing.Size(114, 21);
            this.txtLeituraDaUa.TabIndex = 55;
            this.txtLeituraDaUa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLeituraDaUa_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(125, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 19);
            this.label2.Text = "Leitura UA";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(4, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.Text = "DESTINO";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 19);
            this.label4.Text = "Endereço";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(126, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 19);
            this.label5.Text = "Leitura Endereço";
            // 
            // txtLeituraEndereco
            // 
            this.txtLeituraEndereco.Enabled = false;
            this.txtLeituraEndereco.Location = new System.Drawing.Point(124, 219);
            this.txtLeituraEndereco.Name = "txtLeituraEndereco";
            this.txtLeituraEndereco.Size = new System.Drawing.Size(114, 21);
            this.txtLeituraEndereco.TabIndex = 64;
            this.txtLeituraEndereco.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLeituraEndereco_KeyPress);
            // 
            // lblIdProdutoCliente
            // 
            this.lblIdProdutoCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblIdProdutoCliente.Location = new System.Drawing.Point(5, 250);
            this.lblIdProdutoCliente.Name = "lblIdProdutoCliente";
            this.lblIdProdutoCliente.Size = new System.Drawing.Size(0, 0);
            this.lblIdProdutoCliente.Visible = false;
            // 
            // lblMetodoMovimento
            // 
            this.lblMetodoMovimento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblMetodoMovimento.Location = new System.Drawing.Point(72, 126);
            this.lblMetodoMovimento.Name = "lblMetodoMovimento";
            this.lblMetodoMovimento.Size = new System.Drawing.Size(0, 0);
            this.lblMetodoMovimento.Visible = false;
            // 
            // lblIdEnderecoPicking
            // 
            this.lblIdEnderecoPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblIdEnderecoPicking.Location = new System.Drawing.Point(4, 246);
            this.lblIdEnderecoPicking.Name = "lblIdEnderecoPicking";
            this.lblIdEnderecoPicking.Size = new System.Drawing.Size(91, 20);
            // 
            // lblIdEnderecoOrigem
            // 
            this.lblIdEnderecoOrigem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblIdEnderecoOrigem.Location = new System.Drawing.Point(148, 126);
            this.lblIdEnderecoOrigem.Name = "lblIdEnderecoOrigem";
            this.lblIdEnderecoOrigem.Size = new System.Drawing.Size(91, 20);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // CLW00002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblIdEnderecoOrigem);
            this.Controls.Add(this.lblIdEnderecoPicking);
            this.Controls.Add(this.txtLeituraEndereco);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLeituraDaUa);
            this.Controls.Add(this.lblUaOrigem);
            this.Controls.Add(this.lblEndecoOrigem);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblEnderecoPicking);
            this.Controls.Add(this.lblMetodoMovimento);
            this.Controls.Add(this.lblIdProdutoCliente);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.txtEnderecoProduto);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CLW00002";
            this.Text = "CLW00002";
            this.Load += new System.EventHandler(this.CLW00002_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.TextBox txtEnderecoProduto;
        private System.Windows.Forms.DataGrid grd;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridTextBoxColumn IDPRODUTOCLIENTE;
        private System.Windows.Forms.DataGridTextBoxColumn METODODEMOVIMENTACAO;
        private System.Windows.Forms.DataGridTextBoxColumn Endereco;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblEndecoOrigem;
        private System.Windows.Forms.Label lblEnderecoPicking;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label lblUaOrigem;
        private System.Windows.Forms.TextBox txtLeituraDaUa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLeituraEndereco;
        private System.Windows.Forms.Label lblIdProdutoCliente;
        private System.Windows.Forms.Label lblMetodoMovimento;
        private System.Windows.Forms.Label lblIdEnderecoPicking;
        private System.Windows.Forms.Label lblIdEnderecoOrigem;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
    }
}