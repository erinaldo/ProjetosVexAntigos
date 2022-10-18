using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace ServicosWEB.WSS
{
    /// <summary>
    /// Summary description for wssLogin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wssLogin : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        string cnx = "";

        [WebMethod]
        public DataTable RetornarEmpresa()
        {
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "";
            sql = "SELECT IDEMPRESA, NOME FROM EMPRESA WHERE ATIVO='SIM'  ORDER BY 2";
            return Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
        }

        [WebMethod]
        public DataTable RetornarFilial()
        {
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "";
            sql = "SELECT IDFILIAL, NOME, IDEMPRESA  FROM FILIAL WHERE ATIVO = 'SIM' ORDER BY NOME";
            return Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
        }



        [WebMethod]
        public SistecnoColetor.Classes.DTO.Usuario Logar(SistecnoColetor.Classes.DTO.Usuario usuario)
        {
            try
            {
                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                string sql = "SELECT *, F.NOME FILIAL, E.NOME EMPRESA FROM USUARIO U LEFT JOIN EMPRESA E ON E.IDEMPRESA = U.ULTIMAEMPRESA LEFT JOIN FILIAL F ON F.IDFILIAL = U.ULTIMAFILIAL WHERE LOGIN='" + usuario.Login + "' AND SENHA='" + usuario.Senha + "'";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
                SistecnoColetor.Classes.DTO.Usuario ret = new SistecnoColetor.Classes.DTO.Usuario();
                
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ATIVO"].ToString() == "NAO")
                        throw new Exception("Usuário Inativo");

                    ret.IDUsuario = int.Parse(dt.Rows[0]["IDUsuario"].ToString());
                    ret.IDCadastro = int.Parse(dt.Rows[0]["IDCadastro"].ToString());
                    ret.IDGrupo = (dt.Rows[0]["IDGrupo"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDGrupo"]);
                    ret.IDPerfil = (dt.Rows[0]["IDPerfil"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDPerfil"]);
                    ret.UltimaEmpresa = (dt.Rows[0]["UltimaEmpresa"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaEmpresa"]);
                    ret.UltimaFilial = (dt.Rows[0]["UltimaFilial"] == DBNull.Value ? null : (int?)dt.Rows[0]["UltimaFilial"]);
                    ret.Nome = dt.Rows[0]["nome"].ToString().ToUpper();
                    ret.Login = usuario.Login.ToUpper();
                    ret.Senha = usuario.Senha;
                    ret.TipoDeSistema = dt.Rows[0]["TipoDeSistema"].ToString();
                    ret.NomeEmpresa = dt.Rows[0]["Empresa"].ToString();
                    ret.NomeFilial = dt.Rows[0]["Filial"].ToString();
                    return ret;
                }
                else
                    throw new Exception("Usuário não encontrado!!!");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[WebMethod]
        //public DataTable RetornarFilial(int idempresa, int? idFilial)
        //{
        //    string sql = "";
        //    cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

        //    if (idFilial == null)
        //        sql = "SELECT IDFILIAL ID, NOME FILIAL, * FROM FILIAL WHERE ATIVO='SIM' AND IDEMPRESA = " + idempresa + " ORDER BY NOME";
        //    else
        //        sql = " SELECT TOP 1 EST.NOME ESTADO, CID.NOME CIDADE, BAR.NOME BAIRRO, CCE.ENDERECO EMAIL,* FROM FILIAL FIL  INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = FIL.IDCADASTRO  LEFT JOIN CIDADE CID ON CID.IDCIDADE = CAD.IDCIDADE LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO LEFT JOIN BAIRRO BAR ON BAR.IDBAIRRO = CAD.IDBAIRRO LEFT JOIN CADASTROCONTATOENDERECO CCE ON CCE.IDCADASTRO = CAD.IDCADASTRO AND IDCADASTROTIPODECONTATO=1  WHERE FIL.IDFILIAL=" + idFilial;
        //    return Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //}

        [WebMethod]
        public void GravaInformacoesEmpresaLogin(SistecnoColetor.Classes.DTO.Usuario usuario)
        {
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "UPDATE USUARIO SET ULTIMOACESSO=GETDATE(), ULTIMAFILIAL=" + usuario.UltimaFilial + ", ULTIMAEMPRESA=" + usuario.UltimaEmpresa + " WHERE IDUSUARIO =" + usuario.IDUsuario;
            Sistran.Library.GetDataTables.ExecutarComandoSql(sql, cnx);

        }
    }
}
namespace SistecnoColetor.Classes.DTO
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public Nullable<int> IDCadastro { get; set; }
        public Nullable<int> IDGrupo { get; set; }
        public Nullable<int> IDPerfil { get; set; }
        public Nullable<int> UltimaEmpresa { get; set; }
        public Nullable<int> UltimaFilial { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeFilial { get; set; }


        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string CriaUsuario { get; set; }
        public string Administrador { get; set; }
        public string AutoOcultarMenu { get; set; }
        public Nullable<int> LarguraMenu { get; set; }
        public Nullable<int> AlturaFavoritos { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Tipo { get; set; }
        public string Ativo { get; set; }
        public string AtivaProtecaoTela { get; set; }
        public Nullable<System.DateTime> SenhaValidaAte { get; set; }
        public string TipoDeSistema { get; set; }
        public Nullable<decimal> ValorLimite { get; set; }
        public Nullable<int> ExpirarSenha { get; set; }
        public string AlterarSenhaNoLogin { get; set; }
        public string Site { get; set; }
        public string ValidarUsuarioNoBD { get; set; }
        public Nullable<System.DateTime> UltimoAcesso { get; set; }
        public string DadosMaquinaLocal { get; set; }
    }
}


