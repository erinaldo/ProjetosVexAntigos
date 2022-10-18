namespace WebService
{
    partial class fWS
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
            this.gbAccess = new System.Windows.Forms.GroupBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.lPass = new System.Windows.Forms.Label();
            this.lUser = new System.Windows.Forms.Label();
            this.lUrl = new System.Windows.Forms.Label();
            this.gbParam = new System.Windows.Forms.GroupBox();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.lMethod = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lValue = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbAwnser = new System.Windows.Forms.TextBox();
            this.bSearch = new System.Windows.Forms.Button();
            this.gbAccess.SuspendLayout();
            this.gbParam.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAccess
            // 
            this.gbAccess.Controls.Add(this.tbPass);
            this.gbAccess.Controls.Add(this.tbUser);
            this.gbAccess.Controls.Add(this.tbUrl);
            this.gbAccess.Controls.Add(this.lPass);
            this.gbAccess.Controls.Add(this.lUser);
            this.gbAccess.Controls.Add(this.lUrl);
            this.gbAccess.Location = new System.Drawing.Point(19, 7);
            this.gbAccess.Name = "gbAccess";
            this.gbAccess.Size = new System.Drawing.Size(587, 97);
            this.gbAccess.TabIndex = 0;
            this.gbAccess.TabStop = false;
            this.gbAccess.Text = "Dados de Acesso";
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(66, 68);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(136, 20);
            this.tbPass.TabIndex = 3;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(66, 41);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(136, 20);
            this.tbUser.TabIndex = 2;
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(66, 15);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(484, 20);
            this.tbUrl.TabIndex = 1;
            // 
            // lPass
            // 
            this.lPass.AutoSize = true;
            this.lPass.Location = new System.Drawing.Point(19, 71);
            this.lPass.Name = "lPass";
            this.lPass.Size = new System.Drawing.Size(41, 13);
            this.lPass.TabIndex = 2;
            this.lPass.Text = "Senha:";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(14, 44);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(46, 13);
            this.lUser.TabIndex = 1;
            this.lUser.Text = "Usuário:";
            // 
            // lUrl
            // 
            this.lUrl.AutoSize = true;
            this.lUrl.Location = new System.Drawing.Point(37, 18);
            this.lUrl.Name = "lUrl";
            this.lUrl.Size = new System.Drawing.Size(23, 13);
            this.lUrl.TabIndex = 0;
            this.lUrl.Text = "Url:";
            // 
            // gbParam
            // 
            this.gbParam.Controls.Add(this.cbMethod);
            this.gbParam.Controls.Add(this.lMethod);
            this.gbParam.Controls.Add(this.tbValue);
            this.gbParam.Controls.Add(this.lValue);
            this.gbParam.Location = new System.Drawing.Point(19, 110);
            this.gbParam.Name = "gbParam";
            this.gbParam.Size = new System.Drawing.Size(587, 71);
            this.gbParam.TabIndex = 6;
            this.gbParam.TabStop = false;
            this.gbParam.Text = "Parâmetros";
            // 
            // cbMethod
            // 
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Items.AddRange(new object[] {
            "getDocumentsFromPOD",
            "getADocumentFromPOD"});
            this.cbMethod.Location = new System.Drawing.Point(66, 13);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(159, 21);
            this.cbMethod.TabIndex = 4;
            this.cbMethod.SelectedIndexChanged += new System.EventHandler(this.cbMethod_SelectedIndexChanged);
            // 
            // lMethod
            // 
            this.lMethod.AutoSize = true;
            this.lMethod.Location = new System.Drawing.Point(14, 16);
            this.lMethod.Name = "lMethod";
            this.lMethod.Size = new System.Drawing.Size(46, 13);
            this.lMethod.TabIndex = 6;
            this.lMethod.Text = "Método:";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(66, 41);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(484, 20);
            this.tbValue.TabIndex = 5;
            // 
            // lValue
            // 
            this.lValue.AutoSize = true;
            this.lValue.Location = new System.Drawing.Point(23, 44);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(34, 13);
            this.lValue.TabIndex = 2;
            this.lValue.Text = "Valor:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbAwnser);
            this.groupBox1.Location = new System.Drawing.Point(19, 218);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 211);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parâmetros";
            // 
            // tbAwnser
            // 
            this.tbAwnser.Location = new System.Drawing.Point(6, 19);
            this.tbAwnser.Multiline = true;
            this.tbAwnser.Name = "tbAwnser";
            this.tbAwnser.ReadOnly = true;
            this.tbAwnser.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbAwnser.Size = new System.Drawing.Size(575, 186);
            this.tbAwnser.TabIndex = 0;
            // 
            // bSearch
            // 
            this.bSearch.Location = new System.Drawing.Point(235, 187);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(155, 25);
            this.bSearch.TabIndex = 6;
            this.bSearch.Text = "Buscar";
            this.bSearch.UseVisualStyleBackColor = true;
            this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
            // 
            // fWS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.bSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbParam);
            this.Controls.Add(this.gbAccess);
            this.Name = "fWS";
            this.Text = "WebService";
            this.gbAccess.ResumeLayout(false);
            this.gbAccess.PerformLayout();
            this.gbParam.ResumeLayout(false);
            this.gbParam.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAccess;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Label lPass;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.Label lUrl;
        private System.Windows.Forms.GroupBox gbParam;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.Label lMethod;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bSearch;
        private System.Windows.Forms.TextBox tbAwnser;
    }
}

