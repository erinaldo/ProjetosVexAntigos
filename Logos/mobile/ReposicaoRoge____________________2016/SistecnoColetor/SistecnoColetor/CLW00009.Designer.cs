namespace SistecnoColetor
{
    partial class CLW00009
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLW00009));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChaveDocumento = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumeroDocumentp = new System.Windows.Forms.Label();
            this.lblEmissao = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblQuantidadeDeItens = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.NUMERO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.EMISSAO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DESTINATARIO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 17);
            this.label1.Text = "Chave/Numero Nota Fiscal:";
            // 
            // txtChaveDocumento
            // 
            this.txtChaveDocumento.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtChaveDocumento.Location = new System.Drawing.Point(3, 36);
            this.txtChaveDocumento.Name = "txtChaveDocumento";
            this.txtChaveDocumento.Size = new System.Drawing.Size(234, 19);
            this.txtChaveDocumento.TabIndex = 3;
            this.txtChaveDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChaveDocumento_KeyPress);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 1);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(4, 246);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(232, 24);
            this.btnConfirmar.TabIndex = 43;
            this.btnConfirmar.Text = "Ler Itens";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_2);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(5, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.Text = "Nota Fiscal:";
            // 
            // lblNumeroDocumentp
            // 
            this.lblNumeroDocumentp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblNumeroDocumentp.Location = new System.Drawing.Point(5, 161);
            this.lblNumeroDocumentp.Name = "lblNumeroDocumentp";
            this.lblNumeroDocumentp.Size = new System.Drawing.Size(102, 20);
            // 
            // lblEmissao
            // 
            this.lblEmissao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblEmissao.Location = new System.Drawing.Point(133, 161);
            this.lblEmissao.Name = "lblEmissao";
            this.lblEmissao.Size = new System.Drawing.Size(102, 20);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(133, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 15);
            this.label4.Text = "Emissão:";
            // 
            // lblQuantidadeDeItens
            // 
            this.lblQuantidadeDeItens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblQuantidadeDeItens.Location = new System.Drawing.Point(5, 197);
            this.lblQuantidadeDeItens.Name = "lblQuantidadeDeItens";
            this.lblQuantidadeDeItens.Size = new System.Drawing.Size(231, 46);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(5, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 15);
            this.label5.Text = "Destinatario:";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 58);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(234, 85);
            this.dataGrid1.TabIndex = 47;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.Visible = false;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.NUMERO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.EMISSAO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.DESTINATARIO);
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "IDDOCUMENTO";
            this.dataGridTextBoxColumn1.MappingName = "IDDOCUMENTO";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // NUMERO
            // 
            this.NUMERO.Format = "";
            this.NUMERO.FormatInfo = null;
            this.NUMERO.HeaderText = "NUMERO";
            this.NUMERO.MappingName = "NUMERO";
            this.NUMERO.Width = 60;
            // 
            // EMISSAO
            // 
            this.EMISSAO.Format = "";
            this.EMISSAO.FormatInfo = null;
            this.EMISSAO.HeaderText = "EMISSAO";
            this.EMISSAO.MappingName = "EMISSAO";
            this.EMISSAO.Width = 60;
            // 
            // DESTINATARIO
            // 
            this.DESTINATARIO.Format = "";
            this.DESTINATARIO.FormatInfo = null;
            this.DESTINATARIO.HeaderText = "DESTINATARIO";
            this.DESTINATARIO.MappingName = "DESTINATARIO";
            this.DESTINATARIO.Width = 200;
            // 
            // CLW00009
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.lblQuantidadeDeItens);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblEmissao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblNumeroDocumentp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtChaveDocumento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CLW00009";
            this.Text = "CLW00009";
            this.Load += new System.EventHandler(this.CLW00004_Load);
            this.Activated += new System.EventHandler(this.CLW00009_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChaveDocumento;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNumeroDocumentp;
        private System.Windows.Forms.Label lblEmissao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblQuantidadeDeItens;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn NUMERO;
        private System.Windows.Forms.DataGridTextBoxColumn EMISSAO;
        private System.Windows.Forms.DataGridTextBoxColumn DESTINATARIO;
    }
}