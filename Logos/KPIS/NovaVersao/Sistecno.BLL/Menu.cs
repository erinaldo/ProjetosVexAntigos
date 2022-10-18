using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistecno.BLL
{
    public class Menu
    {
        public DataTable RetornarMenusPlanoEmissorCTE(string cnx)
        {
            return new Sistecno.DAL.Menu().RetornarMenusPlanoEmissorCTE(cnx);
        }

        public DataTable RetornarMenuBase(string cnx, int plano)
        {
            return new Sistecno.DAL.Menu().RetornarMenuBase(cnx, plano);
        }

        public DataTable RetornarMenusPlanoPedidos(string cnx)
        {
            return new Sistecno.DAL.Menu().RetornarMenusPlanoPedidos(cnx);
        }

        public DataTable RetornaPermissoes(string cnx, int idUsuario)
        {
            return new Sistecno.DAL.Menu().RetornaPermissoes(cnx, idUsuario);
        }

        public DataTable RetornarMenuBaseColetor(string cnx)
        {
            return new Sistecno.DAL.Menu().RetornarMenuBaseColetor(cnx);
        }

        /// <summary>
        /// Retorna uma string para o novo menu definido para o site usando a tabela MenuSite
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="cnx"></param>
        /// <returns></returns>
        public string RetornarMenuDoUsuario(int idUsuario, string visao, string cnx)
        {
            DataTable dt = new Sistecno.DAL.Menu().RetornarMenuDoUsuario(idUsuario, visao, cnx);

            DataRow[] Modulos = dt.Select("IDPARENTE=0", "");

            string htmlmenu = "";
            htmlmenu = TelaInicialcs.IniciarArvoreMenu(htmlmenu);


            for (int i = 0; i < Modulos.Length; i++)
            {
                htmlmenu = TelaInicialcs.CriarItemPrincipal(htmlmenu, Modulos[i]["DESCRICAO"].ToString(), Modulos[i]["ICONE"].ToString());
                htmlmenu = ProcurarDescendentes(htmlmenu, dt, Modulos[i]["IDMODULOOPCAo"].ToString());
                htmlmenu = TelaInicialcs.FecharItemPrincipal(htmlmenu);
            }


            #region Troca de Filial
            htmlmenu = TelaInicialcs.CriarItemPrincipal(htmlmenu, "Empresa/Filial", "fa-random", "WEB0001.aspx?opc=Empresa|Filial");
            htmlmenu = TelaInicialcs.FecharItemPrincipal(htmlmenu);


            htmlmenu = TelaInicialcs.CriarItemPrincipal(htmlmenu, "Chamados", "fa-comments", "chamados/WEB00200.aspx?opc=Suporte");
            htmlmenu = TelaInicialcs.FecharItemPrincipal(htmlmenu);

            #endregion
            htmlmenu = TelaInicialcs.EncerrarArvoreMenu(htmlmenu);
            return htmlmenu;
        }

        private string ProcurarDescendentes(string htmlmenu, DataTable dt, string p)
        {

            DataRow[] o = dt.Select("IDPARENTE=" + p, "");

            if (o.Length > 0)
            {
                htmlmenu += "<ul>";

                for (int i = 0; i < o.Length; i++)
                {
                    htmlmenu += "<li class=''><a href='#' title='" + o[i]["Descricao"].ToString() + "'  " + (o[i]["link"].ToString() == "" ? " " : " onclick=setarIframe('" +  o[i]["link"].ToString() + "?opc=" + o[i]["Descricao"].ToString().ToUpper().Replace(" ", "|") + "')") + "><span class='menu-item-parent'>" + o[i]["Descricao"].ToString() + "</span></a>";

                    if (dt.Select("IDPARENTE=" + o[i]["IDMODULOOPCAO"].ToString(), "").Length > 0)
                        htmlmenu = ProcurarDescendentes(htmlmenu, dt, o[i]["IDMODULOOPCAO"].ToString());

                    htmlmenu += "</li>";
                }

            }
            htmlmenu += "</ul>";

            return htmlmenu;
        }
    }
}
