using System.Data;
using System;

namespace SistranBLL
{
    public class Veiculo
    {
        public DataTable ListarMonitoramento(string data, string filiais, string cliente, string ordem, string Conn)
        {
            return new SistranDAO.Veiculo().ListarMonitoramento(data, filiais, cliente, ordem, Conn);
        }

        public DataTable Pesquisar(int? idVeiculo, string placa, bool AnttVencido, bool LicencVencido)
        {
            return new SistranDAO.Veiculo().Pesquisar(idVeiculo, placa, AnttVencido, LicencVencido);
        }

        public int Inserir(int IDVEICULOMODELO, int IDVEICULOTIPO, int IDVEICULORASTREADOR, int IDCIDADE, int IDPROPRIETARIO, int IDMOTORISTA, string PLACA, string RENAVAN, string CHASSI, int ANO, string COR, decimal CAPACIDADEDECARGAKG, decimal CAPACIDADEDECARGAM3, decimal QUATIDADEDEEIXOS, string CATEGORIASDECNHPERMITIDAS, string ANTT, string NUMEROSERIEEQUIPAMENTO, DateTime VencAntt, DateTime DataLicenc, string MarcaDescricao, string AnoModelo)
        {
            return new SistranDAO.Veiculo().Inserir(IDVEICULOMODELO, IDVEICULOTIPO, IDVEICULORASTREADOR, IDCIDADE, IDPROPRIETARIO, IDMOTORISTA, PLACA, RENAVAN, CHASSI, ANO, COR, CAPACIDADEDECARGAKG, CAPACIDADEDECARGAM3, QUATIDADEDEEIXOS, CATEGORIASDECNHPERMITIDAS, ANTT, NUMEROSERIEEQUIPAMENTO, VencAntt, DataLicenc, MarcaDescricao, AnoModelo);
        }

        public void Update(int IDVEICULO, int IDVEICULOMODELO, int IDVEICULOTIPO, int IDVEICULORASTREADOR, int IDCIDADE, int IDPROPRIETARIO, int IDMOTORISTA, string PLACA, string RENAVAN, string CHASSI, int ANO, string COR, decimal CAPACIDADEDECARGAKG, decimal CAPACIDADEDECARGAM3, decimal QUATIDADEDEEIXOS, string CATEGORIASDECNHPERMITIDAS, string ANTT, string NUMEROSERIEEQUIPAMENTO, DateTime VenctoAntt, DateTime DataLiecenc, string MarcaDescricao, string AnoModelo)
        {
            new SistranDAO.Veiculo().Update(IDVEICULO, IDVEICULOMODELO, IDVEICULOTIPO, IDVEICULORASTREADOR, IDCIDADE, IDPROPRIETARIO, IDMOTORISTA, PLACA, RENAVAN, CHASSI, ANO, COR, CAPACIDADEDECARGAKG, CAPACIDADEDECARGAM3, QUATIDADEDEEIXOS, CATEGORIASDECNHPERMITIDAS, ANTT, NUMEROSERIEEQUIPAMENTO, VenctoAntt, DataLiecenc, MarcaDescricao, AnoModelo);
        }

        public DataTable GerarReportVeiculo(bool AnntVenc)
        {
            return new SistranDAO.Veiculo().GerarReportVeiculo(AnntVenc);
        }

        public class Modelo
        {
            public DataTable Listar()
            {
                return new SistranDAO.Veiculo.Modelo().Listar();
            }

            public DataTable Listar(string IdVeiculoMarca)
            {
                return new SistranDAO.Veiculo.Modelo().Listar(IdVeiculoMarca);
            }

            public void AlterarIncluir(string texto, int IDVeiculoMarca, int IDVeiculoModelo)
            {
                new SistranDAO.Veiculo.Modelo().AlterarIncluir(texto, IDVeiculoMarca, IDVeiculoModelo);
            }
        }

        public class Marca
        {
            public DataTable Listar()
            {
                return new SistranDAO.Veiculo.Marca().Listar();
            }

            public void AlterarIncluir(string texto, int IDVeiculoMarca)
            {
                new SistranDAO.Veiculo.Marca().AlterarIncluir(texto, IDVeiculoMarca);
            }
        }

        public class Tipo
        {
            public DataTable Listar()
            {
                return new SistranDAO.Veiculo.Tipo().Listar();
            }
        }

        public class Rastreador
        {
            public DataTable Listar()
            {
                return new SistranDAO.Veiculo.Rastreador().Listar();

            }
        }

    }

    public class DocumentoDeTransporte
    {
        public DataTable Pesquisar(int IdDt, DateTime? ini, DateTime? fim, bool Pendente, string NumeroDT)
        {
            return new SistranDAO.DocumentoDeTransporte().Pesquisar(IdDt, ini, fim, Pendente, NumeroDT);
        }

        public DataTable RetornarDt(int IdDt, int IdFilial)
        {
            return new SistranDAO.DocumentoDeTransporte().RetornarDt(IdDt, IdFilial);
        }

        public DataTable RetornarRomaneios(int IdDt, int IdFilial)
        {
            return new SistranDAO.DocumentoDeTransporte().RetornarRomaneios(IdDt, IdFilial);
        }

    }

    public class KPI
    {
        public decimal Form01(string linha, string DataInicio, string DataFim, ref string strsql)
        {

            return new SistranDAO.KPI().Form01(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form02(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form02(linha, DataInicio, DataFim, ref strsql);

        }

        public decimal Form03(string linha, string DataInicio, string DataFim, ref string strsql)
        {

            return new SistranDAO.KPI().Form03(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form04(string linha, string DataInicio, string DataFim, ref string strsql)
        {

            return new SistranDAO.KPI().Form04(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form05(string linha, string DataInicio, string DataFim, ref string strsql)
        {

            return new SistranDAO.KPI().Form05(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form06(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form06(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form07(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form07(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form08(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form08(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form09(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form09(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form10(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form10(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form11(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form11(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form12(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form12(linha, DataInicio, DataFim, ref  strsql);
        }

        public decimal Form13(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form13(linha, DataInicio, DataFim, ref  strsql);
        }

        public decimal Form14(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form14(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form15(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form15(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form16(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form16(linha, DataInicio, DataFim, ref strsql);
        }

        public decimal Form17(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            return new SistranDAO.KPI().Form17(linha, DataInicio, DataFim, ref strsql);
        }
    }
    public class Faturamento
    {
        public DataTable PalletsRecebidos(string DataInicio, string DataFim)
        {
            return new SistranDAO.Faturamento().PalletsRecebidos(DataInicio, DataFim);
        }

        public DataTable LinhasExpedidas(string DataInicio, string DataFim)
        {
            return new SistranDAO.Faturamento().LinhasExpedidas(DataInicio, DataFim);
        }
    }

}

