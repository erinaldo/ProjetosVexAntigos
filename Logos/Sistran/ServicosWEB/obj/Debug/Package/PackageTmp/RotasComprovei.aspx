<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RotasComprovei.aspx.cs" Inherits="ServicosWEB.RotasComprovei" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ENVIO PARA O COMPROVEI</title>
    <style type="text/css">
        body
        {
            font-size: 9px;
            margin: 0 0 0 0;
            font-family: Tahoma;
            white-space: nowrap;
            width:100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="style1" style="width:100%">
            <tr>
                <td>
                    <h1 style="text-align: center">
                        ENVIO DE ROTAS COMPROVEI
                    </h1>
                    <div style="text-align: right">
                        <asp:Label ID="txtProcessado" runat="server"></asp:Label>
                    </div>

                     <div style="text-align: center">
                         Número:&nbsp;
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                         Data:&nbsp;
                         <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
Até:&nbsp;

                         <asp:TextBox ID="txtDataAte" runat="server"></asp:TextBox>


                         &nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
                         </asp:DropDownList>
                           &nbsp;<asp:DropDownList ID="cboStatus" runat="server">
                             <asp:ListItem>Todos</asp:ListItem>
                             <asp:ListItem Value="Importa??o realizada com sucesso.">Importado Com Sucesso</asp:ListItem>
                             <asp:ListItem Value="Documento % j? est? em uma rota.%">Documento Já esta em Outra Rota</asp:ListItem>
                             <asp:ListItem Value="%driver does not exist! Import aborted%">Motorista Não Cadastrado</asp:ListItem>
                           
                             <asp:ListItem Value="%n?o existente no sistema%">Nota Não Existe No Sistema</asp:ListItem>
                           
                         </asp:DropDownList>
&nbsp;<asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                             Text="PESQUISAR" BackColor="White" BorderColor="#666666" />

                             &nbsp;<asp:Button ID="Button2" runat="server"  
                                    Text="Excel" BackColor="White" BorderColor="#666666" 
                             onclick="Button2_Click" style="height: 26px" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                        ForeColor="Black" GridLines="Vertical" Width="100%" 
                        onrowcommand="GridView1_RowCommand" AllowPaging="True" 
                        onpageindexchanged="GridView1_PageIndexChanged" 
                        onpageindexchanging="GridView1_PageIndexChanging" PageSize="200">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnReenviar" runat="server" BackColor="White" BorderColor="Red" 
                                        BorderStyle="Solid" BorderWidth="1px" CommandArgument="r" 
                                        Text="Reenviar Ao Comprovei" CommandName='<% # Eval("IdDT") %>' />
                                    &nbsp;<asp:Button ID="btnPausar" runat="server" BackColor="White" BorderColor="Red" 
                                        BorderStyle="Solid" BorderWidth="1px" CommandArgument="pausar" 
                                        CommandName='<% # Eval("IdDT") %>' Text="Parar Envio" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" Font-Size="16px" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                            Font-Bold="True" Font-Names="arial" Font-Size="14px" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
 <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
    </asp:Timer>
    </form>
</body>
</html>
