using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistecnoColetor
{
    public partial class Inicial : Form
    {
        public Inicial()
        {
            InitializeComponent();
        }

        string cnx = "";
        Classes.DTO.Usuario user;
        public Inicial(Classes.DTO.Usuario User, string Cnx)
        {
            cnx = Cnx;
            user = User;
            InitializeComponent();
        }

        private void Inicial_Load(object sender, EventArgs e)
        {           
                CarregarCboEmpresa();
        }

        private void CarregarCboEmpresaLogos()
        {

            DataTable d = Classes.BbColetor.RetornarDataTable("Select * from Empresa order by NomeEmpresa");
            Classes.Combo.CarregarCombo(d, ref cboEmpresa, true, "IdEmpresa", "NomeEmpresa");

            //if (user.UltimaEmpresa != null)
            //{
            //    for (int i = 0; i < cboEmpresa.Items.Count; i++)
            //    {
            //        if (cboEmpresa.Items[i].ToString().Split('-')[0] == user.UltimaEmpresa.ToString())
            //        {
            //            cboEmpresa.SelectedIndex = i;                        
            //            break;
            //        }
            //    }
            //}
            //else
                cboEmpresa.SelectedIndex = 1;

            CarregarCboFilialLogos(int.Parse(cboEmpresa.SelectedItem.ToString().Split('-')[0]));

            if (user.UltimaFilial != null)
            {
                for (int i = 0; i < cboFilial.Items.Count; i++)
                {
                    if (cboFilial.Items[i].ToString().Split('-')[0] == user.UltimaFilial.ToString())
                    {
                        cboFilial.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
                cboFilial.SelectedIndex = 1;

        }

        private void CarregarCboEmpresa()
        {
            DataTable d = new Classes.BLL.Empresa().RetornarEmpresa(null, cnx);
            Classes.Combo.CarregarCombo(d, ref cboEmpresa, true, "ID", "NOME");

            if (user.UltimaEmpresa != null)
            {
                for (int i = 0; i < cboEmpresa.Items.Count; i++)
                {
                    if (cboEmpresa.Items[i].ToString().Split('-')[0] == user.UltimaEmpresa.ToString())
                    {
                        cboEmpresa.SelectedIndex =  i;
                        break;
                    }
                }
            }
            else
                cboEmpresa.SelectedIndex = 1;

            CarregarCboFilial(int.Parse(cboEmpresa.SelectedItem.ToString().Split('-')[0]));

            if (user.UltimaFilial != null)
            {
                for (int i = 0; i < cboFilial.Items.Count; i++)
                {
                    if (cboFilial.Items[i].ToString().Split('-')[0] == user.UltimaFilial.ToString())
                    {
                        cboFilial.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
                cboFilial.SelectedIndex = 1;
            
        }

        private void CarregarCboFilialLogos(int idEmpresa)
        {
            
            try
            {
                cboFilial.Items.Clear();
                DataTable d = Classes.BbColetor.RetornarDataTable("Select * from Filial where IdEmpresa=" + idEmpresa);
                Classes.Combo.CarregarCombo(d, ref cboFilial, true, "IdFilial", "NOME");

            }
            catch (Exception)
            {
            }
            
        }
        
        private void CarregarCboFilial(int idEmpresa)
        {
            try
            {
                cboFilial.Items.Clear();
                DataTable d = new Classes.BLL.Empresa().RetornarFilial(idEmpresa, null, cnx);
                Classes.Combo.CarregarCombo(d, ref cboFilial, true, "ID", "NOME");

            }
            catch (Exception)
            {
            }
        }

        private void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {           

            try
            {
                CarregarCboFilial(int.Parse(cboEmpresa.SelectedItem.ToString().Split('-')[0]));                
                cboFilial.SelectedIndex = 1;

            }
            catch (Exception)
            {            
            }
        }

        private void SalvarDadosEmpresaFilial()
        {
            if (cboEmpresa.SelectedItem.ToString() == "" || cboFilial.SelectedItem.ToString() == "")
                return;

            user.UltimaEmpresa = int.Parse(cboEmpresa.SelectedItem.ToString().Split('-')[0]);
            user.UltimaFilial = int.Parse(cboFilial.SelectedItem.ToString().Split('-')[0]);
            user.UltimoAcesso = DateTime.Now;
            new Classes.BLL.Empresa().GravaInformacoesEmpresaLogin(user, cnx);
        }

        private void cboFilial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresa.SelectedItem.ToString() == "" || cboFilial.SelectedItem.ToString() == "")
                return;

                        

        }

        private void btnLogar_Click(object sender, EventArgs e)
        {

        }

        private void btnLogar_Click_1(object sender, EventArgs e)
        {
            if(VarGlobal.Usuario.EmpresaClienteLogado !=2)
                SalvarDadosEmpresaFilial();

            user.UltimaEmpresa = int.Parse(cboEmpresa.SelectedItem.ToString().Split('-')[0]);
            user.UltimaFilial = int.Parse(cboFilial.SelectedItem.ToString().Split('-')[0]);

            user.NomeEmpresa = cboEmpresa.SelectedItem.ToString().Split('-')[1];
            user.NomeFilial = cboFilial.SelectedItem.ToString().Split('-')[1];

            Menu f = new Menu(user, cnx);
            f.Show();
            this.Hide();

        }
    }
}