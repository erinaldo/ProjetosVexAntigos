using System;
using System.Data;
using System.Threading;
using System.Globalization;

namespace ServicosWEB
{
    public partial class EnviarConferenciaDET : System.Web.UI.Page
    {
        string cnx = "";
        DataTable dtNf;

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (Session["User"] == null)
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {

                    Carregar();

                    if (lblTotalFalas.Text != "" && lblTotalFalas.Text != "0")
                    {
                        string sql = "Update ReposicaoRoge set Valor=" + float.Parse(lblTotalFalas.Text).ToString("#0.00").Replace(",", ".") + " where IdReposicaoRoge=" + Request.QueryString["ID"].ToString();
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                    }
                }
                else
                {
                    Response.Redirect("EnviarConferencia.aspx", false);
                }

                if (TextBox3.Text.Contains("ENVIADO AUDITORIA"))
                {
                    if (float.Parse(lblTotalFalas.Text) > 100 || ((DataTable)grdNotaItensQnaoPertencemANF.DataSource).Rows.Count > 0)
                    {
                        Button2.Enabled = false;
                        chkCiencia.Visible = true;
                    }

                }

            }
            
        }

        private void Carregar()
        {
            dtNf = Sistran.Library.GetDataTables.RetornarDataTable("SELECT * FROM REPOSICAOROGE WHERE IDREPOSICAOROGE=" + Request.QueryString["ID"].ToString(), cnx);
            if (dtNf.Rows.Count > 0)
            {
                TextBox1.Text = dtNf.Rows[0]["CHAVE"].ToString();
                TextBox3.Text = dtNf.Rows[0]["STATUS"].ToString() + "- INICIOU: " + dtNf.Rows[0]["DataColetor"].ToString() + "- USUARIO: " + dtNf.Rows[0]["UsuarioColetor"].ToString() + "- FIM: " + dtNf.Rows[0]["FIM"].ToString();
                TextBox2.Text = dtNf.Rows[0]["CLIENTEESPECIAL"].ToString();
                txtResultadoDoEnvio.Text = dtNf.Rows[0]["DescricaoEnvioRoge"].ToString().Replace("<", "").Replace(">", "");

                CarregarGridVolumes();
                CarregarItens();
                CarregarItensNaoPertencemaNota();
                CarregarConfCega();


                if (dtNf.Rows[0]["STATUS"].ToString() == "ENVIADO AUDITORIA")
                {
                    Button1.Enabled = true;
                    Button2.Enabled = true;
                }
                else
                {
                    Button1.Enabled = false;
                    Button2.Enabled = false;
                }

                if (dtNf.Rows[0]["STATUS"].ToString().Contains("EM ABERTO"))
                {
                    Button1.Enabled = true;
                    Button2.Enabled = true;

                }

                if (dtNf.Rows[0]["STATUS"].ToString() == "RESPOSTA NÃO IDENTIFICADA")
                    Button2.Enabled = true;

                if (dtNf.Rows[0]["STATUS"].ToString().ToUpper().Contains("EM CONFERENCIA"))
                {
                    Button4.Text = "LIBERAR NOTA PARA OUTRO COLETOR";
                }
                else
                {
                    Button4.Text = ".";
                    Button4.BorderWidth = 0; 
                }

            }
        }

        private void CarregarGridVolumes()
        {
            grdVolumes.DataSource = Sistran.Library.GetDataTables.RetornarDataTable("SELECT ROW_NUMBER() OVER(ORDER BY  IdReposicaoRogeVolume) AS [#], IdReposicaoRogeVolume [COD.], CodigoDeBarras [CODIGO DE BARRAS], CONFERIDO FROM reposicaorogevolume WHERE IdResposicaoRoge=" + Request.QueryString["ID"].ToString(), cnx);
            grdVolumes.DataBind();
        }

        private void CarregarConfCega()
        {
            //string sql = "select rcc.*, rpi.Descricao from reposicaoroge rp inner  join reposicaoRogeItem rpi on  rpi.IdReposicaoRoge = rp.IdReposicaoRoge inner join  reposicaorogeConferenciaCega rcc on rcc.IdConferenciaItem = rpi.IdReposicaoRogeItem where rp.idreposicaoRoge = " + Request.QueryString["ID"].ToString() + " order by rpi.Descricao";

            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY RPI.DESCRICAO) AS [#], RCC.CODIGOROGE, RCC.CODIGODEBARRASLIDO[CODIGO LIDO], RCC.QUANTIDADE [QTD EMBALAGEM], CB.EMBALAGEM ,RPI.DESCRICAO [DESCRIÇÃO] ";
            sql += " FROM REPOSICAOROGE RP ";
            sql += " INNER  JOIN REPOSICAOROGEITEM RPI ON  RPI.IDREPOSICAOROGE = RP.IDREPOSICAOROGE  ";
            sql += " INNER JOIN  REPOSICAOROGECONFERENCIACEGA RCC ON RCC.IDCONFERENCIAITEM = RPI.IDREPOSICAOROGEITEM  ";
            sql += " INNER JOIN REPOSICAOROGECB CB ON CB.IDREPOSICAOROGEITEM = RPI.IDREPOSICAOROGEITEM ";
            sql += " WHERE RP.IDREPOSICAOROGE = " + Request.QueryString["ID"].ToString();
            sql += " AND CB.CODIGODEBARRAS = RCC.CODIGODEBARRASLIDO ";
            sql += "  ORDER BY RPI.DESCRICAO ";

            GRDcOFcEGA.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sql, cnx);
            GRDcOFcEGA.DataBind();
        }

        private void CarregarItens()
        {

            //string sql = "select ROW_NUMBER() OVER(ORDER BY  ri.IdReposicaoRogeItem) AS [#], /*IdReposicaoRogeItem [COD.],*/ CodigoRoge [Cod.Roge],  ";
            //sql += " CodigoBarrasLido [CodigoDeBarras],  ";
            //sql += " Descricao[Descrição],     ";
            //sql += " DataConferido [Conferencia], ";
            //sql += " isnull(QuantidadeLido, 0) [Qtd.Conf.] ,  ";
            //sql += " QuantidadeNota[Qtd.Nota], ";
            //sql += " cast(isnull(Valor,0) as decimal(10, 2)) ValorDoItem, ";
            //sql += " (QuantidadeNota-isnull(QuantidadeLido, 0)) [Faltas/Sobras], ";
            //sql += "  cast(isnull(Valor,0) * (QuantidadeNota-isnull(QuantidadeLido, 0)) as decimal(10, 2))  [Valor Falta/Sobra] ";
            //sql += " from reposicaorogeitem ri  ";
            ////sql += " left  join ReposicaoRogeCB rcb on rcb.CodigoDeBarras = ri.CodigoBarrasLido and ri.IdReposicaoRogeItem = rcb.IdReposicaoRogeItem ";


            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY  RI.IDREPOSICAOROGEITEM) AS [#], CODIGOROGE [COD.ROGE],   CODIGOBARRASLIDO [CODIGODEBARRAS],   DESCRICAO[DESCRIÇÃO],      ";
            sql += " DATACONFERIDO [CONFERENCIA],  ISNULL(QUANTIDADELIDO, 0) [QTD.CONF.] ,   QUANTIDADENOTA[QTD.NOTA],  ";
            sql += " CAST(CAST(ISNULL(VALOR,0) AS DECIMAL(10, 5))/(SELECT TOP 1 QUANTIDADE FROM REPOSICAOROGECB WHERE IDREPOSICAOROGEITEM = RI.IDREPOSICAOROGEITEM ORDER BY QUANTIDADE) AS DECIMAL(10,2)) VALORDOITEM,  (QUANTIDADENOTA-ISNULL(QUANTIDADELIDO, 0)) [FALTAS/SOBRAS],   ";
            sql += " CAST(CAST(ISNULL(VALOR,0) AS DECIMAL(10, 5))/(SELECT TOP 1 QUANTIDADE FROM REPOSICAOROGECB WHERE IDREPOSICAOROGEITEM = RI.IDREPOSICAOROGEITEM ORDER BY QUANTIDADE) * (QUANTIDADENOTA-ISNULL(QUANTIDADELIDO, 0)) AS DECIMAL(10,2)) [VALOR FALTA/SOBRA]  ";
            sql += " FROM REPOSICAOROGEITEM RI   ";
            sql += " where IdReposicaoRoge=" + Request.QueryString["id"].ToString() + " and isnull(PerteceANota, 'SIM') <> 'NAO' ";
            grdItem.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sql, cnx);
            grdItem.DataBind();
            CalcularFaltasSobras();
        }

        private void CalcularFaltasSobras()
        {
            if (TextBox3.Text.Contains("AUDITORIA") || TextBox3.Text.Contains("FINALIZAD"))
            {
                //string sql = "select ";
                //sql += " sum(isnull(Valor,0) * (isnull(QuantidadeNota, 0)-isnull(QuantidadeLido, 0))) ValorFalta ";
                //sql += " from reposicaorogeitem ri  ";
                ////sql += " left  join ReposicaoRogeCB rcb on rcb.CodigoDeBarras = ri.CodigoBarrasLido and ri.IdReposicaoRogeItem = rcb.IdReposicaoRogeItem ";
                //sql += " where IdReposicaoRoge=" + Request.QueryString["id"].ToString();
                //sql += " and isnull(PerteceANota, 'SIM') <> 'NAO' ";

                string sql = "SELECT CAST(CAST(ISNULL(VALOR,0) AS DECIMAL(10, 5))/(SELECT TOP 1 QUANTIDADE FROM REPOSICAOROGECB WHERE IDREPOSICAOROGEITEM = RI.IDREPOSICAOROGEITEM ORDER BY QUANTIDADE) * (abs(QUANTIDADENOTA-ISNULL(QUANTIDADELIDO, 0))) AS DECIMAL(10,2)) [VALOR]  fROM REPOSICAOROGEITEM RI   WHERE IDREPOSICAOROGE=" + Request.QueryString["id"].ToString() + " AND ISNULL(PERTECEANOTA, 'SIM') <> 'NAO' ";

                DataTable d = Sistran.Library.GetDataTables.RetornarDataTable(sql, cnx);
                lblTotalFalas.Text = double.Parse(d.Compute("SUM(VALOR)","").ToString()).ToString("#0.00");

            }
            else
                lblTotalFalas.Text = "0";
        }


        private void CarregarItensNaoPertencemaNota()
        {
            //grdNotaItensQnaoPertencemANF.DataSource = Sistran.Library.GetDataTables.RetornarDataTable("select ROW_NUMBER() OVER(ORDER BY  IdReposicaoRogeItem) AS [#], IdReposicaoRogeItem [COD.], DataDaInclusao[Recebido],  CodigoBarrasLido [CodigoDeBarras],  DataConferido [Data Da Conferencia], isnull(QuantidadeLido, 0) Quantidade from reposicaorogeitem where IdReposicaoRoge=" + Request.QueryString["id"].ToString() + " and PerteceANota= 'NAO'", cnx);

           grdNotaItensQnaoPertencemANF.DataSource = Sistran.Library.GetDataTables.RetornarDataTable("select ROW_NUMBER() OVER(ORDER BY  IdReposicaoRogeItem) AS [#], IdReposicaoRogeItem [COD.], DataDaInclusao[Recebido],  CodigoBarrasLido [CodigoDeBarras], ean.Descricao [Descrição] , DataConferido [Data Da Conferencia], isnull(QuantidadeLido, 0) Quantidade from reposicaorogeitem rri left join reposicaorogeEan ean on ean.CodigoDeBarras = rri.CodigoBarrasLido where IdReposicaoRoge="+Request.QueryString["id"].ToString() +" and PerteceANota= 'NAO'", cnx);

            grdNotaItensQnaoPertencemANF.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = "";
            s = " UPDATE REPOSICAOROGE SET STATUS='AGUARDANDO CONFERENCIA', Inicio=null, fim=null, UsuarioColetor='' where IdReposicaoRoge=" + Request.QueryString["id"];
            s += " ; UPDATE REPOSICAOROGEVOLUME SET CONFERIDO='NAO', DATACONFERIDO=NULL WHERE IDRESPOSICAOROGE=" + Request.QueryString["id"];


            s += " ; delete from reposicaorogeConferenciaCega WHERE IdConferenciaItem in(select idREPOSICAOROGEITEM from REPOSICAOROGEITEM where IdReposicaoRoge=" + Request.QueryString["id"] + ")";


            s += " ; UPDATE REPOSICAOROGEITEM SET QUANTIDADELIDO=NULL, PERTECEANOTA=NULL, CODIGOBARRASLIDO=NULL, DATACONFERIDO=NULL WHERE IDREPOSICAOROGE=" + Request.QueryString["id"];
            s += " ; delete from ReposicaoRogeItem where IdReposicaoRoge=" + Request.QueryString["id"] + " and codigoRoge is null";

            Sistran.Library.GetDataTables.ExecutarComandoSql(s, cnx);
            Button1.Text = "Reaberto com Sucesso";
            Button1.Enabled = false;
            Button2.Enabled = false;

            Carregar();

            txtResultadoDoEnvio.Text = "Conferência Reinicializada.";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("enviarconferencia.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            


            try
            {
                dtNf = Sistran.Library.GetDataTables.RetornarDataTable("SELECT * FROM REPOSICAOROGE WHERE IDREPOSICAOROGE=" + Request.QueryString["ID"].ToString(), cnx);

                br.com.roge.wslogos01.WSLogos ws = new br.com.roge.wslogos01.WSLogos();
                br.com.roge.wslogos01.ConferenciaNotaFiscalEanVolume vol = new br.com.roge.wslogos01.ConferenciaNotaFiscalEanVolume();
                br.com.roge.wslogos01.ConferenciaNotaFiscalProduto prd = new br.com.roge.wslogos01.ConferenciaNotaFiscalProduto();
                br.com.roge.wslogos01.ConferenciaNotaFiscal nf = new br.com.roge.wslogos01.ConferenciaNotaFiscal();

                for (int inf = 0; inf < dtNf.Rows.Count; inf++)
                {
                    nf.chaveeletronica = dtNf.Rows[inf]["Chave"].ToString();
                    nf.usuario = ((DataTable)Session["User"]).Rows[0]["LOGIN"].ToString();

                    DataTable dtvol = Sistran.Library.GetDataTables.RetornarDataTable("SELECT * FROM reposicaorogevolume WHERE Conferido='sim' and  IdResposicaoRoge=" + Request.QueryString["ID"].ToString(), cnx);

                    br.com.roge.wslogos01.ConferenciaNotaFiscalEanVolume[] avol = new br.com.roge.wslogos01.ConferenciaNotaFiscalEanVolume[dtvol.Rows.Count];

                    for (int ivol = 0; ivol < dtvol.Rows.Count; ivol++)
                    {
                        vol = new br.com.roge.wslogos01.ConferenciaNotaFiscalEanVolume();
                        vol.chaveeletronica = nf.chaveeletronica;
                        vol.codigobarrasvolume = dtvol.Rows[ivol]["CodigoDeBarras"].ToString();
                        avol[ivol] = vol;
                    }
                    nf.eanvolumes = avol;


                    DataTable dtItem = Sistran.Library.GetDataTables.RetornarDataTable("select * from reposicaorogeitem where IdReposicaoRoge=" + Request.QueryString["id"].ToString(), cnx);


                    br.com.roge.wslogos01.ConferenciaNotaFiscalProduto[] aprd = new br.com.roge.wslogos01.ConferenciaNotaFiscalProduto[dtItem.Rows.Count];

                    for (int iItem = 0; iItem < dtItem.Rows.Count; iItem++)
                    {

                        prd = new br.com.roge.wslogos01.ConferenciaNotaFiscalProduto();
                        bool x = true;
                        if (dtItem.Rows[iItem]["PerteceANota"].ToString() == "" || dtItem.Rows[iItem]["PerteceANota"].ToString() == "SIM")
                            x = true;
                        else
                            x = false;

                        prd.pertenceanota = bool.Parse(x.ToString());

                        if (x == false)
                            prd.codigoproduto = dtItem.Rows[iItem]["CodigoBarrasLido"].ToString();
                        else
                            prd.codigoproduto = dtItem.Rows[iItem]["CodigoRoge"].ToString();


                        prd.chaveeletronica = nf.chaveeletronica;

                        string qtd = "0";
                        if (dtItem.Rows[iItem]["QuantidadeLido"].ToString() != "")
                            qtd = dtItem.Rows[iItem]["QuantidadeLido"].ToString();

                        prd.quantidade = int.Parse(qtd).ToString();
                        aprd[iItem] = prd;
                    }

                    nf.produtos = aprd;
                }

                nf.datahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string ds = ws.GravarConferencia(nf);
                //              DataSet ds = ws.GravarConferencia(nf);

                string[] ret = ds.Split('^');
                string status = "";
                //string DescricaoRetorno = "";


                if (ret[0] == "0")
                    status = "FINALIZADO";
                else
                    status = "EM ABERTO";

                //DescricaoRetorno = ret[1];


                if(ret[1].Contains("JA EXISTE NO BANCO DE DADOS"))
                    status="FINALIZADO";

                string s = "update ReposicaoRoge set DataEnvioRoge = GETDATE(),Status='" + status + "', UsuarioEnvioRoge='" + ((DataTable)Session["User"]).Rows[0]["LOGIN"].ToString() + "', DescricaoEnvioRoge='" + ds.Replace("0^","").Replace("1^","") + "' where Chave='" + TextBox1.Text + "'";
                Sistran.Library.GetDataTables.RetornarDataTable(s, cnx);

                txtResultadoDoEnvio.Text += " RESULTADO DO ENVIO: " + ds;


            }
            catch (Exception ex)
            {
                var baseException = ex.GetBaseException();
                txtResultadoDoEnvio.Text = "Problema no envio";


                if (ex.Message.ToUpper().Contains("IMPOSSÍVEL CONECTAR-SE AO SERVIDOR REMOTO") || ex.Message.ToUpper().Contains("UNABLE TO CONNECT TO THE REMOTE SERVER"))
                {
                    txtResultadoDoEnvio.Text = "1ª Tentativa *************** Acesso indisponível no momento tente mais tarde ***************";
                    //txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;
                    Tentar2Link();
                    return;

                }

                string status = "";

                try
                {
                    txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Green;
                    txtResultadoDoEnvio.Text = ex.Message + "\n\r" + ex.InnerException;


                    if (((System.Web.Services.Protocols.SoapException)baseException).Code.Name == "99")
                    {
                        if (((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText.ToUpper().Contains("Á EXISTENTE NO BANCO DE DADOS DA EMPRESA ROGE") || ((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText.ToUpper().Contains("JA EXISTE NO BANCO DE DADOS DA EMPRESA ROGE"))
                        {
                            status = "FINALIZADO";
                            txtResultadoDoEnvio.Text = "(OK) ";
                            txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Blue;
                        }
                        else
                        {

                            status = "EM ABERTO";
                            txtResultadoDoEnvio.Text = "(** VERIFICAR OS DADOS) ";
                            txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;
                        }


                    }
                    else if (((System.Web.Services.Protocols.SoapException)baseException).Code.Name == "00")
                    {
                        status = "FINALIZADO";
                        txtResultadoDoEnvio.Text = "(OK) ";
                        txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        status = "RESPOSTA NÃO IDENTIFICADA";
                        txtResultadoDoEnvio.Text = "(** PROBLEMA NO WEBSERVICE DA ROGE) ";
                        txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;
                    }


                    txtResultadoDoEnvio.Text += " RESULTADO DO ENVIO: " + ((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText;

                    string s = "update ReposicaoRoge set DataEnvioRoge = GETDATE(),Status='" + status + "', UsuarioEnvioRoge='" + ((DataTable)Session["User"]).Rows[0]["LOGIN"].ToString() + "', DescricaoEnvioRoge='" + ((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText.Replace("'", "") + "' where Chave='" + TextBox1.Text + "'";
                    Sistran.Library.GetDataTables.RetornarDataTable(s, cnx);

                    Button1.Enabled = false;
                    Button2.Enabled = false;

                    if (status.Contains("EM ABERTO"))
                    {
                        Button1.Enabled = true; ;
                        Button2.Enabled = true; ;

                    }

                    if (status == "RESPOSTA NÃO IDENTIFICADA")
                        Button2.Enabled = true;


                }
                catch (Exception exi)
                {
                    txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    Button2.Enabled = true;
                    Button1.Enabled = true;

                }
            }
        }


        public void Tentar2Link()
        {
            try
            {
                dtNf = Sistran.Library.GetDataTables.RetornarDataTable("SELECT * FROM REPOSICAOROGE WHERE IDREPOSICAOROGE=" + Request.QueryString["ID"].ToString(), cnx);

                br.com.roge.wslogos02.WSLogos ws = new br.com.roge.wslogos02.WSLogos();
                br.com.roge.wslogos02.ConferenciaNotaFiscalEanVolume vol = new br.com.roge.wslogos02.ConferenciaNotaFiscalEanVolume();
                br.com.roge.wslogos02.ConferenciaNotaFiscalProduto prd = new br.com.roge.wslogos02.ConferenciaNotaFiscalProduto();
                br.com.roge.wslogos02.ConferenciaNotaFiscal nf = new br.com.roge.wslogos02.ConferenciaNotaFiscal();

                for (int inf = 0; inf < dtNf.Rows.Count; inf++)
                {
                    nf.chaveeletronica = dtNf.Rows[inf]["Chave"].ToString();
                    nf.usuario = ((DataTable)Session["User"]).Rows[0]["LOGIN"].ToString();

                    DataTable dtvol = Sistran.Library.GetDataTables.RetornarDataTable("SELECT * FROM reposicaorogevolume WHERE Conferido='sim' and  IdResposicaoRoge=" + Request.QueryString["ID"].ToString(), cnx);

                    br.com.roge.wslogos02.ConferenciaNotaFiscalEanVolume[] avol = new br.com.roge.wslogos02.ConferenciaNotaFiscalEanVolume[dtvol.Rows.Count];

                    for (int ivol = 0; ivol < dtvol.Rows.Count; ivol++)
                    {
                        vol = new br.com.roge.wslogos02.ConferenciaNotaFiscalEanVolume();
                        vol.chaveeletronica = nf.chaveeletronica;
                        vol.codigobarrasvolume = dtvol.Rows[ivol]["CodigoDeBarras"].ToString();
                        avol[ivol] = vol;
                    }
                    nf.eanvolumes = avol;


                    DataTable dtItem = Sistran.Library.GetDataTables.RetornarDataTable("select * from reposicaorogeitem where IdReposicaoRoge=" + Request.QueryString["id"].ToString(), cnx);


                    br.com.roge.wslogos02.ConferenciaNotaFiscalProduto[] aprd = new br.com.roge.wslogos02.ConferenciaNotaFiscalProduto[dtItem.Rows.Count];

                    for (int iItem = 0; iItem < dtItem.Rows.Count; iItem++)
                    {

                        prd = new br.com.roge.wslogos02.ConferenciaNotaFiscalProduto();
                        bool x = true;
                        if (dtItem.Rows[iItem]["PerteceANota"].ToString() == "" || dtItem.Rows[iItem]["PerteceANota"].ToString() == "SIM")
                            x = true;
                        else
                            x = false;

                        prd.pertenceanota = bool.Parse(x.ToString());

                        if (x == false)
                            prd.codigoproduto = dtItem.Rows[iItem]["CodigoBarrasLido"].ToString();
                        else
                            prd.codigoproduto = dtItem.Rows[iItem]["CodigoRoge"].ToString();


                        prd.chaveeletronica = nf.chaveeletronica;

                        string qtd = "0";
                        if (dtItem.Rows[iItem]["QuantidadeLido"].ToString() != "")
                            qtd = dtItem.Rows[iItem]["QuantidadeLido"].ToString();

                        prd.quantidade = int.Parse(qtd).ToString();

                        aprd[iItem] = prd;
                    }

                    nf.produtos = aprd;
                }

                nf.datahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string ds = ws.GravarConferencia(nf);
                //              DataSet ds = ws.GravarConferencia(nf);

                string[] ret = ds.Split('^');
                string status = "";
                //string DescricaoRetorno = "";


                if (ret[0] == "0")
                    status = "FINALIZADO";
                else
                    status = "EM ABERTO";

                //DescricaoRetorno = ret[1];


                if (ret[1].Contains("JA EXISTE NO BANCO DE DADOS"))
                    status = "FINALIZADO";

                string s = "update ReposicaoRoge set DataEnvioRoge = GETDATE(),Status='" + status + "', UsuarioEnvioRoge='" + ((DataTable)Session["User"]).Rows[0]["LOGIN"].ToString() + "', DescricaoEnvioRoge='" + ds.Replace("0^", "").Replace("1^", "") + "' where Chave='" + TextBox1.Text + "'";
                Sistran.Library.GetDataTables.RetornarDataTable(s, cnx);

                txtResultadoDoEnvio.Text += " RESULTADO DO ENVIO: " + ds;


            }
            catch (Exception ex)
            {
                var baseException = ex.GetBaseException();
                txtResultadoDoEnvio.Text = "Problema no envio";


                if (ex.Message.ToUpper().Contains("IMPOSSÍVEL CONECTAR-SE AO SERVIDOR REMOTO") || ex.Message.ToUpper().Contains("UNABLE TO CONNECT TO THE REMOTE SERVER"))
                {
                    txtResultadoDoEnvio.Text = "\r\n 2ª Tentativa *************** Acesso indisponível no momento tente mais tarde ***************";
                    txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;                    
                    return;

                }

                string status = "";

                try
                {
                    txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Green;
                    txtResultadoDoEnvio.Text = ex.Message + "\n\r" + ex.InnerException;


                    if (((System.Web.Services.Protocols.SoapException)baseException).Code.Name == "99")
                    {
                        if (((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText.ToUpper().Contains("Á EXISTENTE NO BANCO DE DADOS DA EMPRESA ROGE") || ((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText.ToUpper().Contains("JA EXISTE NO BANCO DE DADOS DA EMPRESA ROGE"))
                        {
                            status = "FINALIZADO";
                            txtResultadoDoEnvio.Text = "(OK) ";
                            txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Blue;
                        }
                        else
                        {

                            status = "EM ABERTO";
                            txtResultadoDoEnvio.Text = "(** VERIFICAR OS DADOS) ";
                            txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;
                        }


                    }
                    else if (((System.Web.Services.Protocols.SoapException)baseException).Code.Name == "00")
                    {
                        status = "FINALIZADO";
                        txtResultadoDoEnvio.Text = "(OK) ";
                        txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        status = "RESPOSTA NÃO IDENTIFICADA";
                        txtResultadoDoEnvio.Text = "(** PROBLEMA NO WEBSERVICE DA ROGE) ";
                        txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;
                    }


                    txtResultadoDoEnvio.Text += " RESULTADO DO ENVIO: " + ((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText;

                    string s = "update ReposicaoRoge set DataEnvioRoge = GETDATE(),Status='" + status + "', UsuarioEnvioRoge='" + ((DataTable)Session["User"]).Rows[0]["LOGIN"].ToString() + "', DescricaoEnvioRoge='" + ((System.Web.Services.Protocols.SoapException)baseException).Detail.InnerText.Replace("'", "") + "' where Chave='" + TextBox1.Text + "'";
                    Sistran.Library.GetDataTables.RetornarDataTable(s, cnx);

                    Button1.Enabled = false;
                    Button2.Enabled = false;

                    if (status.Contains("EM ABERTO"))
                    {
                        Button1.Enabled = true; ;
                        Button2.Enabled = true; ;

                    }

                    if (status == "RESPOSTA NÃO IDENTIFICADA")
                        Button2.Enabled = true;


                }
                catch (Exception exi)
                {
                    txtResultadoDoEnvio.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    Button2.Enabled = true;
                    Button1.Enabled = true;

                }
            }
        }

        protected void grdItem_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text != e.Row.Cells[6].Text)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.Font.Bold = true;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.Green;

                    if(btnMostrarTudo.Text!="MOSTRAR DIVERGENCIAS")
                        e.Row.Visible = false;
                }
            }
        }

        protected void btnMostrarTudo_Click(object sender, EventArgs e)
        {
            if (btnMostrarTudo.Text.ToUpper() == "MOSTRAR TUDO")
            {
                btnMostrarTudo.Text = "MOSTRAR DIVERGENCIAS";
            }
            else
            {
                btnMostrarTudo.Text = "MOSTRAR TUDO";
            }
            Carregar();

            if(TextBox3.Text.ToUpper().Contains("AUDITORIA"))
            {
                Button1.Enabled = true;
                Button2.Enabled = true;

            }
        }

        protected void grdVolumes_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text =="NAO")
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.Font.Bold = true;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.Green;

                    if (btnMostrarTudo.Text != "MOSTRAR DIVERGENCIAS")
                        e.Row.Visible = false;
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string s = "UPDATE REPOSICAOROGE SET STATUS='AGUARDANDO CONFERENCIA', USUARIOCOLETOR=NULL,  DATACOLETOR=NULL  WHERE CHAVE='" + TextBox1.Text + "'";
            Sistran.Library.GetDataTables.RetornarDataTable(s, cnx);
            Response.Redirect("EnviarConferenciaDET.aspx?id="+ Request.QueryString["id"], false);
        }

        protected void chkCiencia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCiencia.Checked)
            {
                Button2.Enabled = true;
                Button2.Focus();
            }
            else
            {
                Button2.Enabled = false;
                chkCiencia.Focus();
            }
        }
    }
}