<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmAcompanhamento, App_Web_frmacompanhamento.aspx.cdcab7d2" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >
   
    <asp:Timer ID="Timer1" runat="server" Interval="50000" ontick="Timer1_Tick">
    </asp:Timer>
    
    <asp:Panel ID="pnlteste" runat="server">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
       <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" />
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
            </Triggers>
        <ContentTemplate>
        
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    
            <table class="table" cellpadding="1" cellspacing="0" >
            <tr valign="top">
                <td class="tdp" rowspan="2" style="width:1%" >Filial:</td>
                <td class="tdp" rowspan="2" valign="top" style="width:1%">
                    <asp:DropDownList ID="cboFilial" runat="server" CssClass="cbo" 
                        Font-Names="Arial" Font-Size="7pt" Width="150px">
                    </asp:DropDownList>
                    <asp:ListBox ID="lstFilial" runat="server" CssClass="txt" 
                        Rows="2" Width="150
                        " Height="40px"></asp:ListBox>
                </td>
                <td class="tdp" style="width:1%">
                    <asp:Button ID="btnAdicionarFilial" runat="server" BackColor="#990000" 
                        BorderStyle="None" Font-Bold="True" Font-Names="arial" 
                        Height="17px" onclick="btnAdicionarFilial_Click" Text="+" Width="20px" 
                        CssClass="button" />
                </td>
                <td class="tdp" style="width:1%">
                    <asp:Button ID="btnRemoverFilial" runat="server" BackColor="#990000" 
                        BorderStyle="None" Font-Bold="True" Font-Names="arial" 
                        Height="17px" onclick="btnRemoverFilial_Click" Text="-" Width="20px" 
                        CssClass="button" />
                </td>
                <td class="tdp" style="width:1%" >
                    <table cellpadding="1" cellspacing="0" class="table" style="width: 100%">
                        <tr>
                            <td class="tdp">
                                Saída:</td>
                            <td class="tdp">
                                <asp:TextBox ID="txtData" runat="server" CssClass="txt" Width="50px" ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                    TargetControlID="txtData" />
                                <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtData" 
                                    UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender>
                            </td>
                            <td class="tdp">
                                <asp:Button ID="btnPesquisar" runat="server" BackColor="#990000" 
                                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="7pt" Height="17px" onclick="btnPesquisar_Click" Text="Pesquisar" 
                                    Width="55px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdp">
                                Ordenar:</td>
                            <td class="tdp" colspan="2">
                                <b><span style="font-size: 7pt"><span style="font-size: 8pt">
                                <asp:DropDownList ID="cboOrdem" runat="server" CssClass="cbo" 
                                    Font-Names="Arial" Font-Size="7pt" Width="100%">
                                    <asp:ListItem Text="Numero DT" Value="Order By Dt.Numero"></asp:ListItem>
                                    <asp:ListItem Text="Placa" Value="Order By Vei.Placa"></asp:ListItem>
                                    <asp:ListItem Text="Serviços" Value="Order By TotalDeServicos Desc"></asp:ListItem>
                                    <asp:ListItem Text="Notas Fiscais" Value="Order By Documentos "></asp:ListItem>
                                    <asp:ListItem Text="Ocorrências" Value="Order By Ocorrencias Desc"></asp:ListItem>
                                    <asp:ListItem Text="Realizadas" Value="Order By DocumentosConcluido Desc"></asp:ListItem>
                                    <asp:ListItem Text="Não Realizadas" 
                                        Value="Order By DocumentosNaoFinalizado Desc"></asp:ListItem>
                                    <asp:ListItem Text="Pendentes" Value="Order By Pendentes Desc"></asp:ListItem>
                                </asp:DropDownList>
                                </span></span></b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdp" bgcolor="White" style="width:70%">
                <table id="tbltot" runat="server" border="0" class="table2" cellpadding="1" cellspacing="0" width="100%">
                <tr>
                    <td class="tdpRVerdana">Veículos:</td>
                    <td class="tdpRVerdana"><asp:Label ID="lblVeiculos" runat="server" style="font-size: 8pt"></asp:Label></td>
                    <td class="tdpRVerdana">Notas Fiscais:</td>
                    <td class="tdpRVerdana"><span style="font-size: 8pt">
                        <asp:Label ID="lblDoc" runat="server" style="font-size: 8pt"></asp:Label>
                        </span></td>
                    <td class="tdpRVerdana">Realizadas:</td>
                    <td class="tdpRVerdana">
                        <asp:Label ID="lblRealiz" runat="server" style="font-size: 8pt"></asp:Label>
                    </td>
                    <td class="tdpRVerdana">
                        <asp:Label ID="lblRealiz1" runat="server"></asp:Label>
                    </td>
                    <td class="tdpRVerdana">Pendentes:</td>
                    <td class="tdpRVerdana">
                        <asp:Label ID="lblPend" runat="server"></asp:Label>
                    </td>
                    <td class="tdpRVerdana">
                        <asp:Label ID="lblPend1" runat="server"></asp:Label>
                    </td>
                </tr>      
                
                
                    <tr>
                        <td class="tdpRVerdana">
                            Serviços:</td>
                        <td class="tdpRVerdana">
                            <asp:Label ID="lblServ" runat="server"></asp:Label>
                        </td>
                        <td class="tdpRVerdana">
                            &nbsp;</td>
                        <td class="tdpRVerdana">
                            &nbsp;</td>
                        <td class="tdpRVerdana">
                            Retorno:</td>
                        <td class="tdpRVerdana">
                            <asp:Label ID="lblNRealiz" runat="server"></asp:Label>
                        </td>
                        <td class="tdpRVerdana">
                            <asp:Label ID="lblNRealiz1" runat="server"></asp:Label>
                        </td>
                        <td class="tdpRVerdana">
                            Ocorrências:</td>
                        <td class="tdpRVerdana">
                            <span style="font-size: 7pt">
                            <asp:Label ID="lblOcorrencia" runat="server" style="font-size: 8pt"></asp:Label>
                            </span>
                        </td>
                        <td class="tdpRVerdana">
                            <span style="font-size: 7pt">
                            <asp:Label ID="lblOcorrencia1" runat="server" style="font-size: 8pt"></asp:Label>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdpRVerdana" colspan="7">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="True" 
                                Font-Names="Arial" Font-Size="6pt" oncheckedchanged="CheckBox1_CheckedChanged" 
                                style="font-size: 7pt" Text="Atualização Automática" />
                        </td>
                        <td class="tdpRVerdana" colspan="3">
                            <asp:Label ID="Label1" runat="server" style="font-size: 7pt"></asp:Label>
                        </td>
                    </tr>
                
                
                </table>
                
                        <%--<table id="tbltot" runat="server" class="table2" cellpadding="1" cellspacing="0"  visible="False" width="100%" >
                        <tr>
                        
                        <td class="tdp" >Veículos:</td>
                        <td class="tdpR" align="right" >
                        
                        </td>
                            <td class="tdp" nowrap="nowrap" >Notas Fiscais:</td>
                            <td class="tdpR" 
                                align="right" >
                                &nbsp;</td>
                            <td class="tdp" >
                                Realizadas:</td>
                            <td class="tdpR" align="right"  >                      
                                &nbsp;</td>
                            <td class="tdpR" align="right" >
                                
                                &nbsp;</td>
                            <td class="tdp" >
                                Pendentes:</td>
                            <td class="tdpR" align="right" >
                                &nbsp;</td>
                            <td class="tdpR" align="right">
                                &nbsp;</td>
                            </tr>
                        <tr>
                        <td class="tdp" >Serviços:</td>
                        <td class="tdpR" align="right"    >
                            &nbsp;</td><td class="tdp" ></td><td class="tdp"></td>
                            <td class="tdp" >
                                Retornos:</td>
                            <td class="tdpR" align="right" >
                                &nbsp;</td>
                            <td class="tdpR" align="right">
                                &nbsp;</td>
                            <td class="tdp">
                                Ocorrências:</td>
                            <td class="tdpR" align="right">
                                &nbsp;</td>
                            <td class="tdpR" align="right">
                                &nbsp;</td>
                            </tr>
                        
                            <tr>
                                <td class="tdp" colspan="4">
                                    &nbsp;</td>
                                <td class="tdp" colspan="6" align="center">
                                    &nbsp;</td>
                            </tr>
                        
                        </table>--%>
                </td>
            </tr>
            
            </table>
    
          
            <br />
    
          
          <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
