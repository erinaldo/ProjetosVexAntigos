<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnviarConferenciaDET.aspx.cs"
    Inherits="ServicosWEB.EnviarConferenciaDET" %>

<%@ Register src="CtrMenu.ascx" tagname="CtrMenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reposição Roge</title>
    <style type="text/css">
        body
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #666666;
            margin: 2px;
        }
        .style1
        {
            width: 100%;
        }
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
    <script type="text/JavaScript">
        function doLoad() {
            setTimeout("refresh()", 120000);
        }

        function refresh() {
            window.location.href = window.location;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <table style="width:98%; margin:0 auto;">
    <tr>
    <td style="width:45%">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Img/NovoLogoDistecno.jpg" Width="100px" />
    </div></td>
    <td><div style="margin: 0 auto; width: 200px; text-align: center;">
        <strong style="font-family: Tahoma; font-size: 18px; font-weight: bold; color: #808080; text-transform: uppercase">REPOSIÇÃO E SOBRAS</strong>
    </div></td>
    <td style="width:45%; text-align:right">
    <div>
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Img/roge.jpg" Width="200px" />
    </div>
    </td>
    </tr>
    </table>

      <div style="margin: 0 auto; width: 100%; background-color: Black; text-align: center;">
          <uc1:CtrMenu ID="CtrMenu1" runat="server" />
    </div>
    <hr style="border:1px solid silver;" />

    
  
    <div style="margin: 0 auto; width: 100%">
        <table class="style1" style="width: 100%; font-size:12px">
            <tr>
                <td style="text-align: center; white-space:nowrap" colspan="2">
                        <asp:Button ID="Button4" runat="server" BackColor="White" BorderStyle="None" 
                            BorderWidth="1px" onclick="Button4_Click" Text="." 
                        BorderColor="#CC0000" Font-Bold="True" ForeColor="#CC0000" />
                </td>
                <td width="1%" rowspan="3">
                    STATUS:
                </td>
                <td rowspan="3">
                    <asp:Label ID="TextBox3" runat="server" Width="100%" 
                        BorderStyle="None" Font-Bold="True" Font-Names="Tahoma" 
                        ForeColor="#666666"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 1%; white-space:nowrap">
                    CHAVE DA NOTA FISCAL:
                </td>
                <td width="1%">
                    <asp:Label ID="TextBox1" runat="server" Width="458px" CssClass="style2" ReadOnly="True"
                        BorderStyle="None" Font-Bold="True" Font-Names="Tahoma" 
                        ForeColor="#666666"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    CLIENTE ESPECIAL:
                </td>
                <td width="1%">
                    <asp:Label ID="TextBox2" runat="server" Width="200px" CssClass="style2" ReadOnly="True"
                        BorderStyle="None" Font-Bold="True" Font-Names="Tahoma" 
                        ForeColor="#666666"></asp:Label>
                </td>
            </tr>
            </table>
    <hr style="border:1px solid silver;" />

            <table class="style1" style="width: 100%; font-size:12px">
            <tr>
                <td style="background-color: #CCCCCC; color:Black">
                    
                        
                        <b>RESULTADO ENVIO ROGE:</b> <br />
                    
                        
                        <asp:Label ID="txtResultadoDoEnvio" runat="server" ></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td>
                    </hr>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td>
                                VOLUMES
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:Button ID="btnMostrarTudo" runat="server" BackColor="#CCCCCC" 
                                    BorderColor="#666666" BorderStyle="Solid" onclick="btnMostrarTudo_Click" 
                                    Text="MOSTRAR TUDO" />
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td colspan="3">
                                <asp:GridView ID="grdVolumes" runat="server" BackColor="White" BorderColor="#999999"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                    Width="100%" ForeColor="Black" GridLines="Vertical" 
                                    onrowdatabound="grdVolumes_RowDataBound">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="TAHOMA" Font-Size="10px"
                                        ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle Font-Names="Tahoma" Font-Size="9px" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                ITENS
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: right">
                                ITENS NÃO PERTENCEM A NOTA FISCAL
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td style="text-align: right">
                                <strong style="width: 99%">VALOR TOTAL DE FALTAS / SOBRAS</strong></td>
                            <td style="text-align: right">
                    <asp:Label ID="lblTotalFalas" runat="server" Width="100%" 
                        BorderStyle="None" Font-Bold="True" Font-Names="Tahoma" 
                        ForeColor="#666666" style="text-align: center; color: #FF3300"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                &nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td colspan="2" style="width:50%">
                                <asp:GridView ID="grdItem" runat="server" BackColor="White" BorderColor="#999999"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                    Width="99%" ForeColor="Black" GridLines="Vertical" 
                                    onrowdatabound="grdItem_RowDataBound">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="TAHOMA" Font-Size="10px"
                                        ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle Font-Names="Tahoma" Font-Size="9px" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                            <td>
                                <asp:GridView ID="grdNotaItensQnaoPertencemANF" runat="server" BackColor="White"
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                    Width="99%" ForeColor="Black" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="TAHOMA" Font-Size="10px"
                                        ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle Font-Names="Tahoma" Font-Size="9px" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td colspan="3">
                                CONFERENCIA CEGA EFETUADA</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td colspan="3">
                                <asp:GridView ID="GRDcOFcEGA" runat="server" BackColor="White"
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                                    CellPadding="3" EnableModelValidation="True"
                                    Width="99%" ForeColor="Black" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="TAHOMA" Font-Size="10px"
                                        ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle Font-Names="Tahoma" Font-Size="9px" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:CheckBox ID="chkCiencia" runat="server" Font-Bold="True" 
                        ForeColor="#CC0000" 
                        Text="ESTA REPOSIÇÃO EXCEDE VALORES OU TEM ITENS QUE NÃO PERTENCEM A NOTA. TEM CERTEZA QUE DESEJA ENVIAR A ROGE? <br> *** O ACEITE VAI GERAR DÉBITOS, POR ISSO CONFIRME TODOS OS CADOS ANTES DE ENVIAR" 
                        Visible="False" AutoPostBack="True" 
                        oncheckedchanged="chkCiencia_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="Button2" runat="server" Text="ENVIAR CONFERENCIA A ROGE" BackColor="#003399"
                        BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                        OnClick="Button2_Click" ForeColor="White" Width="250px" />
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="REFAZER CONFERENCIA" BackColor="#FF3300"
                        BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                        OnClick="Button1_Click" ForeColor="White" Width="250px" />
                    <br />
                    <br />
                    <asp:Button ID="Button3" runat="server" Text="VOLTAR" BackColor="White" BorderColor="#666666"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" OnClick="Button3_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
