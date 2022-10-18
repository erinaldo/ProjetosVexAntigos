using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SistecnoColetor;

namespace SistecnoColetor.Classes.DAL
{
    public class Usuario
    {
        public Classes.DTO.Usuario Logar(Classes.DTO.Usuario usuario, string cnx)
        {
            try
            {
                //string sql = "Select * from Usuario where login='" + usuario.Login + "' and senha='" + usuario.Senha + "'";
                string sql = "SELECT *, F.NOME FILIAL, E.NOME EMPRESA FROM USUARIO U LEFT JOIN EMPRESA E ON E.IDEMPRESA = U.ULTIMAEMPRESA LEFT JOIN FILIAL F ON F.IDFILIAL = U.ULTIMAFILIAL WHERE LOGIN='"+usuario.Login+"' AND SENHA='"+usuario.Senha+"'";
                DataTable dt = Classes.BdExterno.RetornarDT(sql, cnx);

                Classes.DTO.Usuario ret = new SistecnoColetor.Classes.DTO.Usuario();


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ATIVO"].ToString() == "NAO")
                        throw new Exception("Usuário Inativo");

                    ret.IDUsuario = int.Parse(dt.Rows[0]["IDUsuario"].ToString());
                    ret.IDCadastro = int.Parse(dt.Rows[0]["IDCadastro"].ToString());
                    ret.IDGrupo = (dt.Rows[0]["IDGrupo"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDGrupo"]);
                    ret.IDPerfil = (dt.Rows[0]["IDPerfil"] == DBNull.Value ? null : (int?)dt.Rows[0]["IDPerfil"]);
                    ret.UltimaEmpresa = (dt.Rows[0]["UltimaEmpresa"] == DBNull.Value ? 1 : (int?)dt.Rows[0]["UltimaEmpresa"]);
                    ret.UltimaFilial = (dt.Rows[0]["UltimaFilial"] == DBNull.Value ? 1 : (int?)dt.Rows[0]["UltimaFilial"]);
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
    }
}
