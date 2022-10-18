<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEnviarFatura.aspx.cs" Inherits="Sistecno.Web.Fatura.frmEnviarFatura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enviar Fatura</title>
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size: 10px;
        }
        .style1
        {
            font-size: medium;
        }
    </style>
</head>
<body>
<div style="width:85%; margin:0 auto; text-align:center; border: 1px solid silver">
    <form id="form1" runat="server">

    <div style="text-align:center">
    <H2>ENVIO DE FATURA PARA O CLIENTE</H2>
    <hr />
    <br />
        <strong><span class="style1">Número da Fatura <br />
        </span></strong>
    <asp:TextBox ID="TextBox1" runat="server" Width="452px" BorderStyle="Solid"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Pesquisa" 
            onclick="Button1_Click" BorderStyle="None" Font-Bold="True" />
        <br />
        <br />
        <strong><span class="style1">Email Para Envio: 
        </span></strong> 
        <br />
    <asp:TextBox ID="txtemail" runat="server" Width="534px" BorderStyle="Solid">moises@sistecno.com.br</asp:TextBox>
        <br />
        <br />
        <br />
        <br />
</div>
   
    <div>
        
     <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            EnableModelValidation="True" GridLines="Horizontal" 
            AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
            Width="100%">
         <AlternatingRowStyle BackColor="#F7F7F7" />
         <Columns>
             <asp:BoundField DataField="NOME/NUMERO" HeaderText="CLIENTE / NÚMERO" />
           

             <asp:BoundField DataField="DATADEVENCIMENTO" HeaderText="VENCIMENTO" />
             <asp:BoundField DataField="FILIAL" HeaderText="FILIAL" />
             <asp:TemplateField HeaderText="Link">
             
                 <ItemTemplate>
                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Link") %>' Target="_blank">VER ==>></asp:HyperLink>
                 </ItemTemplate>
             
             </asp:TemplateField>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:Button ID="Button2" runat="server" Text="Enviar E-mail" 
                         CommandArgument='<%# Eval("Numero") %>' CommandName='<%# Eval("IdTitulo") %>' 
                         BorderStyle="None" />
                    
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
         <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
         <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
         <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
         <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
         <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    </asp:GridView>
    </div>
    </div>
    </form>
</body>
</html>
