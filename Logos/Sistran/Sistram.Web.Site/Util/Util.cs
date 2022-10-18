using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Linq;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.Web.Caching;
using System.Data;

public  class Util
{
    /// <summary>
    /// Retorna o nome da connection com o banco
    /// </summary>
    /// <param name="conn"></param>
    /// <returns></returns>
    public static string BuscaConnection(string conn)
    {
        string connection = string.Empty;
        XDocument xmlConnection = LoadXml();

        XElement ConnectionNode = xmlConnection.Descendants("DataBase").FirstOrDefault(i => i.Attribute("key").Value.Equals(conn));

        if (ConnectionNode != null)
        {
            connection = GetAttributeValue(ConnectionNode, "dataBaseName");
        }


        return connection;
    }


    private static string GetAttributeValue(XElement itemNode, string attributeName)
    {
        return itemNode.Attributes(attributeName).Single().Value;
    }


    /// <summary>
    /// Função para leitura do xml - connetions
    /// </summary>
    /// <returns></returns>
    private static XDocument LoadXml()
    {
        XDocument xmlConnections = new XDocument();
        Cache cache = HttpContext.Current.Cache;

        if (cache.Get("xmlConnections") == null)
        {
            string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, ConfigurationManager.AppSettings["UrlXmlConnections"]);
            string fileText = System.IO.File.ReadAllText(filePath);
            xmlConnections = XDocument.Parse(fileText);

            cache.Insert("xmlConnections", xmlConnections);
        }
        else
        {
            xmlConnections = (XDocument)cache.Get("xmlConnections");
        }

        return xmlConnections;
    }

    
}