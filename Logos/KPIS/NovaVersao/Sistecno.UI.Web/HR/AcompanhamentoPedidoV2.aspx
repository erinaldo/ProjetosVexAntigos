<%@ Page Title="" Language="C#" MasterPageFile="~/HR/mpProgramaHR.Master" AutoEventWireup="true" CodeBehind="AcompanhamentoPedidoV2.aspx.cs" Inherits="Sistecno.UI.Web.HR.AcompanhamentoPedidoV2" %>

<%@ Register Src="~/UC/UCGrafico.ascx" TagPrefix="uc1" TagName="UCGrafico" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div id="content">
        <div id="corpo">
            <div class="row" style="height: 20px">
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Timer ID="Timer1" runat="server" Interval="15000" OnTick="Timer1_Tick"></asp:Timer>
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

                                        <div class="row" style="text-align: right">
                                            <asp:Label ID="Label1" runat="server" Text="" Style="margin-right: 11px"></asp:Label>
                                            <br />

                                        </div>
                                        <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row" style="background-color: #fff">

                            <div class="col-md-12">
                                <asp:Panel runat="server" ID="panExemplo">
                                    <uc1:UCGrafico runat="server" ID="UCGrafico" />
                                </asp:Panel>
                                <br />

                            </div>

                        </div>
                    </article>


                </div>

            </section>


        </div>
    </div>
</asp:Content>
