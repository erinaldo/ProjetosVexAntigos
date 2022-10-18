Imports Boleto
Imports System.Data
Partial Class boletosBanco
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        gerarBoletoBB()
    End Sub

    Private Sub gerarBoletoBB()
        Try
            Dim dt As New DataTable

            Dim obj = New SistranDAO.Fatura()
            obj.AtualizarDataValorBoleto(Me.Request.QueryString("titulo").ToString())
            dt = obj.Boleto(Me.Request.QueryString("titulo").ToString())




            'dt = New SistranDAO.Fatura().Boleto(Me.Request.QueryString("titulo").ToString())

            If dt.Rows.Count > 0 Then
                Dim bolBB

                If (CInt(dt.Rows(0)("BANCO").ToString()) = 237) Then
                    bolBB = New BoletoBradesco
                End If

                If (CInt(dt.Rows(0)("BANCO").ToString()) = 1) Then
                    bolBB = New BoletoBrasil
                End If


                If (CInt(dt.Rows(0)("BANCO").ToString()) = 341) Then
                    bolBB = New BoletoItau
                End If


                bolBB.Aceite = True
                bolBB.CedenteAgencia = dt.Rows(0)("AGENCIA").ToString()
                bolBB.CedenteConta = dt.Rows(0)("CONTA").ToString()
                bolBB.CedenteContaDV = dt.Rows(0)("CONTADV").ToString()
                bolBB.CedenteNome = dt.Rows(0)("NOME").ToString()
                bolBB.Carteira = Int32.Parse(dt.Rows(0)("CARTEIRA").ToString())
                bolBB.Instrucao1 = dt.Rows(0)("INSTRUCAO").ToString()
                ' bolBB.CodigoCedente = dt.Rows(0)("CODIGODOCEDENTE").ToString()

                ''

                If (Convert.ToInt32(dt.Rows(0)("NOSSONUMERO")) = 0) Then
                    bolBB.Sequencial = Convert.ToInt32(dt.Rows(0)("DOCUMENTO"))
                Else
                    bolBB.Sequencial = Convert.ToInt32(dt.Rows(0)("NOSSONUMERO"))
                End If

                bolBB.Documento = dt.Rows(0)("DOCUMENTO").ToString()
                'bolBB.NossoNumero = dt.Rows(0)("NOSSONUMERO").ToString()
                bolBB.DtDocumento = Convert.ToDateTime(dt.Rows(0)("DATADODOCUMENTO").ToString())
                bolBB.DtEmissao = Convert.ToDateTime(dt.Rows(0)("DATADEEMISSAO").ToString())
                bolBB.DtProcessamento = Convert.ToDateTime(dt.Rows(0)("DADADEPROCESSAMENTO").ToString())
                bolBB.DtVencimento = Convert.ToDateTime(dt.Rows(0)("DATADEVENCIMENTO").ToString())
                bolBB.Valor = CSng(Convert.ToDouble(dt.Rows(0)("VALOR").ToString()))
                ''
                bolBB.SacadoNome = dt.Rows(0)("NOMESACADO").ToString()
                bolBB.SacadoEndereco = dt.Rows(0)("ENDERECOSACADO").ToString()
                bolBB.SacadoCPF_CNPJ = dt.Rows(0)("CNPJSACADO").ToString()
                bolBB.SacadoCidade = dt.Rows(0)("CIDADE").ToString()
                bolBB.SacadoUF = dt.Rows(0)("ESTADO").ToString()
                bolBB.SacadoBairro = ""
                bolBB.SacadoCEP = dt.Rows(0)("CEP").ToString()

                Dim geraBoleto As New HTMLBoleto()
                geraBoleto.ImagesFolder = Server.MapPath("~/imagesBoleto").ToString()
                geraBoleto.AddBoleto(bolBB)
                geraBoleto.SaveToFile(Server.MapPath("~/imagesBoleto").ToString() + "\\boletobradesco" + Guid.NewGuid.ToString() + ".html")
                PlaceHolder1.Controls.Add(New LiteralControl(geraBoleto.ToString()))
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        
            'Response.Write(geraBoleto.ToString())


    End Sub
End Class
