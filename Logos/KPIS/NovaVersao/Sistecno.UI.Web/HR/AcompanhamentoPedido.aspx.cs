using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebSite.HR
{
    public partial class AcompanhamentoPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                carregar();
        }


        public void abrirTable()
        {
            this.ph.Controls.Add(new LiteralControl("<table class='table' cellspacing=1 celpanding=1 width='100%' border=0;>"));
        }

        public void criarLinha(bool titulo)
        {
            if (titulo)
            {
                this.ph.Controls.Add(new LiteralControl("<tr valign='top' style='background-color:#EEC591; height:15px'>"));
            }
            else
            {
                this.ph.Controls.Add(new LiteralControl("<tr>"));
            }
        }


        public void criarCelula(bool titulo, string texto, ealinhamento alinhamento, int colspan, bool numeral)
        {
            if (colspan == 0)
            {
                colspan = 1;
            }
            if (titulo)
            {
                this.ph.Controls.Add(new LiteralControl(string.Concat(new object[] { "<td class='tdpCabecalho' align='", alinhamento.ToString(), "' nowrap=nowrap colspan=", colspan, " style='font-size:7pt;'  >", texto })));
                this.ph.Controls.Add(new LiteralControl("</td>"));
            }
            else
            {
                if (numeral)
                {
                    this.ph.Controls.Add(new LiteralControl(string.Concat(new object[] { "<td class='tdpR' nowrap=nowrap align='", alinhamento.ToString(), "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto })));
                }
                else
                {
                    this.ph.Controls.Add(new LiteralControl(string.Concat(new object[] { "<td class='tdp' nowrap=nowrap align='", alinhamento.ToString(), "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto })));
                }
                this.ph.Controls.Add(new LiteralControl("</td>"));
            }
        }

        public void fecharLinha()
        {
            this.ph.Controls.Add(new LiteralControl("</tr>"));
        }

    
        public void fecharTable()
        {
            this.ph.Controls.Add(new LiteralControl("</table>"));
        }



        public enum ealinhamento
        {
            left,
            center,
            right
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            carregar();
        }

        private void carregar()
        {
            try
            {
                lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ") + " (" + Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".ASPX", "") + ")";

                CentralWssLogos.wsExecutarComado servLogos = new CentralWssLogos.wsExecutarComado();
                DataTable dt = new CentralWssLogos.wsExecutarComado().ExecSql("SISTECNO", "@ONCETSIS12122014", "SELECT * FROM  PEDIDOS_NF_HR()");


                abrirTable();

                //cabeçalho
                ph.Controls.Add(new LiteralControl("<tr valign='top' style='background-color:#B0C4DE; border-bottom:2px solid white '>"));
                ph.Controls.Add(new LiteralControl("<td colspan=1 style='background-color:#B0C4DE;text-align:center;font-weight: bold; '></td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=1 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff'>REC. PEDIDOS</td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=3 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff'>SEPARAÇÃO DE PEDIDOS</td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=2 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff'>EMISSÃO NFE</td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=2 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff'>EMBARQUE</td>"));
                ph.Controls.Add(new LiteralControl("</tr>"));

                ph.Controls.Add(new LiteralControl("<tr valign='top' style='background-color:#B0C4DE; border-bottom:2px solid white '>"));

                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff '>Data Planejada</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Aguardando Liberaçao</td>"));

                //ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; '>Liberado Para Separacao</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Aguardando Separação</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Em Separção</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Separação Finalizada</td>"));

                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Aguardando Emissão NFe</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Notas Emitidas</td>"));

                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Aguardando Embarque</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Em Entrega</td>"));
                ph.Controls.Add(new LiteralControl("</tr>"));
                // final do cabeçalho



                DataView defaultView = dt.DefaultView.ToTable(true, "Data").DefaultView;
                defaultView.Sort = "Data asc";
                DataTable tb = defaultView.ToTable();
                /*
                                DateTime primeiraData = DateTime.Parse( tb.Compute("min(data)", "").ToString());
                                DateTime ultimaData = DateTime.Parse( tb.Compute("max(data)", "").ToString());

                                while (ultimaData >= primeiraData)
                                {
                                    DataRow[] x = tb.Select("DATA = '" + primeiraData.ToString("yyyy-MM-dd") + "'", "");
                                    if (x == null || x.Length == 0)
                                    {
                                        DataRow r = tb.NewRow();
                                        r[0] = primeiraData;
                                        tb.Rows.Add(r);
                                    }                    
                                   primeiraData =  primeiraData.AddDays(1);
                                }
                
                                tb.DefaultView.Sort = "Data asc";
                                tb = tb.DefaultView.ToTable();

                */
                string vtmp = "";
                string cssLetra = "";
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                        ph.Controls.Add(new LiteralControl("<tr style='backgroud-color:white'>"));
                    else
                        ph.Controls.Add(new LiteralControl("<tr>"));


                    if ((DateTime.Parse(tb.Rows[i]["data"].ToString())).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        cssLetra = ";color:#FF8C00;font-weight: bold;";

                    else if ((DateTime.Parse(tb.Rows[i]["data"].ToString())) < DateTime.Now)
                        cssLetra = ";color:#FF0000;font-weight: bold;";
                    else
                        cssLetra = ";color:#000000;font-weight: bold;";


                    ph.Controls.Add(new LiteralControl("<td  style='text-align:center; border-right: 1px solid  silver" + cssLetra + "''>" + DateTime.Parse(tb.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy") + "</td>"));

                    vtmp = dt.Compute("count(IdDocumento)", "SITUACAO='AGUARDANDO LIBERACAO' and Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td style='text-align:right ; border-right: 1px solid  silver"+cssLetra+"' >" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                    vtmp = dt.Compute("count(IdDocumento)", "SITUACAO='LIBERADO PARA SEPARACAO' and Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    vtmp = dt.Compute("count(IdDocumento)", "MAPABAIXADO='NAO' and Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>")); //EM SEPARAÇAO

                    vtmp = dt.Compute("count(IdDocumento)", "MAPABAIXADO='SIM' and Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>")); // SEPARACAO FINALIZADA

                    vtmp = dt.Compute("SUM(AGURDANDO_EMISSAO_NFE)", "MAPABAIXADO='SIM' and Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));//aGUARDANDO EMISSAO NFE

                    vtmp = dt.Compute("SUM(NFE_EMITIDAS)", "MAPABAIXADO='SIM' and Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));//aGUARDANDO EMISSAO NFE

                    vtmp = dt.Compute("sum(AGURADANDO_EMBARQUE)", "Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>")); // Aguardando Embarque                   

                    vtmp = dt.Compute("sum(EMENTREGA)", "Data='" + tb.Rows[i]["Data"] + "'").ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>")); // EM ENTREGA                 

                    ph.Controls.Add(new LiteralControl("<tr>"));
                }

                #region Totaliza

                ph.Controls.Add(new LiteralControl("<tr style='style='background-color:#B0C4DE;text-align:right;font-weight: bold;'>"));

                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff'>TOTAL</td>"));

                vtmp = dt.Compute("count(IdDocumento)", "SITUACAO='AGUARDANDO LIBERACAO'").ToString();
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("count(IdDocumento)", "SITUACAO='LIBERADO PARA SEPARACAO'").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("count(IdDocumento)", "MAPABAIXADO='NAO'").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>")); //EM SEPARAÇAO

                vtmp = dt.Compute("count(IdDocumento)", "MAPABAIXADO='SIM'").ToString();
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>")); // SEPARACAO FINALIZADA

                vtmp = dt.Compute("SUM(AGURDANDO_EMISSAO_NFE)", "MAPABAIXADO='SIM'").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));//aGUARDANDO EMISSAO NFE

                vtmp = dt.Compute("SUM(NFE_EMITIDAS)", "MAPABAIXADO='SIM'").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + vtmp + "</td>"));//aGUARDANDO EMISSAO NFE

                vtmp = dt.Compute("sum(AGURADANDO_EMBARQUE)", "").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>" + (vtmp == "0" ? "" : vtmp) + "</td>")); // Aguardando Embarque                   


                vtmp = dt.Compute("sum(EMENTREGA)", "").ToString();

                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>")); // EM ENTREGA                 

                ph.Controls.Add(new LiteralControl("<tr>"));
                #endregion


                fecharTable();
                Label1.Text = "Última Atulização: " + DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 

 


 



    }
}