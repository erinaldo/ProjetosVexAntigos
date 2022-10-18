using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MAPA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       Carregar();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
       // Carregar();
    }

    private void Carregar()
    {
        PlaceHolder1.Controls.Clear();

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table cellpadding=0 cellspacing=0 Width=100% border=0 class='table'>"));

        for (int ii = 0; ii < 50; ii++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            for (int i = 0; i < 101; i++)
            {
                if (i == ii)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td style='width:1%' class='tdp'>"));
                    Image im = new Image();
                    im.ImageUrl = @"Imagens/Caminhao.gif";
                    im.Height = Convert.ToUInt16("10");
                    im.Width = Convert.ToUInt16("15");
                    PlaceHolder1.Controls.Add(im);
                }
                else
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td style='width:1%' class='tdp'>" + "x &nbsp;"));
                }
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

        lbl.Text = "Última Atualização: " + DateTime.Now.ToString();
    }
}
