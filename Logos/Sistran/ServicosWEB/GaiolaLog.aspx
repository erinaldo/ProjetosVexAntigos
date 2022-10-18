<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GaiolaLog.aspx.cs" Inherits="ServicosWEB.GaiolaLog" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script language="javascript" type="text/javascript">


            function PrintGridData() {
                var prtGrid = document.getElementById('<%=GridView1.ClientID %>');
                prtGrid.border = 0;
                var prtwin = window.open('','PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
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
            .style1
            {
                width: 100%;
            }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div>
<h2 style="text-align: center;">Relatório de Log</h2>
</div>

    <div style="width: 99%;margin:0 auto;max-width: 725px;">        
        <asp:Panel ID="pnlPorDia" runat="server" DefaultButton="btnPesquisar">
            <fieldset style="    border: 1px solid silver;">
            <legend style="font-size: 14px; font-weight:bold"> FILTROS</legend>
            
            <table cellpadding="2" cellspacing="2" style="width: auto">
                <tr>
                    <td style="width:1%; white-space:nowrap">
                        </td>
                    <td width="1%">
                        &nbsp;</td>
                    <td>
                        Gaiola:</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td nowrap="nowrap" width="1%">
                        <asp:TextBox ID="txtGaiola" runat="server" BorderColor="#999999" 
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="10" 
                            TabIndex="2" 
                            Width="100px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:Button ID="btnPesquisar" runat="server" onclick="btnPesquisar_Click" 
                            style="border: 1px solid silver; background-color:White" Text="Pesquisar" />
                        &nbsp;<%--<asp:Button ID="btnPesquisar0" runat="server" 
                            onclick="btnPesquisar0_Click" 
                            style="border: 1px solid' silver; background-color:White" Text="Imprimir" />--%><input type="button" id="btnPrint"  onclick="PrintGridData()"  style="border: 1px solid silver; background-color:White" value="Imprimir" /></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                        
                        <asp:GridView ID="GridView1" runat="server" 
                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                            CellPadding="3" EnableModelValidation="True" ForeColor="Black" 
                            GridLines="Vertical" onrowcommand="GridView1_RowCommand" 
                            style="margin:0 auto" Width="99%" onrowdatabound="GridView1_RowDataBound" 
                            onselectedindexchanged="GridView1_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" 
                                HorizontalAlign="Left" />
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
