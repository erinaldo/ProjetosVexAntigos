using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
//using ChartDirector;

public partial class frmPopInventario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ChartDirector.WebChartViewer.OnPageInit(Page);
        ListBox1.Visible = false;
        try
        {
            Timer1.Enabled = false;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper().Substring(1, 20)), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

            }
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Carregar();
            CarregarRakuinUsuarios();
            Label1.Text = "Última Atualização: " + DateTime.Now;

            if (CheckBox1.Checked)
            {
                Timer1.Enabled = true;
                Timer1.Interval = Convert.ToInt32(3) * 60000;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    private void CarregarRakuinUsuarios()
    {
        PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99% runat='server' border='0'>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' colspan='2'><b>RAKING DE USUÁRIOS</b>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap><b>USUÁRIO</b>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'><b>QUANTIDADE</b>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

        string[] dados = Request.QueryString["Contagem"].ToString().Split('-');

        DataTable duser = new SistranBLL.Deposito().RakingUsers(int.Parse(dados[0]));

        for (int i = 0; i < duser.Rows.Count; i++)
        {
            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap>" + duser.Rows[i]["NOME"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + duser.Rows[i]["QUANTIDADE"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
        }
        PlaceHolder2.Controls.Add(new LiteralControl(@"</table >"));
    }

    public string CriarBotaoExpandir(string rua, bool expandAll, string RI, string RF)
    {
        string m = "";
        if (expandAll)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<center><div id='dvExpandir' style='font-size:11px;cursor:Hand;background-image:url(Images/seta.jpg); height:12px; width:14px;' OnClick=ExpandirAll('" + RI + "','" + RF + "');>"));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"<img scr='Images/seta.jpg' alt='Expandir e Recolher Todos' style='background-image:url(Images/seta.jpg); height:20px' >"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</div></center>"));
            m="";
        }
        else
        {
            m= "<b><div style='font-size:11px;cursor:Hand' OnClick=Expandir('" + rua + "');>";
            m += rua + "</div></b>";
        }
        //Button1.Attributes.Add("OnClick", "Expandir('" + rua + "');");
        return m;
    }

    public void Carregar()
    {
        Timer1.Enabled = false;
        bool PegarContagemDeRuas = false;
        ListBox1.Items.Clear();
        try
        {
            //Timer1.Enabled = false;
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU INVENTARIO.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

            string[] dados = Request.QueryString["Contagem"].ToString().Split('-');
            DataTable dtruas = new SistranBLL.Deposito().RetornarMaxMinRuas(Request.QueryString["idinventario"]);

            DataTable dt = null;

            string strsql = " SELECT DISTINCT(ISNULL(FIM,0) )FROM DEPOSITOPLANTALEIAUTE DPL ";
            strsql += " INNER JOIN DEPOSITOPLANTA DP ON DP.IDDEPOSITOPLANTA = DPL.IDDEPOSITOPLANTA ";
            strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPLZ ON DPLZ.IDDEPOSITOPLANTA = DPL.IDDEPOSITOPLANTA ";
            strsql += " inner join INVENTARIOCONTAGEMPRODUTO ICP ON ICP.IDDEPOSITOPLANTALOCALIZACAO = DPLZ.IDDEPOSITOPLANTALOCALIZACAO ";
            strsql += " WHERE TIPO='ANDAR' AND IDINVENTARIOCONTAGEM =" + dados[0];

            int andares = Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");


            //significa que o item escolhido é a contagem do sistema q server de base para o inventario
            if ((dados[1].Contains("CONTAGEM") && (dados[1].Contains("SISTEMA")) || dados[2] == "POSICAO DO SISTEMA"))
            {
                PegarContagemDeRuas = false;
                //dt = new SistranBLL.Deposito().CarregarIventario(int.Parse(dados[0]), PegarContagemDeRuas, "", "", Request.QueryString["idinventario"]);
                dt = new SistranBLL.Deposito().CarregarIventario(int.Parse(dados[0]), PegarContagemDeRuas, dtruas.Rows[0]["RUA_MIN"].ToString(), dtruas.Rows[0]["RUA_MAX"].ToString(), Request.QueryString["idinventario"]);
            }
            else
            {
                PegarContagemDeRuas = true;
                dt = new SistranBLL.Deposito().CarregarIventario(int.Parse(dados[0]), PegarContagemDeRuas, dtruas.Rows[0]["RUA_MIN"].ToString(), dtruas.Rows[0]["RUA_MAX"].ToString(), Request.QueryString["idinventario"]);
            }
           // dt = new SistranBLL.Deposito().CarregarIventario(int.Parse(dados[0]), PegarContagemDeRuas, dtruas.Rows[0]["RUA_MIN"].ToString(), dtruas.Rows[0]["RUA_MAX"].ToString(), "64", andares.ToString(), Request.QueryString["idinventario"]);


          

            PlaceHolder1.Controls.Clear();

            if (dt.Rows.Count > 0)
            {

                int MinimoDeRuas = Convert.ToInt32(dt.Compute("MIN(rua)", ""));
                int ruas = Convert.ToInt32(dt.Compute("max(rua)", ""));
                int colunas = Convert.ToInt32(dt.Compute("max(coluna)", ""));
                AcertarColunas(dt, andares, colunas, MinimoDeRuas, ruas, PegarContagemDeRuas);
                string tempI = (MinimoDeRuas.ToString().Length<2? "0"+ MinimoDeRuas.ToString(): MinimoDeRuas.ToString());
                string tempF = (ruas.ToString().Length<2? "0"+ ruas.ToString(): ruas.ToString());               
               

                tblResumo.Visible = true;
                ph3.Controls.Clear();
                PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=90% runat='server' border='0'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' width='1%' ><b>RUA</b>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' colspan='" + (colunas + 1).ToString() + "'><b>COLUNAS</b>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir("", true,tempI ,tempF)));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                string colunaAtual = "";
                
                for (int i = 0; i < colunas; i++)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' ><b><CENTER>" + (Convert.ToInt32(i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString()).ToString()));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<CENTER></b></td>"));
                }
                //coluna de total
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' ><b>TOTAL"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</b></td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

                string RuaAtual = "";
                int qtdContadaTotal = 0;

                for (int ii = MinimoDeRuas; ii <= ruas; ii++)
                //for (int ii = MinimoDeRuas; ii <= int.Parse(dtruas.Rows[0]["RUA_MAX"].ToString()); ii++)                    
                {
                    for (int iLinhaExtra = 0; iLinhaExtra < 4; iLinhaExtra++)
                    {
                        #region Linha 1
                        if (iLinhaExtra == 0)
                        {
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr VALIGN='TOP' ALIGN='CENTER'>"));

                            RuaAtual = ((ii) > 9 ? (ii).ToString() : "0" + (ii).ToString());

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpSemAlign' valign='middle' align='center' >"));

                            PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(RuaAtual, false, "","")));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            int qtdContadaTotalLinha = 0;
                            int qtdNaoContadosLinha = 0;

                            for (int i = 0; i < colunas; i++)
                            {

                                colunaAtual = (Convert.ToInt32(i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString()).ToString();

                                int qtdContada = Convert.ToInt32(dt.Compute("COUNT(PREDIO)", "RUA= " + RuaAtual + " AND COLUNA=" + colunaAtual + " AND QUANTIDADE>=0 AND ATIVO='SIM'"));
                                int qtddesativados = 0, naoExiste = 0, Contados = 0, qtdNaoContados=0;

                                DataRow[] ow = dt.Select("RUA='" + RuaAtual + "' AND COLUNA='" + colunaAtual + "'");

                                for (int iDX = 0; iDX < ow.Length; iDX++)
                                {
                                    if (ow[iDX]["ATIVO"].ToString() == "NAO")
                                    {
                                        qtddesativados++;
                                    }
                                    else if (Convert.ToInt32(ow[iDX]["QUANTIDADE"]) >= 0 && ow[iDX]["ATIVO"].ToString() == "SIM")
                                    {
                                        Contados++;
                                    }
                                    else if (ow[iDX]["ATIVO"].ToString() == "NCC")
                                    {
                                        qtdNaoContados++;
                                    }
                                }

                                naoExiste = andares - (Contados + qtddesativados);
                                //naoExiste = qtdNaoContados;



                                if (Contados > 0)
                                {
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='contados'>"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(qtdContada.ToString()));
                                    qtdContadaTotalLinha += qtdContada;
                                }
                                else if (qtddesativados == andares)
                                {
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='desativados'>"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<img src='Images/proibidoX.bmp' height='11px' alt='Posições Inativas'>"));
                                }
                                else if (qtdNaoContados == andares || naoExiste == qtdNaoContados)
                                {                                    
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='naocontados'>" + naoExiste));
                                }
                                qtdNaoContadosLinha += naoExiste;

                                
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            }
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' style='width:1%'>" + qtdContadaTotalLinha));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                            
                            ListBox1.Items.Add((qtdContadaTotalLinha.ToString() + "|" + qtdNaoContadosLinha.ToString()).ToString());

                        #endregion
                        }
                        else if (iLinhaExtra == 1 || iLinhaExtra == 3)
                        {
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr id='tr" + RuaAtual + iLinhaExtra + "' style='display:none'>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='" + (colunas + 2) + "' class='Divisoria2'><B>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                        }
                        else if (iLinhaExtra == 2)
                        {
                            #region linha extra

                            RuaAtual = ((ii) > 9 ? (ii).ToString() : "0" + (ii).ToString());

                            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr VALIGN='TOP' ALIGN='CENTER' id='" + RuaAtual + "' style='display:none'>"));
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpSemAlign' valign='middle' align='right'>"));

                            for (int idxx = andares; idxx > 0; idxx--)
                            {
                                string andarTmp = ((idxx) > 9 ? (idxx).ToString() : "0" + (idxx).ToString());

                                if (int.Parse(dt.Compute("Count(CODIGO)", "RUA='"+RuaAtual+"' AND PREDIO='"+ andarTmp +"' AND ATIVO='SIM'").ToString()) > 0)
                                {
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<table  cellspacing='1' celpanding='1' border='1' >"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='font-size:7pt;height;15px; ' valign='top' >"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td>" + idxx + "º"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
                                }
                            }

                            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                            for (int i = 0; i < colunas; i++)
                            {
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td>"));

                                colunaAtual = (Convert.ToInt32(i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString()).ToString();
                                #region andares

                                PlaceHolder1.Controls.Add(new LiteralControl(@"<TABLE BORDER='0' width='100%' heigth='100%'>"));
                                for (int III = andares; III > 0; III--)
                                {
                                    string x = RuaAtual + colunaAtual + (Convert.ToInt32(III) >= 10 ? (III).ToString() : "0" + (III).ToString());
                                    string andarTmp = ((III) > 9 ? (III).ToString() : "0" + (III).ToString());

                                    if (int.Parse(dt.Compute("Count(CODIGO)", "RUA='" + RuaAtual + "' AND PREDIO='" + andarTmp + "' AND ATIVO='SIM'").ToString()) > 0)
                                    {
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"<TR VALIGN='TOP' ALIGN='RIGHT'>"));
                                        DataRow[] row = dt.Select("CODIGO='" + RuaAtual + colunaAtual + (Convert.ToInt32(III) >= 10 ? (III).ToString() : "0" + (III).ToString()) + "'");

                                        string ativo = "";
                                        int qtd = 0;
                                        if (row.Length > 0)
                                        {
                                            ativo = row[0]["ATIVO"].ToString();
                                            qtd = Convert.ToInt32(row[0]["QUANTIDADE"]);
                                        }
                                        if (ativo == "NCC")
                                        {
                                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='naocontados_pequeno' nowrap='nowrap'>" + acertar(0.ToString(), 3)));
                                        }
                                        else
                                        {
                                            if (ativo == "SIM")
                                            {
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<TD class='contados_pequeno'>" + acertar(qtd.ToString(), 3)));
                                                qtdContadaTotal++;
                                            }
                                            else
                                            {
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<TD class='desativados' nowrap=nowrap>&nbsp"));
                                                PlaceHolder1.Controls.Add(new LiteralControl(@"<img src='Images/proibidoX.bmp' height='11px' alt='Posições Inativas'>&nbsp;"));
                                            }
                                        }
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                        PlaceHolder1.Controls.Add(new LiteralControl(@"</TR>"));
                                    }
                                }

                                PlaceHolder1.Controls.Add(new LiteralControl(@"</TABLE>"));
                                #endregion
                            }
                            PlaceHolder1.Controls.Add(new LiteralControl(@"<td style='width:100%; align:Right'>"));

                            #endregion
                        }
                    }
                }

                PlaceHolder1.Controls.Add(new LiteralControl(@"</TABLE>"));
                GraficoInventarioXRua(dt);
            }
            else
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<table width='100%'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
                tblResumo.Visible = false;
            }

            Label1.Text = "Última Atualização: " + DateTime.Now;
            if (CheckBox1.Checked == true)
                Timer1.Enabled = true;
            else
                Timer1.Enabled = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private DataTable AcertarColunas(DataTable dt, int Andares, int Colunas, int RuaInicial, int RuaFinal, bool PegarContagemDeRuas)
    {
        string endDesabilitado = "";
        string IdDepositoPlanta = dt.Rows[0]["IDDEPOSITOPLANTA"].ToString();
        for (int i = RuaInicial; i <= RuaFinal; i++)
        {
            string rua = (Convert.ToInt32(i) < 10 ? "0" + (i).ToString() : (i).ToString());
            for (int iCol = 0; iCol < Colunas; iCol++)
            {
                string coluna = (Convert.ToInt32(iCol + 1) < 10 ? "0" + (iCol + 1).ToString() : (iCol + 1).ToString());
                int qdtAndaesTemp = Convert.ToInt32(dt.Compute("COUNT(CODIGO)", "RUA='" + rua + "' AND COLUNA='" + coluna + "'"));

                if (qdtAndaesTemp < Andares)
                {
                    for (int iproc = 1; iproc <= Andares; iproc++)
                    {
                        string TT = Convert.ToInt32(iproc) < 10 ? "0" + (iproc).ToString() : (iproc).ToString();
                        if (dt.Compute("COUNT(CODIGO)", "RUA='" + rua + "' AND COLUNA='" + coluna + "'" + " AND PREDIO='" + TT + "'").ToString() == "0" )
                        {
                            DataRow dr;
                            dr = dt.NewRow();

                            dr["CODIGO"] = rua + coluna + TT;
                            endDesabilitado += "'" + rua + coluna + TT + "',";
                           // IdDepositoPlanta = dt.Rows[iproc]["IDDEPOSITOPLANTA"].ToString();
                            dr["ATIVO"] = "NAO";
                            dr["RUA"] = rua;
                            dr["COLUNA"] = coluna;
                            dr["PREDIO"] = TT;
                            dr["IDINVENTARIO"] = 0;
                            dr["DESCRICAO"] = "DESATIVADO";
                            dr["QUANTIDADE"] = 0;
                            dr["IDINVENTARIO"] = 0;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
        }


        if (endDesabilitado.Length == 0)
        {
            DataRow[] linhasDeseb = dt.Select("ATIVO='NAO'", "");
            for (int ilinha = 0; ilinha < linhasDeseb.Length; ilinha++)
            {
                endDesabilitado += "'" + linhasDeseb[ilinha]["RUA"].ToString() + linhasDeseb[ilinha]["COLUNA"].ToString() + linhasDeseb[ilinha]["PREDIO"].ToString() + "',";
            }
        }




        if (endDesabilitado.Length > 0)
        {
            endDesabilitado = endDesabilitado.Substring(0, endDesabilitado.Length - 1);


            DataTable dtDesab = new Deposito().NaoContados(IdDepositoPlanta, endDesabilitado);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ATIVO"].ToString() == "NAO")
                {
                    // int x = int.Parse(dt.Compute("COUNT(CODIGO)", "CODIGO='"+ dt.Rows[i]["CODIGO"].ToString() + "'").ToString() +"'");

                    int end = int.Parse(dtDesab.Compute("Count(CODIGO)", "ATIVO='SIM' AND CODIGO='" + dt.Rows[i]["CODIGO"].ToString() + "'").ToString());
                    if (end > 0 && PegarContagemDeRuas == true)
                    {
                        dt.Rows[i]["ATIVO"] = "NCC";
                    }
                }
            }
        }
        return dt;
    }

    public string acertar(string tp, int posicoes)
    {
        if (tp.Length < posicoes)
        {
            for (int i = tp.Length; i < posicoes; i++)
            {
                tp = "&nbsp;" + tp;
            }
        }
        return tp;
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Carregar();
    }

    private Label criaLabel(string tipo, int tamanho, int tamAnterior)
    {
        Label l = new Label();
        if (tamanho > 100)
            tamanho = 100;

        if (tamanho < 0)
            tamanho = 0;

        if (tipo == "R")
        {
            l.Width = Convert.ToInt32(tamanho * 4);
            l.CssClass = "lblContado";
        }
        else
        {
            l.Width = Convert.ToInt32(tamanho * 4);
            l.CssClass = "lblNaoContado";
        }

        l.Text = tamanho.ToString("#0") + "%";

        //if (tamanho < 15)
        //{
        //    l.Width = 15;

        //}

        if (tamanho <= 0)
            l.Text = "0%";

        if (tipo != "R" && (int.Parse((tamanho * 4).ToString()) + int.Parse(tamAnterior.ToString())) < 400)
        {
            l.Width = 400;
        }

        if (tipo != "R" && (int.Parse((tamanho * 4).ToString()) + int.Parse(tamAnterior.ToString())) > 400)
        {
            l.Width = 400;
        }


        return l;
    }

    public void GraficoInventarioXRua(DataTable table)
    {
        int ruas = Convert.ToInt32(table.Compute("max(rua)", ""));
        int MinimoDeRuas = Convert.ToInt32(table.Compute("min(rua)", ""));
        ListBox1.Visible = false;
        //int h = 0;
        ph3.Controls.Add(new LiteralControl(@"<table width='400px'>"));
        ph3.Controls.Add(new LiteralControl(@"<tr>"));

        ph3.Controls.Add(new LiteralControl(@"<td><B>RUA</b>"));
        ph3.Controls.Add(new LiteralControl(@"</td>"));

        ph3.Controls.Add(new LiteralControl(@"<td>"));
        ph3.Controls.Add(new LiteralControl(@"</td>"));
        ph3.Controls.Add(new LiteralControl(@"</tr>"));

        int idx = 0;
        int totCont = 0;
        int totNaoContadas = 0;
        for (int i = MinimoDeRuas; i <= ruas; i++)
        {

            string srua = (i.ToString().Length < 2 ? "0" + i.ToString() : i.ToString());
            string[] m = ListBox1.Items[idx].Text.Split('|');


            int totallinha = Convert.ToInt32(m[0]) + Convert.ToInt32(m[1]);

            totCont += totallinha;

            if (int.Parse(m[1]) < 0)
                m[1] = "0";
            totNaoContadas += Convert.ToInt32(m[1]);


            int linha = Convert.ToInt32(m[0]);

            ph3.Controls.Add(new LiteralControl(@"<tr>"));
            ph3.Controls.Add(new LiteralControl(@"<td style='text-align:left' ><b>" + srua.Replace("00", "0")));
            ph3.Controls.Add(new LiteralControl(@"</b></td>"));

            ph3.Controls.Add(new LiteralControl(@"<td NOWRAP=NOWRAP>"));

            decimal calc = Convert.ToDecimal(0);
            if (linha == 0 || totallinha == 0)
                calc = 0;
            else
                calc = (Convert.ToDecimal((linha)) / Convert.ToDecimal((totallinha))) * 100;

            Label ll = criaLabel("R", Convert.ToInt32(calc), Convert.ToInt32(0));
            ph3.Controls.Add(ll);            
            ph3.Controls.Add(criaLabel("", Convert.ToInt32(100) - Convert.ToInt32(calc), Convert.ToInt32(ll.Width.ToString().Replace("px", ""))));
            ph3.Controls.Add(new LiteralControl(@"</td>"));

            ph3.Controls.Add(new LiteralControl(@"</tr>"));
            //h++;'
            idx++;
        }
        ph3.Controls.Add(new LiteralControl(@"</table>"));

        //lblTotalEnderecos.Text = totCont.ToString();

        totCont = int.Parse(table.Compute("COUNT(CODIGO)", " (ATIVO='SIM' OR ATIVO='NCC') AND QUANTIDADE>=0").ToString());

        totNaoContadas = int.Parse(table.Compute("COUNT(CODIGO)", " ATIVO='NCC' AND QUANTIDADE=0").ToString());

        lblTotalEnderecos.Text = table.Compute("COUNT(CODIGO)", " ATIVO='SIM' OR ATIVO='NCC'").ToString();
        
        
        
        lblPosicoesContadas.Text = (totCont - totNaoContadas).ToString();
        Label4.Text = (((Convert.ToDecimal(totCont) - Convert.ToDecimal(totNaoContadas)) / Convert.ToDecimal(totCont)) * 100).ToString("#0.00") + "%";
    }
        
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
            if (CheckBox1.Checked == true)
                Timer1.Enabled = true;        
            else
                Timer1.Enabled = false;
    }
}
