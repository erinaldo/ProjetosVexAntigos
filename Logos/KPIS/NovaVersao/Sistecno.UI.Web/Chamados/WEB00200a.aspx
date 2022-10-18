<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEB00200a.aspx.cs" Inherits="SistecnoWeb.HelpDesk.WEB00200a" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB00200a.aspx.cs" Inherits="Sistecno.UI.Web.Chamados.WEB00200a" %>


<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style type="text/css">
        .formata
        { /* esta classe é somente 
               para formatar a fonte */
            font: 12px arial, verdana, helvetica, sans-serif;
        }

        a.dcontexto
        {
            position: relative;
            text-decoration: none;
            cursor: help;
            z-index: 1;
        }

            a.dcontexto:hover
            {
                background: transparent;
                color: #f00;
                z-index: 1;
            }

            a.dcontexto span
            {
                display: none;
            }

            a.dcontexto:hover span
            {
                display: block;
                position: absolute;
                width: auto;
                top: 3em;
                left: 0;
                font: 10px arial, verdana, helvetica, sans-serif;
                padding: 5px 10px;
                border: 1px solid #999;
                background: #f1ebeb;
                color: #000;
            }
    </style>

    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <%--<link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />--%>




    <script>
        function setarId(vl) {
            document.getElementById("hfId").value = vl;
            alert(document.getElementById("hfId").value);
        }


        function SetText() {

            var t1 = document.getElementById("txtUsuarioNome");
            var t2 = document.getElementById("txtUsuarioEmail");
            var t3 = "";
            document.getElementById("hdDadosUsuarios").value = t1.value + "|" + t2.value + "|" + t3.value;
        }

        function cliqueChk() {
            var txt = document.getElementById("txtAcompanha");
            var controle = document.getElementById("rbSelecionados");


            var chkBoxCount = controle.getElementsByTagName("input");

            var selecionados = "";

            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked) {
                    selecionados += chkBoxCount[i].value + "|";
                }
            }

            //alert(selecionados);
            txt.value = selecionados;

        }

    </script>

    <asp:HiddenField ID="hdDadosUsuarios" runat="server" />
    <asp:HiddenField ID="hfId" runat="server" />
    <asp:HiddenField ID="txtAcompanha" runat="server"></asp:HiddenField>

 <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConfirmar" />
        </Triggers>
        <ContentTemplate>--%>

            <asp:HiddenField ID="txtUserAtrib" runat="server" />

            <!-- MAIN CONTENT -->
            <div id="content">
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
                                            <div class="wrapper wrapper-content">
                                                <div class="row">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <div class="ibox float-e-margins">
                                                            <div class="ibox-title">
                                                                <h5>Status:
                                <asp:Label ID="lblStatus" runat="server" Text="Aberto"></asp:Label></h5>
                                                                <div class="ibox-tools">
                                                                    <!---->
                                                                </div>
                                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                            </div>

                                                            <!--Teste-->
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="col-sm-1" style="padding-right: 1px">
                                                                        <div class="form-group">
                                                                            <label>ID</label>
                                                                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-4" style="padding-left: 3px; padding-right: 3px">
                                                                        <div class="form-group">
                                                                            <label>Responsável</label>
                                                                            <asp:DropDownList ID="cboCliente" runat="server" class="form-control" placeholder="Selecione" OnSelectedIndexChanged="cboCliente_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-3" runat="server" id="dvUsuario" style="padding-left: 3px; padding-right: 3px">
                                                                        <div class="form-group">

                                                                            <label>Usuário</label>
                                                                            <div style="float: right; margin-right: 10px;">
                                                                                <a data-toggle='modal' href='#modal-form' class="dcontexto">
                                                                                    <span>Criar Novo Usuario</span>
                                                                                    <i class='fa fa-group'></i>
                                                                                </a>
                                                                            </div>

                                                                            <div style="float: right">
                                                                                <a data-toggle='modal' href='#modal-escolher' class="dcontexto">
                                                                                    <span>Adicionar Usuários para Acompanhamento</span>
                                                                                    <i class='fa fa-plus-square' style="margin-right: 15px;"></i>
                                                                                </a>


                                                                            </div>


                                                                            <asp:DropDownList ID="cboUsuarioResponsavel" runat="server" class="form-control" placeholder="Selecione" Width="100%"></asp:DropDownList>
                                                                            <%--<div class="form-group" style="text-align:left;">
                                            </div>--%>
                                                                        </div>
                                                                    </div>



                                                                    <div class="col-sm-4" runat="server" id="dvAtribuir" style="padding-left: 3px; padding-right: 3px;">
                                                                        <div class="form-group">
                                                                            <label>
                                                                                <asp:Label ID="lblTitAtribuir" runat="server" Text="Atribuir"></asp:Label></label>
                                                                            <asp:DropDownList ID="CboUsuarioAtribuir" runat="server" class="form-control" placeholder="Selecione" Style="width: 100%"></asp:DropDownList>

                                                                        </div>
                                                                    </div>



                                                                </div>
                                                                <!--Final teste -->


                                                                <div class="row">

                                                                    <div class="col-sm-7" style="padding-right: 5px; padding-right: 3px">
                                                                        <div class="form-group">
                                                                            <label>Assunto</label>
                                                                            <asp:TextBox ID="txtAssunto" runat="server" class="form-control" placeholder="Assunto" required=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-5" style="padding-left: 3px; padding-right: 3px">
                                                                        <div class="form-group">
                                                                            <label>Divisão</label>
                                                                            <asp:DropDownList ID="cboDivisao" runat="server" class="form-control" placeholder="Selecione" Width="100%"></asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12" style="padding-right: 3px">
                                                                        <div class="form-group">
                                                                            <CKEditor:CKEditorControl ID="txtTexto" runat="server" BasePath="~/ckeditor" class="form-control" Height="130"></CKEditor:CKEditorControl>
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="row-fluid">
                                                                    <div class="span12 bgcolor">
                                                                        <label>Anexos</label>

                                                                        <input type="file" id="myfile" multiple="multiple" name="myfile" runat="server" size="100" />

                                                                        <br />
                                                                        <asp:Button ID="btnConfirmar" runat="server" class='btn btn-w-m btn-primary' Text="Confirmar" OnClick="btnConfirmar_Click" />
                                                                        <asp:Button ID="btnIniciarFecharTarefa" runat="server" class='btn btn-w-m btn-primary' Text="Iniciar Chamado" Visible="false" OnClick="btnIniciarFecharTarefa_Click" />

                                                                        <asp:Label ID="Span1" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <hr />
                                                                <div class="row-fluid">
                                                                    <div class="span12 bgcolor">
                                                                        <label>Histórico do Chamado</label>
                                                                        <br />
                                                                        <br />
                                                                        <asp:PlaceHolder ID="phHistorico" runat="server"></asp:PlaceHolder>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                            <div id="modal-form" class="modal fade" aria-hidden="true">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-body">
                                                            <div class="row">
                                                                <h3>Criar Usuário</h3>
                                                                <div class="form-group">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label>Nome</label>
                                                                            <asp:TextBox ID="txtUsuarioNome" runat="server" class="form-control" placeholder="Nome" onblur="SetText();"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label>Email</label>
                                                                            <asp:TextBox ID="txtUsuarioEmail" runat="server" class="form-control" placeholder="e-mail" onblur="SetText();"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <div class="col-sm-12" style="text-align: right">
                                                                        <asp:Button ID="Button1" runat="server" Text="Confirmar" class="btn btn-sm btn-primary pull-right m-t-n-xs" UseSubmitBehavior="false" data-dismiss="modal" OnClick="Button1_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="modal-escolher" class="modal fade" aria-hidden="true">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-body">
                                                            <div class="row">
                                                                <h3>SELECIONE OS USÁRIOS PARA ACOMPANHAR ESTE CHAMADO</h3>
                                                                <div class="form-group">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <asp:CheckBoxList ID="rbSelecionados" runat="server" onclick="cliqueChk();"></asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <div class="col-sm-12" style="text-align: right">
                                                                            <asp:Button ID="brnConfirmarAcompanhamento" runat="server" Text="Confirmar" class="btn btn-sm btn-primary pull-right m-t-n-xs" UseSubmitBehavior="true" data-dismiss="modal" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </article>
                        </div>
                    </section>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>


    <!-- Mainly scripts -->

    <script src="js/libs/jquery-2.1.1.min.js"></script>

    <script src="js/bootstrap/bootstrap.min.js"></script>
    <%--<script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="js/plugins/jeditable/jquery.jeditable.js"></script>--%>

    <!-- Custom and plugin javascript -->
    <%--<script src="js/inspinia.js"></script>--%>
    <script src="js/plugin/pace/pace.min.js"></script>

</asp:Content>
