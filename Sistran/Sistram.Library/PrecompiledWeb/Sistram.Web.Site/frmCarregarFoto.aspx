<%@ page language="C#" autoeventwireup="true" inherits="frmCarregarFoto, App_Web_qetdkgfc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/tableless.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White">
    <center>
        <form id="form1" runat="server">
        <div id="container">           
            
            <div class="linha">
                <div class="coluna">
                    <div class="linha">
                        <div class="colunaLabels">
                            Foto:
                        </div>
                        <div class="colunaInt">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" Width="100%" />
                        </div>
                    </div>
                    <div class="linha" style="margin-top: 40px;">
                        <div class="colunaLabels">
                            Descrição:</div>
                        <div class="colunaInt">
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txt" Width="99%" MaxLength="50"
                                TextMode="SingleLine" Height="40px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="linha" style="margin-top: 30px;">
                        <div class="colunaLabels">
                        </div>
                        <div class="colunaInt" style="text-align: right; margin-top: 30px;">
                            <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" CssClass="button" OnClick="Button1_Click" />
                        </div>
                    </div>
                    <div class="linha">
                    </div>
                </div>
                <div class="coluna45">
                    <asp:Panel ID="pnfoto" runat="server" BorderStyle="None" Height="350px" Direction="LeftToRight"
                        HorizontalAlign="Left" ScrollBars="Vertical">
                        <asp:DataList ID="DataList1" runat="server" BorderColor="Silver" BorderWidth="1px"
                            CellPadding="0" CellSpacing="0" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#666666" GridLines="Both" Height="99%"  Width="100%" HorizontalAlign="Left"
                            RepeatColumns="2" ShowFooter="False" OnItemDataBound="DataList1_ItemDataBound"
                            OnItemCommand="DataList1_ItemCommand">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="#666666" />
                            <ItemTemplate>
                                <div style="width: 180px; height: 160px; vertical-align: top">
                                    <br />
                                    <table style="height: 100px">
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgDL" runat="server" Height="80px" /><br />
                                                <asp:Label ID="lblDescricao" runat="server" Text='<% # Eval("texto") %>' Width="170px"
                                                    Font-Bold="false" Font-Size="7pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr valign="baseline">
                                            <td>
                                                <asp:Label ID="lblIdBanco" runat="server" Text='<% # Eval("id") %>' Visible="False"></asp:Label>
                                                <asp:Label ID="lblIdTemp" runat="server" Text='<% # Eval("idTemp") %>' Visible="False"></asp:Label>
                                                <asp:Button ID="btnExluirImagem" runat="server" CommandName='<% # Eval("idTemp") %>'
                                                    CommandArgument="ExcluirImagem" Text="Excluir" CssClass="button" />
                                                <asp:Button ID="btnAlterarImagem" runat="server" CommandName='<% # Eval("idTemp") %>'
                                                    CommandArgument="AlterarImagem" Text="Editar" CssClass="button" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </asp:Panel>
                </div>
            </div>
            <div class="linha">
            </div>
        </div>
        </form>
    </center>
    <asp:Label ID="lblIdFoto" runat="server" Visible="False"></asp:Label>
</body>
</html>
