using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistranBLL;
using System.IO;
using System.Collections.Generic;
using DynamicTable;
using Microsoft.Reporting.WebForms;
//using SistranBLL;


public partial class frmRptResumoFilial : System.Web.UI.Page
{
    private DataSet m_dataSet;
    private MemoryStream m_rdl;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MontarTable();
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "GEROU O RELATÓRIO RESUMO POR FILIAL", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
        }
    }


    protected void MontarTable()
    {
        int QtosDias = 3;

        DataTable dtTemp = new DataTable("tbLinhas");
        decimal total = Convert.ToDecimal(0);
        DataTable dt = NotasFiscais.ListarResumoPorFilial(Convert.ToDateTime(Request.QueryString["DI"].ToString()), Convert.ToDateTime(Request.QueryString["DF"].ToString()), Sistran.Library.FuncoesUteis.retornarClientes(),Session["Conn"].ToString());
        
        int qtdColunas = (Convert.ToInt32(dt.Compute("MAX(PRAZOUTILIZADO)", "")) * 3) + 4;
        decimal qtdTotalNf = (Convert.ToDecimal(dt.Compute("MAX(TOTALDENOTAS)", "")));
        int contDefColun = 0;
        string NomeColuna = "";

        #region Cabeçalho

        int control = 0;
        for (int i = 0; i < qtdColunas; i++)
        {
            if (i == 0)
            {
                dtTemp.Columns.Add("Filial");
            }

            else
            {
                switch (contDefColun)
                {
                    case 0:
                        NomeColuna = "TRANSITTIME" + control.ToString();
                        contDefColun += 1;
                        break;

                    case 1:
                        NomeColuna = "QTNF" + control.ToString();
                        contDefColun += 1;
                        break;

                    case 2:
                        NomeColuna = "NF" + control.ToString();
                        contDefColun = 0;
                        control += 1;
                        break;
                                        }                               
                
                dtTemp.Columns.Add(NomeColuna);            
                
            }
                          
        }
        
#endregion

        int qtdColunasTemp = (Convert.ToInt32(dt.Compute("MAX(PRAZOUTILIZADO)", "")));
        decimal[] tot = new decimal[qtdColunasTemp+1];

        #region Itens
        for (int i = 0; i < qtdColunas - 1; i++)
        {
            string NomeFilial = "";

            for (int ii = 0; ii < dt.Rows.Count; ii++)
            {
                DataRow o = dtTemp.NewRow();
                if (i == 0)
                {
                    if (NomeFilial != dt.Rows[ii]["NOMEDAFILIAL"].ToString())
                    {
                        o["FILIAL"] = dt.Rows[ii]["NOMEDAFILIAL"].ToString();

                        DataRow[] orw = dt.Select("NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");
                        int contador = 0;
                        int row = 0;
                        decimal porcAtualAcmulado = Convert.ToDecimal("0.00");

                        foreach (DataRow item in orw)
                        {
                            if (contador < Convert.ToInt32(item["PRAZOUTILIZADO"]))
                            {
                                for (int kkkk = contador; kkkk < Convert.ToInt32(item["PRAZOUTILIZADO"]); kkkk++)
                                {
                                    o["TransitTime" + contador.ToString()] = contador.ToString();
                                    o["QTNF" + contador.ToString()] = "0";
                                    o["NF" + contador.ToString()] = "0%";
                                    contador += 1;
                                    row += 1;
                                }
                            }

                            if (contador == Convert.ToInt32(item["PRAZOUTILIZADO"]))
                            {
                                o["TransitTime" + contador.ToString()] = item["PRAZOUTILIZADO"].ToString();
                                o["QTNF" + contador.ToString()] = item["NOTASFISCAIS_ENTREGUE"].ToString();

                                decimal qtdTotal = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                decimal qtdItemAtual = Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                                decimal porcAtual = (qtdItemAtual / qtdTotal) * 100;
                                porcAtualAcmulado += porcAtual;
                                tot[row] += Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                                o["NF" + contador.ToString()] = porcAtualAcmulado.ToString("#0.00") + "%";
                            }
                            row += 1;
                            contador += 1;
                        }

                        if (contador <= qtdColunasTemp)
                        {
                            for (int iii = contador; iii <= qtdColunasTemp; iii++)
                            {
                                o["TransitTime" + contador.ToString()] = contador.ToString();
                                o["QTNF" + contador.ToString()] = "0";
                                o["NF" + contador.ToString()] = porcAtualAcmulado.ToString("#0.00") + "%";
                                contador += 1;
                            }
                        }


                        dtTemp.Rows.Add(o);
                    }


                    NomeFilial = dt.Rows[ii]["NOMEDAFILIAL"].ToString();
                }
            }
        }


        #endregion

        #region Rodape
        int cont2 = 0;
        decimal totporcAcm = Convert.ToDecimal("0.00");
        DataRow ot = dtTemp.NewRow();

        int m = 1;
        for (int i = 0; i < qtdColunas; i++)
        {

            if (i == 0)
            {
                ot["Filial"] = "TOTAL DE NOTAS";
                dtTemp.Rows.Add(ot);
            }
            else
            {
                switch (contDefColun)
                {
                    case 0:
                        NomeColuna = "";
                        contDefColun += 1;
                        break;

                    case 1:
                        NomeColuna = tot[cont2].ToString();
                        contDefColun += 1;

                        break;

                    case 2:
                        decimal AtualPorcent = ((Convert.ToDecimal(tot[cont2])) / Convert.ToDecimal(dt.Compute("sum(TOTALDENOTAS)", "")) * 100);
                        totporcAcm += AtualPorcent;
                        NomeColuna = totporcAcm.ToString("#0.00") + "%";
                        contDefColun = 0;
                        cont2 += 1;
                        break;
                }
                ot[m] = NomeColuna;
                m += 1;
            }
        }
        #endregion

        dtTemp.Columns.Add("TOTALENTREGUES");
        dtTemp.Columns.Add("NFNAOENTREGUES");
        dtTemp.Columns.Add("PNFNAOENTREGUES");
        dtTemp.Columns.Add("TOTALGERAL");
        
        decimal totalDeNotas = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "")) + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", ""));



        for (int i = 0; i < dtTemp.Rows.Count; i++)
        {
            if (dtTemp.Rows[i]["FILIAL"].ToString() != "TOTAL DE NOTAS")
                dtTemp.Rows[i]["TOTALENTREGUES"] = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + dtTemp.Rows[i]["FILIAL"].ToString() + "'"));
            else
                dtTemp.Rows[i]["TOTALENTREGUES"] = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", ""));


            if (dtTemp.Rows[i]["FILIAL"].ToString() != "TOTAL DE NOTAS")
                dtTemp.Rows[i]["NFNAOENTREGUES"] = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dtTemp.Rows[i]["FILIAL"].ToString() + "'"));
            else
                dtTemp.Rows[i]["NFNAOENTREGUES"] = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", ""));


            if (dtTemp.Rows[i]["FILIAL"].ToString() != "TOTAL DE NOTAS")
            {
                decimal perc1 = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dtTemp.Rows[i]["FILIAL"].ToString() + "'"));
                decimal perc2 = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dtTemp.Rows[i]["FILIAL"].ToString() + "'"));
                decimal perc4 = ((perc1 / perc2) * 100);

                dtTemp.Rows[i]["PNFNAOENTREGUES"] = perc4.ToString("#0.00") + "%";
            }
            else
                dtTemp.Rows[i]["PNFNAOENTREGUES"] = ((Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "")) / totalDeNotas) * 100).ToString("#0.00") + "%";

            if (dtTemp.Rows[i]["FILIAL"].ToString() != "TOTAL DE NOTAS")
            {
                decimal TOTALDENOTASFILIAL = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dtTemp.Rows[i]["FILIAL"].ToString() + "'")) + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + dtTemp.Rows[i]["FILIAL"].ToString() + "'"));
                dtTemp.Rows[i]["TOTALGERAL"] = TOTALDENOTASFILIAL;
            }
            else
            {
                dtTemp.Rows[i]["TOTALGERAL"] = totalDeNotas;

            }

        }


     

        dtTemp.WriteXml(Server.MapPath("~\\imgReport\\ResumoFilial.xml"));
        OpenDataFile(Server.MapPath("~\\imgReport\\ResumoFilial.xml"), false);            
    }

    private void OpenDataFile(string filename, bool showOptionsDialog)
    {
        try
        {
            m_dataSet = new DataSet();
            m_dataSet.ReadXml(filename);

            List<string> allFields = GetAvailableFields();          

            List<string> selectedFields = new List<string>();

            for (int i = 0; i < m_dataSet.Tables[0].Columns.Count ; i++)
            {
                selectedFields.Add(m_dataSet.Tables[0].Columns[i].ColumnName);
            }

            if (m_rdl != null)
                m_rdl.Dispose();
            m_rdl = GenerateRdl(allFields, selectedFields);
            DumpRdl(m_rdl);

            ShowReport();
        }
        catch (Exception)
        {
            //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ShowReport()
    {
        this.reportViewer1.Reset();
        this.reportViewer1.LocalReport.LoadReportDefinition(m_rdl);
        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MyData", m_dataSet.Tables[0]));
        this.reportViewer1.LocalReport.DisplayName = "ResumoFilial";

        ////impressao pdf
        if (Request.QueryString["tipo"] == "PDF")
        {
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streamids;
            byte[] exportBytes = reportViewer1.LocalReport.Render("XLS", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = mimeType;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ResultadoPesquisa " + DateTime.Now.ToString() + "." + fileNameExtension);
            HttpContext.Current.Response.BinaryWrite(exportBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        else
        {
           
        }

    }

    private void DumpRdl(MemoryStream rdl)
    {

        //FileStream fs = new FileStream(Server.MapPath(@"~\imgReport\ResumoFilial.rdlc"), FileMode.Create);
        //rdl.WriteTo(fs);
    }

    private List<string> GetAvailableFields()
    {
        DataTable dataTable = m_dataSet.Tables[0];
        List<string> availableFields = new List<string>();
        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            availableFields.Add(dataTable.Columns[i].ColumnName);
        }
        return availableFields;
    }    

    private MemoryStream GenerateRdl(List<string> allFields, List<string> selectedFields)
    {
        MemoryStream ms = new MemoryStream();
        RdlGenerator gen = new RdlGenerator();
        gen.AllFields = allFields;
        gen.SelectedFields = selectedFields;
        gen.WriteXml(ms);
        ms.Position = 0;
        return ms;
    }

}
