<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEB00200.aspx.cs" Inherits="WebSite.Chamados.WEB00200" Culture="pt-BR" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrograma.Master" AutoEventWireup="true" CodeBehind="WEB00200.aspx.cs" Inherits="Sistecno.UI.Web.Chamados.WEB00200" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
            <Triggers>
            <asp:PostBackTrigger ControlID="btnPesquisa" />
        </Triggers>   
        <ContentTemplate>--%>
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
                                            <div class="col-lg-12">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <table style="width: 100%; padding: 1px">
                                                            <tr>
                                                                <td style="width: 99%">
                                                                    <asp:Button ID="btnHabPesquisa" runat="server" Text="Pesquisa >>" class='btn btn-primary btn-xs' OnClick="btnHabPesquisa_Click" />
                                                                    <asp:Button ID="btnMeusChamados" runat="server" Text="Meus Chamados" class='btn btn-primary btn-xs' OnClick="btnMeusChamados_Click" />
                                                                    <asp:Button ID="btnSusChamados" runat="server" Text="Chamados Para Mim" class='btn btn-primary btn-xs' OnClick="btnSusChamados_Click" />
                                                                    <asp:Button ID="btnAtribuirChamados" runat="server" Text="Atribuir Chamados" class='btn btn-primary btn-xs' Visible="False" OnClick="btnAtribuir_Click" />

                                                                </td>
                                                                <td style="width: 2%">
                                                                    <button type='button' class='btn btn-primary btn-xs' onclick="window.location.href='web00200a.aspx?opc=Novo Chamado'">Novo Chamado</button>
                                                                </td>
                                                            </tr>
                                                        </table>


                                                    </div>
                                                    <br />
                                                    <div class="ibox-content" id="tblPesquisa" runat="server" visible="false" style="width: 100%;">
                                                        <div class="form-group">
                                                            <div class="row">

                                                                <div class="col-md-2">
                                                                    <label>ID</label>
                                                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control input-xs" placeholder="ID"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-2">

                                                                    <div>
                                                                        <label style="margin-left: 36px;">Período</label>
                                                                        <asp:TextBox ID="txtPesauisaInicio" runat="server" class="form-control input-xs" placeholder="Data Inicial"></asp:TextBox>

                                                                    </div>


                                                                    <%--<div id="data_ini">
                                                                        <div class="input-group date">
                                                                            <span class="input-group-addon" style="border: 0"><i class="fa fa-calendar" style="border: 0"></i></span>
                                                                        </div>
                                                                    </div>--%>
                                                                </div>

                                                                <div class="col-md-2">
                                                                    <label style="margin-left: 36px;">Até</label>
                                                                    <div id="data_fim">
                                                                        <div class="input-group date ">
                                                                            <span class="input-group-addon" style="border: 0"><i class="fa fa-calendar" style="border: 0"></i></span>
                                                                            <asp:TextBox ID="txtPesauisaFim" runat="server" class="form-control input-xs" placeholder="Data Final"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-2">
                                                                    <label>Usuário</label>
                                                                    <asp:TextBox ID="txtUsuario" runat="server" class="form-control input-xs" placeholder="Nome Usuário"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <label>Status</label>
                                                                    <asp:DropDownList ID="cboDivisao" runat="server" class="form-control input-xs" placeholder="Selecione">
                                                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                                                        <asp:ListItem Text="Aberto"></asp:ListItem>
                                                                        <asp:ListItem Text="Em Anadamento"></asp:ListItem>
                                                                        <asp:ListItem Text="Finalizado"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="col-md-1" style="text-align: right;">
                                                                    <label>.</label>
                                                                    <br />
                                                                    <asp:Button ID="btnPesquisa" runat="server" CssClass="btn btn-primary btn-xs" Text="Pesquisar" OnClick="btnPesquisa_Click"></asp:Button>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="ibox-content">
                                                    <%--<asp:PlaceHolder ID="phTable" runat="server"></asp:PlaceHolder>--%>
                                                    <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>



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
        </div>

    </div>
    <%--   </ContentTemplate>
           </asp:UpdatePanel>--%>
    <!-- Mainly scripts -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="../js/libs/jquery-2.1.1.min.js"></script>
    <script src="../js/bootstrap/bootstrap.min.js"></script>


    
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>

    <script>
        $(function () {            
            $("#ContentPlaceHolder1_txtPesauisaInicio").datepicker();
        });
    </script>


</asp:Content>
