using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistecno.DAL.BD;


namespace Sistecno.UI.Web.HR
{
    public partial class mpPrincipalHR : System.Web.UI.MasterPage
    {
        DAL.Models.Usuario usuarioLogado ;

        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["CNX"] = new Sistecno.DAL.BD.ConexaoPrincipal("").ConexaoLogos();

            if (!IsPostBack)
            {
                imglogoPrincipal.ImageUrl = "http://www.vexlogistica.com.br/3673.jpg";

                lblAmbiente.Text = "CLIENTE";
                cnx = Session["CNX"].ToString();

                lblUsuario.Text = "HOMEREFILL";
                phMenu.Controls.Clear();


                string htm = "<ul> ";
                htm += "  <li class=''> ";
                htm += " 	<a href='#' title='PEDIDOS'> ";
                htm += " 		<i class='fa fa-lg fa-fw fa-clipboard'></i> ";
                htm += " 	<span class='menu-item-parent'>PEDIDOS</span> ";
                htm += " 	</a> ";

                htm += " 	<ul> ";
                htm += " 	<li class=''> ";
                htm += " 		<a href='#' title='ACOMPANHAMENTO DE PEDIDO'   onclick=setarIframe('AcompanhamentoPedidov2.aspx?opc=ACOMPANHAMENTO|DE|PEDIDOS_') > ";
                htm += " 		<span class='menu-item-parent'>ACOMPANHAMENTO DE PEDIDO </span> ";
                htm += " 		</a> ";

                //htm += " 			<ul> ";
                //htm += " 				<li class=''> ";
                //htm += " 					<a href='#' title='Cadastros Básico'   onclick=setarIframe('WEB0002.ASPX?opc=CADASTROS|BÁSICO')> ";
                //htm += " 					<span class='menu-item-parent'>Cadastros Básico</span> ";
                //htm += " 					</a> ";
                //htm += " 				</li> ";

                //htm += " 				<li class=''> ";
                //htm += " 					<a href='#' title=' Proprietários'   onclick=setarIframe('WEB0003.ASPX?opc=|PROPRIETÁRIOS')> ";
                //htm += " 					<span class='menu-item-parent'> Proprietários</span> ";
                //htm += " 					</a> ";
                //htm += " 				</li> ";

                //htm += " 				<li class=''> ";
                //htm += " 					<a href='#' title='Emitentes'   onclick=setarIframe('WEB0004.ASPX?opc=EMITENTES')> ";
                //htm += " 					<span class='menu-item-parent'>Emitentes</span> ";
                //htm += " 					</a> ";
                //htm += " 				</li> ";

                //htm += " 				<li class=''> ";
                //htm += " 					<a href='#' title='Motoristas'   onclick=setarIframe('WEB0005.ASPX?opc=MOTORISTAS')> ";
                //htm += " 					<span class='menu-item-parent'>Motoristas</span> ";
                //htm += " 					</a> ";
                //htm += " 				</li> ";

                //htm += " 				<li class=''> ";
                //htm += " 					<a href='#' title='Cadastro de Veiculos'   onclick=setarIframe('web0006.aspx?opc=CADASTRO|DE|VEICULOS')> ";
                //htm += " 					<span class='menu-item-parent'>Cadastro de Veiculos</span> ";
                //htm += " 					</a> ";
                //htm += " </li> ";

                //htm += " 				<li class=''> ";
                //htm += " 					<a href='#' title='Coletas'   onclick=setarIframe('WEB0009.ASPX?opc=COLETAS')> ";
                //htm += " 					<span class='menu-item-parent'>Coletas</span> ";
                //htm += " 					</a> ";
                //htm += " 				</li> ";
               // htm += " 		</ul> ";
                htm += " 	</li> ";
                htm += " 	</ul>  ";
                htm += " 	</li> 	 ";

                htm += " </ul> ";
                phMenu.Controls.Add(new LiteralControl(htm));

                //VERIFICA SE AS PASTAS DO CLIENTE ESTAO CRIADAS
               // PreencheSiteMap();


            }

           // DataTable fil = new Sistecno.BLL.Filial().RetornarTodosCampos((int)usuarioLogado.UltimaFilial, cnx).Tables[0];

            lblFilail.Text = "MARKETPLACE";
        }

        //private void PreencheSiteMap()
        //{
        //    if (iframePrinc.Src == "")
        //        return;

        //    string[] x = iframePrinc.Src.Split('?');
        //    DataTable d = (DataTable)Session["opcoes"];

        //    DataRow[] rr = d.Select("link='"+ x[0] +"'", "");
        //    string stringFinal = "<li>" + rr[0]["DESCRICAO"].ToString().ToUpper() + "</li>";

        //    string imo = "";
        //    for (int i = 0; i < 10; i++)
        //    {

        //        if(i==0)
        //        imo = rr[0]["idparente"].ToString();

        //        DataRow[] r = d.Select("IdModuloOpcao=" + imo, "");

        //        if (r != null )
        //        {
        //            if (r.Length > 0)
        //            {
        //                stringFinal = "<li>" + r[0]["DESCRICAO"].ToString().ToUpper() + "</li>" + stringFinal;
        //                imo = r[0]["IDPARENTE"].ToString().ToUpper();
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }



        //        if (imo == "")
        //            break;
        //    }


        //    PlaceHolder1.Controls.Add(new LiteralControl(stringFinal));
        //}
    

        private void CriarPast()
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

        
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            iframePrinc.Src = "AcompanhamentoPedidoV2.aspx?opc=Acompanhamento de Pedidos";
        }
    }
}