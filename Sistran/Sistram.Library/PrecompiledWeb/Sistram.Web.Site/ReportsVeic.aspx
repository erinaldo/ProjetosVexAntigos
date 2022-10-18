<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="ReportsVeic, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Relatórios" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="uplm" runat="server">
            <ContentTemplate>
                <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
                    <tr valign="bottom">
                        <td class="tdp" width="10%" nowrap="nowrap">
                            Tipo:
                        </td>
                        <td class="tdp" width="25%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr valign="baseline">
                        <td class="tdp" valign="middle">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                <asp:ListItem>Veículos</asp:ListItem>
                                <asp:ListItem>Motoristas</asp:ListItem>
                                <asp:ListItem>Proprietários</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="tdp" valign="top">
                            
                                <asp:CheckBoxList ID="rbMotorista" runat="server" 
                                    RepeatDirection="Horizontal" Visible="False">
                                    <asp:ListItem Selected="True">Ativo</asp:ListItem>
                                    <asp:ListItem Selected="True">Inativo</asp:ListItem>
                                    <asp:ListItem Selected="True">Liberado</asp:ListItem>
                                    <asp:ListItem Selected="True">Não Liberado</asp:ListItem>
                                    <asp:ListItem Selected="True">Habilitação Vencida</asp:ListItem>
                                </asp:CheckBoxList>
                            
                                <asp:RadioButtonList ID="rblVeiculo" runat="server" 
                                    RepeatDirection="Horizontal" Visible="false" 
                                    onselectedindexchanged="rblVeiculo_SelectedIndexChanged">
                                    <asp:ListItem>Antt Vencido</asp:ListItem>
                                    <asp:ListItem>Licenciamento Vencido</asp:ListItem>
                                </asp:RadioButtonList>
                            
                        </td>
                    </tr>
                    <tr valign="baseline">
                        <td class="tdp" valign="middle">
                            &nbsp;
                        </td>
                        <td class="tdpR" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers >
                            <asp:PostBackTrigger ControlID="Button2" />
                            </Triggers>
                                <ContentTemplate>
                                    <asp:Button ID="Button2" runat="server" CssClass="button" 
                                        OnClick="Button2_Click" Text="Gerar Relatório" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr valign="baseline">
                        <td class="tdp" colspan="2" valign="middle" bgcolor="White">
                            <asp:Panel ID="PnlReport" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                BorderWidth="1px" Height="600px">
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
