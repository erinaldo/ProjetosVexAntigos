<%@ Page Title="" Language="C#" MasterPageFile="~/HR/mpProgramaHR.Master" AutoEventWireup="true" CodeBehind="AcompanhamentoPedido.aspx.cs" Inherits="WebSite.HR.AcompanhamentoPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
                                </asp:Timer>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div style="text-align:right">
                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                            <br />
                                        </div>
                                        <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </article>
                </div>

            </section>
        </div>
    </div>
    <%-- <div style="padding: 10px; margin: 5px 5px 5px 5px">
       
    </div>

    <div style="height: 50px; color: white">0</div>--%>
</asp:Content>
