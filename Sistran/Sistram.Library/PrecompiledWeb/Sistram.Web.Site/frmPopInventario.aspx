<%@ page language="C#" masterpagefile="~/SiteDetalheFull.master" autoeventwireup="true" inherits="frmPopInventario, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        function Expandir(rua) {

            //alert(rua);
            if (document.getElementById(rua).style.display == 'block') {
                document.getElementById(rua).style.display = 'none';
                document.getElementById('tr' + rua + '1').style.display = 'none';
                document.getElementById('tr' + rua + '3').style.display = 'none';
            }
            else {
                document.getElementById(rua).style.display = 'block';
                document.getElementById('tr' + rua + '1').style.display = 'block';
                document.getElementById('tr' + rua + '3').style.display = 'block';
            }
        }

        function Check(control) {
            if (control.checked == true) {
                setTimeout('location.reload();', 5000);
                control.checked = true;
            }
            else {
                setTimeout('location.reload();', 5000000000);                 
            }
            alert(control.checked);
        }


        function ExpandirAll(ruaIni, ruaFin) 
        {
            //alert(ini);
            var i = 0;
            var m = "";
            for (i = ruaIni; i <= ruaFin; i++) {

                //alert(i);

                if (i.toString().length < 2) {
                    m = "0" + i.toString();
                }
                else {
                    m = i.toString();
                }
                
                if (document.getElementById(m).style.display == 'block') {
                    document.getElementById(m).style.display = 'none';
                    document.getElementById('tr' + m + '1').style.display = 'none';
                    document.getElementById('tr' + m + '3').style.display = 'none';
                    document.getElementById('dvExpandir').style.background = "url('Images/seta.jpg') no-repeat";
                }
                else {
                    document.getElementById(m).style.display = 'block';
                    document.getElementById('tr' + m + '1').style.display = 'block';
                    document.getElementById('tr' + m + '3').style.display = 'block';
                    document.getElementById('dvExpandir').style.background = "url('Images/setaPraCima.jpg') no-repeat";
                    
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
                                oncheckedchanged="CheckBox1_CheckedChanged" style="font-weight: 700" 
                                Text="Atualizção Automática" />
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
                                <asp:Timer ID="Timer1" runat="server" Interval="50000" OnTick="Timer1_Tick">
                            </asp:Timer>
                                <br />
                                
                                
                                <table style="border: 0.5pt solid #808080; " cellpadding="1" 
                                    cellspacing="1" border="1" width="800px">
                                    <tr>
                                        <td style="text-align:LEFT; font-weight: bold; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px; font-size: 12px; font-family: verdana;" 
                                            valign="middle" width="410" nowrap=nowrap>
                                            RESUMO</td>
                                        <td style="text-align:LEFT; font-weight: bold; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px; font-size: 12px; font-family: verdana;" 
                                            valign="middle" width="410">
                                            GRÁFICO</td>
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
                                                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
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

                                        </td>
                                        <td style="text-align: left" valign="top">
                                            <asp:PlaceHolder ID="ph3" runat="server"></asp:PlaceHolder>
                                            <br />
                                            <hr /><br />
                                            <table border="0.5pt" cellpadding="1" cellspacing="1" class="table" 
                                                style="width: 1%">
                                                <tr>
                                                    <td class="tdp" colspan="2" 
                                                        
                                                        style="text-align: center; font-weight: bold; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px; font-size: 12px; font-family: verdana;">
                                                        <b>LEGENDA</b></td>
                                                </tr>
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
    <left>
</asp:Content>
