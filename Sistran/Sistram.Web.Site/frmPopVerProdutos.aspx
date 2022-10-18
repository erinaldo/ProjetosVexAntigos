<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPopVerProdutos.aspx.cs" Inherits="frmPopVerProdutos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produtos</title>
    <link href="Styles/estilos.css" rel="stylesheet" type="text/css" />
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
    <div style="width:98%; text-align:center">
    
        <asp:Repeater ID="Repeater1" runat="server" 
            onitemdatabound="Repeater1_ItemDataBound">
        <ItemTemplate>
        <table border="1" cellpadding="2" cellspacing="2" class="table" style='width:98%'>
            <tr>
                <td rowspan="7" class="tdp" valign="top" width="1%">
                    <asp:Image ID="Image1" runat="server" Height="100px" ImageUrl="~/Images/naoDisponivel.jpg" />
                </td>
                <td class="tdp" width="1%">
                    Código:</td>
                <td class="tdp" style="text-align: left">
                    <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap">
                    Código do Cliente:</td>
                <td class="tdp" style="text-align: left">
                    <asp:Label ID="lblCodigoCliente" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Descrição:</td>
                <td class="tdp" style="text-align: left">
                    <asp:Label ID="lblDescricao" runat="server"></asp:Label>
                </td>
            </tr>
            
            
             <tr>
                <td class="tdp">
                    Saldo:</td>
                <td class="tdp" style="text-align:right">
                    <asp:Label ID="lblsaldo" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="tdp" colspan="3">
                    <hr />
                    </td>
            </tr>
            </table>
        
        </ItemTemplate>
        </asp:Repeater>
    
    </div>
    <asp:HyperLink ID="HyperLink1" runat="server" 
        OnClick='javascript:window.close();return false;' CssClass="link">Fechar</asp:HyperLink>
    </form>
</body>
</html>
