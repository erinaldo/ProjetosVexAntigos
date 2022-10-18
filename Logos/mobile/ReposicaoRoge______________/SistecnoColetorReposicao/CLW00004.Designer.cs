namespace SistecnoColetor
{
    partial class CLW00004
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLW00004));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChaveDocumento = new System.Windows.Forms.TextBox();
            this.lblChave = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblQuantidadeVolumes = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblQuantidadeDeItens = new System.Windows.Forms.Label();
            this.btnVolumes = new System.Windows.Forms.Button();
            this.btnItens = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblIdReposicaoRoge = new System.Windows.Forms.Label();
            this.lblClienteEspecial = new System.Windows.Forms.Label();
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
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.Text = "Chave Documento:";
            // 
            // txtChaveDocumento
            // 
            this.txtChaveDocumento.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.txtChaveDocumento.Location = new System.Drawing.Point(3, 47);
            this.txtChaveDocumento.Name = "txtChaveDocumento";
            this.txtChaveDocumento.Size = new System.Drawing.Size(234, 18);
            this.txtChaveDocumento.TabIndex = 3;
            this.txtChaveDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChaveDocumento_KeyPress);
            // 
            // lblChave
            // 
            this.lblChave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblChave.Location = new System.Drawing.Point(3, 103);
            this.lblChave.Name = "lblChave";
            this.lblChave.Size = new System.Drawing.Size(234, 20);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Chave:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.Text = "Volumes";
            // 
            // lblQuantidadeVolumes
            // 
            this.lblQuantidadeVolumes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblQuantidadeVolumes.Location = new System.Drawing.Point(3, 183);
            this.lblQuantidadeVolumes.Name = "lblQuantidadeVolumes";
            this.lblQuantidadeVolumes.Size = new System.Drawing.Size(115, 20);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(124, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.Text = "Itens:";
            // 
            // lblQuantidadeDeItens
            // 
            this.lblQuantidadeDeItens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblQuantidadeDeItens.Location = new System.Drawing.Point(124, 183);
            this.lblQuantidadeDeItens.Name = "lblQuantidadeDeItens";
            this.lblQuantidadeDeItens.Size = new System.Drawing.Size(115, 20);
            // 
            // btnVolumes
            // 
            this.btnVolumes.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnVolumes.Enabled = false;
            this.btnVolumes.Location = new System.Drawing.Point(3, 210);
            this.btnVolumes.Name = "btnVolumes";
            this.btnVolumes.Size = new System.Drawing.Size(83, 24);
            this.btnVolumes.TabIndex = 16;
            this.btnVolumes.Text = "Volumes";
            this.btnVolumes.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnItens
            // 
            this.btnItens.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnItens.Enabled = false;
            this.btnItens.Location = new System.Drawing.Point(154, 210);
            this.btnItens.Name = "btnItens";
            this.btnItens.Size = new System.Drawing.Size(83, 24);
            this.btnItens.TabIndex = 25;
            this.btnItens.Text = "Itens";
            this.btnItens.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Location = new System.Drawing.Point(0, 242);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(237, 24);
            this.btnEnviar.TabIndex = 34;
            this.btnEnviar.Text = "Enviar Para Auditor";
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 1);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
            // 
            // lblIdReposicaoRoge
            // 
            this.lblIdReposicaoRoge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblIdReposicaoRoge.Location = new System.Drawing.Point(122, 24);
            this.lblIdReposicaoRoge.Name = "lblIdReposicaoRoge";
            this.lblIdReposicaoRoge.Size = new System.Drawing.Size(115, 20);
            this.lblIdReposicaoRoge.Visible = false;
            // 
            // lblClienteEspecial
            // 
            this.lblClienteEspecial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblClienteEspecial.Location = new System.Drawing.Point(3, 134);
            this.lblClienteEspecial.Name = "lblClienteEspecial";
            this.lblClienteEspecial.Size = new System.Drawing.Size(234, 20);
            // 
            // CLW00004
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblClienteEspecial);
            this.Controls.Add(this.lblIdReposicaoRoge);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.btnItens);
            this.Controls.Add(this.btnVolumes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblQuantidadeDeItens);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQuantidadeVolumes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblChave);
            this.Controls.Add(this.txtChaveDocumento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CLW00004";
            this.Text = "CLW00004";
            this.Load += new System.EventHandler(this.CLW00004_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChaveDocumento;
        private System.Windows.Forms.Label lblChave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblQuantidadeVolumes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblQuantidadeDeItens;
        private System.Windows.Forms.Button btnVolumes;
        private System.Windows.Forms.Button btnItens;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblIdReposicaoRoge;
        private System.Windows.Forms.Label lblClienteEspecial;
    }
}