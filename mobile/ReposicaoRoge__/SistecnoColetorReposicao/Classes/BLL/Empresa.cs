using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SistecnoColetor.Classes.BLL
{
    public class Empresa
    {
        public DataTable RetornarEmpresa(int? idEmpresa, string cnx)
        {
            return new Classes.DAL.Empresa().RetornarEmpresa(idEmpresa, cnx);
        }

        public DataTable RetornarFilial(int idempresa, int? idFilial, string cnx)
        {
            return new DAL.Empresa().RetornarFilial(idempresa, idFilial, cnx);
        }

        public void GravaInformacoesEmpresaLogin(Classes.DTO.Usuario usuario, string cnx)
        {
            new Classes.DAL.Empresa().GravaInformacoesEmpresaLogin(usuario, cnx);            
        }


    }
    public class Menu
    {
        public DataTable RetornarMenusPermissionados(int idUsuario, string cnx)
        {
          return  new Classes.DAL.Menu().RetornarMenusPermissionados(idUsuario, cnx);
        }
    }
}
