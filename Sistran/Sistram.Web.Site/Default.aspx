<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" BackColor="White" 
    Width="100%" style="text-align: center">
    
    <center>
        <table cellpadding="1" cellspacing="0" class="table" ID="tbTrocaCliente" runat="server" >
        <tr>
            <td style="background-image: url(Images/skins/primeiro/img/menu_3_2.jpg);" 
                class="tdpCabecalho" align="left">
                <b>Selecione o(s) cliente(s) que deseja trabalhar NESTA SESSÃO:</b></td>
        </tr>
        <tr>
            <td style="text-align: left" class="tdpCabecalho" align="left">
                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="cbo" 
                    Font-Names="Arial" 
                    onselectedindexchanged="ddlCliente_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnAdicionarFilial" runat="server" BackColor="#990000" 
                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="arial" 
                    Height="17px" onclick="btnAdicionarFilial_Click" Text="+" Width="20px" />
                &nbsp;<asp:Button ID="btnRemoverFilial" runat="server" BackColor="#990000" 
                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="arial" 
                    Height="17px" onclick="btnRemoverFilial_Click" Text="-" Width="20px" />
                &nbsp;<asp:Button ID="btnConfirmar" runat="server" BackColor="#990000" 
                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="arial" 
                    Height="17px" onclick="btnConfirmar_Click" Text="Confirmar" />
                <br />
            </td>
        </tr>
            <tr>
                <td class="tdpCabecalho" 
                    style="background-image: url(Images/skins/primeiro/img/menu_3_2.jpg);" 
                    align="left">
                    especificar Clientes :
                    <asp:Label ID="lblTodos" runat="server" Text="Todas" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdpCabecalho" style="text-align: left">
                    <asp:ListBox ID="ListBox1" runat="server" CssClass="txt" Height="60px" 
                        Visible="False">
                    </asp:ListBox>
                </td>
            </tr>
    </table></center>
    <br />
</asp:Panel>
</asp:Content>

