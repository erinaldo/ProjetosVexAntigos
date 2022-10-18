using System.Data;

namespace Sistecno.BLL
{
    public class PaisUfCidade
    {
        #region Estados
        public string[] RetornarCodigoIBGE_UF_Cidade(int idCidade, string cnx)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarCodigoIBGE_UF_Cidade(idCidade, cnx);
        }
        public DataTable RetornarUf(string cnx)
        {
            try
            {
                return new Sistecno.DAL.PaisUfCidade().RetornarUf(cnx);

            }
            catch (System.Exception ex )
            {
                
                throw ex;
            }
        }

        public int RetornarIdEstadoPorCidade(string cnx, int idCidade)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarIdEstadoPorCidade(cnx, idCidade);
        }

        public string RetornarUFPorIdCidade(string cnx, int idCidade)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarUFPorIdCidade(cnx, idCidade);
        }
        //public void inicializar(string cnx)
        //{
        //    new Sistecno.DAL.PaisUfCidade().inicializar(cnx);

        //}

        public int RetornarIdEstadoPorUF(string cnx, string Uf)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarIdEstadoPorUF(cnx, Uf);
        }
        #endregion

        #region Cidades

        public Sistecno.DAL.Models.Cidade RetornarByCodigoIBGE(string cnx, string CodigoIBGE)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarByCodigoIBGE(cnx,CodigoIBGE);
        }

        public int RetornarCidadePorUfNome(string cnx, int idestado, string nome)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarCidadePorUfNome(cnx, idestado, nome).IDCidade;
        }

        public DataTable RetornarCidade(string cnx, int iIdEstado, string sCidade)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarCidade(cnx, iIdEstado, sCidade);
        }

        public DataTable RetornarCidadePesquisa(string cnx, string sCidade, int? iCodigoDaCidade, string sEstado)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarCidadePesquisa(cnx, sCidade, iCodigoDaCidade, sEstado);
        }

        #endregion

        #region Pais
        public DataTable RetornarPais(string cnx)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarPais(cnx);
        }

        #endregion
        
        #region Bairro
        public DataTable RetornarBairro(string cnx, int iIdCidade, string sBairro)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarBairro(cnx, iIdCidade, sBairro);
        }

        public int RetornarBairroPorCidadeNome(string cnx, int idCidade, string nome)
        {
            return new Sistecno.DAL.PaisUfCidade().RetornarBairroPorCidadeNome(cnx, idCidade, nome);
        }

        #endregion
    }
}
