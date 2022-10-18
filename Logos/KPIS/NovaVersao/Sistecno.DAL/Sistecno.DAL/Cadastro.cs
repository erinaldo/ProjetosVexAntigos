using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Threading;
using System.Data.SqlClient;
using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class Cadastro
    {

        public int ProcurarIdImagemSite(int idCadastro, string cnx)
        {
            try
            {
                Sistecno_Entities context = new Sistecno_Entities();
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                var oC = context.CadastroImagem.FirstOrDefault(i => i.IDCadastro == idCadastro && i.TipoImagem == "SITE");
                context.Database.Connection.Close();
                return oC.IDCadastroImagem;
            }
            catch (Exception)
            {
                return 0;   
            }
           
        }

        public void GravarImagens(int idCadastro, int IdCadastroImagem, byte[] imagem, string cnx)
        {
            string idImagemSite = ProcurarIdImagemSite(idCadastro, cnx).ToString();


            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            cn.ConnectionString = cnx;

            try
            {
                cd.Connection = cn;
                cn.Open();
                string sql = "";

                if (IdCadastroImagem == 0 || idImagemSite == "" || idImagemSite == "0")
                {
                    string ID = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTROIMAGEM").ToString();
                    sql += " INSERT INTO CADASTROIMAGEM(";
                    sql += " IDCADASTROIMAGEM,";
                    sql += " IDCADASTRO,";
                    sql += " IMAGEM,";
                    sql += " NOME, TIPOIMAGEM";
                    sql += " ) VALUES";
                    sql += " (";
                    sql += ID + " ,";
                    sql += idCadastro + " ,";
                    sql += " @IMAGEM ,";
                    sql += " 'LOGOTIPO',";
                    sql += " 'SITE'";
                    sql += " )";
                }
                else
                {
                    sql += " UPDATE CADASTROIMAGEM SET IMAGEM=@IMAGEM  WHERE IDCADASTROIMAGEM=" + idImagemSite;

                }
                cd.Parameters.Add(new SqlParameter("@IMAGEM", imagem));
                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public DataTable Retornar(int idCadastro, string cnx)
        {
            string sql = "";
            sql = " SELECT EST.NOME ESTADO, CID.NOME CIDADE, BAR.NOME BAIRRO, * FROM CADASTRO CAD  LEFT JOIN CIDADE CID ON CID.IDCIDADE = CAD.IDCIDADE LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO LEFT JOIN BAIRRO BAR ON BAR.IDBAIRRO = CAD.IDBAIRRO WHERE CAD.IDCADASTRO=" + idCadastro;
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public DataTable RetornarCuringa(string texto, string IdCadastro, string cnx)
        {
            string sql = "";
            sql += " SELECT TOP 17 CAD.IDCADASTRO CODIGO,  CAD.CNPJCPF, ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) NOME, EST.NOME ESTADO, CID.NOME CIDADE, ";
            sql += " BAR.NOME BAIRRO, ";
            sql += " CAD.CEP  ";
            sql += " FROM CADASTRO CAD   ";
            sql += " LEFT JOIN CIDADE CID ON CID.IDCIDADE = CAD.IDCIDADE  ";
            sql += " LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO  ";
            sql += " LEFT JOIN BAIRRO BAR ON BAR.IDBAIRRO = CAD.IDBAIRRO  ";
            sql += " WHERE  ";
            if (IdCadastro == "")
            {
                sql += " (CAD.RAZAOSOCIALNOME LIKE '" + texto + "%' ";
                sql += " OR CAD.FANTASIAAPELIDO LIKE '" + texto + "%' ";
                sql += " OR CAD.CNPJCPF LIKE '" + texto + "%') ";
                sql += " AND CAD.ENDERECO <> '' AND CAD.ENDERECO IS NOT NULL";
            }
            else
            {
                sql += " CAD.IDCADASTRO = " + IdCadastro;
            }
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public DataTable RetronarClientesColetor(string cnx)
        {
            string sql = "SELECT IDCLIENTE, LTRIM(RTRIM(CNPJCPF)) + ' - ' + LEFT(FANTASIAAPELIDO, 30) NOME";
            sql += " FROM CLIENTEFILIAL CF  ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = CF.IDCLIENTE ";
            sql += " WHERE REALIZACONFERENCIA ='SIM' ";
            sql += " GROUP BY IDCLIENTE, FANTASIAAPELIDO, CNPJCPF ";
            sql += " ORDER BY FANTASIAAPELIDO ";
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);

        }

        /// <summary>
        /// Insere o Cadastro, Cliente e dados de telefone e email
        /// </summary>
        /// <param name="obj"> Dados do Cadastro </param>
        /// <param name="ends"> Lista com os contatos </param>
        /// <param name="cnx">String de Conexao</param>
        /// <returns></returns>
        //public void Inserir(DAL.Models.Cadastro obj,
        //                    List<DAL.Models.CadastroContatoEndereco> ends,
        //                    string cnx,
        //                    string nomeArqXml,
        //                    string CaminhoENomeSriptGeral,
        //                    int idPlano,
        //                    DAL.Models.Email confEmail,
        //                    string rntc
        //                     )
        //{
        //    try
        //    {
        //        string idEmpresa = "";
        //        List<ParametrosProcedures> list = new List<ParametrosProcedures>();
        //        ParametrosProcedures p = new ParametrosProcedures();


        //        p.nomePar = "IDCADASTRO";
        //        p.valorPar = DAL.BD.cDb.RetornarIDTabela(cnx, "Cadastro").ToString();
        //        p.tipoDeDados = "int";
        //        idEmpresa = p.valorPar;
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "CNPJCPF";
        //        p.valorPar = obj.CnpjCpf;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "RAZAOSOCIALNOME";
        //        p.valorPar = obj.RazaoSocialNome;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "FANTASIAAPELIDO";
        //        p.valorPar = obj.FantasiaApelido;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "INSCRICAORG";
        //        p.valorPar = obj.InscricaoRG;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "ENDERECO";
        //        p.valorPar = obj.Endereco;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "NUMERO";
        //        p.valorPar = obj.Numero;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "BAIRRO";
        //        p.valorPar = obj.Bairro;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "IDCIDADE";
        //        p.valorPar = obj.IDCidade;
        //        p.tipoDeDados = "int";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "CEP";
        //        p.valorPar = obj.Cep;
        //        p.tipoDeDados = "string";
        //        list.Add(p);


        //        for (int i = 0; i < ends.Count; i++)
        //        {

        //            p = new ParametrosProcedures();

        //            switch (ends[i].IDCadastroTipoDeContato)
        //            {
        //                case 1:
        //                    p.nomePar = "EMAIL";
        //                    p.valorPar = ends[i].Endereco;
        //                    p.tipoDeDados = "string";
        //                    list.Add(p);


        //                    p = new ParametrosProcedures();
        //                    p.nomePar = "IDEMAIL";
        //                    p.valorPar = DAL.BD.cDb.RetornarIDTabela(cnx, "cadastrocontatoendereco").ToString(); ;
        //                    p.tipoDeDados = "int";
        //                    list.Add(p);

        //                    break;

        //                case 2:

        //                    p.nomePar = "TELEFONE";
        //                    p.valorPar = ends[i].Endereco;
        //                    p.tipoDeDados = "string";
        //                    list.Add(p);


        //                    p = new ParametrosProcedures();
        //                    p.nomePar = "IDTELEFONE";
        //                    p.valorPar = DAL.BD.cDb.RetornarIDTabela(cnx, "cadastrocontatoendereco").ToString(); ;
        //                    p.tipoDeDados = "int";
        //                    list.Add(p);
        //                    break;
        //            }
        //        }

        //        //definindo o nome da base de dados
        //        string b = obj.RazaoSocialNome.Replace(" ", "");
        //        string c = "";

        //        if (b.Length < 10)
        //            b = b + frwSistecno.Validacoes.ZerosEsquerda(c, 10);

        //        b = "BD_" + frwSistecno.Validacoes.ZerosEsquerda(idEmpresa, 5) + "_" + b.Substring(0, 10);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "BASEDEDADOS";
        //        p.valorPar = b;
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        DataSet conf = new DataSet();
        //        conf.ReadXml(nomeArqXml);


        //        p = new ParametrosProcedures();
        //        p.nomePar = "IPDESTINO";
        //        p.valorPar = conf.Tables["tbDataBaseDestino"].Rows[0]["IP"].ToString();
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "PORTA";
        //        p.valorPar = conf.Tables["tbDataBaseDestino"].Rows[0]["PORTA"].ToString();
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "USRDESTINO ";
        //        p.valorPar = conf.Tables["tbDataBaseDestino"].Rows[0]["User"].ToString();
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "SENHADESTINO ";
        //        p.valorPar = conf.Tables["tbDataBaseDestino"].Rows[0]["Pass"].ToString();
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "STATUSCLIENTE ";
        //        p.valorPar = "EFETIVO";
        //        p.tipoDeDados = "string";
        //        list.Add(p);

        //        p = new ParametrosProcedures();
        //        p.nomePar = "IDPLANO";
        //        p.valorPar = idPlano.ToString();
        //        p.tipoDeDados = "int";
        //        list.Add(p);

        //        DAL.BD.cDbExecutarProcedure("PROC_CADASTRAR", list, cnx);

        //        _idEmpresa = idEmpresa;
        //        _b = b;
        //        _nomeArqXml = nomeArqXml;
        //        _CaminhoENomeSriptGeral = CaminhoENomeSriptGeral;
        //        _cad = obj;
        //        _ends = ends;
        //        _attrEMail = confEmail;
        //        _rntc = rntc;
        //        Thread t = new Thread(CadastroEmBackground);
        //        t.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        string _idEmpresa;
        string _b;
        string _nomeArqXml;
        string _CaminhoENomeSriptGeral;
        string _rntc;
        DAL.Models.Cadastro _cad;
        List<DAL.Models.CadastroContatoEndereco> _ends;
        DAL.Models.Email _attrEMail;

        //private void CadastroEmBackground()
        //{
        //    try
        //    {
        //        try
        //        {
        //            new frwSistecno.ConexaoPrincipal("").CriarDatabase(_b, int.Parse(_idEmpresa), _nomeArqXml, _CaminhoENomeSriptGeral);
        //        }
        //        catch (Exception)
        //        {
        //        }

        //        string cnxDestino = new frwSistecno.ConexaoPrincipal("").RetornarCxCliente(int.Parse(_idEmpresa), "");

        //        #region Cadastro Da Empresa
        //        DAL.Models.Cadastro cc = new DAL.Models.Cadastro();
        //        DAL.Models.DAO.Cadastro.ContatoEndereco Cce = new DAO.Cadastro.ContatoEndereco();

        //        cc.CnpjCpf = _cad.CNPJCPF;
        //        cc.FantasiaApelido = (_cad.RazaoSocialNome.ToUpper().Length > 30 ? _cad.RazaoSocialNome.ToUpper().Substring(0, 29) : _cad.RazaoSocialNome.ToUpper());
        //        cc.RazaoSocialNome = (_cad.RazaoSocialNome.ToUpper().Length > 60 ? _cad.RazaoSocialNome.ToUpper().Substring(0, 59) : _cad.RazaoSocialNome.ToUpper());
        //        cc.CnpjCpf = _cad.CNPJCPF;
        //        cc.Endereco = _cad.Endereco;
        //        cc.Numero = _cad.Numero;
        //        cc.IDCidade = int.Parse(_cad.IDCidade);
        //        cc.InscricaoRG = _cad.InscricaoRG;
        //        cc.Cep = _cad.CEP;
        //        int idCad = this.Gravar(cc, cnxDestino);

        //        _cad.IDCadastro = idCad;

        //        for (int i = 0; i < _ends.Count; i++)
        //        {
        //            DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
        //            cce.IDCadastro = idCad;
        //            cce.Endereco = _ends[i].Endereco;
        //            Cce.Gravar(cce, cnxDestino);
        //        }

        //        #endregion

        //        #region empresa

        //        BLL.EmpresaFilial e = new BLL.EmpresaFilial();
        //        DAL.Models.Empresa ee = new Empresa();

        //        ee.IDGrupo = 1;
        //        ee.Nome = _cad.FantasiaApelido;
        //        ee.PermiteCNPJErrado = "NAO";
        //        ee.PermiteIEErrada = "NAO";
        //        ee.Ativo = "SIM";
        //        ee.IDEmpresa = e.InserirEmpresa(ee, cnxDestino);
        //        #endregion

        //        #region Filial
        //        BLL.Filial f = new BLL.Filial();
        //        DAL.Models.Filial ff = new DAL.Models.Filial();

        //        ff.IDCadastro = _cad.IDCadastro;
        //        ff.Nome = "MATRIZ";
        //        ff.IDEmpresa = ee.IDEmpresa;
        //        ff.Ativo = "SIM";
        //        ff.NumeroDaFilial = _cad.IDCadastro;
        //        ff.Unidade = _cad.IDCadastro;

        //        int idFilial = f.Inserir(ff, cnxDestino);
        //        #endregion

        //        #region Usuario
        //        BLL.Usuario u = new BLL.Usuario();
        //        DAL.Models.Usuario uu = new DAL.Models.Usuario();

        //        string senha = new Random().Next(9999, 999999).ToString();
        //        uu.IDCadastro = _cad.IDCadastro;

        //        for (int i = 0; i < _ends.Count; i++)
        //        {
        //            if (_ends[i].Endereco.Contains("@") && _ends[i].Endereco.Contains("."))
        //            {
        //                uu.Login = _ends[i].Endereco;
        //            }
        //        }


        //        uu.Nome = _cad.FantasiaApelido;
        //        uu.Senha = senha;
        //        uu.Administrador = "NAO";
        //        uu.Tipo = "USUARIO";
        //        uu.Ativo = "SIM";
        //        uu.Site = "ASP";
        //        uu.ValidarUsuarioNoBD = "NAO";
        //        u.Inserir(uu, cnxDestino);


        //        #endregion


        //        #region cliente
        //        BLL.Cliente cl = new BLL.Cliente();
        //        int idcliente = cl.InserirStarter(cnxDestino);


        //        DAL.Models.DocumentoEletronicoParametro doc = new DocumentoEletronicoParametro();
        //        doc.IdFilial = idFilial;
        //        doc.TipoEletronico = "CTe";
        //        doc.Certificado = "";
        //        doc.TipoCertificado = "tcViaPropriedades";
        //        doc.Ambiente = "akhomologacao";
        //        doc.ArquivoServidoresHom = "cteServidoresHom.ini";
        //        doc.ArquivoServidoresProd = "cteServidoresProd.ini";
        //        doc.TipoCertificado = "ckFile";
        //        doc.VersaoManual = "2.00";
        //        doc.IgnoreInvalidCertificates = "1";
        //        doc.MaxSizeLoteEnvio = 500;
        //        doc.DiretorioLog = "";
        //        doc.DiretorioTemplates = "";
        //        doc.ValidarEsquemaAntesEnvio = "1";
        //        doc.DiretorioEsquemas = "";
        //        doc.MappingFileName = "";
        //        doc.FraseHomologacao = "";
        //        doc.FraseContingencia = "";
        //        doc.ModeloRetrato = "";
        //        doc.ModeloPaisagem = "";
        //        doc.LogotipoEmitente = "";
        //        doc.Modelo = "";
        //        doc.Serie = "1";
        //        doc.AnexarPDF = "1";
        //        doc.TimeOut = 0;
        //        doc.ConexaoSegura = "1";
        //        doc.QtdeCopias = 1;
        //        doc.EmailServidor = "";
        //        doc.EmailRemetente = "";
        //        doc.EmailAssunto = "";
        //        doc.EmailUsuario = "";
        //        doc.EmailSenha = "";
        //        doc.EmailTimeOut = 100;
        //        doc.UF = _cad.SIGLA;

        //        doc.cnpj = _cad.CNPJCPF.Replace(".", "").Replace("/", "").Replace("-", "");
        //        doc.DiretorioXml = "";
        //        doc.ViasDeImpressao = 1;
        //        doc.LineDelimiters = "|";
        //        doc.RNTRC = _rntc;


        //        cl.InserirDocumentoClienteParametro(doc, cnxDestino);


        //        #endregion

        //        _attrEMail.corpo = _attrEMail.corpo.Replace("@USUARIO@", uu.Login);
        //        _attrEMail.corpo = _attrEMail.corpo.Replace("@SENHA@", senha);
        //        _attrEMail.corpo = _attrEMail.corpo.Replace("@EMPRESA@", _idEmpresa);
        //        frwSistecno.OperacoesEmail.Enviar(_attrEMail);
        //    }
        //    catch (Exception ex)
        //    {
        //        frwSistecno.OperacoesEmail.EnviarInfoErros(ex);
        //    }


        //}

        public DataTable Retornar(string where, string cnx)
        {
            string sql = "SELECT TOP 50";
            sql += " C.IDCADASTRO ID,  ";
            sql += " C.CNPJCPF [CNPJ/CPF], ";
            sql += " C.INSCRICAORG [IE/RG], ";
            sql += " left(C.RAZAOSOCIALNOME, 40) [RAZAO SOCIAL], ";
            sql += " EST.UF, ";
            sql += " CID.NOME CIDADE, ";
            sql += " C.CEP, ATIVO ";
            sql += " FROM  CADASTRO C  WITH(NOLOCK)   ";
            sql += " LEFT JOIN CIDADE CID ON C.IDCIDADE = CID.IDCIDADE ";
            sql += " LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ";
            sql += where + " AND RAZAOSOCIALNOME <>''   ORDER BY 1 DESC ";
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public DAL.Models.Cadastro RetornarTabela(int idCdastro, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var oC = context.Cadastro.FirstOrDefault(i => i.IDCadastro == idCdastro);
            context.Database.Connection.Close();
            return oC;
        }

        public DAL.Models.Cadastro RetornarByCnpj(string cnpj, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var oC = context.Cadastro.FirstOrDefault(i => i.CnpjCpf == cnpj);
            context.Database.Connection.Close();
            return oC;
        }

        public DataTable RetornarByIdCasdastro(int idCadastro, string cnx)
        {
            string strsq = "SELECT * FROM CADASTRO WHERE IDCADASTRO=" + idCadastro;
            return DAL.BD.cDb.RetornarDataTable(strsq, cnx);
        }

        public DataSet RetornarTodosCampos(int codigo, string cnx/*, string CnpjCpf*/)
        {
            string sql = "  SELECT  C.*, C.IDCIDADE, CID.IDESTADO ";
            sql += " FROM CADASTRO C with (nolock)";
            sql += " left JOIN CIDADE CID ON CID.IDCIDADE = C.IDCIDADE";
            sql += " WHERE C.IDCADASTRO = " + codigo;
            sql += " SELECT CTC.NOME TIPODECONTATO,  CCE.ENDERECO ENDCONTADO, * FROM CADASTROCONTATOENDERECO CCE";
            sql += " LEFT JOIN CADASTROTIPODECONTATO CTC ON CTC.IDCADASTROTIPODECONTATO = CCE.IDCADASTROTIPODECONTATO  ";
            sql += " where CCe.IDCADASTRO=" + codigo;
            return DAL.BD.cDb.RetornarDataSet(sql, cnx);
        }


        /// <summary>
        /// Altera o cadastro basico
        /// </summary>
        /// <param name="cadastro"></param>
        /// <param name="contatos"></param>
        /// <param name="cnx"></param>
        public  void Alterar(DAL.Models.Cadastro cadastro, DataTable contatos, byte[] LogoTipo, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();

            try
            {
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                //alterar o cadastro

                var oC = context.Cadastro.First(i => i.IDCadastro == cadastro.IDCadastro);


                // oC.CnpjCpf = cadastro.CnpjCpf;
                oC.RazaoSocialNome = cadastro.RazaoSocialNome;
                oC.FantasiaApelido = cadastro.FantasiaApelido;
                oC.Cep = cadastro.Cep;
                oC.Endereco = cadastro.Endereco;
                oC.Numero = cadastro.Numero;
                oC.Complemento = cadastro.Complemento;
                oC.IDCidade = cadastro.IDCidade;
                oC.IDBairro = cadastro.IDBairro;
                oC.InscricaoRG = cadastro.InscricaoRG;
                oC.Regional = cadastro.Regional;
                oC.GrupoDePreco = cadastro.GrupoDePreco;
                oC.InscricaoMunicipal = cadastro.InscricaoMunicipal;
                context.SaveChanges();

                if (LogoTipo != null)
                    new DAL.Cadastro().GravarImagens(oC.IDCadastro, oC.IDCadastro, LogoTipo, cnx);

                for (int i = 0; i < contatos.Rows.Count; i++)
                {
                    if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() == "0" && contatos.Rows[i]["STATUS"].ToString() == "SIM")//NOVO
                    {
                        DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
                        cce.IDCadastroContatoEndereco = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroContatoEndereco");
                        cce.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());
                        cce.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                        cce.IDCadastro = cadastro.IDCadastro;
                        context.CadastroContatoEndereco.Add(cce);
                    }
                    else if (contatos.Rows[i]["STATUS"].ToString() == "SIM")//altera
                    {
                        int cod = int.Parse(contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString());
                        var occ = context.CadastroContatoEndereco.First(ii => ii.IDCadastroContatoEndereco == cod);

                        occ.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                        occ.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());

                    }
                    else if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() != "0" && contatos.Rows[i]["STATUS"].ToString() == "NAO")//EXCLUI
                    {
                        int cod = int.Parse(contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString());
                        var occ = context.CadastroContatoEndereco.First(ii => ii.IDCadastroContatoEndereco == cod);
                        context.CadastroContatoEndereco.Remove(occ);
                    }
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }

        public void Alterar(DAL.Models.Cadastro cadastro, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();

            try
            {
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                //alterar o cadastro

                var oC = context.Cadastro.First(i => i.IDCadastro == cadastro.IDCadastro);


                //oC.CnpjCpf = cadastro.CnpjCpf;
                oC.RazaoSocialNome = cadastro.RazaoSocialNome;
                oC.FantasiaApelido = cadastro.FantasiaApelido;
                oC.Cep = cadastro.Cep;
                oC.Endereco = cadastro.Endereco;
                oC.Numero = cadastro.Numero;
                oC.Complemento = cadastro.Complemento;
                oC.IDCidade = cadastro.IDCidade;
                oC.IDBairro = cadastro.IDBairro;
                oC.InscricaoRG = cadastro.InscricaoRG;
                oC.Regional = cadastro.Regional;
                oC.GrupoDePreco = cadastro.GrupoDePreco;
                oC.InscricaoMunicipal = cadastro.InscricaoMunicipal;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }

        /* public int Gravar(DAL.Models.Cadastro cad, DataTable contatos, string cnx)
         {
             var context = new SistecnoContext();
             context.Database.Connection.ConnectionString = cnx;
             //DbTransaction DbtTrans = context.Database.Connection.BeginTransaction();
             try
             {
                 context.Database.Connection.Open();
                 //prepara a inclusao do cadastro
                 cad.IDCadastro = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTRO");
                 context.Cadastro.Add(cad);
                 context.SaveChanges();

                 if (contatos != null)
                 {
                     for (int i = 0; i < contatos.Rows.Count; i++)
                     {

                         //FAZ A INCLUISAO
                         if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() == "0" && contatos.Rows[i]["STATUS"].ToString() == "SIM")
                         {
                             DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
                             cce.IDCadastroContatoEndereco = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroContatoEndereco");
                             cce.IDCadastroContatoEndereco = int.Parse(contatos.Rows[i]["IDCadastroContatoEndereco"].ToString());
                             cce.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());
                             cce.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                             cce.IDCadastro = cad.IDCadastro;
                             context.CadastroContatoEndereco.Add(cce);
                             context.SaveChanges();
                         }
                     }
                 }
                 return cad.IDCadastro;
             }
             catch (Exception ex)
             {                
                 throw ex;
             }
             finally
             {
                 if (context.Database.Connection.State == ConnectionState.Open)
                     context.Database.Connection.Close();
             }
         }
         */

        public int Gravar(DAL.Models.Cadastro cad, DataTable contatos, byte[] Logotipo, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            //DbTransaction DbtTrans = context.Database.Connection.BeginTransaction();
            try
            {
                context.Database.Connection.Open();
                //prepara a inclusao do cadastro
                cad.IDCadastro = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTRO");
                context.Cadastro.Add(cad);
                context.SaveChanges();

                if (Logotipo != null)
                    new DAL.Cadastro().GravarImagens(cad.IDCadastro, 0, Logotipo, cnx);

                if (contatos != null)
                {
                    for (int i = 0; i < contatos.Rows.Count; i++)
                    {

                        //FAZ A INCLUISAO
                        if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() == "0" && contatos.Rows[i]["STATUS"].ToString() == "SIM")
                        {
                            DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
                            cce.IDCadastroContatoEndereco = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroContatoEndereco");
                            // cce.IDCadastroContatoEndereco = int.Parse(contatos.Rows[i]["IDCadastroContatoEndereco"].ToString());
                            cce.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());
                            cce.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                            cce.IDCadastro = cad.IDCadastro;
                            context.CadastroContatoEndereco.Add(cce);
                            context.SaveChanges();
                        }
                    }
                }
                return cad.IDCadastro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }

        public int Gravar(Sistecno.DAL.Models.Cadastro cad, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            try
            {
                context.Database.Connection.Open();
                //prepara a inclusao do cadastro
                cad.IDCadastro = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTRO");
                context.Cadastro.Add(cad);
                context.SaveChanges();
                return cad.IDCadastro;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }

        }

        public int Gravar(DAL.Models.Cadastro cad, DataTable contatos, DAL.Models.Proprietario proprietario, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();

            try
            {

                //prepara a inclusao do cadastro
                cad.IDCadastro = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTRO");
                context.Cadastro.Add(cad);
                context.SaveChanges();

                if (proprietario != null)
                {
                    proprietario.IDProprietario = cad.IDCadastro;
                    context.Proprietario.Add(proprietario);
                    context.SaveChanges();
                }


                if (contatos != null)
                {
                    for (int i = 0; i < contatos.Rows.Count; i++)
                    {

                        //FAZ A INCLUISAO
                        if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() == "0" && contatos.Rows[i]["STATUS"].ToString() == "SIM")
                        {
                            DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
                            cce.IDCadastroContatoEndereco = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroContatoEndereco");
                            cce.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());
                            cce.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                            cce.IDCadastro = cad.IDCadastro;
                            context.CadastroContatoEndereco.Add(cce);
                            context.SaveChanges();
                        }
                    }
                }
                return cad.IDCadastro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }

        }

        /// <summary>
        /// Grava o cadastro com transação
        /// </summary>
        /// <param name="cad">Atributos do cadastro</param>
        /// <param name="contatos">DataTable com os Contatos</param>
        /// <param name="cnx">String de Conexao</param>
        /// <param name="trans">Transação ativa</param>
        /// <param name="context">Contexto de Dados</param>
        public void GravarCadastro(DAL.Models.Cadastro cad, DataTable contatos, string cnx, DbTransaction trans, Sistecno_Entities context)
        {
            try
            {

                //prepara a inclusao do cadastro
                cad.IDCadastro = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTRO");
                context.Cadastro.Add(cad);

                if (contatos != null)
                {
                    for (int i = 0; i < contatos.Rows.Count; i++)
                    {

                        if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() == "0" && contatos.Rows[i]["STATUS"].ToString() == "SIM")
                        {
                            DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
                            cce.IDCadastroContatoEndereco = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroContatoEndereco");
                            cce.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());
                            cce.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                            cce.IDCadastro = cad.IDCadastro;
                            context.CadastroContatoEndereco.Add(cce);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (context.Database.Connection.State == ConnectionState.Open)
                //    context.Database.Connection.Close();
            }

        }

        public class ContatoEndereco
        {

            public void Gravar(DAL.Models.CadastroContatoEndereco obj, string cnx)
            {
                Sistecno_Entities context = new Sistecno_Entities();
                DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
                cce.IDCadastroContatoEndereco = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroContatoEndereco");
                cce.IDCadastroTipoDeContato = obj.IDCadastroTipoDeContato;
                cce.Endereco = obj.Endereco;
                cce.IDCadastro = obj.IDCadastro;
                context.CadastroContatoEndereco.Add(cce);

            }

            public DataTable RetornarByIdCasdastro(int idCadastro, string cnx)
            {
                string sql = " SELECT CTC.NOME TIPODECONTATO,  CCE.ENDERECO ENDCONTADO, * FROM CADASTROCONTATOENDERECO CCE";
                sql += " LEFT JOIN CADASTROTIPODECONTATO CTC ON CTC.IDCADASTROTIPODECONTATO = CCE.IDCADASTROTIPODECONTATO  ";
                sql += " where CCe.IDCADASTRO=" + idCadastro;
                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }

            public DataTable RetornarEmailsByChave(string chaves, string cnx)
            {
                string sql = " SELECT DISTINCT ";
                sql += " CADEMI.RAZAOSOCIALNOME EMITENTE, ";
                sql += " CCEEMI.ENDERECO EMAIL_EMITENTE, ";
                sql += " CADREM.RAZAOSOCIALNOME REMETENTE, ";
                sql += " CCEREM.ENDERECO EMAIL_REMETENTE, ";
                sql += " CADDEST.RAZAOSOCIALNOME DESTINATARIO, ";
                sql += " CCEDEST.ENDERECO EMAIL_DESTINATARIO ";
                sql += " FROM DOCUMENTOELETRONICO DE  ";
                sql += " INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = DE.IDDOCUMENTO  ";
                sql += " INNER JOIN FILIAL F ON F.IDFILIAL = D.IDFILIAL ";
                sql += " INNER JOIN CADASTRO CADEMI ON CADEMI.IDCADASTRO = F.IDCADASTRO ";
                sql += " INNER JOIN CADASTRO CADREM ON CADREM.IDCADASTRO = D.IDREMETENTE ";
                sql += " INNER JOIN CADASTRO CADDEST ON CADDEST.IDCADASTRO = D.IDDESTINATARIO ";
                sql += " LEFT JOIN CADASTROCONTATOENDERECO CCEEMI ON CCEEMI.IDCADASTRO = CADEMI.IDCADASTRO AND CCEEMI.IDCadastroTipoDeContato=1 ";
                sql += " LEFT JOIN CADASTROCONTATOENDERECO CCEREM ON CCEREM.IDCADASTRO = CADREM.IDCADASTRO AND CCEREM.IDCadastroTipoDeContato=1 ";
                sql += " LEFT JOIN CADASTROCONTATOENDERECO CCEDEST ON CCEDEST.IDCADASTRO = CADDEST.IDCADASTRO AND CCEDEST.IDCadastroTipoDeContato=1 ";
                sql += " WHERE DE.IDNOTA IN(" + chaves + ") ";
                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }
        }

        public class Trasnportadora
        {
            public DataTable RetornaDadosTranspotadora(string cnpj, string cnx)
            {
                string sql = " SELECT * FROM CADASTRO C INNER JOIN TRANSPORTADORA T ON T.IDTRANSPORTADORA = C.IDCADASTRO WHERE C.CNPJCPF='" + cnpj + "' ";
                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }
        }

        public class Agregado
        {
            public DataTable RetornaDadosAgregado(string cpf, string cnx)
            {
                string sql = " SELECT * FROM AGREGADO A  INNER JOIN CADASTRO C ON C.IDCADASTRO = A.IDAGREGADO WHERE C.CNPJCPF = '" + cpf + "' ";
                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }
        }

        public class Motorista : Cadastro
        {

            public DataTable RetornarTodosCampos(int IdMotorista, string cnx)
            {
                string sql = " SELECT ";
                sql += " CAD.IDCADASTRO [CODIGO], ";
                sql += " CAD.CNPJCPF,  ";
                sql += " CAD.INSCRICAORG, ";
                sql += " CAD.RAZAOSOCIALNOME,  ";
                sql += " CAD.FANTASIAAPELIDO,  ";
                sql += " CAD.ENDERECO,  ";
                sql += " CAD.NUMERO,  ";
                sql += " CAD.COMPLEMENTO,  ";
                sql += " CAD.IDCIDADE,  ";
                sql += " CAD.IDBAIRRO,  ";
                sql += " CAD.CEP,  ";
                sql += " CAD.DATADECADASTRO,  ";
                sql += " CAD.ANIVERSARIO,  ";
                sql += " CAD.ORGAOEMISSOR, ";
                sql += " CID.NOME,  ";
                sql += " EST.UF, ";
                sql += " MOT.CARTEIRADEHABILITACAO, ";
                sql += " MOT.VALIDADEDAHABILITACAO, ";
                sql += " MOT.DATADAPRIMEIRAHABILITACAO, ";
                sql += " MOT.CATEGORIA,MOT.DATADENASCIMENTO, ";
                sql += " MOT.IDCIDADENASCIMENTO, ";
                sql += " MOT.NOMEDOPAI,MOT.NOMEDAMAE, ";
                sql += " MOT.CONJUGE, ";
                sql += " MOT.VITIMADEROUBOQUANTIDADE, ";
                sql += " MOT.SOFREUACIDADEQUANTIDADE, ";
                sql += " MOT.ESTADOCIVIL, ";
                sql += " MOT.DATADECADASTRO, ";
                sql += " MOT.CARREGAMENTOAUTORIZADOATE, ";
                sql += " MOT.VENCIMENTONOGERENCIADORDERISCO, ";
                sql += " MOT.ALIQUOTASESTSENAT, ";
                sql += " MOT.VINCULOCOMAEMPRESA, ";
                sql += " MOT.NUMEROPANCARD, ";
                sql += " MOT.ATIVO, ";
                sql += " MOT.LIBERADO, ";
                sql += " MOT.MOPP, ";
                sql += " MOT.AGUARDANDOLIBERACAO, ";
                sql += " MOT.VENCIMENTOPANCARY, ";
                sql += " MOT.VENCIMENTOBRASILRISK, ";
                sql += " MOT.VENCIMENTOBUONNY, ";
                sql += " MOT.DATADEBLOQUEIO, ";
                sql += " MOT.NUMEROREGISTROCNH, ";
                sql += " MOT.NUMEROINSS, ";
                sql += " MOT.ORIGEM, ";
                sql += " MOT.RECOLHEINSS, ";
                sql += " MOT.RECOLHEIRRF, ";
                sql += " MOT.RECOLHESESTSENAT, ";
                sql += " MOT.LOCALRG, ";
                sql += " MOT.LOCALEMISSAORG, ";
                sql += " CID.IDESTADO ,  ";
                sql += " CIDNASC.NOME CIDADENASCIMENTO, CIDNASC.IDESTADO IDESTADONASCIMENTO, CIDNASC.IDCIDADE IDCDIDADENASCIMENTO ,";
                sql += " CC.IDBANCO,CC.ANIVERSARIO, CC.DEPENDENTES,CC.BANCO ,CC.AGENCIA,CC.AGENCIADIGITO ,CC.CONTA,";
                sql += " CC.CONTADIGITO, CC.TIPOCONTA,  CC.CNPJCPFFAVORECIDO, CC.NOMEFAVORECIDO, CC.INSCRICAONOINSS, CC.INSCRICAOPIS, CC.VALORPENSAOALIMENTICIA, CC.VINCULOFAVORECIDO, ";
                sql += "  CC.APOSENTADO, CC.CONTRATADO, CC.DATAEXPEDICAORG,CC.ORGAOEXPEDICAORG, CC.ULTIMACOMPROVACAOENDERECO, isnull(PROP.IDPROPRIETARIO, 0) IDPROPRIETARIO ";
                sql += " FROM MOTORISTA MOT WITH (NOLOCK) ";
                sql += " INNER JOIN CADASTRO CAD ON (CAD.IDCADASTRO = MOT.IDMOTORISTA) ";
                sql += " LEFT JOIN CIDADE CID ON (CID.IDCIDADE = CAD.IDCIDADE) ";
                sql += " LEFT JOIN ESTADO EST ON (EST.IDESTADO = CID.IDESTADO) ";
                sql += " LEFT JOIN CADASTROCOMPLEMENTO CC ON CC.IDCADASTRO = CAD.IDCADASTRO ";
                sql += " LEFT JOIN CIDADE CIDNASC ON (CIDNASC.IDCIDADE = MOT.IDCIDADENASCIMENTO) ";
                sql += " LEFT JOIN PROPRIETARIO PROP ON (PROP.IDPROPRIETARIO = CAD.IDCADASTRO) ";
                sql += " WHERE MOT.IDMOTORISTA = " + IdMotorista;

                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }

            /// <summary>
            /// Grava o Motorista
            /// </summary>
            /// <param name="cad">Dados do Cadastro de Motorista</param>
            /// <param name="contatos">Data Table com os contatos</param>
            /// <param name="mot">Dados do Motorista</param>
            /// <param name="cnx">String de Conexão</param>
            public int GravarMotorista(DAL.Models.Cadastro cadastro, DAL.Models.CadastroComplemento cadastroComplemento, DataTable contatos, DAL.Models.Motorista mot, string cnx)
            {
                Sistecno_Entities context = new Sistecno_Entities();

                try
                {
                    GravarCadastro(cadastro, contatos, cnx, null, context);
                    mot.IDMotorista = cadastro.IDCadastro;
                    context.Motorista.Add(mot);
                    cadastroComplemento.IDCadastroComplemento = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroComplemento");
                    context.CadastroComplemento.Add(cadastroComplemento);
                    context.SaveChanges();
                    return cadastro.IDCadastro;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void AlterarMotorista(DAL.Models.Cadastro cadastro, DAL.Models.CadastroComplemento cadastroComplemento, DataTable contatos, DAL.Models.Motorista mot, string cnx)
            {
                Sistecno_Entities context = new Sistecno_Entities();

                try
                {
                    context.Database.Connection.ConnectionString = cnx;
                    context.Database.Connection.Open();

                    #region Cadastro
                    var oC = context.Cadastro.FirstOrDefault(i => i.IDCadastro == cadastro.IDCadastro);
                    if (oC != null)
                    {
                        //oC.IDCadastro = cadastro.IDCadastro;
                        oC.CnpjCpf = cadastro.CnpjCpf;
                        oC.RazaoSocialNome = cadastro.RazaoSocialNome;
                        oC.FantasiaApelido = cadastro.FantasiaApelido;
                        oC.Cep = cadastro.Cep;
                        oC.Endereco = cadastro.Endereco;
                        oC.Numero = cadastro.Numero;
                        oC.Complemento = cadastro.Complemento;
                        oC.IDCidade = cadastro.IDCidade;
                        oC.IDBairro = cadastro.IDBairro;
                        oC.InscricaoRG = cadastro.InscricaoRG;
                        oC.Regional = cadastro.Regional;
                        oC.GrupoDePreco = cadastro.GrupoDePreco;
                        oC.InscricaoMunicipal = cadastro.InscricaoMunicipal;
                        oC.InscricaoRG = cadastro.InscricaoRG;
                        context.SaveChanges();
                    }
                    #endregion


                    #region Cadastro Complemento
                    var oCC = context.CadastroComplemento.FirstOrDefault(i => i.IDCadastro == cadastro.IDCadastro);

                    if (oCC != null && cadastro != null)
                    {
                        oCC.IDCadastro = cadastro.IDCadastro;
                        //oCC.IDCadastroComplemento = cadastroComplemento.IDCadastroComplemento;
                        if (cadastroComplemento != null)
                        {
                            oCC.Banco = cadastroComplemento.Banco;
                            oCC.Agencia = cadastroComplemento.Agencia;
                            oCC.AgenciaDigito = cadastroComplemento.AgenciaDigito;
                            oCC.TipoConta = cadastroComplemento.TipoConta;
                            oCC.Conta = cadastroComplemento.Conta;
                            oCC.ContaDigito = cadastroComplemento.ContaDigito;
                            oCC.NomeFavorecido = cadastroComplemento.NomeFavorecido;
                            oCC.CnpjCpfFavorecido = cadastroComplemento.CnpjCpfFavorecido;
                            oCC.ValorPensaoAlimenticia = cadastroComplemento.ValorPensaoAlimenticia;
                            oCC.Aposentado = cadastroComplemento.Aposentado;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        //insere
                        if (cadastroComplemento != null)
                        {
                            cadastroComplemento.IDCadastro = cadastro.IDCadastro;
                            cadastroComplemento.IDCadastroComplemento = DAL.BD.cDb.RetornarIDTabela(cnx, "CADASTROCOMPLEMENTO");
                            context.CadastroComplemento.Add(cadastroComplemento);
                        }
                    }
                    #endregion


                    #region Motorista
                    var oM = context.Motorista.FirstOrDefault(i => i.IDMotorista == mot.IDMotorista);
                    if (oM != null)
                    {
                        //oM.IDMotorista = cadastro.IDCadastro;
                        oM.CarteiraDeHabilitacao = mot.CarteiraDeHabilitacao;
                        oM.NumeroRegistroCNH = mot.NumeroRegistroCNH;
                        oM.DataDaPrimeiraHabilitacao = mot.DataDaPrimeiraHabilitacao;
                        oM.ValidadeDaHabilitacao = mot.ValidadeDaHabilitacao;
                        oM.IDCidadeNascimento = mot.IDCidadeNascimento;
                        oM.LocalEmissaoRG = mot.LocalEmissaoRG;
                        oM.NumeroInss = mot.NumeroInss;
                        oM.NomeDaMae = mot.NomeDaMae;
                        oM.NomeDoPai = mot.NomeDoPai;
                        oM.VencimentoPancary = mot.VencimentoPancary;
                        oM.VencimentoBrasilrisk = mot.VencimentoBrasilrisk;
                        oM.NumeroPancard = mot.NumeroPancard;
                        oM.VencimentoBuonny = mot.VencimentoBuonny;
                        oM.VitimaDeRouboQuantidade = mot.VitimaDeRouboQuantidade;
                        oM.SofreuAcidadeQuantidade = mot.SofreuAcidadeQuantidade;
                        oM.AliquotaSestSenat = mot.AliquotaSestSenat;
                        oM.Conjuge = mot.Conjuge;
                        oM.EstadoCivil = mot.EstadoCivil;
                        context.SaveChanges();
                    }
                    else
                    {
                        mot.IDMotorista = mot.IDMotorista;
                        context.Motorista.Add(mot);
                        context.SaveChanges();
                    }

                    #endregion

                    if (contatos != null)
                    {
                        for (int i = 0; i < contatos.Rows.Count; i++)
                        {
                            if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() == "0" && contatos.Rows[i]["STATUS"].ToString() == "SIM")//NOVO
                            {
                                DAL.Models.CadastroContatoEndereco cce = new CadastroContatoEndereco();
                                cce.IDCadastroContatoEndereco = DAL.BD.cDb.RetornarIDTabela(cnx, "CadastroContatoEndereco");
                                cce.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());
                                cce.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                                cce.IDCadastro = cadastro.IDCadastro;
                                context.CadastroContatoEndereco.Add(cce);
                            }
                            else if (contatos.Rows[i]["STATUS"].ToString() == "SIM")//altera
                            {
                                int cod = int.Parse(contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString());
                                var occ = context.CadastroContatoEndereco.First(ii => ii.IDCadastroContatoEndereco == cod);

                                occ.Endereco = contatos.Rows[i]["ENDCONTADO"].ToString().ToUpper();
                                occ.IDCadastroTipoDeContato = int.Parse(contatos.Rows[i]["IDCadastroTipoDeContato"].ToString());

                            }
                            else if (contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() != "0" && contatos.Rows[i]["STATUS"].ToString() == "NAO")//EXCLUI
                            {
                                int cod = int.Parse(contatos.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString());
                                var occ = context.CadastroContatoEndereco.First(ii => ii.IDCadastroContatoEndereco == cod);
                                context.CadastroContatoEndereco.Remove(occ);
                            }
                            context.SaveChanges();
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (context.Database.Connection.State == ConnectionState.Open)
                        context.Database.Connection.Close();
                }
            }

            public DataTable RetornarMotorista(string where, string cnx)
            {
                string sql = "SELECT TOP 50";
                sql += " C.IDCADASTRO ID,  ";
                sql += " C.CNPJCPF [CNPJ/CPF], ";
                sql += " C.INSCRICAORG [IE/RG], ";
                sql += " C.RAZAOSOCIALNOME [RAZAO SOCIAL], ";
                sql += " EST.UF, ";
                sql += " CID.NOME CIDADE, ";
                sql += " C.CEP ";
                sql += " FROM  CADASTRO C  WITH(NOLOCK) INNER JOIN MOTORISTA M ON M.IDMOTORISTA = C.IDCADASTRO  ";
                sql += " LEFT JOIN CIDADE CID ON C.IDCIDADE = CID.IDCIDADE ";
                sql += " LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ";
                sql += where + " AND RAZAOSOCIALNOME <>''   order by C.RAZAOSOCIALNOME ";
                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }

            public DAL.Models.Motorista RetornarTable(int idCadastro, string cnx)
            {
                try
                {
                    Sistecno_Entities context = new Sistecno_Entities();
                    context.Database.Connection.ConnectionString = cnx;
                    context.Database.Connection.Open();
                    var oC = context.Motorista.FirstOrDefault(i => i.IDMotorista == idCadastro);
                    context.Database.Connection.Close();
                    return oC;
                }
                catch (Exception)
                {
                    return null;
                }

            }

        }

        public class TipoDeContato
        {
            public DataTable Retornar(string cnx)
            {
                return DAL.BD.cDb.RetornarDataTable("select * from CadastroTipoDeContato order by nome", cnx);
            }
        }

        public class CadadastroComplemento
        {
            public DAL.Models.CadastroComplemento RetornarTabela(int idCadastro, string cnx)
            {
                Sistecno_Entities context = new Sistecno_Entities();
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                var o = context.CadastroComplemento.First(i => i.IDCadastro == idCadastro);
                context.Database.Connection.Close();
                return o;
            }
        }

        public class Proprietario
        {
            public void MotoristaToProprietario(int idMotorista, bool p, string cnx)
            {
                if (idMotorista > 0)
                {
                    Sistecno_Entities context = new Sistecno_Entities();

                    context.Database.Connection.ConnectionString = cnx;
                    context.Database.Connection.Open();
                    var oC = context.Proprietario.FirstOrDefault(i => i.IDProprietario == idMotorista);


                    if (p && oC == null)
                    {
                        DAL.Models.Proprietario pr = new DAL.Models.Proprietario();
                        pr.IDProprietario = idMotorista;
                        context.Proprietario.Add(pr);
                    }
                    else
                    {
                        context.Proprietario.Remove(oC);
                    }
                    context.SaveChanges();
                    context.Database.Connection.Close();
                }
            }

            public int Inserir(int idCadastro, string cnx)
            {
                Sistecno_Entities context = new Sistecno_Entities();

                try
                {
                    context.Database.Connection.ConnectionString = cnx;
                    context.Database.Connection.Open();
                    bool existe = false;

                    try
                    {
                        var oC = context.Proprietario.FirstOrDefault(i => i.IDProprietario == idCadastro);
                        if (oC == null)
                            existe = false;
                        else
                            existe = true;

                    }
                    catch
                    {
                    }

                    if (!existe)
                    {
                        DAL.Models.Proprietario pr = new DAL.Models.Proprietario();
                        pr.IDProprietario = idCadastro;
                        context.Proprietario.Add(pr);
                        context.SaveChanges();
                    }
                    return idCadastro;
                }
                catch (Exception)
                {
                    return 0;
                }
                finally
                {
                    if (context.Database.Connection.State == ConnectionState.Open)
                        context.Database.Connection.Close();
                }
            }


            public DataTable Retornar(string where, string cnx)
            {
                string sql = "SELECT TOP 50";
                sql += " C.IDCADASTRO ID,  ";
                sql += " C.CNPJCPF [CNPJ/CPF], ";
                sql += " C.INSCRICAORG [IE/RG], ";
                sql += " C.RAZAOSOCIALNOME [RAZAO SOCIAL], ";
                sql += " EST.UF, ";
                sql += " CID.NOME CIDADE, ";
                sql += " C.CEP ";
                sql += " FROM  CADASTRO C  WITH(NOLOCK)   INNER JOIN PROPRIETARIO PROP ON PROP.IDPROPRIETARIO = C.IDCADASTRO";
                sql += " LEFT JOIN CIDADE CID ON C.IDCIDADE = CID.IDCIDADE ";
                sql += " LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ";
                sql += where + " AND RAZAOSOCIALNOME <>''   ORDER BY C.IDCADASTRO DESC ";
                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }


        }

    }
}