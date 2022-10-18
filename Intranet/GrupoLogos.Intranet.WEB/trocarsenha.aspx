<%@ Page Title="" Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true" CodeFile="trocarsenha.aspx.cs" Inherits="trocarsenha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:Panel ID="pnlTrocarSenha" runat="server" HorizontalAlign="Left">

<div style="position:absolute; top:35%; left:32%; width:400px">
    <table class="table" cellpadding="2" cellspacing="2" >
        <tr style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px;">
            <td style="font-size:12pt" colspan="2">
                Trocar Senha
            </td>    
        </tr>

        <tr>
            <td class="tdp" style="width:1%" >Usuário:</td>
            <td class="tdp">
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="txt" ReadOnly="True" 
                    Width="99%"></asp:TextBox>
            </td>
        </tr>

         <tr>
            <td class="tdp">Senha Atual:</td>
            <td class="tdp">
                <asp:TextBox ID="txtSenhaAtual" runat="server" CssClass="txt" 
                    Width="99%"></asp:TextBox>
             </td>
        </tr>

         <tr>
            <td class="tdp" nowrap=nowrap>Nova Senha:</td>
            <td class="tdp">
                <asp:TextBox ID="txtNovasenha" runat="server" CssClass="txt"  
                    Width="99%"></asp:TextBox>
             </td>
        </tr>

        <tr>
            <td class="tdp" nowrap=nowrap>Confirme a Senha:</td>
            <td class="tdp">
                <asp:TextBox ID="txtConfirmaNovaSenha" runat="server" CssClass="txt" 
                     Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdp">
                &nbsp;</td>
            <td class="tdpR">
                <asp:Button ID="btnConfirmar" runat="server" CssClass="button" Font-Size="10pt" 
                    Text="Confirmar" onclick="btnConfirmar_Click" />
            </td>
        </tr>
    </table>
    
</div>


</asp:Panel>
</asp:Content>

