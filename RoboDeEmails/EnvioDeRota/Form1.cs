using RestSharp;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using EnvioDeRota.br.com.comprovei.soap;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EnvioDeRota
{
    public partial class frmEnvioDeRota : Form
    {
        public frmEnvioDeRota()
        {
            InitializeComponent();
        }


        public void EnviarRotaWsTransferencia()
        {

            timer1.Enabled = false;
            string iddt = "";


            try
            {
                textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
                Application.DoEvents();


                string x = "exec PRC_DtsLiberadasParaEnvioComproveiTransferencia";

                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                Application.DoEvents();
                const string quote = "\"";

                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
                {                    
                    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI_TRANSFERENCIA " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                    string ids = "";
                    //verifica se a nota foi enviada pro comrpvei
                    for (int o = 0; o < dtGeral.Rows.Count; o++)
                    {
                        if(dtGeral.Rows[o]["EnviadoComprovei"].ToString()=="")
                        {
                            ids = dtGeral.Rows[o]["Iddocumento"].ToString() + ",";
                        }
                    }

                    iddt = dtDTs.Rows[dts]["IdDt"].ToString();


                    if (ids.Length>5)
                    {
                        forcarSubidaNfe(ids, iddt);
                    }



                    if (dtGeral.Rows.Count == 0)
                    {
                      string  sql1 = "Update Dt Set ROTAENVIADACOMPROVEI='erro' where Iddt  = " + iddt;
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql1, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        continue;

                    }
                    DataView view = new DataView(dtGeral);
                    DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
                    string NomeArquivo = "";
                    int NumeroParada = 1;

                    string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

                    #region wsNovo
                    ///////////////////////////////////////////////////
                    br.com.comprovei.soap.Credenciais credenciais = new br.com.comprovei.soap.Credenciais();
                    credenciais.Usuario = "logos";
                    credenciais.Senha = "admin";

                    br.com.comprovei.soap.WebServiceComprovei webServiceComprovei = new br.com.comprovei.soap.WebServiceComprovei();
                    Rotas1 rota = null;
                    List<RotasRotaParada4> paradas = null;
                    for (int i = 0; i < dtds.Rows.Count; i++)
                    {

                        DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString().Trim() + "'" +
                                                        " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOBAIRRO='" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOCIDADE='" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString().Trim() + "'" +
                                                        " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString().Trim() + "'", "");

                        if (ret.Length == 0)
                            continue;
                        if (i == 0)
                        {
                            rota = new Rotas1();
                            paradas = new List<RotasRotaParada4>();

                            RotasRotaTransportadora1 transportadora1 = new RotasRotaTransportadora1();
                            transportadora1.Codigo = "";
                            transportadora1.Razao = "";

                            RotasRotaMotorista1 mot = new RotasRotaMotorista1();
                            mot.Usuario = ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "");
                            mot.PlacaVeiculo = ret[i]["PLACA"].ToString().Replace("-", "");
                            NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";



                            RotasRotaBase1 bs = new RotasRotaBase1()
                            {
                                Origem = new RotasRotaBaseOrigem1()
                                {
                                    codigo = NomeRota.Length > 30 ? NomeRota.Substring(0, 30) : NomeRota,
                                    Nome = NomeRota,
                                    Rua = dtGeral.Rows[0]["EndFil"].ToString().Trim(),
                                    Numero = (dtGeral.Rows[0]["NumFil"].ToString().Trim().ToUpper() == "SN"? "0": dtGeral.Rows[0]["NumFil"].ToString().Trim().ToUpper()),
                                    Complemento = dtGeral.Rows[0]["complFil"].ToString().Trim(),
                                    Cidade = dtGeral.Rows[0]["CidFil"].ToString().Trim(),
                                    Estado = dtGeral.Rows[0]["ufFil"].ToString().Trim(),
                                    Bairro = dtGeral.Rows[0]["BarFil"].ToString().Trim(),
                                    CEP = dtGeral.Rows[0]["CepFil"].ToString().Trim(),
                                    Pais = "BR",
                                    CodigoIBGE = dtGeral.Rows[0]["ibgeFil"].ToString().Trim()


                                },
                                Destino = new RotasRotaBaseDestino1()
                                {
                                    codigo = dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim().Length > 30 ? dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim().Substring(0, 30) : dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim(),
                                    Nome = dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim(),
                                    Rua = dtGeral.Rows[0]["EndFild"].ToString().Trim(),
                                    Numero = (dtGeral.Rows[0]["NumFild"].ToString().Trim().ToUpper() == "SN"? "0" : dtGeral.Rows[0]["NumFild"].ToString().Trim()),
                                    Complemento = dtGeral.Rows[0]["complFild"].ToString().Trim(),
                                    Cidade = dtGeral.Rows[0]["CidFild"].ToString().Trim(),
                                    Estado = dtGeral.Rows[0]["ufFild"].ToString().Trim(),
                                    Bairro = dtGeral.Rows[0]["BarFild"].ToString().Trim(),
                                    CEP = dtGeral.Rows[0]["CepFild"].ToString().Trim(),
                                    Pais = "BR",
                                    CodigoIBGE = dtGeral.Rows[0]["ibgeFild"].ToString().Trim()


                                }
                            };

                            rota.Rota = new RotasRota2()
                            {
                                TipoRotaSpecified = true,
                                TipoRota = RotasRotaTipoRota1.T,
                                numero = dtGeral.Rows[0]["NUMERO"].ToString(),
                                Data = DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd"),
                                Regiao = NomeRota,
                                Transportadora = transportadora1,
                                Motorista = mot,
                                Base = bs

                            };
                        }


                        for (int ii = 0; ii < ret.Length; ii++)
                        {
                            RotasRotaParadaDocumento2 doc = new RotasRotaParadaDocumento2();
                            doc.ChaveNota = ret[ii]["CHAVE"].ToString().Trim();

                            bool jaExiste = false;
                            for (int ienc = 0; ienc < paradas.Count; ienc++)
                            {
                                if (ret[ii]["CHAVE"].ToString().Trim() == paradas[ienc].Documento.ChaveNota)
                                {
                                    jaExiste = true;
                                    continue;
                                }

                            }

                            if (jaExiste)
                                continue;

                            paradas.Add(new RotasRotaParada4 { numero = NumeroParada.ToString(), Documento = doc });

                            if (ii == 0)
                                NumeroParada++;
                        }


                    }

                    rota.Rota.Paradas = paradas.ToArray();
                    rota.Rota.TipoRota = RotasRotaTipoRota1.T;

                    try
                    {
                        System.Xml.Serialization.XmlSerializer xx = new System.Xml.Serialization.XmlSerializer(rota.GetType());
                        TextWriter txtWriter = new StreamWriter(dtDTs.Rows[dts]["IDDt"].ToString() + ".xml");
                        xx.Serialize(txtWriter, rota);
                        txtWriter.Close();
                    }
                    catch (Exception)
                    {
                    }

                    var prot = "";
                    webServiceComprovei.CredenciaisValue = credenciais;
                    var retorno = webServiceComprovei.uploadRouteUsingDocumentKey(rota, NomeArquivo, out prot);


                    

                    /////////////////////////////////////////////////////
                    #endregion


                    string sql = "Update Dt Set ROTAENVIADACOMPROVEI=left('ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "-" + prot + "', 200) where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();

                }
            }
            catch (Exception ex)
            {

                string sql = "Update Dt Set ROTAENVIADACOMPROVEI='erro' where Iddt  = " + iddt;
                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:" + iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                timer1.Enabled = false;
                timer1.Enabled = true;
                textBox1.Text = "Finalizou as : " + DateTime.Now;
            }
            finally
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }

        }


        public void EnviarRotaWsDevolucao()
        {

            timer1.Enabled = false;
            string iddt = "";


            try
            {
                textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
                Application.DoEvents();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                string x = "exec PRC_DtsLiberadasParaEnvioComproveiDevolucao";

                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                Application.DoEvents();
                const string quote = "\"";

                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
                {
                    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI_Devolucao " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                    string ids = "";
                    //verifica se a nota foi enviada pro comrpvei
                    for (int o = 0; o < dtGeral.Rows.Count; o++)
                    {
                        if (dtGeral.Rows[o]["EnviadoComprovei"].ToString() == "")
                        {
                            ids = dtGeral.Rows[o]["Iddocumento"].ToString() + ",";
                        }
                    }

                    iddt = dtDTs.Rows[dts]["IdDt"].ToString();


                    if (ids.Length > 5)
                    {
                        forcarSubidaNfe(ids, iddt);
                    }



                    if (dtGeral.Rows.Count == 0)
                    {
                        string sql1 = "Update Dt Set ROTAENVIADACOMPROVEI='erro' where Iddt  = " + iddt;
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql1, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        continue;

                    }
                    DataView view = new DataView(dtGeral);
                    DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
                    string NomeArquivo = "";
                    int NumeroParada = 1;

                    string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

                    #region wsNovo
                    ///////////////////////////////////////////////////
                    br.com.comprovei.soap.Credenciais credenciais = new br.com.comprovei.soap.Credenciais();
                    credenciais.Usuario = "logos";
                    credenciais.Senha = "admin";

                    br.com.comprovei.ws_teste.WebServiceComprovei webServiceComprovei = new br.com.comprovei.ws_teste.WebServiceComprovei();
                    Rotas1 rota = null;
                    List<RotasRotaParada4> paradas = null;
                    for (int i = 0; i < dtds.Rows.Count; i++)
                    {

                        DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString().Trim() + "'" +
                                                        " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOBAIRRO='" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOCIDADE='" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString().Trim() + "'" +
                                                        " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString().Trim() + "'", "");

                        if (ret.Length == 0)
                            continue;
                        if (i == 0)
                        {
                            rota = new Rotas1();
                            paradas = new List<RotasRotaParada4>();

                            RotasRotaTransportadora1 transportadora1 = new RotasRotaTransportadora1();
                            transportadora1.Codigo = "";
                            transportadora1.Razao = "";

                            RotasRotaMotorista1 mot = new RotasRotaMotorista1();
                            mot.Usuario = ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "");
                            mot.PlacaVeiculo = ret[i]["PLACA"].ToString().Replace("-", "");
                            NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";



                            RotasRotaBase1 bs = new RotasRotaBase1()
                            {
                                Origem = new RotasRotaBaseOrigem1()
                                {
                                    codigo = NomeRota.Length > 30 ? NomeRota.Substring(0, 30) : NomeRota,
                                    Nome = NomeRota,
                                    Rua = dtGeral.Rows[0]["EndFil"].ToString().Trim(),
                                    Numero = (dtGeral.Rows[0]["NumFil"].ToString().Trim().ToUpper() == "SN" ? "0" : dtGeral.Rows[0]["NumFil"].ToString().Trim().ToUpper()),
                                    Complemento = dtGeral.Rows[0]["complFil"].ToString().Trim(),
                                    Cidade = dtGeral.Rows[0]["CidFil"].ToString().Trim(),
                                    Estado = dtGeral.Rows[0]["ufFil"].ToString().Trim(),
                                    Bairro = dtGeral.Rows[0]["BarFil"].ToString().Trim(),
                                    CEP = dtGeral.Rows[0]["CepFil"].ToString().Trim(),
                                    Pais = "BR",
                                    CodigoIBGE = dtGeral.Rows[0]["ibgeFil"].ToString().Trim()


                                },
                                Destino = new RotasRotaBaseDestino1()
                                {
                                    //codigo = dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim().Length > 20 ? dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim().Substring(0, 20) : dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim(),
                                    //Nome = dtGeral.Rows[0]["RegiaoDestino"].ToString().Trim(),
                                    //Rua = dtGeral.Rows[0]["EndFild"].ToString().Trim(),
                                    //Numero = (dtGeral.Rows[0]["NumFild"].ToString().Trim().ToUpper() == "SN" ? "0" : dtGeral.Rows[0]["NumFild"].ToString().Trim()),
                                    //Complemento = dtGeral.Rows[0]["complFild"].ToString().Trim(),
                                    //Cidade = dtGeral.Rows[0]["CidFild"].ToString().Trim(),
                                    //Estado = dtGeral.Rows[0]["ufFild"].ToString().Trim(),
                                    //Bairro = dtGeral.Rows[0]["BarFild"].ToString().Trim(),
                                    //CEP = dtGeral.Rows[0]["CepFild"].ToString().Trim(),
                                    //Pais = "BR",
                                    //CodigoIBGE = dtGeral.Rows[0]["ibgeFild"].ToString().Trim()

                                    codigo = NomeRota.Length > 30 ? NomeRota.Substring(0, 30) : NomeRota,
                                    Nome = NomeRota,
                                    Rua = dtGeral.Rows[0]["EndFil"].ToString().Trim(),
                                    Numero = (dtGeral.Rows[0]["NumFil"].ToString().Trim().ToUpper() == "SN" ? "0" : dtGeral.Rows[0]["NumFil"].ToString().Trim().ToUpper()),
                                    Complemento = dtGeral.Rows[0]["complFil"].ToString().Trim(),
                                    Cidade = dtGeral.Rows[0]["CidFil"].ToString().Trim(),
                                    Estado = dtGeral.Rows[0]["ufFil"].ToString().Trim(),
                                    Bairro = dtGeral.Rows[0]["BarFil"].ToString().Trim(),
                                    CEP = dtGeral.Rows[0]["CepFil"].ToString().Trim(),
                                    Pais = "BR",
                                    CodigoIBGE = dtGeral.Rows[0]["ibgeFil"].ToString().Trim()


                                }
                            };

                            rota.Rota = new RotasRota2()
                            {
                                TipoRotaSpecified = true,
                                TipoRota = RotasRotaTipoRota1.R,
                                numero = dtGeral.Rows[0]["NUMERO"].ToString(),
                                Data = DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd"),
                                Regiao = NomeRota,
                                Transportadora = transportadora1,
                                Motorista = mot,
                                Base = bs

                            };
                        }


                        for (int ii = 0; ii < ret.Length; ii++)
                        {
                            RotasRotaParadaDocumento2 doc = new RotasRotaParadaDocumento2();
                            doc.ChaveNota = ret[ii]["CHAVE"].ToString().Trim();

                            bool jaExiste = false;
                            for (int ienc = 0; ienc < paradas.Count; ienc++)
                            {
                                if (ret[ii]["CHAVE"].ToString().Trim() == paradas[ienc].Documento.ChaveNota)
                                {
                                    jaExiste = true;
                                    continue;
                                }

                            }

                            if (jaExiste)
                                continue;

                            paradas.Add(new RotasRotaParada4 { numero = NumeroParada.ToString(), Documento = doc });

                            if (ii == 0)
                                NumeroParada++;
                        }


                    }

                    rota.Rota.Paradas = paradas.ToArray();
                    rota.Rota.TipoRota = RotasRotaTipoRota1.R;

                    try
                    {
                        System.Xml.Serialization.XmlSerializer xx = new System.Xml.Serialization.XmlSerializer(rota.GetType());
                        TextWriter txtWriter = new StreamWriter(dtDTs.Rows[dts]["IDDt"].ToString() + ".xml");
                        xx.Serialize(txtWriter, rota);
                        txtWriter.Close();
                    }
                    catch (Exception)
                    {
                    }

                    var prot = "";
                    //webServiceComprovei.CredenciaisValue = credenciais;
                    



                   var retorno = webServiceComprovei.uploadRouteUsingDocumentKey(rota.ToString(), NomeArquivo, out prot);
                    /////////////////////////////////////////////////////
                    #endregion


                    string sql = "Update Dt Set ROTAENVIADACOMPROVEI=left('ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "-" + prot + "', 200) where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();
                }
            }
            catch (Exception ex)
            {

                string sql = "Update Dt Set ROTAENVIADACOMPROVEI='erro' where Iddt  = " + iddt;
                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:" + iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                timer1.Enabled = false;
                timer1.Enabled = true;
                textBox1.Text = "Finalizou as : " + DateTime.Now;
            }
            finally
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }

        }
        private void forcarSubidaNfe(string ids, string iddt)
        {

            if (ids != "")
            {
                try
                {
                    ids = ids.Substring(0, ids.Length - 1);
                    Sistran.Library.GetDataTables.RetornarDataSetWS("update documento set EnviadoComprovei='NAO'  where Iddocumento in(" + ids + "); update dt set RotaEnviadaComprovei='9999' where Iddt="+iddt+" ; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());                    
                }
                catch (Exception)
                {
                }

            }
        }

        public void EnviarRotaWs()
        {

            timer1.Enabled = false;
            string iddt = "";
                       

            try
            {
                textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
                Application.DoEvents();


                string x = "exec PRC_DTSLIBERADASPARAENVIOCOMPROVEI";

                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                Application.DoEvents();
                const string quote = "\"";

                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
                {
                    //DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI 1027031 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                    

                    iddt = dtDTs.Rows[dts]["IdDt"].ToString();

                    if (dtGeral.Rows.Count == 0)
                        continue;


                    string ids = "";
                    //verifica se a nota foi enviada pro comrpvei
                    for (int o = 0; o < dtGeral.Rows.Count; o++)
                    {
                        if (dtGeral.Rows[o]["EnviadoComprovei"].ToString() == "")
                        {
                            ids = dtGeral.Rows[o]["Iddocumento"].ToString() + ",";
                        }
                    }

                    if (ids.Length > 5)
                    {
                        forcarSubidaNfe(ids, iddt);
                    }

                    DataView view = new DataView(dtGeral);
                    DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
                    string NomeArquivo = "";
                    int NumeroParada = 1;

                    string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

                    #region wsNovo
                    ///////////////////////////////////////////////////
                    br.com.comprovei.soap.Credenciais credenciais = new br.com.comprovei.soap.Credenciais();
                    credenciais.Usuario = "logos";
                    credenciais.Senha = "admin";

                    br.com.comprovei.soap.WebServiceComprovei webServiceComprovei = new br.com.comprovei.soap.WebServiceComprovei();
                    Rotas1 rota = null;
                    List<RotasRotaParada4> paradas = null;
                    for (int i = 0; i < dtds.Rows.Count; i++)
                    {

                        DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString().Trim() + "'" +
                                                        " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOBAIRRO='" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOCIDADE='" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString().Trim() + "'" +
                                                        " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString().Trim() + "'", "");

                        if (ret.Length == 0)
                            continue;
                        if (i == 0)
                        {
                            rota = new Rotas1();
                             paradas = new List<RotasRotaParada4>();

                            RotasRotaTransportadora1 transportadora1 = new RotasRotaTransportadora1();
                            transportadora1.Codigo = "";
                            transportadora1.Razao = "";

                            RotasRotaMotorista1 mot = new RotasRotaMotorista1();
                            mot.Usuario = ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "");
                            mot.PlacaVeiculo = ret[i]["PLACA"].ToString().Replace("-", "");
                            NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";
                            rota.Rota = new RotasRota2()
                            {
                                numero = dtGeral.Rows[0]["NUMERO"].ToString(),
                                Data = DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd"),
                                Regiao = NomeRota,
                                Transportadora = transportadora1,
                                Motorista = mot
                            };
                        }


                        for (int ii = 0; ii < ret.Length; ii++)
                        {
                            RotasRotaParadaDocumento2 doc = new RotasRotaParadaDocumento2();
                            doc.ChaveNota = ret[ii]["CHAVE"].ToString().Trim();

                            bool jaExiste = false;
                            for (int ienc = 0; ienc < paradas.Count; ienc++)
                            {
                                if (ret[ii]["CHAVE"].ToString().Trim() == paradas[ienc].Documento.ChaveNota)
                                {
                                    jaExiste = true;
                                    continue;
                                }

                            }

                            if (jaExiste)
                                continue;

                            paradas.Add(new RotasRotaParada4 { numero = NumeroParada.ToString(), Documento = doc });

                            if (ii == 0)
                                NumeroParada++;
                        }


                    }
                    
                    rota.Rota.Paradas = paradas.ToArray();

                    try
                    {
                        System.Xml.Serialization.XmlSerializer xx = new System.Xml.Serialization.XmlSerializer(rota.GetType());
                        TextWriter txtWriter = new StreamWriter(dtDTs.Rows[dts]["IDDt"].ToString() + ".xml");
                        xx.Serialize(txtWriter, rota);
                        txtWriter.Close();
                    }
                    catch (Exception)
                    {
                    }

                    var prot = "";
                    webServiceComprovei.CredenciaisValue = credenciais;
                    var retorno = webServiceComprovei.uploadRouteUsingDocumentKey(rota, NomeArquivo, out prot);
                    /////////////////////////////////////////////////////
                    #endregion


                    textBox1.Text = dtDTs.Rows[dts]["IDDt"].ToString() + "-" + retorno;
                    Application.DoEvents();

                    string sql = "Update Dt Set ROTAENVIADACOMPROVEI=left('ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "-" + prot + "', 200) where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();


                }
            }
            catch (Exception ex)
            {

                string sql = "Update Dt Set ROTAENVIADACOMPROVEI='erro' where Iddt  = " + iddt;                
                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:" + iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                timer1.Enabled = false;
                timer1.Enabled = true;
                textBox1.Text = "Finalizou as : " + DateTime.Now;
            }
            finally
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                var x = Sistran.Library.GetDataTables.RetornarDataSetWS("update dt set RotaEnviadaComprovei=null where RotaEnviadaComprovei='9999' and Emissao>=getdate()-2; select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                string iddt = "";

                EnviarRotaWs();
                EnviarRotaWs();


                for (int i = 0; i < 5; i++)
                {
                    EnviarRotaWsTransferencia();
                    EnviarRotaWsTransferencia();

                    try
                    {
                        for (int ii = 0; ii < 5; ii++)
                        {
                            EnviarInicioRota();

                        }
                        // EnviarFinalRota();

                    }
                    catch (Exception)
                    {
                    }

                }

                EnviarRotaWs();
                EnviarRotaWs();
                EnviarRotaWs();

                try
                {
                    EnviarRotaWsDevolucao();
                    EnviarRotaWsDevolucao();
                    EnviarRotaWsDevolucao();
                }
                catch (Exception)
                {

                    throw;
                }


            }
            catch (Exception)
            {

            }
            finally
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }




            //EnviarRotaWsTransferencia();

            /*
            try
            {
                textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
                Application.DoEvents();


                string x = "exec PRC_DtsLiberadasParaEnvioComprovei";

                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                Application.DoEvents();
                const string quote = "\"";

                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
                {
                    //DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI 1027031 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                    iddt = dtDTs.Rows[dts]["IdDt"].ToString();

                    if (dtGeral.Rows.Count == 0)
                        continue;

                    DataView view = new DataView(dtGeral);
                    DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
                    string NomeArquivo = "";
                    int NumeroParada = 1;

                    string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

                    string xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
                    xml += "<Rotas>";
                    // xml += "<Rota numero=" + quote + NomeRota + quote + ">";
                    //  xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMEROROTA"].ToString() + quote + " Regiao=" + quote + NomeRota + quote + ">";
                    xml += "<Rota numero=" + quote + dtGeral.Rows[0]["NUMERO"].ToString() + quote + ">";

                    for (int i = 0; i < dtds.Rows.Count; i++)
                    {

                        DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString().Trim() + "'" +
                                                        " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOBAIRRO='" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOCIDADE='" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString().Trim() + "'" +
                                                        " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString().Trim() + "'", "");

                        if (ret.Length == 0)
                            continue;

                        if (i == 0)
                        {
                            xml += "<Data>" + DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd") + "</Data>";
                            xml += "<Regiao>" + NomeRota + "</Regiao>";
                            xml += "<Transportadora>";
                            xml += "<Codigo></Codigo>";
                            xml += "<Razao></Razao>";
                            xml += "</Transportadora>";
                            xml += "<Motorista>";
                            xml += "<Usuario>" + ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "") + "</Usuario>";
                            xml += "<PlacaVeiculo>" + ret[i]["PLACA"].ToString().Replace("-", "") + "</PlacaVeiculo>";
                            xml += "</Motorista>";
                            xml += "<Paradas>";
                            NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";
                        }


                        for (int ii = 0; ii < ret.Length; ii++)
                        {
                            xml += "<Parada numero=" + quote + NumeroParada + quote + ">";
                            xml += "<Documento>";
                            xml += "<ChaveNota>" + ret[ii]["CHAVE"].ToString().Trim() + "</ChaveNota>";
                            xml += "</Documento>";
                            xml += "</Parada>";
                            NumeroParada++;
                        }
                    }
                    xml += "</Paradas>";
                    xml += "</Rota>";
                    xml += "</Rotas>";

                    try
                    {
                        System.IO.File.WriteAllText(@"C:\temp\xmlRota\" + iddt + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml", xml);
                    }
                    catch (Exception) { }

                    string auxXML = xml;
                    xml = Base64Encode(xml);

                    WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
                    string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
                                   "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<SOAP-ENV:Body>" +
                                   "<tns:sendDocsKeysToPOD xmlns:tns=\"urn:WebServicePOD\">" +
                                   "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
                                   "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
                                   "</tns:" + "sendDocsKeysToPOD" + ">" +
                                   "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "text/xml; charset=ISO-8859-1";
                    request.Headers.Add("SOAPAction", "urn:WebServicePOD#sendDocsKeysToPOD");
                    request.Headers.Add("Authorization", "Basic " + auth);
                    request.ContentLength = data.Length;


                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }


                    Cursor.Current = Cursors.WaitCursor;
                    WebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    XmlDocument xmlAwnser = new XmlDocument();
                    xmlAwnser.LoadXml(sr.ReadToEnd());
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);
                    string retorno = xmlAwnser["status"].InnerText;
                    retorno = retorno.Replace("'", "");

                    string sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();


                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:" + iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                timer1.Enabled = false;
                timer1.Enabled = true;
                textBox1.Text = "Finalizou as : " + DateTime.Now;
            }
            finally
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
            */

            //return;
            /*
            try
            {
                textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
                Application.DoEvents();


                string x = "exec PRC_DTSLIBERADASPARAENVIOCOMPROVEI";

                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                Application.DoEvents();
                const string quote = "\"";

                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
                {
                    //DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI 1027031 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                    iddt = dtDTs.Rows[dts]["IdDt"].ToString();

                    if (dtGeral.Rows.Count == 0)
                        continue;

                    DataView view = new DataView(dtGeral);
                    DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
                    string NomeArquivo = "";
                    int NumeroParada = 1;

                    string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

                    #region wsNovo
                    ///////////////////////////////////////////////////
                    br.com.comprovei.soap.Credenciais credenciais = new br.com.comprovei.soap.Credenciais();
                    credenciais.Usuario = "logos";
                    credenciais.Senha = "admin";

                    br.com.comprovei.soap.WebServiceComprovei webServiceComprovei = new br.com.comprovei.soap.WebServiceComprovei();
                    Rotas1 rota = null;
                    List<RotasRotaParada4> paradas = null;
                    for (int i = 0; i < dtds.Rows.Count; i++)
                    {

                        DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString().Trim() + "'" +
                                                        " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOBAIRRO='" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString().Trim() + "'" +
                                                        " and IDENDERECOCIDADE='" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString().Trim() + "'" +
                                                        " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString().Trim() + "'", "");

                        if (ret.Length == 0)
                            continue;
                        if (i == 0)
                        {
                            rota = new Rotas1();
                            paradas = new List<RotasRotaParada4>();

                            RotasRotaTransportadora1 transportadora1 = new RotasRotaTransportadora1();
                            transportadora1.Codigo = "";
                            transportadora1.Razao = "";

                            RotasRotaMotorista1 mot = new RotasRotaMotorista1();
                            mot.Usuario = ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "");
                            mot.PlacaVeiculo = ret[i]["PLACA"].ToString().Replace("-", "");
                            NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";
                            rota.Rota = new RotasRota2()
                            {
                                numero = dtGeral.Rows[0]["NUMERO"].ToString(),
                                Data = DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd"),
                                Regiao = NomeRota,
                                Transportadora = transportadora1,
                                Motorista = mot
                            };
                        }


                        for (int ii = 0; ii < ret.Length; ii++)
                        {
                            RotasRotaParadaDocumento2 doc = new RotasRotaParadaDocumento2();
                            doc.ChaveNota = ret[ii]["CHAVE"].ToString().Trim();
                            paradas.Add(new RotasRotaParada4 { numero = NumeroParada.ToString(), Documento = doc });

                            if (ii == 0)
                                NumeroParada++;
                        }


                    }

                    rota.Rota.Paradas = paradas.ToArray();
                    var prot = "";
                    webServiceComprovei.CredenciaisValue = credenciais;


                    try
                    {
                        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(rota.GetType());
                        TextWriter txtWriter = new StreamWriter(rota.Rota.numero + ".xml");
                        xs.Serialize(txtWriter, rota);
                        txtWriter.Close();

                    }
                    catch (Exception)
                    {
                    }

                    string sql = "";
                    var retorno = webServiceComprovei.uploadRouteUsingDocumentKey(rota, NomeArquivo, out prot);
                    /////////////////////////////////////////////////////
                    #endregion

                    if (retorno != "Arquivo adicionado para a fila de importação!")
                    {

                        sql = "Update Dt Set ROTAENVIADACOMPROVEI= left('Retorno: " + retorno + "-" + DateTime.Now + "', 150) where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                        sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                        sql = "";
                        return;

                    }
                    sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
                    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:" + iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
                timer1.Enabled = false;
                timer1.Enabled = true;
                textBox1.Text = "Finalizou as : " + DateTime.Now;
            }
            finally
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
            */
        }


        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }



        private void frmEnvioDeRota_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "Iniciou as : " + DateTime.Now;
                timer1.Enabled = true;
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

          //  EnviarInicioRota();


            //var client = new RestClient("https://ws-kabum.transpofrete.com.br/api/auth/login/ws.vex/@Vex2021");
            ////https://ws-kabum.transpofrete.com.br/api/auth/login/ws.vex/@Vex2021
            //var request = new RestRequest(RestSharp.Method.GET);
            //request.AddHeader("content-type", "application/json");
            //request.AddHeader("platform", "x");
            //request.AddParameter("application/json", ParameterType.RequestBody);
            //request.AddHeader("Cookie", "JSESSIONID=C147CC91C8CE56A17FE32F11B69CBD30; USUARIO=ws.vex");
            //IRestResponse response = client.Execute(request);


            //var mm = JsonConvert.SerializeObject(response);
            //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(mm);
            //return myDeserializedClass.Cookies[0].Value;

            //string iddt = "";

            ////if(DateTime.Now.Minute % 5 ==0)
            ////    EnviarComprovei();

            //try
            //{
            //    textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
            //    Application.DoEvents();


            //    string x = "exec PRC_DTSLIBERADASPARAENVIOCOMPROVEI";

            //    DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

            //    Application.DoEvents();
            //    const string quote = "\"";

            //    string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
            //    string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

            //    //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
            //    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

            //    for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
            //    {
            //        //DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI 1027031 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            //        DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            //        iddt = dtDTs.Rows[dts]["IdDt"].ToString();

            //        if (dtGeral.Rows.Count == 0)
            //            continue;

            //        DataView view = new DataView(dtGeral);
            //        DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
            //        string NomeArquivo = "";
            //        int NumeroParada = 1;

            //        string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

            //        #region wsNovo
            //        ///////////////////////////////////////////////////
            //        br.com.comprovei.soap.Credenciais credenciais = new br.com.comprovei.soap.Credenciais();
            //        credenciais.Usuario = "logos";
            //        credenciais.Senha = "admin";

            //        br.com.comprovei.soap.WebServiceComprovei webServiceComprovei = new br.com.comprovei.soap.WebServiceComprovei();
            //        Rotas1 rota = null;
            //        List<RotasRotaParada4> paradas = null;
            //        for (int i = 0; i < dtds.Rows.Count; i++)
            //        {

            //            DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString().Trim() + "'" +
            //                                            " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString().Trim() + "'" +
            //                                            " and IDENDERECOBAIRRO='" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString().Trim() + "'" +
            //                                            " and IDENDERECOCIDADE='" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString().Trim() + "'" +
            //                                            " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString().Trim() + "'", "");

            //            if (ret.Length == 0)
            //                continue;
            //            if (i == 0)
            //            {
            //                rota = new Rotas1();
            //                paradas = new List<RotasRotaParada4>();

            //                RotasRotaTransportadora1 transportadora1 = new RotasRotaTransportadora1();
            //                transportadora1.Codigo = "";
            //                transportadora1.Razao = "";

            //                RotasRotaMotorista1 mot = new RotasRotaMotorista1();
            //                mot.Usuario = ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "");
            //                mot.PlacaVeiculo = ret[i]["PLACA"].ToString().Replace("-", "");
            //                NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";
            //                rota.Rota = new RotasRota2()
            //                {
            //                    numero = dtGeral.Rows[0]["NUMERO"].ToString(),
            //                    Data = DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd"),
            //                    Regiao = NomeRota,
            //                    Transportadora = transportadora1,
            //                    Motorista = mot
            //                };
            //            }


            //            for (int ii = 0; ii < ret.Length; ii++)
            //            {
            //                RotasRotaParadaDocumento2 doc = new RotasRotaParadaDocumento2();
            //                doc.ChaveNota = ret[ii]["CHAVE"].ToString().Trim();
            //                paradas.Add(new RotasRotaParada4 { numero = NumeroParada.ToString(), Documento = doc });

            //                if (ii == 0)
            //                    NumeroParada++;
            //            }


            //        }

            //        rota.Rota.Paradas = paradas.ToArray();
            //        var prot = "";
            //        webServiceComprovei.CredenciaisValue = credenciais;


            //        try
            //        {
            //            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(rota.GetType());
            //            TextWriter txtWriter = new StreamWriter(rota.Rota.numero + ".xml");
            //            xs.Serialize(txtWriter, rota);
            //            txtWriter.Close();

            //        }
            //        catch (Exception)
            //        {
            //        }


            //        var retorno = webServiceComprovei.uploadRouteUsingDocumentKey(rota, NomeArquivo, out prot);
            //        /////////////////////////////////////////////////////
            //        #endregion


            //        string sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno + "-" + prot + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
            //        sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
            //        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            //        textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();


            //    }
            //}
            //catch (Exception ex)
            //{
            //    textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
            //    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:" + iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
            //    timer1.Enabled = false;
            //    timer1.Enabled = true;
            //    textBox1.Text = "Finalizou as : " + DateTime.Now;
            //}
            //finally
            //{
            //    timer1.Enabled = false;
            //    timer1.Enabled = true;
            //}
        }

        private void EnviarInicioRota( )
        {
            //return;
            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient("https://entregas-api.comprovei.com/v1/routes/start");
                string sql = "exec Prc_inicioRota";

            
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                string IdDts = "";
                Root root = new Root();
                List<Route> lista = new List<Route>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Route route = new Route()
                    {
                        occurrence_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),// "2019-11-12 00:00:00",
                        route_number = dt.Rows[i]["NUMERO"].ToString(), //"exemplo4",
                        route_date = DateTime.Now.ToString("yyyy-MM-dd"),  //"2019-11-11",
                        driver = dt.Rows[i]["CNPJCPF"].ToString().Replace("-", "").Replace(".", ""),// "65040930160",
                        latitude = 0,
                        longitude = 0,
                        device_id = "",
                        device_model = ""

                    };
                    lista.Add(route);
                    IdDts += dt.Rows[i]["IdDT"].ToString() + ",";
                }
                if (lista.Count == 0)
                    return;

                root.routes = lista;
                var json = JsonConvert.SerializeObject(root);


                var request = new RestRequest(RestSharp.Method.PATCH);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("User-Agent", "Facility");
                request.AddHeader("Authorization", "Basic bG9nb3M6YWRtaW4=");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                var mm = JsonConvert.SerializeObject(response.Content);

                IdDts += "0";
                sql = "Update Dt set IniRota='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +  " - " +mm.ToString()+"' where IdDt in("+IdDts+")";
                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


            }
            catch (Exception ex )
            {
            }
        }


        private void EnviarFinalRota()
        {
            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient("https://entregas-api.comprovei.com/v1/routes/finish");
                string sql = "exec Prc_FinalRota";


                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                string IdDts = "";
                Root root = new Root();
                List<Route> lista = new List<Route>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Route route = new Route()
                    {
                        occurrence_date = DateTime.Parse(dt.Rows[i]["DataDeChegada"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),// "2019-11-12 00:00:00",
                        route_number = dt.Rows[i]["NUMERO"].ToString(), //"exemplo4",
                        route_date = DateTime.Now.ToString("yyyy-MM-dd"),  //"2019-11-11",
                        driver = dt.Rows[i]["CNPJCPF"].ToString().Replace("-", "").Replace(".", ""),// "65040930160",
                        latitude = 0,
                        longitude = 0,
                        device_id = "",
                        device_model = ""

                    };
                    lista.Add(route);
                    IdDts += dt.Rows[i]["IdDT"].ToString() + ",";
                }

                if (lista.Count == 0)
                    return;
                
                root.routes = lista;
                var json = JsonConvert.SerializeObject(root);

                


                var request = new RestRequest(RestSharp.Method.PATCH);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("User-Agent", "Facility");
                request.AddHeader("Authorization", "Basic bG9nb3M6YWRtaW4=");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                var mm = JsonConvert.SerializeObject(response.Content);

                IdDts += "0";
                sql = "Update Dt set FIMRota='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + mm.ToString() + "' where IdDt in(" + IdDts + ")";
                Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


            }
            catch (Exception ex)
            {
            }
        }
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Route
{
    public string occurrence_date { get; set; }
    public string route_number { get; set; }
    public string route_date { get; set; }
    public string driver { get; set; }
    public int latitude { get; set; }
    public int longitude { get; set; }
    public string device_id { get; set; }
    public string device_model { get; set; }
}

public class Root
{
    public List<Route> routes { get; set; }
}



//using System;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Net;
//using System.IO;
//using System.Xml;
//using EnvioDeRota.br.com.comprovei.soap;
//using System.Collections.Generic;

//namespace EnvioDeRota
//{
//    public partial class frmEnvioDeRota : Form
//    {
//        public frmEnvioDeRota()
//        {
//            InitializeComponent();
//        }

//        private void timer1_Tick(object sender, EventArgs e)
//        {
//             timer1.Enabled = false;
//            string iddt = "";

//            //if(DateTime.Now.Minute % 5 ==0)
//            //    EnviarComprovei();

//            try
//            {
//                textBox1.Text = "Enviar Rotas: " + DateTime.Now.ToString();
//                Application.DoEvents();


//                string x = "exec PRC_DTSLIBERADASPARAENVIOCOMPROVEI";

//                DataTable dtDTs = Sistran.Library.GetDataTables.RetornarDataSetWS(x, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

//                Application.DoEvents();
//                const string quote = "\"";

//                string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
//                string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

//                //string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logosteste" + ":" + "admin"));
//                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin")); //producao

//                for (int dts = 0; dts < dtDTs.Rows.Count; dts++)
//                {
//                    //DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI 1027031 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
//                    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_ROTAS_COMPROVEI " + dtDTs.Rows[dts]["IdDt"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
//                    iddt = dtDTs.Rows[dts]["IdDt"].ToString();

//                    if (dtGeral.Rows.Count == 0)
//                        continue;

//                    DataView view = new DataView(dtGeral);
//                    DataTable dtds = view.ToTable(true, "ENDERECO", "ENDERECONUMERO", "ENDERECOCOMPLEMENTO", "IDENDERECOBAIRRO", "IDENDERECOCIDADE", "ENDERECOCEP");
//                    string NomeArquivo = "";
//                    int NumeroParada = 1;

//                    string NomeRota = dtGeral.Rows[0]["Regiao"].ToString().Trim();

//                    #region wsNovo
//                    ///////////////////////////////////////////////////
//                    br.com.comprovei.soap.Credenciais credenciais = new br.com.comprovei.soap.Credenciais();
//                    credenciais.Usuario = "logos";
//                    credenciais.Senha = "admin";

//                    br.com.comprovei.soap.WebServiceComprovei webServiceComprovei = new br.com.comprovei.soap.WebServiceComprovei();
//                    Rotas1 rota= null;
//                    List<RotasRotaParada4> paradas = null;
//                    for (int i = 0; i < dtds.Rows.Count; i++)
//                    {

//                        DataRow[] ret = dtGeral.Select("ENDERECO='" + dtds.Rows[i]["ENDERECO"].ToString().Trim() + "'" +
//                                                        " and ENDERECONUMERO='" + dtds.Rows[i]["ENDERECONUMERO"].ToString().Trim() + "'" +
//                                                        " and IDENDERECOBAIRRO='" + dtds.Rows[i]["IDENDERECOBAIRRO"].ToString().Trim() + "'" +
//                                                        " and IDENDERECOCIDADE='" + dtds.Rows[i]["IDENDERECOCIDADE"].ToString().Trim() + "'" +
//                                                        " and ENDERECOCEP='" + dtds.Rows[i]["ENDERECOCEP"].ToString().Trim() + "'", "");

//                        if (ret.Length == 0)
//                            continue;
//                        if (i == 0)
//                        {
//                            rota = new Rotas1();
//                            paradas = new List<RotasRotaParada4>();

//                            RotasRotaTransportadora1 transportadora1 = new RotasRotaTransportadora1();
//                            transportadora1.Codigo = "";
//                            transportadora1.Razao = "";

//                            RotasRotaMotorista1 mot = new RotasRotaMotorista1();
//                            mot.Usuario = ret[i]["CODIGOMOTORISTA"].ToString().Replace(".", "").Replace("/", "").Replace("-", "");
//                            mot.PlacaVeiculo = ret[i]["PLACA"].ToString().Replace("-", "");
//                            NomeArquivo = dtDTs.Rows[dts]["IDDt"].ToString() + ".xml";
//                            rota.Rota = new RotasRota2()
//                            {
//                                numero = dtGeral.Rows[0]["NUMERO"].ToString(),
//                                Data = DateTime.Parse(ret[i]["Data"].ToString()).ToString("yyyyMMdd"),
//                                Regiao = NomeRota,
//                                Transportadora = transportadora1,
//                                Motorista = mot
//                            };
//                        }


//                        for (int ii = 0; ii < ret.Length; ii++)
//                        { 
//                            RotasRotaParadaDocumento2 doc = new RotasRotaParadaDocumento2();
//                            doc.ChaveNota = ret[ii]["CHAVE"].ToString().Trim();
//                            paradas.Add(new RotasRotaParada4 { numero = NumeroParada.ToString(), Documento = doc });

//                            if(ii==0)
//                                NumeroParada++;
//                        }


//                    }

//                    rota.Rota.Paradas = paradas.ToArray();
//                    var prot = "";
//                    webServiceComprovei.CredenciaisValue = credenciais;
//                    var retorno = webServiceComprovei.uploadRouteUsingDocumentKey(rota, NomeArquivo, out prot);
//                    /////////////////////////////////////////////////////
//                    #endregion


//                    string sql = "Update Dt Set ROTAENVIADACOMPROVEI='ENVIADO PARA COMPROVEI " + DateTime.Now + " - " + retorno  + "-" + prot + "' where Iddt  = " + dtGeral.Rows[0]["IdDT"].ToString();
//                    sql += "; insert into HistoricoEnvioRota values (" + dtDTs.Rows[dts]["IDDt"].ToString() + ", '" + retorno + "',getdate())";
//                    Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
//                    textBox1.Text = "Enviou a Dt: " + dtDTs.Rows[dts]["IDDt"].ToString();


//                }
//            }
//            catch (Exception ex)
//            {
//                textBox1.Text = "Enviar ROTAS Comprovei Erro: " + DateTime.Now.ToString();
//                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Envio Comprovei - rotas", "iddt:" + iddt + "- Erro em: " + DateTime.Now.ToString() + "-" + ex.Message + ex.InnerException, "mail.grupologos.com.br", "logos0902", "Envio Comprovei erro.");
//                timer1.Enabled = false;
//                timer1.Enabled = true;
//                textBox1.Text = "Finalizou as : " + DateTime.Now;
//            }
//            finally
//            {
//                timer1.Enabled = false;
//                timer1.Enabled = true;
//            }
//        }


//        private string Base64Encode(string plainText)
//        {
//            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
//            return System.Convert.ToBase64String(plainTextBytes);
//        }



//        private void frmEnvioDeRota_Load(object sender, EventArgs e)
//        {
//			try
//			{

////                ///////////////////////////////////////////////////
////                br.com.comprovei.soap.Credenciais credenciais = new br.com.comprovei.soap.Credenciais();
////                credenciais.Usuario = "logos";
////                credenciais.Senha = "admin";

////                br.com.comprovei.soap.WebServiceComprovei webServiceComprovei = new br.com.comprovei.soap.WebServiceComprovei();

////                Rotas1 rota = new Rotas1();                
////                RotasRotaTransportadora1 transportadora1 = new RotasRotaTransportadora1();
////                transportadora1.Codigo = "";
////                transportadora1.Razao= "";

////                RotasRotaMotorista1 mot = new RotasRotaMotorista1();
////                mot.Usuario = "vagners";
////                mot.PlacaVeiculo = "FDQ7136";

////                List<RotasRotaParada4> paradas = new List<RotasRotaParada4>();
////                RotasRotaParadaDocumento2 doc = new RotasRotaParadaDocumento2();
////                doc.ChaveNota = "2020043005221620200430719267877774";

////                paradas.Add(new RotasRotaParada4 { numero="1",  Documento = doc });

////                rota.Rota = new RotasRota2()
////                {
////                    numero = "1234",
////                    Data = "20200506",
////                    Regiao = "70 - VEX CAJAMAR",
////                    Transportadora = transportadora1,
////                    Motorista = mot,
////                    Paradas = paradas.ToArray()
////                };

////                var prot = "";





////                webServiceComprovei.CredenciaisValue = credenciais;

////                var ret = webServiceComprovei.uploadRouteUsingDocumentKey(rota, "teste.xml", out prot);
/////////////////////////////////////////////////////////



//                textBox1.Text = "Iniciou as : " + DateTime.Now;
//				timer1.Enabled = true;
//			}catch(Exception)
//			{
//				timer1.Enabled = false;
//				timer1.Enabled = true;
//			}
//        }       
//    }
//}

