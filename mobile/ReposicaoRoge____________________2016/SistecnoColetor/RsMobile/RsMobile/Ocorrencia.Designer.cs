namespace RsMobile
{
    partial class Ocorrencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ocorrencia));
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnVoltarListaStatus = new System.Windows.Forms.Button();
            this.lblOcorrencia = new System.Windows.Forms.Label();
            this.grd = new System.Windows.Forms.DataGrid();
            this.grdStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.Codigo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridTextBoxColumn();
            this.IdOcorrencia = new System.Windows.Forms.DataGridTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConfirmar.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.Green;
            this.btnConfirmar.Location = new System.Drawing.Point(122, 273);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(115, 20);
            this.btnConfirmar.TabIndex = 21;
            this.btnConfirmar.Text = "CONFIRMAR";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnVoltarListaStatus
            // 
            this.btnVoltarListaStatus.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnVoltarListaStatus.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btnVoltarListaStatus.ForeColor = System.Drawing.Color.Red;
            this.btnVoltarListaStatus.Location = new System.Drawing.Point(3, 273);
            this.btnVoltarListaStatus.Name = "btnVoltarListaStatus";
            this.btnVoltarListaStatus.Size = new System.Drawing.Size(115, 20);
            this.btnVoltarListaStatus.TabIndex = 22;
            this.btnVoltarListaStatus.Text = "VOLTAR";
            this.btnVoltarListaStatus.Click += new System.EventHandler(this.btnVoltarListaStatus_Click);
            // 
            // lblOcorrencia
            // 
            this.lblOcorrencia.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lblOcorrencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblOcorrencia.Location = new System.Drawing.Point(4, 248);
            this.lblOcorrencia.Name = "lblOcorrencia";
            this.lblOcorrencia.Size = new System.Drawing.Size(233, 20);
            // 
            // grd
            // 
            this.grd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grd.Location = new System.Drawing.Point(0, 45);
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(240, 200);
            this.grd.TabIndex = 23;
            this.grd.TableStyles.Add(this.grdStyle1);
            this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
            this.grd.Click += new System.EventHandler(this.grd_Click);
            // 
            // grdStyle1
            // 
            this.grdStyle1.GridColumnStyles.Add(this.Codigo);
            this.grdStyle1.GridColumnStyles.Add(this.Nome);
            this.grdStyle1.GridColumnStyles.Add(this.IdOcorrencia);
            // 
            // Codigo
            // 
            this.Codigo.Format = "";
            this.Codigo.FormatInfo = null;
            this.Codigo.HeaderText = "Código";
            this.Codigo.MappingName = "CODIGO";
            this.Codigo.Width = 40;
            // 
            // Nome
            // 
            this.Nome.Format = "";
            this.Nome.FormatInfo = null;
            this.Nome.HeaderText = "Nome";
            this.Nome.MappingName = "NOME";
            this.Nome.Width = 250;
            // 
            // IdOcorrencia
            // 
            this.IdOcorrencia.Format = "";
            this.IdOcorrencia.FormatInfo = null;
            this.IdOcorrencia.HeaderText = "ID";
            this.IdOcorrencia.MappingName = "IDOCORRENCIA";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // Ocorrencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.lblOcorrencia);
            this.Controls.Add(this.btnVoltarListaStatus);
            this.Controls.Add(this.btnConfirmar);
            this.Name = "Ocorrencia";
            this.Text = "RsMobile";
            this.Load += new System.EventHandler(this.Ocorrencia_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnVoltarListaStatus;
        private System.Windows.Forms.Label lblOcorrencia;
        private System.Windows.Forms.DataGrid grd;
        private System.Windows.Forms.DataGridTableStyle grdStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridTextBoxColumn Nome;
        private System.Windows.Forms.DataGridTextBoxColumn IdOcorrencia;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}