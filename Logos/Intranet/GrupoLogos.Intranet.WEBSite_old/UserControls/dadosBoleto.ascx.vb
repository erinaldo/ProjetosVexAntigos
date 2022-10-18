
Partial Class dadosBoleto
    Inherits System.Web.UI.UserControl

    Protected Sub btnSalvarDados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvarDados.Click
        'salvar dados do Boleto no profile
        'dados do Documento
        'With Profile.DadosDocumento
        '    .DataDocumento = txtDataDocumento.Text
        '    .DataEmissao = txtDataEmissao.Text
        '    .DataProcessamento = txtDataProcessamento.Text
        '    .DataVencimento = txtDataVencimento.Text
        '    .NumeroDocumento = txtNumeroDocumento.Text
        '    .Sequencial = txtSequencial.Text
        '    .Valor = txtValor.Text
        'End With

        ''dados do Cedente
        'With Profile.DadosCedente
        '    .Aceite = True
        '    .Carteira = txtCarteira.Text
        '    .Contrato = txtNumeroContrato.Text
        '    .NomeCedente = txtNomeCedente.Text
        '    .AgenciaCedente = txtAgenciaCedente.Text
        '    .ContaCedente = txtContaCedente.Text
        '    .DVContaCedente = txtDVContaCedente.Text
        '    .instrucao = txtInstrucoes.Text
        'End With
        ''dados do Sacado

        'With Profile.DadosCliente
        '    .NomeSacado = txtNomeSacado.Text
        '    .CPF_CNPJSacado = txtCPF_CNPJSacado.Text
        '    .EnderecoSacado = txtEnderecoSacado.Text
        '    .Cidade = txtCidade.Text
        '    .Estado = txtEstado.Text
        '    .Bairro = txtBairro.Text
        '    .Cep = txtCep.Text
        'End With
        Response.Redirect("boletos/boletosBancoBrasil.aspx")
    End Sub

    Protected Sub txtCPF_CNPJSacado_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPF_CNPJSacado.TextChanged

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
