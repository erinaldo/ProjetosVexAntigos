<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB0003.aspx.cs" Inherits="Sistecno.UI.Web.WEB0003" %>

<%@ Register Src="~/UC/dtrPesquisa.ascx" TagPrefix="uc3" TagName="dtrPesquisa" %>
<%@ Register Src="~/UC/dtrMensagensValidacao.ascx" TagPrefix="uc3" TagName="dtrMensagensValidacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- MAIN CONTENT -->
    <div id="content">
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


                                    <a class="DTTT_button" id="A1" tabindex="0" aria-controls="datatable_tabletools" href="WEB0003.aspx?acao=novo&id=0&opc="><span>Novo</span>
                                        <div style="position: absolute; left: 0px; top: 0px; width: 41px; height: 25px; z-index: 99;">
                                        </div>
                                    </a>
                                </div>



                                <div id="dvPesq" runat="server">
                                    <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                </div>

                                <div id="dvManut" style="min-height: 400px" runat="server" visible="false">
                                    <uc3:dtrMensagensValidacao runat="server" ID="dtrMensagensValidacao" />

                                    <!-- widget div-->

                                    <div>
                                        <!-- widget content -->
                                        <div class="widget-body">

                                            <form>

                                                <fieldset>
                                                    <legend>CADASTRO
                                                    </legend>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">CNPJ/CPF</label>
                                                                <asp:UpdatePanel ID="upl" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtCNPJCadastro" runat="server" class="form-control input-xs" MaxLength="20" AutoPostBack="True"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">IE / RG</label>
                                                                <asp:TextBox ID="txtRG" runat="server" class="form-control input-xs" MaxLength="20"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">INSCRIÇÃO MUNICIPAL</label>
                                                                <asp:TextBox ID="txtInscricaoMunicipal" runat="server" class="form-control input-xs"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label class="control-label">CADASTRO</label>
                                                                <asp:TextBox ID="txtDataCadastro" runat="server" class="form-control input-xs" ReadOnly="True"></asp:TextBox>
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
                                                                <label class="control-label">Fantasia</label>
                                                                <asp:TextBox ID="txtFantasiaApelido" runat="server" class="form-control input-xs" MaxLength="30"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>


                                                <fieldset>
                                                    <legend>ENDEREÇO</legend>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">CEP</label>
                                                                <asp:TextBox ID="txtCEP" runat="server" class="form-control input-xs" MaxLength="8"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-5">
                                                                <label class="control-label">ENDEREÇO</label>
                                                                <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">NÚMERO</label>
                                                                <asp:TextBox ID="txtNumero" runat="server" class="form-control input-xs" MaxLength="10"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">COMPLEMENTO</label>
                                                                <asp:TextBox ID="txtComplemento" runat="server" class="form-control input-xs" MaxLength="60"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label class="control-label">ESTADO</label>
                                                                <asp:DropDownList ID="cboEstado" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged"></asp:DropDownList>

                                                            </div>

                                                            <div class="col-md-5">
                                                                <label class="control-label">CIDADE</label>
                                                                <asp:DropDownList ID="cboCidade" runat="server" class="form-control input-xs" AutoPostBack="True" OnSelectedIndexChanged="cboCidade_SelectedIndexChanged"></asp:DropDownList>

                                                            </div>

                                                            <div class="col-md-4">
                                                                <label class="control-label">BAIRRO</label>
                                                                <asp:DropDownList ID="cboBairro" runat="server" class="form-control input-xs"></asp:DropDownList>

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
                                                            <div class="col-md-6">
                                                                <asp:PlaceHolder ID="phMeioDeContatos" runat="server"></asp:PlaceHolder>

                                                            </div>
                                                            <div class="col-md-6">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>


                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

                                                        </div>
                                                        <div class="col-md-6" style="text-align: right">
                                                            <asp:Button ID="btnConfirmar" runat="server" Text="CONFIRMAR" CssClass="btn btn-primary btn-sm" OnClick="btnConfirmar_Click" />
                                                            <asp:Button ID="btnCancelar" runat="server" Text="CANCELAR" CssClass="btn btn-warning btn-sm" OnClick="btnCancelar_Click" />


                                                        </div>
                                                    </div>
                                                </div>

                                            </form>
                                        </div>
                                        <!-- end widget content -->

                                    </div>
                                    <!-- end widget div -->
                                </div>
                                <!-- end widget content -->

                            </div>
                        </div>
                        <!-- end widget div -->

                    </article>
                </div>
                <!-- end widget -->


            </section>


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
        </div>
    </div>

    <%--  <script>
        ResizeWH();
    </script>--%>
</asp:Content>
