<%@ page language="C#" autoeventwireup="true" inherits="GerarExcelPainelFaturamento, App_Web_gerarexcelpainelfaturamento.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body  onload="javascript:window.close();">
    <form id="form1" runat="server">
    
    <div>
   <asp:Panel ID="Panel1" runat="server">
       Mês Atual Por Cliente<br />
       <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
       <br />
       <hr />
       Mês Atual Por Filial<br />
       <asp:PlaceHolder ID="phFilial" runat="server"></asp:PlaceHolder>
       <br />
       <br />
       <hr />
       Histórico dos últimos 12 meses<br />
       <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>

    </asp:Panel>
    </div>
    </form>
</body>
</html>
