<%@ page language="C#" autoeventwireup="true" inherits="CotacaoFornecedor, App_Web_amyvqcgn" masterpagefile="SiteDetalhe2.master" enabletheming="true" %>

  <%--  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function LimpaValoresZerados(e) 
        {


            if (e.id.toString().indexOf("IPI") > 0 || e.id.toString().indexOf("ICMS")>0) {
                if (e.value == "0,00")
                    e.value = "";
            }
            else {
                if (e.value == "0,0000")
                    e.value = "";
            }
                
        }

        function ColocaValoresZerados(e) {

            if (e.id.toString().indexOf("IPI") > 0 || e.id.toString().indexOf("ICMS") > 0) {
                if (e.value == "")
                    e.value = "0,00";
            }
            else {
                if (e.value == "")
                    e.value = "0,0000";
            }
        }

               
        function SomenteNumeroDecimal(e) {
            var tecla = (window.event) ? event.keyCode : e.which;

            if ((tecla > 47 && tecla < 58) || tecla == 44)
                return true;
            else {
                if (tecla != 8) return false;
                else return true;
            }
        }


        function SomenteNumero(e) {
            var tecla = (window.event) ? event.keyCode : e.which;

            if ((tecla > 47 && tecla < 58))
                return true;
            else {
                if (tecla != 8) return false;
                else return true;
            }
        }

        function confirmarFechamnento() {
            var answer = confirm("Tem certeza que deseja fechar a cotação?")
            if (answer) {
                window.close();
            }
        }

    </script>
    
            <asp:Panel ID="Panel1" runat="server">
                <table class="grid">
                    <tr>
                        <td width="50%">
                            &nbsp;
                        </td>
                        <td class="tdpR">
                            <asp:Button ID="btnSairDv0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="verdana" Font-Size="7pt" ForeColor="Black" OnClientClick="javascript:window.open('frmRptCotacao.aspx');return false;"
                                Text="Gerar PDF" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%" class="table">
                    <tr>
                        <td class="tdp" nowrap="nowrap" width="1%">
                            Previsão de Entrega:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtI" ErrorMessage="Informe a Previsão de Entrega" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" width="1%" nowrap="nowrap">
                            <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                          <%--  <asp:CalendarExtender ID="CalendarExtender54" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtI" />--%>
                           <%-- <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                                UserDateFormat="DayMonthYear">
                            </asp:MaskedEditExtender>--%>
                        </td>
                        <td class="tdp" width="1%">
                            Responsável:
                        </td>
                        <td class="tdp" width="90%">
                            <asp:TextBox ID="txtResponsavel" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtResponsavel"
                                ErrorMessage="Informe o Responsável" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdp" nowrap="nowrap" style="vertical-align: top">
                            Parcelas:
                        </td>
                        <td class="tdp" width="1%" style="vertical-align: top">
                            <asp:DropDownList ID="cboParcelas" runat="server" AutoPostBack="True" CssClass="cbo"
                                Font-Names="Arial" Font-Size="7pt" Height="17px" OnSelectedIndexChanged="cboParcelas_SelectedIndexChanged"
                                Width="102px">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdp" width="1%">
                            &nbsp;
                        </td>
                        <td class="tdp" width="1%">
                            <asp:PlaceHolder ID="phParcelas" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                <br />
                <table class="grid">
                    <tr>
                        <td class="tdpR">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                            <asp:Button ID="Button1" runat="server" BackColor="White" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="10pt" Height="35px" onclick="Button1_Click" 
                                Text="CALCULAR COTAÇÃO" />
                            <asp:Button ID="btnsAIR" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="verdana" Font-Size="10pt" ForeColor="Black" Height="35px"
                                Text="SAIR" Width="150px" CausesValidation="False" />
                            &nbsp;<asp:Label ID="lblTotal" runat="server" Visible="False"></asp:Label>
                            <asp:Button ID="btnConfirmar" runat="server" BackColor="White" BorderStyle="Solid"
                                BorderWidth="1px" Font-Bold="True" Font-Names="verdana" Font-Size="10pt" Height="35px"
                                Text="CONFIRMAR" Width="150px" OnClick="btnConfirmar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div id="dvErro" runat="server" style="position: absolute; top: 50%; left: 35%; width: 400px;
                border: 1px solid black; text-align: center; background-color: White" visible="false">
                <table width="99%">
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblMensagem" runat="server" Text="dfsdf" Style="font-weight: 700;
                                color: #0000FF; font-size: 8pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%">
                            <asp:Button ID="btnSairDv" runat="server" Text="OK" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="verdana" Font-Size="12pt" ForeColor="Black" Height="40px"
                                Width="200px" OnClientClick="javascript:window.close();" Visible="false" CausesValidation="False" />
                            <asp:Button ID="btnVoltarDv" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="verdana" Font-Size="10pt" ForeColor="Red" Height="35px"
                                OnClick="btnVoltarDv_Click" Text="Voltar e Corrigir" Visible="false" Width="200px"
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </div>        
</asp:Content>
