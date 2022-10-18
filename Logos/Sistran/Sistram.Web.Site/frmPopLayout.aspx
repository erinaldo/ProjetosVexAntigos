<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="frmPopLayout.aspx.cs" Inherits="frmPopLayout" EnableTheming="true"
    Title="Deposito" %>

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


        function ExpandirAll(ruaIni, ruaFin) {
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

        function openModalWindow(strURL, strArgument, intWidth, intHeight) {
            var intTop = ((screen.height - intHeight) / 2);
            var intLeft = ((screen.width - intWidth) / 2);
            var strEnderec = strURL;
            var strAjustesIE = 'status=0; help=0; center:yes; dialogWidth:' + intWidth + 'px; dialogHeight:' + intHeight + 'px';
            var strAjustesNS = 'width=' + intWidth + ', height=' + intHeight + ', status=0, scrollbars=1, menubar=0, dependent=1, left=' + intLeft + ', top=' + intTop;

//            with (window.navigator) {
//                switch (appName) {

//                    case 'Microsoft Internet Explorer':
//                        var x = window.showModalDialog(strEnderec, strArgument, strAjustesIE);
//                        break;

//                    case 'Netscape':
//                        var x = window.open(strEnderec, 'Default', strAjustesNS);
//                        break;
//                }
            }
        }

    
    </script>

    <asp:Panel ID="pnlteste" runat="server" Visible="true" HorizontalAlign="Center">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px;
                    text-align: left;" nowrap="nowrap" width="1%">
                    <asp:Label ID="lblTitulo" runat="server" Text="Acompanhamento Inventário" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px;
                    text-align: left;">
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpF" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left" class="tdp">
                            <table border="0.5pt" cellpadding="1" cellspacing="1" class="table" style="width: 1%">
                                <tr>
                                    <td class="tdp" colspan="6" style="text-align: center; font-weight: bold; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                                        height: 25px; font-size: 12px; font-family: verdana;">
                                        <b>LEGENDA</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" width="1%" nowrap="nowrap">
                                        Ativo Vazio:
                                    </td>
                                    <td class="tdp" width="1%">
                                        <table>
                                            <tr>
                                                <td class="contadosContagem" >
                                                    <asp:Image ID="Image1" runat="server"  Width="17" Height="17"
                                                        ImageUrl="~/Images/ativovazio.gif" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="tdp" nowrap="nowrap" width="1%">
                                        Ativo Com Produto:</td>
                                    <td class="tdp" nowrap="nowrap" width="1%">
                                        <table width="15">
                                            <tr>
                                                <td class="contadosContagem"  width="1%">
                                                    <asp:Image ID="Image6" runat="server"  Width="20" Height="20"
                                                        ImageUrl="~/Images/pallet01.jpg" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="tdpR" width="1%">
                                        Inativos:
                                    </td>
                                    <td class="tdp" width="1%">
                                        <table >
                                            <tr>
                                                <td align="center" bgcolor="White" >
                                                    <asp:Image ID="Image5" runat="server" Height="20" Width="20" 
                                                        ImageUrl="~/Images/Bloqueado.jpg" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <div style="width: 100%">
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="dvDetalhe" style="position:absolute; top:35%;top:15%" ></div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
