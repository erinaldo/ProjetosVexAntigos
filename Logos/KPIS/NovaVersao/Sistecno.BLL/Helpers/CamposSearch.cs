using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL.Helpers
{
    public class CamposSearch
    {
        public string TextoExibicao;
        public string NomeCampo;
        public string Largura;
        public string Valor;
        public string CSS;
        public string FuncaoJS;
        public string TipoControle;
        public string PrefixoTabela;
        public DataTable DataSource;

        public CamposSearch(string _TextoExibicao, string _NomeCampo, string _Largura, string _CSS, string _FuncaoJS, string _tipoDeControle, DataTable _dataSource)
        {
            TextoExibicao = _TextoExibicao;
            NomeCampo = _NomeCampo;
            Largura = _Largura;
            CSS = _CSS;
            FuncaoJS = _FuncaoJS;
            TipoControle = _tipoDeControle;
            DataSource = _dataSource;
        }

        public CamposSearch(string _TextoExibicao, string _NomeCampo, string _Largura, string _CSS, string _FuncaoJS, string _tipoDeControle, DataTable _dataSource, string _prefixoTabela)
        {
            TextoExibicao = _TextoExibicao;
            NomeCampo = _NomeCampo;
            Largura = _Largura;
            CSS = _CSS;
            FuncaoJS = _FuncaoJS;
            TipoControle = _tipoDeControle;
            DataSource = _dataSource;
            PrefixoTabela = _prefixoTabela;
        }

    }
}
