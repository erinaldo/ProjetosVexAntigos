using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistecnoColetor.Classes;

namespace SistecnoColetor
{
    public partial class FiliaisLogos : Form
    {
        Classes.DTO.Usuario user;
        public FiliaisLogos(Classes.DTO.Usuario User)
        {
            user = User;        
            InitializeComponent();
        }

        private void FiliaisLogos_Load(object sender, EventArgs e)
        {
            CarregarEmprsas();
        }

        private void CarregarEmprsas()
        {
            DataTable d = Classes.BbColetor.RetornarDataTable("Select * from Empresa order by NomeEmpresa");
            Combo.CarregarCombo(d, ref cboEmpresa, true, "IdEmpresa", "NomeEmpresa");
        }

        private void CarregarCboFilialLogos(int idEmpresa)
        {

            try
            {
                cboFilial.Items.Clear();
                DataTable d = Classes.BbColetor.RetornarDataTable("Select * from Filial where IdEmpresa=" + idEmpresa);
                Classes.Combo.CarregarCombo(d, ref cboFilial, true, "IdFilial", "Nome");

            }
            catch (Exception)
            {
            }

        }

        private void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresa.SelectedIndex >= 0)
            {
                CarregarCboFilialLogos(int.Parse(cboEmpresa.SelectedItem.ToString().Split('-')[0]));
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            if (cboEmpresa.SelectedIndex > -1 && cboEmpresa.SelectedIndex > -1)
            {
                user.UltimaEmpresa = int.Parse(cboEmpresa.SelectedItem.ToString().Split('-')[0]);
                user.UltimaFilial = int.Parse(cboFilial.SelectedItem.ToString().Split('-')[0]);

                user.NomeEmpresa = cboEmpresa.SelectedItem.ToString().Split('-')[1];
                user.NomeFilial = cboFilial.SelectedItem.ToString().Split('-')[1];

                this.Hide();
                Menu f = new Menu(user, VarGlobal.Conexao);
                f.Show();
               
            }
        }
    }
}