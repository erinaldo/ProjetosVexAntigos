namespace SistecnoColetor
{
    partial class ConfGaiolas
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.txtBandeiraConferencia = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabNova = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.tabConf = new System.Windows.Forms.TabPage();
            this.pnlQuantidadeVolumes = new System.Windows.Forms.Panel();
            this.txtQuantidadeDeVolumes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlConItens = new System.Windows.Forms.Panel();
            this.txtVolumeConferencia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlBandeira = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabLancamentos = new System.Windows.Forms.TabPage();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabNova.SuspendLayout();
            this.tabConf.SuspendLayout();
            this.pnlQuantidadeVolumes.SuspendLayout();
            this.pnlConItens.SuspendLayout();
            this.pnlBandeira.SuspendLayout();
            this.tabLancamentos.SuspendLayout();
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
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 22);
            this.label1.Text = "Codigo de Barras Volume";
            // 
            // txtVolume
            // 
            this.txtVolume.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtVolume.Location = new System.Drawing.Point(5, 98);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(232, 23);
            this.txtVolume.TabIndex = 4;
            this.txtVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVolume_KeyPress);
            // 
            // txtBandeiraConferencia
            // 
            this.txtBandeiraConferencia.Location = new System.Drawing.Point(3, 25);
            this.txtBandeiraConferencia.Name = "txtBandeiraConferencia";
            this.txtBandeiraConferencia.Size = new System.Drawing.Size(227, 21);
            this.txtBandeiraConferencia.TabIndex = 5;
            this.txtBandeiraConferencia.TextChanged += new System.EventHandler(this.txtBandeira_TextChanged);
            this.txtBandeiraConferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBandeira_KeyPress);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabNova);
            this.tabControl1.Controls.Add(this.tabConf);
            this.tabControl1.Controls.Add(this.tabLancamentos);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 266);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabNova
            // 
            this.tabNova.Controls.Add(this.label3);
            this.tabNova.Controls.Add(this.txtVolume);
            this.tabNova.Controls.Add(this.label1);
            this.tabNova.Location = new System.Drawing.Point(0, 0);
            this.tabNova.Name = "tabNova";
            this.tabNova.Size = new System.Drawing.Size(240, 243);
            this.tabNova.Text = "Nova Bandeira";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 35);
            this.label3.Text = "Para gerar uma nova bandeira leia um dos itens da Gaiola";
            // 
            // tabConf
            // 
            this.tabConf.Controls.Add(this.pnlQuantidadeVolumes);
            this.tabConf.Controls.Add(this.pnlConItens);
            this.tabConf.Controls.Add(this.pnlBandeira);
            this.tabConf.Controls.Add(this.button1);
            this.tabConf.Controls.Add(this.label2);
            this.tabConf.Location = new System.Drawing.Point(0, 0);
            this.tabConf.Name = "tabConf";
            this.tabConf.Size = new System.Drawing.Size(232, 240);
            this.tabConf.Text = "Conferencia";
            // 
            // pnlQuantidadeVolumes
            // 
            this.pnlQuantidadeVolumes.Controls.Add(this.txtQuantidadeDeVolumes);
            this.pnlQuantidadeVolumes.Controls.Add(this.label6);
            this.pnlQuantidadeVolumes.Location = new System.Drawing.Point(5, 179);
            this.pnlQuantidadeVolumes.Name = "pnlQuantidadeVolumes";
            this.pnlQuantidadeVolumes.Size = new System.Drawing.Size(233, 27);
            this.pnlQuantidadeVolumes.Visible = false;
            // 
            // txtQuantidadeDeVolumes
            // 
            this.txtQuantidadeDeVolumes.Location = new System.Drawing.Point(141, 3);
            this.txtQuantidadeDeVolumes.Name = "txtQuantidadeDeVolumes";
            this.txtQuantidadeDeVolumes.Size = new System.Drawing.Size(87, 21);
            this.txtQuantidadeDeVolumes.TabIndex = 5;
            this.txtQuantidadeDeVolumes.Text = "0";
            this.txtQuantidadeDeVolumes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidadeDeVolumes_KeyPress);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 19);
            this.label6.Text = "Quantidade de Volumes:";
            // 
            // pnlConItens
            // 
            this.pnlConItens.Controls.Add(this.txtVolumeConferencia);
            this.pnlConItens.Controls.Add(this.label5);
            this.pnlConItens.Enabled = false;
            this.pnlConItens.Location = new System.Drawing.Point(3, 124);
            this.pnlConItens.Name = "pnlConItens";
            this.pnlConItens.Size = new System.Drawing.Size(233, 49);
            // 
            // txtVolumeConferencia
            // 
            this.txtVolumeConferencia.Location = new System.Drawing.Point(3, 25);
            this.txtVolumeConferencia.Name = "txtVolumeConferencia";
            this.txtVolumeConferencia.Size = new System.Drawing.Size(227, 21);
            this.txtVolumeConferencia.TabIndex = 5;
            this.txtVolumeConferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVolumeConferencia_KeyPress);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 18);
            this.label5.Text = "Codigo de Barras Volume";
            // 
            // pnlBandeira
            // 
            this.pnlBandeira.Controls.Add(this.txtBandeiraConferencia);
            this.pnlBandeira.Controls.Add(this.label4);
            this.pnlBandeira.Location = new System.Drawing.Point(3, 69);
            this.pnlBandeira.Name = "pnlBandeira";
            this.pnlBandeira.Size = new System.Drawing.Size(233, 51);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 19);
            this.label4.Text = "Codigo de Barras Bandeira";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(234, 20);
            this.button1.TabIndex = 13;
            this.button1.Text = "Fechar Conferencia";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 56);
            this.label2.Text = "Para iniciar a conferêrencia, leia o Codigo de Barras da Bandeira e depois faça a" +
                " leitura dos volumes.";
            // 
            // tabLancamentos
            // 
            this.tabLancamentos.Controls.Add(this.dataGrid1);
            this.tabLancamentos.Location = new System.Drawing.Point(0, 0);
            this.tabLancamentos.Name = "tabLancamentos";
            this.tabLancamentos.Size = new System.Drawing.Size(240, 243);
            this.tabLancamentos.Text = "Lancamentos";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(6, 3);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(230, 233);
            this.dataGrid1.TabIndex = 6;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "ID";
            this.dataGridTextBoxColumn1.MappingName = "ID";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "CODIGO DE BARRAS";
            this.dataGridTextBoxColumn2.MappingName = "CODIGODEBARRAS";
            this.dataGridTextBoxColumn2.Width = 200;
            // 
            // ConfGaiolas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusBar1);
            this.Name = "ConfGaiolas";
            this.Text = "Conferir Gaiola";
            this.Load += new System.EventHandler(this.ConfGaiolas_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabNova.ResumeLayout(false);
            this.tabConf.ResumeLayout(false);
            this.pnlQuantidadeVolumes.ResumeLayout(false);
            this.pnlConItens.ResumeLayout(false);
            this.pnlBandeira.ResumeLayout(false);
            this.tabLancamentos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.TextBox txtBandeiraConferencia;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabNova;
        private System.Windows.Forms.TabPage tabConf;
        private System.Windows.Forms.TabPage tabLancamentos;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlBandeira;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlConItens;
        private System.Windows.Forms.TextBox txtVolumeConferencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlQuantidadeVolumes;
        private System.Windows.Forms.TextBox txtQuantidadeDeVolumes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
    }
}