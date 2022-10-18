<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="Gaiolas.aspx.cs" Inherits="ServicosWEB.Gaiolas" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
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

    </script>
    <div>
        <p style="text-align: right">
            <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick">
            </asp:Timer>
            **Caso nao saia o codigo de barras, favor instalar as fontes: <a href='f1.ttf'>Fonte
                1</a>&nbsp; <a href='f2.otf' style="text-align: right">Fonte 2</a></p>
        <h2 style="text-align: center;">
            EMISSÃO DE BANDEIRAS</h2>
    </div>
    <div style="width: 99%; margin: 0 auto">
        <div style="text-align:center">
            <table class="style1">
                <tr>
                    <td>
        <asp:RadioButtonList ID="rbOpcoesImpressao" runat="server" RepeatColumns="4" Style="margin: 0 auto"
            AutoPostBack="true" OnSelectedIndexChanged="rbOpcoesImpressao_SelectedIndexChanged">
            <asp:ListItem Selected="True" Text="NÃO IMPRESSO" Value="NAO IMPRESSO"></asp:ListItem>
            <asp:ListItem Text="EM ABERTO" Value="EM ABERTO"></asp:ListItem>
            <asp:ListItem Selected="False" Text="EM CONFERÊNCIA" Value="EM CONFERÊNCIA"></asp:ListItem>
            <asp:ListItem Selected="False" Text="RE-IMPRESSÃO" Value="IMPRESSO"></asp:ListItem>
        </asp:RadioButtonList>
                    </td>
                    <td style="width:1%; nowrap:nowrap">
                            <asp:Button ID="btnPesquisar0" runat="server" Text="Atualizar" Style="border: 1px solid silver;
                                background-color: White" onclick="btnPesquisar0_Click" Font-Bold="True"  />
                        </td>
                    <td style="width:1%; white-space:nowrap">
                            <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                        </td>
                </tr>
            </table>
            </div>
        <br />
        <asp:Panel ID="pnlPesq" runat="server" Visible="false" DefaultButton="btnPesquisar">
            <fieldset style="margin: 0 auto; width: 300px; border: 1px solid silver">
                <legend>Opções de Pesquisa</legend>
                <table class="tb">
                    <tr>
                        <td style="white-space: nowrap; width: 1%">
                            Código / Código de Barras:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoBarrasBandeira" runat="server" Style="width: 250px; border: 1px solid silver"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Filial:
                        </td>
                        <td style="white-space: nowrap; width: 1%">
                            <asp:DropDownList ID="cboFilial" Style="width: 250px;" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" Style="border: 1px solid silver;
                                background-color: White" OnClick="btnPesquisar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True"
            ForeColor="Black" GridLines="Horizontal" Width="90%" OnRowCommand="GridView1_RowCommand"
            Style="margin: 0 auto" OnRowDataBound="GridView1_RowDataBound" 
            onload="GridView1_Load" onunload="GridView1_Unload">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/Img/Impressora.png"
                            CommandName='<%# Eval("[CÓDIGO]") %>' CommandArgument="imprimir" ToolTip='<% # Eval("IMPRESSO") %>' />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:ImageButton ID="btnexcluir" runat="server"
                            ImageUrl="~/Img/images.jpg" CommandName='<%# Eval("[CÓDIGO]") %>' CommandArgument="excluir"
                            Height="16px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <div style="width: 5%">
        <rsweb:ReportViewer ID="rptViewer" runat="server" Style="display: table !important;margin: 0px; overflow: auto !important;" ShowBackButton="true">
        </rsweb:ReportViewer>
        </div>
</asp:Content>


