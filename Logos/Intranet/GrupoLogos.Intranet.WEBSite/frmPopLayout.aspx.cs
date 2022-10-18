using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;

public partial class frmPopLayout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//               SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper().Substring(1, 20)), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
                //Carregar();
            }
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Carregar();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    public string CriarBotaoExpandir(string rua, bool expandAll, string RI, string RF)
    {
        string m = "";
        if (expandAll)
        {
            PlaceHolder2.Controls.Add(new LiteralControl(@"<center><div id='dvExpandir' style='font-size:9px;cursor:Hand;background-image:url(Images/seta.jpg); height:12px; width:14px;' OnClick=ExpandirAll('" + RI + "','" + RF + "');>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</div></center>"));
            m = "";
        }
        else
        {
            m = "<b><div style='font-size:11px;cursor:Hand' OnClick=Expandir('" + rua + "');>";
            m += rua + "</div></b>";
        }
        return m;
    }

    public void  CriarBotaoAtivoIniativo(bool Ativo, int IdPlantaLocalizacao, string end, string existeProduto)
    {
        if (Ativo == true)
        {
            ImageButton b = new ImageButton();
            b.ID = IdPlantaLocalizacao.ToString();            
            b.BackColor = System.Drawing.Color.Transparent;
            b.ImageUrl = "Images/ativovazio.gif";
            b.BorderWidth = 0;
            b.BorderStyle = BorderStyle.Solid;
            b.BorderColor = System.Drawing.Color.LightGray;
            b.Font.Name = "Verdana";
            b.Font.Size = 7;
            b.Width = 16;
            b.Height = 16;                        
            b.Style.Add(HtmlTextWriterStyle.Cursor, "Hand");
            b.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            b.Enabled = false;
            b.ToolTip = "Endereço: " + end; 

            if (existeProduto == "SIM")
            {
                b.ImageUrl = "Images/pallet01.jpg";
                b.Height = 16;
                b.Enabled = true;
                b.ToolTip = "Endereço: " + end; 
            }
            PlaceHolder2.Controls.Add(b);

            b.Attributes.Add("onClick", "openModalWindow('frmPopVerProdutos.aspx?id="+end+"' , 'strArgument', '420', '450');return false;");
        }
        else
        {

            ImageButton b = new ImageButton();
            b.ID = IdPlantaLocalizacao.ToString();
            b.BorderWidth = 1;
            b.BorderStyle = BorderStyle.Solid;
            b.BorderColor = System.Drawing.Color.LightGray;
            b.Font.Name = "Verdana";
            b.Font.Size = 7;
            b.Width = 16;
            b.Height = 15;
            b.ImageUrl = "Images/proibidoX.bmp";
            b.Enabled = false;
            b.Style.Add(HtmlTextWriterStyle.Cursor, "Hand");
            b.ToolTip = "Posição Inativa. Endereço: " + end;
            b.Enabled = false;
            PlaceHolder2.Controls.Add(b);

        }        
    }

    protected void Button_ClickAtivar(object sender, ImageClickEventArgs e)
    {
        ImageButton b = (ImageButton)sender;
        Sistran.Library.GetDataTables.ExecutarSemRetorno("UPDATE DEPOSITOPLANTALOCALIZACAO SET ATIVO ='SIM' WHERE IDDEPOSITOPLANTALOCALIZACAO=" + b.ID.ToString() , "");        
        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('ITEM HABILITADO COM SUCESSO')", true);
        Carregar();
    }  

    public void Carregar()
    {
        PlaceHolder2.Controls.Clear();
        try
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU LAYOUT.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
            DataTable dt = null;
            dt = new SistranBLL.Deposito().Layout(Request.QueryString["idPlantaLocalizacao"].ToString(), Request.QueryString["rua"].ToString());

            string strsql = " SELECT DISTINCT(ISNULL(FIM,0) )FROM DEPOSITOPLANTALEIAUTE DPL ";
            strsql += " INNER JOIN DEPOSITOPLANTA DP ON DP.IDDEPOSITOPLANTA = DPL.IDDEPOSITOPLANTA ";            
            strsql += " WHERE  TIPO='ANDAR' AND DPL.IDDEPOSITOPLANTA =" + Request.QueryString["idPlantaLocalizacao"].ToString();

            int andares = Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            PlaceHolder2.Controls.Clear();
            if (dt.Rows.Count > 0)
            {
                int MinimoDeRuas = Convert.ToInt32(dt.Compute("MIN(rua)", ""));
                int ruas = Convert.ToInt32(dt.Compute("max(rua)", ""));
                int colunas = Convert.ToInt32(dt.Compute("max(coluna)", ""));
                AcertarColunas(dt, andares, colunas, MinimoDeRuas, ruas);                

                string tempI = (MinimoDeRuas.ToString().Length < 2 ? "0" + MinimoDeRuas.ToString() : MinimoDeRuas.ToString());
                string tempF = (ruas.ToString().Length < 2 ? "0" + ruas.ToString() : ruas.ToString());

                PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=90% runat='server' border='0'>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' width='1%' ><b>RUA</b>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' style='font-size:9px' colspan='" + (colunas + 1).ToString() + "'><b>COLUNAS</b>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp'>"));
                PlaceHolder2.Controls.Add(new LiteralControl(CriarBotaoExpandir("", true, tempI, tempF)));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                string colunaAtual = "";

                for (int i = 0; i < colunas; i++)
                {
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' style='font-size:9px' ><b><CENTER>" + (Convert.ToInt32(i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString()).ToString()));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<CENTER></b></td>"));
                }
                
                string RuaAtual = "";
                int qtdContadaTotal = 0;

                for (int ii = MinimoDeRuas; ii <= ruas; ii++)
                {
                    for (int iLinhaExtra = 0; iLinhaExtra < 4; iLinhaExtra++)
                    {
                        #region Linha 1
                        if (iLinhaExtra == 0)
                        {
                            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr VALIGN='TOP' ALIGN='CENTER'>"));

                            RuaAtual = ((ii) > 9 ? (ii).ToString() : "0" + (ii).ToString());

                            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpSemAlign' valign='middle' align='center' >"));

                            PlaceHolder2.Controls.Add(new LiteralControl(CriarBotaoExpandir(RuaAtual, false, "", "")));
                            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                            int qtdContadaTotalLinha = 0;
                            int qtdNaoContadosLinha = 0;

                            for (int i = 0; i < colunas; i++)
                            {

                                colunaAtual = (Convert.ToInt32(i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString()).ToString();

                                int qtdContada = 0;
                                int qtddesativados = 0, naoExiste = 0, Contados = 0;

                                DataRow[] ow = dt.Select("RUA='" + RuaAtual + "' AND COLUNA='" + colunaAtual + "'");
                                for (int iDX = 0; iDX < ow.Length; iDX++)
                                {
                                    if (ow[iDX]["ATIVO"].ToString() == "NAO")
                                    {
                                        qtddesativados++;
                                    }
                                    else if (ow[iDX]["ATIVO"].ToString() == "SIM")
                                    {
                                        Contados++;
                                    }                                    
                                }

                                naoExiste = andares - (Contados + qtddesativados);

                                if (Contados > 0)
                                {
                                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='contados_ppequeno'>"));
                                    PlaceHolder2.Controls.Add(new LiteralControl(@"<img src='Images/ativovazio.gif' height='16' width='16'>"));
                              
                                    qtdContadaTotalLinha += qtdContada;
                                }
                                else if (qtddesativados == andares)
                                {
                                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='desativados'>"));
                                    PlaceHolder2.Controls.Add(new LiteralControl(@"<img src='Images/proibidoX.bmp' height='16' width='16' alt='Posições Inativas'>"));
                                }
                                
                                qtdNaoContadosLinha += naoExiste;
                                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                            }
                           
                        #endregion
                        }
                        else if (iLinhaExtra == 1 || iLinhaExtra == 3)
                        {
                            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr id='tr" + RuaAtual + iLinhaExtra + "' style='display:none'>"));
                            PlaceHolder2.Controls.Add(new LiteralControl(@"<td colspan='" + (colunas + 2) + "' class='Divisoria2'><B>"));
                            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
                        }
                        else if (iLinhaExtra == 2)
                        {
                            #region linha extra

                            RuaAtual = ((ii) > 9 ? (ii).ToString() : "0" + (ii).ToString());

                            if (Request.QueryString["rua"].ToString() != "" && Request.QueryString["rua"].ToString() != "TODOS")
                            {
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr VALIGN='TOP' ALIGN='CENTER' id='" + RuaAtual + "' style='display:block'>"));
                            }
                            else
                            {
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr VALIGN='TOP' ALIGN='CENTER' id='" + RuaAtual + "' style='display:none'>"));
                            }

                            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpSemAlign' valign='buttom' align='right'>"));

                            for (int idxx = andares; idxx > 0; idxx--)
                            {
                                string andarTmp = ((idxx) > 9 ? (idxx).ToString() : "0" + (idxx).ToString());
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<table  cellspacing='1' celpanding='1' border='1' heigth='100%' style='border-style: solid; border-color:gray' >"));
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='font-size:7pt;' >"));
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td>" + idxx + "º"));
                                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                                PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
                                PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));
                            }

                            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                            for (int i = 0; i < colunas; i++)
                            {
                                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpSemAlign'>"));

                                colunaAtual = (Convert.ToInt32(i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString()).ToString();
                                #region andares

                                PlaceHolder2.Controls.Add(new LiteralControl(@"<TABLE BORDER='0' width='1%' heigth='1%' cellspacing='0' cellpading='0'>"));
                                for (int III = andares; III > 0; III--)
                                {
                                    string x = RuaAtual + colunaAtual + (Convert.ToInt32(III) >= 10 ? (III).ToString() : "0" + (III).ToString());
                                    string andarTmp = ((III) > 9 ? (III).ToString() : "0" + (III).ToString());
                                    
                                    DataRow[] IDDEPOSITOPLANTALOCALIZACAO = dt.Select("CODIGO='" + RuaAtual + colunaAtual + (Convert.ToInt32(III) >= 10 ? (III).ToString() : "0" + (III).ToString()) + "'");

                                        PlaceHolder2.Controls.Add(new LiteralControl(@"<TR VALIGN='TOP' ALIGN='RIGHT'>"));
                                        DataRow[] row = dt.Select("CODIGO='" + RuaAtual + colunaAtual + (Convert.ToInt32(III) >= 10 ? (III).ToString() : "0" + (III).ToString()) + "'");

                                        string ativo = "";
                                        if (row.Length > 0)
                                        {
                                            ativo = row[0]["ATIVO"].ToString();
                                        }

                                        if (ativo == "SIM")
                                        {
                                            if (IDDEPOSITOPLANTALOCALIZACAO.Length > 0 && IDDEPOSITOPLANTALOCALIZACAO[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString()!="")
                                            {
                                                PlaceHolder2.Controls.Add(new LiteralControl(@"<TD class='contados_ppequeno'>"));
                                                CriarBotaoAtivoIniativo(true, int.Parse(IDDEPOSITOPLANTALOCALIZACAO[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString()), x, IDDEPOSITOPLANTALOCALIZACAO[0]["PRODUTO"].ToString());
                                            }
                                            qtdContadaTotal++;
                                        }
                                        else
                                        {
                                            if (IDDEPOSITOPLANTALOCALIZACAO.Length > 0 && IDDEPOSITOPLANTALOCALIZACAO[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() != "")
                                            {
                                                PlaceHolder2.Controls.Add(new LiteralControl(@"<TD class='desativados' nowrap=nowrap>"));
                                                CriarBotaoAtivoIniativo(false, int.Parse(IDDEPOSITOPLANTALOCALIZACAO[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString()), x,"" );
                                            }                                          
                                        }

                                        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                                        PlaceHolder2.Controls.Add(new LiteralControl(@"</TR>"));
                                }

                                PlaceHolder2.Controls.Add(new LiteralControl(@"</TABLE>"));
                                #endregion
                            }
                            PlaceHolder2.Controls.Add(new LiteralControl(@"<td style='width:100%; align:Right'>"));

                            //lblAtivos.Text = dt.Compute("Count(codigo)", "ATIVO='SIM'").ToString();
                            //lblInativos.Text = dt.Compute("Count(codigo)", "ATIVO='NAO'").ToString();
                            //lblTotal.Text = dt.Compute("Count(codigo)", "").ToString();
                            #endregion
                        }

                    }

                }
                PlaceHolder2.Controls.Add(new LiteralControl(@"</TABLE>"));
            }
            else
            {
                PlaceHolder2.Controls.Add(new LiteralControl(@"<table width='100%'>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));
            }

            PlaceHolder2.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private DataTable AcertarColunas(DataTable dt, int Andares, int Colunas, int RuaInicial, int RuaFinal)
    {
        //string endDesabilitado = "";
        string IdDepositoPlanta = "";
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
                        if (dt.Compute("COUNT(CODIGO)", "RUA='" + rua + "' AND COLUNA='" + coluna + "'" + " AND PREDIO='" + TT + "'").ToString() == "0")
                        {
                            DataRow dr;
                            dr = dt.NewRow();

                            dr["CODIGO"] = rua + coluna + TT;
                            
                            IdDepositoPlanta = dt.Rows[i]["IDDEPOSITOPLANTA"].ToString();
                            dr["ATIVO"] = "NAO";
                            dr["RUA"] = rua;
                            dr["COLUNA"] = coluna;
                            dr["PREDIO"] = TT;

                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DEPOSITOPLANTALOCALIZACAO");
                            dr["IDDEPOSITOPLANTALOCALIZACAO"] = id;
                            string zstr = "INSERT INTO DEPOSITOPLANTALOCALIZACAO ";
                            zstr += " ( ";
                            zstr += " IDDEPOSITOPLANTALOCALIZACAO,";
                            zstr += " IDDEPOSITOPLANTA,";
                            zstr += " DESCRICAO,";
                            zstr += " CODIGO,";
                            zstr += " LARGURA,";
                            zstr += " PROFUNDIDADE,";
                            zstr += " ALTURA,";
                            zstr += " CAPACIDADEEMKG,";
                            zstr += " MULTIPLOSPRODUTOS,";
                            zstr += " DATADECADASTRO,";
                            zstr += " ATIVO";
                            zstr += " )";
                            zstr += " VALUES";
                            zstr += " (";
                            zstr += id + " ,";
                            zstr += Request.QueryString["idPlantaLocalizacao"] + " ,";
                            zstr += " '" + rua + coluna + TT + "',";
                            zstr += " '" + rua + coluna + TT + "',";
                            zstr += " 0.0,";
                            zstr += " 0.0,";
                            zstr += " 0.0,";
                            zstr += " 0.0,";
                            zstr += " 'SIM',";
                            zstr += " GETDATE(),";
                            zstr += " 'NAO'";
                            zstr += " ) ";

                            Sistran.Library.GetDataTables.ExecutarSemRetorno(zstr, "");
                            dt.Rows.Add(dr);

                        }
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

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
}