<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientesComprovei.aspx.cs"
    Inherits="ServicosWEB.ClientesComprovei" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CLIENTES QUE ENVIA NOTAS AO COMPROVEI</title>
    <style type="text/css">
        body
        {
            font-size: 9px;
            margin: 0 0 0 0;
            font-family: Tahoma;
            white-space: nowrap;
            width: 100%;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function confirmation() 
        {
           
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1" style="width: 100%">
            <tr>
                <td>
                    <h1 style="text-align: center">
                        CLIENTE QUE ENVIA NOTAS AO COMPROVEI
                    </h1>
                    <div style="text-align: center">
                       <%-- <fieldset>
                            <legend>Incluir Cliente</legend>
                            <asp:Panel ID="Panel1" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            SELECIONE UM CLIENTE
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cboCliente" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAdcionar" runat="server" Text="Adicionar" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp;
                    </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Panel ID="Panel1" runat="server" style="text-align: center">
                        <asp:Label ID="Label1" runat="server" Text="CNPJ do Cliente: "></asp:Label>
                        &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox>
                        &nbsp;<asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="Solid" 
                            onclick="Button1_Click" Text="Incluir" />
                        &nbsp;<asp:Button ID="Button2" runat="server" BackColor="White" 
                            BorderStyle="Solid" onclick="Button2_Click" Text="Pesquisar" />
                        <br />
                        <asp:Label ID="Label2" runat="server" Font-Size="10pt"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                        ForeColor="Black" GridLines="Vertical" Width="100%" 
                        onrowcommand="GridView1_RowCommand">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="confirmation()" CommandArgument="excluir" CommandName='<% # Eval("CODIGO") %>'>Remover</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
