namespace SistecnoColetor
{
    partial class CLW00023
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
            this.txtUa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.txtCodigoDeBarras = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantidadeConferida = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProdutoEmbalagem = new System.Windows.Forms.Label();
            this.lblFator = new System.Windows.Forms.Label();
            this.lblIddepositoPlantaOrigem = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblIdProdutoCliente = new System.Windows.Forms.Label();
            this.lblTempEtiqueta = new System.Windows.Forms.Label();
            this.lblTempCodigoBarras = new System.Windows.Forms.Label();
            this.lblTempQuantidade = new System.Windows.Forms.Label();
            this.txtLoteValidade = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDigito = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProduto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // txtUa
            // 
            this.txtUa.Location = new System.Drawing.Point(3, 33);
            this.txtUa.Name = "txtUa";
            this.txtUa.Size = new System.Drawing.Size(234, 21);
            this.txtUa.TabIndex = 7;
            this.txtUa.TextChanged += new System.EventHandler(this.txtUa_TextChanged);
            this.txtUa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUa_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 14);
            this.label2.Text = "UA:";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(153, 251);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(83, 20);
            this.btnConfirmar.TabIndex = 13;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_1);
            // 
            // txtCodigoDeBarras
            // 
            this.txtCodigoDeBarras.Enabled = false;
            this.txtCodigoDeBarras.Location = new System.Drawing.Point(3, 116);
            this.txtCodigoDeBarras.Name = "txtCodigoDeBarras";
            this.txtCodigoDeBarras.Size = new System.Drawing.Size(232, 21);
            this.txtCodigoDeBarras.TabIndex = 18;
            this.txtCodigoDeBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoDeBarras_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.Text = "Cód.Barras:";
            // 
            // txtQuantidadeConferida
            // 
            this.txtQuantidadeConferida.Enabled = false;
            this.txtQuantidadeConferida.Location = new System.Drawing.Point(4, 164);
            this.txtQuantidadeConferida.Name = "txtQuantidadeConferida";
            this.txtQuantidadeConferida.Size = new System.Drawing.Size(231, 21);
            this.txtQuantidadeConferida.TabIndex = 21;
            this.txtQuantidadeConferida.TextChanged += new System.EventHandler(this.txtQuantidadeConferida_TextChanged);
            this.txtQuantidadeConferida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantidadeConferida_KeyDown);
            this.txtQuantidadeConferida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidadeConferida_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 15);
            this.label3.Text = "Quantidade Conferida:";
            // 
            // lblProdutoEmbalagem
            // 
            this.lblProdutoEmbalagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblProdutoEmbalagem.Location = new System.Drawing.Point(16, 246);
            this.lblProdutoEmbalagem.Name = "lblProdutoEmbalagem";
            this.lblProdutoEmbalagem.Size = new System.Drawing.Size(41, 20);
            this.lblProdutoEmbalagem.Visible = false;
            // 
            // lblFator
            // 
            this.lblFator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblFator.Location = new System.Drawing.Point(16, 246);
            this.lblFator.Name = "lblFator";
            this.lblFator.Size = new System.Drawing.Size(41, 20);
            this.lblFator.Visible = false;
            // 
            // lblIddepositoPlantaOrigem
            // 
            this.lblIddepositoPlantaOrigem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblIddepositoPlantaOrigem.Location = new System.Drawing.Point(12, 246);
            this.lblIddepositoPlantaOrigem.Name = "lblIddepositoPlantaOrigem";
            this.lblIddepositoPlantaOrigem.Size = new System.Drawing.Size(48, 20);
            this.lblIddepositoPlantaOrigem.Text = "1";
            this.lblIddepositoPlantaOrigem.Visible = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // lblIdProdutoCliente
            // 
            this.lblIdProdutoCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblIdProdutoCliente.Location = new System.Drawing.Point(16, 246);
            this.lblIdProdutoCliente.Name = "lblIdProdutoCliente";
            this.lblIdProdutoCliente.Size = new System.Drawing.Size(41, 20);
            this.lblIdProdutoCliente.Visible = false;
            // 
            // lblTempEtiqueta
            // 
            this.lblTempEtiqueta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTempEtiqueta.Location = new System.Drawing.Point(6, 252);
            this.lblTempEtiqueta.Name = "lblTempEtiqueta";
            this.lblTempEtiqueta.Size = new System.Drawing.Size(101, 17);
            this.lblTempEtiqueta.Visible = false;
            // 
            // lblTempCodigoBarras
            // 
            this.lblTempCodigoBarras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTempCodigoBarras.Location = new System.Drawing.Point(29, 255);
            this.lblTempCodigoBarras.Name = "lblTempCodigoBarras";
            this.lblTempCodigoBarras.Size = new System.Drawing.Size(98, 14);
            this.lblTempCodigoBarras.Visible = false;
            // 
            // lblTempQuantidade
            // 
            this.lblTempQuantidade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTempQuantidade.Location = new System.Drawing.Point(7, 255);
            this.lblTempQuantidade.Name = "lblTempQuantidade";
            this.lblTempQuantidade.Size = new System.Drawing.Size(98, 14);
            this.lblTempQuantidade.Visible = false;
            // 
            // txtLoteValidade
            // 
            this.txtLoteValidade.Enabled = false;
            this.txtLoteValidade.Location = new System.Drawing.Point(3, 74);
            this.txtLoteValidade.Name = "txtLoteValidade";
            this.txtLoteValidade.Size = new System.Drawing.Size(233, 21);
            this.txtLoteValidade.TabIndex = 27;
            this.txtLoteValidade.TextChanged += new System.EventHandler(this.txtLoteValidade_TextChanged);
            this.txtLoteValidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLote_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.Text = "Validade/Lote:";
            // 
            // lblDigito
            // 
            this.lblDigito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblDigito.Location = new System.Drawing.Point(105, 95);
            this.lblDigito.Name = "lblDigito";
            this.lblDigito.Size = new System.Drawing.Size(100, 18);
            // 
            // comboBox1
            // 
            this.comboBox1.Items.Add("DEVOLUCAO");
            this.comboBox1.Items.Add("AVARIA");
            this.comboBox1.Items.Add("QUALIDADE");
            this.comboBox1.Items.Add("COBERTURA");
            this.comboBox1.Items.Add("ARMAZENAGEM");
            this.comboBox1.Location = new System.Drawing.Point(7, 220);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(225, 22);
            this.comboBox1.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 15);
            this.label5.Text = "Tipo De Armazenagem";
            // 
            // lblProduto
            // 
            this.lblProduto.Location = new System.Drawing.Point(29, 252);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(100, 20);
            this.lblProduto.Text = "label6";
            this.lblProduto.Visible = false;
            // 
            // CLW00023
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblDigito);
            this.Controls.Add(this.txtLoteValidade);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTempQuantidade);
            this.Controls.Add(this.lblTempCodigoBarras);
            this.Controls.Add(this.lblTempEtiqueta);
            this.Controls.Add(this.lblIdProdutoCliente);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblIddepositoPlantaOrigem);
            this.Controls.Add(this.lblFator);
            this.Controls.Add(this.lblProdutoEmbalagem);
            this.Controls.Add(this.txtQuantidadeConferida);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCodigoDeBarras);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtUa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusBar1);
            this.KeyPreview = true;
            this.Name = "CLW00023";
            this.Text = "CLW00023";
            this.Load += new System.EventHandler(this.CLW00001_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CLW00006_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.TextBox txtUa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.TextBox txtCodigoDeBarras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuantidadeConferida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProdutoEmbalagem;
        private System.Windows.Forms.Label lblFator;
        private System.Windows.Forms.Label lblIddepositoPlantaOrigem;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblIdProdutoCliente;
        private System.Windows.Forms.Label lblTempEtiqueta;
        private System.Windows.Forms.Label lblTempCodigoBarras;
        private System.Windows.Forms.Label lblTempQuantidade;
        private System.Windows.Forms.TextBox txtLoteValidade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDigito;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProduto;
    }
}