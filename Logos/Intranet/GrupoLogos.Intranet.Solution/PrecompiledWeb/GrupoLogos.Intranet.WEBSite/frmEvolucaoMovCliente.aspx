<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmEvolucaoMovCliente, App_Web_frmevolucaomovcliente.aspx.cdcab7d2" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

<script language="javascript" type="text/jscript">

    function gerarExcel() 
    {
        var el = document.createElement("iframe");
        el.setAttribute('id', 'ifrm');
        document.body.appendChild(el);
        el.setAttribute('src', 'frmphExcel.aspx');
    }
</script>

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="baseline">
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                    Mês / Ano:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 2%">
                    <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px" MaxLength=7></asp:TextBox>
                    <%--<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/9999" MaskType="Date" TargetControlID="txtI"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>--%>
                    &nbsp;Até:
                    <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px" MaxLength=7></asp:TextBox>
                   <%-- <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/9999" MaskType="Date" TargetControlID="txtF"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>--%>
                </td>
               <%-- <td class="tdp" nowrap="nowrap" style="width: 2%" valign="middle">
                    Cliente:</td>
                <td class="tdp" nowrap="nowrap" style="width: 2%" valign="middle">
                    <asp:DropDownList ID="cboCliente" runat="server" CssClass="cbo" Width="400px" >
                    </asp:DropDownList>
                </td>--%>
                <td class="tdp" nowrap="nowrap" style="width: 60%" valign="baseline">
                    <asp:UpdatePanel ID="updBot" runat="server">
                        <ContentTemplate>
                            <table align="left" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="xxx" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                                    OnClick="Button1_Click" Text="Pesquisar" />

                                                    <asp:Button ID="btnExportar" runat="server" CssClass="button" 
                                                    Font-Names="arial" Font-Size="7pt"
                                                    Text="Gerar Excel" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td width="25%" valign="top">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <br />

       <div id="dvSelecionarCliente" runat="server" 
            style="position:absolute; top:45%; left:35%; border:1px solid silver; font-size:14px; color:Blue; background-color:#DFDFDF; text-align:center; margin:10 10 10 10; font-weight: bold;" 
            visible="false">
            <span >É necessário escolher cliente(s) para gerar o Relatório. <br>Clique em OK para continuar. </span><br>
            <br>
            <input type="button" class="button" value="OK" onclick="javascript:window.location.href='default.aspx?opc=INTRANET&trocar=sim'"  style="font-size:14px"/>
       </div>

    </asp:Panel>
</asp:Content>
