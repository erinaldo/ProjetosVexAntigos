using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public sealed class Usuario
    {
        public string ConsultarLoginByEmail(string email)
        {
            string strsql = "SELECT DISTINCT Login 	FROM CadastroContatoEndereco CADCONEND 	INNER JOIN Usuario USU 	ON(USU.IDCadastro = CADCONEND.IDCadastro)  	/*INNER JOIN Aviso AVI 	ON(AVI.IdUsuario = USU.IDUsuario) */ 	WHERE CADCONEND.Endereco IN ('"+email+"') 	AND USU.Ativo = 'SIM' ";
            return Sistran.Library.GetDataTables.ExecutarRetornoIDs(strsql, "");
        }

        public static List<SistranMODEL.Usuario> RetornaUsuarioLogin_Alone(string usuariologin, string usuarioSenha, string Conn, bool intranet)
        {
            StringBuilder strsql = new StringBuilder();

            strsql.Append(" SELECT ");
            strsql.Append(" U.IDUsuario, ");
            strsql.Append(" U.Nome, ");
            strsql.Append(" U.[Login],  ");
            strsql.Append(" CA.IDCadastro,  ");
            strsql.Append(" CA.FantasiaApelido, U.ATIVO,");
            strsql.Append(" ISNULL(CA.FantasiaApelido , CA.RazaoSocialNome) + ' - ' + ISNULL(CA.CnpjCpf, CA.InscricaoRG ) as RazaoSocialNome ");
            strsql.Append(" FROM Usuario U   WITH (NOLOCK)  ");
            strsql.Append(" LEFT JOIN UsuarioCliente E  WITH (NOLOCK)  ON U.IDUsuario = E.IDUsuario ");
            strsql.Append(" LEFT JOIN Cliente C		 WITH (NOLOCK)  ON E.IDCliente = C.IDCliente ");
            strsql.Append(" LEFT JOIN Cadastro CA		 WITH (NOLOCK)  ON C.IDCliente = CA.IDCadastro ");
            strsql.Append(" WHERE  0=0 ");
            strsql.Append(" AND upper(Senha) = '@Senha' COLLATE SQL_Latin1_General_CP1_CS_AS ");
            strsql.Append(" AND [Login] = '@login' ");
            strsql.Append(" AND Senha = '@Senha' ");
            //strsql.Append(" AND U.Ativo = 'SIM' ");
            //strsql.Append(" AND C.Ativo = 'SIM' ");
            strsql.Append(" AND (C.Ativo = 'SIM' OR C.Ativo IS NULL)");
            if(intranet == false)
                strsql.Append(" AND U.Site = 'ASP' ");

            strsql.Replace("@Senha", usuarioSenha);
            strsql.Replace("@login", usuariologin);


            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            IDbConnection cn = factory.CreateConnection();
            IDbCommand cd = factory.CreateCommand();


            if (HttpContext.Current.Request["cf"] == null)
            {
                cn.ConnectionString = Conn;
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }

            cd.Connection = cn;
            cd.CommandText = strsql.ToString();

            List<SistranMODEL.Usuario> ILUsuario = new List<SistranMODEL.Usuario>();
            cn.Open();
            IDataReader drProfile = cd.ExecuteReader();

            while (drProfile.Read())
            {
                if (drProfile["IDCadastro"].ToString() == "" && intranet==false)
                {
                    return ILUsuario;
                }

                ILUsuario.Add(new SistranMODEL.Usuario(Convert.ToInt32(drProfile["IDUsuario"]),
                    drProfile["Nome"].ToString(),
                    drProfile["Login"].ToString(),
                    Convert.ToInt32(drProfile["IDCadastro"] == DBNull.Value ? 0 : drProfile["IDCadastro"]),
                    drProfile["FantasiaApelido"].ToString(),
                    drProfile["RazaoSocialNome"].ToString(), drProfile["Ativo"].ToString()));
            }
            drProfile.Close();
            cn.Close();

            return ILUsuario;
        }

        public DataTable Listar(string login, string nome, string cpf)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT U.IDUsuario, U.Senha, U.IDCadastro, U.Login, U.Nome, CCE.Endereco AS EMAIL, C.CnpjCpf, U.Ativo , U.IDPerfil, ");
                strsql.Append(" (SELECT TOP 1 UU.Nome FROM Usuario UU WHERE U.IDPerfil = UU.IDUsuario  ) AS PERFIL ");
            strsql.Append(" FROM Usuario  U ");            
            strsql.Append(" INNER JOIN Cadastro C ON U.IDCadastro = C.IDCadastro");
            strsql.Append(" INNER JOIN UsuarioCliente UC ON UC.IDUsuario = U.IDUsuario");
            strsql.Append(" LEFT  JOIN CadastroContato CC ON CC.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroContatoEndereco CCE ON CCE.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroTipoDeContato CTC ON CTC.IDCadastroTipoDeContato = CCE.IDCadastroTipoDeContato ");
            strsql.Append(" WHERE SITE='ASP' AND TIPO='USUARIO'");
            
            if (login != "")
                strsql.Append(" AND  U.Login LIKE '%@Login%' ");

            if (nome != "")
                strsql.Append(" AND U.Nome   LIKE '%@Nome%'");

            if (cpf != "")
                strsql.Append(" AND C.CnpjCpf LIKE '%@CnpjCpf%' ");

            strsql.Append(" AND CTC.IDCadastroTipoDeContato = 1");
            strsql.Replace("@Login", login);
            strsql.Replace("@Nome", nome);
            strsql.Replace("@CnpjCpf", cpf);

            strsql.Append(" order by  U.Login, U.Nome");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable ListarIntranet(string login, string nome, string cpf)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT U.IDUsuario, U.IDCadastro, U.Login, U.Nome, C.CnpjCpf, U.Ativo , U.IDPerfil, (SELECT TOP 1 UU.Nome FROM Usuario UU WHERE U.IDPerfil = UU.IDUsuario  ) AS PERFIL ");
            strsql.Append(" FROM Usuario  U ");
            strsql.Append(" INNER JOIN Cadastro C ON U.IDCadastro = C.IDCadastro");            
            strsql.Append(" LEFT  JOIN CadastroContato CC ON CC.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroContatoEndereco CCE ON CCE.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroTipoDeContato CTC ON CTC.IDCadastroTipoDeContato = CCE.IDCadastroTipoDeContato  AND CTC.IDCadastroTipoDeContato = 1");

            strsql.Append(" WHERE  TIPO='USUARIO' and TIPODESISTEMA <>'ASP' AND ATIVO='SIM'");
            strsql.Append(" AND (CTC.IDCadastroTipoDeContato = 1 OR CTC.IDCadastroTipoDeContato IS NULL)");



            if (login != "")
                strsql.Append(" AND  U.Login LIKE '%@Login%' ");

            if (nome != "")
                strsql.Append(" AND U.Nome   LIKE '%@Nome%'");

            if (cpf != "")
                strsql.Append(" AND C.CnpjCpf LIKE '%@CnpjCpf%' ");

            //strsql.Append(" AND CTC.IDCadastroTipoDeContato = 1");
            strsql.Replace("@Login", login);
            strsql.Replace("@Nome", nome);
            strsql.Replace("@CnpjCpf", cpf);

            strsql.Append(" order by  U.Login, U.Nome");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable Listar(string login, string nome)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT TOP 20 U.IDUsuario, U.IDCadastro, U.Login, U.Nome, CCE.Endereco AS EMAIL, C.CnpjCpf, U.Ativo , U.IDPerfil, (SELECT TOP 1 UU.Nome FROM Usuario UU WHERE U.IDPerfil = UU.IDUsuario  ) AS PERFIL ");
            strsql.Append(" FROM Usuario  U ");

            strsql.Append(" INNER JOIN Cadastro C ON U.IDCadastro = C.IDCadastro");
            strsql.Append(" INNER JOIN UsuarioCliente UC ON UC.IDUsuario = U.IDUsuario");
            strsql.Append(" LEFT  JOIN CadastroContato CC ON CC.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroContatoEndereco CCE ON CCE.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroTipoDeContato CTC ON CTC.IDCadastroTipoDeContato = CCE.IDCadastroTipoDeContato AND CTC.IDCadastroTipoDeContato = 1");

            strsql.Append(" WHERE SITE='ASP' AND TIPO='USUARIO'");


            if (login != "")
                strsql.Append(" AND  U.Login LIKE '%@Login%' ");

            if (nome != "")
                strsql.Append(" AND U.Nome   LIKE '%@Nome%'");

            //if (cpf != "")
            //    strsql.Append(" AND C.CnpjCpf LIKE '%@CnpjCpf%' ");

            strsql.Replace("@Login", login);
            strsql.Replace("@Nome", nome);
            //strsql.Replace("@CnpjCpf", cpf);

            strsql.Append(" order by  U.Login, U.Nome");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable ListarPerfil(string s)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT U.IDUsuario, U.Nome, U.Ativo FROM Usuario  U ");
            strsql.Append(" WHERE TIPO='PERFIL'  AND U.Nome LIKE '%"+ s +"%' AND SITE ='ASP'");         
            strsql.Append(" ORDER BY  U.NOME");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public int inserirPerfil(string Nome)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("USUARIO");
            string strsql = " INSERT INTO USUARIO (SITE ,Login, ";
            strsql += "  IDUsuario,";
            strsql += "  Nome,";
            strsql += "  DataDeCadastro,";
            strsql += "  Tipo, ATIVO";
            strsql += "  ) VALUES ( 'ASP' ,"+ID+", ";
            strsql += "  (SELECT ISNULL(MAX(IDUsuario),0) +1 FROM USUARIO),";            
            strsql += "  '" + Nome.ToUpper().Trim() + "',";
            strsql += "  getdate(),";
            strsql += "  'PERFIL',";
            strsql += "  'SIM'";
            strsql += "  ) ";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
            return Convert.ToInt32(ID);
        }

        public void AlterarPerfil(string IdUsuario, string Nome)
        {

            string strsql = "UPDATE USUARIO SET NOME='" + Nome.ToUpper() + "' WHERE IDUSUARIO=" + IdUsuario;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public DataTable Consultar(string idUsuario)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT distinct U.IDUsuario, U.IDCadastro, U.Login, U.Nome, CCE.Endereco AS EMAIL, C.CnpjCpf, U.SENHA, U.IDPerfil , UC.idcliente CODIGODOCLIENTE, CADCLIE.CNPJCPF + ' - ' + CADCLIE.RAZAOSOCIALNOME  NOMECLIENTE FROM Usuario  U ");
            strsql.Append(" INNER JOIN Cadastro C ON U.IDCadastro = C.IDCadastro");
            strsql.Append(" INNER JOIN UsuarioCliente UC ON UC.IDUsuario = U.IDUsuario");
            strsql.Append(" LEFT  JOIN CadastroContato CC ON CC.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroContatoEndereco CCE ON CCE.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroTipoDeContato CTC ON CTC.IDCadastroTipoDeContato = CCE.IDCadastroTipoDeContato AND CTC.IDCadastroTipoDeContato = 1");
            strsql.Append(" LEFT JOIN CADASTRO CADCLIE ON CADCLIE.IDCADASTRO = UC.IDCLIENTE");
            strsql.Append(" WHERE  U.IDUsuario = " + idUsuario);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable ConsultarIntranet(string idUsuario)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT distinct U.IDUsuario, U.IDCadastro, U.Login, U.Nome, CCE.Endereco AS EMAIL, C.CnpjCpf, U.SENHA, U.IDPerfil FROM Usuario  U ");
            strsql.Append(" INNER JOIN Cadastro C ON U.IDCadastro = C.IDCadastro");
            //strsql.Append(" INNER JOIN UsuarioCliente UC ON UC.IDUsuario = U.IDUsuario");
            strsql.Append(" LEFT  JOIN CadastroContato CC ON CC.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroContatoEndereco CCE ON CCE.IDCadastro = C.IDCadastro");
            strsql.Append(" LEFT JOIN CadastroTipoDeContato CTC ON CTC.IDCadastroTipoDeContato = CCE.IDCadastroTipoDeContato AND CTC.IDCadastroTipoDeContato = 1");
            //strsql.Append(" LEFT JOIN CADASTRO CADCLIE ON CADCLIE.IDCADASTRO = UC.IDCLIENTE");
            strsql.Append(" WHERE  U.IDUsuario = " + idUsuario);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public int Incluir(string IDCadastro, string Nome, string Login, string Senha, string perfil)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("USUARIO");
            string strsql = " INSERT INTO USUARIO (";
            strsql += "  IDUsuario,";
            strsql += "  IDCadastro,";
            strsql += "  IDGrupo,";
            strsql += "  Nome,";
            strsql += "  Login,";
            strsql += "  Senha,";
            strsql += "  Administrador, ";
            strsql += "  AutoOcultarMenu,";
            strsql += "  DataDeCadastro,";
            strsql += "  Tipo,";
            strsql += "  Ativo,";
            strsql += "  SenhaValidaAte,";
            strsql += "  TipoDeSistema ,";
            strsql += "  ExpirarSenha ,";
            strsql += "  AlterarSenhaNoLogin ,";
            strsql += "  Site, Idperfil";
            strsql += "  ) VALUES (";

            strsql += ID+" ,";
            strsql += IDCadastro + "  ,";
            strsql += "  1,";
            strsql += "  '" + Nome.ToUpper().Trim() + "',";
            strsql += "  '" + Login.ToUpper().Trim() + "',";
            strsql += "  '" + Senha.ToUpper().Trim() + "',";
            strsql += "  'NAO', ";
            strsql += "  'NAO',";
            strsql += "  GETDATE(),";
            strsql += "  'USUARIO',";
            strsql += "  'SIM',";
            strsql += "  getdate(),";
            strsql += "  'WEB' ,";
            strsql += "  '30' ,";
            strsql += "  'SIM' ,";
            strsql += "  'ASP', "+ perfil;
            strsql += "  ) ";
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            return Convert.ToInt32(ID);

        }

        public void HabDesbUusario(string IdCadastro, string ACAO)
        {
            string strsql = "UPDATE USUARIO SET ATIVO='"+ACAO+"' WHERE IDCADASTRO=" + IdCadastro;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public DataTable ConsultarPerfil(string idUsuario)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT U.IDUsuario, U.Nome, U.Ativo FROM Usuario  U ");
            strsql.Append(" WHERE TIPO='PERFIL'  AND  IDUSUARIO=" + idUsuario);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public void HabDesbPeril(string IdUsuario, string ACAO)
        {
            string strsql = "UPDATE USUARIO SET ATIVO='" + ACAO + "' WHERE IdUsuario=" + IdUsuario;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public void Alterar(string IdUsuario, string Nome, string Login, string Senha, string cpf, string IdCadastro, string perfil)
        {
            string strsql = "UPDATE USUARIO SET NOME='" + Nome.ToUpper() + "', LOGIN='" + Login.ToUpper() + "', SENHA='" + Senha.ToUpper() + "', IDPERFIL="+perfil+" WHERE IDUSUARIO=" + IdUsuario ;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

            if (cpf.Trim() == "")
                cpf = IdCadastro;

            strsql = "UPDATE CADASTRO SET CnpjCpf='"+cpf+"' WHERE IDCADASTRO= " + IdCadastro;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");


        }

        public void AlterarIntranet(string IdUsuario, string Nome, string Login, string Senha, string cpf, string IdCadastro)
        {
            string strsql = "UPDATE USUARIO SET NOME='" + Nome.ToUpper() + "', LOGIN='" + Login.ToUpper() + "', SENHA='" + Senha.ToUpper() + "' WHERE IDUSUARIO=" + IdUsuario;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

            if (cpf.Trim() == "")
                cpf = IdCadastro;

            strsql = "UPDATE CADASTRO SET CnpjCpf='" + cpf + "' WHERE IDCADASTRO= " + IdCadastro;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");


        }


        
        public sealed class LogBDDAO
        {
            public static void GravarLog(int idUser, string login, string acao, string PaginaProcesso)
            {
                string strsql = "INSERT INTO USUARIOLOG(IDUSUARIO,LOGIN,DATAHORA,ACAO,PAGINAPROCESSO) VALUES (" + idUser + ",'" + login + " (IP:" + HttpContext.Current.Request.UserHostAddress + ")',GETDATE(),'" + acao + "','" + PaginaProcesso + "')";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, HttpContext.Current.Session["Conn"].ToString());

            }

        }

        public class UsuarioCliente
        {
            public int Inserir(string IdUsuario, string IdCliente)
            {
                if (!IdCliente.Contains(","))
                {
                    string ID = Sistran.Library.GetDataTables.RetornarIdTabela("USUARIOCLIENTE");
                    string strsql = "INSERT INTO USUARIOCLIENTE (IDUsuarioCliente, IDUsuario,   IDCliente) VALUES (" + ID + " , " + IdUsuario + ",   " + IdCliente + ") SELECT ISNULL(MAX(IDUsuarioCliente),0) FROM USUARIOCLIENTE";
                    Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
                    return Convert.ToInt32(ID);
                }
                else
                {
                    string[] idsClientes = IdCliente.Split(',');

                    string strsqlS = "DELETE FROM USUARIOCLIENTE WHERE IDUSUARIO=" + IdUsuario;
                    Sistran.Library.GetDataTables.ExecutarRetornoID(strsqlS, "");

                    for (int i = 0; i < idsClientes.Length; i++)
                    {
                        if (idsClientes[i] != "")
                        {
                            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("USUARIOCLIENTE");
                            string strsql = "INSERT INTO USUARIOCLIENTE (IDUsuarioCliente, IDUsuario,   IDCliente) VALUES (" + ID + " , " + IdUsuario + ",   " + idsClientes[i] + ") select 0";
                            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");                            
                        }                        
                    }
                    return 0;
                }

                
            }

            public int Consultar(string idusuario, string idCliente)
            {
                string strsql = "select IDUsuarioCliente from UsuarioCliente where IDCliente ="+idCliente+" and IDUsuario="+ idusuario;
                return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            }


            public class UsuarioClienteDivisao
            {
                public void DeletarByIdUsuario(string idUsuario)
                {
                    string strsql = "";//"DELETE FROM UsuarioClienteDivisao WHERE IDClienteDivisao IN (SELECT IDClienteDivisao FROM UsuarioClienteDivisao UCD INNER JOIN UsuarioCliente UC ON UC.IDUsuarioCliente = UCD.IDUsuarioCliente  WHERE UC.IDUsuario = " + idUsuario + " ) ";
                    //strsql += "  DELETE FROM USUARIOCLIENTEDIVISAO ";
                    //strsql += "   WHERE IDUsuarioClienteDivisao IN( ";
                    // strsql += "  SELECT UCD.IDUSUARIOCLIENTEDIVISAO  FROM  USUARIOCLIENTEDIVISAO UCD   ";
                    // strsql += "  INNER JOIN USUARIOCLIENTE UCDD ON UCDD.IDUSUARIOCLIENTE = UCD.IDUSUARIOCLIENTE   ";
                    // strsql += "  WHERE IDCLIENTEDIVISAO IN (  SELECT IDCLIENTEDIVISAO FROM USUARIOCLIENTEDIVISAO UCD ";   
                    // strsql += "  INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIOCLIENTE = UCD.IDUSUARIOCLIENTE     ";
                    // strsql += "  WHERE UC.IDUSUARIO = "+idUsuario+" )    ";
                    // strsql += "  AND UCDD.IDUSUARIO =" + idUsuario;
                    // strsql += "  ) ";

                    strsql = "  SELECT UCD.IDUSUARIOCLIENTEDIVISAO FROM USUARIO U  INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIO = U.IDUSUARIO  INNER JOIN USUARIOCLIENTEDIVISAO UCD ON UCD.IDUSUARIOCLIENTE = UC.IDUSUARIOCLIENTE  WHERE U.IDUSUARIO =" + idUsuario;
                    DataTable dtexclui = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

                    if (dtexclui.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtexclui.Rows.Count; i++)
                        {
                            strsql = " DELETE FROM USUARIOCLIENTEDIVISAO WHERE IDUSUARIOCLIENTEDIVISAO = " + dtexclui.Rows[i]["IDUSUARIOCLIENTEDIVISAO"].ToString();
                            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
                        }
                    }
                }


                public void Inserir(string IDUSUARIOCLIENTE, string IDCLIENTEDIVISAO)
                {
                    string ID = Sistran.Library.GetDataTables.RetornarIdTabela("USUARIOCLIENTEDIVISAO");
                    string strsql = "INSERT INTO USUARIOCLIENTEDIVISAO (IDUSUARIOCLIENTEDIVISAO, IDUSUARIOCLIENTE, IDCLIENTEDIVISAO) VALUES("+ID+", " + IDUSUARIOCLIENTE + ", " + IDCLIENTEDIVISAO + ")";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
                }
            }
        }
    }
}