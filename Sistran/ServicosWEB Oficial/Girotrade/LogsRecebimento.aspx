<%@ Page validateRequest="false" Title="" Language="C#" MasterPageFile="~/Girotrade/Site1.Master" AutoEventWireup="true" CodeBehind="LogsRecebimento.aspx.cs" Inherits="ServicosWEB.Girotrade.LogsRecebimento"   %>
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
            <td>Chave:&nbsp; </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" MaxLength="44" Width="300px"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" BackColor="White" BorderStyle="Solid" /></td>
            <td>

                &nbsp;</td>
             <td>
                 &nbsp;</td>

        </tr>
    </table>
    <hr />


    <asp:GridView ID="GridView1" runat="server" HeaderStyle-HorizontalAlign="Left" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" Font-Names="Verdana" Font-Size="10px"  >
        <Columns>
            <asp:TemplateField ControlStyle-Font-Size="10px">
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" BackColor="#FF3300" BorderStyle="Solid" BorderWidth="1px" OnClick="Button1_Click" Text="Reenviar" CommandArgument='<%# Eval("idDocumento") + "reenviar" %>' CommandName='<%# Eval("idRomaneio") %>' ForeColor="White" Font-Bold="True" />

                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Código Vex">
                <ItemTemplate>
                    <asp:TextBox ID="txtIdDocumento" runat="server" BorderStyle="None" Text='<%# Eval("IdDocumento") %>' Width="100px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Número NF">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox20" runat="server" BorderStyle="None" Text='<%# Eval("Numero") %>' Width="100px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Chave">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox21" runat="server" BorderStyle="None" Text='<%# Eval("Chave") %>' Width="330"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>


                        <asp:TemplateField HeaderText="Romaneio">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox22" runat="server" BorderStyle="None" Text='<%# Eval("IdRomaneio") %>' Width="100"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hora de Envio">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox23" runat="server" BorderStyle="None" Text='<%# Eval("DataHora") %>' Width="120"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status de Envio">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox24" runat="server" BorderStyle="None" Text='<%# Eval("Andamento") %>' Width="150"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

          <asp:TemplateField HeaderText="XML Enviado">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox25" Enabled="false" runat="server" BorderStyle="None" Text='<%# Eval("XMLEnviado") %>' Width="300" TextMode="MultiLine" Height="60px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>


             <asp:TemplateField HeaderText="Retorno da Yandeh">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox26"  Enabled="false" runat="server" BorderStyle="None" Text='<%# Eval("Resposta") %>' Width="300" TextMode="MultiLine" Height="60px"></asp:TextBox>
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
