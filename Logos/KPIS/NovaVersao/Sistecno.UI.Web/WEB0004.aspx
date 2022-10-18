<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB0004.aspx.cs" Inherits="Sistecno.UI.Web.WEB0004" %>

<%@ Register Src="~/UC/dtrPesquisa.ascx" TagPrefix="uc3" TagName="dtrPesquisa" %>
<%@ Register Src="~/UC/dtrMensagensValidacao.ascx" TagPrefix="uc3" TagName="dtrMensagensValidacao" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/libs/jquery-2.1.1.min.js"></script>
    <%--<script type="text/javascript">
        function ResizeWHf() {
            var w;
            var h;

            w = document.getElementById("corpo").clientHeight;
            // h = document.getElementById("dvManut1").clientHeight;

            // alert(document.getElementById("s2"));
            alert(w);
            //ResizeWH2(w, h + 500);
            ResizeWH2(w, w);
        }
    </script>--%>
    <!-- MAIN CONTENT -->
    <div id="content">
        <div id="corpo">
            <div class="row" style="height: 5px">
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                    <asp:Label ID="lblIdEmpresa" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblIdFilial" runat="server" Text="" Visible="false"></asp:Label>

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
                                    <h2>
                                        <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
                                    </h2>

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


                                        <a class="DTTT_button" id="A1" tabindex="0" aria-controls="datatable_tabletools" href="WEB0004.aspx?acao=novo&id=0opc="><span>Novo</span>
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
                                    <h2>EMITENTES MANUTENÇÃO</h2>

                                    <ul class="nav nav-tabs pull-right in" id="myTab">
                                        <li class="active">
                                            <a data-toggle="tab" href="#s1" aria-expanded="true"><span class="hidden-mobile hidden-tablet">EMPRESA</span></a>
                                        </li>

                                        <li class="" id="s2i">
                                            <a data-toggle="tab" href="#s2" aria-expanded="true"><span class="hidden-mobile hidden-tablet">FILIAL</span></a>
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
                                                <fieldset>
                                                    <legend>DADOS DA EMPRESA
                                                    </legend>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">EMPRESA</label>
                                                                <asp:UpdatePanel ID="upl" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtNomeEmpresaSimplificado" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">OPTANTE PELO SIMPES NACIONAL</label>
                                                                <asp:DropDownList ID="cboOptanteSimples" runat="server" class="form-control input-xs" AutoPostBack="True">
                                                                    <asp:ListItem>NAO</asp:ListItem>
                                                                    <asp:ListItem>SIM</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">ALÍQUOTA DO SIMPLES</label>
                                                                <asp:TextBox ID="txtAliquotaSimples" runat="server" class="form-control input-xs" onkeypress="return SomenteNumero(event);"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">ATIVO</label>
                                                                <asp:DropDownList ID="cboEmpresaAtiva" runat="server" class="form-control input-xs" AutoPostBack="True">
                                                                    <asp:ListItem>SIM</asp:ListItem>
                                                                    <asp:ListItem>NAO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </fieldset>

                                                <fieldset>
                                                    <legend>OUTRAS INFORMAÇÕES DA EMPRESA</legend>
                                                    <div class="form-group">

                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">ID</label>
                                                                <asp:TextBox ID="txtPbId" runat="server" class="form-control input-xs" MaxLength="8" ReadOnly="true"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-5">
                                                                <label class="control-label">CNPJ</label>
                                                                <asp:TextBox ID="txtPbCnpj" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                            </div>


                                                            <div class="col-md-4">
                                                                <label class="control-label">IE</label>
                                                                <asp:TextBox ID="txtPbIe" runat="server" class="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">CEP</label>
                                                                <asp:TextBox ID="txtPbCEP" runat="server" class="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-5">
                                                                <label class="control-label">ENDEREÇO</label>
                                                                <asp:TextBox ID="txtPbEndereco" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">NÚMERO</label>
                                                                <asp:TextBox ID="txtPbNumero" runat="server" class="form-control input-xs" MaxLength="10"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">COMPLEMENTO</label>
                                                                <asp:TextBox ID="txtPbComplemento" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="control-label">RAZAO SOCIAL</label>
                                                                <asp:TextBox ID="txtPbRazaoSocial" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <label class="control-label">FANTASIA</label>
                                                                <asp:TextBox ID="txtPbNomeFantasia" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <label class="control-label">ESTADO</label>
                                                                        <asp:DropDownList ID="cboEstado_Empresa" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboEstado_Empresa_SelectedIndexChanged"></asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-5">
                                                                        <label class="control-label">CIDADE</label>
                                                                        <asp:DropDownList ID="cboCidade_Empresa" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboCidade_Empresa_SelectedIndexChanged"></asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">BAIRRO</label>
                                                                        <asp:DropDownList ID="txtPbBairro" runat="server" class="form-control input-xs"></asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">E-mail</label>
                                                                        <asp:TextBox ID="txtPbEmail" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </fieldset>

                                                <fieldset>
                                                    <legend>E-MAIL PARA AVISOS
                                                    </legend>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label class="control-label">E-mail</label>
                                                                <asp:TextBox ID="txtEmailAviso" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                <asp:Label ID="lblIdContaDeEmail" runat="server"></asp:Label>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <label class="control-label">Apelido</label>
                                                                <asp:TextBox ID="txtApelidoAviso" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <label class="control-label">Senha</label>
                                                                <asp:TextBox ID="txtSenhaAviso" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label class="control-label">SMTP</label>
                                                                <asp:TextBox ID="txtSmtpAviso" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <label class="control-label">PORTA</label>
                                                                <asp:TextBox ID="txtPortaAviso" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <label class="control-label">COM CÓPIA</label>
                                                                <asp:TextBox ID="txtCopiaAviso" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </fieldset>

                                                <fieldset>
                                                    <legend>LOGOTIPO
                                                    </legend>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-4">

                                                                <asp:Image ID="imgLogo" runat="server" Height="70" />
                                                            </div>

                                                            <div class="col-md-8" style="white-space: nowrap">
                                                                <label class="control-label">ESCOLHER IMAGEM</label>
                                                                <asp:FileUpload ID="FileUploadControl" runat="server" class="multi" AllowMultiple="True" />
                                                                <asp:Label ID="lblIdCadastroImagem" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btnConfImagem" runat="server" Text="OK" class="btn btn-primary btn-xs" OnClick="btnConfImagem_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>


                                            </div>
                                            <!-- end s1 tab pane -->

                                            <div class="tab-pane fade padding-10 " id="s2">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <fieldset>
                                                            <legend>DADOS DA FILIAL
                                                            </legend>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">NOME</label>
                                                                        <asp:TextBox ID="txtNomeFilial" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">NÚMERO</label>
                                                                        <asp:TextBox ID="txtNumeroFilial" runat="server" class="form-control input-xs" MaxLength="10"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">UNIDADE</label>
                                                                        <asp:TextBox ID="txtUnidade" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CNPJ</label>
                                                                        <asp:TextBox ID="txtCNPJCadastro" runat="server" class="form-control input-xs" MaxLength="20" OnTextChanged="txtCNPJCadastro_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">IE</label>
                                                                        <asp:TextBox ID="txtRG" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">INSCR. MUNICIPAL</label>
                                                                        <asp:TextBox ID="txtInscricaoMunicipal" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="row">

                                                                    <div class="col-md-6">
                                                                        <label class="control-label">RAZÃO SOCIAL</label>
                                                                        <asp:TextBox ID="txtRazaoSocialNome" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="control-label">FANTASIA</label>
                                                                        <asp:TextBox ID="txtFantasiaApelido" runat="server" class="form-control input-xs" MaxLength="30"></asp:TextBox>
                                                                    </div>


                                                                </div>
                                                            </div>


                                                        </fieldset>

                                                        <fieldset>
                                                            <legend>MEIOS DE CONTATOS
                                                            </legend>
                                                            <div class="form-group">
                                                                <div class="row">

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
                                                                    <div class="col-md-6">
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-6" style="height: 150px; overflow-y: scroll" id="dvM">
                                                                        <asp:PlaceHolder ID="phMeioDeContatos" runat="server"></asp:PlaceHolder>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>

                                                        <fieldset>
                                                            <legend>ENDEREÇO</legend>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label">CEP</label>
                                                                        <asp:TextBox ID="txtCEP" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">ENDEREÇO</label>
                                                                        <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <label class="control-label">NÚMERO</label>
                                                                        <asp:TextBox ID="txtNumero" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">COMPLEMENTO</label>
                                                                        <asp:TextBox ID="txtComplemento" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">ESTADO</label>
                                                                        <asp:DropDownList ID="cboEstado" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged"></asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">CIDADE</label>
                                                                        <asp:DropDownList ID="cboCidade" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboCidade_SelectedIndexChanged"></asp:DropDownList>

                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">BAIRRO</label>
                                                                        <asp:DropDownList ID="cboBairro" runat="server" class="form-control input-xs" AutoPostBack="True"></asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>


                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">

                                                                    <fieldset>
                                                                        <legend>CERTIFICADO</legend>
                                                                        <div class="row">
                                                                            <div class="col-md-4">
                                                                                <label class="control-label">STATUS</label>
                                                                                <asp:TextBox ID="txtStatusCertificado" runat="server" class="form-control input-xs" MaxLength="60" ReadOnly="true"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <label class="control-label">VALIDADE</label>
                                                                                <asp:TextBox ID="txtValidadeCertificado" runat="server" class="form-control input-xs" ReadOnly="true" MaxLength="60"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <label class="control-label">NOME</label>
                                                                                <asp:TextBox ID="txtNomeCertificado" runat="server" class="form-control input-xs" ReadOnly="true" MaxLength="60"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>


                                                            </div>


                                                            <div class="col-md-6">

                                                                <fieldset>
                                                                    <legend>INSTALAR CERTIFICADO</legend>
                                                                    <div class="col-md-5">
                                                                        <label class="control-label">SELECIONE</label>
                                                                        <asp:FileUpload ID="FileUploadCentificado" runat="server" class="multi" AllowMultiple="false" />
                                                                    </div>

                                                                    <div class="col-md-5">
                                                                        <label class="control-label">SENHA</label>
                                                                        <asp:TextBox ID="txtSenhaDoCertificado" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-2" style="text-align: right">
                                                                        <label class="control-label">-</label><br />
                                                                        <asp:Button ID="btnIInstalar" runat="server" Text="Instalar" Enabled="false" CssClass="btn btn-primary btn-xs" />
                                                                    </div>
                                                                </fieldset>


                                                            </div>
                                                        </div>
                                                        </div>                                                   
                                                </div>
                                        </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <!-- end s2 tab pane -->


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
                //alert(document.getElementById("corpo").clientHeight);
                ResizeWH2(10, document.getElementById("corpo").clientHeight + 85);
            });
        });
    </script>

</asp:Content>
