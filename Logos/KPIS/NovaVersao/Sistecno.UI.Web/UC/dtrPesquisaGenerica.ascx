<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dtrPesquisaGenerica.ascx.cs" Inherits="Sistecno.UI.Web.UC.dtrPesquisaGenerica" %>
<script type="text/javascript">
    function setarId(valor) {
        document.getElementById('<%=lblId.ClientID %>').value = valor;
    }
</script>

<asp:HiddenField ID="lblId" runat="server"></asp:HiddenField>

<div class="form-group">
    <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlPesquisa1" runat="server" Style="border: 1px solid red;">
       <%-- overflow: overlay;--%>
               <%-- <asp:PlaceHolder ID="phPesquisa" runat="server"></asp:PlaceHolder>--%>

        <br />
    </asp:Panel>

    <div style="border: none; padding: 2px; width: 100%">
        <asp:PlaceHolder ID="htm" runat="server"></asp:PlaceHolder>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

    <br />
</div>
