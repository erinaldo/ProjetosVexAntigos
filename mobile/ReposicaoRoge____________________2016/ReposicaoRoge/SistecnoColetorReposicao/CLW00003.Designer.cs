namespace SistecnoColetor
{
    partial class CLW00003
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLW00003));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.txtCodigoDeBarras = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grd = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.cODIGO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMetodoMovimento = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastro = new System.Windows.Forms.TextBox();
            this.txtLastroAltura = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCubagemLargura = new System.Windows.Forms.TextBox();
            this.txtCubagemAltura = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCubagemPesoBruto = new System.Windows.Forms.TextBox();
            this.txtCubagemPesoLiq = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCubagemComprimento = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.LblIdProdutoCliente = new System.Windows.Forms.Label();
            this.lblIdProduto = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // txtCodigoDeBarras
            // 
            this.txtCodigoDeBarras.Location = new System.Drawing.Point(122, 18);
            this.txtCodigoDeBarras.Name = "txtCodigoDeBarras";
            this.txtCodigoDeBarras.Size = new System.Drawing.Size(116, 21);
            this.txtCodigoDeBarras.TabIndex = 0;
            this.txtCodigoDeBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEnderecoProduto_KeyPress_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 14);
            this.label1.Text = "Código de Barras:";
            // 
            // grd
            // 
            this.grd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grd.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.grd.Location = new System.Drawing.Point(2, 42);
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(235, 49);
            this.grd.TabIndex = 10;
            this.grd.TableStyles.Add(this.dataGridTableStyle1);
            this.grd.Click += new System.EventHandler(this.grd_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cODIGO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Cliente);
            // 
            // cODIGO
            // 
            this.cODIGO.Format = "";
            this.cODIGO.FormatInfo = null;
            this.cODIGO.HeaderText = "CÓD. CLIENTE";
            this.cODIGO.MappingName = "IDCLIENTE";
            // 
            // Cliente
            // 
            this.Cliente.Format = "";
            this.Cliente.FormatInfo = null;
            this.Cliente.MappingName = "CLIENTE";
            this.Cliente.Width = 150;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(2, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.Text = "PALLET";
            // 
            // lblMetodoMovimento
            // 
            this.lblMetodoMovimento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblMetodoMovimento.Location = new System.Drawing.Point(73, 110);
            this.lblMetodoMovimento.Name = "lblMetodoMovimento";
            this.lblMetodoMovimento.Size = new System.Drawing.Size(0, 0);
            this.lblMetodoMovimento.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 14);
            this.label2.Text = "Lastro";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(121, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.Text = "Altura";
            // 
            // txtLastro
            // 
            this.txtLastro.Location = new System.Drawing.Point(2, 123);
            this.txtLastro.Name = "txtLastro";
            this.txtLastro.Size = new System.Drawing.Size(116, 21);
            this.txtLastro.TabIndex = 1;
            this.txtLastro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLastro_KeyDown);
            this.txtLastro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastro_KeyPress);
            this.txtLastro.LostFocus += new System.EventHandler(this.txtLastro_LostFocus);
            // 
            // txtLastroAltura
            // 
            this.txtLastroAltura.Location = new System.Drawing.Point(122, 123);
            this.txtLastroAltura.Name = "txtLastroAltura";
            this.txtLastroAltura.Size = new System.Drawing.Size(116, 21);
            this.txtLastroAltura.TabIndex = 2;
            this.txtLastroAltura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLastroAltura_KeyDown);
            this.txtLastroAltura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastroAltura_KeyPress);
            this.txtLastroAltura.LostFocus += new System.EventHandler(this.txtLastroAltura_LostFocus);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(2, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 15);
            this.label4.Text = "CUBAGEM";
            // 
            // txtCubagemLargura
            // 
            this.txtCubagemLargura.Location = new System.Drawing.Point(123, 177);
            this.txtCubagemLargura.Name = "txtCubagemLargura";
            this.txtCubagemLargura.Size = new System.Drawing.Size(116, 21);
            this.txtCubagemLargura.TabIndex = 4;
            this.txtCubagemLargura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCubagemLargura_KeyDown);
            this.txtCubagemLargura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCubagemLargura_KeyPress);
            this.txtCubagemLargura.LostFocus += new System.EventHandler(this.txtCubagemLargura_LostFocus);
            // 
            // txtCubagemAltura
            // 
            this.txtCubagemAltura.Location = new System.Drawing.Point(2, 177);
            this.txtCubagemAltura.Name = "txtCubagemAltura";
            this.txtCubagemAltura.Size = new System.Drawing.Size(116, 21);
            this.txtCubagemAltura.TabIndex = 3;
            this.txtCubagemAltura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCubagemAltura_KeyDown);
            this.txtCubagemAltura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCubagemAltura_KeyPress);
            this.txtCubagemAltura.LostFocus += new System.EventHandler(this.txtCubagemAltura_LostFocus);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(122, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.Text = "Largura";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(2, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 14);
            this.label7.Text = "Altura";
            // 
            // txtCubagemPesoBruto
            // 
            this.txtCubagemPesoBruto.Location = new System.Drawing.Point(123, 213);
            this.txtCubagemPesoBruto.Name = "txtCubagemPesoBruto";
            this.txtCubagemPesoBruto.Size = new System.Drawing.Size(116, 21);
            this.txtCubagemPesoBruto.TabIndex = 6;
            this.txtCubagemPesoBruto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCubagemPesoBruto_KeyDown);
            this.txtCubagemPesoBruto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCubagemPesoBruto_KeyPress);
            this.txtCubagemPesoBruto.LostFocus += new System.EventHandler(this.txtCubagemPesoBruto_LostFocus);
            // 
            // txtCubagemPesoLiq
            // 
            this.txtCubagemPesoLiq.Location = new System.Drawing.Point(2, 249);
            this.txtCubagemPesoLiq.Name = "txtCubagemPesoLiq";
            this.txtCubagemPesoLiq.Size = new System.Drawing.Size(116, 21);
            this.txtCubagemPesoLiq.TabIndex = 7;
            this.txtCubagemPesoLiq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCubagemPesoLiq_KeyDown);
            this.txtCubagemPesoLiq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCubagemPesoLiq_KeyPress);
            this.txtCubagemPesoLiq.LostFocus += new System.EventHandler(this.txtCubagemPesoLiq_LostFocus);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(122, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.Text = "Peso Bruto";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(2, 235);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 14);
            this.label9.Text = "Peso Liquido";
            // 
            // txtCubagemComprimento
            // 
            this.txtCubagemComprimento.Location = new System.Drawing.Point(2, 213);
            this.txtCubagemComprimento.Name = "txtCubagemComprimento";
            this.txtCubagemComprimento.Size = new System.Drawing.Size(115, 21);
            this.txtCubagemComprimento.TabIndex = 5;
            this.txtCubagemComprimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCubagemComprimento_KeyDown);
            this.txtCubagemComprimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCubagemComprimento_KeyPress);
            this.txtCubagemComprimento.LostFocus += new System.EventHandler(this.txtCubagemComprimento_LostFocus);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(2, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 14);
            this.label10.Text = "Comprimento";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmar.Location = new System.Drawing.Point(124, 249);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(113, 21);
            this.btnConfirmar.TabIndex = 8;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_1);
            // 
            // LblIdProdutoCliente
            // 
            this.LblIdProdutoCliente.Location = new System.Drawing.Point(190, 99);
            this.LblIdProdutoCliente.Name = "LblIdProdutoCliente";
            this.LblIdProdutoCliente.Size = new System.Drawing.Size(47, 13);
            this.LblIdProdutoCliente.Visible = false;
            // 
            // lblIdProduto
            // 
            this.lblIdProduto.Location = new System.Drawing.Point(50, 107);
            this.lblIdProduto.Name = "lblIdProduto";
            this.lblIdProduto.Size = new System.Drawing.Size(47, 13);
            this.lblIdProduto.Visible = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 1);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // CLW00003
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblIdProduto);
            this.Controls.Add(this.LblIdProdutoCliente);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtCubagemComprimento);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCubagemPesoBruto);
            this.Controls.Add(this.txtCubagemPesoLiq);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCubagemLargura);
            this.Controls.Add(this.txtCubagemAltura);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLastroAltura);
            this.Controls.Add(this.txtLastro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblMetodoMovimento);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.txtCodigoDeBarras);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "CLW00003";
            this.Text = "CLE00003";
            this.Load += new System.EventHandler(this.CLW00003_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.TextBox txtCodigoDeBarras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid grd;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMetodoMovimento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLastro;
        private System.Windows.Forms.TextBox txtLastroAltura;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCubagemLargura;
        private System.Windows.Forms.TextBox txtCubagemAltura;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCubagemPesoBruto;
        private System.Windows.Forms.TextBox txtCubagemPesoLiq;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCubagemComprimento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.DataGridTextBoxColumn cODIGO;
        private System.Windows.Forms.DataGridTextBoxColumn Cliente;
        private System.Windows.Forms.Label LblIdProdutoCliente;
        private System.Windows.Forms.Label lblIdProduto;
        private System.Windows.Forms.Label lblTitulo;
    }
}