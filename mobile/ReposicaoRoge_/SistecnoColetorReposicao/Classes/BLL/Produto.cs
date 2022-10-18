using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SistecnoColetor.Classes.BLL
{

    public class Ocorrencia
    {
        public void GravarDevolucao(string IdDocumento, DataTable dtFalta)
        {
            new DAL.Ocorrencia().GravarDevolucao(IdDocumento, dtFalta);
        }
    }

    public class Documento
    {
        public DataTable retornarConferenciaByChave(string chave)
        {
            return new DAL.Documento().retornarConferenciaByChave(chave);
        }
    }

    public class Produto
    {

        public void Alterar(SistecnoColetor.Classes.DTO.Produto pro)
        {
            new DAL.Produto().Alterar(pro);
        }
        public DataTable RetornarProdutoUA(string IdUa, string CodigoBarras)
        {
            return new DAL.Produto().RetornarProdutoUA(IdUa, CodigoBarras);
        }


        public DataTable RetornarProduto(string CodigoBarras)
        {
            return new DAL.Produto().RetornarProduto(CodigoBarras);
        }

        public DataTable RetornarDadosLogistico(string CodigoDeBarras)
        {
            return new DAL.Produto().RetornarDadosLogistico(CodigoDeBarras);
        }

        public DataTable RetornarProdutoByCBandCodigo(string Valor)
        {
            return new DAL.Produto().RetornarProdutoByCBandCodigo(Valor);
        }

        public void AlterarEnderecoDePicking(string idProdutoCliente, string IdDepositoPlantaLocalizacao)
        {
            new DAL.Produto().AlterarEnderecoDePicking(idProdutoCliente, IdDepositoPlantaLocalizacao);
        }
        public DataTable RetornarProdutosCodigoDeBarras(string CodigoDeBarras)
        {
            return new DAL.Produto().RetornarProdutosCodigoDeBarras(CodigoDeBarras);
        }
        public DataTable RetornarProdutosDoEndereco(string IdDepositoPlantaLocalizacao)
        {
            DataTable DT = new DAL.Produto().RetornarProdutosDoEndereco(IdDepositoPlantaLocalizacao);

            if (DT.Rows.Count == 0)
                return this.RetornarProdutosCodigoDeBarras(IdDepositoPlantaLocalizacao);
            else
                return DT;
        }


        public DataTable RetornarProdutosDoEnderecoPicking(string IdDepositoPlantaLocalizacaoPicking)
        {
            return new DAL.Produto().RetornarProdutosDoEnderecoPicking(IdDepositoPlantaLocalizacaoPicking);

        }

    }

    public class DepositoPlantaLocalizacao
    {
        public DataTable RetornarEnderecosPicking(string IdprodutoCliente)
        {
            return new DAL.DepositoPlantaLocalizacao().RetornarEnderecosPicking(IdprodutoCliente);
        }

        public void GravarPicking(string sql, bool insert)
        {
             new DAL.DepositoPlantaLocalizacao().GravarPicking(sql, insert);
        }

        public DataTable RetornarDepositoPlantaLocalizacao(string IdDepositoPlantaLocalizacao)
        {
            return new DAL.DepositoPlantaLocalizacao().RetornarDepositoPlantaLocalizacao(IdDepositoPlantaLocalizacao);
        }

        public DataTable RetornarPorDPL_Prod(string valor)
        {
            return new DAL.DepositoPlantaLocalizacao().RetornarPorDPL_Prod(valor);
        }

        /// <summary>
        /// Retorna idDepositoPlantaLocalização e o Endereco
        /// </summary>
        /// <param name="IdProdutoCliente"></param>
        /// <param name="Metodo"></param>
        /// <returns></returns>
        public string[] RetornarEndereco(string IdProdutoCliente, string Metodo, string enderecoPicking)
        {
            DataTable d = new DAL.DepositoPlantaLocalizacao().RetornarEndereco(IdProdutoCliente);

            if (d.Rows.Count == 0)
                throw new Exception("Produto sem estoque.");
            
            DataTable dttem = new DataTable();

            dttem.Columns.Add("IDDepositoPlantaLocalizacao");
            dttem.Columns.Add("Descricao");
            dttem.Columns.Add("IdUnidadeDeArmazenagem");


            string[] ret = new string[3];
            string PrimeiraDataValidade = "";

            DataView dv = d.AsDataView();
            if (Metodo == "FEFO")
            {
                dv.Sort = "VALIDADE Asc";
                PrimeiraDataValidade = dv[0]["VALIDADE"].ToString();
            }
            else
            {
                dv.Sort = "DATADEENTRADA Asc";
                PrimeiraDataValidade = dv[0]["DATADEENTRADA"].ToString();

            }
            
            DateTime pd = new DateTime();
            
            if (PrimeiraDataValidade != "")
            {
                pd = DateTime.Parse(PrimeiraDataValidade);

                string data = (pd.Year.ToString().Length == 2 ? "20" + pd.Year.ToString() : pd.Year.ToString()) + "-" + (pd.Month.ToString().Length == 1 ? "0" + pd.Month.ToString() : pd.Month.ToString()) + "-" + (pd.Day.ToString().Length == 1 ? "0" + pd.Day.ToString() : pd.Day.ToString());


                DataRow[] o;
                if (Metodo == "FEFO")
                    o = d.Select("VALIDADE ='" + data + "'", "");
                else
                    o = d.Select("DATADEENTRADA ='" + data + "'", "");


                //inclui o proprio endereco de picking

                DataRow x = dttem.NewRow();
                x[0] = "0";
                x[1] = enderecoPicking;
                x[2] = "0";
                dttem.Rows.Add(x);

                foreach (var item in o)
                {
                    x = dttem.NewRow();
                    x[0] = item["IDDepositoPlantaLocalizacao"];
                    x[1] = item["Descricao"];
                    x[2] = item["IdUnidadeDeArmazenagem"];
                    dttem.Rows.Add(x);
                }

              
            }
            else
            {
                ret[0] = dv[0]["IDDepositoPlantaLocalizacao"].ToString();
                ret[1] = dv[0]["Descricao"].ToString();
                ret[2] = dv[0]["IdUnidadeDeArmazenagem"].ToString();
                return ret;
            }
                     


            DataTable A = dttem.Select("Descricao like '%%'", "Descricao ASC").CopyToDataTable();
            DataTable Z = dttem.Select("Descricao like '%%'", "Descricao DESC").CopyToDataTable();
            

            int contA = 0;
            for (int i = 0; i < A.Rows.Count; i++)
            {

                if (A.Rows[i]["IDDepositoPlantaLocalizacao"].ToString() == "0") // é o endereco de picking
                    break;
                else
                    contA++;

            }

            int contZ = 0;
            for (int i = 0; i < Z.Rows.Count; i++)
            {
                if (Z.Rows[i]["IDDepositoPlantaLocalizacao"].ToString() == "0") // é o endereco de picking
                    break;
                else
                    contZ++;
            }


            if (contA < contZ)
            {
                ret[0] = A.Rows[contA+1]["IDDepositoPlantaLocalizacao"].ToString();
                ret[1] = A.Rows[contA+1]["Descricao"].ToString();
                ret[2] = A.Rows[contA + 1]["IdUnidadeDeArmazenagem"].ToString();
            }
            else
            {
                ret[0] = Z.Rows[contZ]["IDDepositoPlantaLocalizacao"].ToString();
                ret[1] = Z.Rows[contZ]["Descricao"].ToString();
                ret[2] = A.Rows[contZ]["IdUnidadeDeArmazenagem"].ToString();
            }
            return ret;

        }
    }

    public class UnidadeDeArmazenagem
    {

        public DataTable RetornarSaida(string idUa, string IdDeposiotoPlantaloc)
        {
            return new DAL.UnidadeDeArmazenagem().RetornarSaida(idUa, IdDeposiotoPlantaloc);
        }

        public DataTable RetornarSaidaLivre(string idUa, string IdDeposiotoPlantaloc)
        {
            return new DAL.UnidadeDeArmazenagem().RetornarSaidaLivre(idUa, IdDeposiotoPlantaloc);
        }

        public DataTable RetornarSaida(string idUa, string codigoDeBarras, string IdDeposiotoPlantaloc)
        {
            return new DAL.UnidadeDeArmazenagem().RetornarSaida(idUa, codigoDeBarras, IdDeposiotoPlantaloc);
        }
        public DataTable RetornarGuardarPallet(string idUa, string status)
        {
            return new DAL.UnidadeDeArmazenagem().RetornarGuardarPallet(idUa, status);
        }

        public DataTable LerUnidadeDeArmazenagem(string idUnidadedeArmazenagem)
        {
            return new DAL.UnidadeDeArmazenagem().LerUnidadeDeArmazenagem(idUnidadedeArmazenagem);
        }

        public DataTable Retornar(string idUnidadedeArmazenagem , string status)
        {
            return new DAL.UnidadeDeArmazenagem().Retornar(idUnidadedeArmazenagem, status);
        }

        public void AlterarEnderecoUa(string idUa, string IdDepositoPlantaLocalizacao)
        {
            new DAL.UnidadeDeArmazenagem().AlterarEnderecoUa(idUa, IdDepositoPlantaLocalizacao);
        }

        public void AlterarEnderecoUa(int idproducliente, string IdDepositoPlantaLocalizacaoDestino, string IdUa)
        {
            new DAL.UnidadeDeArmazenagem().AlterarEnderecoUa(idproducliente, IdDepositoPlantaLocalizacaoDestino, IdUa);

        }
       
    }

    public class Movimentacao
    {
        //public string EnderecoVazio( )
        //{
        //    return new DAL.Movimentacao().RetonarEndereco(
        //}


        public void GravarMovimentacao(
                                        string IdUa,
                                        string IdDepositoPlantaLocalizacaoOrigem,
                                        string IdProdutoEmbalagem,
                                        string Quantidade,
                                        string Fator,
                                        string IdOperacaoColetor,
                                        string EntradaSaida, string IdProdutoCliente)
        {
             new DAL.Movimentacao().GravarMovimentacao(IdUa, IdDepositoPlantaLocalizacaoOrigem, IdProdutoEmbalagem, Quantidade, Fator, IdOperacaoColetor, "ENTRADA", IdProdutoCliente);
        }

        public void GravarMovimentacao(
                                       string IdUa,
                                       string IdDepositoPlantaLocalizacaoOrigem,
                                       string IdProdutoEmbalagem,
                                       string Quantidade,
                                       string Fator,
                                       string IdOperacaoColetor,
                                       string EntradaSaida, string IdProdutoCliente, string GravarUa)
        {
            new DAL.Movimentacao().GravarMovimentacao(IdUa, IdDepositoPlantaLocalizacaoOrigem, IdProdutoEmbalagem, Quantidade, Fator, IdOperacaoColetor, "ENTRADA", IdProdutoCliente,  GravarUa);
        }
    }
}