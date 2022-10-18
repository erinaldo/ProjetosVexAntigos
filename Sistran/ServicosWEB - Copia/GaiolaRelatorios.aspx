<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="GaiolaRelatorios.aspx.cs" Inherits="ServicosWEB.GaiolaRelatorios"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function PrintGridData() {

            var prtGrid;
            //   alert(document.getElementById('<%=GridView1.ClientID %>'));
            // alert(document.getElementById('<%=grdRelatorios.ClientID %>'));


            if (document.getElementById('<%=GridView1.ClientID %>') != null) {
                prtGrid = document.getElementById('<%=GridView1.ClientID %>');
            }
            else
                prtGrid = document.getElementById('<%=grdRelatorios.ClientID %>');

            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }



        function esconderDiv() {
            var dv = document.getElementById("dvMarcar");
            var lbl = document.getElementById("lbldvMarcar");
            var container = document.getElementById("container");
            dv.style.visibility = "hidden";
            container.style.visibility = "visible";
        }

        function verDiv(vlaor) {
            var dv = document.getElementById("dvMarcar");
            var lbl = document.getElementById("lbldvMarcar");
            var container = document.getElementById("container");
            var txt = document.getElementById("txtDvMarcar");
            dv.style.visibility = "visible";
            container.style.visibility = "hidden";
            lbl.value = vlaor;
            txt.focus();
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2 style="text-align: center;">
            Relatórios</h2>
    </div>
    <div style="width: 99%; margin: 0 auto;">
        <asp:Panel ID="pnlPorDia" runat="server" DefaultButton="btnPesquisar">
            <fieldset style="border: 1px solid silver;">
                <legend style="font-size: 14px; font-weight: bold">FILTROS</legend>
                <table cellpadding="2" cellspacing="2" style="width: 100%">
                    <tr>
                        <td style="width: 1%; white-space: nowrap">
                            Tipo de Relatório:
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td>
                            Período:
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="lblTitulo" runat="server" Text="Pré-Nota:"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            &nbsp;</td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="280px" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem>Movimento</asp:ListItem>
                                <asp:ListItem>Divegências</asp:ListItem>
                                <asp:ListItem>Produtividade</asp:ListItem>
                                <asp:ListItem>Produtividade Por Usuário</asp:ListItem>
                                <asp:ListItem>Produção Hora à Hora</asp:ListItem>
                                <asp:ListItem>Conferência Por Pre-Nota</asp:ListItem>
                                <asp:ListItem Selected="True">Movimento Geral</asp:ListItem>
                                <asp:ListItem >Consulta Completa de Gaiola</asp:ListItem>
                                <asp:ListItem>Consolidado Por Gaiola</asp:ListItem>
                                <asp:ListItem>Consolidado Por Dupla</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
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
                        <td nowrap="nowrap" width="1%">
                            <asp:TextBox ID="txtPrenota" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="1px" Enabled="False"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" width="1%">
                            <asp:DropDownList ID="cboRegioes" runat="server" Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" Style="border: 1px solid silver;
                                background-color: White" Text="Pesquisar" />
                            &nbsp;<%--<asp:Button ID="btnPesquisar0" runat="server" 
                            onclick="btnPesquisar0_Click" 
                            style="border: 1px solid' silver; background-color:White" Text="Imprimir" />--%><input
                                type="button" id="btnPrint" onclick="PrintGridData()" style="border: 1px solid silver;
                                background-color: White;" value="Imprimir" />&nbsp;
                            <asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" Style="border: 1px solid silver;
                                background-color: White" Text="Exportar" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                ForeColor="Black" GridLines="Vertical" OnRowCommand="GridView1_RowCommand" Style="margin: 0 auto"
                                Width="99%" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                Visible="False">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="DATA">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" ForeColor="Black"
                                                Text='<%# Eval("DATA") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FILIAL" HeaderText="FILIAL">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GAIOLAS" HeaderText="GAIOLAS">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VOLUMES" HeaderText="VOLUMES">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument="imprimir" 
                                            CommandName='<%# Eval("[FILIAL]") %>' Height="16px" 
                                            ImageUrl="~/Img/Impressora.png" Visible="false" />
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                            <br />
                            <asp:GridView ID="grdRelatorios" runat="server" BackColor="White" BorderColor="#999999"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                ForeColor="Black" GridLines="Vertical" Style="margin: 0 auto" Visible="False"
                                Width="99%" OnRowDataBound="grdRelatorios_RowDataBound">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
    </div>
</asp:Content>
