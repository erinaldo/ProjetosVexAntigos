﻿using System;
using System.Collections.Generic;
using System.Web.Services;
using Sistran.Library.DTO;
using System.Data;

/// <summary>
/// Summary description for ReposcicaoRoge
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class ReposcicaoRoge : System.Web.Services.WebService
{

    public class Estrutura
    {
        public string VolumeCodigoBarras { get; set; }
        public string ItemCodigoBarras { get; set; }
        public string CB_LIDO { get; set; }
    }

    public ReposcicaoRoge()
    {

    }

    /// <summary>
    /// Método Exposto com a finalidade de gravar a nota faturada da roge
    /// </summary>
    /// <param name="Chave">Chave da Nota Fiscal</param>
    /// <param name="IdNota">Chave da Nota Roge</param>
    /// <param name="CodigoRegiao">Código da Região</param>
    /// <param name="NomeRegiao">Nome da Regiao</param>
    /// <param name="ClienteEspecial">Especifica a Se é um cliente especial</param>
    /// <param name="Volume">Lista com codigo de Barras dos volumes</param>
    /// <param name="Item">Lista de Itens</param>
    /// 
    [WebMethod]
    public string GravarDocumento(string Chave, string CodigoRegiao, string NomeRegiao, string ClienteEspecial, List<Volumes> Volume, List<Itens> Item)
    {
        try
        {
            new SistranBLL.ReposicaoRoge().Gravar(Chave, "0", CodigoRegiao, NomeRegiao, ClienteEspecial, Volume, Item);
            return "0^GRAVADO COM SUCESSO";
        }
        catch (Exception ex)
        {
            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "moises@sistecno.com.br", "Gravação de Notas", Chave + " - " + ex.Message + ex.InnerException, "mail.sistecno.com.br", "@oncetsis14", "Gravacao De Nota Roge");


            if (ex.Message.Contains("UC_ReposicaoRoge"))            
                return "0^PROBLEMA AO GRAVAR A NOTA:" + Chave + " DETALHES: NOTA JÁ EXISTENTE NA BASE DA LOGOS.";            
            else
                return "1^PROBLEMA AO GRAVAR A NOTA:" + Chave + " DETALHES: " + ex.Message;
        }
    }


    [WebMethod]
    public string CancelarConferencia(string Chave, string UsuarioSolicitante)
    {
        try
        {
            new SistranBLL.ReposicaoRoge().CancelarConferencia(Chave, UsuarioSolicitante);
            return "0^CANCELAMENTO EFETUADO COM SUCESSO";
        }
        catch (Exception ex)
        {
            return "1^PROBLEMA AO CANCELAR A CONFERENCIA DA NOTA:" + Chave + " DETALHES: " + ex.Message;
        }
    }


    [WebMethod]
    public DataSet ResgatarDocumentoPeloColetor(string Chave, string IdUsuario)
    {
        return new SistranBLL.ReposicaoRoge().ResgatarDocumentoColetor(Chave, IdUsuario);
    }


    [WebMethod]
    public void GravarConferencia(List<SistranDAO.Estrutura> Dados)
    {
        try
        {
            new SistranBLL.ReposicaoRoge().gravarConferencia(Dados);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

}