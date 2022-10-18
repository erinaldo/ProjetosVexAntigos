<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <br /><br /><br /><br />
    Fazendo um teste no meu site
    <br /><br /><br />
    com PopUp<br />
     
 
    <asp:Label ID="lblTeste" runat="server"></asp:Label>
 
    <asp:Panel CssClass="modalPopUp" ID="pnlPopUp" Style="display:none;"
 runat="server">
        <div>
            <asp:Button ID="cmdFechar" Text="X" runat="server" />
            <br /><br /><br /><h2>Novo popup</h2><br /><br /><br />
        </div>
    </asp:Panel>
 
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        BackgroundCssClass="modalBackground"
        CancelControlID="cmdFechar" DropShadow="true"
        PopupControlID="pnlPopUp" PopupDragHandleControlID="panel3"
        TargetControlID="lblTeste">
    </asp:ModalPopupExtender>




</asp:Content>
