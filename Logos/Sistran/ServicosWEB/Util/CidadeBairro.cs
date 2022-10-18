﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ServicosWEB.Util
{
    public static class CidadeBairro
    {

        public static int RetornarCidade(string CEP, string cnx)
        {
           string sql = "";
            sql = "Select c.IdCidade from CidadeFaixaDeCep cfc  ";
            sql += " inner join Cidade c on c.IdCidade = cfc.IdCidade ";
            sql += " where " + CEP + " between CepInicial and CepFinal ";

           DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

            return 0;
        }

        public static int RetornarBairro(string Nome, string IdClidade, string cnx)
        {
            int Id = 0;
            Nome = Validacao.removerAcentos(Nome);

            string sql = "select IdBairro from Bairro where Nome = '" + Nome + "' and IdCidade=" + IdClidade;
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdBairro"]);
            else
            {
                Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("Bairro", cnx));
                sql = "Insert into Bairro (IdBairro, Nome, IdCidade) values (" + Id + ", '" + Nome + "', " + IdClidade + ")";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                return Id;
            }

            return Id;
        }
    }
}