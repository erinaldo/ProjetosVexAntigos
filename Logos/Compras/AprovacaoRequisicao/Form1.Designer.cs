namespace AprovacaoRequisicao
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnAprovacaoRequisicao = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txttempo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAprovacaoCotacao = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAprovacaoRequisicao
            // 
            this.btnAprovacaoRequisicao.Location = new System.Drawing.Point(4, 12);
            this.btnAprovacaoRequisicao.Name = "btnAprovacaoRequisicao";
            this.btnAprovacaoRequisicao.Size = new System.Drawing.Size(104, 59);
            this.btnAprovacaoRequisicao.TabIndex = 0;
            this.btnAprovacaoRequisicao.Text = "Aprovação Requisição";
            this.btnAprovacaoRequisicao.UseVisualStyleBackColor = true;
            this.btnAprovacaoRequisicao.Click += new System.EventHandler(this.btnAprovacaoRequisicao_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txttempo
            // 
            this.txttempo.Location = new System.Drawing.Point(317, 77);
            this.txttempo.Name = "txttempo";
            this.txttempo.Size = new System.Drawing.Size(42, 20);
            this.txttempo.TabIndex = 1;
            this.txttempo.Text = "1440";
            this.txttempo.TextChanged += new System.EventHandler(this.txttempo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Intervalo em minutos:";
            // 
            // btnAprovacaoCotacao
            // 
            this.btnAprovacaoCotacao.Location = new System.Drawing.Point(133, 12);
            this.btnAprovacaoCotacao.Name = "btnAprovacaoCotacao";
            this.btnAprovacaoCotacao.Size = new System.Drawing.Size(104, 59);
            this.btnAprovacaoCotacao.TabIndex = 3;
            this.btnAprovacaoCotacao.Text = "Aprovação Cotação";
            this.btnAprovacaoCotacao.UseVisualStyleBackColor = true;
            this.btnAprovacaoCotacao.Click += new System.EventHandler(this.btnAprovacaoCotacao_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(204, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tempo";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(351, 51);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 59);
            this.button1.TabIndex = 6;
            this.button1.Text = "Aprovação Pedido de Compra";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 178);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAprovacaoCotacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txttempo);
            this.Controls.Add(this.btnAprovacaoRequisicao);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Robo de Aprovações Requisição/Cotação";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAprovacaoRequisicao;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txttempo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAprovacaoCotacao;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}

