<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioFoto.aspx.cs" Inherits="ServicosWEB.RelatorioFoto" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" runat="server">
    <title>Exportar Excel</title>
</head>
<body runat="server">
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" EnableModelValidation="True" 
            onrowdatabound="GridView1_RowDataBound" AutoGenerateColumns="False" 
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
                <asp:BoundField DataField="Codigo" HeaderText="Código " />
                <asp:BoundField DataField="CODIGODOCLIENTE" HeaderText="Cód. Cliente" />
                <asp:BoundField DataField="DESCRICAO" HeaderText="Descrição" />
                <asp:BoundField DataField="Grupo" HeaderText="Grupo" />
                <asp:BoundField DataField="SubGrupo" HeaderText="Sub-Grupo" />
                <asp:BoundField DataField="NOME" HeaderText="Divisões" />
                <asp:BoundField DataField="DATADECADASTRO" HeaderText="Data Do Cadastro" />
                <asp:BoundField DataField="ULTIMAENTRADA" HeaderText="Última Entrada" />
                <asp:BoundField DataField="Ativo" HeaderText="Ativo" />
                <asp:BoundField DataField="Saldo" HeaderText="Saldo" />
                <%--<asp:BoundField DataField="Validade" HeaderText="Validade" />--%>
                <asp:BoundField DataField="VlUnitario" HeaderText="Vl.Unitário" />
                <asp:BoundField DataField="CNPJCLIENTE" HeaderText="CNPJ Cliente" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
