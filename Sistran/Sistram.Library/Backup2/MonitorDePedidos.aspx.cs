using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sistran.Library;

namespace MonitorDePedidos
{
    public partial class MonitorDePedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!base.IsPostBack)
                {
                    this.carregar();
                }

                this.Timer1.Enabled = true;
                this.Timer1.Interval = int.Parse(this.txtTempo.Text) * 0xea60;
            }
            catch (Exception)
            {
                this.carregar();
            }

        }

        private void carregar()
        {
            this.Timer1.Enabled = false;
            this.PlaceHolder1.Controls.Clear();
            try
            {
                string[] columnNames = new string[] {"filial", "cliente", "IDCLIENTE"};
                //((Label) base.Master.FindControl("blbTituloMaster")).Text = "MONITOR DE PEDIDOS";
                DataSet set = GetDataTables.RetornarDataSetWS("EXEC PRC_PEDIDOSPENDENTES",
                    Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                DataTable table = set.Tables[0];
                DataTable table2 = set.Tables[1];
                if (table2.Rows.Count == 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table2.NewRow();
                        row["IDDOCUMENTO"] = 0;
                        row["CLIENTE"] = table.Rows[i]["cliente"].ToString();
                        row["FILIAL"] = table.Rows[i]["filial"].ToString();
                        row["IDCLIENTE"] = table.Rows[i]["IDCLIENTE"].ToString();
                        table2.Rows.Add(row);
                    }
                }

                int num2 = 0;
                while (true)
                {
                    if (num2 >= table.Rows.Count)
                    {
                        DataView defaultView = table2.DefaultView.ToTable(true, columnNames).DefaultView;
                        defaultView.Sort = "filial asc";
                        DataTable table4 = defaultView.ToTable();
                        this.abrirTable();
                        this.criarLinha(true);
                        this.criarCelula(true, "", ealinhamento.left, 1, false);
                        this.criarCelula(true, "FILIAL / CLIENTE", ealinhamento.left, 1, false);
                        this.criarCelula(true, "TOTAL DE <br>PEDIDOS", ealinhamento.center, 1, false);
                        this.criarCelula(true, "AGUARDANDO AUTORIZA\x00c7\x00c3O <br>DO CLIENTE", ealinhamento.center,
                            1, true);
                        this.PlaceHolder1.Controls.Add(new LiteralControl(
                            "<td rowspan='" + ((table.Rows.Count + (table4.Rows.Count * 2)) + 2) +
                            "' style='background-color:black'></td>"));
                        this.criarCelula(true, "PEDIDOS COM <br>A LOGOS", ealinhamento.center, 1, true);
                        this.criarCelula(true, "PEDIDOS AGUARDANDO <br>SEPARA\x00c7\x00c3O", ealinhamento.center, 1,
                            true);
                        this.criarCelula(true, "PEDIDOS EM <br>SEPARA\x00c7\x00c3O", ealinhamento.center, 1, true);
                        this.criarCelula(true, "AGUARDANDO EMISS\x00c3O <br>DE NF", ealinhamento.center, 1, true);
                        this.criarCelula(true, "00:00 <br> 09:59", ealinhamento.center, 1, true);
                        this.criarCelula(true, "10:00 <br> 11:59", ealinhamento.center, 1, true);
                        this.criarCelula(true, "12:00 <br> 14:59", ealinhamento.center, 1, true);
                        this.criarCelula(true, "15:00 <br> 17:59", ealinhamento.center, 1, true);
                        this.criarCelula(true, "18:00 <br> 23:59", ealinhamento.center, 1, true);
                        this.PlaceHolder1.Controls.Add(new LiteralControl(
                            "<td rowspan='" + ((table.Rows.Count + (table4.Rows.Count * 2)) + 2) +
                            "' style='background-color:black'></td>"));
                        this.criarCelula(true, "TOTAL DE NOTAS", ealinhamento.center, 1, true);
                        this.criarCelula(true, "AGUARDANDO EMBARQUE", ealinhamento.center, 1, true);
                        this.criarCelula(true, "ROMANEIO GERADO", ealinhamento.center, 1, true);
                        this.criarCelula(true, "DT SEM PORTARIA", ealinhamento.center, 1, true);
                        this.fecharLinha();
                        string str2 = "";
                        int num4 = 0;
                        while (true)
                        {
                            if (num4 >= table4.Rows.Count)
                            {
                                this.criarLinha(true);
                                this.criarCelula(true, "", ealinhamento.left, 1, false);
                                this.criarCelula(true, "", ealinhamento.right, 1, false);
                                this.criarCelula(true,
                                    this.zeroTOVazio(table.Compute("SUM(PEDIDOSGERAL)", "").ToString()),
                                    ealinhamento.right, 1, false);
                                this.criarCelula(true,
                                    this.zeroTOVazio(table.Compute("SUM(AUTOCLIENTE)", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true, this.zeroTOVazio(table.Compute("SUM(PEDIDOS)", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true, this.zeroTOVazio(table.Compute("SUM(AUTOLOGOS)", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true,
                                    this.zeroTOVazio(table.Compute("SUM(EMSEPARACAO)", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true,
                                    this.zeroTOVazio(table.Compute("SUM(AGUARDANDOEMISSAO)", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true, this.zeroTOVazio(table.Compute("SUM([00-08])", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true, this.zeroTOVazio(table.Compute("SUM([09-11])", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true, this.zeroTOVazio(table.Compute("SUM([12-14])", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true, this.zeroTOVazio(table.Compute("SUM([15-17])", "").ToString()),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(true, this.zeroTOVazio(table.Compute("SUM([18-23])", "").ToString()),
                                    ealinhamento.right, 1, true);
                                string texto = "0";
                                string str8 = table2.Compute("COUNT(IDDOCUMENTO)",
                                    "IDDOCUMENTO>0 AND SITUACAO='AGUARDANDO EMBARQUE' AND IDDT IS NULL").ToString();
                                string str9 = table2.Compute("COUNT(IDDOCUMENTO)",
                                    "IDDOCUMENTO>0 AND SITUACAO<>'AGUARDANDO EMBARQUE' AND IDDT IS NULL").ToString();
                                string str10 = table2.Compute("COUNT(IDDOCUMENTO)",
                                        "IDDOCUMENTO>0 AND IDROMANEIO IS not NULL AND DATADESAIDA IS NULL AND SITUACAO<>'AGUARDANDO EMBARQUE' AND IDDT IS NOT NULL")
                                    .ToString();
                                if (((int.Parse(str8) + int.Parse(str9)) + int.Parse(str10)).ToString() == "0")
                                {
                                    texto = "";
                                }

                                if (str8 == "0")
                                {
                                    str8 = "";
                                }

                                if (str9 == "0")
                                {
                                    str9 = "";
                                }

                                if (str10 == "0")
                                {
                                    str10 = "";
                                }

                                this.criarCelula(true, texto, ealinhamento.right, 1, true);
                                this.criarCelula(true, str8, ealinhamento.right, 1, true);
                                this.criarCelula(true, str9, ealinhamento.right, 1, true);
                                this.criarCelula(true, str10, ealinhamento.right, 1, true);
                                this.fecharLinha();
                                this.fecharTable();
                                break;
                            }

                            if (str2 != table4.Rows[num4]["filial"].ToString())
                            {
                                this.criarLinha(true, true);
                                this.criarCelula(true, "", ealinhamento.right, 1, false);
                                this.criarCelula(true, table4.Rows[num4]["filial"].ToString(), ealinhamento.left, 0x12,
                                    false);
                                this.fecharLinha();
                            }

                            this.criarLinha(false, (decimal) (num4 % 2));
                            this.criarCelula(false, table4.Rows[num4]["IDCLIENTE"].ToString(), ealinhamento.right, 1,
                                true);
                            this.criarCelula(false, "&nbsp;&nbsp;&nbsp;" + table4.Rows[num4]["CLIENTE"].ToString(),
                                ealinhamento.left, 1, false);
                            DataRow[] rowArray =
                                table.Select("cliente='" + table4.Rows[num4]["CLIENTE"].ToString() + "' and filial='" +
                                             table4.Rows[num4]["FILIAL"].ToString() + "'");
                            if (rowArray.Length > 0)
                            {
                                this.criarCelula(false,
                                    (rowArray[0]["PEDIDOSGERAL"].ToString() == "0")
                                        ? ""
                                        : rowArray[0]["PEDIDOSGERAL"].ToString(), ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["AUTOCLIENTE"].ToString() == "0")
                                        ? ""
                                        : rowArray[0]["AUTOCLIENTE"].ToString(), ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["PEDIDOS"].ToString() == "0") ? "" : rowArray[0]["PEDIDOS"].ToString(),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["AUTOLOGOS"].ToString() == "0")
                                        ? ""
                                        : rowArray[0]["AUTOLOGOS"].ToString(), ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["EMSEPARACAO"].ToString() == "0")
                                        ? ""
                                        : rowArray[0]["EMSEPARACAO"].ToString(), ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["AGUARDANDOEMISSAO"].ToString() == "0")
                                        ? ""
                                        : rowArray[0]["AGUARDANDOEMISSAO"].ToString(), ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["00-08"].ToString() == "0") ? "" : rowArray[0]["00-08"].ToString(),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["09-11"].ToString() == "0") ? "" : rowArray[0]["09-11"].ToString(),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["12-14"].ToString() == "0") ? "" : rowArray[0]["12-14"].ToString(),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["15-17"].ToString() == "0") ? "" : rowArray[0]["15-17"].ToString(),
                                    ealinhamento.right, 1, true);
                                this.criarCelula(false,
                                    (rowArray[0]["18-23"].ToString() == "0") ? "" : rowArray[0]["18-23"].ToString(),
                                    ealinhamento.right, 1, true);
                            }
                            else
                            {
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                                this.criarCelula(false, "", ealinhamento.right, 1, true);
                            }

                            string str3 = "0";
                            string[] strArray3 = new string[]
                            {
                                "IDDOCUMENTO>0 AND FILIAL='", table4.Rows[num4]["FILIAL"].ToString(), "' AND CLIENTE='",
                                table4.Rows[num4]["CLIENTE"].ToString(),
                                "' AND SITUACAO='AGUARDANDO EMBARQUE'  AND IDDT IS NULL"
                            };
                            string s = table2.Compute("COUNT(IDDOCUMENTO)", string.Concat(strArray3)).ToString();
                            string[] strArray4 = new string[]
                            {
                                "IDDOCUMENTO>0 AND FILIAL='", table4.Rows[num4]["FILIAL"].ToString(), "' AND CLIENTE='",
                                table4.Rows[num4]["CLIENTE"].ToString(),
                                "' AND SITUACAO<>'AGUARDANDO EMBARQUE' AND IDDT IS NULL"
                            };
                            string str5 = table2.Compute("COUNT(IDDOCUMENTO)", string.Concat(strArray4)).ToString();
                            string[] strArray5 = new string[]
                            {
                                "IDDOCUMENTO>0 AND FILIAL='", table4.Rows[num4]["FILIAL"].ToString(), "' AND CLIENTE='",
                                table4.Rows[num4]["CLIENTE"].ToString(),
                                "'AND IDROMANEIO IS not NULL AND DATADESAIDA IS NULL AND SITUACAO<>'AGUARDANDO EMBARQUE' AND IDDT IS NOT NULL"
                            };
                            string str6 = table2.Compute("COUNT(IDDOCUMENTO)", string.Concat(strArray5)).ToString();
                            str3 = ((int.Parse(s) + int.Parse(str5)) + int.Parse(str6)).ToString();
                            this.criarCelula(false, (str3 == "0") ? "" : str3, ealinhamento.right, 1, true);
                            this.criarCelula(false, (s == "0") ? "" : s, ealinhamento.right, 1, true);
                            this.criarCelula(false, (str5 == "0") ? "" : str5, ealinhamento.right, 1, true);
                            this.criarCelula(false, (str6 == "0") ? "" : str6, ealinhamento.right, 1, true);
                            this.fecharLinha();
                            str2 = table4.Rows[num4]["filial"].ToString();
                            num4++;
                        }

                        break;
                    }

                    bool flag = false;
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 < table2.Rows.Count)
                        {
                            if ((table2.Rows[num2]["filial"].ToString() != table2.Rows[num3]["filial"].ToString()) ||
                                (table.Rows[num2]["cliente"].ToString() != table2.Rows[num3]["cliente"].ToString()))
                            {
                                num3++;
                                continue;
                            }

                            flag = true;
                        }

                        if (!flag)
                        {
                            DataRow row = table2.NewRow();
                            row["IDDOCUMENTO"] = 0;
                            row["CLIENTE"] = table.Rows[num2]["cliente"].ToString();
                            row["FILIAL"] = table.Rows[num2]["filial"].ToString();
                            row["IDCLIENTE"] = table.Rows[num2]["IDCLIENTE"].ToString();
                            table2.Rows.Add(row);
                        }

                        num2++;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Timer1.Enabled = true;
                this.lblTempo.Text = "\x00daltima Atualiza\x00e7\x00e3o: " + DateTime.Now.ToString();
            }
        }

        public enum ealinhamento
        {
            left,
            center,
            right
        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.PlaceHolder1.Controls.Clear();
            this.Timer1.Enabled = true;
            this.carregar();
        }

        //public void criarCelula(bool titulo, string texto, ealinhamento alinhamento, int colspan, bool numeral)
        //{
        //    if (colspan == 0)
        //    {
        //        colspan = 1;
        //    }

        //    if (titulo)
        //    {
        //        object[] objArray = new object[]
        //        {
        //            "<td class='tdpCabecalho' align='", alinhamento.ToString(), "' nowrap=nowrap colspan=", colspan,
        //            " style='font-size:7pt;'  >", texto
        //        };
        //        this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray)));
        //        this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //    }
        //    else
        //    {
        //        if (numeral)
        //        {
        //            object[] objArray2 = new object[]
        //            {
        //                "<td class='tdpR' nowrap=nowrap align='", alinhamento.ToString(),
        //                "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
        //            };
        //            this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray2)));
        //        }
        //        else
        //        {
        //            object[] objArray3 = new object[]
        //            {
        //                "<td class='tdp' nowrap=nowrap align='", alinhamento.ToString(),
        //                "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
        //            };
        //            this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray3)));
        //        }

        //        this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //    }
        //}


        public void criarCelula(bool titulo, string texto, ealinhamento alinhamento, int colspan, bool numeral)
        {
            if (colspan == 0)
            {
                colspan = 1;
            }

            if (titulo)
            {
                object[] objArray = new object[]
                {
                    "<td class='tdpCabecalho' align='", alinhamento.ToString(), "' nowrap=nowrap colspan=", colspan,
                    " style='font-size:7pt;'  >", texto
                };
                this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray)));
                this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            }
            else
            {
                if (numeral)
                {
                    object[] objArray2 = new object[]
                    {
                        "<td class='tdpR' nowrap=nowrap align='", alinhamento.ToString(),
                        "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
                    };
                    this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray2)));
                }
                else
                {
                    object[] objArray3 = new object[]
                    {
                        "<td class='tdp' nowrap=nowrap align='", alinhamento.ToString(),
                        "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
                    };
                    this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray3)));
                }

                this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            }
        }

        public void criarCelula(bool titulo, string texto, ealinhamento alinhamento, int colspan, bool numeral,
            string classCss)
        {
            if (colspan == 0)
            {
                colspan = 1;
            }

            if (titulo)
            {
                object[] objArray = new object[]
                {
                    "<td class='tdpCabecalho' align='", alinhamento.ToString(), "' nowrap=nowrap colspan=", colspan,
                    " style='font-size:7pt;' VALIGN='BOTTOM' >", texto
                };
                this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray)));
                this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            }
            else
            {
                if (numeral)
                {
                    object[] objArray2 = new object[]
                    {
                        "<td class='", classCss, "' align='", alinhamento.ToString(), "'  colspan=", colspan, ">", texto
                    };
                    this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray2)));
                }
                else
                {
                    object[] objArray3 = new object[]
                    {
                        "<td class='", classCss, "' align='", alinhamento.ToString(), "' colspan=", colspan, ">", texto
                    };
                    this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray3)));
                }

                this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            }
        }

        //public void criarCelula(bool titulo, string texto, ealinhamento alinhamento, int colspan, bool numeral,
        //    string classCss)
        //{
        //    if (colspan == 0)
        //    {
        //        colspan = 1;
        //    }

        //    if (titulo)
        //    {
        //        object[] objArray = new object[]
        //        {
        //            "<td class='tdpCabecalho' align='", alinhamento.ToString(), "' nowrap=nowrap colspan=", colspan,
        //            " style='font-size:7pt;' VALIGN='BOTTOM' >", texto
        //        };
        //        this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray)));
        //        this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //    }
        //    else
        //    {
        //        if (numeral)
        //        {
        //            object[] objArray2 = new object[]
        //            {
        //                "<td class='", classCss, "' align='", alinhamento.ToString(), "'  colspan=", colspan, ">", texto
        //            };
        //            this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray2)));
        //        }
        //        else
        //        {
        //            object[] objArray3 = new object[]
        //            {
        //                "<td class='", classCss, "' align='", alinhamento.ToString(), "' colspan=", colspan, ">", texto
        //            };
        //            this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray3)));
        //        }

        //        this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //    }
        //}

        //public void criarCelulaRowspan(bool titulo, string texto, ealinhamento alinhamento, int colspan, bool numeral)
        //{
        //    if (colspan == 0)
        //    {
        //        colspan = 1;
        //    }

        //    if (titulo)
        //    {
        //        object[] objArray = new object[]
        //        {
        //            "<td class='tdpCabecalho' align='", alinhamento.ToString(), "' colspan=", colspan,
        //            " rowspan=2 style='font-size:7pt;' >", texto
        //        };
        //        this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray)));
        //        this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //    }
        //    else
        //    {
        //        if (numeral)
        //        {
        //            object[] objArray2 = new object[]
        //            {
        //                "<td class='tdpR' nowrap=nowrap align='", alinhamento.ToString(),
        //                "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
        //            };
        //            this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray2)));
        //        }
        //        else
        //        {
        //            object[] objArray3 = new object[]
        //            {
        //                "<td class='tdp' nowrap=nowrap align='", alinhamento.ToString(),
        //                "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
        //            };
        //            this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray3)));
        //        }

        //        this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //    }
        //}


        public void criarCelulaRowspan(bool titulo, string texto, ealinhamento alinhamento, int colspan, bool numeral)
        {
            if (colspan == 0)
            {
                colspan = 1;
            }

            if (titulo)
            {
                object[] objArray = new object[]
                {
                    "<td class='tdpCabecalho' align='", alinhamento.ToString(), "' colspan=", colspan,
                    " rowspan=2 style='font-size:7pt;' >", texto
                };
                this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray)));
                this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            }
            else
            {
                if (numeral)
                {
                    object[] objArray2 = new object[]
                    {
                        "<td class='tdpR' nowrap=nowrap align='", alinhamento.ToString(),
                        "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
                    };
                    this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray2)));
                }
                else
                {
                    object[] objArray3 = new object[]
                    {
                        "<td class='tdp' nowrap=nowrap align='", alinhamento.ToString(),
                        "' style='font-size:7pt;height:10px' colspan=", colspan, ">", texto
                    };
                    this.PlaceHolder1.Controls.Add(new LiteralControl(string.Concat(objArray3)));
                }

                this.PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            }
        }


        public void criarLinha(bool titulo)
        {
            if (titulo)
            {
                this.PlaceHolder1.Controls.Add(
                    new LiteralControl("<tr valign='top' style='background-color:#EEC591; height:15px'>"));
            }
            else
            {
                this.PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
            }
        }


        //public void criarLinha(bool titulo)
        //{
        //    if (titulo)
        //    {
        //        this.PlaceHolder1.Controls.Add(
        //            new LiteralControl("<tr valign='top' style='background-color:#EEC591; height:15px'>"));
        //    }
        //    else
        //    {
        //        this.PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
        //    }
        //}


        public void criarLinha(bool titulo, bool filial)
        {
            if (titulo)
            {
                this.PlaceHolder1.Controls.Add(
                    new LiteralControl("<tr valign='top' style='background-color: #EEC591; height:15px'>"));
            }
            else
            {
                this.PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
            }
        }

        //public void criarLinha(bool titulo, bool filial)
        //{
        //    if (titulo)
        //    {
        //        this.PlaceHolder1.Controls.Add(
        //            new LiteralControl("<tr valign='top' style='background-color: #EEC591; height:15px'>"));
        //    }
        //    else
        //    {
        //        this.PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
        //    }
        //}


        //public void criarLinha(bool titulo, decimal resto)
        //{
        //    if (titulo)
        //    {
        //        if (resto > 0M)
        //        {
        //            this.PlaceHolder1.Controls.Add(
        //                new LiteralControl("<tr valign='top' style='background-color:#EEC591; height:20px'>"));
        //        }
        //        else
        //        {
        //            this.PlaceHolder1.Controls.Add(
        //                new LiteralControl("<tr valign='top' style='background-color:#FFE7BA; height:20px'>"));
        //        }
        //    }
        //    else if (resto > 0M)
        //    {
        //        this.PlaceHolder1.Controls.Add(new LiteralControl("<tr style='background-color:white;'>"));
        //    }
        //    else
        //    {
        //        this.PlaceHolder1.Controls.Add(new LiteralControl("<tr style='background-color:#FFE7BA;'>"));
        //    }
        //}


        public void criarLinha(bool titulo, decimal resto)
        {
            if (titulo)
            {
                if (resto > 0M)
                {
                    this.PlaceHolder1.Controls.Add(
                        new LiteralControl("<tr valign='top' style='background-color:#EEC591; height:20px'>"));
                }
                else
                {
                    this.PlaceHolder1.Controls.Add(
                        new LiteralControl("<tr valign='top' style='background-color:#FFE7BA; height:20px'>"));
                }
            }
            else if (resto > 0M)
            {
                this.PlaceHolder1.Controls.Add(new LiteralControl("<tr style='background-color:white;'>"));
            }
            else
            {
                this.PlaceHolder1.Controls.Add(new LiteralControl("<tr style='background-color:#FFE7BA;'>"));
            }
        }


        public void criarLinhaFilial()
        {
            this.PlaceHolder1.Controls.Add(new LiteralControl("<tr valign='top' style='background-color:#EEC591; '>"));
        }


        //public void criarLinhaFilial()
        //{
        //    this.PlaceHolder1.Controls.Add(new LiteralControl("<tr valign='top' style='background-color:#EEC591; '>"));
        //}


        public void fecharLinha()
        {
            this.PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));
        }



        //public void fecharLinha()
        //{
        //    this.PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));
        //}

        //public void fecharTable()
        //{
        //    this.PlaceHolder1.Controls.Add(new LiteralControl("</table>"));
        //}



        public void fecharTable()
        {
            this.PlaceHolder1.Controls.Add(new LiteralControl("</table>"));
        }


        protected void txtTempo_TextChanged(object sender, EventArgs e)
        {
            this.Timer1.Enabled = true;
            this.Timer1.Interval = int.Parse(this.txtTempo.Text) * 0xea60;
            this.carregar();
        }

        public void abrirTable()
        {
            this.PlaceHolder1.Controls.Add(new LiteralControl("<table class='table' cellspacing=1 celpanding=1 width='100%'>"));
        }


        private string zeroTOVazio(string valor)
        {
          return  ((valor != "0") ? valor : "");
        }

    }
}