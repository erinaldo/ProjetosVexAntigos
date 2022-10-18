using Sistecno;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de OL005
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class OL005 : System.Web.Services.WebService
    {
        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\Cadastrar005.log")]
        public Retorno Cadastrar(Autentica Credenciais, cOL005 cOL005)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
          
            //if (Credenciais.login != "wsbarbosa_prod")
            //    cnx = "Data Source=192.168.10.10;Initial Catalog=vex;User ID=sa;Password=WERasd27;  ";
            

            try
            {
                if (!Autentica.Autenticar(Credenciais.login, Credenciais.senha))
                    throw new Exception("Nao Autorizado");


                string ss = "Select Count(*) from BarbosaOL005 where NumeroRecebimento='"+ cOL005.NumeroRecebimento + "'";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(ss, cnx).Tables[0];

                if (dt.Rows[0][0].ToString() != "0")
                    throw new Exception("Recebimento já existe.");


                Guid cvPrincipal = Guid.NewGuid();

                string s = "Insert into BarbosaOL005(IdOL005,NumeroRecebimento,CriadoPor, CriadoEmCriadoEm, TipoDeRemessa, NrNotaFiscal,Fornecedor, chave, numero, TipoDePedido) ";
                s += "values ('" + cvPrincipal + "','" + cOL005.NumeroRecebimento + "','" + cOL005.CriadoPor + "', '" + cOL005.CriadoEm + "', '" + cOL005.TipoDeRemessa + "', '" + cOL005.NrNotaFiscal + "','" + cOL005.Fornecedor + "', '" + cOL005.Chave + "', '" + cOL005.NrNotaFiscal + "', '" + cOL005.TipoDePedido + "'); ";


                for (int i = 0; i < cOL005.Itens.Count; i++)
                {
                    string cd = cOL005.Itens[i].CodigoMaterial;
                    try
                    {
                        cd = int.Parse(cd).ToString();
                    }
                    catch (Exception)
                    {
                    }

                    s += "Insert into BarbosaOL005Item (IdOL005Itens,IdOL005,ItemRecebimento,CodigoMaterial,TextoMaterial,UnidadeDeMedidaBasica,Fator,QuantidadeNF,QuantidadeConferida,DataVencimento, PerfilDeDistribuicao, centro, deposito) ";
                    s += " values ('" + Guid.NewGuid() + "','" + cvPrincipal + "','" + cOL005.Itens[i].ItemRecebimento + "' ,'" + cd + "','" + cOL005.Itens[i].TextoMaterial + "','" + cOL005.Itens[i].UnidadeDeMedidaBasica + "','" + cOL005.Itens[i].Fator + "','" + cOL005.Itens[i].QuantidadeNF + "','" + cOL005.Itens[i].QuantidadeConferida + "','" + cOL005.Itens[i].DataVencimento + "', '" + cOL005.Itens[i].PerfilDeDidtribuicao + "', '" + cOL005.Itens[i].Centro + "', '" + cOL005.Itens[i].Deposito + "'); ";
                }


                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(s, cnx);

                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }


        public class cOL005
        {
            public string NumeroRecebimento { get; set; }
            public string CriadoPor { get; set; }

            public string CriadoEm { get; set; }
            public string TipoDePedido { get; set; }

            public string TipoDeRemessa { get; set; }
            public string NrNotaFiscal { get; set; }
            public string Fornecedor { get; set; }
            public string Chave { get; set; }


            public List<cOL005_Itens> Itens { get; set; }
        }

        public class cOL005_Itens
        {
            public string ItemRecebimento { get; set; }
            public string PerfilDeDidtribuicao { get; set; }
            public string CodigoMaterial { get; set; }
            public string TextoMaterial { get; set; }
            public string UnidadeDeMedidaBasica { get; set; }
            public string Fator { get; set; }
            public string QuantidadeNF { get; set; }
            public string QuantidadeConferida { get; set; }
            public string DataVencimento { get; set; }
             public string Deposito { get; set; }
            public string Centro { get; set; }

        }
    }
}