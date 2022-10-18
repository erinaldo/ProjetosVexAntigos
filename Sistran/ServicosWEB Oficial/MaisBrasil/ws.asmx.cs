using ServicosWEB.Util;
using Sistecno;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using static ProcurarEndereco2;

namespace ServicosWEB.MaisBrasil
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ws : System.Web.Services.WebService
    {
        //[WebMethod]
        //public RetornoStatusNfes RetornarStatusNfe(string usuario, string senha, string chave)
        //{
        //    RetornoStatusNfes ret = new RetornoStatusNfes();
        //    if (usuario != "girotrade" || senha != "@giro2020")
        //    {
        //        ret.Erro = true;
        //        ret.DescErro = "Usuário ou senha inválidos";
        //        return ret;
        //    }


        //    try
        //    {
        //        string sql = " select do.IDDocumentoOcorrencia ID, d.Numero, d.Serie, do.DataOcorrencia, isnull(o.Nome, do.descricao)  DescricaoOcorrencia, ISNULL(o.Codigo, 999) CodigoOcorrencia, " +
        //    // " Case o.Finalizador When 'SIM' then 'Processo Finalizado' else ''  end StatusFinalizada" +
        //    "Case ISNULL(o.Codigo, 999) When '0' then 'Processo Finalizado' else ''  end StatusFinalizada "+
        //        " from Documento d" +
        //        " Left" +
        //        " join DocumentoOcorrencia do on do.IDDocumento = d.IDDocumento" +
        //        " Left join Ocorrencia o on o.IDOcorrencia = do.IDOcorrencia" +
        //        " Left join DocumentoFilial df on df.IDDocumento = d.IDDocumento" +
        //        " where DocumentodoCliente4 = '"+chave+"'" +
        //        " and(IdOcorrenciaSerie = 3 or o.IDOcorrencia = 22603 Or do.Descricao like '%VEICULO FOI LIBERADO USUARIO:%')";

        //        string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
        //        var dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

        //        List<Ocorrencias> lista = new List<Ocorrencias>();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            lista.Add(new Ocorrencias()
        //            {
        //                DataOcorrencia = dt.Rows[i]["DataOcorrencia"].ToString(),
        //                ID = dt.Rows[i]["ID"].ToString(),
        //                Numero = dt.Rows[i]["Numero"].ToString(),
        //                Serie = dt.Rows[i]["Serie"].ToString(),
        //                StatusFinalizada = dt.Rows[i]["StatusFinalizada"].ToString(),
        //                CodigoOcorrencia = dt.Rows[i]["CodigoOcorrencia"].ToString().Trim(),
        //                DescricaoOcorrencia = (dt.Rows[i]["DescricaoOcorrencia"].ToString().Contains("VEICULO FOI LIBERADO USUARIO") ? "MERCADORIA EMBARCADA" : dt.Rows[i]["DescricaoOcorrencia"].ToString())
        //            });
        //        }

        //        ret.Ocorrencias = lista;
        //        return ret;
        //    }
        //    catch (Exception)
        //    {
        //        ret.Erro = true;
        //        ret.DescErro = "Não foi possível recuperdar informações de entrega desta NFe.";
        //        return ret;
        //    }
        //}

        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\Pedido\\NEEntradaMaisBrasil.log")]
        public string RemessaDeRecebimento(string Login, string Senha, Recebimento Recebimento, string CNPJdoCliente)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

           

            try
            {
                if (Login != "321500" && Senha != "321500")
                    throw new Exception("Credenciais Inválidas");

                if(Recebimento.Remetente.Fantasia=="")
                {
                    Recebimento.Remetente.Fantasia = (Recebimento.Remetente.RazaoSocial.Length > 30 ? Recebimento.Remetente.RazaoSocial.Substring(0, 29) : Recebimento.Remetente.RazaoSocial);
                }

                if (Recebimento.Remetente.RazaoSocial == "" || Recebimento.Remetente.Fantasia == "" || Recebimento.Remetente.CNPJ == "" || Recebimento.Remetente.IE == "" || Recebimento.Remetente.Endereco == "" ||
                    Recebimento.Remetente.TipoDeEndereco == "" || Recebimento.Remetente.Numero == "" || Recebimento.Remetente.CodigoIBGE == 0 || Recebimento.Remetente.UF == "")
                    throw new Exception("Dados do Remetente Inválido(s).");

                if (Recebimento.Destinatario.RazaoSocial == "" || Recebimento.Destinatario.Fantasia == "" || Recebimento.Destinatario.CNPJ == "" || Recebimento.Destinatario.IE == "" || Recebimento.Destinatario.Endereco == "" ||
                   Recebimento.Destinatario.TipoDeEndereco == "" || Recebimento.Destinatario.Numero == "" || Recebimento.Destinatario.CodigoIBGE == 0 || Recebimento.Destinatario.UF == "")
                    throw new Exception("Dados do Destinatario Inválido(s).");

                if(Recebimento.Itens == null || Recebimento.Itens.Count==0)
                    throw new Exception("NFe não pode ser recebido por falta de itens.");


                FormatarCnpj(Recebimento.Destinatario.CNPJ);

                var idRem = "";
                var idDest = "";
                idRem = VerificaCadastro(Recebimento.Remetente);
                idDest = VerificaCadastro(Recebimento.Destinatario);


             string   sql = " SELECT count(*)  FROM DOCUMENTO WHERE IDCLIENTE="+idDest+" AND  IDREMETENTE=" + idRem + " AND NUMERO ='" + Recebimento.NumeroNfe + "' AND TIPODEDOCUMENTO='NOTA FISCAL' AND SERIE = '" + Recebimento.Serie + "' AND ENTRADASAIDA='ENTRADA' AND ATIVO='SIM' AND DATADECANCELAMENTO IS NULL ";
                if (Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString() != "0")
                    throw new Exception("Pedido de entrada já existente");


                string sqlRem = "SELECT * FROM Cadastro WHERE IdCadastro=" + idRem;
                DataTable dtRem = Sistran.Library.GetDataTables.RetornarDataTableWS(sqlRem, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                string sqlDest = "SELECT * FROM Cadastro WHERE IdCadastro=" + idDest;
                DataTable dtDest = Sistran.Library.GetDataTables.RetornarDataTableWS(sqlDest, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                string idDocumento = Sistran.Library.GetDataTables.RetornarIdTabela("Documento", cnx);
                string sqlGeral = " Insert into Documento (DocumentodoCliente4, IDDocumento,IDFilial,IDFilialAtual,TipoDeServico,TipoDeDocumento,Serie,Numero,AnoMes,NumeroOriginal,IDCliente,IDRemetente,IDDestinatario,ClasseCFOP,Origem,EntradaSaida,DataDeEntrada,DataDeEmissao,Ativo,Endereco,EnderecoNumero,EnderecoComplemento,IDEnderecoBairro,IDEnderecoCidade,EnderecoCep,CodigoDoRecExpImpresso) ";
                sqlGeral += "values ('" + Recebimento.Chave + "'," + idDocumento + ",37,37,'NORMAL','NOTA FISCAL','" + Recebimento.Serie + "','" + Recebimento.NumeroNfe + "','" + DateTime.Parse(Recebimento.DataEmissaoNf.ToString()).ToString("yyyy/MM") + "','', " + idDest + "," + idRem + "," + idDest + ",5,'WS_ROBO','ENTRADA',GETDATE(),'" + Recebimento.DataEmissaoNf.ToString("yyyy-MM-dd") + "','SIM','" + dtDest.Rows[0]["Endereco"].ToString() + "','" + dtDest.Rows[0]["Numero"].ToString() + "','" + dtDest.Rows[0]["Complemento"].ToString() + "', '" + dtDest.Rows[0]["IdBairro"].ToString() + "','" + dtDest.Rows[0]["IdCidade"].ToString() + "','" + dtDest.Rows[0]["CEP"].ToString() + "','NAO') ";

                // var prods = Recebimento.Itens.Select(x => new { x.Codigo }).Distinct().ToList();

                //List<string> CdsJaExistentesNoInsert = new List<string>();
                //List<string> CdBarrassJaExistentesNoInsert = new List<string>();
                CadastrarProdutos(Recebimento);

                for (int iped = 0; iped < Recebimento.Itens.Count; iped++)
                //for (int iped = 0; iped < prods.Count; iped++)
                {
                    
                    //  var produtos = Recebimento.Itens.Where(x => x.Codigo == prods[iped].Codigo);


                    #region Verificações

                    if (Recebimento.Itens[iped].Codigo.Length > 20)
                        Recebimento.Itens[iped].Codigo = Recebimento.Itens[iped].Codigo.Substring(0, 20);

                    if (Recebimento.Itens[iped].Descricao.Length > 60)
                        Recebimento.Itens[iped].Descricao = Recebimento.Itens[iped].Descricao.Substring(0, 60);

                 
                    if (Recebimento.Itens[iped].CodigoDeBarras.ToString().Length > 20)
                        Recebimento.Itens[iped].CodigoDeBarras = Recebimento.Itens[iped].CodigoDeBarras.ToString().Substring(0, 20);

                    sql = "SELECT * FROM PRODUTOCLIENTE PC WHERE IDCLIENTE= " + idDest + " AND CODIGO = '" + Recebimento.Itens[iped].Codigo.Trim() + "'";
                    DataTable dProdCli = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    string IdprodCli = "";
                    if (dProdCli.Rows.Count == 0)
                    {
                        IdprodCli = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE", cnx);

                        if (Recebimento.Itens[iped].Descricao.Trim().ToUpper().Length > 60)
                            Recebimento.Itens[iped].Descricao = Recebimento.Itens[iped].Descricao.Substring(0, 59);


                        //for (int ifx = 0; ifx < CdsJaExistentesNoInsert.Count(); ifx++)
                        //{
                        //    if(CdsJaExistentesNoInsert[ifx].Trim().ToUpper() == Recebimento.Itens[iped].Codigo.ToUpper().Trim())
                        //    {
                        //        jaExiste = true;
                        //        break;
                        //    }
                        //}

                        //if (!jaExiste)
                        //{
                            sqlGeral += "; INSERT INTO PRODUTOCLIENTE (IDProdutoCliente, IDCliente, IDUnidadeDeMedida,Codigo, Descricao,MetodoDeMovimentacao, DesmembraNaNF, SolicitarDataDeValidade,UnidadeDoFornecedor,Ativo, CodigoNCM, FatorUsoPosicaoPallet) ";
                            sqlGeral += " VALUES(" + IdprodCli + ", " + idDest + ", 1,'" + Recebimento.Itens[iped].Codigo.ToUpper().Trim() + "', '" + Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "") + "','FEFO', 'NAO', 'SIM',1.00,'SIM', '', 1)";
                        //}
                        //CdsJaExistentesNoInsert.Add(Recebimento.Itens[iped].Codigo.ToUpper().Trim());
                    }
                    else
                    {

                        IdprodCli = dProdCli.Rows[0]["IdProdutoCliente"].ToString();

                        if (Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "") != dProdCli.Rows[0]["DESCRICAO"].ToString().ToUpper().Replace("'", ""))
                        {
                            string nomeprod = Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "");

                            if (nomeprod.Length > 60)
                                nomeprod = nomeprod.Substring(0, 59);

                            sqlGeral += "; UPDATE PRODUTOCLIENTE SET DESCRICAO='" + nomeprod + "' WHERE IDPRODUTOCLIENTE=" + IdprodCli + " ";
                        }
                    }


                    string idProd = "";
                    sql = "SELECT * FROM PRODUTO WHERE CodigoDeBarras='" + Recebimento.Itens[iped].CodigoDeBarras.Trim() + "'";
                    DataTable dProd = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    if (dProd.Rows.Count == 0)
                    {
                        idProd = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTO", cnx);

                        if (Recebimento.Itens[iped].Peso.ToString() == "")
                            Recebimento.Itens[iped].Peso = 0;

                        sqlGeral += "; INSERT INTO PRODUTO (IDProduto, CodigoDeBarras, PesoLiquido, Especie, DataDeCadastro, PesoBruto) VALUES (" + idProd + ", '" + Recebimento.Itens[iped].CodigoDeBarras.Trim() + "', " + Recebimento.Itens[iped].Peso.ToString().Trim().Replace(",", ".") + ", 'UNI', Getdate(), " + Recebimento.Itens[iped].Peso.ToString().Trim().Replace(",", ".") + ")";
                    }
                    else
                        idProd = dProd.Rows[0]["IDPRODUTO"].ToString();


                    string idProdEmb = "";
                    sql = "SELECT * FROM PRODUTOEMBALAGEM WHERE IDPRODUTOCLIENTE= " + IdprodCli + " AND IDPRODUTO=" + idProd;
                    DataTable dProdEmb = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    if (dProdEmb.Rows.Count == 0)
                    {
                        string nomeprod = Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "");

                        if (nomeprod.Length > 60)
                            nomeprod = nomeprod.Substring(0, 59);

                        idProdEmb = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOEMBALAGEM", cnx);
                        sqlGeral += "; INSERT INTO PRODUTOEMBALAGEM(IDProdutoEmbalagem, IDProdutoCliente, IDProduto, Conteudo, UnidadeDoCliente, ValorUnitario, DataDeCadastro, Ativo) ";
                        sqlGeral += " VALUES(" + idProdEmb + ", " + IdprodCli + ", " + idProd + ", '" + nomeprod + "', 1, " + Recebimento.Itens[iped].ValorUnitario.ToString().Replace(",", ".") + ", GETDATE(), 'SIM')";
                    }
                    else
                        idProdEmb = dProdEmb.Rows[0]["IDProdutoEmbalagem"].ToString();

                    #endregion

                    #region Documento Item

                    decimal vltot = Recebimento.Itens[iped].Quantidade * Recebimento.Itens[iped].ValorUnitario;

                    string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOITEM", cnx);
                    sqlGeral += "; INSERT INTO DOCUMENTOITEM (IDDOCUMENTOITEM, IDDOCUMENTO,IDPRODUTOEMBALAGEM, IDUSUARIO, QUANTIDADE,VALORUNITARIO,VALORTOTALDOITEM,IDPRODUTOCLIENTE, QUANTIDADEUNIDADEESTOQUE, SALDO, PesoLiquido,PesoBruto, UnidadeDoCLiente, QuantidadeNFe) ";
                    sqlGeral += " values(" + id + ", " + idDocumento + "," + idProdEmb + ",2, " + Recebimento.Itens[iped].Quantidade.ToString().Replace(",", ".") + "," + Recebimento.Itens[iped].ValorUnitario.ToString().Replace(",", ".") + ", " + vltot.ToString().Replace(",", ".") + "," + IdprodCli + ", " + Recebimento.Itens[iped].Quantidade.ToString().Replace(",", ".") + ", " + Recebimento.Itens[iped].Quantidade.ToString().Replace(",", ".") + "," + Recebimento.Itens[iped].Peso.ToString().Replace(",", ".") + " , " + Recebimento.Itens[iped].Peso.ToString().Replace(",", ".") + ", "+Recebimento.Itens[iped].FatorDeConversao+", "+ Recebimento.Itens[iped].QuantidadeNF + ")";

                    #endregion
                }
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOFILIAL", cnx);
                sqlGeral += "; INSERT INTO DOCUMENTOFILIAL (IDDOCUMENTOFILIAL, IDDOCUMENTO, IDFILIAL, SITUACAO, DATA, IDRegiaoItem) VALUES (" + ID + ", " + idDocumento + ", 37, 'ARMAZENAGEM', GETDATE(), 0) ";


                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlGeral, cnx);



                try
                {
                    XmlSerializer xsSubmit = new XmlSerializer(typeof(Recebimento));
                    var subReq = Recebimento;
                    var xml = "";

                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            xsSubmit.Serialize(writer, subReq);
                            xml = sww.ToString(); // Your XML
                        }
                    }

                    string ssql = "Insert into MaisBrasilLogRemessaRecebimento(Id,NrNfe,XML,DataHora, IdDocumento) values ('" + Guid.NewGuid() + "','" + Recebimento.NumeroNfe + "',@XML,getDate(),"+ idDocumento+ ")";

                    SqlConnection cnn = new SqlConnection(cnx);
                    SqlCommand cmm = new SqlCommand();
                    cmm.CommandText = ssql;
                    cmm.CommandType = CommandType.Text;
                    cmm.Connection = cnn;
                    SqlParameter par = new SqlParameter("@XML", xml);
                    cmm.Parameters.Add(par);

                    cnn.Open();
                    cmm.ExecuteNonQuery();
                    cnn.Close();

                }
                catch (Exception cc)
                {


                }

                return "Recebimento Cadastrado Com Sucesso";
            }
            catch (Exception ex)
            {
                return "Não foi possível cadastrar o Recebimeto. " + ex.Message;
            }
        }

        private void CadastrarProdutos(Recebimento Recebimento)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();


            for (int iped = 0; iped < Recebimento.Itens.Count; iped++)            
            {                
                string sqlGeral = "";

                #region Verificações

                if (Recebimento.Itens[iped].Codigo.Length > 20)
                    Recebimento.Itens[iped].Codigo = Recebimento.Itens[iped].Codigo.Substring(0, 20);

                if (Recebimento.Itens[iped].Descricao.Length > 60)
                    Recebimento.Itens[iped].Descricao = Recebimento.Itens[iped].Descricao.Substring(0, 60);


                if (Recebimento.Itens[iped].CodigoDeBarras.ToString().Length > 20)
                    Recebimento.Itens[iped].CodigoDeBarras = Recebimento.Itens[iped].CodigoDeBarras.ToString().Substring(0, 20);

               string  sql = "SELECT * FROM PRODUTOCLIENTE PC WHERE IDCLIENTE= 5300980 AND CODIGO = '" + Recebimento.Itens[iped].Codigo.Trim() + "'";
                DataTable dProdCli = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                string IdprodCli = "";
                if (dProdCli.Rows.Count == 0)
                {
                    IdprodCli = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE", cnx);

                    if (Recebimento.Itens[iped].Descricao.Trim().ToUpper().Length > 60)
                        Recebimento.Itens[iped].Descricao = Recebimento.Itens[iped].Descricao.Substring(0, 59);
                                        
                    sqlGeral += "; INSERT INTO PRODUTOCLIENTE (IDProdutoCliente, IDCliente, IDUnidadeDeMedida,Codigo, Descricao,MetodoDeMovimentacao, DesmembraNaNF, SolicitarDataDeValidade,UnidadeDoFornecedor,Ativo, CodigoNCM, FatorUsoPosicaoPallet) ";
                    sqlGeral += " VALUES(" + IdprodCli + ", " + 5300980 + ", 1,'" + Recebimento.Itens[iped].Codigo.ToUpper().Trim() + "', '" + Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "") + "','FEFO', 'NAO', 'SIM',1.00,'SIM', '', 1)";
                   
                }
                else
                {

                    IdprodCli = dProdCli.Rows[0]["IdProdutoCliente"].ToString();

                    if (Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "") != dProdCli.Rows[0]["DESCRICAO"].ToString().ToUpper().Replace("'", ""))
                    {
                        string nomeprod = Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "");

                        if (nomeprod.Length > 60)
                            nomeprod = nomeprod.Substring(0, 59);

                        sqlGeral += "; UPDATE PRODUTOCLIENTE SET DESCRICAO='" + nomeprod + "' WHERE IDPRODUTOCLIENTE=" + IdprodCli + " ";
                    }
                }


                string idProd = "";
                sql = "SELECT * FROM PRODUTO WHERE CodigoDeBarras='" + Recebimento.Itens[iped].CodigoDeBarras.Trim() + "'";
                DataTable dProd = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dProd.Rows.Count == 0)
                {
                    idProd = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTO", cnx);

                    if (Recebimento.Itens[iped].Peso.ToString() == "")
                        Recebimento.Itens[iped].Peso = 0;

                    sqlGeral += "; INSERT INTO PRODUTO (IDProduto, CodigoDeBarras, PesoLiquido, Especie, DataDeCadastro, PesoBruto) VALUES (" + idProd + ", '" + Recebimento.Itens[iped].CodigoDeBarras.Trim() + "', " + Recebimento.Itens[iped].Peso.ToString().Trim().Replace(",", ".") + ", 'UNI', Getdate(), " + Recebimento.Itens[iped].Peso.ToString().Trim().Replace(",", ".") + ")";
                }
                else
                    idProd = dProd.Rows[0]["IDPRODUTO"].ToString();


                string idProdEmb = "";
                sql = "SELECT * FROM PRODUTOEMBALAGEM WHERE IDPRODUTOCLIENTE= " + IdprodCli + " AND IDPRODUTO=" + idProd;
                DataTable dProdEmb = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dProdEmb.Rows.Count == 0)
                {
                    string nomeprod = Recebimento.Itens[iped].Descricao.Trim().ToUpper().Replace("'", "");

                    if (nomeprod.Length > 60)
                        nomeprod = nomeprod.Substring(0, 59);

                    idProdEmb = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOEMBALAGEM", cnx);
                    sqlGeral += "; INSERT INTO PRODUTOEMBALAGEM(IDProdutoEmbalagem, IDProdutoCliente, IDProduto, Conteudo, UnidadeDoCliente, ValorUnitario, DataDeCadastro, Ativo) ";
                    sqlGeral += " VALUES(" + idProdEmb + ", " + IdprodCli + ", " + idProd + ", '" + nomeprod + "', 1, " + Recebimento.Itens[iped].ValorUnitario.ToString().Replace(",", ".") + ", GETDATE(), 'SIM')";
                }
                else
                    idProdEmb = dProdEmb.Rows[0]["IDProdutoEmbalagem"].ToString();

                #endregion

                if(sqlGeral.Length>10)
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlGeral, cnx);


            }
        }

        private string VerificaCadastro(Cadastro c )
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            //#region Dados do Destinatario
            string sql = "SELECT C.IDCADASTRO, IDCIDADE, IDBAIRRO FROM CADASTRO C WHERE C.CNPJCPF='" + FormatarCnpj(c.CNPJ) + "' ";
            DataTable dtDest = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);


            //cadastra o destinario

            //verifica se pega o IdCdastro Zerado
            if (dtDest.Rows.Count > 0)
            {
                if (dtDest.Rows[0]["IDCADASTRO"].ToString() == "0")
                {
                    dtDest.Rows.Clear();
                }
            }

            string idCidade = ProcurarEndereco.RetornarCidadeIBGE(c.CodigoIBGE.ToString(), c.CEP, cnx).ToString();
            string idDest="";

            if (dtDest.Rows.Count == 0)
            {
                 idDest = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO", cnx);
                sql = "INSERT INTO CADASTRO (IDCADASTRO,CNPJCPF, INSCRICAORG, RAZAOSOCIALNOME,FANTASIAAPELIDO,ENDERECO,NUMERO,COMPLEMENTO,IDCIDADE,IDBAIRRO,CEP,Latitude, Longitude) ";
                sql += " VALUES(@IDCADASTRO,'@CNPJCPF', '@INSCRICAORG', '@RAZAOSOCIALNOME','@FANTASIAAPELIDO','@ENDERECO','@NUMERO', '@COMPLEMENTO',@IDCIDADE,@IDBAIRRO,'@CEP', '', '')";

                sql = sql.Replace("@CEP", c.CEP.Trim().Replace("-", ""));
                sql = sql.Replace("@ENDERECO", c.TipoDeEndereco + " " + c.Endereco);
                sql = sql.Replace("@IDCIDADE", idCidade);
                sql = sql.Replace("@NUMERO", c.Numero);
                sql = sql.Replace("@COMPLEMENTO", (c.Complemento == null ? "" : c.Complemento));
                sql = sql.Replace("@IDBAIRRO", "0");
                sql = sql.Replace("@IDCADASTRO", idDest);
                sql = sql.Replace("@CNPJCPF", FormatarCnpj(c.CNPJ));
                sql = sql.Replace("@INSCRICAORG", c.IE);
                sql = sql.Replace("@RAZAOSOCIALNOME", c.RazaoSocial.Replace("'", "").Trim());

                if (c.Fantasia.Length > 30)
                    c.Fantasia = c.Fantasia.Substring(0, 30).Replace("'", "");

                sql = sql.Replace("@FANTASIAAPELIDO", c.Fantasia.Replace("'", ""));

                string comp = "";
                comp = c.Complemento;

                if (comp != null && comp.Length > 50)
                    comp = comp.Substring(0, 50);

                sql = sql.Replace("@NUMERO", c.Numero);
                sql = sql.Replace("@COMPLEMENTO", comp.Replace("'", ""));


                if (c.Endereco.Trim() != "")
                    sql = sql.Replace("@ENDERECO", c.Endereco.Trim().Replace("'", ""));


                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, cnx);
                return idDest;
            }
            else
            {

                 idDest = dtDest.Rows[0]["IdCadastro"].ToString();


                //Iguala o endereço do Cadastro = endereço do Pedido//
                string ss = "Update Cadastro set IdCidade=" + idCidade + ", IdBairro=0, CEP='" + c.CEP.Trim().Replace("-", "") + "', Complemento='" + (c.Complemento != null ? c.Complemento.Trim().Replace("-", "") : "") + "', Endereco='" + c.TipoDeEndereco + " " + c.Endereco + "', Numero='" + c.Numero + "', Latitude ='', Longitude='' ";
                ss += " Where IdCadastro =" + idDest + " And IdCadastro not in(Select IdCadastro from Filial)";
                Sistran.Library.GetDataTables.ExecutarComandoSql(ss, cnx);
                //
                return idDest;

            }
            
        }
    





        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\Pedido\\pedidoMaisBrasil.log")]
        public string ReceberPedidos(string Login, string Senha, List<Pedido> pedido, string prod_hom, string TipoDePedido, string CNPJdoCliente)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string IdCliente = "";

            if (Login != "321500" && Senha != "321500")
                throw new Exception("Credenciais Inválidas");


            if (TipoDePedido == "")
                TipoDePedido = "EFETIVO";


            string sql = "select IDCliente, * from Cadastro c Inner join Cliente cl on c.IDCadastro = cl.IDCliente where CnpjCpf = '" + FormatarCnpj(CNPJdoCliente) + "'";
            var dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

            if (dt.Rows.Count == 0)
                throw new Exception("Cliente não encontrado.");
            else
                IdCliente = dt.Rows[0][0].ToString();


            string iddoc = "0";
            string cepRecebido = "";
            string comp = "";
            string Err = "";
            string ssqlFinal = "";
            string idDest = "";


            try
            {


                if (pedido == null)
                    throw new Exception("Objeto de Pedido Vazio. Favor Verificar");


                foreach (var p in pedido)
                {

                    if (p.Dest_ENTREGA_ENDERECO == null)
                        throw new Exception("Pedido Recusado. Favor Informar o Nome da Rua.");


                    p.Cliente_CNPJ = (p.Cliente_CNPJ == null ? "" : p.Cliente_CNPJ);
                    p.DataDeEmissao = (p.DataDeEmissao == null ? DateTime.Now : p.DataDeEmissao);
                    p.NumeroDocumento = (p.NumeroDocumento == null ? "" : p.NumeroDocumento).Replace("*", "");
                    p.Serie = (p.Serie == null ? "" : p.Serie);
                    p.Dest_IERG = (p.Dest_IERG == null ? "" : p.Dest_IERG);
                    p.Dest_RAZAOSOCIAL = (p.Dest_RAZAOSOCIAL == null ? "" : p.Dest_RAZAOSOCIAL).Replace("'", "");
                    p.Dest_FANTASIA = (p.Dest_FANTASIA == null ? "" : p.Dest_FANTASIA).Replace("'", "");
                    p.Dest_ENDERECO = (p.Dest_ENDERECO == null ? "" : p.Dest_ENDERECO).Replace("'", "");
                    p.Dest_NUMERO = (p.Dest_NUMERO == null ? "" : p.Dest_NUMERO).Replace("'", "");
                    p.Dest_COMPLEMENTO = (p.Dest_COMPLEMENTO == null ? "" : p.Dest_COMPLEMENTO).Replace("'", "");
                    p.Dest_BAIRRO = (p.Dest_BAIRRO == null ? "" : p.Dest_BAIRRO).Replace("'", "");
                    p.Dest_CEP = (p.Dest_CEP == null ? "" : p.Dest_CEP).Replace("'", "");

                    cepRecebido = p.Dest_CEP;

                    p.Dest_ENTREGA_CEP = (p.Dest_ENTREGA_CEP == null ? "" : p.Dest_ENTREGA_CEP).Replace("'", "");

                    if (cepRecebido == "")
                        cepRecebido = p.Dest_ENTREGA_CEP;

                    p.Dest_ENTREGA_ENDERECO = (p.Dest_ENTREGA_ENDERECO == null ? "" : p.Dest_ENTREGA_ENDERECO).Replace("'", "");
                    p.Dest_ENTREGA_NUMERO = (p.Dest_ENTREGA_NUMERO == null ? "" : p.Dest_ENTREGA_NUMERO).Replace("'", "");
                    p.Dest_ENTREGA_COMPLEMENTO = (p.Dest_ENTREGA_COMPLEMENTO == null ? "" : p.Dest_ENTREGA_COMPLEMENTO).Replace("'", "");
                    p.Dest_ENTREGA_BAIRRO = (p.Dest_ENTREGA_BAIRRO == null ? "" : p.Dest_ENTREGA_BAIRRO).Replace("'", "");
                    p.Dest_ENTREGA_COD_IBGE_CIDADE = (p.Dest_ENTREGA_COD_IBGE_CIDADE == null ? "" : p.Dest_ENTREGA_COD_IBGE_CIDADE);
                    p.PeriodoDeEntregaInicio = (p.PeriodoDeEntregaInicio == null ? "" : p.PeriodoDeEntregaInicio);
                    p.PeriodoDeEntregaFim = (p.PeriodoDeEntregaFim == null ? "" : p.PeriodoDeEntregaFim);
                    p.Longitude = (p.Longitude == null ? "" : p.Longitude);
                    p.Latitude = (p.Latitude == null ? "" : p.Latitude);
                    p.TipoDeDocumento = "PEDIDO";


                    if (p.Dest_COD_IBGE_CIDADE == null)
                        p.Dest_COD_IBGE_CIDADE = "0";

                    if (p.Dest_ENTREGA_COD_IBGE_CIDADE == null)
                        p.Dest_ENTREGA_COD_IBGE_CIDADE = "0";

                    if (p.TipoDeDocumento == null || p.TipoDeDocumento == "")
                        throw new Exception("Pedido Recusado falta Tipo de Documento. Informe Pedido ou NOTA FISCAL");

                    if (prod_hom == null || prod_hom == "")
                        throw new Exception("Pedido Recusado. Defina Homologação ou Produção");


                    if (p.Cliente_CNPJ.Length > 20)
                        p.Cliente_CNPJ = p.Cliente_CNPJ.Substring(0, 20);


                    if (p.Serie.Length > 3)
                        p.Serie.Substring(0, 3);

                    if (p.Dest_IERG.Length > 20)
                        p.Dest_IERG.Substring(0, 20);

                    if (p.Dest_RAZAOSOCIAL.Length > 60)
                        p.Dest_RAZAOSOCIAL = p.Dest_RAZAOSOCIAL.Substring(0, 60);


                    if (p.Dest_FANTASIA.Length > 30)
                        p.Dest_FANTASIA = p.Dest_FANTASIA.Substring(0, 30);

                    if (p.Dest_ENDERECO.Length > 60)
                        p.Dest_ENDERECO = p.Dest_ENDERECO.Substring(0, 60);

                    if (p.Dest_NUMERO.Length > 10)
                        p.Dest_NUMERO = p.Dest_NUMERO.Substring(0, 10);


                    if (p.Dest_COMPLEMENTO.Length > 50)
                        p.Dest_COMPLEMENTO = p.Dest_COMPLEMENTO.Substring(0, 50);

                    if (p.Dest_CEP.Length > 8)
                        p.Dest_CEP = p.Dest_CEP.Replace("-", "").Substring(0, 8);


                    if (p.Dest_ENTREGA_CEP.Length > 8)
                        p.Dest_ENTREGA_CEP = p.Dest_ENTREGA_CEP.Replace("-", "").Substring(0, 8);

                    if (p.Dest_ENTREGA_ENDERECO.Length > 60)
                        p.Dest_ENTREGA_ENDERECO = p.Dest_ENTREGA_ENDERECO.Substring(0, 60);

                    if (p.Dest_ENTREGA_NUMERO.Length > 10)
                        p.Dest_ENTREGA_NUMERO = p.Dest_ENTREGA_NUMERO.Substring(0, 10);

                    if (p.Dest_ENTREGA_COMPLEMENTO.Length > 50)
                        p.Dest_ENTREGA_COMPLEMENTO = p.Dest_ENTREGA_COMPLEMENTO.Substring(0, 50);

                    if (p.DataParaEntrega == null)
                        p.DataParaEntrega = "";

                    if (p.Longitude != "" && p.Latitude != "")
                    {
                        p.Longitude = p.Longitude.Replace(",", ".");
                        p.Latitude = p.Latitude.Replace(",", ".");
                    }


                    try
                    {
                        List<Pedido.Itens> item = pedido[0].itens;
                        for (int iped = 0; iped < item.Count; iped++)
                        {
                            item[iped].SKU = (item[iped].SKU == null ? "" : item[iped].SKU);
                            item[iped].Descricao = (item[iped].Descricao == null ? "" : item[iped].Descricao);
                            item[iped].CodigoNCM = (item[iped].CodigoNCM == null ? "" : item[iped].CodigoNCM);
                            item[iped].PesoLiquido = (item[iped].PesoLiquido == null ? 0 : item[iped].PesoLiquido);
                            item[iped].Quantidade = (item[iped].Quantidade == null ? 0 : item[iped].Quantidade);
                            item[iped].ValorUnitario = (item[iped].ValorUnitario == null ? 0 : item[iped].ValorUnitario);
                        }
                    }
                    catch (Exception)
                    { }
                }



                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                iddoc = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOPEDIDO", cnx);

                string dataPlajenadaAlterada = "";
                for (int i = 0; i < pedido.Count; i++)
                {
                    string tipo = "";

                    if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                        tipo = "'SAIDA'";
                    else
                        tipo = "'ENTRADA'";



                    sql = " SELECT count(*)  FROM DOCUMENTO WHERE IDCLIENTE=" + IdCliente + " AND NUMERO ='" + pedido[i].NumeroDocumento + "' AND TIPODEDOCUMENTO='PEDIDO' AND SERIE = '" + pedido[i].Serie + "' AND ENTRADASAIDA='SAIDA' AND ATIVO='SIM' AND DATADECANCELAMENTO IS NULL ";

                    if (Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString() != "0" && tipo.ToUpper() == "'SAIDA'")
                        continue;

                    pedido[i].Dest_CNPJCPF = FormatarCnpj(pedido[i].Dest_CNPJCPF);


                    if (tipo.ToUpper() == "'ENTRADA'")
                    {
                        sql = "select idcadastro from Cadastro where CNPJCPF='" + pedido[i].Dest_CNPJCPF + "'";
                        string idc = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString();


                        sql = " SELECT count(*)  FROM DOCUMENTO WHERE IDCLIENTE=" + IdCliente + " AND  IDREMETENTE=" + idc + " AND NUMERO ='" + pedido[i].NumeroDocumento + "' AND TIPODEDOCUMENTO='PEDIDO' AND SERIE = '" + pedido[i].Serie + "' AND ENTRADASAIDA='ENTRADA' AND ATIVO='SIM' AND DATADECANCELAMENTO IS NULL ";
                        if (Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString() != "0")
                            throw new Exception("Pedido de entrada já existente");

                    }


                    if (i > 0)
                        ssqlFinal += ";";

                    string trataData = "";
                    if (pedido[i].DataParaEntrega == null)
                        pedido[i].DataParaEntrega = "";

                    if (pedido[i].DataParaEntrega != "")
                    {
                        try
                        {
                            trataData = DateTime.Parse(pedido[i].DataParaEntrega).ToString("yyyy-MM-dd");
                        }
                        catch (Exception)
                        {
                            if (tipo == "SAIDA")
                            {
                                trataData = DateTime.Now.ToString("yyyy-MM-dd");
                                dataPlajenadaAlterada = "Pedido: " + pedido[i].NumeroDocumento + " foi alterada a Data Planejada para a Data Atual";
                            }
                            else
                                trataData = "";
                        }
                    }
                    else
                    {
                        if (tipo == "SAIDA")
                        {
                            trataData = DateTime.Now.ToString("yyyy-MM-dd");
                            dataPlajenadaAlterada = "Pedido: " + pedido[i].NumeroDocumento + " foi alterada a Data Planejada para a Data Atual";
                        }
                    }


                    string pei = pedido[i].PeriodoDeEntregaInicio;
                    if (pei == "")
                        pei = "";
                    else
                    {
                        try
                        {
                            if (pei.Split(' ').Length > 1) // vem hora no formato certro
                            {
                                pei = pei;
                            }
                            else
                            {
                                string[] h = pei.Split('-');
                                string[] mm = h[3].Split(':');
                                DateTime dd = new DateTime(int.Parse(h[0]), int.Parse(h[1]), int.Parse(h[2]), int.Parse(mm[0]), int.Parse(mm[1]), 0);
                                pei = dd.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }


                    string pef = pedido[i].PeriodoDeEntregaFim;
                    if (pef == "")
                        pef = "";
                    else
                    {
                        try
                        {
                            if (pef.Split(' ').Length > 1) // vem hora no formato certro
                            {
                                pef = pef;
                            }
                            else
                            {
                                string[] h = pef.Split('-');
                                string[] mm = h[3].Split(':');
                                DateTime dd = new DateTime(int.Parse(h[0]), int.Parse(h[1]), int.Parse(h[2]), int.Parse(mm[0]), int.Parse(mm[1]), 0);
                                pef = dd.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    string primeiraEntrega = "0";

                    ssqlFinal += " INSERT INTO DOCUMENTOPEDIDO (IDDOCUMENTO, IDCLIENTE, IDREMETENTE, IDFILIAL, IDFILIALATUAL,  NUMERO, SERIE, TIPODEDOCUMENTO, IDDESTINATARIO,  ORIGEM, DATADEEMISSAO, DATADEENTRADA, ENDERECO, ENDERECONUMERO, ENDERECOCOMPLEMENTO, IDENDERECOBAIRRO, IDENDERECOCIDADE, ENDERECOCEP, ANOMES, DATADECANCELAMENTO, ENTRADASAIDA, ATIVO, TIPODESERVICO, DATAPLANEJADA, PERIODODEENTREGAINICIO, PERIODODEENTREGAFIM, TipoDePedido, Latitude, Longitude, EnderecoCoord, CepCoord, PrimeiroPedido, CEPRecebido) VALUES";
                    ssqlFinal += " (@IDDOCUMENTO, @IDCLIENTE, @IDREMETENTE, @IDFILIAL@, @IDFILIALATUAL@,  '@NUMERO', '@SERIE', '@TIPODEDOCUMENTO', @IDDESTINATARIO,  'WEBSERVICE', '@DATADEEMISSAO', GETDATE(),  '@ENDERECO_ENTREGA@', '@ENDERECONUMERO_ENTREGA@', '@ENDERECOCOMPLEMENTO_ENTREGA@', @IDENDERECOBAIRRO_ENTREGA@, @IDENDERECOCIDADE_ENTREGA@, '@ENDERECOCEP_ENTREGA@', '@ANOMES', @DATADECANCELAMENTO@, @ENTRADASAIDA@, '" + (prod_hom.ToUpper() == "PRODUCAO" ? "SIM" : "NAO") + "', 'NORMAL', " + (trataData == "" ? "NULL" : "'" + trataData + "'") + ", " + (pei == "" ? "NULL" : "'" + pei + "'") + ", " + (pei == "" ? "NULL" : "'" + pef + "'") + ", '" + TipoDePedido + "', '" + pedido[i].Latitude + "', '" + pedido[i].Longitude + "', '@@EnderecoCoord@@','@CepCoord@', '" + primeiraEntrega + "', '" + cepRecebido + "'  ) ";
                    ssqlFinal = ssqlFinal.Replace("@IDDOCUMENTO", iddoc);

                    ssqlFinal = ssqlFinal.Replace("@IDFILIAL@", dt.Rows[0]["IDFILIALPADRAOINTERNET"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@IDFILIALATUAL@", dt.Rows[0]["IDFILIALPADRAOINTERNET"].ToString());
                    ssqlFinal = ssqlFinal.Replace("@NUMERO", pedido[i].NumeroDocumento);

                    if (TipoDePedido == "EFETIVO" || tipo == "ENTRADA")
                        ssqlFinal = ssqlFinal.Replace("@SERIE", pedido[i].Serie);
                    else
                        ssqlFinal = ssqlFinal.Replace("@SERIE", "ESP");


                    ssqlFinal = ssqlFinal.Replace("@TIPODEDOCUMENTO", pedido[i].TipoDeDocumento);
                    ssqlFinal = ssqlFinal.Replace("@DATADEEMISSAO", DateTime.Parse(pedido[i].DataDeEmissao.ToString()).ToString("yyyy-MM-dd"));
                    ssqlFinal = ssqlFinal.Replace("@DATADECANCELAMENTO@", (prod_hom.ToUpper() == "PRODUCAO" ? "NULL" : "GetDate()"));
                    ssqlFinal = ssqlFinal.Replace("@ANOMES", DateTime.Now.Year.ToString() + "/" + DateTime.Now.ToString("MM"));
                    ssqlFinal = ssqlFinal.Replace("@IDCLIENTE", dt.Rows[0]["IDCLIENTE"].ToString());



                    comp = "";
                    comp = pedido[i].Dest_ENTREGA_COMPLEMENTO;
                    if (comp != null && comp.Length > 60)
                        comp = comp.Substring(0, 58);


                    if (pedido[i].Dest_ENTREGA_NUMERO.Length > 9)
                        pedido[i].Dest_ENTREGA_NUMERO = pedido[i].Dest_ENTREGA_NUMERO.Substring(0, 9);

                    ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", pedido[i].Dest_ENTREGA_ENDERECO.Replace("'", ""));
                    ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO_ENTREGA@", pedido[i].Dest_ENTREGA_NUMERO.Replace("'", ""));
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO_ENTREGA@", comp.Replace("'", ""));

                    string idCidade = ProcurarEndereco.RetornarCidade(pedido[i].Dest_CIDADE_NOME, pedido[i].Dest_ENTREGA_CEP, cnx).ToString();
                    string idBairro = ProcurarEndereco.RetornarBairro(pedido[i].Dest_ENTREGA_BAIRRO, idCidade, cnx).ToString();


                    ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", idCidade);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@", pedido[i].Dest_ENTREGA_CEP);
                    ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", idBairro);


                    if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                    {
                        ssqlFinal = ssqlFinal.Replace("@ENTRADASAIDA@", "'SAIDA'");
                        ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", dt.Rows[0]["IDCLIENTE"].ToString());
                    }
                    else
                    {
                        ssqlFinal = ssqlFinal.Replace("@ENTRADASAIDA@", "'ENTRADA'");
                    }


                    #region Dados do Destinatario
                    sql = "SELECT C.IDCADASTRO, IDCIDADE, IDBAIRRO FROM CADASTRO C WHERE C.CNPJCPF='" + FormatarCnpj(pedido[i].Dest_CNPJCPF) + "' ";
                    DataTable dtDest = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);


                    //cadastra o destinario

                    //verifica se pega o IdCdastro Zerado
                    if (dtDest.Rows.Count > 0)
                    {
                        if (dtDest.Rows[0]["IDCADASTRO"].ToString() == "0")
                        {
                            dtDest.Rows.Clear();
                        }
                    }

                    idCidade = ProcurarEndereco.RetornarCidade(pedido[i].Dest_CIDADE_NOME, pedido[i].Dest_CEP, cnx).ToString();
                    idBairro = ProcurarEndereco.RetornarBairro(pedido[i].Dest_BAIRRO, idCidade, cnx).ToString();


                    if (dtDest.Rows.Count == 0)
                    {
                        idDest = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO", cnx);
                        sql = "INSERT INTO CADASTRO (IDCADASTRO,CNPJCPF, INSCRICAORG, RAZAOSOCIALNOME,FANTASIAAPELIDO,ENDERECO,NUMERO,COMPLEMENTO,IDCIDADE,IDBAIRRO,CEP,Latitude, Longitude) ";
                        sql += " VALUES(@IDCADASTRO,'@CNPJCPF', '@INSCRICAORG', '@RAZAOSOCIALNOME','@FANTASIAAPELIDO','@ENDERECO','@NUMERO', '@COMPLEMENTO',@IDCIDADE,@IDBAIRRO,'@CEP', '" + pedido[i].Latitude + "', '" + pedido[i].Longitude + "')";

                        sql = sql.Replace("@CEP", pedido[i].Dest_CEP.Trim().Replace("-", ""));
                        sql = sql.Replace("@ENDERECO", pedido[i].Dest_ENDERECO.Trim().Replace("'", ""));


                        sql = sql.Replace("@IDCIDADE", idCidade);
                        sql = sql.Replace("@NUMERO", pedido[i].Dest_NUMERO);
                        sql = sql.Replace("@COMPLEMENTO", pedido[i].Dest_COMPLEMENTO.Replace("'", ""));
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", idBairro);
                        sql = sql.Replace("@IDBAIRRO", idBairro);


                        sql = sql.Replace("@IDCADASTRO", idDest);
                        sql = sql.Replace("@CNPJCPF", FormatarCnpj(pedido[i].Dest_CNPJCPF));
                        sql = sql.Replace("@INSCRICAORG", pedido[i].Dest_IERG);
                        sql = sql.Replace("@RAZAOSOCIALNOME", pedido[i].Dest_RAZAOSOCIAL.Replace("'", ""));

                        if (pedido[i].Dest_FANTASIA.Length > 30)
                            pedido[i].Dest_FANTASIA = pedido[i].Dest_FANTASIA.Substring(0, 30).Replace("'", "");

                        sql = sql.Replace("@FANTASIAAPELIDO", pedido[i].Dest_FANTASIA.Replace("'", ""));

                        comp = "";
                        comp = pedido[i].Dest_ENTREGA_COMPLEMENTO; ;
                        if (comp != null && comp.Length > 50)
                            comp = comp.Substring(0, 50);

                        sql = sql.Replace("@NUMERO", pedido[i].Dest_ENTREGA_NUMERO);
                        sql = sql.Replace("@COMPLEMENTO", comp.Replace("'", ""));


                        sql = sql.Replace("@CEP", pedido[i].Dest_CEP.Trim().Replace("-", ""));

                        if (pedido[i].Dest_ENTREGA_ENDERECO.Trim() != "")
                            sql = sql.Replace("@ENDERECO", pedido[i].Dest_ENDERECO.Trim().Replace("'", ""));
                        else
                            sql = sql.Replace("@ENDERECO", pedido[i].Dest_ENDERECO.Replace("'", ""));

                        sql = sql.Replace("@IDCIDADE", idCidade.ToString());
                        sql = sql.Replace("@IDBAIRRO", idBairro.ToString());

                        ssqlFinal = ssqlFinal.Replace("@ENDERECO_ENTREGA@", pedido[i].Dest_ENDERECO);
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO_ENTREGA@", idBairro.ToString());
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE_ENTREGA@", idCidade.ToString());
                        ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP_ENTREGA@", pedido[i].Dest_CEP.Trim().Replace("-", ""));

                        ssqlFinal = ssqlFinal.Replace("@@EnderecoCoord@@", pedido[i].Dest_ENDERECO);
                        ssqlFinal = ssqlFinal.Replace("@CepCoord@", pedido[i].Dest_CEP.Trim().Replace("-", ""));
                                               

                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, cnx);


                        if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", idDest);
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", dt.Rows[0]["IDCLIENTE"].ToString());
                        }
                        else
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", dt.Rows[0]["IDCLIENTE"].ToString());
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", idDest);
                        }

                        //////////////////////////////////////////////////////////////////////////
                        string idCadEndereco = "";
                        if (pedido[i].Dest_Email == null)
                            pedido[i].Dest_Email = "";

                        if (pedido[i].Dest_Telefone == null)
                            pedido[i].Dest_Telefone = "";


                        if (pedido[i].Dest_Email.ToString() != "" && pedido[i].Dest_Email.ToString().Contains("@"))
                        {
                            idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                            ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                            ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 1, '" + pedido[i].Dest_Email.ToLower() + "'); ";
                        }

                        if (pedido[i].Dest_Telefone.ToString() != "")
                        {
                            idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                            ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                            ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 2, '" + pedido[i].Dest_Telefone + "'); ";
                        }

                        //////////////////////////////////////////////////////////////////////////

                    }
                    else
                    {

                        idDest = dtDest.Rows[0]["IdCadastro"].ToString();


                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOBAIRRO@", (dtDest.Rows[0]["IDBAIRRO"].ToString() == "" ? "NULL" : dtDest.Rows[0]["IDBAIRRO"].ToString()));
                        ssqlFinal = ssqlFinal.Replace("@IDENDERECOCIDADE@", dtDest.Rows[0]["IDCIDADE"].ToString());

                        if (pedido[i].CompraVenda.ToUpper() == "VENDA")
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", idDest);
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", dt.Rows[0]["IDCLIENTE"].ToString());
                        }
                        else
                        {
                            ssqlFinal = ssqlFinal.Replace("@IDDESTINATARIO", dt.Rows[0]["IDCLIENTE"].ToString());
                            ssqlFinal = ssqlFinal.Replace("@IDREMETENTE", idDest);
                        }

                        //Iguala o endereço do Cadastro = endereço do Pedido//
                        string ss = "Update Cadastro set IdCidade=" + idCidade + ", IdBairro=" + idBairro + ", CEP='" + pedido[i].Dest_CEP.Trim().Replace("-", "") + "', Complemento='" + pedido[i].Dest_COMPLEMENTO.Trim().Replace("-", "") + "', Endereco='" + pedido[i].Dest_ENDERECO + "', Numero='" + pedido[i].Dest_NUMERO + "', Latitude ='" + pedido[i].Latitude + "', Longitude='" + pedido[i].Longitude + "' ";
                        ss += " Where IdCadastro =" + idDest + " And IdCadastro not in(Select IdCadastro from Filial)";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(ss, cnx);
                        //

                        /////////////////////////////////////////////////////////////////////////////////////////
                        if (pedido[i].Dest_Email != "" || pedido[i].Dest_Telefone != "")
                        {
                            string xsql = "SELECT * FROM CadastroContatoEndereco where idCadastro=" + idDest + " and IDCadastroTipoDeContato in (1,2)";
                            DataTable dx = Sistran.Library.GetDataTables.RetornarDataTableWS(xsql, cnx);


                            if (dx.Rows.Count == 0)
                            {
                                if (pedido[i].Dest_Email == null)
                                    pedido[i].Dest_Email = "";

                                if (pedido[i].Dest_Telefone == null)
                                    pedido[i].Dest_Telefone = "";


                                string idCadEndereco = "";
                                if (pedido[i].Dest_Email.ToString() != "" && pedido[i].Dest_Email.ToString().Contains("@"))
                                {
                                    idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                    ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                    ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 1, '" + pedido[i].Dest_Email.ToLower().Replace("'", "") + "'); ";
                                }

                                if (pedido[i].Dest_Telefone.ToString() != "")
                                {
                                    idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                    ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                    ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 2, '" + pedido[i].Dest_Telefone + "'); ";
                                }

                            }
                            else
                            {
                                if (pedido[i].Dest_Email == null)
                                    pedido[i].Dest_Email = "";

                                if (pedido[i].Dest_Telefone == null)
                                    pedido[i].Dest_Telefone = "";


                                if (pedido[i].Dest_Email.ToString() != "" && pedido[i].Dest_Email.Contains("@"))
                                {
                                    DataRow[] rx = dx.Select("IDCadastroTipoDeContato=1", "");

                                    if (rx != null && rx.Length > 0)
                                    {
                                        ssqlFinal += "UPDATE CadastroContatoEndereco SET ENDERECO='" + pedido[i].Dest_Email.ToString() + "' WHERE IDCadastroContatoEndereco=" + rx[0]["IDCadastroContatoEndereco"].ToString() + "; ";
                                    }
                                    else
                                    {
                                        string idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                        ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                        ssqlFinal += "(" + idCadEndereco + ", " + idDest + ", 1, '" + pedido[i].Dest_Email.ToString().ToLower() + "'); ";
                                    }
                                }


                                if (pedido[i].Dest_Telefone.ToString() != "")
                                {
                                    DataRow[] rx = dx.Select("IDCadastroTipoDeContato=2", "");

                                    if (rx != null && rx.Length > 0)
                                    {
                                        ssqlFinal += "UPDATE CadastroContatoEndereco SET ENDERECO='" + pedido[i].Dest_Telefone.ToString() + "' WHERE IDCadastroContatoEndereco=" + rx[0]["IDCadastroContatoEndereco"].ToString() + "; ";
                                    }
                                    else
                                    {
                                        string idCadEndereco = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO", cnx);
                                        ssqlFinal += "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro, IDCadastroTipoDeContato, Endereco) values";
                                        ssqlFinal += "(" + idCadEndereco + ", " + idDest + ",2, '" + pedido[i].Dest_Telefone.ToLower() + "'); ";
                                    }
                                }
                            }
                        }
                        /////////////////////////////////////////////////////////////////////////////////////////


                    }

                    #endregion

                    ssqlFinal = ssqlFinal.Replace("@ENDERECO@", pedido[i].Dest_ENTREGA_ENDERECO);

                    if (pedido[i].Dest_NUMERO.Length > 9)
                        pedido[i].Dest_NUMERO = pedido[i].Dest_ENTREGA_NUMERO.Substring(0, 9);

                    ssqlFinal = ssqlFinal.Replace("@ENDERECONUMERO@", pedido[i].Dest_ENTREGA_NUMERO);

                    comp = "";
                    comp = pedido[i].Dest_COMPLEMENTO;

                    if (comp != null && comp.Length > 50)
                        comp = comp.Substring(0, 50);
                

                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCOMPLEMENTO@", comp);
                    ssqlFinal = ssqlFinal.Replace("@ENDERECOCEP@", pedido[i].Dest_CEP);

                    List<Pedido.Itens> item = pedido[i].itens;

                    if (item == null || item.Count == 0)
                        throw new Exception("Não foi possível receber o pedido. Motivo: Falta de itens.");

                    for (int iped = 0; iped < item.Count; iped++)
                    {
                        #region Verificações

                        if (item[iped].SKU.Length > 20)
                            item[iped].SKU = item[iped].SKU.Substring(0, 20);

                        if (item[iped].Descricao.Length > 60)
                            item[iped].Descricao = item[iped].Descricao.Substring(0, 60);

                        if (item[iped].CodigoNCM.Length > 10)
                            item[iped].CodigoNCM = item[iped].CodigoNCM.Substring(0, 10);

                        if (item[iped].EAN.Length > 20)
                            item[iped].EAN = item[iped].EAN.Substring(0, 20);

                        sql = "SELECT * FROM PRODUTOCLIENTE PC WHERE IDCLIENTE= " + dt.Rows[0]["IDCLIENTE"].ToString() + " AND CODIGO = '" + item[iped].SKU.Trim() + "'";
                        DataTable dProdCli = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        string IdprodCli = "";
                        if (dProdCli.Rows.Count == 0)
                        {
                            IdprodCli = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE", cnx);

                            if (item[iped].Descricao.Trim().ToUpper().Length > 60)
                                item[iped].Descricao = item[iped].Descricao.Substring(0, 59);

                            ssqlFinal += "; INSERT INTO PRODUTOCLIENTE (IDProdutoCliente, IDCliente, IDUnidadeDeMedida,Codigo, Descricao,MetodoDeMovimentacao, DesmembraNaNF, SolicitarDataDeValidade,UnidadeDoFornecedor,Ativo, CodigoNCM, FatorUsoPosicaoPallet) ";
                            ssqlFinal += " VALUES(" + IdprodCli + ", " + dt.Rows[0]["IDCLIENTE"].ToString() + ", 1,'" + item[iped].SKU.ToUpper().Trim() + "', '" + item[iped].Descricao.Trim().ToUpper().Replace("'", "") + "','FEFO', 'NAO', 'SIM',1.00,'SIM', '" + item[iped].CodigoNCM + "', 1)";
                        }
                        else
                        {

                            IdprodCli = dProdCli.Rows[0]["IdProdutoCliente"].ToString();

                            if (item[iped].Descricao.Trim().ToUpper().Replace("'", "") != dProdCli.Rows[0]["DESCRICAO"].ToString().ToUpper().Replace("'", ""))
                            {
                                string nomeprod = item[iped].Descricao.Trim().ToUpper().Replace("'", "");

                                if (nomeprod.Length > 60)
                                    nomeprod = nomeprod.Substring(0, 59);

                                ssqlFinal += "; UPDATE PRODUTOCLIENTE SET DESCRICAO='" + nomeprod + "' WHERE IDPRODUTOCLIENTE=" + IdprodCli + " ";
                            }
                        }


                        string idProd = "";
                        sql = "SELECT * FROM PRODUTO WHERE CodigoDeBarras='" + item[iped].EAN.Trim() + "'";
                        DataTable dProd = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        if (dProd.Rows.Count == 0)
                        {
                            idProd = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTO", cnx);

                            if (item[iped].PesoLiquido.ToString() == "")
                                item[iped].PesoLiquido = 0;

                            ssqlFinal += "; INSERT INTO PRODUTO (IDProduto, CodigoDeBarras, PesoLiquido, Especie, DataDeCadastro, PesoBruto) VALUES (" + idProd + ", '" + item[iped].EAN.Trim() + "', " + item[iped].PesoLiquido.ToString().Trim().Replace(",", ".") + ", 'UNI', Getdate(), " + item[iped].PesoLiquido.ToString().Trim().Replace(",", ".") + ")";
                        }
                        else
                            idProd = dProd.Rows[0]["IDPRODUTO"].ToString();


                        string idProdEmb = "";
                        sql = "SELECT * FROM PRODUTOEMBALAGEM WHERE IDPRODUTOCLIENTE= " + IdprodCli + " AND IDPRODUTO=" + idProd;
                        DataTable dProdEmb = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                        if (dProdEmb.Rows.Count == 0)
                        {
                            string nomeprod = item[iped].Descricao.Trim().ToUpper().Replace("'", "");

                            if (nomeprod.Length > 60)
                                nomeprod = nomeprod.Substring(0, 59);

                            idProdEmb = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOEMBALAGEM", cnx);
                            ssqlFinal += "; INSERT INTO PRODUTOEMBALAGEM(IDProdutoEmbalagem, IDProdutoCliente, IDProduto, Conteudo, UnidadeDoCliente, ValorUnitario, DataDeCadastro, Ativo) ";
                            ssqlFinal += " VALUES(" + idProdEmb + ", " + IdprodCli + ", " + idProd + ", '" + nomeprod + "', 1, " + item[iped].ValorUnitario.ToString().Replace(",", ".") + ", GETDATE(), 'SIM')";
                        }
                        else
                            idProdEmb = dProdEmb.Rows[0]["IDProdutoEmbalagem"].ToString();

                        #endregion

                        #region Documento Item

                        decimal vltot = item[iped].Quantidade * item[iped].ValorUnitario;

                        string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOPEDIDOITEM", cnx);
                        ssqlFinal += "; INSERT INTO DOCUMENTOPEDIDOITEM (IDDOCUMENTOITEM, IDDOCUMENTO,IDPRODUTOEMBALAGEM, IDUSUARIO, QUANTIDADE,VALORUNITARIO,VALORTOTALDOITEM,IDPRODUTOCLIENTE, QUANTIDADEUNIDADEESTOQUE, SALDO, PesoLiquido,PesoBruto) ";
                        ssqlFinal += " values(" + id + ", " + iddoc + "," + idProdEmb + ",2, " + item[iped].Quantidade.ToString().Replace(",", ".") + "," + item[iped].ValorUnitario.ToString().Replace(",", ".") + ", " + vltot.ToString().Replace(",", ".") + "," + IdprodCli + ", " + item[iped].Quantidade.ToString().Replace(",", ".") + ", " + item[iped].Quantidade.ToString().Replace(",", ".") + ","+ item[iped].PesoLiquido.ToString().Replace(",", ".") + " , " + item[iped].PesoLiquido.ToString().Replace(",", ".") + ")";

                        #endregion
                    }


                    //if (pedido[i].OcorrenciasPedidosAnteriores != null)
                    //{
                    //    for (int ih = 0; ih < pedido[i].OcorrenciasPedidosAnteriores.Count; ih++)
                    //    {
                    //        ssqlFinal += "; Insert into HistoricoPedRH (IdDocumento, Ocorrencia) values (" + iddoc + ", '" + pedido[i].OcorrenciasPedidosAnteriores[ih].ToString() + "') ";
                    //    }
                    //}
                }

                if (ssqlFinal == "")
                {

                    if (pedido[0].NumeroDocumento == null)
                    {
                        pedido[0].NumeroDocumento = "";
                    }

                    if (pedido[0].Serie == null)
                    {
                        pedido[0].Serie = "";
                    }

                    throw new Exception("Verifique o Documento. Já existe na base de dados. " + pedido[0].NumeroDocumento + " - " + pedido[0].Serie);
                }

                ssqlFinal = ssqlFinal.Replace("@@EnderecoCoord@@", "");
                ssqlFinal = ssqlFinal.Replace("@CepCoord@", "");

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(ssqlFinal.ToUpper(), cnx);
                  


                return "Documento(s) Gravados com Sucesso ";
            }
            catch (Exception ex)
            {
                //Err = ex.Message;

               // if (ex.Message.Contains("Object reference"))
                    return "Não Foi Possivel Receber o Pedido";

                try
                {
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - Pedido Girotrade HR ", "Ex:" + ex.Message + ssqlFinal, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
                }
                catch (Exception ex2)
                { }


                //return ex.Message;
            }
            //finally
            //{
            //    idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "ReceberPedidos", null, null, "Finalizou -" + Err, cnx);
            //    //AlterarCadastroDestinatario(pedido[0].Dest_CNPJCPF, pedido[0].Dest_BAIRRO, pedido[0].Latitude, pedido[0].Longitude, iddoc, cnx, cid, pedido[0].Dest_ENTREGA_ENDERECO);

            //}
        }


        //[WebMethod]
        //public string RelacionarPedidoChave(string Login, string Senha, string Cnpj, int NrPedido, string chave)
        //{
        //    try
        //    {

        //        if (chave == "" || chave.Length != 44 || NrPedido == 0)
        //            return "";

        //        string sql = "Insert into YandehPedidoChave(Chave, Numero) values ('"+chave+"', "+NrPedido+")";
        //        string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
        //        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //        return "OK";

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}


        [WebMethod]
        public RetornoPedidoStatus RetornarStatusPedido(string Login, string Senha, string Cnpj, int NrPedido)
        {
            if (Login != "321500" && Senha != "321500")
                throw new Exception("Credenciais Inválidas");

            RetornoPedidoStatus ret = new RetornoPedidoStatus();
            List<RetornoPedidoStatusItem> it = new List<RetornoPedidoStatusItem>();
            try
            {
                string sql = "Select top 1 * from MaisStatusPedido Where NrPedido=" + NrPedido + " and Consumido is null Order by Id" ;                

                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dt.Rows.Count == 0)
                    return null;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ret.NrPedido = int.Parse(dt.Rows[i]["NRPedido"].ToString());
                    ret.DataHora = DateTime.Parse(dt.Rows[i]["DataHora"].ToString());
                    ret.Status = dt.Rows[i]["Status"].ToString();
                    ret.Id = int.Parse(dt.Rows[i]["Id"].ToString());

                    sql = "Select * from MaisStatusPedidoItem where IdStatusPedido=" + dt.Rows[i]["Id"].ToString();
                    DataTable dti = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);


                    for (int ii = 0; ii < dti.Rows.Count; ii++)
                    {

                        it.Add(new RetornoPedidoStatusItem()
                        {
                            CodigoDoProduto = dti.Rows[ii]["CodigoDoProduto"].ToString(),
                            Quantidade = int.Parse(dti.Rows[ii]["Quantidade"].ToString()),
                            Id = int.Parse(dti.Rows[ii]["IdItem"].ToString())
                            //IdStatusPedido = int.Parse(dt.Rows[ii]["IdStatusPedido"].ToString())
                        });

                        ret.Items = it;
                    }

                    Sistran.Library.GetDataTables.RetornarDataTableWS("UPDATE MaisStatusPedido set Consumido=getDate() where Id=" + dt.Rows[i]["Id"].ToString() + "; select 1", cnx);

                }

                return ret;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        [WebMethod]
        public DataTable Estoque(string login, string senha)
        {
            if (login != "321500" || senha != "321500")
                return null;

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {
                string sql = "exec PRC_Saldo_MaisBrasil";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private int RetornarCidade(string NomeCidade, string CEP, string cnx)
        {
            NomeCidade = removerAcentos(NomeCidade);

            string sql = "select IdCidade from Cidade where Nome = '" + NomeCidade + "' and IdEstado=26";
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

            sql = "Select * from CidadeFaixaDeCep cfc  ";
            sql += " inner join Cidade c on c.IdCidade = cfc.IdCidade ";
            sql += " where "+CEP+" between CepInicial and CepFinal ";

            dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

            return 0;
        }

        private int RetornarBairro(string Nome, string IdClidade, string cnx)
        {
            Nome = removerAcentos(Nome);
            int Id = 0;

            string sql = "select IdBairro from Bairro where Nome = '" + Nome + "' and IdCidade="+ IdClidade;
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdBairro"]);
            else
            {
                Id = int.Parse( Sistran.Library.GetDataTables.RetornarIdTabela("Bairro", cnx));
                sql = "Insert into Bairro (IdBairro, Nome, IdCidade) values (" + Id + ", '" + Nome + "', " + IdClidade + ")";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                return Id;
            }
        }


        public static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        //[WebMethod]
        //public DataTable PosicaoEstoque(string Login, string Senha)
        //{
        //    string cnx =  ConfigurationSettings.AppSettings["cnxHomeRefill"].ToString();;

        //    int idLog = 0;
        //    string Err = "";
        //    try
        //    {


        //        idLog = Sistran.Library.GetDataTables.LogMetodo(null, "PosicaoEstoque", null, null, "EXEC PRC_ESTOQUE_HOMEREFIILL", cnx);

        //        string sql = "";
        //        sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM'";
        //        DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //        //if (d.Rows.Count == 0)
        //        //    throw new Exception("USUARIO OU SENHA INVALIDOS");
        //        //PREVISAO DE PEDIDO

        //        sql = "EXEC PRC_ESTOQUE_HOMEREFIILL";
        //        try
        //        {
        //            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - PosicaoEstoque ", "SQL:" + sql, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
        //        }
        //        catch (Exception)
        //        { }



        //        return Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //    }
        //    catch (Exception EX)
        //    {

        //        Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - PosicaoEstoque ", "Ex:" + EX.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
        //        Err = EX.Message;
        //        throw EX;
        //    }
        //    finally
        //    {
        //        idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "PosicaoEstoque", null, null, "EXEC PRC_ESTOQUE_HOMEREFIILL - Finalizou -" + Err, cnx);
        //    }
        //}

        //[WebMethod]
        //public List<Produto> ProdutosAtivos(string Login, string Senha)
        //{
        //    string cnx =  ConfigurationSettings.AppSettings["cnxHomeRefill"].ToString();;

        //    int idLog = 0;
        //    string Err = "";
        //    try
        //    {
        //        idLog = Sistran.Library.GetDataTables.LogMetodo(null, "ProdutosAtivos", null, null, "EXEC PRC_RETORNAR_PRODUTOS_HOMEREFILL", cnx);

        //        string sql = "";
        //        sql = "SELECT * FROM USUARIO WHERE LOGIN='" + Login + "' AND SENHA='" + Senha + "' AND ATIVO='SIM'";

        //        DataTable d = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //        //if (d.Rows.Count == 0)
        //        //    throw new Exception("USUARIO OU SENHA INVALIDOS");


        //        sql = "EXEC PRC_RETORNAR_PRODUTOS_HOMEREFILL";

        //        DataTable dprod = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

        //        DataView view = new DataView(dprod);
        //        DataTable dd = view.ToTable(true, "IDPRODUTOCLIENTE", "CODIGO", "DESCRICAO", "CODIGONCM", "ATIVO");


        //        List<Produto> prd = new List<Produto>();
        //        for (int i = 0; i < dd.Rows.Count; i++)
        //        {
        //            Produto p = new Produto();
        //            p.IDPRODUTOCLIENTE = dd.Rows[i]["IDPRODUTOCLIENTE"].ToString();
        //            p.CODIGO = dd.Rows[i]["CODIGO"].ToString();
        //            p.CODIGONCM = dd.Rows[i]["CODIGONCM"].ToString();
        //            p.DESCRICAO = dd.Rows[i]["DESCRICAO"].ToString();

        //            DataRow[] it = dprod.Select("IDPRODUTOCLIENTE='" + p.IDPRODUTOCLIENTE + "'", "");
        //            List<CodigoDeBarras> lbar = new List<CodigoDeBarras>(); ;

        //            for (int j = 0; j < it.Length; j++)
        //            {
        //                CodigoDeBarras cb = new CodigoDeBarras();
        //                cb.CODIGODEBARRAS = it[j]["CODIGODEBARRAS"].ToString();
        //                lbar.Add(cb);
        //            }


        //            if (it.Length > 0)
        //                p.CODIGODEBARRA = lbar;

        //            prd.Add(p);

        //        }

        //        try
        //        {
        //            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "OK - ProdutosAtivos ", "SQL:" + sql, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
        //        }
        //        catch (Exception)
        //        { }

        //        return prd;
        //    }
        //    catch (Exception ex)
        //    {
        //        Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Erro - ProdutosAtivos ", "Ex:" + ex.Message, "mail.sistecno.com.br", "@oncetsis12122014", "HOMEREFILL");
        //        Err = ex.Message;
        //        throw ex;
        //    }
        //    finally
        //    {
        //        idLog = Sistran.Library.GetDataTables.LogMetodo(idLog, "ProdutosAtivos", null, null, "EXEC PRC_RETORNAR_PRODUTOS_HOMEREFILL - Finalizou -" + Err, cnx);
        //    }
        //}

        #region CNPJ / CPF
        public string FormatarCnpj(string s)
        {
            s = s.Replace(".", "");
            s = s.Replace("-", "");
            s = s.Replace("/", "");
            s = s.Replace(@"\", "");

            string resultCNPJ = "SIM";
            string resultCPF = "SIM";

            if (!IsCnpj(s))
                resultCNPJ = "NAO";

            if (!IsCpf(s))
                resultCPF = "NAO";

            if (resultCNPJ == "NAO" && resultCPF == "NAO")
                throw new Exception("CNPJ/CPF Inválidos");



            if (s.Length == 0)
            {
                return "";
            }

            if (s.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(s, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(s, 11));
                return mtpCnpj.ToString();
            }
        }

        public string ZerosEsquerda(string strString, int intTamanho)
        {

            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {

                strResult += "0";

            }

            return strResult + strString;

        }

        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        #endregion
    }

    public class Produto
    {
        public string IDPRODUTOCLIENTE { get; set; }
        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }
        public string CODIGONCM { get; set; }
        public List<CodigoDeBarras> CODIGODEBARRA { get; set; }

    }

    public class CodigoDeBarras
    {
        public string CODIGODEBARRAS { get; set; }
    }
    
    public class PedidoCancelado
    {
        public string Cliente_CNPJ { get; set; }
        public string NumeroDocumento { get; set; }
        public string Serie { get; set; }
        public string Dest_CNPJCPF { get; set; }
        public string CompraVenda { get; set; }

    }

    public class Pedido
    {
        public string Cliente_CNPJ { get; set; }
        public string TipoDeDocumento { get; set; }
        public DateTime DataDeEmissao { get; set; }
        public string NumeroDocumento { get; set; }
        public string Serie { get; set; }
        public string Dest_CNPJCPF { get; set; }
        public string Dest_IERG { get; set; }
        public string Dest_RAZAOSOCIAL { get; set; }
        public string Dest_FANTASIA { get; set; }
        public string Dest_ENDERECO { get; set; }
        public string Dest_NUMERO { get; set; }
        public string Dest_COMPLEMENTO { get; set; }
        public string Dest_BAIRRO { get; set; }
        public string Dest_COD_IBGE_CIDADE { get; set; }
        public string Dest_CIDADE_NOME { get; set; }
        public string Dest_CEP { get; set; }
        public string Dest_ENTREGA_CEP { get; set; }
        public string Dest_ENTREGA_ENDERECO { get; set; }
        public string Dest_ENTREGA_NUMERO { get; set; }
        public string Dest_ENTREGA_COMPLEMENTO { get; set; }
        public string Dest_ENTREGA_BAIRRO { get; set; }
        public string Dest_ENTREGA_COD_IBGE_CIDADE { get; set; }
        public string Dest_Email { get; set; }
        public string Dest_Telefone { get; set; }
        public string DataParaEntrega { get; set; }
        public string CompraVenda { get; set; }
        public string PeriodoDeEntregaInicio { get; set; }
        public string PeriodoDeEntregaFim { get; set; }
        public List<Itens> itens { get; set; }
        //public List<string> OcorrenciasPedidosAnteriores { get; set; }
        //public bool? PrimeiraEntrega { get; set; }
        
        //public class HistoricoPedido
        //{
        //    public bool PrimeiraEntrega { get; set; }
        //    public List<string> Ocorrencias { get; set; }

        //}


        public class Itens
        {
            public string SKU { get; set; }
            public string Descricao { get; set; }
            public string EAN { get; set; }

            public string CodigoNCM { get; set; }
            public int Quantidade { get; set; }
            public decimal ValorUnitario { get; set; }
            public decimal PesoLiquido { get; set; }

        }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    
}

public struct Pe22end
{
    public string IdCidadeP { get; set; }
    public string IdBairroP { get; set; }
    public string CEPP { get; set; }
    public string EnderecoP { get; set; }
    public string nBairroP { get; set; }
}

public static class ProcurarEndereco22
{

    public static string removerAcentos2(string texto)
    {
        string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

        for (int i = 0; i < comAcentos.Length; i++)
        {
            texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
        }
        return texto;
    }

    public static int RetornarCidade2(string NomeCidade, string CEP, string cnx)
    {
        NomeCidade = removerAcentos2(NomeCidade).Trim();

        string sql = "select IdCidade from Cidade where Nome = '" + NomeCidade + "' and IdEstado=26 ";
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

        sql = "Select * from CidadeFaixaDeCep cfc  ";
        sql += " inner join Cidade c on c.IdCidade = cfc.IdCidade ";
        sql += " where " + CEP + " between CepInicial and CepFinal ";

        dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0]["IdCidade"]);



        return 0;
    }

    public static int RetornarBairro(string Nome, string IdClidade, string cnx)
    {
        Nome = removerAcentos2(Nome);

        string sql = "select IdBairro from Bairro where Nome = '" + Nome + "' and IdCidade=" + IdClidade;
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0]["IdBairro"]);
        else
        {
            string Id = Sistran.Library.GetDataTables.RetornarIdTabela("Bairro", cnx);
            sql = "Insert into Bairro (IdBairro, Nome, IdCidade) values (" + Id + ", '" + Nome + "', " + IdClidade + ")";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
            return int.Parse(Id);
        }




    }
    public static Pe p;


    public class RetornoStatusNfes
    {
        public bool Erro { get; set; }
        public string DescErro { get; set; }

        public List<Ocorrencias> Ocorrencias { get; set; }


    }

    public class Ocorrencias
    {
        public string ID { get; set; }
        public string Numero { get; set; }
        public string Serie { get; set; }
        public string DataOcorrencia { get; set; }
        public string DescricaoOcorrencia { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string StatusFinalizada { get; set; }

    }





    //public class RetornoPedidoStatus
    //{
    //    public int Id { get; set; }
    //    public int NrPedido { get; set; }
    //    public string Status { get; set; }
    //    public string Consumido { get; set; }
    //    public DateTime DataHora { get; set; }

    //    public List<RetornoPedidoStatusItem> Items { get; set; }
    //}

    //public class RetornoPedidoStatusItem
    //{
    //   public int Id { get; set; }
    //    public int IdStatusPedido { get; set; }
    //    public string CodigoDoProduto { get; set; }
    //    public int Quantidade { get; set; }

    //}


    //public static Pe ProcurarPeloGoogle(string Log, string Lat, string cnx, string NomeRua)
    //{
    //    string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
    //    string requestUri = string.Format(baseUri, Lat, Log);
    //    p = new Pe();
    //    string url = requestUri;
    //    WebRequest request = WebRequest.Create(url);
    //    using (WebResponse response = (HttpWebResponse)request.GetResponse())
    //    {
    //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
    //        {
    //            DataSet dsResult = new DataSet();
    //            dsResult.ReadXml(reader);
    //            if (dsResult.Tables["GeocodeResponse"].Rows[0]["status"].ToString().ToUpper() == "OK")
    //            {
    //                string[] ret = dsResult.Tables["result"].Rows[0][1].ToString().Split(',');

    //                if (ret.Length >= 5)
    //                {

    //                    p.EnderecoP = ret[0];
    //                    p.CEPP = ret[3].Replace("-", "").Trim();
    //                    p.nBairroP = ret[1].Replace("'", "");

    //                    try
    //                    {
    //                        int c = int.Parse(ret[3].Replace("-", ""));
    //                        p.IdCidadeP = RetornarCidade(ret[2].Replace(" - SP", ""), p.CEPP, cnx).ToString();
    //                        p.IdBairroP = RetornarBairro(p.nBairroP, p.IdCidadeP, cnx).ToString();
    //                        return p;
    //                    }
    //                    catch (Exception ex)
    //                    {

    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return p;
    //}

    //public static Pe ProcurarPeloBing(string Log, string Lat, string cnx, string nomeRua)
    //{
    //    string baseUri = "http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&key=HTyoRgOqBO4gN9SL8ceN~h2Uqv9KKr4_F1Lqe2cVBbw~AsHsYOFxZiU8Jv2aDKhDohShli1JAiPvcwFBHZzBC-IOB8cA6VDQkIoxFfh1Zug6";
    //    string requestUri = string.Format(baseUri, Lat, Log);


    //    string url = requestUri;
    //    WebRequest request = WebRequest.Create(url);
    //    using (WebResponse response = (HttpWebResponse)request.GetResponse())
    //    {
    //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
    //        {
    //            DataSet dsResult = new DataSet();
    //            dsResult.ReadXml(reader);
    //            p = new Pe();

    //            if (dsResult.Tables["Response"].Rows[0]["statusDescription"].ToString().ToUpper() == "OK")
    //            {
    //                string[] ret = dsResult.Tables["Location"].Rows[0]["Name"].ToString().Split(',');


    //                if (ret.Length >= 3)
    //                {
    //                    if (ret.Length == 3)
    //                    {
    //                        p.CEPP = ret[1].Replace("-", "").Trim();
    //                    }
    //                    else if (ret.Length == 4)
    //                    {                           

    //                        //if (ret[2].Replace("-", "").Trim() == "SAO PAUL")
    //                        //{
    //                            p.EnderecoP = nomeRua.Replace("'", "");
    //                            p.CEPP = ret[3].Replace("-", "").Trim();
    //                            p.nBairroP = ret[1].Replace("'", "");

    //                            if (p.nBairroP.Contains("-"))
    //                                p.nBairroP = p.nBairroP.Split('-')[1].Trim();

    //                            if (p.CEPP.Length > 8)
    //                            {
    //                                p.CEPP = p.CEPP.Substring(0, 8);
    //                            }
    //                        //}
    //                        //else
    //                        //{

    //                        //    p.EnderecoP = nomeRua.Replace("'", "");
    //                        //    p.CEPP = ret[2].Replace("-", "").Trim();
    //                        //    p.nBairroP = ret[0].Replace("'", "");

    //                        //    if (p.CEPP.Length > 8)
    //                        //    {
    //                        //        p.CEPP = p.CEPP.Substring(0, 8);
    //                        //    }
    //                        //}
    //                    }
    //                    else
    //                    {
    //                        p.EnderecoP = ret[0];

    //                        if (ret[4].Replace("-", "").Trim().ToUpper() == "BRAZIL" || ret[4].Replace("-", "").Trim().ToUpper() == "BRASIL")
    //                            p.CEPP = ret[3].Replace("-", "").Trim();
    //                        else
    //                            p.CEPP = ret[4].Replace("-", "").Trim();

    //                        if (p.CEPP.Length > 8)
    //                        {
    //                            p.CEPP = p.CEPP.Substring(0, 8);
    //                        }

    //                        p.nBairroP = ret[1];

    //                    }

    //                    try
    //                    {
    //                        int c = int.Parse(p.CEPP);
    //                    }
    //                    catch (Exception)
    //                    {
    //                        for (int i = 1; i < dsResult.Tables["Location"].Rows.Count; i++)
    //                        {

    //                           string[]  ret1 = dsResult.Tables["Location"].Rows[i]["Name"].ToString().Split(',');

    //                            if (ret1.Length == 6)
    //                            {
    //                                p.EnderecoP = ret[0];

    //                                if (ret1[4].Replace("-", "").Trim().ToUpper() == "BRAZIL" || ret1[4].Replace("-", "").Trim().ToUpper() == "BRASIL" || ret1[4].Replace("-", "").Trim().ToUpper().Contains("SP"))
    //                                    p.CEPP = ret1[3].Replace("-", "").Trim();
    //                                else
    //                                    p.CEPP = ret1[4].Replace("-", "").Trim();

    //                                p.nBairroP = ret1[1];

    //                                try
    //                                {
    //                                    int d = int.Parse(p.CEPP);
    //                                    p.nBairroP = ret1[2];

    //                                    break;
    //                                }
    //                                catch (Exception ex)
    //                                {
    //                                    throw new Exception("Não foi Possivel Localizzar o Endereco 04. Lat:" + Lat + ", long: " + Log);

    //                                }
    //                            }
    //                        }

    //                        //throw new Exception("Não foi Possivel Localizzar o Endereco 03. Lat:"+ Lat + ", long: "+ Log);
    //                    }

    //                    try
    //                    {
    //                        if (ret[0].Contains("SP"))
    //                        {
    //                            p.IdCidadeP = RetornarCidade(ret[0].Trim().Replace("-", "").Replace("SP", ""), p.CEPP, cnx).ToString();
    //                            p.IdBairroP = "0";
    //                        }
    //                        else
    //                        {
    //                            p.IdCidadeP = RetornarCidade(ret[3].Trim().Replace("-", "").Replace("SP", ""), p.CEPP, cnx).ToString();
    //                            p.IdBairroP = RetornarBairro(ret[2].Trim(), p.IdCidadeP, cnx).ToString();
    //                        }
    //                    }
    //                    catch (Exception) { }

    //                }                    
    //            }
    //            else
    //                throw new Exception("Não foi Possivel Localizzar o Endereco 04. Lat:"+ Lat + ", long: "+ Log);

    //        }
    //    }
    //    return p;
    //}   
}