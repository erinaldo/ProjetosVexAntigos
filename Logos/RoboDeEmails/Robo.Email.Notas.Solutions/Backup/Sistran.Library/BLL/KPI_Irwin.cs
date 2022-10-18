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

        public DataTable GerarPlanilha(string MesAno, string ídGrupoDeProduto)
        {
           DataTable dt = new SistranDAO.KPI_Irwin().GerarPlanilha(MesAno, ídGrupoDeProduto);
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
                    rw[3] = dt.Rows[i]["DESCRICAOTARGUET"].ToString().ToUpper();
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
                            rw["DIA" + index] = (dt.Rows[i]["Valor"].ToString() == "0" ? "*" : Convert.ToDecimal(dt.Rows[i]["Valor"]).ToString("#0.00")+"%");
                        else
                            rw["DIA" + index] = (dt.Rows[i]["Valor"].ToString() == "0" ? "*" : Convert.ToDecimal(dt.Rows[i]["Valor"]).ToString("#0"));

                        
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
                        rw[7] = totalLinha.ToString("#0.00") ;
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