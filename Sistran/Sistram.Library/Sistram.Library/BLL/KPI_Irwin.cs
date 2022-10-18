using System.Data;
using System;

namespace SistranBLL
{
    public sealed class KPI_Irwin
    {
        public DataTable Pesquisar(string mesAno, string descricao, int idGrupoDeProduto)
        {
            return new SistranDAO.KPI_Irwin().Pesquisar(mesAno, descricao, idGrupoDeProduto);
        }

        public DataTable GerarPlanilha(string MesAno, string idGrupoDeProduto, string versao)
        {
            DataTable dt = new SistranDAO.KPI_Irwin().GerarPlanilha(MesAno, idGrupoDeProduto, versao);
            return MontarTable(dt);
        }


        private DataTable MontarTable(DataTable dt)
        {
            DataTable dtMontado = new DataTable();
            dtMontado.Columns.Add("IDKPIIRWINMOV");
            dtMontado.Columns.Add("DESCRICAO");
            dtMontado.Columns.Add("DESCRICAOUNIDADEDEMEDIDA");
            dtMontado.Columns.Add("DESCRICAOTARGUET");
            dtMontado.Columns.Add("MESANO");
            dtMontado.Columns.Add("DIA");
            dtMontado.Columns.Add("CALCULADO");
            dtMontado.Columns.Add("TOTAL");
            dtMontado.Columns.Add("CHAVE");

            for (int i = 1; i <= 31; i++)
            {
                dtMontado.Columns.Add("DIA" + i.ToString());
                dtMontado.Columns.Add("CODIGO_CELULA" + i.ToString());
            }

            int index = 1;
            DataRow rw = dtMontado.NewRow();

            decimal totalLinha = Convert.ToDecimal(0);
            decimal totalLinhaGeral = Convert.ToDecimal(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["DIA"].ToString() == "1")
                {
                    rw[0] = dt.Rows[i]["IDKPIIRWINMOV"].ToString();
                    rw[1] = dt.Rows[i]["DESCRICAO"].ToString();
                    rw[2] = dt.Rows[i]["DESCRICAOUNIDADEDEMEDIDA"].ToString().ToUpper();

                    if (dt.Rows[i]["CHAVE"].ToString() == "8.1" && dt.Rows[i]["IDGRUPODEPRODUTO"].ToString() == "387")
                        rw[3] = "2500";
                    else if (dt.Rows[i]["CHAVE"].ToString() == "8.1" && dt.Rows[i]["IDGRUPODEPRODUTO"].ToString() == "403")
                        rw[3] = "2000";
                    else if (dt.Rows[i]["CHAVE"].ToString() == "8.1" && dt.Rows[i]["IDGRUPODEPRODUTO"].ToString() == "405")
                        rw[3] = "1500";
                    else if (dt.Rows[i]["CHAVE"].ToString() == "7.2" && dt.Rows[i]["IDGRUPODEPRODUTO"].ToString() == "387")
                        rw[3] = "2500";
                    else if (dt.Rows[i]["CHAVE"].ToString() == "7.2" && dt.Rows[i]["IDGRUPODEPRODUTO"].ToString() == "403")
                        rw[3] = "2000";
                    else if (dt.Rows[i]["CHAVE"].ToString() == "7.2" && dt.Rows[i]["IDGRUPODEPRODUTO"].ToString() == "405")
                        rw[3] = "1000";
                    else
                        rw[3] = dt.Rows[i]["DESCRICAOTARGUET"].ToString().ToUpper();



                    //7.2 Counted Position alterar Target p/ 2500 Office

                    //7.2 Counted Position alterarTarget p/ 1000 Rubbermaid

                    //7.2 Counted Position alterar Target p/ 2000 Décor 


                    rw[4] = dt.Rows[i]["MESANO"].ToString();
                    rw[5] = dt.Rows[i]["DIA"].ToString();
                    rw[6] = dt.Rows[i]["CALCULADO"].ToString();
                    rw[8] = dt.Rows[i]["CHAVE"].ToString();
                }

                rw["CODIGO_CELULA" + index] = dt.Rows[i]["IDKPIIRWINMOV"].ToString();

                if (dt.Rows[i]["CALCULADO"].ToString() == "SIM")
                {
                    if (dt.Rows[i]["Valor"] != DBNull.Value)
                    {
                        //se for % coloca casas decimais
                        if (dt.Rows[i]["DESCRICAOUNIDADEDEMEDIDA"].ToString().ToUpper().Contains("%") || dt.Rows[i]["DESCRICAOTARGUET"].ToString().ToUpper().Contains("%"))
                            //rw["DIA" + index] = (dt.Rows[i]["Valor"].ToString() == "0" ? "*" : Convert.ToDecimal(dt.Rows[i]["Valor"]).ToString("#0.00")+"%");
                            rw["DIA" + index] = Convert.ToDecimal(dt.Rows[i]["Valor"]).ToString("#0.00") + "%";
                        else
                            rw["DIA" + index] = Convert.ToDecimal(dt.Rows[i]["Valor"]).ToString("#0");
                        //rw["DIA" + index] = (dt.Rows[i]["Valor"].ToString() == "0" ? "*" : Convert.ToDecimal(dt.Rows[i]["Valor"]).ToString("#0"));


                        totalLinha += Convert.ToDecimal(dt.Rows[i]["Valor"]);
                        totalLinhaGeral += Convert.ToDecimal(dt.Rows[i]["Valor"]);
                    }
                    else
                    {
                        rw["DIA" + index] = "-";
                    }
                }

                if (index == 31)
                {
                    if (dt.Rows[i]["CALCULADO"].ToString() == "SIM")
                    {
                        if ((dt.Rows[i]["DESCRICAOUNIDADEDEMEDIDA"].ToString().ToUpper().Contains("%") || dt.Rows[i]["DESCRICAOTARGUET"].ToString().ToUpper().Contains("%") || dt.Rows[i]["DESCRICAO"].ToString().ToUpper().Contains("HOUR") || dt.Rows[i]["DESCRICAO"].ToString().ToUpper().Contains("TIME")) && dt.Rows[i]["CHAVE"].ToString()!="2")
                        {

                            string simboPerc = "";

                            if (dt.Rows[i]["DESCRICAOUNIDADEDEMEDIDA"].ToString().ToUpper().Contains("%") || dt.Rows[i]["DESCRICAOTARGUET"].ToString().ToUpper().Contains("%"))
                            {
                                simboPerc = "%";
                            }

                            if (dt.Compute("AVG(VALOR)", "CALCULADO='SIM' AND VALOR IS NOT NULL AND VALOR >0.00  AND CHAVE='" + dt.Rows[i]["CHAVE"].ToString() + "' ").ToString() != "")
                                rw[7] = decimal.Parse(dt.Compute("AVG(VALOR)", "CALCULADO='SIM' AND VALOR IS NOT NULL AND VALOR >0.00  AND CHAVE='" + dt.Rows[i]["CHAVE"].ToString() + "' ").ToString()).ToString("#0.00") + simboPerc;
                            else
                                rw[7] = Convert.ToDecimal("0").ToString("#0.00");
                        }
                        else if (dt.Rows[i]["CHAVE"].ToString() == "8.1")
                        {
                            if (dt.Compute("MAX(VALOR)", "CALCULADO='SIM' AND  VALOR IS NOT NULL AND VALOR >0.00  AND CHAVE='" + dt.Rows[i]["CHAVE"].ToString() + "' ").ToString()!="")
                                rw[7] = decimal.Parse(dt.Compute("MAX(VALOR)", "CALCULADO='SIM' AND  VALOR IS NOT NULL AND VALOR >0.00  AND CHAVE='" + dt.Rows[i]["CHAVE"].ToString() + "' ").ToString()).ToString();
                            else
                                rw[7] = Convert.ToDecimal("0").ToString("#0.00");

                        }
                        else
                        {
                            if (dt.Compute("SUM(VALOR)", "CALCULADO='SIM' AND VALOR IS NOT NULL AND VALOR >0.00  AND CHAVE='" + dt.Rows[i]["CHAVE"].ToString() + "' ").ToString() != "")
                                rw[7] = decimal.Parse(dt.Compute("SUM(VALOR)", "CALCULADO='SIM' AND  VALOR IS NOT NULL AND VALOR >0.00  AND CHAVE='" + dt.Rows[i]["CHAVE"].ToString() + "' ").ToString()).ToString("#0.00") ;
                            else
                                rw[7] = Convert.ToDecimal("0").ToString("#0.00");
                        }

                        //rw[7] = totalLinha.ToString("#0.00");  
                    }
                    totalLinha = 0;
                    dtMontado.Rows.Add(rw);
                    rw = dtMontado.NewRow();
                    index = 1;
                }
                else
                {
                    index++;
                }
            }
            return dtMontado;
        }
    }
}