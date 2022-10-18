namespace SistecnoColetor
{
    partial class CLW00019
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLW00019));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.Romaneio = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Situacao = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblIdConferenciaPallet = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblInfoRomaneio = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.txtUa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodConferencia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(1, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 16);
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
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.None;
            this.tabControl1.Location = new System.Drawing.Point(3, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 251);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGrid1);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(234, 228);
            this.tabPage1.Text = "Pendentes";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 3);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(228, 222);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.DoubleClick += new System.EventHandler(this.dataGrid1_DoubleClick);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Romaneio);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Situacao);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            // 
            // Romaneio
            // 
            this.Romaneio.Format = "";
            this.Romaneio.FormatInfo = null;
            this.Romaneio.HeaderText = "Romaneio";
            this.Romaneio.MappingName = "IDROMANEIO";
            // 
            // Situacao
            // 
            this.Situacao.Format = "";
            this.Situacao.FormatInfo = null;
            this.Situacao.HeaderText = "Situação";
            this.Situacao.MappingName = "SITUACAO";
            this.Situacao.Width = 100;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "OBS";
            this.dataGridTextBoxColumn1.MappingName = "OBS";
            this.dataGridTextBoxColumn1.Width = 200;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblIdConferenciaPallet);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.lblInfoRomaneio);
            this.tabPage2.Controls.Add(this.btnConfirmar);
            this.tabPage2.Controls.Add(this.txtUa);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtCodConferencia);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(234, 228);
            this.tabPage2.Text = "Execução";
            // 
            // lblIdConferenciaPallet
            // 
            this.lblIdConferenciaPallet.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblIdConferenciaPallet.Location = new System.Drawing.Point(7, 174);
            this.lblIdConferenciaPallet.Name = "lblIdConferenciaPallet";
            this.lblIdConferenciaPallet.Size = new System.Drawing.Size(72, 15);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(4, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 22);
            this.button1.TabIndex = 31;
            this.button1.TabStop = false;
            this.button1.Text = "FECHAR CARREGAMENTO";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblInfoRomaneio
            // 
            this.lblInfoRomaneio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.lblInfoRomaneio.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblInfoRomaneio.Location = new System.Drawing.Point(5, 5);
            this.lblInfoRomaneio.Name = "lblInfoRomaneio";
            this.lblInfoRomaneio.Size = new System.Drawing.Size(224, 66);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(147, 169);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(83, 20);
            this.btnConfirmar.TabIndex = 29;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_1);
            // 
            // txtUa
            // 
            this.txtUa.Enabled = false;
            this.txtUa.Location = new System.Drawing.Point(6, 142);
            this.txtUa.Name = "txtUa";
            this.txtUa.Size = new System.Drawing.Size(224, 21);
            this.txtUa.TabIndex = 3;
            this.txtUa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUa_KeyPress);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 15);
            this.label2.Text = "Cód. Barras Pallet(UA)";
            // 
            // txtCodConferencia
            // 
            this.txtCodConferencia.Location = new System.Drawing.Point(5, 100);
            this.txtCodConferencia.Name = "txtCodConferencia";
            this.txtCodConferencia.Size = new System.Drawing.Size(224, 21);
            this.txtCodConferencia.TabIndex = 1;
            this.txtCodConferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodConferencia_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 15);
            this.label1.Text = "Cód. Barras Conferência";
            // 
            // CLW00019
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CLW00019";
            this.Text = "CLW00019";
            this.Load += new System.EventHandler(this.CLW00019_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridTextBoxColumn Romaneio;
        private System.Windows.Forms.DataGridTextBoxColumn Situacao;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodConferencia;
        private System.Windows.Forms.Label lblInfoRomaneio;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblIdConferenciaPallet;
    }
}