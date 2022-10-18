namespace SistecnoColetor
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroColetor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 263);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // treeView1
            // 
            this.treeView1.Indent = 12;
            this.treeView1.Location = new System.Drawing.Point(4, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(234, 202);
            this.treeView1.TabIndex = 15;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnVoltar.Location = new System.Drawing.Point(168, 209);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(69, 22);
            this.btnVoltar.TabIndex = 16;
            this.btnVoltar.Text = "Sair";
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 10);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(25, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 10);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.Text = "Base De Produtos";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Novos";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Completo";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Nº do Coletor";
            // 
            // txtNumeroColetor
            // 
            this.txtNumeroColetor.Location = new System.Drawing.Point(5, 221);
            this.txtNumeroColetor.Name = "txtNumeroColetor";
            this.txtNumeroColetor.Size = new System.Drawing.Size(81, 21);
            this.txtNumeroColetor.TabIndex = 19;
            this.txtNumeroColetor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroColetor_KeyPress);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 285);
            this.Controls.Add(this.txtNumeroColetor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Menu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.Activated += new System.EventHandler(this.Menu_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumeroColetor;
    }
}