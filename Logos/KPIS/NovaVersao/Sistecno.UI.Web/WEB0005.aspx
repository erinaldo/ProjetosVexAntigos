<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB0005.aspx.cs" Inherits="Sistecno.UI.Web.WEB0005" %>

<%@ Register Src="~/UC/dtrPesquisa.ascx" TagPrefix="uc3" TagName="dtrPesquisa" %>
<%@ Register Src="~/UC/dtrMensagensValidacao.ascx" TagPrefix="uc3" TagName="dtrMensagensValidacao" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/libs/jquery-2.1.1.min.js"></script>
    <script src="js/MascaraValidacao.js"></script>
    <!-- MAIN CONTENT -->
    <div id="content">
        <div id="corpo">
                <div class="row" style="height:5px">
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                   <%-- <h3 class="page-title txt-color-blueDark" style="margin: -0px 0 19px"><i class="fa fa-edit fa-fw"></i>
                        
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
                                    <h2><asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h2>

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


                                        <a class="DTTT_button" id="A1" tabindex="0" aria-controls="datatable_tabletools" href="WEB0005.aspx?acao=novo&id=0&opc="><span>Novo</span>
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
                            <div class="jarviswidget jarviswidget-sortable" id="Div1" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="true" data-widget-colorbutton="true" data-widget-deletebutton="false" role="widget" style="position: relative; opacity: 1;">
                                <header role="heading">
                                    <span class="widget-icon"><i class="glyphicon glyphicon-stats txt-color-darken"></i></span>
                                    <h2>MOTORISTAS MANUTENÇÃO</h2>

                                    <ul class="nav nav-tabs pull-right in" id="myTab">
                                        <li class="active" id="s1i">
                                            <a data-toggle="tab" href="#s1" aria-expanded="true"><span class="hidden-mobile hidden-tablet">DADOS PESSOAIS</span></a>
                                        </li>

                                        <li class="" id="s2i">
                                            <a data-toggle="tab" href="#s2" aria-expanded="true"><span class="hidden-mobile hidden-tablet">MEIOS DE CONTATO</span></a>
                                        </li>


                                        <li class="" id="s3i">
                                            <a data-toggle="tab" href="#s3" aria-expanded="true"><span class="hidden-mobile hidden-tablet">DADOS BANCÁRIOS</span></a>
                                        </li>



                                    </ul>

                                    <span class="jarviswidget-loader"><i class="fa fa-refresh fa-spin"></i></span>
                                </header>

                                <!-- widget div-->
                                <div class="no-padding" role="content">
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
                                                            <legend>DADOS PESSOAIS
                                                            </legend>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label">CPF</label>

                                                                        <asp:TextBox ID="txtCNPJCadastro" runat="server" class="form-control input-xs" MaxLength="20" AutoPostBack="True" OnTextChanged="txtCNPJCadastro_TextChanged1"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">RG</label>
                                                                        <asp:TextBox ID="txtRG" runat="server" CssClass="form-control input-xs" MaxLength="18" Width="100%" ReadOnly="true"></asp:TextBox>


                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">CADASTRO</label>
                                                                        <asp:TextBox ID="txtDataCadastro" runat="server" CssClass="form-control input-xs" MaxLength="18" Width="100%" ReadOnly="true"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">ATIVO</label>
                                                                        <asp:DropDownList ID="cboAtivo" runat="server" class="form-control input-xs">
                                                                            <asp:ListItem>SIM</asp:ListItem>
                                                                            <asp:ListItem>NAO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">LIBERADO</label>
                                                                        <asp:DropDownList ID="cboLiberado" runat="server" class="form-control input-xs">
                                                                            <asp:ListItem>SIM</asp:ListItem>
                                                                            <asp:ListItem>NAO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">DATA DE BLOQUEIO</label>
                                                                        <asp:TextBox ID="txtDataBloqueio" runat="server" CssClass="form-control input-xs" MaxLength="18" Width="100%" ReadOnly="true"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <label class="control-label">NOME</label>
                                                                        <asp:TextBox ID="txtRazaoSocialNome" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="control-label">PRIMEIRO NOME</label>
                                                                        <asp:TextBox ID="txtFantasiaApelido" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <label class="control-label">CEP</label>
                                                                        <asp:TextBox ID="txtCEP" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </div>


                                                                    <div class="col-md-3">
                                                                        <label class="control-label">ENDERECO</label>
                                                                        <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </div>



                                                                    <div class="col-md-3">
                                                                        <label class="control-label">NÚMERO</label>
                                                                        <asp:TextBox ID="txtNumero" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </div>



                                                                    <div class="col-md-3">
                                                                        <label class="control-label">COMPLEMENTO</label>
                                                                        <asp:TextBox ID="txtComplemento" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-5">
                                                                        <label class="control-label">ESTADO</label>
                                                                        <asp:DropDownList ID="cboEstado" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged"></asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CIDADE</label>
                                                                        <asp:DropDownList ID="cboCidade" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboCidade_SelectedIndexChanged"></asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">BAIRRO</label>
                                                                        <asp:DropDownList ID="cboBairro" runat="server" class="form-control input-xs" AutoPostBack="True"></asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <fieldset>
                                                    <legend>OUTRAS INFORMAÇÕES
                                                    </legend>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">CARTEIRA HABILITAÇÃO</label>
                                                                <asp:TextBox ID="txtCarteiraHab" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">Nº. REGISTRO</label>
                                                                <asp:TextBox ID="txtNumeroRegistro" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">CATEGORIA</label>
                                                                <asp:TextBox ID="txtCategoria" runat="server" class="form-control input-xs" MaxLength="5" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">1ª HABIL</label>
                                                                <asp:TextBox ID="txtDataPrimeiraHab" runat="server" class="form-control input-xs" MaxLength="10" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">VALIDADE</label>
                                                                <asp:TextBox ID="txtValidade" runat="server" class="form-control input-xs" MaxLength="10" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>



                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">UF NASCIMENTO</label>
                                                                <asp:DropDownList ID="cboEstadoNascimento" runat="server" class="form-control input-xs" OnSelectedIndexChanged="cboEstadoNascimento_SelectedIndexChanged"></asp:DropDownList>

                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">CIDADE NASCIMENTO</label>
                                                                <asp:DropDownList ID="cboCidadeNascimento" runat="server" class="form-control input-xs"></asp:DropDownList>

                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">LOCAL EMISSÃO RG</label>
                                                                <asp:TextBox ID="txtEmissaoRG" runat="server" class="form-control input-xs" MaxLength="10" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">NÚMERO INSS</label>
                                                                <asp:TextBox ID="txtNumeroINSS" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                            </div>



                                                        </div>
                                                    </div>



                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">NOME DO PAI</label>
                                                                <asp:TextBox ID="txtNomePai" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">NOME DA MÃE</label>
                                                                <asp:TextBox ID="txtNomeMae" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">ESTADO CIVIL</label>
                                                                <asp:DropDownList ID="cboEstadoCivil" runat="server" class="form-control input-xs">
                                                                    <asp:ListItem>CASADO</asp:ListItem>
                                                                    <asp:ListItem>SEPARADO</asp:ListItem>
                                                                    <asp:ListItem>VIUVO</asp:ListItem>
                                                                    <asp:ListItem>AMAZIADO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">CÔNJUGE</label>
                                                                <asp:TextBox ID="txtConjuge" runat="server" class="form-control input-xs" MaxLength="10" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">NUMERO PANCARD</label>
                                                                <asp:TextBox ID="txtNumeroPancard" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">VCTO PANCARY</label>
                                                                <asp:TextBox ID="txtVencimentoPancary" runat="server" class="form-control input-xs" MaxLength="10" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">VCTO BRASIL RISK</label>
                                                                <asp:TextBox ID="txtVencimentoBrasilRisk" runat="server" class="form-control input-xs" MaxLength="10" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">VCTO BUONNY</label>
                                                                <asp:TextBox ID="txtVencimentoBUony" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">COMPROV ENDEREÇO</label>
                                                                <asp:TextBox ID="txtUltimaComprovacaoDeEndereco" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="MascaraData(this)" onblur="validaDat(this, this.value);"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label class="control-label">VITIMA QTD ROUBOS</label>
                                                                <asp:TextBox ID="txtNumeroRoubos" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <label class="control-label">SOFREU QTD ACIDENTE</label>
                                                                <asp:TextBox ID="txtNumeroAcidentes" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <label class="control-label">ALIQUOTA SEST SENAT</label>
                                                                <asp:TextBox ID="txtAliquitaSestSenat" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>



                                                </fieldset>
                                            </div>


                                            <!-- end s1 tab pane -->

                                            <div class="tab-pane fade padding-10 " id="s2" style="min-height: 400px">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <fieldset>
                                                            <legend>MEIOS DE CONTATOS
                                                            </legend>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:DropDownList ID="cboTipoDeEndereco" runat="server" class="input-xs"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtEnderecoMeioDeContato" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>

                                                                    </div>
                                                                    <div class="col-md-1" style="text-align: right">
                                                                        <asp:Button ID="btnAdicionarMeioContato" runat="server" Text="Adicionar" class="btn btn-primary btn-xs" OnClick="btnAdicionarMeioContato_Click" />
                                                                        <asp:Label ID="lblSequencia" runat="server" Visible="False"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <asp:PlaceHolder ID="phMeioDeContatos" runat="server"></asp:PlaceHolder>
                                                                    </div>
                                                                    <div class="col-md-3">
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
                                                            <legend>DADOS BANCÁRIOS
                                                            </legend>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">BANCO</label>
                                                                        <asp:TextBox ID="txtNumeroBanco" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">AGÊNCIA</label>
                                                                        <asp:TextBox ID="txtAgencia" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">DÍGITO AGÊNCIA</label>
                                                                        <asp:TextBox ID="txtAgenciaDigito" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">TIPO DE CONTA</label>
                                                                        <asp:DropDownList ID="cboTipodeConta" runat="server" class="form-control input-xs" Width="100%">
                                                                            <asp:ListItem>CORRENTE</asp:ListItem>
                                                                            <asp:ListItem>POUPANÇA</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">NÚMERO DA CONTA</label>
                                                                        <asp:TextBox ID="txtNumeroConta" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">DÍGITO CONTA</label>
                                                                        <asp:TextBox ID="txtDigitoConta" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <label class="control-label">CNPJ CPF FAVORECIDO</label>
                                                                        <asp:TextBox ID="txtCpfFavorecido" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">NOME DO FAVORECIDO</label>
                                                                        <asp:TextBox ID="txtFavorecido" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">NÚMERO DA CONTA</label>
                                                                        <asp:TextBox ID="TextBox5" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <label class="control-label">DÍGITO CONTA</label>
                                                                        <asp:TextBox ID="TextBox6" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">VALOR DA PENSÃO</label>
                                                                        <asp:TextBox ID="txtValorPensao" runat="server" class="form-control input-xs" MaxLength="20" onkeypress="return SomenteNumero(event); "></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">APOSENTADO</label>
                                                                        <asp:DropDownList ID="cboAposentado" runat="server" class="form-control input-xs" Width="100%">
                                                                            <asp:ListItem>NAO</asp:ListItem>
                                                                            <asp:ListItem>SIM</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label class="control-label"></label>
                                                                        <asp:CheckBox ID="chkProprietario" runat="server" Text="PROPRIETARIO" />
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
