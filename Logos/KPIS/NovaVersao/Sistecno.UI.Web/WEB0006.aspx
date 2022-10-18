<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB0006.aspx.cs" Inherits="Sistecno.UI.Web.WEB0006" %>

<%@ Register Src="~/UC/dtrPesquisa.ascx" TagPrefix="uc3" TagName="dtrPesquisa" %>
<%@ Register Src="~/UC/dtrMensagensValidacao.ascx" TagPrefix="uc3" TagName="dtrMensagensValidacao" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/libs/jquery-2.1.1.min.js"></script>
    <script src="js/MascaraValidacao.js"></script>
    <!-- MAIN CONTENT -->
    <div id="content">
        <asp:HiddenField runat="server" ID="hdIdVeiculo" Value="0"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hdIdMotorista" Value="0"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hdIdProprietario" Value="0"></asp:HiddenField>
        <div id="corpo">
                <div class="row" style="height:5px">
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                   <%-- <h3 class="page-title txt-color-blueDark" style="margin: -0px 0 19px"><i class="fa fa-edit fa-fw"></i>
                        <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                    </h3>--%>
                </div>

                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-8">
                </div>

            </div>
            <!-- widget grid -->
            <section id="widget-grid" class="content">

                <!-- row -->
                <div class="row">

                    <div id="dvGrid" style="min-height: 400px;" runat="server">
                        <!-- NEW WIDGET START -->
                        <article class="col-sm-12" id="cnm">
                            <!-- Widget ID (each widget will need unique ID)-->
                            <div class="jarviswidget jarviswidget-color-blueDark jarviswidget-sortable" id="wid-id-0" data-widget-editbutton="false">

                                <header>
                                    <span class="widget-icon"><i class="fa fa-building"></i></span>
                                    <h2> <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h2>

                                </header>


                                <!-- widget div-->
                                <div>

                                    <!-- widget edit box -->
                                    <div class="jarviswidget-editbox">
                                        <!-- This area used as dropdown edit box -->
                                        <input type="text" />

                                    </div>


                                    <div id="dvbot" style="width: auto; position: absolute; top: 6px; left: 70%; z-index: 1" runat="server">
                                        <a tabindex="0" aria-controls="datatable_tabletools" data-toggle="modal" href="#myModal" class="DTTT_button">
                                            <span>Pesquisa</span>
                                        </a>


                                        <a class="DTTT_button" id="A1" tabindex="0" aria-controls="datatable_tabletools" href="WEB0006.aspx?acao=novo&id=0&opc="><span>Novo</span>
                                            <div style="position: absolute; left: 0px; top: 0px; width: 41px; height: 25px; z-index: 99;">
                                            </div>
                                        </a>
                                    </div>



                                    <div id="dvPesq" runat="server">
                                        <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                    </div>


                                </div>
                                <!-- end widget content -->

                            </div>

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

                                            <uc3:dtrPesquisa runat="server" ID="dtrPesquisa" />

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
                            <!-- /.modal -->

                        </article>
                    </div>

                    <!--ini-->
                    <div id="dvManut" style="min-height: 400px;" runat="server" visible="false">
                        <uc3:dtrMensagensValidacao runat="server" ID="dtrMensagensValidacao" />

                        <article class="col-sm-12 col-md-12 col-lg-12 sortable-grid ui-sortable">
                            <div class="jarviswidget jarviswidget-sortable" id="Div1" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="true" data-widget-colorbutton="true" data-widget-deletebutton="false" style="position: relative; opacity: 1;">
                                <!--role="widget"-->
                                <header role="heading">
                                    <span class="widget-icon"><i class="glyphicon glyphicon-stats txt-color-darken"></i></span>
                                    <h2>VEÍCULOS MANUTENÇÃO</h2>

                                    <ul class="nav nav-tabs pull-right in" id="myTab">
                                        <li class="active" id="s1i">
                                            <a data-toggle="tab" href="#s1" aria-expanded="true"><span class="hidden-mobile hidden-tablet">DADOS DO VEÍCULO</span></a>
                                        </li>

                                        <li class="" id="s2i">
                                            <a data-toggle="tab" href="#s2" aria-expanded="true"><span class="hidden-mobile hidden-tablet">MOTORÍSTA</span></a>
                                        </li>


                                        <li class="" id="s3i">
                                            <a data-toggle="tab" href="#s3" aria-expanded="true"><span class="hidden-mobile hidden-tablet">PROPRIETÁRIO</span></a>
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
                                                            <legend>DADOS DO VEÍCULO
                                                            </legend>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">PLACA</label>
                                                                        <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control input-xs" MaxLength="8" OnTextChanged="txtPlaca_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">MODELO</label>
                                                                        <asp:DropDownList ID="cboModelo" runat="server" CssClass="form-control input-xs" Style="width: 100%"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">ANO FABRICAÇÃO</label>
                                                                        <asp:TextBox ID="txtAnoFabricacao" runat="server" CssClass="form-control input-xs" MaxLength="4" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">ANO MODELO</label>
                                                                        <asp:TextBox ID="txtAnoModelo" runat="server" CssClass="form-control input-xs" MaxLength="4" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">COR</label>
                                                                        <asp:TextBox ID="txtCor" runat="server" CssClass="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">TIPO</label>
                                                                        <asp:DropDownList ID="cboTipo" runat="server" CssClass="form-control input-xs" Style="width: 100%">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">EIXOS</label>
                                                                        <asp:TextBox ID="txtEixos" runat="server" CssClass="form-control input-xs" onkeypress="return SomenteNumero(event);" MaxLength="2"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">CAPACIDADE DE CARGA (M3)</label>
                                                                        <asp:TextBox ID="txtCapacidadeCargam3" runat="server" CssClass="form-control input-xs" onkeypress="return SomenteNumero(event);" MaxLength="4"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">CAPACIDADE DE CARGA (KG)</label>
                                                                        <asp:TextBox ID="txtCapacidadeCargakg" runat="server" CssClass="form-control input-xs" onkeypress="return SomenteNumero(event);" MaxLength="4"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">CNH PERMITIDAS</label>
                                                                        <asp:DropDownList ID="cboCategoriaPermitida" runat="server" CssClass="form-control input-xs">
                                                                            <asp:ListItem>SELECIONE</asp:ListItem>
                                                                            <asp:ListItem>A</asp:ListItem>
                                                                            <asp:ListItem>A/B</asp:ListItem>
                                                                            <asp:ListItem>A/C</asp:ListItem>
                                                                            <asp:ListItem>A/D</asp:ListItem>
                                                                            <asp:ListItem>A/E</asp:ListItem>
                                                                            <asp:ListItem>B</asp:ListItem>
                                                                            <asp:ListItem>C</asp:ListItem>
                                                                            <asp:ListItem>D</asp:ListItem>
                                                                            <asp:ListItem>E</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">RASTREADOR</label>
                                                                        <asp:DropDownList ID="cboRastreador" runat="server" CssClass="form-control input-xs" Style="width: 100%">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">SÉRIE DO EQUIPAMENTO</label>
                                                                        <asp:TextBox ID="txtSerieRastreador" runat="server" CssClass="form-control input-xs" MaxLength="20"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-7">
                                                                        <label class="control-label">CHASSI</label>
                                                                        <asp:TextBox ID="txtChassi" runat="server" CssClass="form-control input-xs" MaxLength="30"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">RENAVAN</label>
                                                                        <asp:TextBox ID="txtRenavam" runat="server" CssClass="form-control input-xs" MaxLength="30"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">ANTT</label>
                                                                        <asp:TextBox ID="txtAntt" runat="server" CssClass="form-control input-xs" MaxLength="30"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">VENCIMENTO ANTT</label>
                                                                        <asp:TextBox ID="txtVencAntt" runat="server" CssClass="form-control input-xs" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);" MaxLength="10"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">DATA DE LICENCIAMENTO</label>
                                                                        <asp:TextBox ID="txtDataLicenciamento" runat="server" CssClass="form-control input-xs" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);" MaxLength="10"></asp:TextBox>

                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>


                                            <!-- end s1 tab pane -->

                                            <div class="tab-pane fade padding-10 " id="s2" style="min-height: 400px">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <fieldset>
                                                            <legend>MOTORÍSTA
                                                            </legend>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CPF</label>
                                                                        <asp:TextBox ID="txtMotoristaCpf" runat="server" CssClass="form-control input-xs" MaxLength="18" onKeyPress="MascaraCPF(this);" AutoPostBack="True" OnTextChanged="txtMotoristaCpf_TextChanged"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-8">
                                                                        <label class="control-label">NOME</label>
                                                                        <asp:TextBox ID="txtMotoristaNome" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">ENDEREÇO</label>
                                                                        <asp:TextBox ID="txtMotoristaEndreco" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">NÚMERO</label>
                                                                        <asp:TextBox ID="txtMotoristaNumero" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">COMPLEMENTO</label>
                                                                        <asp:TextBox ID="txtMotoristaComplemento" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">ESTADO</label>
                                                                        <asp:DropDownList ID="cboMotoristaEstado" runat="server" CssClass="form-control input-xs" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged">
                                                                        </asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CIDADE</label>
                                                                        <asp:DropDownList ID="cboMotoristaCidade" runat="server" CssClass="form-control input-xs">
                                                                        </asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CEP</label>
                                                                        <asp:TextBox ID="txtMotoristaCEP" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">RG</label>
                                                                        <asp:TextBox ID="txtMotoristaRg" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">DATA DE NASCIMENTO</label>
                                                                        <asp:TextBox ID="txtMotoristaDataDeNascimento" runat="server" CssClass="form-control input-xs" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);" MaxLength="10"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">CNH</label>
                                                                        <asp:TextBox ID="txtMotoristaCNH" runat="server" CssClass="form-control input-xs" MaxLength="15"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">CATEGORIA</label>
                                                                        <asp:TextBox ID="txtMotoristaCategoria" runat="server" CssClass="form-control input-xs" MaxLength="3"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <!-- end s2 tab pane -->

                                            <div class="tab-pane fade padding-10" id="s3" style="min-height: 400px">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <fieldset>
                                                            <legend>DADOS PROPRIETÁRIO
                                                            </legend>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CPF</label>
                                                                        <asp:TextBox ID="txtProprietarioCpf" runat="server" CssClass="form-control input-xs" MaxLength="18" onKeyPress="MascaraCPF(this);" AutoPostBack="True" OnTextChanged="txtProprietarioCpf_TextChanged"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-8">
                                                                        <label class="control-label">NOME</label>
                                                                        <asp:TextBox ID="txtProprietarioNome" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">ENDEREÇO</label>
                                                                        <asp:TextBox ID="txtProprietarioEndereco" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">NÚMERO</label>
                                                                        <asp:TextBox ID="txtProprietarioNumero" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">COMPLEMENTO</label>
                                                                        <asp:TextBox ID="txtProprietarioComplemento" runat="server" CssClass="form-control input-xs"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">ESTADO</label>
                                                                        <asp:DropDownList ID="cboProprietarioEstado" runat="server" CssClass="form-control input-xs" Style="width: 100%" AutoPostBack="True" OnSelectedIndexChanged="cboProprietarioEstado_SelectedIndexChanged">
                                                                        </asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CIDADE</label>
                                                                        <asp:DropDownList ID="cboProprietarioCidade" runat="server" CssClass="form-control input-xs" Style="width: 100%">
                                                                        </asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CEP</label>
                                                                        <asp:TextBox ID="txtProprietarioCEP" runat="server" CssClass="form-control input-xs"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>


                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>


                                            <!-- end content -->

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
                                    </div>
                                </div>

                            </div>
                        </article>

                    </div>

                </div>
            </section>
            <!-- end widget div -->
        </div>

    </div>
    <!--fim-->

    <script>
        $(document).ready(function () {
            $('#s2i').click(function () {
                ResizeWH2(10, document.getElementById("corpo").clientHeight + 85);
            });
            $('#s1i').click(function () {
                ResizeWH2(10, document.getElementById("corpo").clientHeight + 85);
            });
            //$('#s4i').click(function () {
            //    ResizeWH2(10, document.getElementById("corpo").clientHeight + 85);
            //});
        });
    </script>



</asp:Content>
