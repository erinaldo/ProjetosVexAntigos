using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web.UC
{
    public partial class dtrMensagensValidacao : System.Web.UI.UserControl
    {
        public  List<string> listMensagens { get; set; }
        public string TituloMensagem { get;set;}

        protected void Page_Load(object sender, EventArgs e)
        {
                     
        }

        public void MostrarMensagem()
        {
            if (listMensagens == null)
                return;

            phValida.Controls.Clear();

            phValida.Controls.Add(new LiteralControl("<h4><span class='has-error'>" + TituloMensagem + "</span></h4>"));

            for (int i = 0; i < listMensagens.Count; i++)
            {
                phValida.Controls.Add(new LiteralControl("<small class='help-block' data-bv-validator='notEmpty' data-bv-for='fullName' data-bv-result='INVALID' style=''>" + listMensagens[i] + "</small>"));
            }
        }
    }
}