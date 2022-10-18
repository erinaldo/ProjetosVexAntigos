using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SistecnoColetor;

namespace SistecnoColetor.Classes.DAL
{
    public class Empresa
    {
        public DataTable RetornarEmpresa(int? idEmpresa, string cnx)
        {
            string sql = "";
            if (idEmpresa == null)
                sql = "SELECT IDEMPRESA ID, * FROM EMPRESA WHERE ATIVO='SIM'  ORDER BY 4";
            else
                sql = " SELECT * FROM EMPRESA EMP INNER  JOIN CADASTRO CAD ON CAD.IDCADASTRO = EMP.IDEMPRESA  WHERE EMP.IDEMPRESA=2 ";

            return Classes.BdExterno.RetornarDT(sql, cnx);
        }

        public DataTable RetornarFilial(int idempresa, int? idFilial, string cnx)
        {
            string sql = "";

            if (idFilial == null)
                sql = "SELECT IDFILIAL ID, NOME FILIAL, * FROM FILIAL WHERE ATIVO='SIM' AND IDEMPRESA = " + idempresa + " ORDER BY NOME";
            else
                sql = " SELECT TOP 1 EST.NOME ESTADO, CID.NOME CIDADE, BAR.NOME BAIRRO, CCE.ENDERECO EMAIL,* FROM FILIAL FIL  INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = FIL.IDCADASTRO  LEFT JOIN CIDADE CID ON CID.IDCIDADE = CAD.IDCIDADE LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO LEFT JOIN BAIRRO BAR ON BAR.IDBAIRRO = CAD.IDBAIRRO LEFT JOIN CADASTROCONTATOENDERECO CCE ON CCE.IDCADASTRO = CAD.IDCADASTRO AND IDCADASTROTIPODECONTATO=1  WHERE FIL.IDFILIAL=" + idFilial;
            return Classes.BdExterno.RetornarDT(sql, cnx);
            
        }

        public void GravaInformacoesEmpresaLogin(Classes.DTO.Usuario usuario, string cnx)
        {

            string sql = "UPDATE USUARIO SET ULTIMOACESSO=GETDATE(), ULTIMAFILIAL=" + usuario.UltimaFilial + ", ULTIMAEMPRESA="+usuario.UltimaEmpresa+" WHERE IDUSUARIO =" + usuario.IDUsuario ;
            Classes.BdExterno.RetornarDT(sql, cnx);
        }
    }

    public class Menu
    {
        public DataTable RetornarMenusPermissionados(int usuario, string cnx)
        {
            string sql = " SELECT * FROM USUARIOOPCAO UO  INNER JOIN MODULOOPCAO MO ON MO.IDMODULOOPCAO = UO.IDMODULOOPCAO WHERE UO.IDUSUARIO =" + usuario + " AND PERMISSAO ='SIM' AND MO.TIPO='CLW' ";
            return Classes.BdExterno.RetornarDT(sql, cnx);

        }
    }

   
}
