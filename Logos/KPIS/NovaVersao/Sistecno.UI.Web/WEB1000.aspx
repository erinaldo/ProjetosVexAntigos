<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB1000.aspx.cs" Inherits="Sistecno.UI.Web.WEB1000" EnableEventValidation="false" %>

<%@ Register Src="~/UC/dtrPesquisa.ascx" TagPrefix="uc3" TagName="dtrPesquisa" %>
<%@ Register Src="~/UC/dtrMensagensValidacao.ascx" TagPrefix="uc3" TagName="dtrMensagensValidacao" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN CONTENT -->
    <div id="content">
        <div id="corpo">
            <div class="row" style="height: 5px">
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                    <%--  <script>
        ResizeWH();
    </script>--%>
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


                                    <a class="DTTT_button" id="A1" tabindex="0" aria-controls="datatable_tabletools" href="WEB1000.aspx?opc=&acao=novo&id=0"><span>Novo</span>
                                        <div style="position: absolute; left: 0px; top: 0px; width: 41px; height: 25px; z-index: 99;">
                                        </div>
                                    </a>
                                </div>



                                <div id="dvPesq" runat="server">
                                    <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                </div>

                                <div id="dvManut" style="min-height: 400px" runat="server" visible="false">

                                    <%--  <script>
        ResizeWH();
    </script>--%>
                                    <uc3:dtrMensagensValidacao runat="server" ID="dtrMensagensValidacao" />
                                    <%--  <script>
        ResizeWH();
    </script>--%>


                                    <!-- widget div-->

                                    <div>
                                        <!-- widget content -->
                                        <div class="widget-body">

                                            <form>
                                                <div class="col-md-2">
                                                </div>
                                                <fieldset class="col-md-8">
                                                    <legend>CONEXÃO
                                                    </legend>

                                                    <div class="form-group">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="control-label">Id</label>
                                                                <asp:TextBox ID="txtIdConexao" runat="server" class="form-control input-xs" MaxLength="20" AutoPostBack="True"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <label class="control-label">Id Cliente</label>
                                                                <asp:TextBox ID="txtIdCliente" runat="server" class="form-control input-xs" MaxLength="20" AutoPostBack="True"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="form-group">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="control-label">Cliente</label>
                                                                <asp:DropDownList ID="DropDownListCliente" runat="server" class="form-control input-xs">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <label class="control-label">IP</label>
                                                                <asp:TextBox ID="txtIP" runat="server" class="form-control input-xs"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>


                                                    <div class="form-group">
                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <label class="control-label">Banco de dados</label>
                                                                <asp:TextBox ID="txtBancoDeDados" runat="server" class="form-control input-xs"></asp:TextBox>
                                                            </div>


                                                            <div class="col-md-6">
                                                                <label class="control-label">Usuário</label>
                                                                <asp:TextBox ID="txtUsuario" runat="server" class="form-control input-xs"></asp:TextBox>
                                                            </div>


                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <label class="control-label">Senha</label>

                                                                <asp:TextBox ID="txtSenha" runat="server" class="form-control input-xs"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <label class="control-label">Porta</label>

                                                                <asp:TextBox ID="txtPorta" runat="server" class="form-control input-xs"></asp:TextBox>
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
                                                </fieldset>
                                                
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
