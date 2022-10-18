namespace SistecnoColetor
{
    partial class ConfVolume
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfVolume));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.grdVolumes = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Cod = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnConfirmarVolume = new System.Windows.Forms.Button();
            this.txtCbVolume = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalDevolume = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Excluir";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click_1);
            // 
            // grdVolumes
            // 
            this.grdVolumes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdVolumes.ContextMenu = this.contextMenu1;
            this.grdVolumes.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.grdVolumes.Location = new System.Drawing.Point(3, 68);
            this.grdVolumes.Name = "grdVolumes";
            this.grdVolumes.Size = new System.Drawing.Size(235, 168);
            this.grdVolumes.TabIndex = 29;
            this.grdVolumes.TableStyles.Add(this.dataGridTableStyle1);
            this.grdVolumes.CurrentCellChanged += new System.EventHandler(this.grdVolumes_CurrentCellChanged);
            this.grdVolumes.Click += new System.EventHandler(this.grdVolumes_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Cod);
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "IdReposicaoRogeVolume";
            this.dataGridTextBoxColumn1.MappingName = "CODIGO";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // Cod
            // 
            this.Cod.Format = "";
            this.Cod.FormatInfo = null;
            this.Cod.HeaderText = "VOLUME";
            this.Cod.MappingName = "VOLUME";
            this.Cod.Width = 300;
            // 
            // btnConfirmarVolume
            // 
            this.btnConfirmarVolume.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmarVolume.Location = new System.Drawing.Point(3, 242);
            this.btnConfirmarVolume.Name = "btnConfirmarVolume";
            this.btnConfirmarVolume.Size = new System.Drawing.Size(232, 24);
            this.btnConfirmarVolume.TabIndex = 28;
            this.btnConfirmarVolume.Text = "Confirmar Volumes";
            this.btnConfirmarVolume.Click += new System.EventHandler(this.btnConfirmarVolume_Click_1);
            // 
            // txtCbVolume
            // 
            this.txtCbVolume.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.txtCbVolume.Location = new System.Drawing.Point(3, 41);
            this.txtCbVolume.Name = "txtCbVolume";
            this.txtCbVolume.Size = new System.Drawing.Size(235, 18);
            this.txtCbVolume.TabIndex = 27;
            this.txtCbVolume.Text = "670103010102207140690000000120150523000169";
            this.txtCbVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCbVolume_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 20);
            this.label1.Text = "Codigo de Barras do Volume:";
            // 
            // lblTotalDevolume
            // 
            this.lblTotalDevolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalDevolume.Location = new System.Drawing.Point(0, 0);
            this.lblTotalDevolume.Name = "lblTotalDevolume";
            this.lblTotalDevolume.Size = new System.Drawing.Size(237, 20);
            // 
            // ConfVolume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblTotalDevolume);
            this.Controls.Add(this.grdVolumes);
            this.Controls.Add(this.btnConfirmarVolume);
            this.Controls.Add(this.txtCbVolume);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfVolume";
            this.Text = "Volumes";
            this.Load += new System.EventHandler(this.ConfVolume_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.DataGrid grdVolumes;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.Button btnConfirmarVolume;
        private System.Windows.Forms.TextBox txtCbVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn Cod;
        private System.Windows.Forms.Label lblTotalDevolume;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
    }
}