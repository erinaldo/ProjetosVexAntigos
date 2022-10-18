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

public partial class nfLista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //verifica o tipo de parametro

            if (Request.QueryString["tipo"] == "todosDias")
            {
                carregarPorDias("");
                this.Title = " NOTAS FISCAIS AGUARDANDO EMBARQUE";                  
            }

            if (Request.QueryString["tipo"] == "todos3Dias")
            {
                carregarPorDias(" and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) >=3  ");
                this.Title = " NOTAS FISCAIS AGUARDANDO EMBARQUE";
            }

            if (Request.QueryString["tipo"] == "todos2Dias")
            {
                carregarPorDias(" and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) >1 and  DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) <=2  ");
                this.Title = "  NOTAS FISCAIS AGUARDANDO EMBARQUE";
            }

            if (Request.QueryString["tipo"] == "todos1Dia")
            {
                carregarPorDias(" and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) <=1 ");
                this.Title = "NOTAS FISCAIS AGUARDANDO EMBARQUE";
            }

            if (Request.QueryString["tipo"] == "normal")
            {
                this.Title = "NOTAS FISCAIS AGUARDANDO EMBARQUE - FILIAL: " + Request.QueryString["nome"];                   
            }
            else if (Request.QueryString["tipo"] == "todos")
            {
                this.Title = "NOTAS FISCAIS AGUARDANDO EMBARQUE - FILIAL: TODOS";
            }
            else
            {
                switch (Request.QueryString["dias"])
                {
                    case "3":
                        this.Title = "NOTAS FISCAIS AGUARDANDO EMBARQUE 72 HS OU MAIS - FILIAL: " + Request.QueryString["nome"];
                        break;

                    case "2":
                        this.Title = "NOTAS FISCAIS AGUARDANDO EMBARQUE 48 HS  - FILIAL: " + Request.QueryString["nome"];
                        break;

                    case "1":
                        this.Title = "NOTAS FISCAIS AGUARDANDO EMBARQUE 24 HS  - FILIAL: " + Request.QueryString["nome"];
                        break;
                }

            }

            if (Request.QueryString["r"] != null)
            {
                this.Title = "NOTAS FISCAIS SEM DATA DE ENTREGA(ACIMA DE 5 DIAS) - FILIAL: " + Request.QueryString["nome"];
            }

            lblTitulo.Text = this.Title;
            carregar();
        }
    }

    private void carregarPorDias( string condicao)
    {
        string strsql = "";

        strsql += " SELECT  CEMB.FILIALATUAL FILIAL,";
        strsql += "R.RAZAOSOCIAL REMETENTE,  D.RAZAOSOCIAL DESTINATARIO,  CNH.SERIEDOCONHECIMENTO SERIE,  CNH.CONHECIMENTO NOTAFISCAL,  CNH.VOLUMES,  CNH.PESO,  CNH.VALORDANOTA,  CNH.DATADECADASTRO,  CNH.DATADEEMISSAO,  ( SELECT TOP 1 O.DESCRICAO  FROM CONHECIMENTOOCORRENCIA CNOCO   LEFT JOIN OCORRENCIA O ON (O.SERIEDAOCORRENCIA= CNOCO.SERIEDAOCORRENCIA AND O.CODIGODAOCORRENCIA = CNOCO.CODIGODAOCORRENCIA)  WHERE CNOCO.CONTROLE=CNH.CONTROLE AND CNOCO.FILIALLANCAMENTO = CNH.FILIALLANCAMENTO  ORDER BY  CNOCO.DATADAOCORRENCIA DESC  ) OCORRENCIA     ";
        strsql += "FROM CONHECIMENTOAEMBARCAR CEMB   ";
        strsql += "INNER JOIN CONHECIMENTO CNH ON (CNH.FILIALLANCAMENTO = CEMB.FILIALLANCAMENTO AND CNH.CONTROLE = CEMB.CONTROLE)   ";
        strsql += "INNER JOIN FILIAL FL ON (FL.FILIAL = CEMB.FILIALATUAL)  ";
        strsql += "LEFT JOIN CLIENTE R ON (R.CNPJ = CNH.CNPJREMETENTE)    ";
        strsql += "LEFT JOIN CLIENTE D ON (D.CNPJ = CNH.CNPJDESTINATARIO) ";
        strsql += "WHERE CEMB.SITUACAO IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE')  ";
        //strsql += "AND CEMB.FILIALATUAL <> '14'  ";
        //strsql += "AND CEMB.FILIALATUAL <> '00' ";

        if (condicao.Length > 0)
            strsql += condicao;

        strsql += " ORDER BY  CEMB.FILIALATUAL";

        DataTable dtReport = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());
        RadGrid16.DataSource = dtReport;
        Session["dtReport"] = dtReport;
        RadGrid16.DataBind();

        Button1.Visible = false;

        if (((DataTable)RadGrid16.DataSource).Rows.Count > 0)
        {
            Button1.Visible = true;

            Button1.Attributes.Add("OnClick", "window.open('frmRptDetalheNFS.aspx?titulo=" + this.lblTitulo.Text + "&fl=" + Request.QueryString["fl"] + "&dias=" + 0 + "', '','fullscreen=yes, scrollbars=auto'); return false;");
        }
    }

    private void carregar()
    {
        if (Request.QueryString["fl"] != null && Request.QueryString["tipo"] != null)
        {
            string strsql="";
            if (Request.QueryString["r"] == null)
            {
                strsql += " SELECT ";
                strsql += " R.RAZAOSOCIAL REMETENTE, ";
                strsql += " D.RAZAOSOCIAL DESTINATARIO, ";
                strsql += " CNH.SERIEDOCONHECIMENTO SERIE, ";
                strsql += " CNH.CONHECIMENTO NOTAFISCAL, ";
                strsql += " CNH.VOLUMES, ";
                strsql += " CNH.PESO, ";
                strsql += " CNH.VALORDANOTA, ";
                strsql += " CNH.DATADECADASTRO, ";
                strsql += " CNH.DATADEEMISSAO, ";
                strsql += " ( SELECT TOP 1 O.Descricao ";
                strsql += " FROM CONHECIMENTOOCORRENCIA CNOCO  ";
                strsql += " Left Join Ocorrencia O on (O.SerieDaOcorrencia= CNOCO.SerieDaOcorrencia and O.CodigoDaOcorrencia = CNOCO.CodigoDaOcorrencia) ";
                strsql += " WHERE CNOCO.CONTROLE=CNH.CONTROLE AND CNOCO.FilialLancamento = CNH.FilialLancamento ";
                strsql += " ORDER BY  CNOCO.DATADAOCORRENCIA DESC ";
                strsql += " ) OCORRENCIA, CEMB.FILIALATUAL FILIAL  ";

                strsql += " FROM CONHECIMENTO CNH ";
                strsql += " INNER JOIN CLIENTE R ON (R.CNPJ = CNH.CNPJREMETENTE)  ";
                strsql += " INNER JOIN CLIENTE D ON (D.CNPJ = CNH.CNPJDESTINATARIO)  ";
                strsql += " INNER JOIN CONHECIMENTOAEMBARCAR CEMB ON (CEMB.FILIALLANCAMENTO = CNH.FILIALLANCAMENTO AND CEMB.CONTROLE = CNH.CONTROLE)  ";
                strsql += " WHERE     ";
                strsql += " CEMB.SITUACAO IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE')  ";

                if (Request.QueryString["tipo"] != "todos")
                    strsql += " AND CEMB.FILIALATUAL = '" + Request.QueryString["fl"] + "' ";

                string dias = "0";
                if (Request.QueryString["tipo"] != "normal")
                {
                    //MontarAguardandoEmbarqueAtraso(">=3", PhEmAtraso);
                    //MontarAguardandoEmbarqueAtraso("=2", phEmAtraso2dias);
                    //MontarAguardandoEmbarqueAtraso("<=1", phEmAtraso1dia);

                    if(Request.QueryString["dias"] =="3")
                        strsql += " AND DATEDIFF(DAY, CNH.DATADECADASTRO, GETDATE()) >=3 ";

                    if (Request.QueryString["dias"] == "2")
                        strsql += " AND DATEDIFF(DAY, CNH.DATADECADASTRO, GETDATE()) =2 ";

                    if (Request.QueryString["dias"] == "1")
                        strsql += " AND DATEDIFF(DAY, CNH.DATADECADASTRO, GETDATE()) <=1 ";

                    dias = Request.QueryString["dias"];
                }
            }

            else
            {
                strsql += " SELECT  ";
                strsql += " R.RAZAOSOCIAL REMETENTE,   ";
                strsql += " D.RAZAOSOCIAL DESTINATARIO,   ";
                strsql += " CNH.SERIEDOCONHECIMENTO SERIE,   ";
                strsql += " CNH.CONHECIMENTO NOTAFISCAL,   ";
                strsql += " CNH.VOLUMES,   ";
                strsql += " CNH.PESO,   ";
                strsql += " CNH.VALORDANOTA,   ";
                strsql += " CNH.DATADECADASTRO,   ";
                strsql += " CNH.DATADEEMISSAO   ,";
                strsql += " ( SELECT TOP 1 O.Descricao ";
                strsql += " FROM CONHECIMENTOOCORRENCIA CNOCO  ";
                strsql += " Left Join Ocorrencia O on (O.SerieDaOcorrencia= CNOCO.SerieDaOcorrencia and O.CodigoDaOcorrencia = CNOCO.CodigoDaOcorrencia) ";
                strsql += " WHERE CNOCO.CONTROLE=CNH.CONTROLE AND CNOCO.FilialLancamento = CNH.FilialLancamento ";
                strsql += " ORDER BY  CNOCO.DATADAOCORRENCIA DESC ";
                strsql += " ) OCORRENCIA  ";
                strsql += " FROM CONHECIMENTO CNH  ";
                strsql += " INNER JOIN CLIENTE R ON (R.CNPJ = CNH.CNPJREMETENTE)    ";
                strsql += " INNER JOIN CLIENTE D ON (D.CNPJ = CNH.CNPJDESTINATARIO)  ";
                strsql += " WHERE  0=0";

                if (Request.QueryString["tipo"] != "todos")
                    strsql += " AND CNH.FILIALDESTINO = '" + Request["fl"] + "'   ";

                strsql += " AND CNH.DATADEEMISSAO >= '01/jan/2012'  ";
                strsql += " AND CNH.DATADEENTREGA IS NULL  ";
                strsql += " AND DATEDIFF(DAY, CNH.DATADEEMISSAO, GETDATE()) > 5  AND CNH.SITUACAODAIMPRESSAODOCTRC<>'CANCELADO' ";
            }
            DataTable dtReport = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());
            RadGrid16.DataSource = dtReport;
            Session["dtReport"] = dtReport;
            RadGrid16.DataBind();

            Button1.Visible = false;

            if (((DataTable)RadGrid16.DataSource).Rows.Count > 0)
            {
                Button1.Visible = true;

                Button1.Attributes.Add("OnClick", "window.open('frmRptDetalheNFS.aspx?titulo="+ this.lblTitulo.Text +"&fl=" + Request.QueryString["fl"] + "&dias=" + 0 + "', '','fullscreen=yes, scrollbars=auto'); return false;");
            }

        }
    }


    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        carregar();
    }
    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        carregar();

    }
}
