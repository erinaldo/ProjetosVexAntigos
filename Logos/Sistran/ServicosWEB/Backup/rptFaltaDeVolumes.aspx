<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptFaltaDeVolumes.aspx.cs"
    Inherits="ServicosWEB.rptFaltaDeVolumes" %>

<%@ Register src="CtrMenu.ascx" tagname="CtrMenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Relatório de Volumes</title>
    <script language="javascript" type="text/javascript">

        function mascara_data(campo) {
            if (campo.value.length == 2) {
                campo.value += '/';
            }
            if (campo.value.length == 5) {
                campo.value += '/';
            }
        }

        function formataData(campo, evt) {
            //dd/MM/yyyy
            //alert(campo);
            evt = getEvent(evt);
            var tecla = getKeyCode(evt);
            if (!teclaValida(tecla))
                return;
            vr = campo.value = filtraNumeros(filtraCampo(campo));
            tam = vr.length;
            if (tam >= 2 && tam < 4)
                campo.value = vr.substr(0, 2) + '/' + vr.substr(2);
            if (tam == 4)
                campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 2) + '/';
            if (tam > 4)
                campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 2) + '/' + vr.substr(4);
        }

        function fctValidaData(obj) {
            var data = obj.value;

            if (data == "")
                return true;

            var dia = data.substring(0, 2)
            var mes = data.substring(3, 5)
            var ano = data.substring(6, 10)

            //Criando um objeto Date usando os valores ano, mes e dia.
            var novaData = new Date(ano, (mes - 1), dia);

            var mesmoDia = parseInt(dia, 10) == parseInt(novaData.getDate());
            var mesmoMes = parseInt(mes, 10) == parseInt(novaData.getMonth()) + 1;
            var mesmoAno = parseInt(ano) == parseInt(novaData.getFullYear());

            if (!((mesmoDia) && (mesmoMes) && (mesmoAno))) {
                alert('Data informada é inválida!');
                obj.focus();
                return false;
            }
            return true;
        }

    </script>
    <style type="text/css">
        body
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #666666;
            margin: 0;
        }
        
        .direito-inferior
        {
            position: absolute;
            background-image: url(imagens/direito-inferior.png);
            float: right;
            width: 262px;
            height: 261px;
            bottom: 0;
        }
    </style>
    <script type="text/JavaScript">
<!--

        function doLoad() {
            setTimeout("refresh()", 120000);
        }

        function refresh() {
            window.location.href = window.location;
        }
//-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 98%; margin: 0 auto;">
        <tr>
            <td style="width: 45%">
                <div>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Img/NovoLogoDistecno.jpg" Width="100px" />
                </div>
            </td>
            <td>
                <div style="margin: 0 auto; width: 200px; text-align: center;">
                    <strong style="font-family: Tahoma; font-size: 18px; font-weight: bold; color: #808080;
                        text-transform: uppercase">REPOSIÇÃO E SOBRAS</strong>
                </div>
            </td>
            <td style="width: 45%; text-align: right">
                <div>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Img/roge.jpg" Width="200px" />
                </div>
            </td>
        </tr>
    </table>
    <div style="margin: 0 auto; width: 100%; background-color: Black; text-align: center;">
        <uc1:CtrMenu ID="CtrMenu1" runat="server" />
    </div>
     <div style="margin:5px; width: 100%">
        <span style="font-size:14px; font-weight:bold; color:Black; font-family:Verdana;">RELATÓRIO DE FALTAS DE VOLUMES POR FILIAL</span>
    </div>
    <div style="margin: 0 auto; width: 100%">
        <table style="width: 99%; margin: 0 auto;">
            <tr>
                <td colspan="2">
                    <div style="width: 99%; margin: 0 auto">
                        <fieldset style="width: 99%; border: 1px solid silver; font-size: 10px;">
                            <legend><b>FILTROS</b></legend>
                            <table style="width: 100%; margin: 0 auto;">
                                <tr>
                                    <td style="width: 1%">
                                        FILIAL:&nbsp;
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cboStatus" runat="server" Width="99%" Style="border: 1px solid silver"
                                            OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblQuantidadeDocumentos" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                                    </td>
                                    <td style="text-align: right; width: 1%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PERÍODO:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataI" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                            BorderWidth="1px" Width="99%" MaxLength="10" onblur="fctValidaData(this);" TabIndex="2"
                                            onKeyUp="mascara_data(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        ATÉ:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataF" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                            BorderWidth="1px" Width="99%" MaxLength="10" onblur="fctValidaData(this);" TabIndex="3"
                                            onKeyUp="mascara_data(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="Solid" OnClick="Button1_Click"
                                            Text="Pesquisar" Width="120px" />
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="Button3" runat="server" BackColor="White" BorderStyle="Solid" OnClick="Button3_Click"
                                            Text="Gerar Excel" Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                        GridLines="Vertical" ForeColor="Black" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                        PageSize="50" Style="margin: 0 auto">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="TAHOMA" Font-Size="10px"
                            ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle Font-Names="Tahoma" Font-Size="9px" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
