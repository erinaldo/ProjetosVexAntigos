using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Data;
using Artem.Web.UI.Controls;
using System.Configuration;
using Subgurim.Controles;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class frmlocazcliente : System.Web.UI.Page
{
    List<SistranMODEL.Usuario> ILusuario;

    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");
        GMap1.CommercialKey = "hZ5MC9FZhx8=";


        ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        scli = RetronarClientes(ILusuario);

        if (!IsPostBack)
        {



            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

            Timer1.Enabled = false;
            Timer1.Interval = int.Parse((txtAtualizar.Text == "" ? "1" : txtAtualizar.Text)) * 60000;


            carregarCombo();
            CarregarTodosVeiculos();
            Timer1.Enabled = true;

            HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
            tr0.Style.Add("display", "none");
        }

        lblQtdVeiculos.Text = (lstVeiculos.Items.Count - 1).ToString();
        lblAtualizadoEm.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


        if (txtAtualizar.Text != "" && txtAtualizar.Text != "0")
            Timer1.Enabled = true;
        else
            Timer1.Enabled = false;
    }

    private void CarregarPosicaoEndrecoEntregaNF(int iddoc)
    {
        try
        {

            string s = "SELECT D.NUMERO, D.SERIE, ISNULL(CADDEST.ENDERECO,'') +','+ ISNULL(CADDEST.NUMERO,'') +',' + ISNULL(C.NOME,'')  + ','+ ISNULL(E.UF,'') ENDERECO,  CADDEST.RAZAOSOCIALNOME DEST";
            s += ", ( ";
            s += " 	SELECT TOP 1 LONGITUDE FROM RASTREAMENTO R WHERE R.IDDT = DTR.IDDT AND LATITUDE<>0 AND LONGITUDE<>0 ORDER BY DATAHORA DESC ";
            s += " ) LONGITUDE, ";
            s += " ( ";
            s += " 	SELECT TOP 1 LATITUDE FROM RASTREAMENTO R WHERE R.IDDT = DTR.IDDT AND LATITUDE<>0 AND LONGITUDE<>0 ORDER BY DATAHORA DESC ";
            s += " ) LATITUDE, ";
            s += "  CADDEST.CEP";

            s += " FROM DOCUMENTO D  ";
            s += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDDOCUMENTO = D.IDDOCUMENTO ";
            s += " INNER JOIN DTROMANEIO DTR ON DTR.IDROMANEIO = ROMDOC.IDROMANEIO ";
            s += " INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = D.IDDESTINATARIO ";
            s += " LEFT JOIN CIDADE C ON C.IDCIDADE = CADDEST.IDCIDADE ";
            s += " LEFT JOIN ESTADO E ON E.IDESTADO = C.IDESTADO   ";
            s += " WHERE D.IDDOCUMENTO=" + iddoc;


            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(s);

            GMap1.Controls.Clear();
            GMap1.reset();

            lblTipoMapa.Text = "LOCALIZAÇÃO DO VEÍCULO vs. LOCAL DE ENTREGA";

            GMap1.Visible = true;
            lblTipoMapa.Visible = true;
            GMap1.Height = 350;

            GeoCode geo = new GeoCode();
            lblPxEEndDestino.Text = dt.Rows[0]["endereco"].ToString().Replace(",,,", ",");
            lblPxEEndDestino.Text = dt.Rows[0]["endereco"].ToString().Replace(",,", ",");
            lblPxENota.Text = dt.Rows[0]["numero"].ToString() + "-" + dt.Rows[0]["serie"].ToString();
            lblPxEDestinatario.Text = dt.Rows[0]["dest"].ToString();

            geo = GMap1.getGeoCodeRequest(lblPxEEndDestino.Text);



            //pocicao do carro
            GIcon icon = new GIcon();
            icon.image = "Caminhao.png";
            icon.iconSize = new GSize(40, 40);

            GMarkerOptions mOpts = new GMarkerOptions();
            mOpts.clickable = true;
            mOpts.icon = icon;
            List<GLatLng> puntos = new List<GLatLng>();

            GMap1.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
            GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
            GMap1.Add(new GControl(GControl.preBuilt.RotateControl));
            GMap1.Add(new GControl(GControl.preBuilt.ScaleControl));
            GMap1.Add(new GControl(GControl.preBuilt.MenuMapTypeControl));
            GMap1.Add(new GControl(GControl.preBuilt.StreetViewControl));

            GMap1.setCenter(new GLatLng(geo.Placemark.coordinates.lat, geo.Placemark.coordinates.lng), 12);

            GLatLng gl = new GLatLng(double.Parse(dt.Rows[0]["LATITUDE"].ToString()), double.Parse(dt.Rows[0]["LONGITUDE"].ToString()));
            GMarker marker = new GMarker(gl, mOpts);
            GInfoWindow window = new GInfoWindow(marker, "Localização do Veículo", false);
            GMap1.Add(window);
            puntos.Add(new GLatLng(double.Parse(dt.Rows[0]["LATITUDE"].ToString()), double.Parse(dt.Rows[0]["LONGITUDE"].ToString())));
            GPolyline linea = new GPolyline(puntos, "FF0000", 2);

            GMap1.Add(linea);

            GIcon iconcli = new GIcon();
            iconcli.image = "cliente.jpg";
            iconcli.iconSize = new GSize(40, 40);

            GMarkerOptions mOptsCli = new GMarkerOptions();
            mOptsCli.clickable = true;
            mOptsCli.icon = iconcli;

            GLatLng gl1 = new GLatLng(geo.Placemark.coordinates.lat, geo.Placemark.coordinates.lng);
            GMarker marker1 = new GMarker(gl1, mOptsCli);
            GInfoWindow window1 = new GInfoWindow(marker1, "Localização do Cliente", false);
            puntos.Add(new GLatLng(geo.Placemark.coordinates.lat, geo.Placemark.coordinates.lng));

            GPolyline linea2 = new GPolyline(puntos, "FF0000", 2);
            GMap1.Add(linea2);

            GMap1.Add(window1);
            carregarGridInfoNotasFiscais();
        }
        catch (Exception)
        {
            Panel4.Visible = false;
            GridView1.Visible = false;
        }
    }


    string scli = "";
    private void carregarCombo()
    {
        string ssql = "SELECT DISTINCT  DT.DATADESAIDA, DT.IDDT, DT.NUMERO, VEI.PLACA, CDMOT.RAZAOSOCIALNOME MOTORISTA, CAST(VEI.PLACA AS NVARCHAR(10)) + '(DT: '+ CAST(DT.NUMERO AS NVARCHAR(10))+ ')'  PLACA_NUMERO   ";
        ssql += " FROM DT    WITH (NOLOCK)  ";
        ssql += " INNER JOIN RASTREAMENTO RAS ON RAS.IDDT = DT.IDDT     ";
        ssql += " INNER JOIN DTROMANEIO DTROM ON DTROM.IDDT = DT.IDDT     ";
        ssql += " INNER JOIN ROMANEIODOCUMENTO RDOC ON RDOC.IDROMANEIO = DTROM.IDROMANEIO     ";
        ssql += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTO = RDOC.IDDOCUMENTO     ";
        ssql += " INNER JOIN CADASTRO CDMOT ON CDMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA     ";
        ssql += " INNER JOIN VEICULO VEI ON VEI.IDVEICULO = DT.IDPRIMEIROVEICULO     ";
        ssql += " WHERE      ";
        ssql += " ANDAMENTO in ('EM VIAGEM', 'DOCUMENTACAO LIBERADA')         ";
        ssql += " AND SITUACAO='ABERTO'      ";
        ssql += " AND  RAS.LONGITUDE<>0    ";
        ssql += " AND IDPORTARIA IS NOT NULL     ";
        ssql += " AND (DOC.IDCLIENTE in(" + scli + ") OR IDREMETENTE in(" + scli + ") )     ";
        ssql += " ORDER BY DT.NUMERO  ";
        DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS(ssql, Session["ConnLogin"].ToString());

        lstVeiculos.DataTextField = "PLACA_NUMERO";
        lstVeiculos.DataValueField = "IDDT";

        lstVeiculos.DataSource = ds.Tables[0];
        lstVeiculos.DataBind();

        lstVeiculos.Items.Insert(0, new ListItem((lstVeiculos.Items.Count == 0 ? "NENHUM VEÍCULO" : "TODOS VEÍCULOS"), "TODOS VEÍCULOS"));
        lstVeiculos.SelectedIndex = 0;

    }

    private string RetronarClientes(List<SistranMODEL.Usuario> ILusuario)
    {
        for (int i = 0; i < ILusuario.Count; i++)
        {
            scli += ILusuario[i].EmpresaId;

            if (i + 1 < ILusuario.Count)
                scli += ",";

        }

        return scli;
    }

    private void CarregarTodosVeiculos()
    {
        btnMultiplosPontos.Visible = false;
        chkVerPontos.Visible = false;
        txtDataHora.Visible = false;
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        
        string strsql = "";
        strsql += " SELECT DISTINCT  DT.NUMERO, DT.IDDT, DT.SITUACAO, ";        
        strsql += " ISNULL((SELECT  TOP 1 cast(LATITUDE as nvarchar(30))+ '|' + cast(LONGITUDE as nvarchar(30)) + '|' + convert(varchar(30), DATAHORA, 120)   FROM RASTREAMENTO RAST WHERE RAST.IDDT = DT.IDDT AND RAST.LATITUDE<>0 ORDER BY DATAHORA DESC),0) LATCompleta, ";
        strsql += " VEI.PLACA,         ";
        strsql += " CADMOT.RAZAOSOCIALNOME,         ";
        strsql += " DT.NUMERO,         ";
        //strsql += " COUNT(DISTINCT D.IDDOCUMENTO) NOTAS ,       ";
        strsql += " CONVERT(VARCHAR(10),DT.DATADESAIDA,103) DATANOTA,       ";
        strsql += " DT.DATADESAIDA       ";
        strsql += " FROM DOCUMENTO D         ";
        strsql += " INNER JOIN ROMANEIODOCUMENTO RM ON RM.IDDOCUMENTO = D.IDDOCUMENTO         ";
        strsql += " INNER JOIN DTROMANEIO DTROM ON DTROM.IDROMANEIO = RM.IDROMANEIO   ";
        strsql += " INNER JOIN DT ON DT.IDDT  = DTROM.IDDT         ";
        strsql += " INNER JOIN RASTREAMENTO RAS ON RAS.IDDT = DT.IDDT ";
        strsql += " INNER JOIN VEICULO VEI ON VEI.IDVEICULO = DT.IDPRIMEIROVEICULO         ";
        strsql += " INNER JOIN CADASTRO CADMOT ON CADMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA  ";
        strsql += " WHERE (IDCLIENTE IN(" + scli + ")   OR IDREMETENTE IN(" + scli + "))       ";
        strsql += " AND TIPODEDOCUMENTO IN ('NOTA FISCAL' , 'ORDEM DE SERVICO', 'GUIA DE REMESSA', 'ORDEM DE COLETA')           ";
        strsql += " AND DT.SITUACAO = 'ABERTO'         ";
        strsql += " AND ANDAMENTO IN ('EM VIAGEM', 'DOCUMENTACAO LIBERADA')            ";
        strsql += " AND IDPORTARIA IS NOT NULL         ";
        //strsql += " AND YEAR(D.DATADEEMISSAO)>=2011";
        //strsql += " GROUP BY DT.NUMERO, DT.IDDT, DT.SITUACAO,VEI.PLACA,CADMOT.RAZAOSOCIALNOME,DT.NUMERO  , ISNULL(D.DATADEENTRADA, DATADEEMISSAO), DT.DATADESAIDA  ";
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

        GMap1.Controls.Clear();
        GMap1.reset();

        

        DataRow[] dr = dt.Select("0=0");

        if (dr.Length == 0)
        {
            GMap1.Visible = false;
            lblTipoMapa.Visible = false;
            return;
        }
        else
        {
            GMap1.Visible = true;
            lblTipoMapa.Visible = true;
            lblTipoMapa.Text = "ÚLTIMA POSIÇÃO DOS  VEÍCULOS";
            GMap1.Height = 350;

            carregarGridInfoNotasFiscais();

        }

        GMap1.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.RotateControl));
        GMap1.Add(new GControl(GControl.preBuilt.ScaleControl));
        GMap1.Add(new GControl(GControl.preBuilt.MenuMapTypeControl));
        GMap1.Add(new GControl(GControl.preBuilt.StreetViewControl));

        string[] dadosRas = dr[0]["LATCompleta"].ToString().Split('|');


        //GMap1.setCenter(new GLatLng(double.Parse(dr[0]["lat"].ToString()), double.Parse(dr[0]["long"].ToString())), 8);
        GMap1.setCenter(new GLatLng(double.Parse(dadosRas[0].Replace(".", ",")), double.Parse(dadosRas[1].Replace(".", ","))), 8);

        GIcon icon = new GIcon();
        icon.image = "Caminhao.png";
        icon.iconSize = new GSize(40, 40);

        GMarkerOptions mOpts = new GMarkerOptions();
        mOpts.clickable = true;
        mOpts.icon = icon;

        for (int i = 0; i < dr.Length; i++)
        {
            dadosRas = dr[i]["LATCompleta"].ToString().Split('|');

            string coment = "<div style='font-name:ARIAL; font-name:arial; font-size:7px'>";
            coment = "Data/Hora: " + DateTime.Parse(dadosRas[2]).ToString("dd/MM/yyyy HH:mm:ss");
            //            coment = "Data/Hora: " + DateTime.Parse(dr[i]["dataHra"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
            coment += "<br>Motorista: " + dr[i]["RazaoSocialNome"].ToString();
            coment += "<br>Placa: " + dr[i]["Placa"].ToString();
            coment += "<br>Nº.DT: " + dr[i]["Numero"].ToString();
            coment += "<br><a class='link' href='filtro.aspx?opc=Localizar Carga&gps=s&ic=" + ILusuario[0].EmpresaId + "&iddt=" + dr[i]["IDDT"].ToString() + "'>Ver Notas</a>";
            coment += "</div>";
            GLatLng gl = new GLatLng(double.Parse(dadosRas[0].Replace(".", ",")), double.Parse(dadosRas[1].Replace(".", ",")));
            GMarker marker = new GMarker(gl, mOpts);
            GInfoWindow window = new GInfoWindow(marker, coment, false);
            GMap1.Add(window);
        }
    }

    private void CarregarPosicaoEntregaNF(string IdDocumento)
    {

        string s = "SELECT D.NUMERO, D.SERIE, DO.LATITUDE, DO.LONGITUDE, DO.DATAOCORRENCIA, DO.DATALANCAMENTO, OCO.NOME ";
        s += " FROM DOCUMENTO D  ";
        s += " INNER JOIN DOCUMENTOOCORRENCIA DO ON DO.IDDOCUMENTOOCORRENCIA = D.IDDOCUMENTOOCORRENCIA ";
        s += " INNER JOIN OCORRENCIA OCO ON OCO.IDOCORRENCIA = DO.IDOCORRENCIA ";
        s += " WHERE D.IDDOCUMENTO= " + IdDocumento;

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(s);

        GMap1.Controls.Clear();
        GMap1.reset();

        DataRow[] dr = dt.Select("0=0");

        if (dr.Length == 0)
        {
            GMap1.Visible = false;
            lblTipoMapa.Visible = false;
            return;
        }
        else
        {
            GMap1.Visible = true;
            lblTipoMapa.Visible = true;
            lblTipoMapa.Text = "LOCAL DE ENTREGA DA NF: " + dr[0]["NUMERO"].ToString();
            GMap1.Height = 350;
        }

        GMap1.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.RotateControl));
        GMap1.Add(new GControl(GControl.preBuilt.ScaleControl));
        GMap1.Add(new GControl(GControl.preBuilt.MenuMapTypeControl));
        GMap1.Add(new GControl(GControl.preBuilt.StreetViewControl));

        GMap1.setCenter(new GLatLng(double.Parse(dr[0]["LATITUDE"].ToString()), double.Parse(dr[0]["LONGITUDE"].ToString())), 15);

        GIcon icon = new GIcon();
        icon.image = "pontoEntrega.png";
        icon.iconSize = new GSize(30, 50);

        GMarkerOptions mOpts = new GMarkerOptions();
        mOpts.clickable = true;
        mOpts.icon = icon;

        for (int i = 0; i < dr.Length; i++)
        {
            string coment = "<div style='font-name:ARIAL; font-name:arial; font-size:7px'>";
            coment = "***PONTO DE ENTREGA*** <br>Data/Hora: " + DateTime.Parse(dr[i]["DATAOCORRENCIA"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
            coment += "<br>Ocorrencia:  " + dr[i]["NOME"].ToString();
            coment += "</div>";
            GLatLng gl = new GLatLng(double.Parse(dr[i]["LATITUDE"].ToString()), double.Parse(dr[i]["LONGITUDE"].ToString()));
            GMarker marker = new GMarker(gl, mOpts);
            GInfoWindow window = new GInfoWindow(marker, coment, false);
            GMap1.Add(window);
        }
    }

    protected void lstVeiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        atualizar();
    }

    private void atualizar()
    {
        pnlPxE.Visible = false;
        if (lstVeiculos.SelectedIndex > 0)
        {
            carregarLocalizaoVeiculoUnico();
            lblQtdVeiculos.Text = "1";
        }
        else
        {
            CarregarTodosVeiculos();
            //lblQtdVeiculos.Text = "33333";
        }
    }

    private void carregarLocalizaoVeiculoUnico()
    {        
        string strsql = "";
        strsql += " SELECT top 1  DT.NUMERO, DT.IDDT, DT.SITUACAO, ";
        strsql += " ISNULL((SELECT  TOP 1 LATITUDE FROM RASTREAMENTO RAST WHERE RAST.IDDT = DT.IDDT AND RAST.LATITUDE<>0 ORDER BY DATAHORA DESC),0) LAT,         ";
        strsql += " ISNULL((SELECT  TOP 1 LONGITUDE FROM RASTREAMENTO RAST WHERE RAST.IDDT = DT.IDDT AND RAST.LATITUDE<>0 ORDER BY DATAHORA DESC), 0) LONG,         ";
        strsql += " ISNULL((SELECT  TOP 1 DATAHORA FROM RASTREAMENTO RAST WHERE RAST.IDDT = DT.IDDT AND RAST.LATITUDE<>0 ORDER BY DATAHORA DESC), GETDATE()) DATAHRA,  ";
        strsql += " VEI.PLACA,         ";
        strsql += " CADMOT.RAZAOSOCIALNOME,         ";
        strsql += " DT.NUMERO,         ";
        //strsql += " COUNT(DISTINCT D.IDDOCUMENTO) NOTAS ,       ";
        strsql += " CONVERT(VARCHAR(10),DT.DATADESAIDA,103) DATANOTA,       ";
        strsql += " DT.DATADESAIDA       ";
        strsql += " FROM DOCUMENTO D         ";
        strsql += " INNER JOIN ROMANEIODOCUMENTO RM ON RM.IDDOCUMENTO = D.IDDOCUMENTO         ";
        strsql += " INNER JOIN DTROMANEIO DTROM ON DTROM.IDROMANEIO = RM.IDROMANEIO   ";
        strsql += " INNER JOIN DT ON DT.IDDT  = DTROM.IDDT         ";
        strsql += " INNER JOIN RASTREAMENTO RAS ON RAS.IDDT = DT.IDDT ";
        strsql += " INNER JOIN VEICULO VEI ON VEI.IDVEICULO = DT.IDPRIMEIROVEICULO         ";
        strsql += " INNER JOIN CADASTRO CADMOT ON CADMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA  ";
        strsql += " WHERE (IDCLIENTE IN(" + scli + ")   OR IDREMETENTE IN(" + scli + "))       ";
        strsql += " AND TIPODEDOCUMENTO IN ('NOTA FISCAL' , 'ORDEM DE SERVICO', 'GUIA DE REMESSA', 'ORDEM DE COLETA')           ";
        strsql += " AND DT.SITUACAO = 'ABERTO'         ";
        strsql += " AND ANDAMENTO IN ('EM VIAGEM', 'DOCUMENTACAO LIBERADA')            ";
        strsql += " AND IDPORTARIA IS NOT NULL         ";
        strsql += " AND DT.IDDT=" + lstVeiculos.SelectedValue;
        //strsql += " AND ISNULL((SELECT  TOP 1 LATITUDE FROM RASTREAMENTO RAST WHERE RAST.IDDT = DT.IDDT AND RAST.LATITUDE<>0 ORDER BY DATAHORA DESC),0) <>0       ";
        //strsql += " GROUP BY DT.NUMERO, DT.IDDT, DT.SITUACAO,VEI.PLACA,CADMOT.RAZAOSOCIALNOME,DT.NUMERO  , ISNULL(D.DATADEENTRADA, DATADEEMISSAO), DT.DATADESAIDA  ";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

        GMap1.Controls.Clear();
        GMap1.reset();

        //DataRow[] dr = dt.Select("IDDT=" + lstVeiculos.SelectedValue, "");

        if (dt.Rows.Count == 0)
        {
            GMap1.Visible = false;
            lblTipoMapa.Visible = false;
            chkVerPontos.Visible = false;
            txtDataHora.Visible = false;
            btnMultiplosPontos.Visible = false;
            return;
        }
        else
        {
            GMap1.Visible = true;
            lblTipoMapa.Visible = true;
            chkVerPontos.Visible = true;
            txtDataHora.Visible = true;
            btnMultiplosPontos.Visible = true;

            lblTipoMapa.Text = "ÚLTIMA POSIÇÃO DO VEÍCULO PLACA: " + lstVeiculos.SelectedItem.Text;
        }

        GMap1.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.RotateControl));
        GMap1.Add(new GControl(GControl.preBuilt.ScaleControl));
        GMap1.Add(new GControl(GControl.preBuilt.MenuMapTypeControl));
        GMap1.Add(new GControl(GControl.preBuilt.StreetViewControl));

        GMap1.setCenter(new GLatLng(double.Parse(dt.Rows[0]["lat"].ToString()), double.Parse(dt.Rows[0]["long"].ToString())), 15);

        GIcon icon = new GIcon();
        icon.image = "Caminhao.png";
        icon.iconSize = new GSize(40, 40);

        GMarkerOptions mOpts = new GMarkerOptions();
        mOpts.clickable = true;
        mOpts.icon = icon;

        for (int i = 0; i < dt.Rows.Count ; i++)
        {
            string coment = "<div style='font-name:ARIAL; font-name:arial; font-size:7px'>";
            coment = "Data/Hora: " + DateTime.Parse(dt.Rows[i]["dataHra"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
            coment += "<br>Motorista: " + dt.Rows[i]["RazaoSocialNome"].ToString();
            coment += "<br>PLaca: " + dt.Rows[i]["Placa"].ToString();
            coment += "<br>Nº. DT: " + dt.Rows[i]["Numero"].ToString();
            coment += "<br><a class='link' href='filtro.aspx?opc=Localizar Carga&gps=s&ic=" + ILusuario[0].EmpresaId + "&iddt=" + dt.Rows[i]["IDDT"].ToString() + "'>Ver Notas  </a>";     
            coment += "</div>";

            txtDataHora.Text = DateTime.Parse(dt.Rows[i]["dataHra"].ToString()).ToString("dd/MM/yyyy HH:mm");

            GLatLng gl = new GLatLng(double.Parse(dt.Rows[i]["lat"].ToString()), double.Parse(dt.Rows[i]["long"].ToString()));

            GMarker marker = new GMarker(gl, mOpts);
            GInfoWindow window = new GInfoWindow(marker, coment, true);
            GMap1.Add(window);
        }
        carregarGridInfoNotasFiscais();
    }

    private void carregarGridInfoNotasFiscais()
    {
        string ssql = "SELECT DISTINCT  D.IDDOCUMENTO,SERIE,CAST(D.NUMERO AS NVARCHAR(20)) +  '-' +CAST(D.SERIE AS NVARCHAR(5))  NUMERO,  ";
        ssql += " ISNULL(DOCOCO.LATITUDE,0) LATITUDE,ISNULL(DOCOCO.LONGITUDE,0) LONGITUDE,OCO.NOME,DOCOCO.DATALANCAMENTO DATAHORA,   ";
        ssql += " UPPER(CADDEST.RAZAOSOCIALNOME) DESTINATARIO,   ";
        ssql += " CASE ISNULL(IDDOCUMENTOOCORRENCIAARQUIVO, '') WHEN '' THEN '' ELSE 'FOTO' END FOTO,   ";
        ssql += " REPLACE(REPLACE(UPPER(ISNULL(CADDEST.ENDERECO,'') + ', ' + ISNULL(CADDEST.NUMERO,'') + ', ' + ISNULL(C.NOME,'') + '-' + ISNULL(E.UF,'')), ',,',','),', ,',',') ENDERECO, ";
        ssql += " DT.NUMERO  NDT,  V.PLACA , CADDEST.CEP  ";
        ssql += " FROM DT   WITH (NOLOCK) ";
        ssql += " INNER JOIN RASTREAMENTO RAS ON RAS.IDDT = DT.IDDT   ";
        ssql += " INNER JOIN DTROMANEIO DTR ON DTR.IDDT = DT.IDDT   ";
        ssql += " INNER JOIN ROMANEIODOCUMENTO RDOC ON DTR.IDROMANEIO = RDOC.IDROMANEIO   ";
        ssql += " INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = RDOC.IDDOCUMENTO   ";
        ssql += " INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = D.IDDESTINATARIO   ";
        ssql += " LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO ON DOCOCO.IDDOCUMENTOOCORRENCIA = D.IDDOCUMENTOOCORRENCIA   ";
        ssql += " LEFT JOIN OCORRENCIA OCO ON OCO.IDOCORRENCIA = DOCOCO.IDOCORRENCIA   ";
        ssql += " LEFT JOIN DOCUMENTOOCORRENCIAARQUIVO DOA ON DOA.IDDOCUMENTOOCORRENCIA = DOCOCO.IDDOCUMENTOOCORRENCIA   ";
        ssql += " LEFT JOIN CIDADE C ON C.IDCIDADE = D.IDENDERECOCIDADE   ";
        ssql += " LEFT JOIN ESTADO E ON E.IDESTADO = C.IDESTADO  ";
        ssql += " LEFT JOIN VEICULO V ON V.IDVEICULO = DT.IDPRIMEIROVEICULO    ";
        ssql += " WHERE 0=0 ";

        if (lstVeiculos.SelectedIndex > 0)
            ssql += " AND DT.IDDT =" + lstVeiculos.SelectedValue;

        ssql += " AND (D.IDCLIENTE IN(" + scli + ") OR D.IDREMETENTE IN(" + scli + "))      ";
        ssql += " AND  YEAR(DT.DATADESAIDA)>=2011     ";
        ssql += " AND ANDAMENTO IN ('EM VIAGEM', 'DOCUMENTACAO LIBERADA')   AND IDPORTARIA IS NOT NULL      ";
        ssql += " AND SITUACAO='ABERTO'   ";
        ssql += " ORDER BY 3 ";

        DataTable dNotas = Sistran.Library.GetDataTables.RetornarDataTable(ssql, Session["Conn"].ToString());

        GridView1.Visible = true;

        if (dNotas.Rows.Count > 0)
            Panel4.Visible = true;
        else
            Panel4.Visible = false;


        DataView dv = dNotas.DefaultView;

        string ordem = " asc";

        if (DropDownList1.SelectedValue.ToLower() == "nome" || DropDownList1.SelectedValue.ToLower() == "DataHora".ToLower())
            ordem = " desc";

        dv.Sort = DropDownList1.SelectedValue + ordem;

        GridView1.DataSource = dv;
        GridView1.DataBind();
        lblQtdServicos.Text = dNotas.Rows.Count.ToString();
        lblQtdPendentes.Text = dNotas.Compute("count(iddocumento)", "nome is null or nome =''").ToString();
        lblQtdEntregues.Text = dNotas.Compute("count(iddocumento)", "nome ='ENTREGA REALIZADA NORMALMENTE'").ToString();
        lblQtdRetorno.Text = dNotas.Compute("count(iddocumento)", "nome <>'ENTREGA REALIZADA NORMALMENTE'").ToString();

        if (dNotas.Rows.Count > 0)
            lblPerEntregues.Text = ((decimal.Parse(dNotas.Compute("count(iddocumento)", "nome ='ENTREGA REALIZADA NORMALMENTE'").ToString()) / decimal.Parse(dNotas.Rows.Count.ToString())) * 100).ToString("#0.00") + "%";
        else
            lblPerEntregues.Text = "0";

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lstVeiculos.SelectedIndex = -1;
        if (e.CommandArgument.ToString() == "VerFoto")
        {
            dvFoto.Visible = true;
            DataTable dtfotos = buscarFoto(e.CommandName.ToString());
            lstFoto.DataSource = dtfotos;
            lstFoto.DataBind();

            for (int i = 0; i < dtfotos.Rows.Count; i++)
            {
                byte[] imagem = (byte[])dtfotos.Rows[i]["ARQUIVO"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + dtfotos.Rows[i][0].ToString() + ".jpg"));
                imgFotoGrande.ImageUrl = "imgReport/" + dtfotos.Rows[i]["IDDOCUMENTOOCORRENCIAARQUIVO"] + ".jpg";
            }
        }

        if (e.CommandArgument.ToString() == "VerRota")
        {
            CarregarPosicaoEntregaNF(e.CommandName.ToString());
        }

        if (e.CommandArgument.ToString() == "pxe")
        {
            CarregarPosicaoEndrecoEntregaNF(int.Parse(e.CommandName.ToString()));
        }
    }

    private DataTable buscarFoto(string iddocumento)
    {
        string strsq = "";
        strsq += " SELECT DOA.IDDOCUMENTOOCORRENCIAARQUIVO, ";
        strsq += " DOA.ARQUIVO,  ";
        strsq += " CONVERT(VARCHAR(10),DO.DATAOCORRENCIA, 103) DATAOCORRENCIA ";
        strsq += " FROM DOCUMENTOOCORRENCIAARQUIVO DOA ";
        strsq += " INNER JOIN DOCUMENTOOCORRENCIA DO ON DO.IDDOCUMENTOOCORRENCIA =  DOA.IDDOCUMENTOOCORRENCIA ";
        strsq += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTOOCORRENCIA  =  DOA.IDDOCUMENTOOCORRENCIA";
        strsq += " WHERE DOC.IDDOCUMENTO = " + iddocumento;
        strsq += " ORDER BY DO.DATAOCORRENCIA DESC ";
        return new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), strsq).Tables[0];
    }
    int numero = 0;
    protected void lstFoto_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        ImageButton imgFotos = (ImageButton)e.Item.FindControl("btnAmpliarImagem");
        Label lblData = (Label)e.Item.FindControl("lblData");

        if (imgFotos != null)
        {
            imgFotos.ImageUrl = "imgReport/" + imgFotos.CommandArgument.ToString() + ".jpg";
            numero = numero + 1;
            lblData.Text = "Foto: " + numero.ToString();
        }
    }

    protected void lstFoto_ItemCommand1(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Ampliar")
        {
            imgFotoGrande.ImageUrl = "imgReport/" + e.CommandArgument.ToString() + ".jpg";

        }
    }

    protected void btnFecharImagem_Click(object sender, EventArgs e)
    {
        dvFoto.Visible = false;
        //  pnlteste.Enabled = true;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lnkPosicaoEntrega = (LinkButton)e.Row.FindControl("lnkPosicaoEntrega");
        Label lblGridDT = (Label)e.Row.FindControl("lblGridDT");
        DataRowView x = (DataRowView)e.Row.DataItem;


        if (lnkPosicaoEntrega != null)
        {
            lnkPosicaoEntrega.Visible = false;
            lblGridDT.ForeColor = System.Drawing.Color.Red;
            lblGridDT.ToolTip = x["NOME"].ToString();
            lblGridDT.ToolTip = "Pendente de Entrega";

            if (x["Longitude"].ToString() != "" && x["Longitude"].ToString() != "0")
            {
                lnkPosicaoEntrega.Visible = true;
                lblGridDT.ForeColor = System.Drawing.Color.Green;
                lblGridDT.ToolTip = x["NOME"].ToString();
            }
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
    {
        carregarGridInfoNotasFiscais();
    }
    protected void chkVerPontos_CheckedChanged(object sender, EventArgs e)
    {
        if (chkVerPontos.Checked == true && lstVeiculos.SelectedIndex > 0)
        {
            carregarPontosVeiculo();
        }
    }

    private void carregarPontosVeiculo()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        //GMap1.CommercialKey = "hZ5MC9FZhx8=";

        string dts = "";
        try
        {
            DateTime dti = DateTime.Parse(txtDataHora.Text);
            dts = dti.ToString("yyyy-MM-dd HH:mm");
        }
        catch (Exception)
        {
        }

        string STRSQL = "";
        //pega a configuração da tabela para cortar os digitos da latitude e longitude
        STRSQL = "SELECT TOP 1 CORTAREMLATITUDE, CORTAREMLOGINTUDE  FROM CONFIGURACOESMOBILE";
        DataTable dtConfig = Sistran.Library.GetDataTables.RetornarDataTable(STRSQL);


        STRSQL = "SELECT DISTINCT IDDT, CONVERT(nvarchar(10), CAST(LATITUDE AS numeric(10," + (dtConfig.Rows.Count > 0 ? dtConfig.Rows[0]["CORTAREMLATITUDE"].ToString() : "5") + "))) LATITUDE, CONVERT(nvarchar(10), CAST(LONGITUDE AS numeric(10," + (dtConfig.Rows.Count > 0 ? dtConfig.Rows[0]["CORTAREMLOGINTUDE"].ToString() : "6") + "))) LONGITUDE, DATAHORA,  CAST(DATEPART(MINUTE, DATAHORA) AS NVARCHAR(2)), PONTODEOCORRENCIA  FROM RASTREAMENTO ";
        STRSQL += " WHERE IDDT= " + lstVeiculos.SelectedValue;
        STRSQL += " AND LATITUDE<>0 ";
        STRSQL += " AND LONGITUDE <>0 ";
        STRSQL += " AND DATAHORA >='" + dts + "'";
        //STRSQL += " AND (DATEPART(MINUTE, DATAHORA) % 5=0 OR  PONTODEOCORRENCIA='SIM' ) AND (DATEPART(SECOND, DATAHORA) % 5 =0) ";
        STRSQL += " ORDER BY DATAHORA DESC";
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(STRSQL);


        if (dt.Rows.Count == 0)
        {
            CarregarTodosVeiculos();
            return;
        }


        GMap1.Controls.Clear();
        GMap1.reset();

        DataRow[] dr = dt.Select("0=0", "");

        if (dr.Length == 0)
        {
            GMap1.Visible = false;
            lblTipoMapa.Visible = false;
            chkVerPontos.Visible = false;
            txtDataHora.Visible = false;
            btnMultiplosPontos.Visible = true;
            return;
        }
        else
        {
            GMap1.Visible = true;
            lblTipoMapa.Visible = true;
            chkVerPontos.Visible = true;
            txtDataHora.Visible = true;
            lblTipoMapa.Text = "PONTOS DE PASSAGEM DO VEÍCULO PLACA: " + lstVeiculos.SelectedItem.Text;
            btnMultiplosPontos.Visible = true;
        }

        GMap1.Add(new GControl(GControl.preBuilt.GOverviewMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
        GMap1.Add(new GControl(GControl.preBuilt.RotateControl));
        GMap1.Add(new GControl(GControl.preBuilt.ScaleControl));
        GMap1.Add(new GControl(GControl.preBuilt.MenuMapTypeControl));
        GMap1.Add(new GControl(GControl.preBuilt.StreetViewControl));


        GMap1.setCenter(new GLatLng(double.Parse(dr[0]["LATITUDE"].ToString().Replace(".", ",")), double.Parse(dr[0]["LONGITUDE"].ToString().Replace(".", ","))), 12);

        GLatLng latlng = new GLatLng(double.Parse(dr[dr.Length - 1]["LATITUDE"].ToString().Replace(".", ",")), double.Parse(dr[0]["LONGITUDE"].ToString().Replace(".", ",")));
        List<GLatLng> puntos = new List<GLatLng>();

        GLatLng latlng1 = new GLatLng(double.Parse(dr[0]["LATITUDE"].ToString().Replace(".", ",")), double.Parse(dr[0]["LONGITUDE"].ToString().Replace(".", ",")));


        string lat = "";
        string log = "";
        int excluidos = 0;

        for (int i = 0; i < dr.Length; i++)
        {
            if (dr[i]["LATITUDE"].ToString() != lat || dr[i]["LONGITUDE"].ToString() != log)
            {
                GLatLng gl = new GLatLng(double.Parse(dr[i]["LATITUDE"].ToString().Replace(".", ",")), double.Parse(dr[i]["LONGITUDE"].ToString().Replace(".", ",")));
                GMarker marker;//= new GMarker(gl);             
                marker = new GMarker(gl);
                GInfoWindow window = new GInfoWindow(marker, "Horário: " + dr[i]["dataHora"].ToString() + "<br> PLACA: " + lstVeiculos.SelectedItem.Text, false);
                puntos.Add(gl);
                GMap1.Add(window);
            }
            else
                excluidos++;

            lat = dr[i]["LATITUDE"].ToString();
            log = dr[i]["LONGITUDE"].ToString();
        }

        GPolyline linea = new GPolyline(puntos, "FF0000", 2);
        linea.geodesic = true;
        GMap1.Add(linea);
        carregarGridInfoNotasFiscais();
        //Response.Write("Pontos Excluidos: " + excluidos.ToString());
    }

    protected void btnMultiplosPontos_Click(object sender, EventArgs e)
    {
        if (chkVerPontos.Checked == true && lstVeiculos.SelectedIndex > 0)
        {
            carregarPontosVeiculo();
        }
    }
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        carregarPontosVeiculo();

    }
    protected void GridView1_Sorting2(object sender, GridViewSortEventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        carregarGridInfoNotasFiscais();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {

        atualizar();

    }
    protected void txtAtualizar_TextChanged(object sender, EventArgs e)
    {
        Timer1.Enabled = false;
        if (txtAtualizar.Text != "" && txtAtualizar.Text != "0")
        {
            Timer1.Interval = int.Parse((txtAtualizar.Text == "" ? "1" : txtAtualizar.Text)) * 60000;
            Timer1.Enabled = true;
            atualizar();
        }
    }
}