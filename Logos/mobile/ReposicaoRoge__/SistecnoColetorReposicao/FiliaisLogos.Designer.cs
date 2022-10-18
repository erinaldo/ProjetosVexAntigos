namespace SistecnoColetor
{
    partial class FiliaisLogos
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
            this.cboEmpresa = new System.Windows.Forms.ComboBox();
            this.cboFilial = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLogar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.Location = new System.Drawing.Point(3, 65);
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Size = new System.Drawing.Size(234, 22);
            this.cboEmpresa.TabIndex = 0;
            this.cboEmpresa.SelectedIndexChanged += new System.EventHandler(this.cboEmpresa_SelectedIndexChanged);
            // 
            // cboFilial
            // 
            this.cboFilial.Location = new System.Drawing.Point(3, 163);
            this.cboFilial.Name = "cboFilial";
            this.cboFilial.Size = new System.Drawing.Size(234, 22);
            this.cboFilial.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.Text = "Empresa";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.Text = "Filial";
            // 
            // btnLogar
            // 
            this.btnLogar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnLogar.Location = new System.Drawing.Point(20, 233);
            this.btnLogar.Name = "btnLogar";
            this.btnLogar.Size = new System.Drawing.Size(200, 35);
            this.btnLogar.TabIndex = 13;
            this.btnLogar.Text = "Confirma";
            this.btnLogar.Click += new System.EventHandler(this.btnLogar_Click);
            // 
            // FiliaisLogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.btnLogar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboFilial);
            this.Controls.Add(this.cboEmpresa);
            this.Name = "FiliaisLogos";
            this.Text = "FiliaisLogos";
            this.Load += new System.EventHandler(this.FiliaisLogos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboEmpresa;
        private System.Windows.Forms.ComboBox cboFilial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogar;
    }
}