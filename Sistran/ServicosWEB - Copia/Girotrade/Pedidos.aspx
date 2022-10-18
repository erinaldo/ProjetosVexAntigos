<%@ Page Title="" Language="C#" MasterPageFile="~/Girotrade/Site1.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="ServicosWEB.Girotrade.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
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
     <style type="text/css">
        body
        {
            font-size: 12px;
            margin: 0 0 0 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            white-space: nowrap;
            width: 100%;
        }
        table
        {
            width: 100%
        }
    </style>
    <table style="width:200px">
        <tr>
            <td>N. Pedido: </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>

            <td>
                Status
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="Pendentes" Text="PENDENTES" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Faturados" Text="LIBERADO PARA FATURAMENTO" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="Todos" Text="TODOS" Selected="False"></asp:ListItem>
                </asp:DropDownList>
            </td>
           
             <td nowrap="nowrap" width="1%">
                            <asp:TextBox ID="txtDataI" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="1px" MaxLength="16" onblur="fctValidaData(this);" onKeyUp="mascara_data(this)"
                                TabIndex="2" Width="150px"></asp:TextBox>
                            &nbsp;até&nbsp;<asp:TextBox ID="txtDataF" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="1px" MaxLength="16" onblur="fctValidaData(this);" onKeyUp="mascara_data(this)"
                                TabIndex="3" Width="150px"></asp:TextBox>
                            &nbsp;
                        </td>
            <td>
                Registros:&nbsp;
            </td>
            
            <td>
               <asp:DropDownList ID="DropDownList2" runat="server">
                   <asp:ListItem>500</asp:ListItem>
                   <asp:ListItem>1000</asp:ListItem>
                   <asp:ListItem>1500</asp:ListItem>
                   <asp:ListItem>2000</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>

                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" BackColor="White" BorderStyle="Solid" /></td>
            <td>

                <asp:Button ID="Button3" runat="server" BorderColor="White" BorderStyle="Solid" OnClick="Button3_Click" Text="Exportar" />
            </td>
             <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>

        </tr>
    </table>
    <hr />


    <asp:GridView ID="GridView1" runat="server" HeaderStyle-HorizontalAlign="Left" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand"  >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px" OnClick="Button1_Click" Text="Ver" CommandArgument="ver"  CommandName='<%# Eval("[Número]") %>'/>
                    &nbsp;<asp:Button ID="Button2" runat="server" BackColor="#FF3300" BorderStyle="Solid" BorderWidth="1px" CommandArgument="AlterarStatus" CommandName='<%# Eval("[Número]") %>' ForeColor="White" OnClick="Button1_Click" Text="Liberar " />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
<HeaderStyle HorizontalAlign="Left" BackColor="#333333" Font-Bold="True" ForeColor="White"></HeaderStyle>
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
</asp:Content>
