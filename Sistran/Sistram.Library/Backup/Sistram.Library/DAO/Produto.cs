using System.Text;
using System.Data;
using System.Web;
using System;

namespace SistranDAO
{
    public sealed class Produto
    {
        public DataTable ConsultarCodigoBarras(string CodigoBarras)
        {
            string strsql = "SELECT IDProduto,CodigoDeBarras,Altura,Largura,Comprimento,PesoLiquido,PesoBruto,Especie,DataDeCadastro FROM PRODUTO WHERE CodigoDeBarras='"+CodigoBarras+"'";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public DataTable ConsultarProdutoClienteCodigo(string Codigo)
        {
            string strsql = "SELECT P.*, PE.*, PC.Codigo, PC.Descricao, PC.CodigoDoCliente, MetodoDeMovimentacao, IDUnidadeDeMedida FROM PRODUTOCLIENTE PC INNER JOIN PRODUTOEMBALAGEM  PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE INNER JOIN Produto P ON P.IDProduto = PE.IDProduto WHERE CODIGO = '" + Codigo + "'";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public DataTable ConsultarProdutoComFoto(string idClienteDivisao)
        {
            string strsql = "SELECT  ";
            strsql += " (SELECT TOP 1 FOTO FROM PRODUTOFOTO PF WHERE PF.IDPRODUTO=PR.IDPRODUTO ) FOTO, ";
            strsql += " ISNULL(PROCLI.CODIGODOCLIENTE, PROCLI.CODIGO) CODIGODOCLIENTE,";
            strsql += " PROCLI.CODIGO,";
            strsql += " PROCLI.DESCRICAO,";
            strsql += " SOLICITARDATADEVALIDADE 'PERECIVEL',";
            strsql += " SOLICITARDATADEVALIDADE 'VALIDADE',";
            strsql += " VALORUNITARIO,";
            strsql += " CAST(COALESCE(ESTDIV.SALDO, 0) - (SELECT COALESCE(SUM(DOCIT.QUANTIDADEUNIDADEESTOQUE),0) AS QUANTIDADEUNIDADEESTOQUE FROM DOCUMENTOITEM DOCIT    ";
            strsql += " 		INNER JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO = DOCIT.IDDOCUMENTO)    ";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PRODE 	ON (PRODE.IDPRODUTOEMBALAGEM = DOCIT.IDPRODUTOEMBALAGEM)    ";
            strsql += " WHERE DOC.ENTRADASAIDA = 'SAIDA'    ";
            strsql += " AND DOC.TIPODEDOCUMENTO = 'PEDIDO'   ";
            strsql += " AND DOC.ATIVO = 'SIM'    ";
            strsql += " AND PRODE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE    ";
            strsql += " AND COALESCE(DOCIT.ESTOQUEPROCESSADO,'NAO') = 'NAO'     ";
            strsql += " AND DOCIT.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO   ) AS NUMERIC (10,0)) AS SALDODIVISAODISPONIVEL,";
            strsql += " CONTEUDO,								";
            strsql += " UNIDADEDEMEDIDA,";
            strsql += " PR.ALTURA,";
            strsql += " PR.LARGURA,";
            strsql += " PR.COMPRIMENTO,";
            strsql += " PR.PESOBRUTO,";
            strsql += " PR.PESOLIQUIDO,";
            strsql += " '0.00' RODOVIARIO,";
            strsql += " '0.00' AERIO,";
            strsql += " DATALIMITEDEUSO";

            strsql += " FROM ESTOQUEDIVISAO ESTDIV    ";
            strsql += " LEFT JOIN ESTOQUE EST   ON(EST.IDESTOQUE = ESTDIV.IDESTOQUE)   ";
            strsql += " LEFT JOIN PRODUTOCLIENTE PROCLI   ON(PROCLI.IDPRODUTOCLIENTE = EST.IDPRODUTOCLIENTE)   ";
            strsql += " LEFT JOIN PRODUTOEMBALAGEM PROEMB   ON (PROEMB.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE)   ";
            strsql += " LEFT JOIN UNIDADEDEMEDIDA UNIDEMED   ON (UNIDEMED.IDUNIDADEDEMEDIDA = PROCLI.IDUNIDADEDEMEDIDA)";
            strsql += " INNER JOIN PRODUTO PR ON PR.IDPRODUTO =   PROEMB.IDPRODUTO ";
            strsql += " WHERE ESTDIV.IDCLIENTEDIVISAO=" + idClienteDivisao;
            strsql += " AND PROCLI.IDCLIENTE = 1 ";
            strsql += " AND COALESCE(ESTDIV.SALDO, 0) - (SELECT COALESCE(SUM(DOCIT.QUANTIDADEUNIDADEESTOQUE),0) AS QUANTIDADEUNIDADEESTOQUE ";
            strsql += " FROM DOCUMENTOITEM DOCIT";
            strsql += " INNER JOIN DOCUMENTO DOC";
            strsql += " ON (DOC.IDDOCUMENTO = DOCIT.IDDOCUMENTO)";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PRODE";
            strsql += " ON (PRODE.IDPRODUTOEMBALAGEM = DOCIT.IDPRODUTOEMBALAGEM)";
            strsql += " WHERE DOC.ENTRADASAIDA = 'SAIDA'";
            strsql += " AND DOC.TIPODEDOCUMENTO = 'PEDIDO'";
            strsql += " AND DOC.ATIVO = 'SIM'";
            strsql += " AND PRODE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE";
            strsql += " AND COALESCE(DOCIT.ESTOQUEPROCESSADO,'NAO') = 'NAO' ";
            strsql += " AND DOCIT.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO	) >=0";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }
        
        public void VoltarRegistrosCadProdutos(string delete)
        {
            Sistran.Library.GetDataTables.ExecutarSemRetorno(delete, "");
        }

        public int Inserir(string CDBarras, string altura, string largura, string comprimento, string pesoLiquido, string pesoBruto, string especie)
        {
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("select * from PRODUTO where CODIGODEBARRAS='"+ CDBarras +"'");
            if (dt.Rows.Count == 0)
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTO");
                string strsql = " INSERT INTO PRODUTO (";
                strsql += " IDPRODUTO,";
                strsql += " CODIGODEBARRAS,";
                strsql += " ALTURA,";
                strsql += " LARGURA,";
                strsql += " COMPRIMENTO,";
                strsql += " PESOLIQUIDO,";
                strsql += " PESOBRUTO,";
                strsql += " ESPECIE,";
                strsql += " DATADECADASTRO";
                strsql += " )";
                strsql += " VALUES";
                strsql += " (";
                strsql += ID + " ,";
                strsql += " '" + CDBarras.ToUpper() + "',";
                strsql += altura.Replace(",", ".") + " ,";
                strsql += largura.Replace(",", ".") + ",";
                strsql += comprimento.Replace(",", ".") + " ,";
                strsql += pesoLiquido.Replace(",", ".") + " ,";
                strsql += pesoBruto.Replace(",", ".") + " ,";
                strsql += "'" + especie.ToUpper() + "',";
                strsql += " GETDATE()";
                strsql += " ) ";
                Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
                return Convert.ToInt32(ID);
            }
            else
                return int.Parse(dt.Rows[0]["IDPRODUTO"].ToString());
        }

        public int InserirProdutoCliente(string idcliente, string UnidadeMedida, string Codigo, string Descricao, string MetodoArmz, string Ativo)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE");
            string strsql = " INSERT INTO PRODUTOCLIENTE (";
            strsql += " IDPRODUTOCLIENTE, ";
            strsql += " IDCLIENTE,";
            strsql += " IDUNIDADEDEMEDIDA,";
            strsql += " CODIGO, CodigoDoCliente,";
            strsql += " DESCRICAO,";
            strsql += " METODODEMOVIMENTACAO,";
            strsql += " ATIVO";
            strsql += " ) VALUES";
            strsql += " (";
            strsql += ID + " , ";
            strsql += idcliente + " ,";
            strsql += "'"+UnidadeMedida+"',";
            strsql += "'"+Codigo+"', '"+Codigo+"',";
            strsql += "'"+Descricao+"',";
            strsql += "'"+MetodoArmz+"' ,";
            strsql += "'"+Ativo+"')";
            //strsql += " SELECT ISNULL(MAX(IDPRODUTOCLIENTE),0) FROM PRODUTOCLIENTE ";
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            return Convert.ToInt32(ID);
        }

        public void UpdateProdutoCliente(string idProdutoCliente, string UnidadeMedida, string Codigo, string Descricao, string MetodoArmz, string Ativo, string IdProdutoEmbalagem, string ValorUnitario, string UnidCliente, string UnidLogs)
        {
            string strsql = " update PRODUTOCLIENTE set IDUNIDADEDEMEDIDA = " + UnidadeMedida + ", CodigoDoCliente ='" + Codigo + "' ,CODIGO ='" + Codigo + "' , DESCRICAO='" + Descricao.ToUpper() + "' , METODODEMOVIMENTACAO='" + MetodoArmz + "' , ATIVO='" + Ativo + "'  where IDPRODUTOCLIENTE=" + idProdutoCliente;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

            strsql = " update PRODUTOEMBALAGEM set ValorUnitario=" + ValorUnitario.Replace(",", ".") + ", UnidadeDoCliente=" + UnidCliente.Replace(",", ".") + ", UnidadeLogistica=" + UnidLogs.Replace(",", ".") + "  WHERE idProdutoCliente=" + idProdutoCliente;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public int InserirProdutoImagem(string idProduto, byte[] imagem)
        {
            //string strsql = " INSERT INTO PRODUTOFOTO (";
            //strsql += " IDProdutoFoto, ";
            //strsql += " IDProduto,";
            //strsql += " Foto";
            //strsql += " ) VALUES";
            //strsql += " (";
            //strsql += " (SELECT ISNULL(MAX(IDProdutoFoto),0) +1 FROM PRODUTOFOTO), ";
            //strsql += idProduto + " ,";
            //strsql += "'" + imagem + "')";
            //strsql += " SELECT ISNULL(MAX(IDProdutoFoto),0) FROM PRODUTOFOTO ";
            //return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");

            string strsql = " SELECT ISNULL(IDPRODUTO,0) FROM PRODUTOFOTO WHERE IDPRODUTO=" + idProduto;
            DataTable DF = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

            if(DF.Rows.Count ==0)
               Sistran.Library.GetDataTables.InserirImagens(idProduto, imagem);
            else
                Sistran.Library.GetDataTables.AlterarImagens(idProduto, imagem);              
               

            return 1;
        }

        public int InserirProdutoEmbalagem(string IdProdCli, string IdProd, string UnidCliente, string UnidLogs, string UnidMed, string ValorUnit, string Conteudo)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOEMBALAGEM");
            string strsql = " INSERT INTO PRODUTOEMBALAGEM( ";
            strsql += " IDPRODUTOEMBALAGEM , ";
            strsql += " IDPRODUTOCLIENTE, ";
            strsql += " IDPRODUTO, ";
            strsql += " UNIDADEDOCLIENTE, ";
            strsql += " UNIDADELOGISTICA, ";
            strsql += " UNIDADEDEMEDIDA, ";
            strsql += " VALORUNITARIO, ";
            strsql += " ATIVO, CONTEUDO";
            strsql += " )";
            strsql += " VALUES";
            strsql += " (";
            strsql += ID+" , ";
            strsql +=  IdProdCli + ", ";
            strsql +=  IdProd + ", ";
            strsql +=  UnidCliente.Replace(",",".") + " , ";
            strsql += UnidLogs.Replace(",", ".") + " , ";
            strsql += "'" + UnidMed + "' , ";
            strsql += ValorUnit.Replace(",", ".") + " , ";
            strsql += " 'SIM', '"+Conteudo+"'";
            strsql += " )";
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            return Convert.ToInt32(ID);

        }
        
        public DataTable ListarProdutoAleatorio(int idCliente)
        {
            string strsql = "SELECT TOP 1 PC.IDGRUPODEPRODUTO FROM PRODUTOCLIENTE PC LEFT JOIN GRUPODEPRODUTO GP ON (PC.IDGRUPODEPRODUTO = GP.IDGRUPODEPRODUTO)  WHERE PC.IDCLIENTE=" + idCliente.ToString() + "  AND GP.IDGRUPODEPRODUTO >= (RAND())*(SELECT MAX(IDGrupoDeProduto) FROM GRUPODEPRODUTO WHERE IDCliente=" + idCliente.ToString() + ") ";
            int idGrupoDeProduto = Sistran.Library.GetDataTables.ExecutarRetornoID(strsql,"");
            return ListarProdutosByGrupo(idCliente, idGrupoDeProduto.ToString(), true);
        }

        public DataTable RetornarImagemProduto(int idProduto)
        {
            string strsql = "";
            strsql += "  SELECT IDPRODUTOFOTO, PF.IDPRODUTO,  FOTO FROM PRODUTOFOTO PF ";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = PF.IDPRODUTO ";
            strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE  ";
            strsql += " WHERE PF.IDPRODUTO = " + idProduto;
            strsql += " AND PC.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }
        
        public DataTable RetornarUltimaImagemProduto(int idProduto)
        {
            string strsql = "";
            strsql += "  SELECT top 1 IDPRODUTOFOTO, PF.IDPRODUTO,  FOTO FROM PRODUTOFOTO PF ";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = PF.IDPRODUTO ";
            strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE  ";
            strsql += " WHERE PF.IDPRODUTO = " + idProduto;
            strsql += " AND PC.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")  order by 1 desc";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable ListarProdutosByGrupo(int idCliente, string idGrupoDeProduto, bool Aleatorio)
        {            
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT DISTINCT ProEmb.IDProdutoEmbalagem, ProEmb.IDProduto,   ");
            txt.Append(" CASE WHEN ProCli.CodigoDoCliente is NULL THEN ProCli.Codigo ELSE 	ProCli.CodigoDoCliente   END as Codigo,  ");
            txt.Append(" ProCli.Descricao,   ");
            txt.Append(" ProEmb.Conteudo,  ESTDIV.IDCLIENTEDIVISAO, ClienteDivisao.Nome, PROCLI.IDProdutoCliente,");
            txt.Append(" Cast(COALESCE(EstDiv.Saldo, 0) - (Select Coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque From DocumentoItem DocIt   ");
            txt.Append(" inner Join Documento Doc On (Doc.IdDocumento = DocIt.IdDocumento)   ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE 	on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)   ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'   ");
            txt.Append(" and Doc.TipoDeDocumento = 'PEDIDO'  ");
            txt.Append(" and Doc.Ativo = 'SIM'   ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente   ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'    ");
            txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO  ");
            txt.Append(" ) as numeric (10,0)) AS SaldoDivisaoDisponivel,   ");
            txt.Append(" Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) As ValorUnitario,  ");
            txt.Append(" UniDeMed.Decimais,  ");
            txt.Append(" 'R$ ' + Cast(Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) as varchar(12)) As RealValorUnitario,  ");
            txt.Append(" null As nulo  ");
            txt.Append(" FROM EstoqueDivisao EstDiv   ");
            txt.Append(" LEFT JOIN Estoque Est  ");
            txt.Append(" ON(Est.IDEstoque = EstDiv.IDEstoque)  ");
            txt.Append(" LEFT JOIN ProdutoCliente ProCli  ");
            txt.Append(" ON(ProCli.IDProdutoCliente = Est.IDProdutoCliente)  ");
            txt.Append(" LEFT JOIN ProdutoEmbalagem ProEmb  ");
            txt.Append(" ON (ProEmb.IDProdutoCliente = ProCli.IDProdutoCliente)  ");
            txt.Append(" LEFT JOIN UnidadeDeMedida UniDeMed  ");
            txt.Append(" ON (UniDeMed.IDUnidadeDeMedida = ProCli.IDUnidadeDeMedida)  ");
            txt.Append(" inner join GrupoDeProduto pg on   pg.IDGrupoDeProduto = ProCli.IDGrupoDeProduto ");

            if (idGrupoDeProduto.ToString().Length>0)
            {
                if (Aleatorio == true)
                    txt.Append(" and pg.IDGrupoDeProduto = '" + idGrupoDeProduto + "'  ");
                else
                    txt.Append(" and pg.Codigo = '" + idGrupoDeProduto + "'  ");
            }
            txt.Append(" inner join ClienteDivisao on ClienteDivisao.IDClienteDivisao = ESTDIV.IDClienteDivisao ");
            txt.Append(" WHERE  ProCli.idcliente=" + idCliente.ToString());
            txt.Append(" AND COALESCE(EstDiv.Saldo, 0) - (Select coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque   ");
            txt.Append(" From DocumentoItem DocIt   ");
            txt.Append(" inner Join Documento Doc   ");
            txt.Append(" On (Doc.IdDocumento = DocIt.IdDocumento)   ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE   ");
            txt.Append(" on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem) 				  ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'   ");
            txt.Append(" and Doc.TipoDeDocumento = 'PEDIDO'  ");
            txt.Append(" and Doc.Ativo = 'SIM'   ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente   ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'    ");
            txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO	) > 0  ");
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public DataTable ListarProdutoByDivisaoCliente(int Iddivisao, int idCliente, bool MaiorqueZero)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT DISTINCT (SELECT Nome FROM ClienteDivisao CD WHERE ESTDIV.IDCLIENTEDIVISAO = CD.IDCLIENTEDIVISAO) DIVISAO, '' 'Nome', ESTDIV.IDCLIENTEDIVISAO, ProCli.IDProdutoCliente, ProEmb.IDProdutoEmbalagem, ProEmb.IDProduto, CASE WHEN ProCli.CodigoDoCliente is NULL THEN ProCli.Codigo ELSE 	ProCli.CodigoDoCliente   END as Codigo, ProCli.Descricao, ProEmb.Conteudo,  ");
            txt.Append(" Cast(COALESCE(EstDiv.Saldo, 0) - (Select Coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque From DocumentoItem DocIt   ");
            txt.Append(" inner Join Documento Doc On (Doc.IdDocumento = DocIt.IdDocumento)   ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE 	on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)   ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'   ");
            txt.Append(" and Doc.TipoDeDocumento = 'PEDIDO'  ");
            txt.Append(" and Doc.Ativo = 'SIM'   ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente   ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'    ");
            txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO  ");
            txt.Append(" ) as numeric (10,0)) AS SaldoDivisaoDisponivel,   ");
            txt.Append(" Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) As ValorUnitario,  ");
            txt.Append(" UniDeMed.Decimais,  ");
            txt.Append(" 'R$ ' + Cast(Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) as varchar(12)) As RealValorUnitario,  ");
            txt.Append(" null As nulo  ");
            txt.Append(" FROM EstoqueDivisao EstDiv   ");
            txt.Append(" LEFT JOIN Estoque Est  ");
            txt.Append(" ON(Est.IDEstoque = EstDiv.IDEstoque)  ");
            txt.Append(" LEFT JOIN ProdutoCliente ProCli  ");
            txt.Append(" ON(ProCli.IDProdutoCliente = Est.IDProdutoCliente)  ");
            txt.Append(" LEFT JOIN ProdutoEmbalagem ProEmb  ");
            txt.Append(" ON (ProEmb.IDProdutoCliente = ProCli.IDProdutoCliente)  ");
            txt.Append(" LEFT JOIN UnidadeDeMedida UniDeMed  ");
            txt.Append(" ON (UniDeMed.IDUnidadeDeMedida = ProCli.IDUnidadeDeMedida)  ");
            txt.Append(" WHERE EstDiv.IDClienteDivisao = " + Iddivisao.ToString() + "  ");
            txt.Append(" AND ProCli.idcliente = " + idCliente.ToString());
            txt.Append(" AND COALESCE(EstDiv.Saldo, 0) - (Select coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque   ");
            txt.Append(" From DocumentoItem DocIt   ");
            txt.Append(" inner Join Documento Doc   ");
            txt.Append(" On (Doc.IdDocumento = DocIt.IdDocumento)   ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE   ");
            txt.Append(" on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)   ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'   ");
            txt.Append(" and Doc.TipoDeDocumento = 'PEDIDO'  ");
            txt.Append(" and Doc.Ativo = 'SIM'   ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente   ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'    ");


            if(MaiorqueZero)
                txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO	) > 0  ");


            txt.Append(" ORDER BY ProCli.Descricao");
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public DataTable ListarProdutoByCodigos(string Codigo, string CodigoDoCliente, string IdCliente, bool EstoqueZerado)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT DISTINCT (SELECT Nome FROM ClienteDivisao CD WHERE ESTDIV.IDCLIENTEDIVISAO = CD.IDCLIENTEDIVISAO) DIVISAO, '' 'Nome', ESTDIV.IDCLIENTEDIVISAO, ProCli.IDProdutoCliente, ProEmb.IDProdutoEmbalagem, ProEmb.IDProduto, CASE WHEN ProCli.CodigoDoCliente is NULL THEN ProCli.Codigo ELSE 	ProCli.CodigoDoCliente   END as Codigo, ProCli.Descricao, ProEmb.Conteudo,  ");
            txt.Append(" Cast(COALESCE(EstDiv.Saldo, 0) - (Select Coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque From DocumentoItem DocIt   ");
            txt.Append(" inner Join Documento Doc On (Doc.IdDocumento = DocIt.IdDocumento)   ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE 	on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)   ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'   ");
            txt.Append(" and Doc.TipoDeDocumento = 'PEDIDO'  ");
            txt.Append(" and Doc.Ativo = 'SIM'   ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente   ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'    ");
            txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO  ");
            txt.Append(" ) as numeric (10,0)) AS SaldoDivisaoDisponivel,   ");
            txt.Append(" Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) As ValorUnitario,  ");
            txt.Append(" UniDeMed.Decimais,  ");
            txt.Append(" 'R$ ' + Cast(Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) as varchar(12)) As RealValorUnitario,  ");
            txt.Append(" null As nulo  ");
            txt.Append(" FROM EstoqueDivisao EstDiv   ");
            txt.Append(" LEFT JOIN Estoque Est  ");
            txt.Append(" ON(Est.IDEstoque = EstDiv.IDEstoque)  ");
            txt.Append(" LEFT JOIN ProdutoCliente ProCli  ");
            txt.Append(" ON(ProCli.IDProdutoCliente = Est.IDProdutoCliente)  ");
            txt.Append(" LEFT JOIN ProdutoEmbalagem ProEmb  ");
            txt.Append(" ON (ProEmb.IDProdutoCliente = ProCli.IDProdutoCliente)  ");
            txt.Append(" LEFT JOIN UnidadeDeMedida UniDeMed  ");
            txt.Append(" ON (UniDeMed.IDUnidadeDeMedida = ProCli.IDUnidadeDeMedida)  ");
            txt.Append(" WHERE EstDiv.IDClienteDivisao in(" + HttpContext.Current.Session["Divisoes"].ToString() + ")");
            txt.Append(" and ProCli.idcliente = " + IdCliente);
            txt.Append(" AND COALESCE(EstDiv.Saldo, 0) - (Select coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque   ");
            txt.Append(" From DocumentoItem DocIt   ");
            txt.Append(" inner Join Documento Doc   ");
            txt.Append(" On (Doc.IdDocumento = DocIt.IdDocumento)   ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE   ");
            txt.Append(" on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)   ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA' ");
            txt.Append(" and Doc.TipoDeDocumento = 'PEDIDO' ");
            txt.Append(" and Doc.Ativo = 'SIM'   ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente  ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'   ");
            txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO) >"+ (EstoqueZerado==true?"=":"")  +"0  ");           
            txt.Append(" and EstDiv.IDClienteDivisao in(" + HttpContext.Current.Session["Divisoes"] + ")  ");

            if (Codigo != "")
                txt.Append(" AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN PROCLI.CODIGO ELSE 	PROCLI.CODIGODOCLIENTE   END  LIKE '%"+Codigo+"%'");

            if (CodigoDoCliente != "")
                txt.Append(" AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN PROCLI.CODIGO ELSE 	PROCLI.CODIGODOCLIENTE   END  LIKE '%" + CodigoDoCliente + "%'");
                
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

        }

        public decimal RetornarSaldoDisponivelPorIdProdutoClienteDivisao(int idclienteDivisao, int idProdutoCliente)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT DISTINCT  ");
            //txt.Append(" ESTDIV.IDCLIENTEDIVISAO,  ");
            //txt.Append(" PROCLI.IDPRODUTOCLIENTE, 	 ");
            txt.Append(" CAST(COALESCE(ESTDIV.SALDO, 0) - (SELECT COALESCE(SUM(DOCIT.QUANTIDADEUNIDADEESTOQUE),0) AS QUANTIDADEUNIDADEESTOQUE FROM DOCUMENTOITEM DOCIT     ");
            txt.Append(" INNER JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO = DOCIT.IDDOCUMENTO)     ");
            txt.Append(" INNER JOIN PRODUTOEMBALAGEM PRODE 	ON (PRODE.IDPRODUTOEMBALAGEM = DOCIT.IDPRODUTOEMBALAGEM)     ");
            txt.Append(" WHERE DOC.ENTRADASAIDA = 'SAIDA'     ");
            txt.Append(" AND DOC.TIPODEDOCUMENTO = 'PEDIDO'    ");
            txt.Append(" AND DOC.ATIVO = 'SIM'     ");
            txt.Append(" AND PRODE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE     ");
            txt.Append(" AND COALESCE(DOCIT.ESTOQUEPROCESSADO,'NAO') = 'NAO'      ");
            txt.Append(" AND DOCIT.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO   ) AS NUMERIC (10,0)) AS SALDODIVISAODISPONIVEL				 ");
            txt.Append(" FROM ESTOQUEDIVISAO ESTDIV     ");
            txt.Append(" LEFT JOIN ESTOQUE EST   ON(EST.IDESTOQUE = ESTDIV.IDESTOQUE)    ");
            txt.Append(" LEFT JOIN PRODUTOCLIENTE PROCLI   ON(PROCLI.IDPRODUTOCLIENTE = EST.IDPRODUTOCLIENTE)    ");
            txt.Append(" LEFT JOIN PRODUTOEMBALAGEM PROEMB   ON (PROEMB.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE)    ");
            txt.Append(" LEFT JOIN UNIDADEDEMEDIDA UNIDEMED   ON (UNIDEMED.IDUNIDADEDEMEDIDA = PROCLI.IDUNIDADEDEMEDIDA)    ");
            txt.Append(" WHERE  ");
            txt.Append(" PROCLI.IDPRODUTOCLIENTE= " + idProdutoCliente);
            txt.Append(" AND ESTDIV.IDCLIENTEDIVISAO=" + idclienteDivisao);

            return Sistran.Library.GetDataTables.RetornarDecimal(txt.ToString(), "");

        }

        public DataTable Pesquisar(string texto, int idCliente)
        {
            System.Text.StringBuilder txt = new StringBuilder();

            txt.Append(" SELECT DISTINCT cd.Nome 'Nome', ESTDIV.IDCLIENTEDIVISAO, ProCli.IDProdutoCliente, ProEmb.IDProdutoEmbalagem, ProEmb.IDProduto,  ");
            txt.Append(" CASE WHEN ProCli.CodigoDoCliente is NULL THEN ProCli.Codigo ELSE 	ProCli.CodigoDoCliente   END as Codigo, ProCli.Descricao, ProEmb.Conteudo,    ");
            txt.Append(" Cast(COALESCE(EstDiv.Saldo, 0) - (Select Coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque From DocumentoItem DocIt    ");
            txt.Append(" inner Join Documento Doc On (Doc.IdDocumento = DocIt.IdDocumento)    inner Join ProdutoEmbalagem ProdE 	on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)     ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'    and Doc.TipoDeDocumento = 'PEDIDO'    ");
            txt.Append(" and Doc.Ativo = 'SIM'    and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'     and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO   ) as numeric (10,0)) AS SaldoDivisaoDisponivel,    ");
            txt.Append(" Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) As ValorUnitario,   UniDeMed.Decimais,    ");
            txt.Append(" 'R$ ' + Cast(Cast(COALESCE(ProEmb.ValorUnitario, 0) as numeric (10,2)) as varchar(12)) As RealValorUnitario,   null As nulo    ");
            txt.Append(" FROM EstoqueDivisao EstDiv    LEFT JOIN Estoque Est   ON(Est.IDEstoque = EstDiv.IDEstoque)    ");
            txt.Append(" LEFT JOIN ProdutoCliente ProCli   ON(ProCli.IDProdutoCliente = Est.IDProdutoCliente)    ");
            txt.Append(" LEFT JOIN ProdutoEmbalagem ProEmb   ON (ProEmb.IDProdutoCliente = ProCli.IDProdutoCliente)  ");
            txt.Append(" LEFT JOIN UnidadeDeMedida UniDeMed   ON (UniDeMed.IDUnidadeDeMedida = ProCli.IDUnidadeDeMedida) ");
            txt.Append(" inner join ClienteDivisao cd on (cd.IDClienteDivisao = EstDiv.IDClienteDivisao)");


            txt.Append(" WHERE ( ");
            txt.Append(" ProCli.Codigo like '%" + texto + "%' or   ");
            txt.Append(" ProCli.Descricao like '%"+texto+"%') ");
            txt.Append(" AND ProCli.idcliente =" + idCliente);
            txt.Append(" AND COALESCE(EstDiv.Saldo, 0) - (Select coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque    From DocumentoItem DocIt     ");
            txt.Append(" inner Join Documento Doc    On (Doc.IdDocumento = DocIt.IdDocumento)     ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE    on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)     ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'    and Doc.TipoDeDocumento = 'PEDIDO'   and Doc.Ativo = 'SIM'     ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente    and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'     ");
            txt.Append("   and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO	) > 0   AND EstDiv.IDClienteDivisao IN(" + HttpContext.Current.Session["Divisoes"] + ")");

            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

        }

        public DataTable ListarProdutoByDivisaoCliente(int IDClienteDivisao, bool EstoqueMaiorZero)
        {
            System.Text.StringBuilder txt = new StringBuilder();

            txt.Append(" SELECT  PE.IDProduto,");
            txt.Append(" 	ProCli.IDProdutoCliente,  ");
            txt.Append(" ProCli.Codigo, ");
            txt.Append(" CASE WHEN ProCli.CodigoDoCliente is NULL THEN ");
            txt.Append(" ProCli.Codigo ");
            txt.Append(" ELSE ");
            txt.Append(" ProCli.CodigoDoCliente ");
            txt.Append(" END as CodigoDoCliente,  ");
            txt.Append(" ProCli.Descricao,  ");
            txt.Append(" ProCli.Ativo, ");
            txt.Append(" ProCli.SaldoMinimo,  ");
            txt.Append(" ProCli.ConsumoMensal, ");
            txt.Append(" ( ");
            txt.Append(" Convert(varchar(10), ProCli.DataLimiteDeUso, 103) + ' ' + ");
            txt.Append(" Convert(varchar(08), ProCli.DataLimiteDeUso, 108) ");
            txt.Append(" ) AS DataLimiteDeUso, ");
            txt.Append(" ( ");
            txt.Append(" Convert(varchar(10), ProCli.DataDeCadastro, 103) + ' ' + ");
            txt.Append(" Convert(varchar(08), ProCli.DataDeCadastro, 108) ");
            txt.Append(" ) AS DataDeCadastro, ");
            txt.Append(" Cast((COALESCE(EstDiv.Saldo, 0) + COALESCE(EstDiv.SaldoBaseExterna, 0)) - ( ");
            txt.Append(" Select  ");
            txt.Append(" Coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque  ");
            txt.Append(" From DocumentoItem DocIt  ");
            txt.Append(" inner Join Documento Doc  ");
            txt.Append(" On (Doc.IdDocumento = DocIt.IdDocumento)  ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE  ");
            txt.Append(" on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)  ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'  ");
            txt.Append(" and Doc.TipoDeDocumento='PEDIDO' ");
            txt.Append(" and Doc.Ativo = 'SIM'  ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente  ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'   ");
            txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO ");
            txt.Append(" ) as numeric (10,0)) As Saldo, ");
            txt.Append(" UniDeMed.Decimais ");
            txt.Append(" FROM EstoqueDivisao EstDiv  ");
            txt.Append(" LEFT JOIN Estoque Est ");
            txt.Append(" ON(Est.IDEstoque = EstDiv.IDEstoque) ");
            txt.Append(" LEFT JOIN ProdutoCliente ProCli ");
            txt.Append(" ON(ProCli.IDProdutoCliente = Est.IDProdutoCliente) ");
            txt.Append(" LEFT JOIN UnidadeDeMedida UniDeMed ");
            txt.Append(" ON (UniDeMed.IDUnidadeDeMedida = ProCli.IDUnidadeDeMedida) ");
            txt.Append(" INNER JOIN PRODUTOEMBALAGEM PE ON  PROCLI.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ");
            txt.Append(" WHERE EstDiv.IDClienteDivisao =" + IDClienteDivisao.ToString());
            txt.Append(" AND (COALESCE(EstDiv.Saldo, 0) + COALESCE(EstDiv.SaldoBaseExterna, 0))  - ( ");
            txt.Append(" Select  ");
            txt.Append(" Coalesce(Sum(DocIt.QuantidadeUnidadeEstoque),0) As QuantidadeUnidadeEstoque  ");
            txt.Append(" From DocumentoItem DocIt  ");
            txt.Append(" inner Join Documento Doc  ");
            txt.Append(" On (Doc.IdDocumento = DocIt.IdDocumento)  ");
            txt.Append(" inner Join ProdutoEmbalagem ProdE  ");
            txt.Append(" on (ProdE.IdProdutoEmbalagem = DocIt.IdProdutoEmbalagem)  ");
            txt.Append(" where Doc.EntradaSaida = 'SAIDA'  ");
            txt.Append(" and Doc.Ativo = 'SIM'  ");
            txt.Append(" and Doc.TipoDeDocumento='PEDIDO' ");
            txt.Append(" and ProdE.IdProdutoCliente = ProCli.IDProdutoCliente  ");
            txt.Append(" and Coalesce(DocIt.EstoqueProcessado,'NAO') = 'NAO'   ");
            txt.Append(" and DocIt.IDCLIENTEDIVISAO = EstDiv.IDCLIENTEDIVISAO ");
            if(EstoqueMaiorZero)
                txt.Append(" ) > 0 ");
            else
                txt.Append(" ) >=0 ");

            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public DataTable ListarProdutosByIdProdutoCliente(string IdProdutoCliente)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append("  SELECT PRO.IDPRODUTO, PRO.CODIGODEBARRAS, PRO.PESOLIQUIDO, PRO.PESOBRUTO, PRO.ESPECIE, ");
            txt.Append(" PRO.LARGURA,  PRO.ALTURA, PRO.COMPRIMENTO,");
            txt.Append(" CAST((PRO.LARGURA * PRO.ALTURA * PRO.COMPRIMENTO) AS NUMERIC(10,3)) AS CUBAGEM,");
            txt.Append(" CAST(((PRO.LARGURA * PRO.ALTURA * PRO.COMPRIMENTO) * 300) AS NUMERIC(10,3)) AS RODOVIARIO,");
            txt.Append(" CAST(((PRO.LARGURA * PRO.ALTURA * PRO.COMPRIMENTO) * 165) AS NUMERIC(10,3)) AS AEREO, ");
            txt.Append(" CAST(PROEMB.UNIDADEDOCLIENTE AS NUMERIC(10,0)) AS UNIDADEDOCLIENTE,");
            txt.Append(" PROEMB.CONTEUDO");
            txt.Append(" FROM ");
            txt.Append(" PRODUTOEMBALAGEM PROEMB");
            txt.Append(" LEFT JOIN PRODUTO PRO");
            txt.Append(" ON (PROEMB.IDPRODUTO = PRO.IDPRODUTO)");
            txt.Append(" WHERE PROEMB.IDPRODUTOCLIENTE =" + IdProdutoCliente);
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public DataTable ListarProdutosFiltroConsultarMaterial(string codigo, string Descricao, bool ConsiderarSaldo, bool SemDivisao, string clientes)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT   ");
            txt.Append("    PROCLI.IDPRODUTOCLIENTE, PROCLI.CODIGO, PE.IDPRODUTO,  EST.SALDO SALDOESTOQUE, ");
            txt.Append("  CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO   ");
            txt.Append("  ELSE  ");
            txt.Append("  PROCLI.CODIGODOCLIENTE  ");
            txt.Append("  END AS CODIGODOCLIENTE,   ");
            txt.Append("  PROCLI.DESCRICAO,   ");
            txt.Append("  PROCLI.ATIVO,  ");
            txt.Append("  PROCLI.SALDOMINIMO, PROCLI.CONSUMOMENSAL,  ");
            txt.Append("  GRUDOPRO.DESCRICAO AS GRUPODEPRODUTO,   ");
            txt.Append("  CLITIPDEMAT.NOME AS CLIENTETIPODEMATERIAL,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATALIMITEDEUSO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATALIMITEDEUSO, 108)  ");
            txt.Append("  ) AS DATALIMITEDEUSO,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DTDECAD,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DATADECADASTRO,  ");
            txt.Append("  (  ");
            txt.Append("  COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0)  ");
            txt.Append("  ) AS SALDO,  ");
            txt.Append("  UNIDEMED.DECIMAIS,  ");
            txt.Append("  CLIDIV.NOME AS NOMEDIVISAO  ");
            txt.Append("  FROM  ");
            txt.Append("  PRODUTOCLIENTE PROCLI  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  UNIDADEDEMEDIDA UNIDEMED  ");
            txt.Append("  ON(UNIDEMED.IDUNIDADEDEMEDIDA = PROCLI.IDUNIDADEDEMEDIDA)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUE EST  ");
            txt.Append("  ON(EST.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUEDIVISAO ESTDIV  ");
            txt.Append("  ON(ESTDIV.IDESTOQUE = EST.IDESTOQUE)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTEDIVISAO CLIDIV  ");
            txt.Append("  ON(CLIDIV.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTETIPODEMATERIAL CLITIPDEMAT  ");
            txt.Append("  ON(CLITIPDEMAT.IDCLIENTETIPODEMATERIAL = PROCLI.IDCLIENTETIPODEMATERIAL)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  GRUPODEPRODUTO GRUDOPRO  ");
            txt.Append("  ON(GRUDOPRO.IDGRUPODEPRODUTO = PROCLI.IDGRUPODEPRODUTO  ");
            txt.Append("  AND GRUDOPRO.ATIVO = 'SIM')  ");
            txt.Append("  INNER JOIN PRODUTOEMBALAGEM PE ON  PE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE  ");
            txt.Append("  WHERE  ");
            txt.Append("  (PROCLI.CODIGODOCLIENTE LIKE '%" + codigo + "%' OR PROCLI.CODIGO LIKE '%" + codigo + "%' ) "+(Descricao.Trim()==""?"":" AND PROCLI.DESCRICAO LIKE '" + Descricao + "%'"));

            if (!SemDivisao)
            {
                txt.Append(" AND CLIDIV.IDCLIENTEDIVISAO IN (" + HttpContext.Current.Session["Divisoes"].ToString() + ") ");
                txt.Append("  AND (COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0))  > " + (ConsiderarSaldo == true ? "" : "=") + " 0  ");
            }
            else
                txt.Append("  AND (COALESCE(EST.SALDO, 0))  > " + (ConsiderarSaldo == true ? "" : "=") + " 0  ");
            
            txt.Append(" AND PROCLI.IDCLIENTE IN ("+clientes+")");

            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

        }

        public DataTable ListarProdutosCadastroProdutos(string codigo, string CodigoCliente)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT   ");
            txt.Append("    PROCLI.IDPRODUTOCLIENTE, PROCLI.CODIGO, PE.IDPRODUTO,  ");
            txt.Append("  CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO   ");
            txt.Append("  ELSE  ");
            txt.Append("  PROCLI.CODIGODOCLIENTE  ");
            txt.Append("  END AS CODIGODOCLIENTE,   ");
            txt.Append("  PROCLI.DESCRICAO,   ");
            txt.Append("  PROCLI.ATIVO,  ");
            txt.Append("  PROCLI.SALDOMINIMO, PROCLI.CONSUMOMENSAL,  ");
            txt.Append("  GRUDOPRO.DESCRICAO AS GRUPODEPRODUTO,   ");
            txt.Append("  CLITIPDEMAT.NOME AS CLIENTETIPODEMATERIAL,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATALIMITEDEUSO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATALIMITEDEUSO, 108)  ");
            txt.Append("  ) AS DATALIMITEDEUSO,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DTDECAD,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DATADECADASTRO,  ");
            txt.Append("  (  ");
            txt.Append("  COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0)  ");
            txt.Append("  ) AS SALDO,  ");
            txt.Append("  UNIDEMED.DECIMAIS,  ");
            txt.Append("  CLIDIV.NOME AS NOMEDIVISAO  ");
            txt.Append("  FROM  ");
            txt.Append("  PRODUTOCLIENTE PROCLI  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  UNIDADEDEMEDIDA UNIDEMED  ");
            txt.Append("  ON(UNIDEMED.IDUNIDADEDEMEDIDA = PROCLI.IDUNIDADEDEMEDIDA)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUE EST  ");
            txt.Append("  ON(EST.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUEDIVISAO ESTDIV  ");
            txt.Append("  ON(ESTDIV.IDESTOQUE = EST.IDESTOQUE)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTEDIVISAO CLIDIV  ");
            txt.Append("  ON(CLIDIV.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTETIPODEMATERIAL CLITIPDEMAT  ");
            txt.Append("  ON(CLITIPDEMAT.IDCLIENTETIPODEMATERIAL = PROCLI.IDCLIENTETIPODEMATERIAL)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  GRUPODEPRODUTO GRUDOPRO  ");
            txt.Append("  ON(GRUDOPRO.IDGRUPODEPRODUTO = PROCLI.IDGRUPODEPRODUTO  ");
            txt.Append("  AND GRUDOPRO.ATIVO = 'SIM')  ");
            txt.Append("  INNER JOIN PRODUTOEMBALAGEM PE ON  PE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE  ");
            txt.Append("  WHERE  ");

            txt.Append("  CLIDIV.IDCLIENTEDIVISAO IN (" + HttpContext.Current.Session["Divisoes"].ToString() + ") ");
            txt.Append("  AND (COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0))  > =0  ");

            if (codigo.Trim().Length > 0)
            {
                txt.Append("  AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO     ELSE    PROCLI.CODIGODOCLIENTE END LIKE '%" + codigo + "%'   ");

            }

            if (CodigoCliente.Trim().Length > 0)
            {
                txt.Append("  AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO     ELSE    PROCLI.CODIGODOCLIENTE END LIKE '%" + CodigoCliente + "%' ");
            }
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

        }

        public DataTable Listar(string codigo, string CodigoCliente, string Descricao)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT   ");
            txt.Append("    PROCLI.IDPRODUTOCLIENTE, PROCLI.CODIGO, PE.IDPRODUTO,  ");
            txt.Append("  CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO   ");
            txt.Append("  ELSE  ");
            txt.Append("  PROCLI.CODIGODOCLIENTE  ");
            txt.Append("  END AS CODIGODOCLIENTE,   ");
            txt.Append("  PROCLI.DESCRICAO,   ");
            txt.Append("  PROCLI.ATIVO,  ");
            txt.Append("  PROCLI.SALDOMINIMO, PROCLI.CONSUMOMENSAL,  ");
            txt.Append("  GRUDOPRO.DESCRICAO AS GRUPODEPRODUTO,   ");
            txt.Append("  CLITIPDEMAT.NOME AS CLIENTETIPODEMATERIAL,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATALIMITEDEUSO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATALIMITEDEUSO, 108)  ");
            txt.Append("  ) AS DATALIMITEDEUSO,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DTDECAD,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DATADECADASTRO,  ");
            txt.Append("  (  ");
            txt.Append("  COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0)  ");
            txt.Append("  ) AS SALDO,  ");
            txt.Append("  UNIDEMED.DECIMAIS,  ");
            txt.Append("  CASE WHEN ESTDIV.ATIVO = 'NAO' THEN CLIDIV.NOME  + ' (DESABILITADO) '  ELSE  CLIDIV.NOME END  AS NOMEDIVISAO  ");
//            txt.Append("  CLIDIV.NOME AS NOMEDIVISAO  ");
            txt.Append("  FROM  ");
            txt.Append("  PRODUTOCLIENTE PROCLI  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  UNIDADEDEMEDIDA UNIDEMED  ");
            txt.Append("  ON(UNIDEMED.IDUNIDADEDEMEDIDA = PROCLI.IDUNIDADEDEMEDIDA)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUE EST  ");
            txt.Append("  ON(EST.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUEDIVISAO ESTDIV  ");
            txt.Append("  ON(ESTDIV.IDESTOQUE = EST.IDESTOQUE)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTEDIVISAO CLIDIV  ");
            txt.Append("  ON(CLIDIV.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTETIPODEMATERIAL CLITIPDEMAT  ");
            txt.Append("  ON(CLITIPDEMAT.IDCLIENTETIPODEMATERIAL = PROCLI.IDCLIENTETIPODEMATERIAL)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  GRUPODEPRODUTO GRUDOPRO  ");
            txt.Append("  ON(GRUDOPRO.IDGRUPODEPRODUTO = PROCLI.IDGRUPODEPRODUTO  ");
            txt.Append("  AND GRUDOPRO.ATIVO = 'SIM')  ");
            txt.Append("  INNER JOIN PRODUTOEMBALAGEM PE ON  PE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE  ");
            txt.Append("  WHERE  ");

            txt.Append("  CLIDIV.IDCLIENTEDIVISAO IN (" + HttpContext.Current.Session["Divisoes"].ToString() + ") ");
            txt.Append("  AND (COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0))  > =0  ");

            if (codigo.Trim().Length > 0)
            {
                txt.Append("  AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO     ELSE    PROCLI.CODIGODOCLIENTE END LIKE '%" + codigo + "%'   ");

            }

            if (CodigoCliente.Trim().Length > 0)
            {
                txt.Append("  AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO     ELSE    PROCLI.CODIGODOCLIENTE END LIKE '%" + CodigoCliente + "%' ");
            }

            if (Descricao.Trim().Length > 0)
            {
                txt.Append("  AND  PROCLI.DESCRICAO  LIKE '%"+Descricao+"%' ");

               
            }
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

        }

        public DataTable ListarTelaCadastro(string codigo, string CodigoCliente, string Descricao)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT   ");
            txt.Append("    PROCLI.IDPRODUTOCLIENTE, PROCLI.CODIGO, PE.IDPRODUTO,  ");
            txt.Append("  CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO   ");
            txt.Append("  ELSE  ");
            txt.Append("  PROCLI.CODIGODOCLIENTE  ");
            txt.Append("  END AS CODIGODOCLIENTE,   ");
            txt.Append("  PROCLI.DESCRICAO,   ");
            txt.Append("  PROCLI.ATIVO,  ");
            txt.Append("  PROCLI.SALDOMINIMO, PROCLI.CONSUMOMENSAL,  ");
            txt.Append("  GRUDOPRO.DESCRICAO AS GRUPODEPRODUTO,   ");
            txt.Append("  CLITIPDEMAT.NOME AS CLIENTETIPODEMATERIAL,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATALIMITEDEUSO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATALIMITEDEUSO, 108)  ");
            txt.Append("  ) AS DATALIMITEDEUSO,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DTDECAD,  ");
            txt.Append("  (  ");
            txt.Append("  CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +  ");
            txt.Append("  CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108)  ");
            txt.Append("  )AS DATADECADASTRO,  ");
            txt.Append("  (  ");
            txt.Append("  COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0)  ");
            txt.Append("  ) AS SALDO,  ");
            txt.Append("  UNIDEMED.DECIMAIS,  ");
            txt.Append("  CASE WHEN ESTDIV.ATIVO = 'NAO' THEN CLIDIV.NOME  + ' (DESABILITADO) '  ELSE  CLIDIV.NOME END  AS NOMEDIVISAO  ");
            //            txt.Append("  CLIDIV.NOME AS NOMEDIVISAO  ");
            txt.Append("  FROM  ");
            txt.Append("  PRODUTOCLIENTE PROCLI  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  UNIDADEDEMEDIDA UNIDEMED  ");
            txt.Append("  ON(UNIDEMED.IDUNIDADEDEMEDIDA = PROCLI.IDUNIDADEDEMEDIDA)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUE EST  ");
            txt.Append("  ON(EST.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  ESTOQUEDIVISAO ESTDIV  ");
            txt.Append("  ON(ESTDIV.IDESTOQUE = EST.IDESTOQUE AND (ESTDIV.ATIVO='SIM' OR ESTDIV.ATIVO='' OR ESTDIV.ATIVO IS NULL ))  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTEDIVISAO CLIDIV  ");
            txt.Append("  ON(CLIDIV.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  CLIENTETIPODEMATERIAL CLITIPDEMAT  ");
            txt.Append("  ON(CLITIPDEMAT.IDCLIENTETIPODEMATERIAL = PROCLI.IDCLIENTETIPODEMATERIAL)  ");
            txt.Append("  LEFT JOIN  ");
            txt.Append("  GRUPODEPRODUTO GRUDOPRO  ");
            txt.Append("  ON(GRUDOPRO.IDGRUPODEPRODUTO = PROCLI.IDGRUPODEPRODUTO  ");
            txt.Append("  AND GRUDOPRO.ATIVO = 'SIM')  ");
            txt.Append("  INNER JOIN PRODUTOEMBALAGEM PE ON  PE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE  ");
            txt.Append("  WHERE  ");

            txt.Append("  CLIDIV.IDCLIENTEDIVISAO IN (" + HttpContext.Current.Session["Divisoes"].ToString() + ") ");
            txt.Append("  AND (COALESCE(ESTDIV.SALDO, 0) + COALESCE(ESTDIV.SALDOBASEEXTERNA, 0))  >= 0  ");

            if (codigo.Trim().Length > 0)
            {
                txt.Append("  AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO     ELSE    PROCLI.CODIGODOCLIENTE END LIKE '%" + codigo + "%'   ");

            }

            if (CodigoCliente.Trim().Length > 0)
            {
                txt.Append("  AND CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN  PROCLI.CODIGO     ELSE    PROCLI.CODIGODOCLIENTE END LIKE '%" + CodigoCliente + "%' ");
            }

            if (Descricao.Trim().Length > 0)
            {
                txt.Append("  AND  PROCLI.DESCRICAO  LIKE '%" + Descricao + "%' ");


            }
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

        }

        public DataTable ListarSaldoOrigem( string IdProdutoCliente, string IdCliente)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append("  SELECT PC.CODIGO,CD.NOME,IDPARENTE,ET.SALDO,");
            txt.Append("  ED.SALDO SALDODIVISAO,");
            txt.Append("  CD.IDCLIENTEDIVISAO,");
            txt.Append("  ED.SALDO - ");
            txt.Append("  (SELECT COALESCE(SUM(DOCIT.QUANTIDADEUNIDADEESTOQUE),0) AS QUANTIDADEUNIDADEESTOQUE ");
            txt.Append("  FROM DOCUMENTOITEM DOCIT ");
            txt.Append("  INNER JOIN DOCUMENTO DOC ");
            txt.Append("  ON (DOC.IDDOCUMENTO = DOCIT.IDDOCUMENTO) ");
            txt.Append("  INNER JOIN PRODUTOEMBALAGEM PRODE ");
            txt.Append("  ON (PRODE.IDPRODUTOEMBALAGEM = DOCIT.IDPRODUTOEMBALAGEM) ");
            txt.Append("  WHERE DOC.ENTRADASAIDA = 'SAIDA'");
            txt.Append("  AND DOC.TIPODEDOCUMENTO='PEDIDO'");
            txt.Append("  AND DOC.ATIVO = 'SIM' ");
            txt.Append("  AND DOC.IDCLIENTE =" + IdCliente);
            txt.Append("  AND PRODE.IDPRODUTOCLIENTE =" + IdProdutoCliente);
            txt.Append("  AND COALESCE(DOCIT.ESTOQUEPROCESSADO,'NAO') = 'NAO'  ");
            txt.Append("  AND DOCIT.IDCLIENTEDIVISAO = CD.IDCLIENTEDIVISAO)  SALDODISPONIVEL, ");
            txt.Append("  ED.IDESTOQUEDIVISAO  ");
            txt.Append("  FROM PRODUTOCLIENTE PC");
            txt.Append("  INNER JOIN ESTOQUE ET ON (ET.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE) ");
            txt.Append("  INNER JOIN ESTOQUEDIVISAO ED ON (ED.IDESTOQUE = ET.IDESTOQUE)");
            txt.Append("  INNER JOIN CLIENTEDIVISAO CD ON (CD.IDCLIENTEDIVISAO = ED.IDCLIENTEDIVISAO)");
            txt.Append("  WHERE PC.IDPRODUTOCLIENTE = "+ IdProdutoCliente);
            txt.Append("  AND   ED.SALDO > 0");
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public DataTable ListarRptPedidos(string Codigo)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append("  SELECT ");
            txt.Append("  DOC.NUMERO, ");
            txt.Append("  PC.DESCRICAO,  ");
            txt.Append("  PC.CODIGO,  ");
            txt.Append("  DOC.DATADOMOVIMENTO,  ");
            txt.Append("  DI.QUANTIDADE,  ");
            txt.Append("  DI.IDUSUARIO,  ");
            txt.Append("  USU.NOME,  ");
            txt.Append("  CADDEST.RAZAOSOCIALNOME DESTINATARIO ");
            txt.Append("  FROM DOCUMENTO DOC ");
            txt.Append("  INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTO = DOC.IDDOCUMENTO ");
            txt.Append("  INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ");
            txt.Append("  INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ");
            txt.Append("  INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = DOC.IDDESTINATARIO ");
            txt.Append("  INNER JOIN USUARIO USU ON  USU.IDUSUARIO = DI.IDUSUARIO ");
            txt.Append("  WHERE TIPODEDOCUMENTO='PEDIDO'  ");
            txt.Append("  AND ORIGEM='INTERNET'  ");
            txt.Append("  AND CODIGO ='"+Codigo+"' ");
            txt.Append("  ORDER BY DOC.NUMERO, PC.DESCRICAO ");
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");            
        }

        public DataTable ListarRptPedidosConsolidados(string Codigo, DateTime? inicio, DateTime? fim, int? IdDestinatario)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT  ");
            txt.Append(" CAST(DOC.DATADEEMISSAO AS DATE) DATA,   ");
            txt.Append(" PC.CODIGO,  ");
            txt.Append(" pc.Descricao, ");
            txt.Append(" SUM(DIT.QUANTIDADE) QTD, ");
            txt.Append(" DEST.CNPJCPF CNPJCPF_DESTINATARIO,  ");
            txt.Append(" ISNULL(DEST.RAZAOSOCIALNOME, DEST.FANTASIAAPELIDO) AS DESTINATARIO ");
            txt.Append(" FROM DOCUMENTO DOC ");
            txt.Append(" INNER JOIN DOCUMENTOITEM DIT ON DIT.IDDOCUMENTO = DOC.IDDOCUMENTO ");
            txt.Append(" INNER JOIN PRODUTOEMBALAGEM PEM ON PEM.IDPRODUTOEMBALAGEM = DIT.IDPRODUTOEMBALAGEM ");
            txt.Append(" INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PEM.IDPRODUTOCLIENTE ");
            txt.Append(" INNER JOIN CADASTRO DEST ON DEST.IDCADASTRO = DOC.IDDESTINATARIO ");
            txt.Append(" WHERE  ");
            txt.Append(" DOC.TIPODEDOCUMENTO='PEDIDO'  ");
            txt.Append(" AND DOC.ATIVO='SIM'  ");
            txt.Append(" AND DATADECANCELAMENTO IS NULL ");

            if (Codigo.Length > 0)
                txt.Append(" AND PC.CODIGO='"+Codigo+"'");

            if (inicio!=null && fim !=null)
                txt.Append(" AND CAST(DOC.DATADEEMISSAO AS DATE) BETWEEN '" + Convert.ToDateTime(inicio).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(fim).ToString("yyyy-MM-dd") + "'");
            
            if(IdDestinatario !=null)
                txt.Append(" AND DEST.IDCADASTRO=" + IdDestinatario );


            txt.Append(" GROUP BY  ");
            txt.Append(" CAST(DOC.DATADEEMISSAO AS DATE),   ");
            txt.Append(" PC.CODIGO,  ");
            txt.Append(" pc.Descricao, ");
            txt.Append(" DEST.CNPJCPF ,  ");
            txt.Append(" ISNULL(DEST.RAZAOSOCIALNOME, DEST.FANTASIAAPELIDO)  ");
            txt.Append(" ORDER BY DATA DESC ");


            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public DataTable ListarProdutoIniciais(string Codigo, string Descricao, string IdCliente)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append("  SELECT TOP 20 IDProdutoCliente, Codigo, Descricao FROM PRODUTOCLIENTE WHERE IDCLIENTE = " + IdCliente);
            
            if(Codigo.Length>0)
                txt.Append("  AND CODIGO LIKE '" + Codigo + "%' ");

            if(Descricao.Length>0)
                txt.Append("  AND DESCRICAO LIKE '%" + Descricao + "%' ");
            
            

            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }
        
        public DataTable ListarProdutoIniciais2(string str)
        {
            System.Text.StringBuilder txt = new StringBuilder();
            txt.Append("  SELECT IDProdutoCliente, Codigo, Descricao FROM PRODUTOCLIENTE WHERE IDCLIENTE = 7588 AND (CODIGO LIKE '%" + str + "%' OR DESCRICAO LIKE '%" + str + "%')");
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public sealed class GrupoProduto
        {
            public DataTable ListarMenu(int idCliente)
            {
                string strsql = "SELECT DISTINCT CASE WHEN PC.IDGRUPODEPRODUTO IS NOT NULL THEN GP.DESCRICAO ELSE 'OUTROS' END NOMEGRUPO, GP.CODIGO FROM PRODUTOCLIENTE PC LEFT JOIN GRUPODEPRODUTO GP ON (PC.IDGRUPODEPRODUTO = GP.IDGRUPODEPRODUTO) WHERE PC.IDCLIENTE=" + idCliente.ToString() + " ORDER BY 1";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }

            public DataTable ListarGrupoDeProdutos()
            {
                //string strsql = " SELECT DISTINCT PC.IDGRUPODEPRODUTO,CAST(PC.IDGRUPODEPRODUTO AS NVARCHAR(MAX)) + '-' + GP.DESCRICAO 'NOME' FROM PRODUTOCLIENTE PC ";
                //strsql += " INNER JOIN GRUPODEPRODUTO GP ON PC.IDGRUPODEPRODUTO = PC.IDGRUPODEPRODUTO ";
                //strsql += " WHERE PC.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes()  +") ";
                //strsql += " AND GP.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
                //strsql += " AND PC.ATIVO='SIM' ";
                //strsql += " AND GP.ATIVO='SIM'; ";
                string strsql = " SELECT IDGRUPODEPRODUTO, DESCRICAO  FROM GRUPODEPRODUTO WHERE IDCLIENTE  IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";

                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }
        }
        public sealed class Estoque
        {
            public int ConsultarEstoqueByIDProdutoCliente(int IDProdutoCliente)
            {
                string strsql = "SELECT IDESTOQUE FROM ESTOQUE WHERE IDPRODUTOCLIENTE=" + IDProdutoCliente;
                return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
            }  
 
             public int ConsultarEstoqueDivisao(int IDEstoque, int IdClienteDivisao)
            {
                string strsql = " SELECT IDEstoqueDivisao FROM ESTOQUEDIVISAO WHERE IDCLIENTEDIVISAO =" + IdClienteDivisao + " AND IDESTOQUE =" + IDEstoque.ToString();
                return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
            }

             public int InserirEstoque(string IDProdutoCliente, string IDFilial,string Saldo)
             {
                 string ID = Sistran.Library.GetDataTables.RetornarIdTabela("ESTOQUE");
                 string strsql = "INSERT INTO ESTOQUE(IDEstoque, IDProdutoCliente, IDFilial,Saldo) VALUES ("+ID+","+  IDProdutoCliente + ", "+IDFilial+","+Saldo+") ";
                 Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");
                 return Convert.ToInt32(ID);
                 
             }

             public void AdicionarSaldoTabelaEstoque(string IDEstoque, string Qtd)
             {
                 string strsql = " UPDATE ESTOQUE SET SALDO = SALDO + " + Qtd.Replace(",", ".") + " WHERE IDESTOQUE=" + IDEstoque;
                 Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
             }

            public int InserirEstoqueDivisao(int IDEstoque, int IDClienteDivisao, int Saldo)
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("ESTOQUEDIVISAO");

                string strsql = " INSERT INTO ESTOQUEDIVISAO (IDESTOQUEDIVISAO,IDESTOQUE,IDCLIENTEDIVISAO,SALDO)";
                strsql += " VALUES ( "+ID+"  , " + IDEstoque.ToString() + " ," + IDClienteDivisao.ToString() + ", " + Saldo.ToString() + ") SELECT ISNULL(MAX(IDESTOQUEDIVISAO), 0) FROM ESTOQUEDIVISAO";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");
                return Convert.ToInt32(ID);
                 
            }

            public int retornarIdEstoqueDivisao(string IDEstoque, string IDClienteDivisao )
            {
               string strsql = "SELECT IDESTOQUEDIVISAO FROM ESTOQUEDIVISAO WHERE IDESTOQUE ="+ IDEstoque + " AND IDCLIENTEDIVISAO =" + IDClienteDivisao;
               return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            }
            
            public int AtualizarEstoqueDivisao(int IDESTOQUEDIVISAO, int Saldo, string Operador)
            {
                string strsql = "UPDATE ESTOQUEDIVISAO SET SALDO = SALDO " + Operador + "  " + Saldo.ToString() +" WHERE IDESTOQUEDIVISAO=" + IDESTOQUEDIVISAO.ToString() ;
                return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
            }

            public int InserirMovimentacaoEntradaInicial(string idestoqueDivisaoDestino, string UsuarioID, string saldo)
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("EstoqueDivisaoMov");
                StringBuilder txt = new StringBuilder();
                txt.Append("Insert into EstoqueDivisaoMov (");
                txt.Append(" IDEstoqueDivisaoMov,");
                txt.Append(" IDEstoqueDivisao,");
                txt.Append(" IDEstoqueOperacao,");
                txt.Append(" IDEstoqueDivisaoOrigem,");                
                txt.Append(" IDUsuario,");
                txt.Append(" Historico,");
                txt.Append(" Quantidade,  ");
                txt.Append(" Saldo, DataHora ");
                txt.Append(") Values (");
                txt.Append(ID +",");
                txt.Append(idestoqueDivisaoDestino + " ,");
                txt.Append(" 1,");
                txt.Append( "null,");
                txt.Append(UsuarioID + ",");
                txt.Append(" 'Carga Feita pelo Site Acerto de Estoque WEB ',");
                txt.Append(saldo + " ,  ");
                txt.Append(saldo + ", getdate()");
                txt.Append(") ");
                Sistran.Library.GetDataTables.ExecutarRetornoID(txt.ToString(), "");
                return Convert.ToInt32(idestoqueDivisaoDestino);
            }

            public int InserirMovimentacaoEntrada(string idestoqueDivisaoDestino, string IDEstoqueDivisaoOrigem, string UsuarioID, string saldo )
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("EstoqueDivisaoMov");

                StringBuilder txt = new StringBuilder();
                txt.Append("Insert into EstoqueDivisaoMov (");
                txt.Append(" IDEstoqueDivisaoMov,");
                txt.Append(" IDEstoqueDivisao,");
                txt.Append(" IDEstoqueOperacao,");
                txt.Append(" IDEstoqueDivisaoOrigem,");                
                txt.Append(" IDUsuario,");
                txt.Append(" Historico,");
                txt.Append(" Quantidade,  ");
                txt.Append(" Saldo, DataHora ");
                
                txt.Append(") Values (");

                txt.Append(ID+ " ,");
                txt.Append( idestoqueDivisaoDestino + " ,");
                txt.Append(" 1,");
                //txt.Append(" ,");
                txt.Append(IDEstoqueDivisaoOrigem + ",");
                txt.Append(UsuarioID  +",");
                txt.Append(" 'Divisão de origem ("+IDEstoqueDivisaoOrigem +"). Divisao de destino ("+idestoqueDivisaoDestino +")',");
                txt.Append( saldo +" ,  ");
                txt.Append( saldo + ", getdate())");
                Sistran.Library.GetDataTables.ExecutarRetornoID(txt.ToString(), "");
                return Convert.ToInt32(ID);
            }

            public int InserirMovimentacaoSaida(string idestoqueDivisaoDestino, string IDEstoqueDivisaoOrigem, string UsuarioID, string saldo)
            {
                StringBuilder txt = new StringBuilder();
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("EstoqueDivisaoMov");

                txt.Append("Insert into EstoqueDivisaoMov (");
                txt.Append(" IDEstoqueDivisaoMov,");
                txt.Append(" IDEstoqueDivisao,");
                txt.Append(" IDEstoqueOperacao,");
                //txt.Append(" IDEstoqueDivisaoOrigem,");
               // txt.Append(" IDEstoqueDivisaoDestino,");
                txt.Append(" IDUsuario,");
                txt.Append(" Historico,");
                txt.Append(" Quantidade,  ");
                txt.Append(" Saldo, DataHora ");

                txt.Append(") Values (");

                txt.Append(ID+" ,");
                txt.Append(IDEstoqueDivisaoOrigem  + " ,");
                txt.Append(" 2,");
                //txt.Append(" ,");
                //txt.Append(idestoqueDivisaoDestino + ",");
                txt.Append(UsuarioID + ",");
                txt.Append(" 'Divisão de origem (" + idestoqueDivisaoDestino  + "). Divisao de destino (" + IDEstoqueDivisaoOrigem + ")',");
                txt.Append(saldo + " ,  ");
                txt.Append(saldo + ", getdate())");

                Sistran.Library.GetDataTables.ExecutarRetornoID(txt.ToString(), "");
                return Convert.ToInt32(idestoqueDivisaoDestino);
            }

            public int InserirMovimentacaoSaidaEDI(string idestoqueDivisaoDestino, string IDEstoqueDivisaoOrigem, string UsuarioID, string saldo)
            {
                StringBuilder txt = new StringBuilder();
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("EstoqueDivisaoMov");

                txt.Append("Insert into EstoqueDivisaoMov (");
                txt.Append(" IDEstoqueDivisaoMov,");
                txt.Append(" IDEstoqueDivisao,");
                txt.Append(" IDEstoqueOperacao,");
                //txt.Append(" IDEstoqueDivisaoOrigem,");
                // txt.Append(" IDEstoqueDivisaoDestino,");
                txt.Append(" IDUsuario,");
                txt.Append(" Historico,");
                txt.Append(" Quantidade,  ");
                txt.Append(" Saldo, DataHora ");

                txt.Append(") Values (");

                txt.Append(ID+" ,");
                txt.Append(IDEstoqueDivisaoOrigem + " ,");
                txt.Append(" 2,");
                //txt.Append(" ,");
                //txt.Append(idestoqueDivisaoDestino + ",");
                txt.Append(UsuarioID + ",");
                txt.Append(" 'SAIDA EDI WEB',");
                txt.Append(saldo.Replace(",",".") + " ,  ");
                txt.Append(saldo.Replace(",", ".") + ", getdate())");

                //txt.Append(") SELECT MAX(IDEstoqueDivisaoMov) FROM EstoqueDivisaoMov");

                Sistran.Library.GetDataTables.ExecutarRetornoID(txt.ToString(), "");
                return Convert.ToInt32(idestoqueDivisaoDestino);
            }  
           
        }
    }
}