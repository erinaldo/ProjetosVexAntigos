using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sistecno.BLL.Helpers
{
    public static class DocEletronico
    {
        public static CTeX.spdCTeX PreencherParametros(DataTable emitente, DataTable cids, string CaminhoCliente, string CaminhoDoPadrao, int idEmpresa, string CaminhoLog, int idfilial)
        {
            try
            {
                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "Iniciou a Função", "Preencher");

                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "Instanciou Variáveis do Componente", "Preencher");
                CTeX.spdCTeX CTe = new CTeX.spdCTeX();
                CTeDataSetX.spdCTeDataSetX CTeDataSet = new CTeDataSetX.spdCTeDataSetX();
                XmlDocument xDoc = new XmlDocument();
                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "Terminou Instanciar Variáveis do Componente", "Preencher");


                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "Setou Variáveis", "Preencher");

                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, emitente.Rows[0]["Certificado"].ToString(), "Preencher");

                CTe.NomeCertificado = emitente.Rows[0]["Certificado"].ToString();
                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "CTe.NomeCertificado", "PreencherParametros");

                CTe.UF = cids.Rows[0]["UF"].ToString();
                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "CTe.UF", "PreencherParametros");


                CTe.CNPJ = emitente.Rows[0]["CNPJCPF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "");
                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, " CTe.CNPJ", "PreencherParametros");


                string vRNTRC = emitente.Rows[0]["RNTRC"].ToString();
                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "vRNTRC", "PreencherParametros");



                if (emitente.Rows[0]["Ambiente"].ToString() == "akProducao")
                    CTe.Ambiente = CTeX.Ambiente.akProducao;
                else
                    CTe.Ambiente = CTeX.Ambiente.akHomologacao;


                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "Ambiente", "PreencherParametros");


                CTe.VersaoManual = CTeX.VersaoManual.vm200;
                CTe.ModoOperacao = CTeX.ModoOperacao.moNormalX;

                CTe.ArquivoServidoresHom = CaminhoDoPadrao + "cteServidoresHom.ini";
                CTe.ArquivoServidoresProd = CaminhoDoPadrao + "cteServidoresProd.ini";

                Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "ArquivoServidoresHom", "PreencherParametros");


                if (emitente.Rows[0]["TipoCertificado"].ToString() == "ckFile")
                    CTe.TipoCertificado = CTeX.TipoCerticado.ckFileX;
                else
                    CTe.TipoCertificado = CTeX.TipoCerticado.ckSmartCardX;

                CTe.MappingFileName = "MappingCte.txt";

               Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "TipoCertificado", "PreencherParametros");



                if (emitente.Rows[0]["IgnoreInvalidCertificates"].ToString() == "1")
                    CTe.IgnoreInvalidCertificates = true;
                else
                    CTe.IgnoreInvalidCertificates = false;

                CTe.MaxSizeLoteEnvio = int.Parse(emitente.Rows[0]["MaxSizeLoteEnvio"].ToString());
                CTe.DiretorioLog = CaminhoCliente + "\\cte\\Sistecno.BLL.Helpers.Util.Log";
                CTe.DiretorioXMLTomadorServico = CaminhoCliente + "\\cte\\XML";


               Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "MaxSizeLoteEnvio", "PreencherParametros");


                CTe.DiretorioTemplates = CaminhoDoPadrao + "templates";
                CTe.DiretorioEsquemas = CaminhoDoPadrao + "Esquemas";


                if (emitente.Rows[0]["ValidarEsquemaAntesEnvio"].ToString() == "1")
                    CTe.ValidarEsquemaAntesEnvio = true;
                else
                    CTe.ValidarEsquemaAntesEnvio = false;

                CTe.ValidarEsquemaAntesEnvio = true;


                CTe.DiretorioXMLTomadorServico = CaminhoCliente + "\\cte\\XMl"; ;
                CTe.IgnoreInvalidCertificates = true;
                CTe.AnexarDactePDF = false;
                CTe.TimeOut = 0;

                CTe.ImpressaoLogoTipoEmitente = CaminhoCliente + "\\" + idEmpresa + ".jpg";
                CTe.ImpressaoModeloRetrato = CTe.DiretorioTemplates + "2.00\\Dacte\\Retrato.rtm";
                CTe.ImpressaoModeloPaisagem = CTe.DiretorioTemplates + "2.00\\Dacte\\Paisagem.rtm";
                CTe.ImpressaoFraseHomologacao = "SEM VALOR FISCAL";
                CTe.ImpressaoQtdeCopias = 1;

               Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "Retornou Obj CTE", "Preencher");
                return CTe;
            }
            catch (Exception ex)
            {
               Sistecno.BLL.Helpers.Util.Log.GravarLog(CaminhoLog, "Erro: " + ex.Message + " - " + ex.InnerException + " - " + ex.Source, "Preencher");
                throw ex;
            }
        }

        public static int GerarLoteEnviarCte(DataTable d, CTeX.spdCTeX CTe, string modelo, string serieDP, int idfilial, List<int> CtesSelecionados, string rntc, string cnx, string caminhoDoCliente)
        {
            string xmlLote = "", chave = "";
            try
            {
                CTeDataSetX.spdCTeDataSetX CTeDataSet = new CTeDataSetX.spdCTeDataSetX();
                List<NumeroChave> lNumeroNfs = new List<NumeroChave>();


                string IdLoteEletronico = LoteEletronico.CriarLote(idfilial, d.Rows[0]["REMFANTASIAAPELIDO"].ToString(), cnx).ToString();

                CTeDataSet.ConfigSection = "XMLENVIO";
                CTeDataSet.MappingFileName = CTe.DiretorioEsquemas + "2.00\\MappingCte.txt";
                CTeDataSet.Versao = "2.00";
                string aCodigo = Sistecno.BLL.Helpers.Util.Validacoes.ZerosEsquerda(IdLoteEletronico, 8);
                CTeDataSet.IdLote = aCodigo;
                CTeDataSet.CreateDataSets();
                Random Rand = new Random();

                for (int i = 0; i < (d.Rows.Count > 50 ? 49 : d.Rows.Count); i++)
                {
                    if ((d.Rows[i]["NumeroNF"].ToString() == "" || int.Parse(d.Rows[i]["NumeroNF"].ToString()) > 0) &&
                                  d.Rows[i]["CStatus"].ToString() != "100" &&
                                  d.Rows[i]["CStatus"].ToString() != "103" &&
                                  d.Rows[i]["CStatus"].ToString() != "105")
                    {
                        string aCodigoUF = CTe.ObterCodigoUF(CTe.UF).ToString();


                        string aModelo = modelo;
                        string aSerie = serieDP;

                        if (aSerie.Trim() == "")
                            aSerie = "1";
                        string aTpEmiss = "";

                        switch (CTe.ModoOperacao)
                        {
                            case CTeX.ModoOperacao.moNormalX:
                                aTpEmiss = "1";
                                break;

                            case CTeX.ModoOperacao.moFSDAX:
                                aTpEmiss = "5";
                                break;

                            case CTeX.ModoOperacao.moEPECX:
                                aTpEmiss = "4";
                                break;

                            case CTeX.ModoOperacao.moSVCX:
                                aTpEmiss = "8";
                                break;
                        }

                        int aNumero = 0;
                        if (d.Rows[i]["NumeroDocumentoEletronico"].ToString() == "" || int.Parse(d.Rows[i]["NumeroDocumentoEletronico"].ToString()) <= 0)
                            aNumero = FuncoesCte.Numerar("CONHECIMENTO", idfilial, true, int.Parse(d.Rows[i]["IDDOCUMENTO"].ToString()), aSerie, cnx);
                        else
                            aNumero = int.Parse(d.Rows[i]["Numero"].ToString());



                        CTeDataSet.Incluir();
                        CTeDataSet.SetCampo("versao_2", "2.00");
                        CTeDataSet.SetCampo("Id_3", "");
                        CTeDataSet.SetCampo("cUF_5", aCodigoUF);
                        CTeDataSet.SetCampo("cCT_6", aCodigo);
                        CTeDataSet.SetCampo("CFOP_7", "5357");
                        CTeDataSet.SetCampo("natOp_8", "TRANSPORTE RODOVIARIO DE CARGAS");
                        CTeDataSet.SetCampo("forPag_9", "0");
                        CTeDataSet.SetCampo("mod_10", aModelo);
                        CTeDataSet.SetCampo("serie_11", aSerie);
                        CTeDataSet.SetCampo("nCT_12", aNumero.ToString());

                        lNumeroNfs.Add(new NumeroChave(aNumero.ToString(), int.Parse(d.Rows[i]["IDDOCUMENTO"].ToString())));

                        CTeDataSet.SetCampo("dhEmi_13", String.Format("{0:yyyy-MM-ddThh:mm:ss}", DateTime.Now));
                        CTeDataSet.SetCampo("tpImp_14", "1");
                        CTeDataSet.SetCampo("tpEmis_15", aTpEmiss);
                        CTeDataSet.SetCampo("cDV_16", Convert.ToString(Rand.Next(1, 9)));
                        CTeDataSet.SetCampo("tpAmb_17", "2");
                        CTeDataSet.SetCampo("tpCTe_18", "0");
                        CTeDataSet.SetCampo("procEmi_19", "0");
                        CTeDataSet.SetCampo("verProc_20", "1");
                        CTeDataSet.SetCampo("refCTE_21", "");

                        CTeDataSet.SetCampo("cMunEnv_672", d.Rows[i]["EmiIBGE"].ToString());
                        CTeDataSet.SetCampo("xMunEnv_673", d.Rows[i]["EmiCidade"].ToString());
                        CTeDataSet.SetCampo("uFEnv_674", d.Rows[i]["EmiUF"].ToString());


                        //01=Rodoviário; 02=Aéreo; 03=Aquaviário; 04=Ferroviário; 05=Dutoviário
                        CTeDataSet.SetCampo("modal_25", "01");

                        CTeDataSet.SetCampo("tpServ_26", "0");
                        CTeDataSet.SetCampo("cMunIni_27", d.Rows[i]["RemIBGE"].ToString()); //BARUERIRemIBGE
                        CTeDataSet.SetCampo("xMunIni_28", d.Rows[i]["RemCidade"].ToString());
                        CTeDataSet.SetCampo("UFIni_29", d.Rows[i]["RemUF"].ToString());
                        CTeDataSet.SetCampo("cMunFim_30", d.Rows[i]["DesIBGE"].ToString());
                        CTeDataSet.SetCampo("xMunFim_31", d.Rows[i]["DesCidade"].ToString());
                        CTeDataSet.SetCampo("UFFim_32", d.Rows[i]["DesUF"].ToString());
                        CTeDataSet.SetCampo("retira_33", "1");
                        CTeDataSet.SetCampo("xDetRetira_34", "");
                        CTeDataSet.SetCampo("toma_36", "0");



                        //DADOS DO EMTENTE
                        CTeDataSet.SetCampo("CNPJ_95", CTe.CNPJ.Replace("/", "").Replace("-", "").Replace(".", ""));
                        CTeDataSet.SetCampo("IE_96", d.Rows[i]["EmiIE"].ToString());

                        if (CTe.Ambiente == CTeX.Ambiente.akHomologacao)
                            CTeDataSet.SetCampo("xNome_97", "CT-E EMITIDO EM AMBIENTE DE HOMOSistecno.BLL.Helpers.Util.LogACAO - SEM VALOR FISCAL");
                        else
                            CTeDataSet.SetCampo("xNome_97", d.Rows[i]["EmiRazaoSocial"].ToString());




                        CTeDataSet.SetCampo("xFant_98", d.Rows[i]["EmiFantasiaApelido"].ToString());
                        CTeDataSet.SetCampo("xLgr_100", d.Rows[i]["EmiEndereco"].ToString());
                        CTeDataSet.SetCampo("nro_101", d.Rows[i]["EmiNumero"].ToString());
                        CTeDataSet.SetCampo("xCpl_102", d.Rows[i]["EmiComplemento"].ToString());



                        if (d.Rows[i]["EmiBairro"].ToString().Trim() == "")
                            CTeDataSet.SetCampo("xBairro_103", "BAIRRO");
                        else
                            CTeDataSet.SetCampo("xBairro_103", d.Rows[i]["EmiBairro"].ToString());


                        CTeDataSet.SetCampo("cMun_104", d.Rows[i]["EmiIBGE"].ToString());
                        CTeDataSet.SetCampo("xMun_105", d.Rows[i]["EmiCidade"].ToString());
                        CTeDataSet.SetCampo("CEP_106", d.Rows[i]["EmiCep"].ToString());
                        CTeDataSet.SetCampo("UF_107", d.Rows[i]["EmiUF"].ToString());
                        CTeDataSet.SetCampo("fone_110", d.Rows[i]["EmiFone"].ToString().Replace(" ", "").Replace("-", ""));


                        //DADOS DO REMETENTE
                        if (d.Rows[i]["RemCnpjCpf"].ToString().Trim().Length > 14)
                        {
                            CTeDataSet.SetCampo("CNPJ_112", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemCnpjCpf"].ToString())));
                            CTeDataSet.SetCampo("CPF_113", "");
                            CTeDataSet.SetCampo("IE_114", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemIE"].ToString())));
                        }
                        else
                        {
                            CTeDataSet.SetCampo("CNPJ_112", "");
                            CTeDataSet.SetCampo("CPF_113", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemCnpjCpf"].ToString())));
                            CTeDataSet.SetCampo("IE_114", "");
                        }



                        if (CTe.Ambiente == CTeX.Ambiente.akHomologacao)
                        {
                            CTeDataSet.SetCampo("xNome_115", "CT-E EMITIDO EM AMBIENTE DE HOMOSistecno.BLL.Helpers.Util.LogACAO - SEM VALOR FISCAL");
                            CTeDataSet.SetCampo("xFant_116", "CT-E EMITIDO EM AMBIENTE DE HOMOSistecno.BLL.Helpers.Util.LogACAO - SEM VALOR FISCAL");
                        }
                        else
                        {
                            CTeDataSet.SetCampo("xNome_115", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemRazaoSocial"].ToString())));
                        }

                        CTeDataSet.SetCampo("xFant_116", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemFantasiaApelido"].ToString())));


                        CTeDataSet.SetCampo("fone_117", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemFone"].ToString().Replace(" ", "").Replace("-", ""))));
                        CTeDataSet.SetCampo("xLgr_119", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemEndereco"].ToString())));
                        CTeDataSet.SetCampo("nro_120", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemNumero"].ToString())));
                        CTeDataSet.SetCampo("xCpl_121", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemComplemento"].ToString())));
                        CTeDataSet.SetCampo("xBairro_122", (Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemBairro"].ToString() == "" ? "BAIRRO:" : Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemBairro"].ToString()))))));
                        CTeDataSet.SetCampo("cMun_123", d.Rows[i]["RemIBGE"].ToString());
                        CTeDataSet.SetCampo("xMun_124", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemCidade"].ToString())));
                        CTeDataSet.SetCampo("CEP_125", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemCep"].ToString())));
                        CTeDataSet.SetCampo("UF_126", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemUF"].ToString())));
                        CTeDataSet.SetCampo("cPais_127", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemPaisCodigo"].ToString())));
                        CTeDataSet.SetCampo("xPais_128", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp((d.Rows[i]["RemPais"].ToString())));
                        CTeDataSet.SetCampo("email_604", "");

                        //CHAVE DA NOTA

                        DataTable Nfs = DocumentoRelacionado.RetornarNotasCte(int.Parse(d.Rows[i]["IDDOCUMENTO"].ToString()), cnx);

                        for (int inf = 0; inf < Nfs.Rows.Count; inf++)
                        {
                            //if (Nfs.Rows[inf]["Chaveexterna"].ToString() != "" || Nfs.Rows[inf]["ChaveDeAcesso"].ToString() != "")
                            if (0 == 1)
                            {
                                CTeDataSet.IncluirParte("infNFe");
                                if (Nfs.Rows[inf]["Chaveexterna"].ToString() != "")
                                    CTeDataSet.SetCampo("chave_156", Nfs.Rows[inf]["Chaveexterna"].ToString());
                                else
                                    CTeDataSet.SetCampo("chave_156", Nfs.Rows[inf]["ChaveDeAcesso"].ToString());

                                CTeDataSet.SetCampo("PIN_157", "");
                                CTeDataSet.SalvarParte("infNFe");
                            }
                            else
                            {
                                CTeDataSet.IncluirParte("infNF");
                                CTeDataSet.SetCampo("nRoma_130", d.Rows[i]["DocumentoDoCliente1"].ToString());


                                CTeDataSet.SetCampo("nPed_131", "");
                                CTeDataSet.SetCampo("mod_605", "01");

                                if (Nfs.Rows[inf]["Serie"].ToString() == "")
                                    CTeDataSet.SetCampo("serie_132", "1");
                                else
                                    CTeDataSet.SetCampo("serie_132", Nfs.Rows[inf]["Serie"].ToString());

                                if (Nfs.Rows[inf]["NF"].ToString() == "")
                                    CTeDataSet.SetCampo("nDoc_133", d.Rows[i]["IdDocumento"].ToString());
                                else
                                    CTeDataSet.SetCampo("nDoc_133", Nfs.Rows[inf]["NF"].ToString());


                                if (Nfs.Rows[inf]["DataDeEmissao"].ToString() == "")
                                    CTeDataSet.SetCampo("dEmi_134", DateTime.Now.ToString("yyyy-MM-dd"));
                                else
                                    CTeDataSet.SetCampo("dEmi_134", DateTime.Parse(Nfs.Rows[inf]["DataDeEmissao"].ToString()).ToString("yyyy-MM-dd"));

                                CTeDataSet.SetCampo("vBC_135", float.Parse((Nfs.Rows[inf]["BaseDoIcms"].ToString() == "" ? "0" : Nfs.Rows[inf]["BaseDoIcms"].ToString())).ToString("#0.00").Replace(",", "."));
                                CTeDataSet.SetCampo("vICMS_136", float.Parse((Nfs.Rows[inf]["ValorDoIcms"].ToString() == "" ? "0" : Nfs.Rows[inf]["ValorDoIcms"].ToString())).ToString("#0.00").Replace(",", "."));
                                CTeDataSet.SetCampo("vBCST_137", float.Parse((Nfs.Rows[inf]["BaseDoIcmsSubst"].ToString() == "" ? "0" : Nfs.Rows[inf]["BaseDoIcmsSubst"].ToString())).ToString("#0.00").Replace(",", "."));

                                CTeDataSet.SetCampo("vST_138", float.Parse((Nfs.Rows[inf]["ValorDoIcmsSubst"].ToString() == "" ? "0" : Nfs.Rows[inf]["ValorDoIcmsSubst"].ToString())).ToString("#0.00").Replace(",", "."));
                                CTeDataSet.SetCampo("vProd_139", float.Parse((Nfs.Rows[inf]["BaseDoIcmsSubst"].ToString() == "" ? "0" : Nfs.Rows[inf]["BaseDoIcmsSubst"].ToString())).ToString("#0.00").Replace(",", "."));
                                CTeDataSet.SetCampo("vNF_140", float.Parse((Nfs.Rows[inf]["ValorDaNota"].ToString() == "" ? "0" : Nfs.Rows[inf]["ValorDaNota"].ToString())).ToString("#0.00").Replace(",", "."));

                                string ClasseCfop = d.Rows[i]["ClasseCfop"].ToString();

                                if (Nfs.Rows[inf]["Cfop"].ToString() == "")
                                    CTeDataSet.SetCampo("nCFOP_141", ClasseCfop + 102);
                                else
                                    CTeDataSet.SetCampo("nCFOP_141", ClasseCfop + Nfs.Rows[inf]["Cfop"].ToString());

                                CTeDataSet.SetCampo("nPeso_142", "");
                                CTeDataSet.SetCampo("PIN_143", "");

                                if (d.Rows[i]["RemCnpjCpf"].ToString().Trim().Length > 14)
                                {
                                    CTeDataSet.SetCampo("CNPJ_145", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RemCnpjCpf"].ToString()));
                                    CTeDataSet.SetCampo("CPF_146", "");
                                }
                                else
                                {
                                    CTeDataSet.SetCampo("CNPJ_145", "");
                                    CTeDataSet.SetCampo("CPF_146", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RemCnpjCpf"].ToString()));
                                }

                                if (CTe.Ambiente == CTeX.Ambiente.akHomologacao)
                                    CTeDataSet.SetCampo("xNome_147", "CT-E EMITIDO EM AMBIENTE DE HOMOSistecno.BLL.Helpers.Util.LogACAO - SEM VALOR FISCAL");
                                else
                                    CTeDataSet.SetCampo("xNome_147", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RemRazaoSocial"].ToString()));

                                CTeDataSet.SetCampo("xLgr_148", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiEndereco"].ToString()));
                                CTeDataSet.SetCampo("nro_149", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiNumero"].ToString()));

                                CTeDataSet.SetCampo("nro_149", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiNumero"].ToString()));
                                CTeDataSet.SetCampo("xCpl_150", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiComplemento"].ToString()));

                                if (d.Rows[i]["EmiBairro"].ToString() == "")
                                    CTeDataSet.SetCampo("xBairro_151", "BAIRRO:");
                                else
                                    CTeDataSet.SetCampo("xBairro_151", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiBairro"].ToString()));

                                CTeDataSet.SetCampo("cMun_152", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiIBGE"].ToString()));
                                CTeDataSet.SetCampo("xMun_153", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiCidade"].ToString()));
                                CTeDataSet.SetCampo("UF_154", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["EmiUF"].ToString()));
                                CTeDataSet.SalvarParte("infNF");
                            }
                        }

                        CTeDataSet.IncluirParte("infOutros");
                        CTeDataSet.SetCampo("tpDoc_159", "");
                        CTeDataSet.SetCampo("descOutros_160", "");
                        CTeDataSet.SetCampo("nDoc_161", "");
                        CTeDataSet.SetCampo("dEmi_162", "");
                        CTeDataSet.SetCampo("vDocFisc_163", "");
                        CTeDataSet.SalvarParte("infOutros");

                        //  {EXPEDIDOR}
                        CTeDataSet.SetCampo("CNPJ_165", "");
                        CTeDataSet.SetCampo("CPF_166", "");
                        CTeDataSet.SetCampo("IE_167", "");
                        CTeDataSet.SetCampo("xNome_168", "");
                        CTeDataSet.SetCampo("fone_169", "");
                        CTeDataSet.SetCampo("xLgr_171", "");
                        CTeDataSet.SetCampo("nro_172", "");
                        CTeDataSet.SetCampo("xCpl_173", "");
                        CTeDataSet.SetCampo("xBairro_174", "");
                        CTeDataSet.SetCampo("cMun_175", "");
                        CTeDataSet.SetCampo("xMun_176", "");
                        CTeDataSet.SetCampo("CEP_177", "");
                        CTeDataSet.SetCampo("UF_178", "");
                        CTeDataSet.SetCampo("cPais_179", "");
                        CTeDataSet.SetCampo("xPais_180", "");
                        CTeDataSet.SetCampo("email_606", "");



                        //      {RECEBEDOR DA CARGA}
                        if (d.Rows[i]["RedIDCadastro"].ToString() == "" || int.Parse(d.Rows[i]["RedIDCadastro"].ToString()) < 1)
                        {
                            CTeDataSet.SetCampo("CNPJ_182", "");
                            CTeDataSet.SetCampo("CPF_183", "");
                            CTeDataSet.SetCampo("IE_184", "");
                            CTeDataSet.SetCampo("xNome_185", "");
                            CTeDataSet.SetCampo("fone_186", "");
                            CTeDataSet.SetCampo("xLgr_188", "");
                            CTeDataSet.SetCampo("nro_189", "");
                            CTeDataSet.SetCampo("xCpl_190", "");
                            CTeDataSet.SetCampo("xBairro_191", "");
                            CTeDataSet.SetCampo("cMun_192", "");
                            CTeDataSet.SetCampo("xMun_193", "");
                            CTeDataSet.SetCampo("CEP_194", "");
                            CTeDataSet.SetCampo("UF_195", "");
                            CTeDataSet.SetCampo("cPais_196", "");
                            CTeDataSet.SetCampo("xPais_197", "");
                        }
                        else
                        {
                            if (d.Rows[i]["RedCnpjCpf"].ToString().Trim().Length > 14)
                            {
                                CTeDataSet.SetCampo("CNPJ_182", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedCnpjCpf"].ToString()));
                                CTeDataSet.SetCampo("CPF_183", "");
                                CTeDataSet.SetCampo("IE_184", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedIE"].ToString()));
                            }
                            else
                            {
                                CTeDataSet.SetCampo("CNPJ_182", "");
                                CTeDataSet.SetCampo("CPF_183", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedCnpjCpf"].ToString()));
                                CTeDataSet.SetCampo("IE_184", "");
                            }

                            if (CTe.Ambiente == CTeX.Ambiente.akHomologacao)
                                CTeDataSet.SetCampo("xNome_185", "CT-E EMITIDO EM AMBIENTE DE HOMOSistecno.BLL.Helpers.Util.LogACAO - SEM VALOR FISCAL");
                            else
                                CTeDataSet.SetCampo("xNome_185", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedRazaoSocial"].ToString()));


                            CTeDataSet.SetCampo("xNome_185", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedRazaoSocial"].ToString()));
                            CTeDataSet.SetCampo("fone_186", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedFone"].ToString().Replace(" ", "").Replace("-", "")));
                            CTeDataSet.SetCampo("xLgr_188", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedEndereco"].ToString()));

                            if (d.Rows[i]["RedNumero"].ToString() == "")
                                CTeDataSet.SetCampo("nro_189", "NUMERO:");
                            else
                                CTeDataSet.SetCampo("nro_189", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedNumero"].ToString()));

                            CTeDataSet.SetCampo("xCpl_190", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedComplemento"].ToString()));

                            if (d.Rows[i]["RedBairro"].ToString() == "")
                                CTeDataSet.SetCampo("xBairro_191", "BAIRRO:");
                            else
                                CTeDataSet.SetCampo("xBairro_191", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedBairro"].ToString()));


                            CTeDataSet.SetCampo("cMun_192", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedIBGE"].ToString()));
                            CTeDataSet.SetCampo("xMun_193", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedBairro"].ToString()));
                            CTeDataSet.SetCampo("CEP_194", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedCep"].ToString()));
                            CTeDataSet.SetCampo("UF_195", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedUF"].ToString()));
                            CTeDataSet.SetCampo("cPais_196", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedPaisCodigo"].ToString()));
                            CTeDataSet.SetCampo("xPais_197", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["RedPais"].ToString()));

                        }
                        CTeDataSet.SetCampo("email_607", "");

                        //DESTINATARIO
                        if (d.Rows[i]["DesCnpjCpf"].ToString().Trim().Length > 14)
                        {
                            CTeDataSet.SetCampo("CNPJ_199", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesCnpjCpf"].ToString()));
                            CTeDataSet.SetCampo("CPF_200", "");
                            CTeDataSet.SetCampo("IE_201", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesIE"].ToString()));
                        }
                        else
                        {
                            CTeDataSet.SetCampo("CNPJ_182", "");
                            CTeDataSet.SetCampo("CPF_200", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesCnpjCpf"].ToString()));
                            CTeDataSet.SetCampo("IE_201", "");
                        }



                        CTeDataSet.SetCampo("xNome_202", "CT-E EMITIDO EM AMBIENTE DE HOMOSistecno.BLL.Helpers.Util.LogACAO - SEM VALOR FISCAL");

                        if (CTe.Ambiente == CTeX.Ambiente.akHomologacao)
                            CTeDataSet.SetCampo("xNome_202", "CT-E EMITIDO EM AMBIENTE DE HOMOSistecno.BLL.Helpers.Util.LogACAO - SEM VALOR FISCAL");
                        else
                            CTeDataSet.SetCampo("xNome_202", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesRazaoSocial"].ToString()));

                        CTeDataSet.SetCampo("fone_203", "");

                        CTeDataSet.SetCampo("ISUF_204", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesSuframa"].ToString()));
                        CTeDataSet.SetCampo("xLgr_206", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesEndereco"].ToString()));

                        if (d.Rows[i]["DesNumero"].ToString() == "")
                            CTeDataSet.SetCampo("nro_207", "NUMERO:");
                        else
                            CTeDataSet.SetCampo("nro_207", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesNumero"].ToString()));

                        CTeDataSet.SetCampo("xCpl_208", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesComplemento"].ToString()));


                        if (d.Rows[i]["DesBairro"].ToString() == "")
                            CTeDataSet.SetCampo("xBairro_209", "BAIRRO:");
                        else
                            CTeDataSet.SetCampo("xBairro_209", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesBairro"].ToString()));


                        CTeDataSet.SetCampo("cMun_210", d.Rows[i]["DesIBGE"].ToString());
                        CTeDataSet.SetCampo("xMun_211", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesCidade"].ToString()));
                        CTeDataSet.SetCampo("CEP_212", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesCep"].ToString()));
                        CTeDataSet.SetCampo("UF_213", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesUF"].ToString()));
                        CTeDataSet.SetCampo("cPais_214", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesPaisCodigo"].ToString()));
                        CTeDataSet.SetCampo("xPais_215", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(d.Rows[i]["DesPais"].ToString()));
                        CTeDataSet.SetCampo("email_608", "");


                        //{LOCAL DE ENTREGA CONSTANTE NA NOTA}
                        CTeDataSet.SetCampo("CNPJ_217", "");
                        CTeDataSet.SetCampo("CPF_218", "");
                        CTeDataSet.SetCampo("xNome_219", "");
                        CTeDataSet.SetCampo("xLgr_220", "");
                        CTeDataSet.SetCampo("nro_221", "");
                        CTeDataSet.SetCampo("xCpl_222", "");
                        CTeDataSet.SetCampo("xBairro_223", "");
                        CTeDataSet.SetCampo("cMun_224", "");
                        CTeDataSet.SetCampo("xMun_225", "");
                        CTeDataSet.SetCampo("UF_226", "");


                        //frete Modal
                        DataTable dtFt = Frete.RetornarFrete(int.Parse(d.Rows[i]["IdDocumento"].ToString()), cnx);
                        if (dtFt.Rows.Count == 0)
                            throw new Exception("Frete Não Encontrado");

                        if (d.Rows[i]["CteTipoDeCte"].ToString() != "COMPLEMENTO")
                        {

                            CTeDataSet.SetCampo("vTPrest_228", float.Parse((dtFt.Rows[0]["Frete"].ToString() == "" ? "0" : dtFt.Rows[0]["Frete"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("vRec_229", float.Parse((dtFt.Rows[0]["Frete"].ToString() == "" ? "0" : dtFt.Rows[0]["Frete"].ToString())).ToString("#0.00").Replace(",", "."));

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Frete Peso");
                            CTeDataSet.SetCampo("vComp_232", float.Parse((dtFt.Rows[0]["FretePeso"].ToString() == "" ? "0" : dtFt.Rows[0]["FretePeso"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Frete Valor");

                            float valor =
                                float.Parse((dtFt.Rows[0]["FretePercentual"].ToString() == "" ? "0" : dtFt.Rows[0]["FretePercentual"].ToString())) +
                                float.Parse((dtFt.Rows[0]["FreteValor"].ToString() == "" ? "0" : dtFt.Rows[0]["FreteValor"].ToString()));

                            CTeDataSet.SetCampo("vComp_232", valor.ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Gris");
                            CTeDataSet.SetCampo("vComp_232",
                                float.Parse(dtFt.Rows[0]["GRIS"].ToString() == "" ? "0" : dtFt.Rows[0]["GRIS"].ToString()).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Cat");
                            CTeDataSet.SetCampo("vComp_232",
                                float.Parse(dtFt.Rows[0]["Cat"].ToString() == "" ? "0" : dtFt.Rows[0]["Cat"].ToString()).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Despacho");
                            CTeDataSet.SetCampo("vComp_232", float.Parse((dtFt.Rows[0]["Despacho"].ToString() == "" ? "0" : dtFt.Rows[0]["Despacho"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "TED");
                            CTeDataSet.SetCampo("vComp_232", float.Parse((dtFt.Rows[0]["TED"].ToString() == "" ? "0" : dtFt.Rows[0]["TED"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Pedagio");
                            CTeDataSet.SetCampo("vComp_232", float.Parse(dtFt.Rows[0]["Pedagio"].ToString() == "" ? "0" : dtFt.Rows[0]["Pedagio"].ToString()).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Seguro/Suframa");
                            valor = float.Parse((dtFt.Rows[0]["seguro"].ToString() == "" ? "0" : dtFt.Rows[0]["seguro"].ToString())) +
                                float.Parse((dtFt.Rows[0]["suframa"].ToString() == "" ? "0" : dtFt.Rows[0]["suframa"].ToString()));
                            CTeDataSet.SetCampo("vComp_232", valor.ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Taxa Col/Ent");
                            valor = float.Parse((dtFt.Rows[0]["TaxaDeColeta"].ToString() == "" ? "0" : dtFt.Rows[0]["TaxaDeColeta"].ToString())) +
                                    float.Parse((dtFt.Rows[0]["TaxaDeEntrega"].ToString() == "" ? "0" : dtFt.Rows[0]["TaxaDeEntrega"].ToString()));
                            CTeDataSet.SetCampo("vComp_232", valor.ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Carga/Descarga");
                            valor = float.Parse((dtFt.Rows[0]["descarga"].ToString() == "" ? "0" : dtFt.Rows[0]["descarga"].ToString())) +
                                    float.Parse((dtFt.Rows[0]["paletizacao"].ToString() == "" ? "0" : dtFt.Rows[0]["paletizacao"].ToString())) +
                                    float.Parse((dtFt.Rows[0]["ajudante"].ToString() == "" ? "0" : dtFt.Rows[0]["ajudante"].ToString())) +
                                    float.Parse((dtFt.Rows[0]["diaria"].ToString() == "" ? "0" : dtFt.Rows[0]["diaria"].ToString())) +
                                    float.Parse((dtFt.Rows[0]["descarganaopaletizada"].ToString() == "" ? "0" : dtFt.Rows[0]["descarganaopaletizada"].ToString())) +
                                    float.Parse((dtFt.Rows[0]["descargapaletizada"].ToString() == "" ? "0" : dtFt.Rows[0]["descargapaletizada"].ToString()));

                            CTeDataSet.SetCampo("vComp_232", valor.ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                            CTeDataSet.IncluirParte("Comp");
                            CTeDataSet.SetCampo("xNome_231", "Outros/DevCan");
                            valor = float.Parse((dtFt.Rows[0]["outros"].ToString() == "" ? "0" : dtFt.Rows[0]["outros"].ToString())) +
                                    float.Parse((dtFt.Rows[0]["ImpostoARecolher"].ToString() == "" ? "0" : dtFt.Rows[0]["ImpostoARecolher"].ToString()));

                            CTeDataSet.SetCampo("vComp_232", valor.ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SalvarParte("Comp");

                        }

                        string CSTCTe = Frete.RetornarCSTCTe(int.Parse(d.Rows[i]["IdDocumento"].ToString()), cnx);

                        if (CSTCTe == "CST20")//Prestação Sujeito à tributação do ICMS
                        {
                            CTeDataSet.SetCampo("CST_609", "00");
                            CTeDataSet.SetCampo("vBC_610",
                                float.Parse(dtFt.Rows[0]["BaseDeCalculo"].ToString() == "" ? "0" : dtFt.Rows[0]["BaseDeCalculo"].ToString()).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("pICMS_611",
                                float.Parse(dtFt.Rows[0]["Aliquota"].ToString() == "" ? "0" : dtFt.Rows[0]["Aliquota"].ToString()).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("vICMS_612",
                                float.Parse(dtFt.Rows[0]["ImpostoARecolher"].ToString() == "" ? "0" : dtFt.Rows[0]["ImpostoARecolher"].ToString()).ToString("#0.00").Replace(",", "."));
                        }



                        if (CSTCTe == "CST00")//Prestação Sujeito à tributação do ICMS
                        {
                            CTeDataSet.SetCampo("CST_613", "20");
                            CTeDataSet.SetCampo("pRedBC_614", "1");

                            CTeDataSet.SetCampo("vBC_615",
                                float.Parse((dtFt.Rows[0]["BaseDeCalculo"].ToString() == "" ? "0" : dtFt.Rows[0]["BaseDeCalculo"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("pICMS_616",
                                float.Parse((dtFt.Rows[0]["Aliquota"].ToString() == "" ? "0" : dtFt.Rows[0]["Aliquota"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("vICMS_617",
                                float.Parse((dtFt.Rows[0]["ImpostoARecolher"].ToString() == "" ? "0" : dtFt.Rows[0]["ImpostoARecolher"].ToString())).ToString("#0.00").Replace(",", "."));
                        }

                        if (CSTCTe == "CST45")//ICMS Isento, não tributado ou deferido
                            CTeDataSet.SetCampo("CST_618", "40");

                        if (CSTCTe == "CST80")//Responsabilidade do recolhimento do ICMS atribuido ao tomador ou 3º por ST
                        {
                            CTeDataSet.SetCampo("CST_619", "80");
                            CTeDataSet.SetCampo("vBCSTRet_620",
                                float.Parse((dtFt.Rows[0]["BaseDeCalculo"].ToString() == "" ? "0" : dtFt.Rows[0]["BaseDeCalculo"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("vICMSSTRet_621",
                                float.Parse((dtFt.Rows[0]["Aliquota"].ToString() == "" ? "0" : dtFt.Rows[0]["Aliquota"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("pICMSSTRet_622",
                                float.Parse((dtFt.Rows[0]["ImpostoARecolher"].ToString() == "" ? "0" : dtFt.Rows[0]["ImpostoARecolher"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("vCred_623", "");
                        }

                        if (CSTCTe == "CST81")//ICMS Devido à outra UF
                        {
                            CTeDataSet.SetCampo("CST_624", "81");
                            CTeDataSet.SetCampo("pRedBC_625", "");
                            CTeDataSet.SetCampo("vBC_627",
                                float.Parse((dtFt.Rows[0]["BaseDeCalculo"].ToString() == "" ? "0" : dtFt.Rows[0]["BaseDeCalculo"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("pICMS_628",
                                float.Parse((dtFt.Rows[0]["Aliquota"].ToString() == "" ? "0" : dtFt.Rows[0]["Aliquota"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("vICMS_629",
                                float.Parse((dtFt.Rows[0]["ImpostoARecolher"].ToString() == "" ? "0" : dtFt.Rows[0]["ImpostoARecolher"].ToString())).ToString("#0.00").Replace(",", "."));
                        }

                        if (CSTCTe == "CST90")//ICMS Outros
                        {
                            CTeDataSet.SetCampo("CST_630", "90");
                            CTeDataSet.SetCampo("pRedBCOutraUF_631", "");
                            CTeDataSet.SetCampo("vBCOutraUF_632",
                                float.Parse((dtFt.Rows[0]["BaseDeCalculo"].ToString() == "" ? "0" : dtFt.Rows[0]["BaseDeCalculo"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("pICMSOutraUF_633",
                                float.Parse((dtFt.Rows[0]["Aliquota"].ToString() == "" ? "0" : dtFt.Rows[0]["Aliquota"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("vICMSOutraUF_634",
                                float.Parse((dtFt.Rows[0]["ImpostoARecolher"].ToString() == "" ? "0" : dtFt.Rows[0]["ImpostoARecolher"].ToString())).ToString("#0.00").Replace(",", "."));
                            CTeDataSet.SetCampo("indSN_635", "");

                        }




                        //{OBSERVACAO}
                        CTeDataSet.SetCampo("InfAdFisco_267", "FISCO:");


                        if (d.Rows[i]["CteTipoDeCte"].ToString() != "COMPLEMENTO")
                        {

                            CTeDataSet.SetCampo("vCarga_671",
                                float.Parse(d.Rows[i]["CteTipoDeCte"].ToString() == "" ? "0" : d.Rows[i]["CteTipoDeCte"].ToString()).ToString("#0.00").Replace(",", "."));

                            if (d.Rows[i]["Natureza"].ToString().Trim() != "")
                                CTeDataSet.SetCampo("proPred_271", d.Rows[i]["Natureza"].ToString());
                            else
                                CTeDataSet.SetCampo("proPred_271", "CONF NFE");

                            CTeDataSet.SetCampo("xOutCat_272", "");

                            if (d.Rows[i]["Especie"].ToString().Trim() != "")
                                CTeDataSet.SetCampo("xOutCat_272", d.Rows[i]["Especie"].ToString());

                            CTeDataSet.IncluirParte("infQ");
                            CTeDataSet.SetCampo("cUnid_274", "01");
                            CTeDataSet.SetCampo("tpMed_275", "Peso Bruto");
                            CTeDataSet.SetCampo("qCarga_276",
                                float.Parse(d.Rows[i]["PesoBruto"].ToString() == "" ? "0" : d.Rows[i]["PesoBruto"].ToString()).ToString("#0.0000").Replace(",", "."));
                            CTeDataSet.SalvarParte("infQ");

                            CTeDataSet.IncluirParte("infQ");
                            CTeDataSet.SetCampo("cUnid_274", "02");
                            CTeDataSet.SetCampo("tpMed_275", "Peso Cubado");
                            CTeDataSet.SetCampo("qCarga_276",
                                float.Parse(d.Rows[i]["PesoCubado"].ToString() == "" ? "0" : d.Rows[i]["PesoCubado"].ToString()).ToString("#0.0000").Replace(",", "."));
                            CTeDataSet.SalvarParte("infQ");

                            CTeDataSet.IncluirParte("infQ");
                            CTeDataSet.SetCampo("cUnid_274", "03");
                            CTeDataSet.SetCampo("tpMed_275", "Volumes");
                            CTeDataSet.SetCampo("qCarga_276",
                                float.Parse(d.Rows[i]["Volumes"].ToString() == "" ? "0" : d.Rows[i]["Volumes"].ToString()).ToString("#0.0000").Replace(",", "."));
                            CTeDataSet.SalvarParte("infQ");

                            CTeDataSet.IncluirParte("infQ");
                            CTeDataSet.SetCampo("cUnid_274", "00");
                            CTeDataSet.SetCampo("tpMed_275", "Cubagem");
                            CTeDataSet.SetCampo("qCarga_276",
                                float.Parse(d.Rows[i]["MetragemCubica"].ToString() == "" ? "0" : d.Rows[i]["MetragemCubica"].ToString()).ToString("#0.0000").Replace(",", "."));
                            CTeDataSet.SalvarParte("infQ");

                        }

                        CTeDataSet.IncluirParte("emiDocAnt");
                        CTeDataSet.SetCampo("CNPJ_284", "");
                        CTeDataSet.SetCampo("CPF_285", "");
                        CTeDataSet.SetCampo("IE_286", "");
                        CTeDataSet.SetCampo("UF_287", "");
                        CTeDataSet.SetCampo("xNome_288", "");
                        CTeDataSet.SalvarParte("emiDocAnt");

                        CTeDataSet.IncluirParte("idDocAntPap");
                        CTeDataSet.SetCampo("tpDoc_291", "");
                        CTeDataSet.SetCampo("serie_292", "");
                        CTeDataSet.SetCampo("subser_293", "");
                        CTeDataSet.SetCampo("nDoc_294", "");
                        CTeDataSet.SetCampo("dEmi_295", "");
                        CTeDataSet.SalvarParte("idDocAntPap");

                        CTeDataSet.IncluirParte("idDocAntEle");
                        CTeDataSet.SetCampo("chave_297", "");
                        CTeDataSet.SalvarParte("idDocAntEle");

                        CTeDataSet.IncluirParte("seg");
                        CTeDataSet.SetCampo("respSeg_299", "0");
                        CTeDataSet.SetCampo("xSeg_300", "");
                        CTeDataSet.SetCampo("nApol_301", "");
                        CTeDataSet.SetCampo("nAver_302", "");
                        CTeDataSet.SetCampo("vCarga_675", "");
                        CTeDataSet.SalvarParte("seg");

                        if (CTe.VersaoManual == CTeX.VersaoManual.vm200)
                            CTeDataSet.SetCampo("versaoModal_636", "2.00");



                        if (CTeDataSet.GetCampo("modal_25").ToString() == "01")// Rodoviario
                        {
                            CTeDataSet.SetCampo("RNTRC_305", Sistecno.BLL.Helpers.Util.Validacoes.ZerosEsquerda(rntc, 8));
                        }

                        if (d.Rows[i]["DataPlanejada"].ToString().Length > 0 && DateTime.Parse(d.Rows[i]["DataPlanejada"].ToString()) >= DateTime.Now)
                            CTeDataSet.SetCampo("dPrev_306", DateTime.Parse(d.Rows[i]["DataPlanejada"].ToString()).ToString("yyy-MM-dd"));
                        else
                            CTeDataSet.SetCampo("dPrev_306", DateTime.Now.ToString("yyy-MM-dd"));

                        DataTable dtMot = Veiculo.RetornarVeiculo(int.Parse(d.Rows[i]["IDDocumento"].ToString()), cnx);

                        if (int.Parse((dtMot.Rows[0]["IdVeiculo"].ToString() == "" ? "0" : dtMot.Rows[0]["IdVeiculo"].ToString())) > 0)
                        {
                            CTeDataSet.SetCampo("lota_307", "1");
                            CTeDataSet.SetCampo("CIOT_1001", "");
                            CTeDataSet.IncluirParte("occ");
                            CTeDataSet.SetCampo("serie_312", "");
                            CTeDataSet.SetCampo("nOcc_313", "");
                            CTeDataSet.SetCampo("dEmi_314", "");
                            CTeDataSet.SetCampo("CNPJ_316", "");
                            CTeDataSet.SetCampo("cInt_317", "");
                            CTeDataSet.SetCampo("IE_318", "");
                            CTeDataSet.SetCampo("UF_319", "");
                            CTeDataSet.SetCampo("fone_320", "");
                            CTeDataSet.SalvarParte("occ");

                            CTeDataSet.IncluirParte("valePed");
                            CTeDataSet.SetCampo("CNPJForn_1002", "");
                            CTeDataSet.SetCampo("nCompra_1003", "");
                            CTeDataSet.SetCampo("CNPJPg_1004", "");
                            CTeDataSet.SalvarParte("valePed");

                            CTeDataSet.IncluirParte("veic");
                            CTeDataSet.SetCampo("cInt_332", dtMot.Rows[0]["IdVeiculo"].ToString());
                            CTeDataSet.SetCampo("RENAVAM_333", dtMot.Rows[0]["Renavam"].ToString());
                            CTeDataSet.SetCampo("placa_334", dtMot.Rows[0]["Placa"].ToString());
                            CTeDataSet.SetCampo("tara_335", dtMot.Rows[0]["CapacidadeDeCargaKg"].ToString());
                            CTeDataSet.SetCampo("capKG_336", dtMot.Rows[0]["CapacidadeDeCargaKg"].ToString());
                            CTeDataSet.SetCampo("capM3_337", dtMot.Rows[0]["CapacidadeDeCargaM3"].ToString());

                            if (dtMot.Rows[0]["IdCadastro"].ToString() == dtMot.Rows[0]["IdProprietario"].ToString())
                                CTeDataSet.SetCampo("tpProp_338", "P");  // Proprio
                            else
                                CTeDataSet.SetCampo("tpProp_338", "T");  // Terceiro



                            string vTipoDeRodado, vTracaoReboque, vTipoDeCarroceria;

                            if (dtMot.Rows[0]["TipoDeRodado"].ToString().Trim().Length < 2 || dtMot.Rows[0]["TipoDeRodado"].ToString().Trim().Substring(0, 2) == "")
                                vTipoDeRodado = "0";
                            else
                                vTipoDeRodado = dtMot.Rows[0]["TipoDeRodado"].ToString().Substring(0, 2);


                            if (dtMot.Rows[0]["TracaoReboque"].ToString().Trim().Length < 2 || dtMot.Rows[0]["TracaoReboque"].ToString().Trim().Substring(0, 2) == "")
                                vTracaoReboque = "00";
                            else
                                vTracaoReboque = dtMot.Rows[0]["TracaoReboque"].ToString().Substring(0, 2);

                            if (dtMot.Rows[0]["TipoDeCarroceria"].ToString().Trim().Length < 2 || dtMot.Rows[0]["TipoDeCarroceria"].ToString().Trim().Substring(0, 2) == "")
                                vTipoDeCarroceria = "00";
                            else
                                vTipoDeCarroceria = dtMot.Rows[0]["TipoDeCarroceria"].ToString().Substring(0, 2);

                            CTeDataSet.SetCampo("tpVeic_339", vTracaoReboque); // 0=Tracao 1=Reboque
                            CTeDataSet.SetCampo("tpRod_340", vTipoDeRodado);
                            CTeDataSet.SetCampo("tpCar_341", vTipoDeCarroceria);
                            CTeDataSet.SetCampo("UF_342", dtMot.Rows[0]["UF"].ToString());

                            if (Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["CnpjCpfProprietario"].ToString()).Trim().Length > 13)
                            {
                                CTeDataSet.SetCampo("CPF_344", "");
                                CTeDataSet.SetCampo("CNPJ_345", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["CnpjCpfProprietario"].ToString()));
                            }
                            else
                            {
                                CTeDataSet.SetCampo("CPF_344", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["CnpjCpfProprietario"].ToString()));
                                CTeDataSet.SetCampo("CNPJ_345", "");
                            }

                            CTeDataSet.SetCampo("RNTRC_346", dtMot.Rows[0]["ANTT"].ToString());
                            CTeDataSet.SetCampo("xNome_347", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["Proprietario"].ToString()));
                            CTeDataSet.SetCampo("IE_348", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["InscricaoRG"].ToString()));
                            CTeDataSet.SetCampo("UF_349", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["ufp"].ToString()));
                            CTeDataSet.SetCampo("tpProp_350", "0");
                            CTeDataSet.SalvarParte("veic");


                            CTeDataSet.IncluirParte("lacRodo");
                            CTeDataSet.SetCampo("nLacre_352", "000");
                            CTeDataSet.SalvarParte("lacRodo");

                            //{campo para lotação}
                            CTeDataSet.IncluirParte("moto");
                            CTeDataSet.SetCampo("xNome_354", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["Motorista"].ToString()));
                            CTeDataSet.SetCampo("CPF_355", Sistecno.BLL.Helpers.Util.Validacoes.RemoverCarEsp(dtMot.Rows[0]["CnpjCpfMotorista"].ToString()));
                            CTeDataSet.SalvarParte("moto");

                        }
                        else // Veiculo 0
                        {
                            // {campo para lotação}
                            CTeDataSet.SetCampo("lota_307", "0");
                            CTeDataSet.SetCampo("CIOT_1001", "");

                            CTeDataSet.IncluirParte("occ");
                            CTeDataSet.SetCampo("serie_312", "");
                            CTeDataSet.SetCampo("nOcc_313", "");
                            CTeDataSet.SetCampo("dEmi_314", "");
                            CTeDataSet.SetCampo("CNPJ_316", "");
                            CTeDataSet.SetCampo("cInt_317", "");
                            CTeDataSet.SetCampo("IE_318", "");
                            CTeDataSet.SetCampo("UF_319", "");
                            CTeDataSet.SetCampo("fone_320", "");
                            CTeDataSet.SalvarParte("occ");

                            CTeDataSet.IncluirParte("valePed");
                            CTeDataSet.SetCampo("CNPJForn_1002", "");
                            CTeDataSet.SetCampo("nCompra_1003", "");
                            CTeDataSet.SetCampo("CNPJPg_1004", "");
                            CTeDataSet.SalvarParte("valePed");

                            CTeDataSet.IncluirParte("veic");
                            CTeDataSet.SetCampo("cInt_332", "");
                            CTeDataSet.SetCampo("RENAVAM_333", "");
                            CTeDataSet.SetCampo("placa_334", "");
                            CTeDataSet.SetCampo("tara_335", "");
                            CTeDataSet.SetCampo("capKG_336", "");
                            CTeDataSet.SetCampo("capM3_337", "");

                            CTeDataSet.SetCampo("tpProp_338", "");
                            CTeDataSet.SetCampo("tpVeic_339", "");
                            CTeDataSet.SetCampo("tpRod_340", "");
                            CTeDataSet.SetCampo("tpCar_341", "");
                            CTeDataSet.SetCampo("UF_342", "");
                            CTeDataSet.SetCampo("CPF_344", "");
                            CTeDataSet.SetCampo("CNPJ_345", "");
                            CTeDataSet.SetCampo("RNTRC_346", "");
                            CTeDataSet.SetCampo("xNome_347", "");
                            CTeDataSet.SetCampo("IE_348", "");
                            CTeDataSet.SetCampo("UF_349", "");
                            CTeDataSet.SetCampo("tpProp_350", "");
                            CTeDataSet.SalvarParte("veic");

                            CTeDataSet.IncluirParte("lacRodo");
                            CTeDataSet.SetCampo("nLacre_352", "");
                            CTeDataSet.SalvarParte("lacRodo");

                            //{campo para lotação}
                            CTeDataSet.IncluirParte("moto");
                            CTeDataSet.SetCampo("xNome_354", "");
                            CTeDataSet.SetCampo("CPF_355", "");
                            CTeDataSet.SalvarParte("moto");
                        }

                        #region Modal
                        //aerio
                        if (CTeDataSet.GetCampo("modal_25").ToString() == "02")
                        {
                            CTeDataSet.SetCampo("nMinu_357", "");
                            CTeDataSet.SetCampo("nOCA_358", "");
                            CTeDataSet.SetCampo("dPrev_359", "");
                            CTeDataSet.SetCampo("xLAgEmi_360", "");
                            CTeDataSet.SetCampo("IdT_1104", "");
                            CTeDataSet.SetCampo("CL_364", "");
                            CTeDataSet.SetCampo("cTar_365", "");
                            CTeDataSet.SetCampo("vTar_366", "");

                            CTeDataSet.SetCampo("xDime_1101", "");
                            CTeDataSet.IncluirParte("cInfManu");
                            CTeDataSet.SetCampo("cInfManu_1102", "");
                            CTeDataSet.SalvarParte("cInfManu");

                            CTeDataSet.IncluirParte("cIMP");
                            CTeDataSet.SetCampo("cIMP_1103", "");
                            CTeDataSet.SalvarParte("cIMP");

                        }

                        //Aquaviario
                        if (CTeDataSet.GetCampo("modal_25").ToString() == "03")
                        {
                            CTeDataSet.SetCampo("vPrest_368", "10.00");
                            CTeDataSet.SetCampo("vAFRMM_369", "1.00");
                            CTeDataSet.SetCampo("nBooking_370", "123456");
                            CTeDataSet.SetCampo("nCtrl_371", "123456");
                            CTeDataSet.SetCampo("xNavio_372", "TesteNav");
                            CTeDataSet.SetCampo("nViag_373", "12");
                            CTeDataSet.SetCampo("direc_374", "N");
                            CTeDataSet.SetCampo("prtEmb_375", "testeinicio");
                            CTeDataSet.SetCampo("prtTrans_376", "testefim");
                            CTeDataSet.SetCampo("prtDest_377", "testedest");
                            CTeDataSet.SetCampo("tpNav_378", "1");
                            CTeDataSet.SetCampo("irin_379", "testeirin");

                            CTeDataSet.IncluirParte("balsa");
                            CTeDataSet.SetCampo("xBalsa_1201", "identidabalsa");
                            CTeDataSet.SalvarParte("balsa");

                            //--- Trecho adicionado na NT 2011.003 ----
                            CTeDataSet.IncluirParte("detCont");
                            CTeDataSet.SetCampo("nCont_1202')", "1");
                            CTeDataSet.SalvarParte("detCont");

                            CTeDataSet.IncluirParte("lacre");
                            CTeDataSet.SetCampo("nLacre_381", "1");
                            CTeDataSet.SalvarParte("lacre");

                            CTeDataSet.IncluirParte("Aquav.infNF");
                            CTeDataSet.SetCampo("serie_1203", "123");
                            CTeDataSet.SetCampo("nDoc_1204", "123");
                            CTeDataSet.SetCampo("unidRat_1205", "100.00");
                            CTeDataSet.SalvarParte("Aquav.infNF");
                        }


                        if (CTeDataSet.GetCampo("modal_25").ToString() == "04") // ferroviario
                        {
                            CTeDataSet.SetCampo("fluxo_384", "Teste");
                            CTeDataSet.SetCampo("idTrem_385", "");
                            CTeDataSet.SetCampo("vFrete_386", "1.00");
                            CTeDataSet.SetCampo("CNPJ_388", "00000000000000");
                            CTeDataSet.SetCampo("cInt_389", "");
                            CTeDataSet.SetCampo("IE_390", "");
                            CTeDataSet.SetCampo("xNome_391", "Teste");
                            CTeDataSet.SetCampo("xLgr_393", "Rua teste");
                            CTeDataSet.SetCampo("nro_394", "1");
                            CTeDataSet.SetCampo("xCpl_395", "");
                            CTeDataSet.SetCampo("xBairro_396", "");
                            CTeDataSet.SetCampo("cMun_397", "4302105");
                            CTeDataSet.SetCampo("xMun_398", "Bento Goncalves");
                            CTeDataSet.SetCampo("CEP_399", "00000000");
                            CTeDataSet.SetCampo("UF_400", "RS");


                            CTeDataSet.IncluirParte("detVag");
                            CTeDataSet.SetCampo("nVag_424", "12345678");
                            CTeDataSet.SetCampo("cap_425", "");
                            CTeDataSet.SetCampo("tpVag_426", "123");
                            CTeDataSet.SetCampo("pesoR_427", "123.00");
                            CTeDataSet.SetCampo("pesoBC_428", "123.00");
                            CTeDataSet.SalvarParte("detVag");

                            CTeDataSet.IncluirParte("lacDetVag");
                            CTeDataSet.SetCampo("nLacre_430", "1234");
                            CTeDataSet.SalvarParte("lacDetVag");

                            CTeDataSet.IncluirParte("contVag");
                            CTeDataSet.SetCampo("nCont_432", "1234");
                            CTeDataSet.SetCampo("dPrev_433", "");
                            CTeDataSet.SalvarParte("contVag");

                            //ratNF e ratNFe fazem Choice, por isso deve ocorrer um ou outro...
                            CTeDataSet.IncluirParte("ratNF");
                            CTeDataSet.SetCampo("serie_1303", "123");
                            CTeDataSet.SetCampo("nDoc_1304", "12345678");
                            CTeDataSet.SetCampo("pesoRat_1305", "123.00");
                            CTeDataSet.SalvarParte("ratNF");

                        }


                        if (CTeDataSet.GetCampo("modal_25").ToString() == "05") // dutoviario
                        {
                            CTeDataSet.SetCampo("vTar_435", "1.000000");
                            CTeDataSet.SetCampo("dIni_1401", "2013-01-01");
                            CTeDataSet.SetCampo("dFim_1402", "2013-01-01");

                            CTeDataSet.IncluirParte("peri");
                            CTeDataSet.SetCampo("nONU_437", "");
                            CTeDataSet.SetCampo("xNomeAE_438", "");
                            CTeDataSet.SetCampo("xClaRisco_439", "");
                            CTeDataSet.SetCampo("grEmb_440", "");
                            CTeDataSet.SetCampo("qTotProd_441", "");
                            CTeDataSet.SetCampo("qVolTipo_442", "");
                            CTeDataSet.SetCampo("pontoFulgor_443", "");
                            CTeDataSet.SalvarParte("peri");

                            CTeDataSet.IncluirParte("veicNovos");
                            CTeDataSet.SetCampo("chassi_445", "");
                            CTeDataSet.SetCampo("cCor_446", "");
                            CTeDataSet.SetCampo("xCor_447", "");
                            CTeDataSet.SetCampo("cMod_448", "");
                            CTeDataSet.SetCampo("vUnit_449", "");
                            CTeDataSet.SetCampo("vFrete_450", "");
                            CTeDataSet.SalvarParte("veicNovos");

                            CTeDataSet.SetCampo("nFat_637", "");
                            CTeDataSet.SetCampo("vOrig_638", "");
                            CTeDataSet.SetCampo("vDesc_639", "");
                            CTeDataSet.SetCampo("vLiq_640", "");

                            CTeDataSet.IncluirParte("dup");
                            CTeDataSet.SetCampo("nDup_641", "");
                            CTeDataSet.SetCampo("dVenc_642", "");
                            CTeDataSet.SetCampo("vDup_643", "");
                            CTeDataSet.SalvarParte("dup");

                            CTeDataSet.SetCampo("chCte_452", "");
                            CTeDataSet.SetCampo("refNFe_454", "");
                            CTeDataSet.SetCampo("CNPJ_456", "");
                            CTeDataSet.SetCampo("mod_457", "");
                            CTeDataSet.SetCampo("serie_458", "");
                            CTeDataSet.SetCampo("subserie_459", "");
                            CTeDataSet.SetCampo("nro_460", "");
                            CTeDataSet.SetCampo("valor_461", "");
                            CTeDataSet.SetCampo("dEmi_462", "");
                            CTeDataSet.SetCampo("refCte_463", "");
                            CTeDataSet.SetCampo("refCteAnu_465", "");

                        }

                        #endregion

                        #region Complemento

                        if (d.Rows[i]["CteTipoDeCte"].ToString() == "COMPLEMENTO")
                            CTeDataSet.SetCampo("chave_467", CteComplemento.RetornarComplemento(int.Parse(d.Rows[i]["IDDocumetno"].ToString()), cnx).Rows[0]["IdNota"].ToString());



                        #endregion

                        CTeDataSet.SetCampo("chCte_509", "");
                        CTeDataSet.SetCampo("dEmi_510", "");
                        CTeDataSet.Salvar();

                    }

                }//loop


                xmlLote = CTeDataSet.LoteCTe();
                GravarDocumentoEletronico(lNumeroNfs, IdLoteEletronico, cnx, xmlLote);
                string assinatura = CTe.AssinarCT(ref xmlLote);
                string xml_envio = CTe.EnviarCT(IdLoteEletronico, assinatura);
                string xCaminho = CTe.UltimoLogEnvio;
                string text = System.IO.File.ReadAllText(xCaminho);

                LoteEletronico.AtuliazarNomeXmlLote(IdLoteEletronico, text, Path.GetFileNameWithoutExtension(xCaminho) + ".xml", cnx);

                string edtRecibo = CTe.ExtrairRecibo(xml_envio);
                ProcessarXML(xml_envio, IdLoteEletronico, chave, edtRecibo, idfilial, cnx);
                ConsultarRecibo(edtRecibo, CTe, IdLoteEletronico, chave, edtRecibo, idfilial, cnx);

                return int.Parse(IdLoteEletronico);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void GravarDocumentoEletronico(List<NumeroChave> Documentos, string IdLoteEletronico, string cnx, string xmlLote)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlLote);

            for (int i = 0; i < Documentos.Count; i++)
            {
                string chave = xDoc.GetElementsByTagName("infCte").Item(i).Attributes[0].Value.Replace("CTe", "");
                string nCte = xDoc.GetElementsByTagName("nCT").Item(i).InnerText;

                if (nCte == Documentos[i].numero)
                {
                    DocumentoEletronico.CriarDocumentoEletronico(Documentos[i].IdDocumento, "LOTE GERADO", int.Parse(IdLoteEletronico), chave, "", cnx);
                }
            }
        }

        public static void ConsultarRecibo(string edtRecibo, CTeX.spdCTeX CTe, string IdLoteEletronico, string chaveCte, string Recibo, int idfilial, string cnx)
        {
            try
            {
                string xmlRec = CTe.ConsultarRecibo(edtRecibo);
                ProcessarXML(xmlRec, IdLoteEletronico, chaveCte, Recibo, idfilial, cnx);
            }
            catch (Exception)
            { }
        }

        private static void ProcessarXML(string xlms, string IdLoteEletronico, string chaveCte, string Recibo, int IdFilial, string cnx)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xlms);
            string c_status = ""; // codigo do status
            string status = ""; // descrição do status


            try
            {
                if (xDoc.GetElementsByTagName("infProt").Count == 0) // nao vem os dados doLote e conhecimentos
                {
                    c_status = xDoc.GetElementsByTagName("cStat").Item(0).InnerText;
                    status = xDoc.GetElementsByTagName("xMotivo").Item(0).InnerText;
                    LoteEletronico.AtuliazarLote(IdLoteEletronico, Recibo, c_status, status, xlms, cnx, true);
                }
                else
                {
                    //informações do Lote
                    c_status = xDoc.GetElementsByTagName("cStat").Item(0).InnerText;
                    status = xDoc.GetElementsByTagName("xMotivo").Item(0).InnerText;

                    LoteEletronico.AtuliazarLote(IdLoteEletronico, Recibo, c_status, status, xlms, cnx, false);
                    for (int i = 0; i < xDoc.GetElementsByTagName("infProt").Count; i++)
                    {
                        //informacoes do conhecimento
                        string chave = xDoc.GetElementsByTagName("chCTe").Item(i).InnerText;
                        string protocolo = "";
                        c_status = xDoc.GetElementsByTagName("cStat").Item(i + 1).InnerText;
                        status = xDoc.GetElementsByTagName("xMotivo").Item(i + 1).InnerText;

                        if (xDoc.GetElementsByTagName("nProt").Item(i) != null)
                        {
                            protocolo = xDoc.GetElementsByTagName("nProt").Item(i).InnerText;
                        }

                        LoteEletronico.AtuliazarDoCumentoEletronico(chave, Recibo, c_status, status, xlms, protocolo, IdFilial, cnx);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            //LoteEletronico -- Primeira tag
            //DocumentoEletronico --- inf
        }

        public static class FuncoesCte
        {
            public static int Numerar(string TipoDocumento, int idFilial, bool AlterarDoc, int iddocumento, string serie, string cnx)
            {
                try
                {
                    string ssql = " UPDATE NUMERADOR SET PROXIMONUMERO=PROXIMONUMERO+1  WHERE IDFILIAL=" + idFilial + " AND NOME='" + TipoDocumento + "' ";

                    ssql += "SELECT (ProximoNumero-1) FROM NUMERADOR WHERE IDFILIAL=" + idFilial + " AND NOME='" + TipoDocumento + "'";
                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand(ssql, c);
                    int ret = (int)comm.ExecuteScalar();

                    if (AlterarDoc)
                    {
                        ssql = "UPDATE DOCUMENTO SET NUMERO='" + ret + "' , Serie ='" + serie + "', NumeroDocumentoEletronico= " + ret + " WHERE IDDOCUMENTO=" + iddocumento;
                        comm = new SqlCommand(ssql, c);
                        comm.ExecuteNonQuery();
                    }
                    c.Close();
                    return ret;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static class LoteEletronico
        {
            public static int CriarLote(int idFilial, string Remetente, string cnx)
            {
                try
                {
                    System.Text.StringBuilder sql = new StringBuilder();
                    string id = DAL.BD.cDb.RetornarIDTabela(cnx, "LOTEELETRONICO").ToString();

                    sql.Append("Insert into LOTEELETRONICO (IdLoteEletronico, Descricao, IdFilial) values (@IdLoteEletronico, '@Descricao', @IdFilial)");
                    sql.Replace("@IdLoteEletronico", id);
                    sql.Replace("@Descricao", Remetente);
                    sql.Replace("@IdFilial", idFilial.ToString());
                    //sql.Append(" ; ");

                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand(sql.ToString(), c);
                    comm.ExecuteScalar();
                    c.Close();
                    return int.Parse(id);
                }
                catch (Exception EX)
                {
                    throw EX;
                }
            }

            /// <summary>
            /// atualiza o documento eletronico, documento filial e documento a faturar
            /// </summary>
            /// <param name="idNota"></param>
            /// <param name="recibo"></param>
            /// <param name="cstatus"></param>
            /// <param name="status"></param>
            /// <param name="xml"></param>
            /// <param name="protocolo"></param>
            /// <param name="idFilial"></param>
            /// <param name="cnx"></param>
            public static void AtuliazarDoCumentoEletronico(string idNota, string recibo, string cstatus, string status, string xml, string protocolo, int idFilial, string cnx)
            {
                try
                {
                    string sql = "";

                    sql = "UPDATE DOCUMENTOELETRONICO SET ";
                    sql += "NumeroRecibo='" + recibo + "', ";
                    sql += "Status='" + (status.Length > 50 ? status.Substring(0, 49) : status) + "', ";
                    sql += "UltimoArquivoXml=@xml, ";
                    sql += "CStatus='" + (cstatus.Length > 50 ? cstatus.Substring(0, 49) : cstatus) + "', ";
                    sql += " StatusCompleto='" + status + "', ";
                    sql += " NumeroProtocolo='" + protocolo + "' ";
                    sql += " where IdNota ='" + idNota + "'; ";

                    if (cstatus == "100")
                    {
                        //acerta o enviado na documento Eletronico
                        sql += " Update Documento set Enviado='SIM' WHERE IDDOCUMENTO IN(SELECT IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + idNota + "') ";

                        //acerta a documentoFilial
                        sql += " IF NOT EXISTS(SELECT IDDOCUMENTO FROM DOCUMENTOFILIAL WHERE IDDOCUMENTO IN(SELECT IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + idNota + "')) ";
                        sql += " INSERT INTO DOCUMENTOFILIAL (IDDOCUMENTOFILIAL, IDDOCUMENTO, IDFILIAL,IDREGIAOITEM, SITUACAO) VALUES (" + Sistecno.DAL.BD.cDb.RetornarIDTabela(cnx, "DOCUMENTOFILIAL").ToString() + ", (SELECT IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + idNota + "'), " + idFilial + ",-1, 'AGUARDANDO EMBARQUE')  ";
                        sql += " ELSE ";
                        sql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO EMBARQUE' WHERE IDDOCUMENTO IN(SELECT IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + idNota + "')";

                        sql += " ; ";
                        //acerta a documento a faturar
                        sql += " IF NOT EXISTS(SELECT IDDOCUMENTO FROM DOCUMENTOAFATURAR WHERE IDDOCUMENTO IN(SELECT IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + idNota + "'))";
                        sql += " INSERT INTO DOCUMENTOAFATURAR (IDDOCUMENTOAFATURAR, IDDOCUMENTO, IDFILIAL, TIPODEFATURA) VALUES (" + Sistecno.DAL.BD.cDb.RetornarIDTabela(cnx, "DOCUMENTOAFATURAR").ToString() + ",  (SELECT IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + idNota + "'), " + idFilial + ", 'FRETE')";
                    }
                    else
                        sql += " Update Documento set Enviado='NAO' WHERE IDDOCUMENTO IN(SELECT IDDOCUMENTO FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + idNota + "') ";

                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand(sql.ToString(), c);
                    SqlParameter par = new SqlParameter();
                    par.ParameterName = "@xml";
                    par.Value = xml;
                    comm.Parameters.Add(par);
                    comm.ExecuteScalar();
                    c.Close();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            public static void AtuliazarNomeXmlLote(string idLoteEletronico, string xml, string nomeArq, string cnx)
            {
                string sql = "";

                sql += " UPDATE LOTEELETRONICO SET ";
                sql += " EnvLot='" + nomeArq + "', ";
                sql += " EnvLotXML=@xml ";
                sql += " where idloteEletronico =" + idLoteEletronico;

                SqlConnection c = new SqlConnection(cnx);
                c.Open();

                SqlCommand comm = new SqlCommand(sql.ToString(), c);
                SqlParameter par = new SqlParameter();
                par.ParameterName = "@xml";
                par.Value = xml;
                comm.Parameters.Add(par);
                comm.ExecuteScalar();
                c.Close();
            }

            public static void AtualizarDtEletronivoEvento(int idDtEletronico, string codigoEvento, string evento, DateTime dhEvento, string cnx)
            {
                int id = Sistecno.DAL.BD.cDb.RetornarIDTabela(cnx, "DTELETRONICOEVENTO");
                string ssql = "INSERT INTO DTELETRONICOEVENTO ( IdDtELetronicoEvento, IdDtEletronico, CodigoEvento, Evento,DataHora) VALUES (" + id + ", " + idDtEletronico + ", '" + codigoEvento + "', '" + evento + "',getDate())";
                SqlConnection c = new SqlConnection(cnx);
                c.Open();
                SqlCommand comm = new SqlCommand(ssql, c);
                comm.ExecuteScalar();
                c.Close();
            }

            public static void AtuliazarLote(string idLoteEletronico, string recibo, string cstatus, string status, string xml, string cnx, bool AtualizarDocumentoEletronico)
            {
                string sql = "";


                sql += " UPDATE LOTEELETRONICO SET ";
                sql += " Recibo='" + recibo + "', ";
                sql += " CStatus='" + cstatus + "', ";
                sql += " Status='" + status + "', ";
                sql += " xml=@xml";
                sql += " where idloteEletronico =" + idLoteEletronico;


                SqlConnection c = new SqlConnection(cnx);
                c.Open();

                SqlCommand comm = new SqlCommand(sql.ToString(), c);
                SqlParameter par = new SqlParameter();
                par.ParameterName = "@xml";
                par.Value = xml;
                comm.Parameters.Add(par);
                comm.ExecuteScalar();


                if (AtualizarDocumentoEletronico)
                {
                    sql = "UPDATE DOCUMENTOELETRONICO SET ";
                    sql += "NumeroRecibo='" + recibo + "', ";
                    sql += "Status='" + status + "', ";
                    sql += "UltimoArquivoXml=@xml, ";
                    sql += "CStatus='" + cstatus + "', ";
                    sql += " StatusCompleto='" + status + "' ";
                    sql += " where idloteEletronico =" + idLoteEletronico;

                    par = new SqlParameter();
                    par.ParameterName = "@xml";
                    par.Value = xml;
                    comm = new SqlCommand(sql.ToString(), c);
                    comm.Parameters.Add(par);
                    comm.ExecuteScalar();
                }
                c.Close();

            }
        }

        public static class DocumentoEletronico
        {
            public static int CriarDocumentoEletronico(int IdDocumento, string status, int IdLoteEletronico, string chave, string recibo, string cnx)
            {
                try
                {
                    string id = "0";
                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand("SELECT count(*) FROM DOCUMENTOELETRONICO WHERE IDDOCUMENTO = " + IdDocumento, c);
                    int e = int.Parse(comm.ExecuteScalar().ToString());

                    if (e == 0)
                    {
                        System.Text.StringBuilder sql = new StringBuilder();
                        id = Sistecno.DAL.BD.cDb.RetornarIDTabela(cnx, "DOCUMENTOELETRONICO").ToString();
                        sql.Append("Insert into DOCUMENTOELETRONICO (IdDocumentoEletronico,  IdDocumento, Status, IdLoteEletronico, IdNota, NumeroRecibo) values (@IdDocumentoEletronico,  @IdDocumento, '@Status', @IdLoteEletronico , '@IdNota', '@NumeroRecibo')");
                        sql.Replace("@IdDocumentoEletronico", id);
                        sql.Replace("@IdDocumento", IdDocumento.ToString());
                        sql.Replace("@Status", status);
                        sql.Replace("@IdLoteEletronico", IdLoteEletronico.ToString());
                        sql.Replace("@IdNota", chave);
                        sql.Replace("@NumeroRecibo", recibo);
                        comm = new SqlCommand(sql.ToString(), c);
                        comm.ExecuteNonQuery();
                    }
                    c.Close();
                    return int.Parse(id);
                }
                catch (Exception EX)
                {
                    throw EX;
                }
            }
        }

        public static class Frete
        {
            public static string RetornarCSTCTe(int iddocumento, string cnx)
            {
                string sql = " Select   TC.CSTCTe  From Documento Doc  with (Nolock) left Join TesCfop TC on (TC.IdTesCFOP=Doc.IDTesCFOP)  Where Doc.IdDocumento = " + iddocumento;
                SqlConnection c = new SqlConnection(cnx);
                c.Open();
                SqlCommand comm = new SqlCommand(sql.ToString(), c);
                string ret = comm.ExecuteScalar().ToString();
                c.Close();
                return ret;
            }

            public static DataTable RetornarFrete(int IdDocumento, string cnx)
            {
                try
                {
                    string ssql = "Select * From DocumentoFrete  with (Nolock) where IDDocumento =" + IdDocumento;


                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand(ssql.ToString(), c);
                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    c.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public static class CteComplemento
        {
            public static DataTable RetornarComplemento(int idDocumento, string cnx)
            {
                try
                {
                    string ssql = "";
                    ssql += " Select Top 1 De.IdNota ,DF.BaseDeCalculo,DF.Frete, DF.Aliquota, DF.ImpostoARecolher ";
                    ssql += " From Documento Cte  with (Nolock) ";
                    ssql += " Inner Join DocumentoRelacionado DR on (DR.IdDocumentoPai = Cte.IDDocumento)  ";
                    ssql += " Inner Join Documento CteRel on (CteRel.IDDocumento = DR.IdDocumentoFilho and CteRel.TipoDeDocumento = 'CONHECIMENTO')  ";
                    ssql += " Inner Join DOcumentoEletronico DE on (DE.IdDocumento = CteRel.IdDocumento and DE.Cstatus = '100')  ";
                    ssql += " Inner Join DocumentoFrete DF on (DF.IDDocumento = CteRel.IDDocumento and DF.Proprietario = 'CLIENTE') ";
                    ssql += " where Cte.IDDocumento = " + idDocumento;

                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand(ssql.ToString(), c);
                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    c.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static class Veiculo
        {
            public static DataTable RetornarVeiculo(int iddocumento, string cnx)
            {
                try
                {
                    string ssql = "";
                    ssql += " Select    Doc.IdVeiculo,   V.Renavam,    V.Placa,    ISNULL(V.CapacidadeDeCargaKg,0)CapacidadeDeCargaKg,    ISNULL(V.CapacidadeDeCargaM3,0)CapacidadeDeCargaM3,    F.IdCadastro,  ";
                    ssql += " V.IdProprietario,    V.IdVeiculoTipo,    V.TipoDeCarroceria,    Est.Uf,    P.CnpjCpf CnpjCpfProprietario,    V.ANTT,    P.RazaoSocialNome Proprietario,    P.InscricaoRG,    EstP.Uf UfP,  ";
                    ssql += " M.RazaoSocialNome Motorista,    M.CnpjCpf CnpjCpfMotorista,    VT.TipoDeRodado, VT.TracaoReboque   ";
                    ssql += " From Documento Doc  with (Nolock)  ";
                    ssql += " Inner Join Filial F on (Doc.IdFilial = F.IdFilial)  ";
                    ssql += " Left Join Veiculo V on (V.IdVeiculo = Doc.IdVeiculo)  ";
                    ssql += " Left Join VeiculoTipo VT on (VT.IdVeiculoTipo = V.IdVeiculoTipo)  ";
                    ssql += " Left Join Cidade Cid on (Cid.IdCidade = V.IdCidade)  ";
                    ssql += " Left Join Estado Est on (Est.IdEstado = Cid.IdEstado)  ";
                    ssql += " Left Join Cadastro P on (V.IdProprietario = P.IdCadastro) ";
                    ssql += " Left Join Cidade CidP on (CidP.IdCidade = P.IdCidade)  ";
                    ssql += " Left Join Estado EstP on (EstP.IdEstado = CidP.IdEstado)  ";
                    ssql += " Left Join Cadastro M on (V.IdMotorista = M.IdCadastro)  ";
                    ssql += " where Doc.IdDocumento =" + iddocumento;

                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand(ssql.ToString(), c);
                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    c.Close();
                    return dt;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static class DocumentoRelacionado
        {
            public static DataTable RetornarNotasCte(int IdDocumentoPai, string cnx)
            {
                try
                {
                    string ssql = "";
                    ssql += " Select Doc.Numero NF,Doc.Serie, Doc.DataDeEmissao,  ";
                    ssql += " Case When Doc.BaseDoIcms=null Then 0 Else Doc.BaseDoIcms End BaseDoIcms,  ";
                    ssql += " Case When Doc.ValorDoIcms=null Then 0 Else Doc.ValorDoIcms End ValorDoIcms,  ";
                    ssql += " Case When Doc.BaseDoIcmsSubst=null Then 0 Else Doc.BaseDoIcmsSubst End BaseDoIcmsSubst,  ";
                    ssql += " Case When Doc.ValorDoIcmsSubst=null Then 0 Else Doc.ValorDoIcmsSubst End ValorDoIcmsSubst,  ";
                    ssql += " Doc.ValorDaNota, Doc.ValorDasMercadorias,  ";
                    ssql += " Doc.PesoBruto, Doc.ClasseCfop,  ";
                    ssql += " ( Select  ";
                    ssql += " Top 1 Cfop.Codigo  ";
                    ssql += " From DocumentoCfop DocCfop  ";
                    ssql += " Inner Join Cfop Cfop on (Cfop.IdCfop=DocCfop.IdCfop)  ";
                    ssql += " where DocCfop.IdDocumento = DocRel.IdDocumentoPai ) Cfop,  ";
                    ssql += " Doc.DocumentoDoCliente4 Chaveexterna,  ";
                    ssql += " (Select Top 1 IdNota From DocumentoEletronico DocE Where DocE.Status='NF AUTORIZADA PARA USO' ";
                    ssql += " and DocE.IdDocumento=Doc.IdDocumento) ChaveDeAcesso  ";
                    ssql += " From DocumentoRelacionado DocRel  with (Nolock)  ";
                    ssql += " Inner Join Documento Doc on (Doc.IdDocumento = DocRel.IdDocumentoFilho and Doc.TipoDeDocumento = 'NOTA FISCAL' )  ";
                    ssql += " where DocRel.IdDocumentoPai =  " + IdDocumentoPai;


                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand(ssql.ToString(), c);
                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    c.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        public static class FuncoesMDFe
        {

            public static void GravarDtEletronico(int iddt, string chave, string status, string cStatus, string xml, string recibo, string protocolo, string idLoteEletronico, string idUsuario, string statusLote, string cnx)
            {

                try
                {
                    string id = "0";
                    SqlConnection c = new SqlConnection(cnx);
                    c.Open();
                    SqlCommand comm = new SqlCommand("SELECT count(*) FROM DTELETRONICO WHERE IDDT = " + iddt, c);
                    string e = comm.ExecuteScalar().ToString();


                    System.Text.StringBuilder sql = new StringBuilder();

                    if (e == "0")
                    {
                        id = Sistecno.DAL.BD.cDb.RetornarIDTabela(cnx, "DTELETRONICO").ToString();
                        sql.Append("Insert into DTELETRONICO (IddtEletronico,  IdDt, Status, IdLoteEletronico, NumeroRecibo, Chave, cStatus, UltimoArquivoXML, NumeroProtocolo) values (@IdDtEletronico,  @IdDt, '@Status', @IdLoteEletronico, '@NumeroRecibo', '@Chave', '@cStatus', '@UltimoArquivoXML', '@NumeroProtocolo')");
                    }
                    else
                        sql.Append("UPDATE DTELETRONICO  SET Status='@Status',  NumeroRecibo='@NumeroRecibo', cStatus='@cStatus', UltimoArquivoXML='@UltimoArquivoXML', " + (protocolo == "" ? "NumeroProtocolo=NumeroProtocolo" : "NumeroProtocolo=" + protocolo) + " WHERE IDDT= " + iddt.ToString());

                    sql.Replace("@IdDtEletronico", id);
                    sql.Replace("@IdDt", iddt.ToString());
                    sql.Replace("@Status", status);
                    sql.Replace("@IdLoteEletronico", idLoteEletronico.ToString());
                    sql.Replace("@Chave", chave);
                    sql.Replace("@NumeroRecibo", recibo);
                    sql.Replace("@cStatus", cStatus);
                    sql.Replace("@UltimoArquivoXML", xml);
                    sql.Replace("@NumeroProtocolo", protocolo);

                    comm = new SqlCommand(sql.ToString(), c);
                    comm.ExecuteNonQuery();
                    c.Close();
                    //atualiza a Lote Eletronico

                    if (idLoteEletronico == "")
                        idLoteEletronico = RetornarIdLoteEletronico(iddt, cnx).Rows[0]["IDLOTEELETRONICO"].ToString();

                    LoteEletronico.AtuliazarLote(idLoteEletronico, "rec", cStatus, statusLote, xml, cnx, false);
                }
                catch (Exception EX)
                {
                    throw EX;
                }

            }

            public static DataTable RetornarIdLoteEletronico(int iddt, string cnx)
            {
                string strsql = "SELECT * FROM DTELETRONICO WHERE IDDT = " + iddt;
                return Sistecno.DAL.BD.cDb.RetornarDataTable(strsql, cnx);
            }




            public static DataTable RetornarDtParaEnvio(int idDT, string cnx)
            {
                string ssql = "Select  D.IDDT, D.Numero, D.Lacres, '' CIOT, CdOrig.CodigoDoIbge OrigCodigoDoIbge, CdDest.CodigoDoIbge DestCodigoDoIbge, OriEst.UF OriEst, ";
                ssql += " DesEst.UF DesEst, CdDest.Nome CdDestMunicipio, CdOrig.CodigoDoIbge OrigCodigoDoIbge, CdDest.CodigoDoIbge DestCodigoDoIbge, CdOrig.CodigoDoIBGE CarregamentoCodigoDoIBGE,  ";
                ssql += " CdOrig.Nome CarregamentoMunicipio,CDFL.CnpjCpf EmitCnpj, CDFL.InscricaoRG EmitIE, CDFL.RazaoSocialNome EmitRazaoSocial, CDFL.FantasiaApelido EmitApelido,  ";
                ssql += " CDFL.Endereco EmitEndereco, CDFL.Numero EmitNumero, CDFL.Complemento EmitComplemento, B.Nome EmitBairro, CDFLC.Nome EmitMunicipio, CDFLC.CodigoDoIbge EmitCodigoDoIbge,  ";
                ssql += " CDFL.Cep EmitCep, CDFLCE.UF EmitUF, CDFLCE.CodigoDoIbge EmitCodigoUF,  ";
                ssql += " (Select Top 1 CCE.Endereco  ";
                ssql += "           From CadastroContatoEndereco CCE  ";
                ssql += "           Left Join CadastroTipoDeContato CTC on (CTC.IDCadastroTipoDeContato=CCE.IDCadastroTipoDeContato)  ";
                ssql += "           where CCE.IdCadastro = CDFL.IdCadastro and CTC.Nome = 'E-MAIL' ";
                ssql += " ) EmitEmail,  ";
                ssql += " ( Select Top 1 CCE.Endereco  ";
                ssql += "           From CadastroContatoEndereco CCE  ";
                ssql += "           Left Join CadastroTipoDeContato CTC on (CTC.IDCadastroTipoDeContato=CCE.IDCadastroTipoDeContato)  ";
                ssql += "           where CCE.IdCadastro = CDFL.IdCadastro and CTC.Nome = 'TELEFONE COMERCIAL' ";
                ssql += " ) EmitTelefone,  ";
                ssql += " VPri.Placa VPriPlaca, VPri.CapacidadeDeCargaKG VPriVCapacidadeDeCargaKG, VPri.CapacidadeDeCargaM3 VPriVCapacidadeDeCargaM3, VPriEst.UF VPriEst,  ";
                ssql += " VSeg.Placa VSegPlaca, VSeg.CapacidadeDeCargaKG VSegVCapacidadeDeCargaKG, VSeg.CapacidadeDeCargaM3 VSegVCapacidadeDeCargaM3, VSegEst.UF VSegEst,  ";
                ssql += " MPri.CnpjCpf MPriCpf, MPri.RazaoSocialNome MPriNome, MPriCid.Nome MPriCid, MPriEst.Uf MPriEst, MSeg.CnpjCpf MSegCpf, MSeg.RazaoSocialNome MSegNome, ";
                ssql += " MSegCid.Nome MSegCid, MSegEst.Uf MSegEst, VPriProp.RazaoSocialNome VPriPropNome, VPriProp.CnpjCpf VPriPropCnpjCpf, VPriProp.RazaoSocialNome VPriPropNome,  ";
                ssql += " VPriProp.InscricaoRG VPriPropInscricao, VPriPropEst.UF VPriPropEst, VSegProp.CnpjCpf VSegPropCnpjCpf, VSegProp.RazaoSocialNome VSegPropNome, VSegProp.InscricaoRG VSegPropInscricao,  ";
                ssql += " VSegPropEst.UF VSegPropEst  ";
                ssql += " From DT D ";
                ssql += " Inner Join Filial FL on (FL.IdFilial = D.IdFilial)  ";
                ssql += " Inner Join Cadastro CDFL on (CDFL.IdCadastro = FL.IdCadastro)  ";
                ssql += " Inner Join Cidade CDFLC on (CDFLC.IdCidade = CDFL.IdCidade)  ";
                ssql += " Inner Join Estado CDFLCE on (CDFLCE.IdEstado = CDFLC.IdEstado)  ";
                ssql += " Left Join Bairro B on (B.IdBairro = CDFL.IdBairro)  ";
                ssql += " Left Join Cidade CdOrig on (CdOrig.IDCidade = D.IDCidadeDeOrigem)  ";
                ssql += " Left Join Cidade CdDest on (CdDest.IDCidade = D.IDCidadeDeDestino)  ";
                ssql += " Left Join Estado OriEst on (OriEst.IdEstado = CdOrig.IdEstado)  ";
                ssql += " Left Join Estado DesEst on (DesEst.IdEstado = CdDest.IdEstado)  ";
                ssql += " Left Join Veiculo VPri on (VPri.IdVeiculo=D.IdPrimeiroVeiculo)  ";
                ssql += " Left Join Cidade  VPriCid on (VPriCid.IdCidade = VPri.IdCidade)  ";
                ssql += " Left Join Estado  VPriEst on (VPriEst.IdEstado = VPriCid.IdEstado)  ";
                ssql += " Left Join Veiculo VSeg on (VSeg.IdVeiculo=D.IdSegundoVeiculo)  ";
                ssql += " Left Join Cidade  VSegCid on (VSegCid.IdCidade = VPri.IdCidade)  ";
                ssql += " Left Join Estado  VSegEst on (VSegEst.IdEstado = VPriCid.IdEstado)  ";
                ssql += " Left Join Cadastro MPri on (MPri.IdCadastro = D.IdPrimeiroMotorista)  ";
                ssql += " Left Join Cidade MPriCid on (MPriCid.IdCidade = MPri.IdCidade)  ";
                ssql += " Left Join Estado MPriEst on (MPriEst.IdEstado = MPriCid.IdEstado)  ";
                ssql += " Left Join Cadastro MSeg on (MSeg.IdCadastro = D.IdPrimeiroMotorista)  ";
                ssql += " Left Join Cidade MSegCid on (MSegCid.IdCidade = MSeg.IdCidade)  ";
                ssql += " Left Join Estado MSegEst on (MSegEst.IdEstado = MSegCid.IdEstado)  ";
                ssql += " Left Join Cadastro VPriProp on (VPriProp.IdCadastro = VPri.IDProprietario)  ";
                ssql += " Left Join Cidade   VPriPropCid on (VPriPropCid.IdCidade = VPriProp.IdCidade)  ";
                ssql += " Left Join Estado   VPriPropEst on (VPriPropEst.IdEstado = VPriPropCid.IdEstado)  ";
                ssql += " Left Join Cadastro VSegProp on (VSegProp.IdCadastro = VSeg.IDProprietario)  ";
                ssql += " Left Join Cidade   VSegPropCid on (VSegPropCid.IdCidade = VSegProp.IdCidade)  ";
                ssql += " Left Join Estado   VSegPropEst on (VSegPropEst.IdEstado = VSegPropCid.IdEstado)  ";
                ssql += " where D.IdDt =  " + idDT;

                SqlConnection c = new SqlConnection(cnx);
                c.Open();
                SqlCommand comm = new SqlCommand(ssql.ToString(), c);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(dt);
                c.Close();
                return dt;
            }

            public static DataTable RetornarPercurso(string ufOrigem, string ufDestino, string cnx)
            {
                string ssql = "Select * from PercursoMDFe where UfOrigem='" + ufOrigem + "' and UfDestino='" + ufDestino + "' order by Ordem";
                SqlConnection c = new SqlConnection(cnx);
                c.Open();
                SqlCommand comm = new SqlCommand(ssql.ToString(), c);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(dt);
                c.Close();
                return dt;
            }

            public static DataTable RetornarTotalizadores(int iddt, string tipoDeDocumento, string cnx)
            {
                string ssql = "";

                if (tipoDeDocumento == "CONHECIMENTO")
                {
                    ssql += "  Select Distinct  DE.IdNota, SUM(nf.ValorDaNota) ValorDaCarga,  ";
                    ssql += " Sum(nf.PesoBruto) PesoBruto, Count(NF.IdDocumento) QTDNotas   ";
                    ssql += " From DT D    ";
                    ssql += " Inner Join DTRomaneio DTR on (DTR.IdDt = D.IdDt)    ";
                    ssql += " Inner Join Romaneio R on (R.IdRomaneio = DTR.IdRomaneio)    ";
                    ssql += " Inner Join RomaneioDocumento RD on (RD.IdRomaneio=R.IdRomaneio)    ";
                    ssql += " Inner Join Documento NF on (NF.IdDocumento = RD.IdDocumento)    ";
                    ssql += " left Join DocumentoEletronico DE on (DE.IdDocumento=nf.IdDocumento)  ";
                    ssql += " where D.IdDt = " + iddt;
                    ssql += " and NF.TipoDeDocumento = 'CONHECIMENTO'  ";
                    ssql += " and DE.CStatus ='100' ";
                    ssql += " Group By DE.IdNota  ";
                }
                else
                {
                    ssql = "Select Distinct  DE.IdNota, SUM(CT.ValorDaNota) ValorDaCarga, Sum(CT.PesoBruto) PesoBruto, Count(NF.IdDocumento) QTDNotas ";
                    ssql += " From DT D ";
                    ssql += "  Inner Join DTRomaneio DTR on (DTR.IdDt = D.IdDt) ";
                    ssql += "  Inner Join Romaneio R on (R.IdRomaneio = DTR.IdRomaneio) ";
                    ssql += "  Inner Join RomaneioDocumento RD on (RD.IdRomaneio=R.IdRomaneio) ";
                    ssql += "  Inner Join Documento NF on (NF.IdDocumento = RD.IdDocumento) ";
                    ssql += "  Inner Join DocumentoRelacionado DR on (DR.IdDocumentoFilho = NF.IdDocumento) ";
                    ssql += "  Inner Join Documento CT on (CT.IdDocumento = DR.IdDocumentoPai) ";
                    ssql += "  Inner Join DocumentoEletronico DE on (DE.IdDocumento=CT.IdDocumento) ";
                    ssql += "where D.IdDt =  " + iddt;
                    ssql += "and CT.TipoDeDocumento = 'NOTA FISCAL' and DE.CStatus ='100'";
                    ssql += "Group By DE.IdNota ";
                }
                SqlConnection c = new SqlConnection(cnx);
                c.Open();
                SqlCommand comm = new SqlCommand(ssql.ToString(), c);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(dt);
                c.Close();
                return dt;
            }
        }
    
    }



    public class NumeroChave
    {
        public string numero;
        public int IdDocumento;

        public NumeroChave(string _numero, int _idDocumento)
        {
            this.numero = _numero;
            this.IdDocumento = _idDocumento;
        }
    }
}
