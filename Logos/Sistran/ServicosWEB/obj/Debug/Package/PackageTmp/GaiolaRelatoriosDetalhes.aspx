﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GaiolaRelatoriosDetalhes.aspx.cs" Inherits="ServicosWEB.GaiolaRelatoriosDetalhes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script language="javascript" type="text/javascript">

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

        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div>
<h2 style="text-align: center;">
    <asp:Label ID="lbltitulo" runat="server"></asp:Label>
        </h2>
</div>

    <div style="width: 99%;margin:0 auto;max-width: 725px;">        
        <asp:Panel ID="pnlPorDia" runat="server">
            &nbsp;</asp:Panel>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#999999" 
            BorderStyle="Solid" BorderWidth="1px" 
                            CellPadding="3" EnableModelValidation="True" ForeColor="Black" 
                            GridLines="Vertical" onrowcommand="GridView1_RowCommand" 
                            style="margin:0 auto" Width="99%" onrowdatabound="GridView1_RowDataBound" 
                            onselectedindexchanged="GridView1_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="DATA" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" 
                                            ForeColor="Black" Text='<%# Eval("DATA") %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                

                                <asp:BoundField DataField="GAIOLA" HeaderText="GAIOLA" >
                                </asp:BoundField>

<asp:BoundField DataField="DATA DO FECHAMENTO" HeaderText="DATA DO FECHAMENTO">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
                                
                                <asp:BoundField DataField="STATUS" HeaderText="STATUS" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>


                                <asp:BoundField DataField="VOLUMES" HeaderText="VOLUMES" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%--<asp:ImageButton ID="ImageButton1" runat="server" CommandArgument="imprimir" 
                                           Height="16px" 
                                           ImageUrl="~/Img/Impressora.png" />
                                        &nbsp;--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" 
                                HorizontalAlign="Left" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
        <br />

    </div>
</asp:Content>