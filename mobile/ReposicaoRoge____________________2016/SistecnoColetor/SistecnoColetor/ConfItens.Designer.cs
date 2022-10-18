﻿namespace SistecnoColetor
{
    partial class ConfItens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfItens));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.grdVolumes = new System.Windows.Forms.DataGrid();
            this.btnConfirmarVolume = new System.Windows.Forms.Button();
            this.txtCbItem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalDevolume = new System.Windows.Forms.Label();
            this.dataGridTableStyle2 = new System.Windows.Forms.DataGridTableStyle();
            this.ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QUANTIDADE = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.grdVolumes.Location = new System.Drawing.Point(3, 68);
            this.grdVolumes.Name = "grdVolumes";
            this.grdVolumes.Size = new System.Drawing.Size(235, 168);
            this.grdVolumes.TabIndex = 29;
            this.grdVolumes.TableStyles.Add(this.dataGridTableStyle2);
            this.grdVolumes.CurrentCellChanged += new System.EventHandler(this.grdVolumes_CurrentCellChanged);
            this.grdVolumes.Click += new System.EventHandler(this.grdVolumes_Click);
            // 
            // btnConfirmarVolume
            // 
            this.btnConfirmarVolume.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnConfirmarVolume.Location = new System.Drawing.Point(3, 242);
            this.btnConfirmarVolume.Name = "btnConfirmarVolume";
            this.btnConfirmarVolume.Size = new System.Drawing.Size(232, 24);
            this.btnConfirmarVolume.TabIndex = 28;
            this.btnConfirmarVolume.Text = "Confirmar Itens";
            this.btnConfirmarVolume.Click += new System.EventHandler(this.btnConfirmarVolume_Click_1);
            // 
            // txtCbItem
            // 
            this.txtCbItem.Location = new System.Drawing.Point(3, 41);
            this.txtCbItem.Name = "txtCbItem";
            this.txtCbItem.Size = new System.Drawing.Size(235, 21);
            this.txtCbItem.TabIndex = 27;
            this.txtCbItem.Text = "7896000705983";
            this.txtCbItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCbVolume_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 20);
            this.label1.Text = "Codigo de Barras do Item:";
            // 
            // lblTotalDevolume
            // 
            this.lblTotalDevolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalDevolume.Location = new System.Drawing.Point(0, 0);
            this.lblTotalDevolume.Name = "lblTotalDevolume";
            this.lblTotalDevolume.Size = new System.Drawing.Size(237, 20);
            // 
            // dataGridTableStyle2
            // 
            this.dataGridTableStyle2.GridColumnStyles.Add(this.ID);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.CODIGO);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.QUANTIDADE);
            // 
            // ID
            // 
            this.ID.Format = "";
            this.ID.FormatInfo = null;
            this.ID.HeaderText = "ID";
            this.ID.MappingName = "ID";
            this.ID.Width = 0;
            // 
            // CODIGO
            // 
            this.CODIGO.Format = "";
            this.CODIGO.FormatInfo = null;
            this.CODIGO.HeaderText = "CODIGO";
            this.CODIGO.MappingName = "CODIGO";
            this.CODIGO.Width = 100;
            // 
            // QUANTIDADE
            // 
            this.QUANTIDADE.Format = "";
            this.QUANTIDADE.FormatInfo = null;
            this.QUANTIDADE.HeaderText = "QUANTIDADE";
            this.QUANTIDADE.MappingName = "QUANTIDADE";
            // 
            // ConfItens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblTotalDevolume);
            this.Controls.Add(this.grdVolumes);
            this.Controls.Add(this.btnConfirmarVolume);
            this.Controls.Add(this.txtCbItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfItens";
            this.Text = "ConfItens";
            this.Load += new System.EventHandler(this.ConfVolume_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.DataGrid grdVolumes;
        private System.Windows.Forms.Button btnConfirmarVolume;
        private System.Windows.Forms.TextBox txtCbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalDevolume;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle2;
        private System.Windows.Forms.DataGridTextBoxColumn ID;
        private System.Windows.Forms.DataGridTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridTextBoxColumn QUANTIDADE;
    }
}