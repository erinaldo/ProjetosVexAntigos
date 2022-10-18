using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Sistecno.UI.Web.HR
{
    public partial class AcompanhamentoPedidoV2 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {         
            if(! IsPostBack)
                carregar();         
        }


        public void abrirTable()
        {
            this.ph.Controls.Add(new LiteralControl("<table class='table' cellspacing=1 celpanding=1 width='100%; border:1px solid silver' border=0;>"));
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

      

        
        private void carregar()
        {
            ph.Controls.Clear();
            string CorpoEmail = "";
            try
            {
                int em_sep = 0;
                int em_ag = 0;
                lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ") + " (" + Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".ASPX", "") + ")";

                CentralWssLogos.wsExecutarComado servLogos = new CentralWssLogos.wsExecutarComado();
                DataTable dt = new CentralWssLogos.wsExecutarComado().ExecSql("SISTECNO", "@ONCETSIS12122014", "SELECT * FROM  VW_LIBERACAO_PEDIDOS WHERE IDCLIENTE = 150000 ORDER BY 2");
                
                abrirTable();

                //cabeçalho
                ph.Controls.Add(new LiteralControl("<tr valign='top' style='background-color:#B0C4DE; border-bottom:1px solid silver '>"));
                //ph.Controls.Add(new LiteralControl("<td colspan=1 style='background-color:#B0C4DE;text-align:center;font-weight: bold; '></td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=3 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff'>RECEBIMENTO DE PEDIDOS</td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=5 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 5px solid  #ffffff'>SEPARAÇÃO DE PEDIDOS</td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=2 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 5px solid  #ffffff'>EMISSÃO NFe</td>"));
                ph.Controls.Add(new LiteralControl("<td colspan=3 style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff'>EMBARQUE</td>"));
                ph.Controls.Add(new LiteralControl("</tr>"));

                ph.Controls.Add(new LiteralControl("<tr valign='top' style='background-color:#B0C4DE; border-bottom:2px solid white '>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:center;font-weight: bold; border-right: 1px solid  #ffffff '>Data Planejada</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Total de Pedidos</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Aguardando Liberação</td>"));

                
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Liberado Para Separação</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Em Separção</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Separação Finalizada</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Liberado Para Embalagem</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 5px solid  #ffffff'>Pedidos Finalizados</td>"));

                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Aguardando Emissão NFe</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 5px solid  #ffffff'>NFe Emitidas</td>"));

                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Aguardando Embarque</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Em Entrega</td>"));
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold; border-right: 1px solid  #ffffff'>Entregue</td>"));
                ph.Controls.Add(new LiteralControl("</tr>"));
                // final do cabeçalho


                                
                string vtmp = "";
                string cssLetra = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["QUANTIDADEDEPEDIDOS"].ToString() == dt.Rows[i]["ENTREGUE"].ToString())
                        continue;

                    bool primo = false;

                    if (i % 2 > 0)
                        primo = true;

                     ph.Controls.Add(new LiteralControl("<tr>"));

                     if ((DateTime.Parse(dt.Rows[i]["data"].ToString())).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                     {
                         cssLetra = ";color:#FF8C00;font-weight: bold;text-decoration: underline;background-color:#F0E68C;";
                         primo = false;
                     }
                     else if ((DateTime.Parse(dt.Rows[i]["data"].ToString())) < DateTime.Now)
                         cssLetra = ";color:#FF0000;font-weight: bold;";
                     else
                         cssLetra = ";color:#006400;font-weight: bold;";

                     if (primo)
                         cssLetra = cssLetra + "background-color:#F8F8FF; ";

                    ph.Controls.Add(new LiteralControl("<td  style='text-align:center; border: 1px solid  silver" + cssLetra + "''>" + DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy") + "</td>"));
                    ph.Controls.Add(new LiteralControl("<td  style='border: 1px solid  silver; text-align:right" + cssLetra + "''>" + dt.Rows[i]["QUANTIDADEDEPEDIDOS"].ToString() + "</td>"));

                    vtmp = dt.Rows[i]["AGUARDANDO_LIBERACAO"].ToString();
                    ph.Controls.Add(new LiteralControl("<td style='text-align:right ; border: 1px solid  silver" + cssLetra + "' >" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                    vtmp = dt.Rows[i]["LIBERADO_PARA_SEPARACAO"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    vtmp = dt.Rows[i]["EM_SEPARACAO"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    vtmp = dt.Rows[i]["SEPARACAO_FINALIZADA"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    vtmp = dt.Rows[i]["LIBERADO_PARA_EMBALAGEM"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                    vtmp = dt.Rows[i]["PEDIDO_FATURADO"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                    //ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border-right: 1px solid  silver; background-color:#000; color:#FFF;'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    em_sep = int.Parse(dt.Rows[i]["EM_SEPARACAO"].ToString());
                    em_ag = int.Parse(dt.Rows[i]["AGUARDADO_EMISSAO_NFE"].ToString());

                    em_ag = (em_ag - em_sep);

                    if (em_ag < 0)
                        vtmp = "0";
                    else
                        vtmp = em_ag.ToString();
                    
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                    //validas as informações de pedidos se tiver inconsistente avisa por e-mail

                    int TotalPedidosLinha = int.Parse(dt.Rows[i]["QUANTIDADEDEPEDIDOS"].ToString());
                    int somaLinha = 0;

                    //somaLinha += int.Parse(vtmp);
                    somaLinha += int.Parse(dt.Rows[i]["AGUARDANDO_LIBERACAO"].ToString());
                    somaLinha += int.Parse(dt.Rows[i]["LIBERADO_PARA_SEPARACAO"].ToString());
                    somaLinha += int.Parse(dt.Rows[i]["EM_SEPARACAO"].ToString());
                    somaLinha += int.Parse(dt.Rows[i]["SEPARACAO_FINALIZADA"].ToString());
                    somaLinha += int.Parse(dt.Rows[i]["PEDIDO_FATURADO"].ToString());
                    somaLinha += int.Parse(dt.Rows[i]["LIBERADO_PARA_EMBALAGEM"].ToString());

                    if (TotalPedidosLinha != somaLinha)
                    {
                        try
                        {
                            CorpoEmail += "Verificar a Data: " + DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy") + " - Quantidade de Pedidos" + "<br>";
                           // frwSistecno.EmailsLogs.EnviarErrosLogs("moises@sistecno.com.br", "moises@sistecno.com.br", "Divergência Liberação de Pedido", "Verificar a Data: " + DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy") + " - Quantidade de Pedidos", "Divergência Liberação de Pedido");
                        }
                        catch (Exception)
                        {
                        }
                    }

                    int pedFimLinha = int.Parse(dt.Rows[i]["PEDIDO_FATURADO"].ToString());

                    em_sep = int.Parse(dt.Rows[i]["EM_SEPARACAO"].ToString());
                    em_ag = int.Parse(dt.Rows[i]["AGUARDADO_EMISSAO_NFE"].ToString());

                    em_ag = (em_ag - em_sep);

                    if (em_ag < 0)
                        em_ag = 0;                   

                    int notaLinha = int.Parse(dt.Rows[i]["NFE_EMITIDAS"].ToString()) +  em_ag;


                    if (pedFimLinha != notaLinha)
                    {
                        try
                        {



                            CorpoEmail += "Verificar a Data: " + DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy") + " - EMISSÃO NFe" + "<br>";

                            //frwSistecno.EmailsLogs.EnviarErrosLogs("moises@sistecno.com.br", "moises@sistecno.com.br", "Divergência Liberação de Pedido", "Verificar a Data: " + DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy") + " - EMISSÃO NFe", "Divergência Liberação de Pedido");
                        }
                        catch (Exception)
                        {
                        }

                       
                    }


                    int nfeEmitLinha = int.Parse(dt.Rows[i]["NFE_EMITIDAS"].ToString());
                    int entregueEmEntregAguembarq = int.Parse(dt.Rows[i]["AGUARDANDO_EMBARQUE"].ToString()) + int.Parse(dt.Rows[i]["EM_ENTREGA"].ToString()) + int.Parse(dt.Rows[i]["ENTREGUE"].ToString());

                    if(nfeEmitLinha!=entregueEmEntregAguembarq)
                    {
                        try
                        {
                            CorpoEmail+="Verificar a Data: " + DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy") + " - EMBARQUE" + "<br>";

                            //frwSistecno.EmailsLogs.EnviarErrosLogs("moises@sistecno.com.br", "moises@sistecno.com.br", "Divergência Liberação de Pedido", "Verificar a Data: " + DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy") + " - EMBARQUE", "Divergência Liberação de Pedido");
                        }
                        catch (Exception)
                        {
                        }
                    }

                    ////////////////////////////////////////////////////////////////////////////////

                    //vtmp = dt.Rows[i]["AGUARDADO_EMISSAO_NFE"].ToString();

                    vtmp = dt.Rows[i]["NFE_EMITIDAS"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    vtmp = dt.Rows[i]["AGUARDANDO_EMBARQUE"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    vtmp = dt.Rows[i]["EM_ENTREGA"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    vtmp = dt.Rows[i]["ENTREGUE"].ToString();
                    ph.Controls.Add(new LiteralControl("<td  style='text-align:right; border: 1px solid  silver" + cssLetra + "'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                    ph.Controls.Add(new LiteralControl("<tr>"));
                }

                #region Totaliza

                ph.Controls.Add(new LiteralControl("<tr style='style='background-color:#B0C4DE;text-align:right;font-weight: bold;'>"));

                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff'>TOTAL</td>"));

                vtmp = dt.Compute("sum(QUANTIDADEDEPEDIDOS)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(AGUARDANDO_LIBERACAO)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                vtmp = dt.Compute("sum(LIBERADO_PARA_SEPARACAO)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(EM_SEPARACAO)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                vtmp = dt.Compute("sum(SEPARACAO_FINALIZADA)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(LIBERADO_PARA_EMBALAGEM)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(PEDIDO_FATURADO)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));


                //ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>--</td>"));

                em_sep = int.Parse(dt.Compute("sum(EM_SEPARACAO)", "QUANTIDADEDEPEDIDOS<>entregue").ToString());
                em_ag = int.Parse(dt.Compute("sum(AGUARDADO_EMISSAO_NFE)", "QUANTIDADEDEPEDIDOS<>entregue").ToString());

                em_ag = (em_ag - em_sep);

                if (em_ag < 0)
                    vtmp = "0";
                else
                    vtmp = em_ag.ToString();


                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(NFE_EMITIDAS)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff'>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(AGUARDANDO_EMBARQUE)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(EM_ENTREGA)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                vtmp = dt.Compute("sum(ENTREGUE)", "QUANTIDADEDEPEDIDOS<>entregue").ToString();
                ph.Controls.Add(new LiteralControl("<td  style='background-color:#B0C4DE;text-align:right;font-weight: bold;border-right: 1px solid  #ffffff '>" + (vtmp == "0" ? "" : vtmp) + "</td>"));

                
                ph.Controls.Add(new LiteralControl("<tr>"));
                #endregion


                fecharTable();
               //Control uc = new Control();

                int totPed = int.Parse(dt.Compute("SUM(QUANTIDADEDEPEDIDOS)", "DATA<='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and QUANTIDADEDEPEDIDOS<>entregue").ToString());
                int totPedEntregues = int.Parse(dt.Compute("SUM(ENTREGUE)", "DATA<='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and QUANTIDADEDEPEDIDOS<>entregue").ToString());


                //int totPed = int.Parse(dt.Compute("SUM(QUANTIDADEDEPEDIDOS)", "QUANTIDADEDEPEDIDOS<>entregue").ToString());
                //int totPedEntregues = int.Parse(dt.Compute("SUM(ENTREGUE)", "QUANTIDADEDEPEDIDOS<>entregue").ToString());

                decimal retult = 0;

                if (totPed > 0)
                    retult = ((decimal)totPedEntregues / (decimal)totPed) * 100;
                
                UCGrafico.percentualEntregas = retult;


                 totPed = int.Parse(dt.Compute("SUM(QUANTIDADEDEPEDIDOS)", "DATA<='" + DateTime.Now.ToString("yyyy-MM-dd") +"'").ToString());
                 totPedEntregues = int.Parse(dt.Compute("SUM(PEDIDO_FATURADO)", "DATA<='" + DateTime.Now.ToString("yyyy-MM-dd") + "'").ToString());

                 retult = 0;
                 if (totPed > 0)
                     retult = ((decimal)totPedEntregues / (decimal)totPed) * 100;

                 UCGrafico.percentualSeparacaoPedidos = retult;               
                
               
                Label1.Text = "Última Atulização: " + DateTime.Now.ToString();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                                

        

       protected void Timer2_Tick(object sender, EventArgs e)
        {
            Response.Redirect("AcompanhamentoPedidoV2.aspx?opc=Acompanhamento de Pedidos", false);
        }

        protected void Timer1_Tick1(object sender, EventArgs e)
        {
           // Response.Redirect("AcompanhamentoPedidoV2.aspx?opc=Acompanhamento de Pedidos", false);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            carregar();
        }
    }
}