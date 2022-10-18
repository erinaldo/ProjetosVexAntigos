<%@ control language="VB" autoeventwireup="false" inherits="dadosBoleto, App_Web_dadosboleto.ascx.6bb32623" %>
<table bgcolor="lemonchiffon" border="1" style="width: 600px; height: 174px">
    <tr>
        <td colspan="2" style="height: 21px">
            <strong><span style="font-size: 14pt">Dados do Documento</span></strong></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Sequencial</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtSequencial" runat="server">5021034</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Data emissão</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtDataEmissao" runat="server">01/10/2007</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Data&nbsp; vencimento</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtDataVencimento" runat="server">15/10/2007</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Data documento</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtDataDocumento" runat="server">01/10/2007</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Data &nbsp;processamento</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtDataProcessamento" runat="server">15/10/2007</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Número</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtNumeroDocumento" runat="server">12345678</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px; height: 23px">
            <span style="font-size: 10pt">Valor</span></td>
        <td style="width: 97px; height: 23px">
            <asp:TextBox ID="txtValor" runat="server">1,00</asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
            <strong><span style="font-size: 14pt">Dados do Cedente</span></strong></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">
            Aceite</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtAceite" runat="server" Width="76px">Sim</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px; height: 23px;">
            <span style="font-size: 10pt">
            Carteira</span></td>
        <td style="width: 97px; height: 23px;">
            <asp:TextBox ID="txtCarteira" runat="server">14</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Documento</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtNumeroContrato" runat="server">09/200701</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Nome&nbsp;</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtNomeCedente" runat="server">Jos&#233; Carlos Macoratti</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Agência</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtAgenciaCedente" runat="server">00057</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px; height: 32px;">
            <span style="font-size: 10pt">Conta</span></td>
        <td style="width: 97px; height: 32px;">
            <asp:TextBox ID="txtContaCedente" runat="server">37450</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">DV Conta</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtDVContaCedente" runat="server">7</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">
            Instrução</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtInstrucoes" runat="server" Width="282px">N&#227;o receber ap&#243;s o vencimento</asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
            <strong><span style="font-size: 14pt">Dados do Cliente/Sacado</span></strong></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Nome </span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtNomeSacado" runat="server">James Joyce</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">CPF/CNPJ </span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtCPF_CNPJSacado" runat="server">000000000000</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">Endereço</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtEnderecoSacado" runat="server">Rua Dublin , 1800</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">
            Bairro</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtBairro" runat="server">Dublin</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">
            Cidade</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtCidade" runat="server">Dublin</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">
            Estado</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtEstado" runat="server">Dublin</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px">
            <span style="font-size: 10pt">
            CEP</span></td>
        <td style="width: 97px">
            <asp:TextBox ID="txtCep" runat="server">12345-789</asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 18px; height: 28px;" bgcolor="#ffffff">
        </td>
        <td style="width: 97px; height: 28px;" bgcolor="#ffffff">
            <asp:Button ID="btnSalvarDados" runat="server" Text="Salvar Dados e Gerar Boleto"
                Width="184px" /></td>
    </tr>
</table>
