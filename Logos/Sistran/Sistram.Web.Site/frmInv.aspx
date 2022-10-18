<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="frmInv.aspx.cs" Inherits="frmPopInv" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        
        function Expandir(rua, andares) {
            for (i = 1; i <= andares; i++) {

                if (document.getElementById('trAndar' + rua + i).style.display == 'block') 
                {
                    document.getElementById('trAndar' + rua + i).style.display = 'none';
                    document.getElementById('tr' + rua + "1").style.display = 'none';
                    document.getElementById('tr' + rua + "3").style.display = 'none';
                    document.getElementById('tr' + rua + "4").style.display = 'none';                    

                }
                else 
                {
                    document.getElementById('trAndar' + rua + i).style.display = 'block';
                    document.getElementById('tr' + rua + "1").style.display = 'block';
                    document.getElementById('tr' + rua + "3").style.display = 'block';
                    document.getElementById('tr' + rua + "4").style.display = 'block';    
                }
            }
        }

        

        function ExpandirAll(ruaIni, ruaFin, andares) {
            //alert(ini);
            var i = 0;
            var m = "";
            for (i = ruaIni; i <= ruaFin; i++) {

                

                if (i.toString().length < 2) {
                    m = "0" + i.toString();
                }
                else {
                    m = i.toString();
                }

                //alert(andares);

                if (document.getElementById('tr' + m + '1').style.display == 'block') 
                {                    
                    document.getElementById('tr' + m + '1').style.display = 'none';                    
                    document.getElementById('tr' + m + '3').style.display = 'none';
                    document.getElementById('tr' + m + '4').style.display = 'none';
                    document.getElementById('dvExpandir').style.background = "url('Images/seta.jpg') no-repeat";

                    for (ia = 1; ia <= andares; ia++) {
                        document.getElementById('trAndar' + m + ia).style.display = 'none';
                       
                    }

                }
                else 
                {
                    //document.getElementById(m).style.display = 'block';
                    document.getElementById('tr' + m + '1').style.display = 'block';
                    //document.getElementById('tr' + m + '2').style.display = 'block';
                    document.getElementById('tr' + m + '3').style.display = 'block';
                    document.getElementById('tr' + m + '4').style.display = 'block';
                    document.getElementById('dvExpandir').style.background = "url('Images/setaPraCima.jpg') no-repeat";

                    for (ia = 1; ia <= andares; ia++) {
                        document.getElementById('trAndar' + m + ia).style.display = 'block';
                    }

                }
            }
        }

        var parselimit;
        function inicio() {
            clock1.innerHTML = "";
            var limit = "1:01"

            if (document.images) {
                parselimit = limit.split(":")
                parselimit = parselimit[0] * 60 + parselimit[1] * 1
            }

            begintimer();
        }

        function finalizar() {
            parselimit = 1;
        }

        function begintimer() {

            if (!document.images)
                return

            if (parselimit == 1) {
                //document.forms[0].submit();
                //window.location = "site de destino apos o tempo"
            } else {
                parselimit -= 1
                curmin = Math.floor(parselimit / 60)
                cursec = parselimit % 60
                if (curmin != 0) {
                    curtime = curmin + " minutos e " + cursec + " segundos para você sair dessa página"
                }
                else {
                    curtime = cursec + " Segundos regredindo....."
                    //window.status = curtime
                    clock1.innerHTML = cursec
                    setTimeout("begintimer()", 1000)
                }
            }
        }
    
    </script>

    <left>

    <asp:Panel ID="pnlteste" runat="server" Visible="true" HorizontalAlign="Center" >
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px; text-align: left;" nowrap="nowrap" width="1%">
                    <asp:Label ID="lblTitulo" runat="server" Text="Acompanhamento Inventário" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px; text-align: left;">
                    <asp:UpdatePanel ID="uplCheck" runat="server" ChildrenAsTriggers="False" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                 style="font-weight: 700" 
                                Text="Atualizção Automática" oncheckedchanged="CheckBox1_CheckedChanged" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        
        <asp:UpdatePanel ID="UpF" runat="server"  >
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <div style='width:100%' >
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </div>
                                <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" >
                            </asp:Timer>
                                <br />
                                
                                
                                <table style="border: 0.5pt solid #808080; " cellpadding="1" 
                                    cellspacing="1" border="1" width="99%">
                                    <tr>
                                        <td style="text-align:LEFT; font-weight: bold; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px; font-size: 12px; font-family: verdana;" 
                                            valign="middle" width="20%" nowrap=nowrap>
                                            RESUMO</td>
                                        <td style="text-align:LEFT; font-weight: bold; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px; font-size: 12px; font-family: verdana;" 
                                            valign="middle" width="85%">
                                            GRÁFICO</td>
                                        <td style="text-align: LEFT; font-weight: bold; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px; font-size: 12px; font-family: verdana;" 
                                            valign="middle" width="1%">
                                            <b>LEGENDA</b></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" valign="top" width="1%">
                                            <table ID="tblResumo" runat="server" class="table2" style="width: 99%" 
                                                visible="false">
                                                <tr>
                                                    <td class="tdp" nowrap="nowrap" style="font-weight: 700" width="1%">
                                                        Total de Posições:</td>
                                                    <td class="tdpR" style="font-weight: 700; text-align: right" width="99%">
                                                        <asp:Label ID="lblTotalEnderecos" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdp" nowrap="nowrap" style="font-weight: 700">
                                                        Posições Contadas:</td>
                                                    <td class="tdpR" style="font-weight: 700; text-align: right">
                                                        <asp:Label ID="lblPosicoesContadas" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdp" style="font-weight: 700">
                                                        % Inventário:</td>
                                                    <td class="tdpR" style="font-weight: 700; text-align: right">
                                                        <asp:Label ID="lblPercInv" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                              <br />
                                            <hr /><br />
                                            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                         <br />
                                            <hr /><br />
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>

                                            <asp:ListBox ID="ListBox1" runat="server" Visible="False"></asp:ListBox>
                                            <br />
                                            

                                        </td>
                                        <td style="text-align: left" valign="top">
                                            <asp:Panel ID="Panel9" runat="server">
                                            </asp:Panel>
                                            <br />
                                            <br />
                                            
                                            </td>
                                        <td style="text-align: left" valign="top">
                                            <table border="0.5pt" cellpadding="1" cellspacing="1" class="table" 
                                                style="width: 1%">
                                                <tr>
                                                    <td class="tdp" width="1%">
                                                        Contados:
                                                    </td>
                                                    <td class="tdpR" width="1%">
                                                        <table width="15">
                                                            <tr>
                                                                <td class="contados" height="10px" width="1%">
                                                                    1</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdp" width="1%">
                                                        Pendentes:</td>
                                                    <td class="tdpR" width="1%">
                                                        <table width="15">
                                                            <tr>
                                                                <td class="naocontados" height="10px" width="15">
                                                                    0</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdp" width="1%">
                                                        Inativos:</td>
                                                    <td class="tdpR" width="1%">
                                                        <table width="15">
                                                            <tr>
                                                                <td align="center" bgcolor="White" height="11px" style="text-align: center" 
                                                                    width="1%">
                                                                    <asp:Image ID="Image5" runat="server" Height="10px" 
                                                                        ImageUrl="~/Images/proibidoX.bmp" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                           
                        </td>
                    </tr>
                </table>                
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>   
    </left>
</asp:Content>
