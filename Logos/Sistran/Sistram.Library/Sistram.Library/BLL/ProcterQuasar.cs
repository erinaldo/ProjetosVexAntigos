using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranBLL
{
    public sealed class ProcterQuasar
    {
        public DataTable RerotnarLista(DateTime dti, DateTime dtf)
        {
            return new SistranDAO.ProcterQuasar().RerotnarLista(dti, dtf);
        }

        public DataTable RerotnarLista(DateTime dti, DateTime dtf, string cnpj)
        {
            return new SistranDAO.ProcterQuasar().RerotnarLista(dti, dtf, cnpj);
        }
    }
}