using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.Generic;

/// <summary>
/// Summary description for FuncoesGerais
/// </summary>
public static class FuncoesGerais
{

    public static string LoadDataSetConstantes(string constante)
    {
        string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, "Config/Constantes.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(filePath);
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            if (item["Key"].ToString() == constante)
            {
                return item["Situacao"].ToString();
            }
        }
        return "";

    }

    public static string LoadDataSetCamposRepots(int idusuario)
    {
        string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, "Config/CamposReports.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(filePath);
        string ret = "";
        if (ds.Tables[0].Rows.Count == 0)
            return ret; // pesquisa pelo campos idremtente e idcliente
        else
        {
            DataRow[] rw = ds.Tables[0].Select("key = " + idusuario);
            foreach (var item in rw)
            {
                ret += item["campo"].ToString() + "|";
            }

        }

        return ret;
    }

    #region CNPJ / CPF
    public static string FormatarCnpj(string s)
    {
        s = s.Replace(".", "");
        s = s.Replace("-", "");
        s = s.Replace("/", "");
        s = s.Replace(@"\", "");

        if (s.Length == 0)
        {
            return "";
        }

        if (s.Length <= 11)
        {
            MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
            mtpCpf.Set(ZerosEsquerda(s, 11));
            return mtpCpf.ToString();
        }
        else
        {
            MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
            mtpCnpj.Set(ZerosEsquerda(s, 11));
            return mtpCnpj.ToString();
        }
    }

    public static string ZerosEsquerda(string strString, int intTamanho)
    {

        string strResult = "";

        for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
        {

            strResult += "0";

        }

        return strResult + strString;

    }

    #endregion

    public static string LoadDataSetLogo(string Conexao)
    {
        string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, "Config/ConfigLogo.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(filePath);

        foreach (DataRow item in ds.Tables[0].Rows)
        {
            if (item["Key"].ToString() == Conexao)
            {
                return item["Imagem"].ToString();
            }
        }
        return "";
    }

    public static string VerificarLicenca(string Conexao)
    {
        string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, "Config/ConfigLicense.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(filePath);
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            if (item["Key"].ToString() == Conexao)
            {
                return item["Arquivo"].ToString();
            }
        }
        return "";

    }

    public static string[] DataConf()
    {
        string ret = "";

        if (HttpContext.Current.Session["DataConf"] == null)
        {
            DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ret = primeiroDiaMes + "|" + DateTime.Now.ToShortDateString();
        }
        else
        {
            ret = HttpContext.Current.Session["DataConf"].ToString();
        }

        HttpContext.Current.Session["DataConf"] = ret.Replace(" 00:00:00", "");
        return ret.Replace(" 00:00:00", "").Split('|');
    }

    public static string[] CarregarConexoesRastreamento()
    {
        string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, "Config/ConfigRatreamento.xml");
        string Ret = "";

        DataSet ds = new DataSet();
        ds.ReadXml(filePath);

        foreach (DataRow item in ds.Tables[0].Rows)
        {
            Ret += item["value"].ToString() + "|";
        }

        return Ret.Split('|');
    }

    public static int RetornarIntervaloDiasPesqusa()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>) HttpContext.Current.Session["USUARIO"];
        string intervalo="";
       // intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);
        intervalo = System.Configuration.ConfigurationManager.AppSettings["DiasPesquisa"];

        if (ILusuario[0].Login.Contains("PROCTER") ||  ILusuario[0].Login.ToUpper().Contains("JBS"))
            intervalo = "1000";

        return int.Parse(intervalo);
    }
}


