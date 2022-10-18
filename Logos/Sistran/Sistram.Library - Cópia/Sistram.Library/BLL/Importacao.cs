using System;
using System.Data;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;

namespace SistranBLL
{
    public class Importacao
    {
        public Importacao()
        { }

        public void ProcessarArquivoNF(System.IO.StreamReader objStreamReader, string endreco)
        {
            string linha = "";
            string chave = "WEB_" + DateTime.Now.ToString("yyyyMMddmmss");
            int qtdLinhasInsertidas = 0;


            while (objStreamReader.Peek() >= 0)
            {
                linha = objStreamReader.ReadLine();

                ExecutarSemRetorno(chave, linha, "");
                qtdLinhasInsertidas += 1;
            }
            objStreamReader.Close();

            if (qtdLinhasInsertidas > 0)
            {
                ProcessarRegistroNF(chave);
            }
        }

        public void ProcessarArquivo(System.IO.StreamReader objStreamReader, string endreco)
        {
            string linha = "";
            string chave = "WEB_" + DateTime.Now.ToString("yyyyMMddmmss");
            int qtdLinhasInsertidas = 0;


            while (objStreamReader.Peek() >= 0)
            {
                linha = objStreamReader.ReadLine();

                ExecutarSemRetorno(chave, linha, "");
                qtdLinhasInsertidas += 1;
            }
            objStreamReader.Close();

            if (qtdLinhasInsertidas > 0)
            {
                ProcessarRegistro(chave);
            }
        }

        private string FormatarCnpj(string s)
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

        private void ProcessarRegistro(string chave)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>) HttpContext.Current.Session["USUARIO"];
            DataTable dt = new SistranDAO.Importacao().ConsultarEdiArqivoByChave(chave, "");
            SistranDAO.Importacao dal = new SistranDAO.Importacao();                
            int idFilial = 0;
            int idCadastro = 0;
            int idRomaneio = 0;
            int idDocTrans = 0;
            int IdRomaneioDT = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string IdentificadorRegistro = dt.Rows[i][1].ToString().Substring(0, 1).Trim();

                #region 1 - F I L I A L
                if (IdentificadorRegistro == "1") // FILIAL
                {
                    string CNPJEmbarcador =  FormatarCnpj(dt.Rows[i][1].ToString().Substring(1, 18).Trim());
                    string InscricaoEstadual =  dt.Rows[i][1].ToString().Substring(19, 18).Trim();
                    string RazaoSocial = dt.Rows[i][1].ToString().Substring(37, 60).Trim();
                    string Endereco = dt.Rows[i][1].ToString().Substring(97, 60).Trim();
                    string Cidade = dt.Rows[i][1].ToString().Substring(157, 60).Trim();
                    string UF = dt.Rows[i][1].ToString().Substring(217, 2).Trim();
                    string Cep = dt.Rows[i][1].ToString().Substring(219, 8).Trim();


                    //1-Procura na tabela de cadastro pelo cnpj
                    idCadastro = dal.ConsultarCadastroByCNPJ(CNPJEmbarcador, "");
                    if (idCadastro == 0)
                    {
                        //cadastrar 
                        int[] m = dal.RetornaIdCidadeEstadoByNome(Cidade, UF);
                        idCadastro = dal.CadastrarTbCadastro(CNPJEmbarcador, InscricaoEstadual, RazaoSocial, Endereco, m[0], m[1], Cep);

                        //cadastrar a filial            
                        idFilial = dal.CadastrarFilial(Convert.ToInt32(HttpContext.Current.Session["IDEmpresa"]) , idCadastro, RazaoSocial.Split(' ')[0]);
                    }
                    else
                    {
                        //existe cadastro
                        //verificar na tabela de filial
                        idFilial = dal.ConsultarFilialByIDCadastro(idCadastro, "");

                        if (idFilial == 0)
                        {
                            idFilial = dal.CadastrarFilial(Convert.ToInt32(HttpContext.Current.Session["IDEmpresa"]), idCadastro, RazaoSocial.Split(' ')[0]);
                        }
                    }

                }
                #endregion

                #region 2 - P L A N O   D E   C A R G A
                if (IdentificadorRegistro == "2") //
                {

                    string NumeroPlanoCarga = dt.Rows[i][1].ToString().Substring(1, 10).Trim();
                    string Emissao = dt.Rows[i][1].ToString().Substring(11, 10).Trim();
                    string PlacaVeiculo = dt.Rows[i][1].ToString().Substring(21, 8).Trim();
                    string CidadeEmplacamento = dt.Rows[i][1].ToString().Substring(29, 60).Trim();
                    string UfEmplacamento = dt.Rows[i][1].ToString().Substring(89, 2).Trim();
                    string CapacidadeCubicaVeiculo = dt.Rows[i][1].ToString().Substring(91, 10).Trim();
                    string CapacidadePesoVeiculo = dt.Rows[i][1].ToString().Substring(101, 10).Trim();
                    string TipoVeiculo = dt.Rows[i][1].ToString().Substring(111, 30).Trim();
                    string CpfMotorista = FormatarCnpj(dt.Rows[i][1].ToString().Substring(141, 14).Trim());
                    string NomeMotorista = dt.Rows[i][1].ToString().Substring(155, 60).Trim();
                    string EnderecoMotorista = dt.Rows[i][1].ToString().Substring(215, 60).Trim();
                    string CidadeMotorista = dt.Rows[i][1].ToString().Substring(275, 60).Trim();
                    string UFMotorista = dt.Rows[i][1].ToString().Substring(336, 2).Trim();
                    string FoneMotorista = dt.Rows[i][1].ToString().Substring(338, 20).Trim();


                    string CnpjTransportador = FormatarCnpj(dt.Rows[i][1].ToString().Substring(357, 18).Trim());
                    string InscricaoEstadualTransportador = dt.Rows[i][1].ToString().Substring(375, 18).Trim();
                    string RazaoSocialTransportador = dt.Rows[i][1].ToString().Substring(393, 60).Trim();
                    string EnderecoTransportador = dt.Rows[i][1].ToString().Substring(453, 60).Trim();
                    string CidadeTransportador = dt.Rows[i][1].ToString().Substring(513, 60).Trim(); 
                    string UFTransportador = dt.Rows[i][1].ToString().Substring(573, 2).Trim();

                    try
                    {
                        string FoneTransportador = dt.Rows[i][1].ToString().Substring(575, 20).Trim();
                    }
                    catch (Exception)
                    {
                    }


                    //1-Motorista
                    int idCadastroMotorista = dal.ConsultarCadastroByCNPJ(CpfMotorista, "");
                    if (idCadastroMotorista == 0)
                    {
                        //insere na tabela cadastro

                        if (NomeMotorista == "")
                            NomeMotorista = "Motorista";


                        int[] m = dal.RetornaIdCidadeEstadoByNome(CidadeMotorista, UFMotorista);
                        idCadastroMotorista = dal.CadastrarTbCadastro(CpfMotorista, "", NomeMotorista, EnderecoMotorista, m[0], m[1], "");

                        //insere na tabela Motorista
                        dal.CadastrarMotorista(idCadastroMotorista);

                    }

                    //2-Veiculo
                    int IDVeiculo = dal.ConsultarVeiculoByPlaca(PlacaVeiculo);
                    if (IDVeiculo == 0)
                    {
                        int[] m = dal.RetornaIdCidadeEstadoByNome(CidadeEmplacamento, UfEmplacamento);
                        IDVeiculo = dal.CadastrarVeiculo(m[0], idCadastroMotorista, PlacaVeiculo);                  
                    }

                    // 3-Transportadora

                    int idCadastroTransportadora = dal.ConsultarCadastroByCNPJ(CnpjTransportador, "");
                    if (idCadastroTransportadora == 0)
                    {
                        //insere na tabela cadastro
                        int[] m = dal.RetornaIdCidadeEstadoByNome(CidadeTransportador, UFTransportador);
                        idCadastroTransportadora = dal.CadastrarTbCadastro(CnpjTransportador, InscricaoEstadualTransportador, RazaoSocialTransportador, EnderecoTransportador, m[0], m[1], "" );

                        //insere na tabela Transportadora
                        dal.CadastrarTrasportadora(idCadastroTransportadora, 6);
                    }

                    //gera o dt

                    idDocTrans = dal.AbrirDT(idFilial, Convert.ToInt32(NumeroPlanoCarga), IDVeiculo, idCadastroMotorista, 1);
                    if (idDocTrans == 0)
                    {
                        //criticar
                    }
                    else
                    {
                        //gera o romaneio
                        idRomaneio = dal.AbrirRomaneio(idFilial, ILusuario[0].UsuarioId.ToString());

                        //gera dtromaneio
                       IdRomaneioDT = dal.AbrirRomaneioDt(idDocTrans, idRomaneio);
                    }
                }

                #endregion

                #region 3 - N O T A  F I S C A L 
                if (IdentificadorRegistro == "3") //
                {
                    string CnpjRemetente =FormatarCnpj( dt.Rows[i][1].ToString().Substring(1, 18).Trim());
                    string CnpjDestinatario = FormatarCnpj(dt.Rows[i][1].ToString().Substring(19, 18).Trim());
                    string RazaoSocialDest = dt.Rows[i][1].ToString().Substring(37, 60).Trim();
                    string InsricaoEstadualDest = dt.Rows[i][1].ToString().Substring(97, 18).Trim();
                    string EnderecoDest = dt.Rows[i][1].ToString().Substring(115, 60).Trim();
                    string BairoDest = dt.Rows[i][1].ToString().Substring(175, 60).Trim();
                    string CidadeDest = dt.Rows[i][1].ToString().Substring(235, 60).Trim();
                    string UFDest = dt.Rows[i][1].ToString().Substring(295, 2).Trim();
                    string NumeroNotaFiscal = dt.Rows[i][1].ToString().Substring(297, 10).Trim();
                    string SerieNotaFiscal = dt.Rows[i][1].ToString().Substring(307, 10).Trim();
                    string Volumes = dt.Rows[i][1].ToString().Substring(317, 10).Trim();
                    string PesoBruto = dt.Rows[i][1].ToString().Substring(327, 10).Trim();
                    string MetragemCubica = dt.Rows[i][1].ToString().Substring(337, 10).Trim();
                    string ValorNota = dt.Rows[i][1].ToString().Substring(347, 10).Trim();
                    string FormaRecebimento = dt.Rows[i][1].ToString().Substring(357, 30).Trim();
                    string Latitude = dt.Rows[i][1].ToString().Substring(387, 20).Trim();
                    string Longitude = dt.Rows[i][1].ToString().Substring(407, 20).Trim();

                    //insere na tabela de documento
                    int idRemetente = dal.ConsultarCadastroByCNPJ(CnpjRemetente,"");
                    int idDestinatario = dal.ConsultarCadastroByCNPJ(CnpjDestinatario, "");

                    if (idDestinatario == 0)
                    {
                        int[] m = dal.RetornaIdCidadeEstadoByNome(CnpjDestinatario, UFDest);
                        idDestinatario = dal.CadastrarTbCadastro(CnpjDestinatario, InsricaoEstadualDest, RazaoSocialDest, EnderecoDest, m[0], m[1], "");
                    }


                    int idDocumento = dal.InserirTabDocumento(idFilial.ToString(), idFilial.ToString(), SerieNotaFiscal, NumeroNotaFiscal,HttpContext.Current.Session["IDEmpresa"].ToString(), idRemetente.ToString(), idDestinatario.ToString(), PesoBruto, MetragemCubica, Volumes, ValorNota);

                    //insere na tabela de romaneio documento

                    if (PesoBruto == "")
                        PesoBruto = "0";

                    if (MetragemCubica == "")
                        MetragemCubica = "0";

                    if (ValorNota == "")
                        ValorNota = "0";

                    dal.InserirTabRomaneioDocumento(idRomaneio, idDocumento, Convert.ToInt32(Volumes), Convert.ToDecimal(PesoBruto) , Convert.ToDecimal(0), Convert.ToDecimal(MetragemCubica),Convert.ToDecimal( ValorNota));
                }

                #endregion
                
                //limpa a tabela atraves da chaver
                dal.LimparbyChave(chave, dt.Rows[i][1].ToString());
            }
        }

        private void ProcessarRegistroNF(string chave)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)HttpContext.Current.Session["USUARIO"];

            DataTable dt = new SistranDAO.Importacao().ConsultarEdiArqivoByChave(chave, "");
            SistranDAO.Importacao dal = new SistranDAO.Importacao();            
            SistranBLL.Pedido oPed = new Pedido();
            SistranBLL.Produto.Estoque oProd = new Produto.Estoque();

            dal.LimparbyChave(chave);

            int idDocumento = 0;
            int idFilial = 0;
            string CnpjRemetente = "";
            string CnpjDestinatario = "";
            string NumerodoPedido = "";
            string NumerodaNotaFiscal = "";
            string SeriedaNotaFiscal = "";
            string Volumes = "";
            string PesoBruto = "";
            string Valordopedido = "";
            string ObservacaodoPedido = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // string IdentificadorRegistro = dt.Rows[i][1].ToString().Substring(0, 1).Trim();

                string[] m = dt.Rows[i][1].ToString().Split(';');

                CnpjRemetente = m[0];
                CnpjDestinatario = m[1];
                NumerodoPedido = m[2];
                NumerodaNotaFiscal = m[3];
                SeriedaNotaFiscal = m[4];
                Volumes = m[5];
                PesoBruto = m[6];
                Valordopedido = m[7];
                ObservacaodoPedido = m[8];

                int idRemetente = dal.ConsultarCadastroByCNPJ(CnpjRemetente, "");
                int idDestinatario = dal.ConsultarCadastroByCNPJ(CnpjDestinatario, "");

                //grava a table de documento
                DataTable dtCliente = SistranBLL.Cliente.Read(Convert.ToInt32(HttpContext.Current.Session["IDEmpresa"].ToString()));
                idFilial = Convert.ToInt32(dtCliente.Rows[0]["IDFilialPadraoInternet"]);
                idDocumento = dal.InserirTabDocumento(idFilial.ToString(), idFilial.ToString(), SeriedaNotaFiscal, NumerodaNotaFiscal, HttpContext.Current.Session["IDEmpresa"].ToString(), idRemetente.ToString(), idDestinatario.ToString(), PesoBruto, "0", Volumes, Valordopedido.Replace(",", "."));


                string CodidodoProduto = m[9];
                string QuantidadePedido = Volumes;
                string QuantidadeFaturada = m[10];


                //Pega o valor do id ProdutoEmbalagem                    
                int[] mm = oPed.RetornarIdProdutoEmbalagemByIdprodutoCliente(CodidodoProduto);
                int idProdutoEmbalagem = mm[0];

                //Pega demais dados do pedido
                DataTable dx = dal.ConsultarDadosPed(idProdutoEmbalagem.ToString(), NumerodoPedido);

                //insere na PedidoItem
                oPed.InserirTabDocumentoItemPedido(idDocumento, idProdutoEmbalagem, ILusuario[0].UsuarioId, Convert.ToInt32(dx.Rows[0]["IdClienteDivisao"]), Convert.ToInt32(Convert.ToDouble(QuantidadeFaturada)), Convert.ToDecimal(dx.Rows[0]["VALORUNITARIO"]));

                //verifica e insere a documento Relacionado
                if (dal.LeDocReladionado(dx.Rows[0]["IDDOCUMENTO"].ToString(), idDocumento.ToString()) == 0)
                {
                    dal.GravarDocumentoRelacionado(dx.Rows[0]["IDDOCUMENTO"].ToString(), idDocumento.ToString());
                }

                //baixa o saldo na table de estoque
                int idEstoque = dal.RetornarIDEstoque(m[1].ToString(), idFilial.ToString(), QuantidadeFaturada);
                dal.BaixarSaldoTabelaEstoque(idEstoque.ToString(), QuantidadeFaturada);

                //grava a EstoqueMov
                decimal ve = (Convert.ToDecimal(QuantidadeFaturada) * Convert.ToDecimal(dx.Rows[0]["VALORUNITARIO"]));
                dal.GravarEstoqueMov(idEstoque.ToString(), m[1].ToString(), ILusuario[0].UsuarioId.ToString(), "SAIDA EDI - WEB", QuantidadeFaturada.ToString(), QuantidadeFaturada.ToString(), QuantidadeFaturada.ToString(), ve.ToString("#0.00"));

                //atualiza a estoque divisao
                int IdEstoqueDivisao = oProd.retornarIdEstoqueDivisao(idEstoque.ToString(), dx.Rows[0]["IdClienteDivisao"].ToString());
                oProd.AtualizarEstoqueDivisao(IdEstoqueDivisao, Convert.ToInt32(Convert.ToDouble(QuantidadeFaturada)), "-");
                oProd.InserirMovimentacaoSaidaEDI(IdEstoqueDivisao.ToString(), IdEstoqueDivisao.ToString(), ILusuario[0].UsuarioId.ToString(), QuantidadeFaturada);

            }
        }

        public void ExecutarSemRetorno(string chave, string linha, string Conn)
        {
            SistranDAO.Importacao DAL = new SistranDAO.Importacao();
            DAL.ExecutarSemRetorno(chave, linha, Conn);
        }
    }
}