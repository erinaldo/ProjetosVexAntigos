<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmaprovarCotacao.aspx.cs"
    Inherits="frmaprovarCotacao" MasterPageFile="SiteDetalhe2.master" EnableTheming="true" %>

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
        
        function confirmation() {
            var answer = confirm("Tem Certeza que Deseja Aprovar?")

            if (!answer) {
                return false;
            }
            else
                return true;
            
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
                        <td style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium;">
                            <strong>COTAÇÃO(ÕES) PENDENTE(S) DE APROVAÇÃO(ÇÕES)</strong></td>
                    </tr>
                    <tr>
                        <td>
                           <hr /></td>
                    </tr>
                </table>
                <br />
                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                <br />
                <table class="grid">
                    <tr>
                        <td class="tdpR">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                            <asp:Button ID="btnsAIR" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="verdana" Font-Size="10pt" ForeColor="Black" Height="35px"
                                Text="SAIR" Width="150px" CausesValidation="False" />
                            &nbsp;<asp:Label ID="lblTotal" runat="server" Visible="False"></asp:Label>
                            <asp:Button ID="btnConfirmar" runat="server" BackColor="White" BorderStyle="Solid"
                                BorderWidth="1px" Font-Bold="True" Font-Names="verdana" Font-Size="10pt" Height="35px"
                                Text="APROVAR" Width="150px" OnClick="btnConfirmar_Click" />
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
