using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web
{
    public partial class WEB0001 : System.Web.UI.Page
    {
        string cnx = "";
        DAL.Models.Usuario usuarioLogado;

        //Sistecno.DAL.Models.Usuario usuarioLogado;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                cnx = Session["CNX"].ToString();
                usuarioLogado = (Sistecno.DAL.Models.Usuario)Session["USUARIOLOGADO"];

                if (!IsPostBack)
                {
                    CarregarCboEmpresa();
                    if (usuarioLogado.UltimaEmpresa > 0)
                    {
                        cboEmpresa.SelectedValue = usuarioLogado.UltimaEmpresa.ToString();
                        CarregarCboFilial(Convert.ToInt32(usuarioLogado.UltimaEmpresa));
                        cboFilial.SelectedValue = usuarioLogado.UltimaFilial.ToString();
                    }
                    else
                    {
                        SavarDadosEmpresaFilial();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void SavarDadosEmpresaFilial()
        {
            usuarioLogado.UltimaEmpresa = int.Parse(cboEmpresa.SelectedValue);
            usuarioLogado.UltimaFilial = int.Parse(cboFilial.SelectedValue);
            usuarioLogado.UltimoAcesso = DateTime.Now;
            Session["USUARIOLOGADO"] = usuarioLogado;

            new Sistecno.BLL.Usuario().GravaInformacoesEmpresaLogin(usuarioLogado, cnx);
        }

        private void CarregarCboEmpresa()
        {
            DataTable d = new Sistecno.BLL.EmpresaFilial().RetornarEmpresa(null, cnx);
            Util.Combo.CarregarCombo(d, ref cboEmpresa, true, false, "ID", "NOME");
            CarregarCboFilial(int.Parse(cboEmpresa.SelectedValue));
        }

        private void CarregarCboFilial(int idEmpresa)
        {

            try
            {
                cboFilial.Items.Clear();
                DataTable d = new Sistecno.BLL.EmpresaFilial().RetornarFilial(idEmpresa, null, cnx);
                Util.Combo.CarregarCombo(d, ref cboFilial, false, false, "ID", "NOME");               

            }
            catch (Exception )
            {
            }
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            SavarDadosEmpresaFilial();
            CarregarSessaoEmpresaFilial();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "window.open('default.aspx', '_top');", true);            
        }

        private void CarregarSessaoEmpresaFilial()
        {
            Sistecno.BLL.EmpresaFilial ef = new Sistecno.BLL.EmpresaFilial();
            Session["Empresa"] = ef.RetornarEmpresa(int.Parse(cboEmpresa.SelectedValue), cnx);
            Session["Filial"] = ef.RetornarFilial(0, int.Parse(cboFilial.SelectedValue), cnx);
        }

        protected void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarCboFilial(int.Parse(cboEmpresa.SelectedValue));
                SavarDadosEmpresaFilial();
                CarregarSessaoEmpresaFilial();

            }
            catch (Exception )
            {               
            }
        }

        protected void cboFilial_SelectedIndexChanged(object sender, EventArgs e)
        {
            SavarDadosEmpresaFilial();
            CarregarSessaoEmpresaFilial();
            CarregarSessaoEmpresaFilial(); 
        }
    }
}