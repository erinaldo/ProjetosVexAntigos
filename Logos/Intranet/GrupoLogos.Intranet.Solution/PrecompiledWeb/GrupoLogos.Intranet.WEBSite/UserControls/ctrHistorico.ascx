<%@ control language="C#" autoeventwireup="true" inherits="UserControls_ctrHistorico, App_Web_ctrhistorico.ascx.6bb32623" enabletheming="true" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<style type="text/css">
    .style1
    {
        font-family: Verdana;
        font-weight: bold;
        font-size: 7pt;
    }
</style>
<telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" 
    Skin="Telerik">
    <ItemStyle Font-Names="Verdana" Font-Size="7pt" />
<MasterTableView>
    <NoRecordsTemplate>
        <span class="style1">Nenhum Histórico Encontrado</span>
    </NoRecordsTemplate>
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
    <Columns>
        <telerik:GridBoundColumn DataField="DataDeCadastro" EmptyDataText="&amp;nbsp;" 
            HeaderText="Data" UniqueName="column1">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="NomeUsuario" EmptyDataText="&amp;nbsp;" 
            HeaderText="Usuário" UniqueName="column2">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Historico" EmptyDataText="&amp;nbsp;" 
            HeaderText="Histórico" UniqueName="column">
        </telerik:GridBoundColumn>
    </Columns>
</MasterTableView>

    <HeaderStyle Font-Names="Verdana" Font-Size="8pt" />
    <AlternatingItemStyle Font-Names="Verdana" Font-Size="7pt" />
    <ItemStyle Font-Names="Verdana" Font-Size="7pt" />

<FilterMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</FilterMenu>
</telerik:RadGrid>
