<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GaiolaManut.aspx.cs" Inherits="ServicosWEB.GaiolaManut" %>




<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            font-size: medium;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
<h2 style="text-align: center">MANUTENÇÃO DE BANDEIRAS</h2>
</div>



<%--<fieldset style="margin:0 auto; width:99%; border: 1px solid silver;min-height:400px;">
<legend  style="font-size:12px">Pendencias</legend>
    <br />

            <asp:GridView ID="GridView1" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal"
                Width="99%" onrowcommand="GridView1_RowCommand" 
        style="margin:0 auto" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="IDGAIOLA" HeaderText="CÓDIGO" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DATA" HeaderText="DATA" >
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NUMEROCOLETOR" HeaderText="COLETOR" />
                    <asp:BoundField DataField="NOMEFILIAL" HeaderText="FILIAL" />
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" 
                                ImageUrl="~/Img/editar.png" CommandArgument='<%# Eval("[idgaiola]") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        
    <br />
</fieldset>
--%>


    <div style="width: 90%; margin:0 auto; ">               

            <asp:Panel ID="pnlPesq" runat="server" DefaultButton="btnPesquisar">

            <fieldset style="margin:0 auto; width:90%; border: 1px solid silver; min-height:400px" >
            <legend style="font-size:14px; font-weight:bold">OPÇÕES DE PESQUISA</legend>
            
                <table class="tb">
                    <tr>
                        <td style="white-space:nowrap; width:1%">
                            Código de Barras(Bandeira):</td>
                        <td>
                            <asp:TextBox ID="txtCodigoBarrasBandeira" runat="server" style="border:1px solid silver" Width="98%"></asp:TextBox>
                            </td>
                            <td style="width:1%">
                                <asp:Button ID="btnPesquisar" runat="server" onclick="btnPesquisar_Click" 
                                    style="border: 1px solid silver; background-color:White" Text="Pesquisar" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="white-space:nowrap; ">
                            <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="white-space:nowrap; ">
                            <hr />
                            </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="white-space:nowrap; ">
                            <asp:Panel ID="Panel1" runat="server" style="text-align: center" 
                                Visible="False">
                                <br />
                                <strong><span class="style2">BANDEIRA</span></strong><span class="style2">:
                                <asp:Label ID="lblBandeira" runat="server"></asp:Label>
                                &nbsp;<asp:Label ID="lblFilial" runat="server"></asp:Label>
                                <br />
                                </span>
                                <br />
                                <br />
                                <asp:Button ID="btnReabrir" runat="server" Font-Bold="False" 
                                    onclick="btnReabrir_Click" 
                                    style="border: 1px solid silver; background-color:White" 
                                    Text="ABRIR PARA NOVOS LANCAMENTOS" Height="50px" />
                                &nbsp;<asp:Button ID="btnExcluirVolume" runat="server" onclick="btnExcluirVolume_Click" 
                                    style="border: 1px solid silver; background-color:White" 
                                    Text="EXCLUIR VOLUME" Font-Bold="False" Height="50px" />
                                <br />
                                <br />
                            </asp:Panel>
                        </td>
                    </tr>

                </table>
                 <asp:Panel ID="Panel2" runat="server" Visible="False" DefaultButton="Button5">
                <table class="style1">
                    <tr>
                        <td>
                            Codigo de Barras do Volume</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCodigoBarrasBandeira0" runat="server" 
                                style="width:99%; border:1px solid silver"></asp:TextBox>
                        </td>
                        <td style="width:1%; white-space:nowrap">
                            <asp:Button ID="Button5" runat="server" BackColor="Red" BorderColor="White" 
                                BorderStyle="Solid" BorderWidth="1px" ForeColor="White" onclick="Button4_Click" 
                                Text="Confirmar Exclusão" Font-Bold="True" Width="140px" />
                            <asp:Button ID="Button3" runat="server" BackColor="#FF9966" 
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                                onclick="Button3_Click" Text="Cancelar / Finalizar" Font-Bold="True" 
                                Width="140px" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView2" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal"
                Width="99%" onrowcommand="GridView1_RowCommand" 
        style="margin:0 auto" AutoGenerateColumns="False" Visible="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkGrid" runat="server" ToolTip='<%# Eval("[IdGaiolaConferencia]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CODIGODEBARRAS" HeaderText="CÓDIGO DE BARRAS" />
                        <asp:BoundField DataField="PertenceAFilial" HeaderText="PERTENCE À FILIAL" />
                        <asp:BoundField DataField="SITUACAO" HeaderText="STATUS" />
                        <asp:BoundField DataField="NUMEROCOLETOR" HeaderText="COLETOR" />
                      
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                &nbsp;<br />
            </asp:Panel>
            </fieldset>
                <br />
            </asp:Panel>

           


            <br />

    </div>

    <br />

    </asp:Content>
