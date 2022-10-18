using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public static class TelaInicialcs
    {       


        public static string IniciarArvoreMenu(string htmlMenu)
        {
            htmlMenu = "<ul>";
            return htmlMenu;
        }

        public static string EncerrarArvoreMenu(string htmlMenu)
        {
            htmlMenu += "</ul>";
            return htmlMenu;
        }

        public static string CriarItemPrincipal(string htmlMenu, string texto, string icone)
        {
            htmlMenu += " <li class=''>";
            htmlMenu += "<a href='#' title='" + texto + "'><i class='fa fa-lg fa-fw " + icone + "'></i><span class='menu-item-parent'>" + texto + "</span></a>";
            return htmlMenu;
        }

        public static string CriarItemPrincipal(string htmlMenu, string texto, string icone, string link)
        {
            htmlMenu += " <li class=''>";
            htmlMenu += "<a href='#' title='" + texto + "' onclick=setarIframe('" + link + "')><i class='fa fa-lg fa-fw " + icone + "'></i><span class='menu-item-parent'>" + texto + "</span></a>";
            return htmlMenu;
        }

        public static string FecharItemPrincipal(string htmlMenu)
        {
            htmlMenu += " </li>";
            return htmlMenu;
        }

        public static string CriarFilho(string htmlMenu)
        {

            return htmlMenu;
        }


        public static DataTable RetonornarItensMenu(int idUsuaario, string cnx)
        {
            return null;
        }

    }
}