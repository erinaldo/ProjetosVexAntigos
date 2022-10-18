﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioFotoGestaoArmazenagem.aspx.cs" Inherits="ServicosWEB.RelatorioFotoGestaoArmazenagem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
    
        <asp:GridView ID="GridView1" runat="server" EnableModelValidation="True" 
            onrowdatabound="GridView1_RowDataBound" 
            CellPadding="3" CellSpacing="3">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        IMAGEM
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Width="90px"  Height="90px"
                            ImageUrl='<% # Eval("IdProdutoCliente") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="Validade" HeaderText="Validade" />--%>
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>