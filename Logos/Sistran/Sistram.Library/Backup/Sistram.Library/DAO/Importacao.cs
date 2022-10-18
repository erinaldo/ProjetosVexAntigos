using System;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public class Importacao
    {
        public Importacao()
        { }

        public void GravarEstoqueMov(string IDESTOQUE, string IDPRODUTOCLIENTE, string IDUSUARIO, string Historico, string QUANTIDADESOLICITADA, string Saldo, string Quantidade, string VALOREMESTOQUE)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("ESTOQUEMOV", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            string strsql = "INSERT INTO ESTOQUEMOV( ";
            strsql += " IDESTOQUEMOV, ";
            strsql += " IDESTOQUE, ";
            strsql += " IDPRODUTOCLIENTE,  ";
            strsql += " IDESTOQUEOPERACAO, ";
            strsql += " IDUSUARIO, ";
            strsql += " HISTORICO, ";
            strsql += " QUANTIDADESOLICITADA, ";
            strsql += " QUANTIDADE, ";
            strsql += " SALDO, ";
            strsql += " VALOREMESTOQUE ";
            strsql += " ) VALUES ( ";
            strsql += ID + " ,  ";
            strsql += IDESTOQUE + ", ";
            strsql += IDPRODUTOCLIENTE + ",  ";
            strsql += " 2, ";
            strsql += IDUSUARIO + ", ";
            strsql += "'" + Historico + "', ";
            strsql += QUANTIDADESOLICITADA.Replace(",", ".") + " , ";
            strsql += Quantidade.Replace(",", ".") + ", ";
            strsql += Saldo.Replace(",", ".") + ", ";
            strsql += VALOREMESTOQUE.Replace(",",".") + ")";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

        }

        public void BaixarSaldoTabelaEstoque(string IDEstoque,  string Qtd)
        {
            string strsql = " UPDATE ESTOQUE SET SALDO = SALDO - " + Qtd.Replace(",",".") + " WHERE IDESTOQUE=" + IDEstoque;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public int RetornarIDEstoque(string idProdutoCliente, string IdFilial, string Qtd)
        {
            string strsql = " SELECT IDESTOQUE FROM ESTOQUE WHERE IDPRODUTOCLIENTE =" + idProdutoCliente + " AND IDFILIAL =" + IdFilial;
            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public void GravarDocumentoRelacionado(string CodPedido, string CodNotaFiscal)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTORELACIONADO", HttpContext.Current.Session["ConnLogin"].ToString());
            string strsql = " INSERT INTO DOCUMENTORELACIONADO (IDDOCUMENTORELACIONADO, IDDOCUMENTOPAI , IDDOCUMENTOFILHO) VALUES ("+ID+" , " + CodPedido + " , " + CodNotaFiscal + ")";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public int LeDocReladionado(string CodPedido, string CodNotaFiscal)
        {
            string strsql = " SELECT ISNULL(IDDOCUMENTORELACIONADO,0) IDDOCUMENTORELACIONADO FROM DOCUMENTORELACIONADO  WITH (NOLOCK) WHERE IDDOCUMENTOPAI=" + CodPedido + "  AND IDDOCUMENTOFILHO = " + CodNotaFiscal;
            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");            
        }

        public DataTable ConsultarDadosPed(string IDPRODUTOEMBALAGEM, string NumeroPedido)
        {
            string strsql = " SELECT DI.IDCLIENTEDIVISAO, QUANTIDADE, VALORUNITARIO, DC.IDDOCUMENTO   FROM DOCUMENTO  WITH (NOLOCK) DC INNER JOIN DOCUMENTOITEM DI ON DC.IDDOCUMENTO = DI.IDDOCUMENTO WHERE NUMERO = " + NumeroPedido + " AND DI.IDPRODUTOEMBALAGEM =" + IDPRODUTOEMBALAGEM;
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
        }

        public void ExecutarSemRetorno(string chave, string linha, string Conn)
        {
            try
            {
                string strsql = "INSERT INTO EdiArquivo (Chave,Linha) values ('" + chave + "','" + linha + "')";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, Conn);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public int ConsultarCadastroByCNPJ(string CNPJ, string Conn)
        {
            string strsql = "select IDCadastro from Cadastro where CnpjCpf='" + CNPJ + "'";
            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, Conn);
        }

        public int ConsultarFilialByIDCadastro(int idCadastro, string Conn)
        {
            string strsql = "select * from Filial where IDCadastro=" + idCadastro.ToString();
            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, Conn);
        }

        public DataTable ConsultarEdiArqivoByChave(string chave, string Conn)
        {
            string strsql = "select * from ediarquivo where chave='" + chave + "'";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql, Conn);
        }

        public int CadastrarTbCadastro(string CnpjCpf, string InscricaoRG, string RazaoSocialNome, string Endereco, int IDCidade, int IDestado, string Cep)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("Cadastro");
            string strsql = "INSERT INTO Cadastro ";
            strsql += " (IDCadastro, CnpjCpf, InscricaoRG, RazaoSocialNome, Endereco, IDCidade, Cep)";
            strsql += " values ("+ID+", '" + CnpjCpf + "', '" + InscricaoRG + "', '" + RazaoSocialNome + "', '" + Endereco + "', '" + IDCidade + "', '" + Cep.Replace("-","").Replace(".","") + "')  ";
            //strsql += " select MAX(idcadastro) from Cadastro";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
            return Convert.ToInt32(ID);
        }

        public int[] RetornaIdCidadeEstadoByNome(string cidade, string estado)
        {
            string strsql = "SELECT ESTADO.IDESTADO, IDCIDADE FROM CIDADE LEFT JOIN ESTADO ON CIDADE.IDESTADO = ESTADO.IDESTADO WHERE CIDADE.NOME='" + cidade + "' AND ESTADO.NOME='" + estado + "'";
            DataTable DT = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");

            int[] m = new int[2];

            if (DT.Rows.Count > 0)
            {
                m[0] = Convert.ToInt32(DT.Rows[0][1]);
                m[1] = Convert.ToInt32(DT.Rows[0][0]);
            }
            return m;
        }

        public int CadastrarFilial(int IDEmpresa, int IDCadastro, String Nome)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("Filial");
            string strsql = "INSERT INTO Filial (IDFilial, IDEmpresa, IDCadastro, NumeroDaFilial, Nome)";
            strsql += " VALUES ("+ID+", " + IDEmpresa + ", " + IDCadastro + ", 0, '" + Nome + "')";
            //strsql += " SELECT MAX(IDFILIAL) FROM Filial";
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            return Convert.ToInt32(ID);
        }

        public int ConsultarVeiculoByPlaca(string placa)
        {
            string strsql = "SELECT IDVeiculo FROM VEICULO  WHERE PLACA ='" + placa + "'";
            int m = Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            return m;
        }

        public void CadastrarMotorista(int IdCadastro)
        {
            string strsql = "INSERT INTO Motorista (IDMotorista) VALUES (" + IdCadastro.ToString() + ") ";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public int CadastrarVeiculo(int IDCidade, int IDMotorista, string placa)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("VEICULO");
            string strsql = "INSERT INTO VEICULO (IDVeiculo, IDCidade, IDMotorista, Placa) VALUES ("+ID+", " + IDCidade.ToString() + ", " + IDMotorista.ToString() + ", '" + placa + "') SELECT MAX(IDVeiculo) FROM VEICULO";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
            return Convert.ToInt32(ID);
        }

        public void CadastrarTrasportadora(int IdCadastro, int IDContaContabil)
        {
            string strsql = "INSERT INTO TRANSPORTADORA (IDTransportadora, IDContaContabil) VALUES (" + IdCadastro.ToString() + ", " + IDContaContabil + ") ";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
        }

        public int AbrirRomaneio(int IdFilial, string idUsuario)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("ROMANEIO");
            string strsql = "INSERT INTO ROMANEIO (IDROMANEIO, IDFILIAL, IDUSUARIO, IDDepositoPlantaLocalizacao, Emissao, Tipo) VALUES ("+ID+", " + IdFilial + ", " + idUsuario + ", 1, getdate(),'')  SELECT MAX(IDROMANEIO) FROM ROMANEIO";
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            return Convert.ToInt32(idUsuario);
        }

        public int AbrirRomaneioDt(int IdDT, int IdRomaneio)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DTRomaneio");
            string strsql = "INSERT INTO DTRomaneio (IDDTRomaneio, IDDT, IDRomaneio) VALUES ("+ID+", " + IdDT + ", " + IdRomaneio + ")  SELECT MAX(IDDTRomaneio) FROM DTRomaneio";
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
            return Convert.ToInt32(ID);
        }

        public int AbrirDT(int IdFilial, int Numero, int IdVeiculo, int IdMotorista, int IDDTTipo)
        {
            // verifica se já existe o dt naquela filial
            string strsql = "SELECT NUMERO FROM DT  WITH (NOLOCK) WHERE IDFILIAL=" + IdFilial.ToString() + " AND NUMERO=" + Numero.ToString();
            int ret = Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");

            if (ret > 0)
            {
                //criticar de alguma forma
                return 0;
            }
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DT");
            strsql = "INSERT INTO DT (IDDT, IDFILIAL, NUMERO, IDPRIMEIROVEICULO, IDPRIMEIROMOTORISTA, IDDTTipo) VALUES ("+ID+"), " + IdFilial + ", " + Numero.ToString() + ", " + IdVeiculo.ToString() + ", " + IdMotorista.ToString() + ", " + IDDTTipo.ToString() + ") ";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
            return Convert.ToInt32(ID);
        }

        internal void LimparbyChave(string chave, string linha)
        {
            Sistran.Library.GetDataTables.ExecutarSemRetorno("DELETE FROM EDIARQUIVO WHERE CHAVE='" + chave + "' and linha ='"+ linha +"'", "");
        }

        internal void LimparbyChave(string chave)
        {
            Sistran.Library.GetDataTables.ExecutarSemRetorno("DELETE FROM EDIARQUIVO WHERE CHAVE='" + chave + "' ", "");
        }

        public int InserirTabDocumento(string IDFilial, string IDFilialAtual, string Serie, string Numero, string IDCliente, string IDRemetente, string IDDestinatario, string PesoBruto, string MetragemCubica, string Volumes, string ValorDaNota)
        {

            if (MetragemCubica == "")
                MetragemCubica = "0";
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTO");

            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" INSERT INTO DOCUMENTO (IDDocumento, ");
            s.Append(" IDFilial, ");
            s.Append(" IDFilialAtual, ");
            //s.Append(" IDCondicaoDePagamento, ");
            s.Append(" TipoDeDocumento, ");
            s.Append(" Serie, ");
            s.Append(" Numero, ");
            s.Append(" IDCliente, ");
            s.Append(" IDRemetente, ");
            s.Append(" IDDestinatario, ");
            s.Append(" PesoBruto, ");
            s.Append(" MetragemCubica, ");
            s.Append(" Volumes, ");
            s.Append(" AnoMes, ");
            s.Append(" ValorDaNota)");
            s.Append(" VALUES ( ");

            s.Append(ID + " ,");
            s.Append(IDFilial + " , ");
            s.Append(IDFilial + ", ");
            //s.Append(" IDCondicaoDePagamento, ");
            s.Append(" 'NOTA FISCAL', ");
            s.Append("'" + Serie.ToString() + "' , ");
            s.Append(Convert.ToInt32(Numero).ToString() + " , ");
            s.Append(IDCliente + ", ");
            s.Append(IDRemetente + ", ");
            s.Append(IDDestinatario + ", ");
            s.Append(Convert.ToDecimal(PesoBruto).ToString().Replace(",",".") + ", ");
            s.Append(Convert.ToDecimal(MetragemCubica.ToString().Replace(",", ".")) + ", ");
            s.Append(Convert.ToInt32(Volumes).ToString() + " , ");
            s.Append(DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + ",");
            s.Append(ValorDaNota.ToString().Replace(",",".") + ")  ");
            Sistran.Library.GetDataTables.ExecutarRetornoID(s.ToString(), "");
            return Convert.ToInt32(ID);
        }

        public int InserirTabRomaneioDocumento(int IDRomaneio, int IDDocumento, int Volumes, decimal Peso, decimal PesoCubado, decimal Cubagem, decimal ValorDoDocumento)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("ROMANEIODOCUMENTO");

            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" INSERT INTO ROMANEIODOCUMENTO (");
            s.Append(" IDRomaneioDocumento, ");
            s.Append(" IDRomaneio, ");
            s.Append(" IDDocumento, ");
            s.Append(" Volumes, ");
            s.Append(" Peso, ");
            s.Append(" PesoCubado, ");
            s.Append(" Cubagem, ");
            s.Append(" ValorDoDocumento ");

            s.Append("  ) VALUES (");

            s.Append(ID+" , ");
            s.Append(IDRomaneio.ToString() + " , ");
            s.Append(IDDocumento.ToString() + ",");
            s.Append(Volumes.ToString() + ", ");
            s.Append(Peso.ToString() + ", ");
            s.Append( PesoCubado.ToString() + ", ");
            s.Append(Cubagem.ToString() +  ",");
            s.Append(ValorDoDocumento.ToString() + " )  ");
            Sistran.Library.GetDataTables.ExecutarSemRetorno(s.ToString(), "");
            return Convert.ToInt32(ID);
        }
    }
}