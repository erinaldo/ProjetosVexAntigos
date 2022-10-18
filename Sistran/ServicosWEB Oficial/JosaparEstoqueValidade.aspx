<%@ Page Title="" Language="C#" MasterPageFile="~/mpJosapar.Master" AutoEventWireup="true"
    CodeBehind="JosaparEstoqueValidade.aspx.cs" Inherits="ServicosWEB.JosaparEstoqueValidade" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <h2 style="text-align: center;">
            ESTOQUE POR VALIDADE</h2>
    </div>
    <div style="width: 99%; margin: 0 auto">
        <br />
        <asp:Panel ID="pnlPesq" runat="server" DefaultButton="btnPesquisar">
            <fieldset style="margin: 0 auto; width: 300px; border: 1px solid silver">
                <legend>Opções de Pesquisa</legend>
                <table class="tb">
                    <tr>
                        <td style="white-space: nowrap; width: 1%">
                            Quantidade de dias:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoBarrasBandeira" runat="server" Style="width: 250px; border: 1px solid silver">90</asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" Style="border: 1px solid silver;
                                background-color: White" Text="Pesquisar" />
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
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <div style="width: 5%">
        </div>
</asp:Content>


