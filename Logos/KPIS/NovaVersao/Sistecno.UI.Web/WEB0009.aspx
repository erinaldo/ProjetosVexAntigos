<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB0009.aspx.cs" Inherits="Sistecno.UI.Web.WEB0009" %>

<%@ Register Src="~/UC/dtrPesquisa.ascx" TagPrefix="uc3" TagName="dtrPesquisa" %>
<%@ Register Src="~/UC/dtrMensagensValidacao.ascx" TagPrefix="uc3" TagName="dtrMensagensValidacao" %>
<%@ Register Src="~/UC/dtrPesquisaGenerica.ascx" TagPrefix="uc3" TagName="dtrPesquisaGenerica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/libs/jquery-2.1.1.min.js"></script>
    <script src="js/MascaraValidacao.js"></script>
    <script type="text/javascript">
        function openModal(tipo) {

            switch (tipo) {
                case "myModalPesquisaGenerica":
                    $('#myModalPesquisaGenerica').modal('show');
                    break;

                case "myModalPesquisaGenericaDetinatario":
                    $('#myModalPesquisaGenericaDetinatario').modal('show');
                    break;

                case "myModalPesquisaGenericaCliente":
                    $('#myModalPesquisaGenericaCliente').modal('show');
                    break;
            }


        }

        function fecharModal(tipo) {
            alert(tipo);
            switch (tipo) {
                case "myModalPesquisaGenerica":
                    $('#myModalPesquisaGenerica').modal('hide');
                    break;

                case "myModalPesquisaGenericaDetinatario":
                    $('#myModalPesquisaGenericaDetinatario').modal('hide');
                    break;

                case "myModalPesquisaGenericaCliente":
                    $('#myModalPesquisaGenericaCliente').modal('hide');
                    break;
            }
        }
    </script>

    <!-- MAIN CONTENT -->
    <div id="content">
        <asp:HiddenField runat="server" ID="hdIdRemetente" Value="0"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hdICliente" Value="0"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hdIdDestinatario" Value="0"></asp:HiddenField>
        <div id="corpo">
            <div class="row" style="height: 5px">
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                </div>

                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-8">
                </div>

            </div>
            <!-- widget grid -->
            <section id="widget-grid" class="content">

                <!-- row -->
                <div class="row">
                    <!-- NEW WIDGET START -->
                    <article class="col-sm-12" id="cnm">
                        <!-- Widget ID (each widget will need unique ID)-->
                        <div class="jarviswidget jarviswidget-color-blueDark jarviswidget-sortable" id="wid-id-0" data-widget-editbutton="false">

                            <header>
                                <span class="widget-icon"><i class="fa fa-building"></i></span>
                                <h2>
                                    <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h2>

                            </header>

                            <!-- widget div-->
                            <div>
                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropdown edit box -->
                                    <input type="text" />

                                </div>

                                <div id="dvGrid" style="min-height: 400px;" runat="server">

                                    <div id="dvbot" style="width: auto; position: absolute; top: 6px; left: 70%; z-index: 1" runat="server">
                                        <a tabindex="0" aria-controls="datatable_tabletools" data-toggle="modal" href="#myModal" class="DTTT_button">
                                            <span>Pesquisa</span>
                                        </a>
                                        <a class="DTTT_button" id="A1" tabindex="0" aria-controls="datatable_tabletools" href="WEB0009.aspx?acao=novo&id=0&opc=Coletas"><span>Novo</span>
                                            <div style="position: absolute; left: 0px; top: 0px; width: 41px; height: 25px; z-index: 99;">
                                            </div>
                                        </a>
                                    </div>

                                    <div id="dvPesq" runat="server">
                                        <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                                <!-- end widget content -->

                                <!-- Modal -->
                                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" style="margin: 1px 1px 1px 1px">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                    &times;</button>
                                                <h3 class="modal-title" style="font-weight: bold">PESQUISA
                                                </h3>
                                                <hr class="single" />
                                                <asp:UpdatePanel ID="updt" runat="server">
                                                    <ContentTemplate>
                                                        <uc3:dtrPesquisa runat="server" ID="dtrPesquisa" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>

                                            <div class="modal-body no-padding">
                                                <div class="col col-10">
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
                                </div>



                                <div id="dvManut" style="min-height: 400px;" runat="server" visible="false">
                                    <uc3:dtrMensagensValidacao runat="server" ID="dtrMensagensValidacao" />

                                    <article class="col-sm-12 col-md-12 col-lg-12 sortable-grid ui-sortable">
                                        <div class="jarviswidget jarviswidget-sortable" id="Div1" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="true" data-widget-colorbutton="true" data-widget-deletebutton="false" style="position: relative; opacity: 1;">
                                            <!--role="widget"-->
                                            <header role="heading">
                                                <span class="widget-icon"><i class="glyphicon glyphicon-stats txt-color-darken"></i></span>
                                                <h2>COLETAS MANUTENÇÃO</h2>

                                                <ul class="nav nav-tabs pull-right in" id="myTab">
                                                    <li class="active" id="s1i">
                                                        <a data-toggle="tab" href="#s1" aria-expanded="true"><span style="color: #555;">DADOS DA COLETA</span></a>
                                                    </li>

                                                    <li class="" id="s2i">
                                                        <a data-toggle="tab" href="#s2"><span style="color: #555;">REMETENTE</span></a>
                                                    </li>


                                                    <li class="" id="s3i">
                                                        <a data-toggle="tab" href="#s3" aria-expanded="true"><span style="color: #555;">DESTINATÁRIO</span></a>
                                                    </li>

                                                    <li class="" id="s4i">
                                                        <a data-toggle="tab" href="#s4" aria-expanded="true"><span style="color: #555;">CLIENTE</span></a>
                                                    </li>



                                                </ul>

                                                <span class="jarviswidget-loader"><i class="fa fa-refresh fa-spin"></i></span>
                                            </header>

                                            <!-- widget div-->
                                            <div class="no-padding">
                                                <!--role="content"-->
                                                <!-- widget edit box -->
                                                <div class="jarviswidget-editbox">
                                                    test
                                                </div>
                                                <!-- end widget edit box -->

                                                <div class="widget-body">
                                                    <!-- content -->


                                                    <div id="myTabContent" class="tab-content">


                                                        <div class="tab-pane fade padding-10 no-padding-bottom active in" id="s1">
                                                            <asp:UpdatePanel ID="upl" runat="server">
                                                                <ContentTemplate>
                                                                    <fieldset>
                                                                        <legend>SOLICITANTE
                                                                        </legend>

                                                                        <div class="form-group">
                                                                            <div class="row">

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">SOLICITANTE</label>
                                                                                    <asp:TextBox ID="txtSolicitanteNome" runat="server" CssClass="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">TELEFONE</label>
                                                                                    <asp:TextBox ID="txtSolicitanteTelefone" runat="server" CssClass="form-control input-xs" MaxLength="8"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">E-MAIL</label>
                                                                                    <asp:TextBox ID="txtSolicitanteEmial" runat="server" CssClass="form-control input-xs" MaxLength="4"></asp:TextBox>
                                                                                </div>


                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <div class="row">

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">NÚMERO</label>
                                                                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">DATA DA COLETA</label>
                                                                                    <asp:TextBox ID="txtDataDeColeta" runat="server" CssClass="form-control input-xs" MaxLength="10" onblur="validaDat(this, this.value);" onkeypress="MascaraData(this)"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">DATA PARA COLETAR</label>
                                                                                    <asp:TextBox ID="txtDataParaColetar" runat="server" CssClass="form-control input-xs" MaxLength="10" onblur="validaDat(this, this.value);" onkeypress="MascaraData(this)"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">TIPO</label>
                                                                                    <asp:TextBox ID="txtTipoDeServico" runat="server" CssClass="form-control input-xs" MaxLength="4" Text="COLETA"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">Data de Cadastro</label>
                                                                                    <asp:TextBox ID="txtDataDeCadastro" runat="server" CssClass="form-control input-xs" MaxLength="10" onblur="validaDat(this, this.value);" onkeypress="MascaraData(this)"></asp:TextBox>
                                                                                </div>


                                                                            </div>
                                                                        </div>


                                                                    </fieldset>

                                                                    <fieldset>
                                                                        <legend>DOCUMENTO
                                                                        </legend>

                                                                        <div class="form-group">
                                                                            <div class="row">

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">NOTA FISCAL</label>
                                                                                    <asp:TextBox ID="txtNotaFiscalDocumento" runat="server" CssClass="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">NATUREZA</label>
                                                                                    <asp:TextBox ID="txtNatureza" runat="server" CssClass="form-control input-xs" MaxLength="30"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">PESO REAL</label>
                                                                                    <asp:TextBox ID="txtPesoReal" runat="server" CssClass="form-control input-xs" MaxLength="10" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">PESO CUBADO</label>
                                                                                    <asp:TextBox ID="txtPesoCubado" runat="server" CssClass="form-control input-xs" MaxLength="10" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                                                </div>


                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">VOLUMES</label>
                                                                                    <asp:TextBox ID="txtVolumes" runat="server" CssClass="form-control input-xs" MaxLength="10" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                                                </div>


                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">VL. NF</label>
                                                                                    <asp:TextBox ID="txtValorDocumento" runat="server" CssClass="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                                                </div>


                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <div class="row">

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">VEÍCULO SUGERIDO</label>
                                                                                    <asp:TextBox ID="txtVeiculoSugerido" runat="server" CssClass="form-control input-xs" MaxLength="30"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">CONTATO / TELEFONE</label>
                                                                                    <asp:TextBox ID="txtContatoTelefone" runat="server" CssClass="form-control input-xs" MaxLength="20"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">MODAL</label>
                                                                                    <asp:DropDownList ID="cboModal" runat="server" CssClass="form-control input-xs" Width="100%">
                                                                                        <asp:ListItem Value="1">RODOVIÁRIO</asp:ListItem>
                                                                                        <asp:ListItem Value="4">AEREO</asp:ListItem>
                                                                                        <asp:ListItem Value="2">AQUAVIÁRIO</asp:ListItem>
                                                                                        <asp:ListItem Value="3">DUTOVIARIO</asp:ListItem>
                                                                                        <asp:ListItem Value="4">MULTIMODAL</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    </td>
                                                                                </div>



                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">

                                                                                <div class="col-md-12">
                                                                                    <label class="control-label">OBSERVAÇÕES</label>
                                                                                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="form-control input-xs" MaxLength="2000" TextMode="MultiLine" Height="60"></asp:TextBox>
                                                                                    <asp:Label ID="lblIdObserv" runat="server" Text="" Visible="false"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </fieldset>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>


                                                        <!-- end s1 tab pane -->

                                                        <div class="tab-pane fade padding-10 no-padding-bottom " id="s2" style="min-height: 400px">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <fieldset>
                                                                        <legend>REMETENTE
                                                                          <%--  <a tabindex="0" aria-controls="datatable_tabletools" data-toggle="modal" href="#myModalPesquisaGenerica" class="DTTT_button">
                                                                                <span>Pesquisa</span>
                                                                            </a>--%>


                                                                            <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" CssClass="btn btn-labeled btn-success" />--%>
                                                                        </legend>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">ID</label>
                                                                                    <asp:TextBox ID="txtPbId_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-1">
                                                                                    <label class="control-label" style="color: white"></label>
                                                                                    <asp:ImageButton ID="imgBuscarRemetente" runat="server" ImageUrl="~/img/lupa.png" Height="16" OnClick="imgBuscarRemetente_Click" />
                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">CNPJ</label>
                                                                                    <asp:TextBox ID="txtPbCnpj_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">IR/RG</label>
                                                                                    <asp:TextBox ID="txtPbIe_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <label class="control-label">RAZÃO SOCIAL</label>
                                                                                    <asp:TextBox ID="txtPbRazaoSocial_0" runat="server" CssClass="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">ENDEREÇO</label>
                                                                                    <asp:TextBox ID="txtPbEndereco_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">NUMERO</label>
                                                                                    <asp:TextBox ID="txtPbNumero_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">COMPLEMENTO</label>
                                                                                    <asp:TextBox ID="txtPbComplemento_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">BAIRRO</label>
                                                                                    <asp:TextBox ID="txtPbBairro_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">CIDADE</label>
                                                                                    <asp:TextBox ID="txtPbCidade_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">UF</label>
                                                                                    <asp:TextBox ID="txtPbUF_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">CEP</label>
                                                                                    <asp:TextBox ID="txtPbCEP_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">E-Mail</label>
                                                                                    <asp:TextBox ID="txtPbEmail_0" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                    </fieldset>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                        </div>
                                                        <!-- end s2 tab pane -->

                                                        <div class="tab-pane fade padding-10 no-padding-bottom " id="s3" style="min-height: 400px">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <fieldset>
                                                                        <legend>DESTINATÁRIO
                                                                        </legend>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">ID</label>
                                                                                    <asp:TextBox ID="txtPbId_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="imgPesquisaDestinatario" runat="server" ImageUrl="~/img/lupa.png" Height="16" OnClick="imgBuscarDestinatario_Click" />
                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">CNPJ</label>
                                                                                    <asp:TextBox ID="txtPbCnpj_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">IR/RG</label>
                                                                                    <asp:TextBox ID="txtPbIe_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <label class="control-label">RAZÃO SOCIAL</label>
                                                                                    <asp:TextBox ID="txtPbRazaoSocial_1" runat="server" CssClass="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">ENDEREÇO</label>
                                                                                    <asp:TextBox ID="txtPbEndereco_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">NUMERO</label>
                                                                                    <asp:TextBox ID="txtPbNumero_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">COMPLEMENTO</label>
                                                                                    <asp:TextBox ID="txtPbComplemento_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">BAIRRO</label>
                                                                                    <asp:TextBox ID="txtPbBairro_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">CIDADE</label>
                                                                                    <asp:TextBox ID="txtPbCidade_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">UF</label>
                                                                                    <asp:TextBox ID="txtPbUF_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">CEP</label>
                                                                                    <asp:TextBox ID="txtPbCEP_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">E-Mail</label>
                                                                                    <asp:TextBox ID="txtPbEmail_1" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>


                                                        <div class="tab-pane fade padding-10 no-padding-bottom " id="s4" style="min-height: 400px">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <fieldset>
                                                                        <legend>CLIENTE
                                                                        </legend>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">ID</label>
                                                                                    <asp:TextBox ID="txtPbId_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/lupa.png" Height="16" OnClick="imgBuscarCliente_Click" />
                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">CNPJ</label>
                                                                                    <asp:TextBox ID="txtPbCnpj_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="control-label">IR/RG</label>
                                                                                    <asp:TextBox ID="txtPbIe_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <label class="control-label">RAZÃO SOCIAL</label>
                                                                                    <asp:TextBox ID="txtPbRazaoSocial_2" runat="server" CssClass="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">ENDEREÇO</label>
                                                                                    <asp:TextBox ID="txtPbEndereco_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">NUMERO</label>
                                                                                    <asp:TextBox ID="txtPbNumero_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">COMPLEMENTO</label>
                                                                                    <asp:TextBox ID="txtPbComplemento_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">BAIRRO</label>
                                                                                    <asp:TextBox ID="txtPbBairro_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">CIDADE</label>
                                                                                    <asp:TextBox ID="txtPbCidade_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">UF</label>
                                                                                    <asp:TextBox ID="txtPbUF_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">CEP</label>
                                                                                    <asp:TextBox ID="txtPbCEP_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">E-Mail</label>
                                                                                    <asp:TextBox ID="txtPbEmail_2" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                    </fieldset>

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                        <!-- end content -->

                                                    </div>

                                                </div>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                </div>
                                                <div class="col-md-6" style="text-align: right">
                                                    <asp:Button ID="btnConfirmar" runat="server" Text="CONFIRMAR" CssClass="btn btn-primary btn-sm" OnClick="btnConfirmar_Click" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="CANCELAR" CssClass="btn btn-warning btn-sm" OnClick="btnCancelar_Click" />
                                                </div>
                                            </div>

                                        </div>
                                    </article>
                                </div>
                            </div>
                        </div>
                    </article>


                </div>
            </section>
        </div>
    </div>
    <!--fim-->

    <div class="modal  fade" id="myModalPesquisaGenerica" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false">
        <div class="modal-dialog" style="margin-left: 17%;">
            <div class="modal-content" style="min-width: 800px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title" style="font-weight: bold">PESQUISA REMETENTE
                    </h3>
                    <hr class="single" />

                    <uc3:dtrPesquisaGenerica runat="server" ID="dtrPesquisaGenericaRemetente" />

                </div>
                <div class="modal-body no-padding">


                    <div class="col col-10">
                    </div>

                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal  fade" id="myModalPesquisaGenericaDetinatario" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false">
        <div class="modal-dialog" style="margin-left: 17%;">
            <div class="modal-content" style="min-width: 800px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title" style="font-weight: bold">PESQUISA
                    </h3>
                    <hr class="single" />

                    <uc3:dtrPesquisaGenerica runat="server" ID="dtrPesquisaGenericaDetinatario" />

                </div>
                <div class="modal-body no-padding">


                    <div class="col col-10">
                    </div>

                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal  fade" id="myModalPesquisaGenericaCliente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false">
        <div class="modal-dialog" style="margin-left: 17%;">
            <div class="modal-content" style="min-width: 800px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title" style="font-weight: bold">PESQUISA
                    </h3>
                    <hr class="single" />

                    <uc3:dtrPesquisaGenerica runat="server" ID="dtrPesquisaGenericaCliente" />

                </div>
                <div class="modal-body no-padding">


                    <div class="col col-10">
                    </div>

                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
