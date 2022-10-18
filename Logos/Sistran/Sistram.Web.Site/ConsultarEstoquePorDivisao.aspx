<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConsultarEstoquePorDivisao.aspx.cs"
    Inherits="ConsultarEstoquePorDivisao" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" >
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Estoque Por Divisão" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    
    
    <asp:UpdatePanel ID="xxwewx" runat="server">
        <ContentTemplate>
        <table width="100%">
        <tr valign="top">
        <td style="width:5%">
              <asp:Panel ID="pnlMenu" runat="server" 
                  ScrollBars="Auto">
                        
                                <asp:PlaceHolder ID="PlaceHolderMenuDivisao" runat="server"></asp:PlaceHolder>
                        
                    </asp:Panel>
        </td>
        <td valign="top">
        
               
        <asp:Panel id="PnlMensagem" runat="server" HorizontalAlign="Center"  Visible="false"
                BackColor="#EBEBEB" Width="100%" >
                <asp:RoundedCornersExtender Corners="All" Radius="6" TargetControlID="pnlMensagem"
          ID="RoundedCornersExtender1" runat="server"></asp:RoundedCornersExtender>
            <br />
        <asp:Label ID="lblMensagem" runat="server" CssClass="txt" BorderStyle="None" 
                Font-Bold="True" Font-Names="Verdana"></asp:Label>
        </asp:Panel>            
        
        <asp:Panel ID="pn" runat="server" DefaultButton="Button1">
        
      <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="1%" nowrap="nowrap">
                    Código:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="txt" 
                        Width="100px"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Código Cliente:
                </td>
                <td class="tdp" nowrap="nowrap" width="60%">
                    <asp:TextBox ID="txtCodigo0" runat="server" CssClass="txt" 
                        Width="100px"></asp:TextBox>
                    &nbsp;<asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" OnClick="Button1_Click" Text="Pesquisar" />
                </td>
                <td class="tdpR" nowrap="nowrap" width="1%">
                    Exibir:&nbsp;
                    <asp:DropDownList ID="cboTipoDes0" runat="server" CssClass="cbo" 
                        Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        onselectedindexchanged="cboTipoDes0_SelectedIndexChanged" Width="35px">
                        <asp:ListItem Enabled="False" Selected="True">100</asp:ListItem>
                        <asp:ListItem Selected="True">200</asp:ListItem>
                        <asp:ListItem>300</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        </asp:Panel>
        
            <br />
        
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
                GridLines="None" AllowSorting="True" AutoGenerateColumns="False" 
                Width="100%" onpageindexchanged="RadGrid1_PageIndexChanged" 
                onselectedindexchanged="RadGrid1_SelectedIndexChanged" 
                onsortcommand="RadGrid1_SortCommand" PageSize="20" 
                onpagesizechanged="RadGrid1_PageSizeChanged">
                <MasterTableView>
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn DataField="CODIGO" HeaderText="Código" 
                            UniqueName="column1">
                            <EditItemTemplate>
                                <asp:TextBox ID="CODIGOTextBox" runat="server" Text='<%# Bind("CODIGO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server"  ToolTip="Clique aqui para ver o detalhe." Target="_blank" Text='<%# Bind("CODIGO") %>' NavigateUrl='<% # "frmDetalheProduto.aspx?click="+ Eval("IDCLIENTEDIVISAO") +"&Codigo=" + Eval("CODIGO") + "&IdProduto=" + Eval("IDPRODUTO")  %>' ></asp:HyperLink>
                                
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                     
                        
                        
                        <telerik:GridBoundColumn DataField="CODIGO" EmptyDataText="&amp;nbsp;" 
                            HeaderText="Código do Cliente" UniqueName="column">
                        </telerik:GridBoundColumn>
                        
                     
                        
                        
                        <telerik:GridBoundColumn DataField="descricao" EmptyDataText="&amp;nbsp;" 
                            HeaderText="Descrição" UniqueName="column3">
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Divisao" EmptyDataText="&amp;nbsp;" 
                            HeaderText="Divisão" UniqueName="column10">
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="SALDODIVISAODISPONIVEL" 
                            EmptyDataText="&amp;nbsp;" HeaderText="Saldo" UniqueName="column2">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu EnableTheming="True">
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
            </telerik:RadGrid>
            </td>
        </tr>
        </table>
        
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>
