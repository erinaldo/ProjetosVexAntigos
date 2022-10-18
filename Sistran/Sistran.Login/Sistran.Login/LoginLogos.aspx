<%@ Page Language="C#" MasterPageFile="~/Bkank.master" AutoEventWireup="true" CodeFile="LoginLogos.aspx.cs"
    Inherits="LoginLogos" Title="" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" SelectedIndex="0" Selected="True"
                Width="100%"  Skin="Outlook" BackColor="#CCCCCC">
                <Tabs>
                    <telerik:RadTab runat="server" PageViewID="rpvPromocional" Text="Promocional" >
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" PageViewID="rpvTransporte" Text="Transporte" >
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="rmpX" runat="server" SelectedIndex="4" BackColor="White"  Width="100%">
                <telerik:RadPageView ID="rpvPromoX" runat="server" Width="100%">
                    <iframe allowtransparency="true" src="http://192.168.10.8/sistranweb/index.php?cf=sistecno2&lg="
                        frameborder="0" scrolling="no" marginheight="0" marginwidth="0" style="width: 100%;
                        height: 110px;"></iframe>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvTrans" runat="server" Width="100%">
                    <iframe allowtransparency="true" src="http://www.sistecno.com.br/novodotnet/web/frmLogin.aspx"
                        frameborder="0" scrolling="no" marginheight="0" marginwidth="0" style="width: 100%;
                        height: 110px;"></iframe>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
