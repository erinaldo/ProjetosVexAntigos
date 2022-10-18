using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web.UC
{
    public partial class UCGrafico : System.Web.UI.UserControl
    {
         public decimal percentualSeparacaoPedidos { get; set; }
         public decimal percentualEntregas{ get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string graf = "";
                //graf += " <div style='text-align:center'><b>Cenário: "+ DateTime.Now.ToString("dd/MM/yyyy")+"</b><br> ";

                //graf += " </div>";

                graf += " <div class='col-xs-10 col-sm-3 col-md-3 col-lg-3' style='text-align: center;'>";
                graf += " </div>";


                graf += " <div class='col-xs-10 col-sm-3 col-md-3 col-lg-3' style='text-align: center;'>";

                graf += " <div class='easy-pie-chart txt-color-orangeDark' data-percent='" + percentualSeparacaoPedidos + "' data-pie-size='100'>";
                graf += " <span class='percent percent-sign'>" + percentualSeparacaoPedidos + "</span>";
                graf += " <canvas height='50' width='0'></canvas></div>";
                graf += " <span> SEPARAÇÃO DE PEDIDOS </span>";
                graf += " </div>";

                //graf += " <div class='col-xs-10 col-sm-4 col-md-4 col-lg-4'>";
                //graf += " <div class='easy-pie-chart txt-color-blueDark' data-percent='0' data-pie-size='100'>";
                //graf += " <span class='percent percent-sign'>0</span>";
                //graf += " <canvas height='50' width='50'></canvas></div>";
                //graf += " <span>ENTREGA</span>";
                //graf += " </div>";

                graf += " <div class='col-xs-10 col-sm-3 col-md-3 col-lg-3' style='text-align: center;'>";
                graf += " <div class='easy-pie-chart txt-color-greenLight' data-percent='"+ percentualEntregas+"' data-pie-size='100'>";
                graf += " <span class='percent percent-sign'>" + percentualEntregas+ "</span>";
                graf += " <canvas height='50' width='0'></canvas></div>";
                graf += " <span class='easy-pie-title'> ENTREGA </span>";
                graf += " </div>";


                graf += "<br><br><br><br><br><br>";
                PlaceHolder1.Controls.Clear();
                PlaceHolder1.Controls.Add(new LiteralControl(graf));
            }
        }
    }
}