namespace SistecnoColetor
{
    partial class ConfVeiculos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlConItens = new System.Windows.Forms.Panel();
            this.txtVolumeConferencia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlBandeira = new System.Windows.Forms.Panel();
            this.txtCodigoCarreta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlConItens.SuspendLayout();
            this.pnlBandeira.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 268);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.pnlConItens);
            this.tabPage1.Controls.Add(this.pnlBandeira);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 245);
            this.tabPage1.Text = "Conferência";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 20);
            this.button1.TabIndex = 14;
            this.button1.Text = "Fechar Conferencia";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnlConItens
            // 
            this.pnlConItens.Controls.Add(this.txtVolumeConferencia);
            this.pnlConItens.Controls.Add(this.label5);
            this.pnlConItens.Enabled = false;
            this.pnlConItens.Location = new System.Drawing.Point(6, 143);
            this.pnlConItens.Name = "pnlConItens";
            this.pnlConItens.Size = new System.Drawing.Size(233, 49);
            // 
            // txtVolumeConferencia
            // 
            this.txtVolumeConferencia.Location = new System.Drawing.Point(3, 25);
            this.txtVolumeConferencia.MaxLength = 42;
            this.txtVolumeConferencia.Name = "txtVolumeConferencia";
            this.txtVolumeConferencia.Size = new System.Drawing.Size(227, 21);
            this.txtVolumeConferencia.TabIndex = 5;
            this.txtVolumeConferencia.TextChanged += new System.EventHandler(this.txtVolumeConferencia_TextChanged_1);
            this.txtVolumeConferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVolumeConferencia_KeyPress_1);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 18);
            this.label5.Text = "Codigo de Barras Gaiola";
            // 
            // pnlBandeira
            // 
            this.pnlBandeira.Controls.Add(this.txtCodigoCarreta);
            this.pnlBandeira.Controls.Add(this.label4);
            this.pnlBandeira.Location = new System.Drawing.Point(5, 64);
            this.pnlBandeira.Name = "pnlBandeira";
            this.pnlBandeira.Size = new System.Drawing.Size(233, 51);
            // 
            // txtCodigoCarreta
            // 
            this.txtCodigoCarreta.Location = new System.Drawing.Point(3, 25);
            this.txtCodigoCarreta.MaxLength = 100;
            this.txtCodigoCarreta.Name = "txtCodigoCarreta";
            this.txtCodigoCarreta.Size = new System.Drawing.Size(227, 21);
            this.txtCodigoCarreta.TabIndex = 5;
            this.txtCodigoCarreta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBandeiraConferencia_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 19);
            this.label4.Text = "Codigo de Barras Carreta";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(5, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 35);
            this.label3.Text = "Leia o Codigo de Barras da Bandeira da carreta";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGrid1);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 245);
            this.tabPage2.Text = "Lançamento";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(240, 245);
            this.dataGrid1.TabIndex = 0;
            // 
            // ConfVeiculos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.Name = "ConfVeiculos";
            this.Text = "Conferência de Carretas";
            this.Load += new System.EventHandler(this.ConfVeiculos_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlConItens.ResumeLayout(false);
            this.pnlBandeira.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlBandeira;
        private System.Windows.Forms.TextBox txtCodigoCarreta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlConItens;
        private System.Windows.Forms.TextBox txtVolumeConferencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid dataGrid1;
    }
}