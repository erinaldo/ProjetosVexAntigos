using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SistranBLL
{
    public sealed class Menu
    {
        public static List<SistranMODEL.Menu> GetMenuParent(int user, string Conn)
        {

            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(user)))
                {
                    throw new Exception("Usuario inválido");
                    //erro -  retornar mensagem para preencher usuario e senha
                }
                else
                {
                    //faz a busca do nome do banco no xml - 
                    List<SistranMODEL.Menu> IMenuParent = SistranDAO.Menu.RetornaMenuParent(user, Conn);

                    if (IMenuParent.Count > 0)
                    {
                        return IMenuParent;
                    }
                    else
                    {
                        throw new Exception("Erro ao carrgar os menus pais, por favor entre em contato com o administrador do sistema");
                        //erro -  retornar mensagem para preencher usuario e senha
                    }
                }

            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }

        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable MontarMenu(int IDUsuario)
        {
            return SistranDAO.Menu.MontarMenu(IDUsuario);
        }

        public static DataTable MenuOpcoes()
        {
            return SistranDAO.Menu.MenuOpcoes();
        }

        public static DataTable MenuOpcoesGravados(string idUsuario)
        {
            return SistranDAO.Menu.MenuOpcoesGravados(idUsuario);
        }

         public static void DesabilitarAcessoByIdUsuario(string idUsuario, string cx)
        {
            SistranDAO.Menu.DesabilitarAcessoByIdUsuario(idUsuario, cx);
        }

        public static void InserirHabilitacoes(string idUsuario, string IDModuloOpcao, string Habilitado, string cx)
        {
            SistranDAO.Menu.InserirHabilitacoes(idUsuario, IDModuloOpcao, Habilitado, cx);
        }
    }

    public class Modulo
    {
        public DataTable RetornarModulosWeb()
        {
            return new SistranDAO.Modulo().RetornarModulosWeb();
        }

        public DataTable RetornarTodosModulosMenus()
        {
            return new SistranDAO.Modulo().RetornarTodosModulosMenus();
        }

        public DataTable RetornarPermissoes(int idUsuario)
        {
            return new SistranDAO.Modulo().RetornarPermissoes(idUsuario);
        }
    }
}
