namespace Robo.Email.Notas.Solutions.Windows.Testes
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnTWX = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtIdDt = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.chkHabilitaJosapar = new System.Windows.Forms.CheckBox();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // btnTWX
            // 
            this.btnTWX.Location = new System.Drawing.Point(248, 13);
            this.btnTWX.Name = "btnTWX";
            this.btnTWX.Size = new System.Drawing.Size(110, 60);
            this.btnTWX.TabIndex = 5;
            this.btnTWX.Text = "Processar Comprovei";
            this.btnTWX.UseVisualStyleBackColor = true;
            this.btnTWX.Click += new System.EventHandler(this.btnTWX_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(17, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 60);
            this.button3.TabIndex = 6;
            this.button3.Text = "Ean Roge";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnRoge_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(132, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(110, 60);
            this.button4.TabIndex = 8;
            this.button4.Text = "Enviar Comprovei";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(14, 79);
            this.Label2.Multiline = true;
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1095, 104);
            this.Label2.TabIndex = 12;
            this.Label2.TextChanged += new System.EventHandler(this.Label2_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(729, 218);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(223, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Habilitar envio Home Refill (Conta Própria)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(852, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Enviar Rota de Teste";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(719, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(258, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Enviar Notas do Dt Para Base de Teste ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtIdDt
            // 
            this.txtIdDt.Location = new System.Drawing.Point(671, 189);
            this.txtIdDt.Name = "txtIdDt";
            this.txtIdDt.Size = new System.Drawing.Size(42, 20);
            this.txtIdDt.TabIndex = 16;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button5.Location = new System.Drawing.Point(14, 201);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(213, 61);
            this.button5.TabIndex = 17;
            this.button5.Text = "Emergencia Comprovei";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(376, 230);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(113, 61);
            this.button6.TabIndex = 19;
            this.button6.Text = "Consultar Protocolo";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(729, 245);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(200, 17);
            this.checkBox2.TabIndex = 20;
            this.checkBox2.Text = "Habilitar Recebimento sem Protocolo";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(367, 13);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(110, 60);
            this.button7.TabIndex = 21;
            this.button7.Text = "Processar Somente Imagens";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(483, 12);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(110, 60);
            this.button8.TabIndex = 22;
            this.button8.Text = "Josapar Pedido Teste";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(1027, 307);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 65);
            this.button9.TabIndex = 23;
            this.button9.Text = "Nestle";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(599, 13);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(124, 59);
            this.button10.TabIndex = 24;
            this.button10.Text = "Testar Tracking Josapar";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(729, 12);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(120, 59);
            this.button11.TabIndex = 25;
            this.button11.Text = "Setar Stock Infra";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // chkHabilitaJosapar
            // 
            this.chkHabilitaJosapar.AutoSize = true;
            this.chkHabilitaJosapar.Checked = true;
            this.chkHabilitaJosapar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitaJosapar.Location = new System.Drawing.Point(729, 268);
            this.chkHabilitaJosapar.Name = "chkHabilitaJosapar";
            this.chkHabilitaJosapar.Size = new System.Drawing.Size(104, 17);
            this.chkHabilitaJosapar.TabIndex = 27;
            this.chkHabilitaJosapar.Text = "Habilitar Josapar";
            this.chkHabilitaJosapar.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(855, 12);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(124, 61);
            this.button13.TabIndex = 29;
            this.button13.Text = "Enviar Tracking BD";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(985, 13);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(124, 61);
            this.button12.TabIndex = 30;
            this.button12.Text = "Enviar Tracking WMS BD";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(946, 349);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 31;
            this.button14.Text = "button14";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 289);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(819, 83);
            this.textBox1.TabIndex = 32;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(1034, 189);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 33;
            this.button15.Text = "Enviar Sem Banco";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(248, 230);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 34;
            this.button16.Text = "button16";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(248, 201);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 35;
            this.button17.Text = "button17";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(376, 201);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(217, 23);
            this.button18.TabIndex = 36;
            this.button18.Text = "Relacionar PedNfe Josapar";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 384);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.chkHabilitaJosapar);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtIdDt);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnTWX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Robo de Envio  de Notas  04/10/2022";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnTWX;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox Label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtIdDt;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.CheckBox chkHabilitaJosapar;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.Button button17;
		private System.Windows.Forms.Button button18;
	}
}

