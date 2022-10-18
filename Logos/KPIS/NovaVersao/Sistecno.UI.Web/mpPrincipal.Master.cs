using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web
{
    public partial class mpPrincipal : System.Web.UI.MasterPage
    {
        DAL.Models.Usuario usuarioLogado = null;

        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (Session["CNX"] == null)
            {
                Response.Redirect("frmDirecionador.aspx", false);
                return;
            }



            if (!IsPostBack)
            {
                imglogoPrincipal.ImageUrl = Session["logoCliente"].ToString();

                if (((DataTable)Session["USUARIOCLIENTE"]).Rows.Count == 0)
                    lblAmbiente.Text = "INTRANET";
                else
                    lblAmbiente.Text = "CLIENTE TERCEIRO";


                cnx = Session["CNX"].ToString();
                Session["dtUF"] = new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx);


                if (Session["USUARIOLOGADO"] == null)
                {
                    Response.Redirect("index.aspx", false);
                    return;
                }

                usuarioLogado = (Sistecno.DAL.Models.Usuario)Session["USUARIOLOGADO"];
                lblUsuario.Text = usuarioLogado.Nome;
                phMenu.Controls.Clear();


                Session["opcoes"] = new Sistecno.BLL.Menu().RetornarMenuDoUsuario(usuarioLogado.IDUsuario, "", cnx);
                phMenu.Controls.Add(new LiteralControl(new Sistecno.BLL.Menu().RetornarMenuDoUsuario(usuarioLogado.IDUsuario, "", cnx)));


                //VERIFICA SE AS PASTAS DO CLIENTE ESTAO CRIADAS
                CriarPasta();

               // PreencheSiteMap();


            }

            DataTable fil = new Sistecno.BLL.Filial().RetornarTodosCampos((int)usuarioLogado.UltimaFilial, cnx).Tables[0];

            lblFilail.Text = (fil.Rows.Count > 0 ? fil.Rows[0]["Nome1"].ToString() : usuarioLogado.UltimaFilial.ToString()); ;
        }

        private void PreencheSiteMap()
        {

            if (Session["opcoes"] == null)
                return;


            string stringFinal = "";

            if (iframePrinc.Src == "")
                return;

            string[] x = iframePrinc.Src.Split('?');
            DataTable d = (DataTable)Session["opcoes"];

            DataRow[] rr = d.Select("link='"+ x[0] +"'", "");
            if (rr == null || rr.Length==0)
            {
                stringFinal = "Home";
            }
            else
            {
                 stringFinal = "<li>" + rr[0]["DESCRICAO"].ToString().ToUpper() + "</li>";

                string imo = "";
                for (int i = 0; i < 10; i++)
                {

                    if (i == 0)
                        imo = rr[0]["idparente"].ToString();

                    DataRow[] r = d.Select("IdModuloOpcao=" + imo, "");

                    if (r != null)
                    {
                        if (r.Length > 0)
                        {
                            stringFinal = "<li>" + r[0]["DESCRICAO"].ToString().ToUpper() + "</li>" + stringFinal;
                            imo = r[0]["IDPARENTE"].ToString().ToUpper();
                        }
                        else
                        {
                            break;
                        }
                    }



                    if (imo == "")
                        break;
                }
            }

            PlaceHolder1.Controls.Add(new LiteralControl(stringFinal));
        }
    

        private void CriarPasta()
        {
            Sistecno.DAL.Plano p = (Sistecno.DAL.Plano)Session["PLANOCLIENTE"];
            string v = p.NomeCliente.Replace(".", "").Replace("-", "").Replace("-", "").Replace(" ", "").Substring(0, 10) + Sistecno.BLL.Helpers.Util.Validacoes.ZerosEsquerda(p.IdCliente.ToString(), 4);

            //se nao existe o Diretório
            if (!Directory.Exists(MapPath("~/Documentos/Clientes/") + v))
            {
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v);
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/CTe/Log");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/CTe/PDF");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/CTe/ZIP");

                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/CTe/XML");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/MDFe/Log");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/MDFe/XML");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/NFSe/Log");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/NFSe/XML");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/NFe/Log");
                Directory.CreateDirectory(MapPath("~/Documentos/Clientes/") + v + "/NFe/XML");
            }
        }
    }
}