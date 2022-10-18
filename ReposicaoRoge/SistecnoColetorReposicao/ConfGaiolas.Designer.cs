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
            this.button2 = new System.Windows.Forms.Button();
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
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.txtVolume.MaxLength = 60;
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(232, 23);
            this.txtVolume.TabIndex = 4;
            this.txtVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVolume_KeyPress);
            // 
            // txtBandeiraConferencia
            // 
            this.txtBandeiraConferencia.Location = new System.Drawing.Point(3, 25);
            this.txtBandeiraConferencia.MaxLength = 15;
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
            this.tabNova.Click += new System.EventHandler(this.tabNova_Click);
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
            this.tabConf.Controls.Add(this.button2);
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
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(141, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 20);
            this.button2.TabIndex = 15;
            this.button2.Text = "Zerar Itens";
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.txtVolumeConferencia.MaxLength = 42;
            this.txtVolumeConferencia.Name = "txtVolumeConferencia";
            this.txtVolumeConferencia.Size = new System.Drawing.Size(227, 21);
            this.txtVolumeConferencia.TabIndex = 5;
            this.txtVolumeConferencia.TextChanged += new System.EventHandler(this.txtVolumeConferencia_TextChanged);
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
            this.button1.Size = new System.Drawing.Size(132, 20);
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
            this.tabLancamentos.Controls.Add(this.textBox1);
            this.tabLancamentos.Location = new System.Drawing.Point(0, 0);
            this.tabLancamentos.Name = "tabLancamentos";
            this.tabLancamentos.Size = new System.Drawing.Size(232, 240);
            this.tabLancamentos.Text = "Lancamentos";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(232, 240);
            this.textBox1.TabIndex = 0;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
    }
}