<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="EtiquetaCarreta.aspx.cs" Inherits="ServicosWEB.EtiquetaCarreta" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .auto-style1 {
            font-size: medium;
            font-weight: bold;
            height: 20px;
            text-align: center;
        }
        .auto-style2 {
            font-size: medium;
            font-weight: bold;
            height: 20px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function pergunta() {
            if (confirm("Deseja confirmar essa operação?")) {
                return true;
            } else {
                return false;
            }
        }


        function pergunta2(via) {
            if (via == "0")
                return true;

            if (confirm("Tem certeza imprimir uma nova via?")) {
                return true;
            } else {
                return false;
            }
        }

        function mascara_data(campo) {
            if (campo.value.length == 2) {
                campo.value += '/';
            }

            if (campo.value.length == 5) {
                campo.value += '/';
            }

            if (campo.value.length == 10)
                campo.value += ' ';

            if (campo.value.length == 13)
                campo.value += ':';
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
            //alert(data);

            if (data == "")
                return true;
            //10/10/2010 13:15
            var dia = data.substring(0, 2);
            var mes = data.substring(3, 5);
            var ano = data.substring(6, 10);
            var hora = data.substring(11, 13);
            var min = data.substring(14, 16);


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

            if (parseInt(hora) > 23 || parseInt(min) > 59) {
                alert('Hora informada é inválida!');
                obj.focus();
                return false;
            }



            return true;
        }

    </script>
    <div>
        <p style="text-align: right">
            **Caso nao saia o codigo de barras, favor instalar as fontes: <a href='f1.ttf'>Fonte
                1</a>&nbsp; <a href='f2.otf' style="text-align: right">Fonte 2</a>
        </p>
        <h2 style="text-align: center;">EMISSÃO DE ETIQUETAS (CARRETAS)</h2>
    </div>


    <table style="width: 95%; margin: 0 auto">
        <tr>
            <td class="auto-style1">NOVO EMBARQUE</td>
            <td class="auto-style1">EM PROCESSAMENTO / FINALIZADOS </td>
            <td class="auto-style2">A partir de:
                <asp:TextBox ID="txtData" runat="server" BorderColor="#000CCC" BorderStyle="Solid" BorderWidth="1px"  onblur="fctValidaData(this);" onKeyUp="mascara_data(this)" MaxLength="10" AutoPostBack="True" OnTextChanged="txtData_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 99%">
                    <tr>
                        <td><strong>Placa</strong>:</td>
                        <td>
                            <asp:TextBox ID="txtPlaca" runat="server" BorderColor="#000CCC" BorderStyle="Solid" BorderWidth="1px" MaxLength="8" Width="99%" AutoPostBack="True" OnTextChanged="txtPlaca_TextChanged1"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="GERAR" BackColor="White" BorderColor="#000CCC" BorderStyle="Solid" BorderWidth="1px" OnClick="Button1_Click" /></td>
                    </tr>
                    <tr>
                        <td>Motorista: </td>
                        <td>
                            <asp:Label ID="lblMotorista" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblIdVeiculo" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><strong>Filiais:</strong></td>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <asp:CheckBoxList ID="chkFiliais" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>

            </td>
            <td style="vertical-align: top" colspan="2">

                <div style="width: 100%; margin: 0 auto">

                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True"
                        ForeColor="Black" GridLines="Horizontal" Width="100%" OnRowCommand="GridView1_RowCommand"
                        Style="margin: 0 auto" OnRowDataBound="GridView1_RowDataBound"
                        OnLoad="GridView1_Load" OnUnload="GridView1_Unload">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/Img/Impressora.png"
                                        CommandName='<%# Eval("[CÓDIGO]") %>' CommandArgument="imprimir" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        
                        <asp:ImageButton ID="btnexcluir" runat="server"
                            ImageUrl="~/Img/images.jpg"
                            Height="16px"   CommandName='<%# Eval("[CÓDIGO]") %>' CommandArgument="apagar"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>

                    <rsweb:ReportViewer ID="rptViewer" runat="server"
                        Style="display: table !important; margin: 0px; overflow: auto !important;" ShowBackButton="true">
                    </rsweb:ReportViewer>
                </div>

            </td>
        </tr>
    </table>
</asp:Content>


