namespace SistecnoColetor
{
    partial class CLW00018
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLW00018));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblIdProdutoCliente = new System.Windows.Forms.Label();
            this.lblSaldoUa = new System.Windows.Forms.Label();
            this.lblUaDestino = new System.Windows.Forms.Label();
            this.txtUa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.Red;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(149, 136);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(86, 21);
            this.btnConfirmar.TabIndex = 13;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_1);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 14);
            this.label4.Text = "Endereço:";
            // 
            // txtEndereco
            // 
            this.txtEndereco.Location = new System.Drawing.Point(6, 38);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(229, 21);
            this.txtEndereco.TabIndex = 24;
            this.txtEndereco.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEndereco_KeyPress);
            // 
            // lblIdProdutoCliente
            // 
            this.lblIdProdutoCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblIdProdutoCliente.ForeColor = System.Drawing.Color.Black;
            this.lblIdProdutoCliente.Location = new System.Drawing.Point(6, 249);
            this.lblIdProdutoCliente.Name = "lblIdProdutoCliente";
            this.lblIdProdutoCliente.Size = new System.Drawing.Size(103, 20);
            this.lblIdProdutoCliente.Visible = false;
            // 
            // lblSaldoUa
            // 
            this.lblSaldoUa.Location = new System.Drawing.Point(16, 255);
            this.lblSaldoUa.Name = "lblSaldoUa";
            this.lblSaldoUa.Size = new System.Drawing.Size(103, 14);
            this.lblSaldoUa.Text = "0";
            this.lblSaldoUa.Visible = false;
            // 
            // lblUaDestino
            // 
            this.lblUaDestino.Location = new System.Drawing.Point(115, 249);
            this.lblUaDestino.Name = "lblUaDestino";
            this.lblUaDestino.Size = new System.Drawing.Size(29, 14);
            this.lblUaDestino.Visible = false;
            // 
            // txtUa
            // 
            this.txtUa.Enabled = false;
            this.txtUa.Location = new System.Drawing.Point(6, 84);
            this.txtUa.Name = "txtUa";
            this.txtUa.Size = new System.Drawing.Size(229, 21);
            this.txtUa.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 14);
            this.label1.Text = "UA:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Blue;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(6, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 21);
            this.button1.TabIndex = 49;
            this.button1.Text = "Cancelar";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CLW00018
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUa);
            this.Controls.Add(this.lblUaDestino);
            this.Controls.Add(this.lblIdProdutoCliente);
            this.Controls.Add(this.txtEndereco);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.lblSaldoUa);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "CLW00018";
            this.Text = "CLW00007";
            this.Load += new System.EventHandler(this.CLW00001_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CLW00008_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblIdProdutoCliente;
        private System.Windows.Forms.Label lblSaldoUa;
        private System.Windows.Forms.Label lblUaDestino;
        private System.Windows.Forms.TextBox txtUa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}