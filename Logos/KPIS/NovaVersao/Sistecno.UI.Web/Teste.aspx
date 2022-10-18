<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teste.aspx.cs" Inherits="Sistecno.UI.Web.Teste" %>

<%@ Register Src="~/UC/dtrPesquisa.ascx" TagPrefix="uc1" TagName="dtrPesquisa" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sietecno</title>
    <meta charset="utf-8" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">-->

 <%--   <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />--%>

    <!-- Basic Styles -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/font-awesome.min.css" />

    <!-- SmartAdmin Styles : Caution! DO NOT change the order -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production-plugins.min.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production.min.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-skins.min.css" />

    <!-- SmartAdmin RTL Support -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-rtl.min.css" />

    <!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/demo.min.css" />

    <!-- GOOGLE FONT -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700" />

    <!-- Specifying a Webpage Icon for Web Clip 
			 Ref: https://developer.apple.com/library/ios/documentation/AppleApplications/Reference/SafariWebContent/ConfiguringWebApplications/ConfiguringWebApplications.html -->
    <link rel="apple-touch-icon" href="img/splash/sptouch-icon-iphone.png" />
    <link rel="apple-touch-icon" sizes="76x76" href="img/splash/touch-icon-ipad.png" />
    <link rel="apple-touch-icon" sizes="120x120" href="img/splash/touch-icon-iphone-retina.png" />
    <link rel="apple-touch-icon" sizes="152x152" href="img/splash/touch-icon-ipad-retina.png" />

    <!-- iOS web-app metas : hides Safari UI Components and Changes Status Bar Appearance -->
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />

    <!-- Startup image for web apps -->
    <link rel="apple-touch-startup-image" href="img/splash/ipad-landscape.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)" />
    <link rel="apple-touch-startup-image" href="img/splash/ipad-portrait.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)" />
    <link rel="apple-touch-startup-image" href="img/splash/iphone.png" media="screen and (max-device-width: 320px)" />

    <style type="text/css">
        html
        {
            height: 100%;
        }

        body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        #conteudo
        {
            min-height: 100%;
            height: auto;
        }
    </style>

    
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdPk" runat="server" />

        <div id="conteudo">

             <div id="content">


        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <h3 class="page-title txt-color-blueDark" style="margin: -0px 0 19px"><i class="fa fa-building "></i>
                    <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                    <span></span></h3>

            </div>
        </div>



        <!-- widget grid -->
        <section id="widget-grid" class="content">

            <!-- row -->
            <div class="row">

                <!-- NEW WIDGET START -->
                <article class="col-sm-12">

                    <!-- Widget ID (each widget will need unique ID)-->
                    <div class="jarviswidget jarviswidget-color-blueDark jarviswidget-sortable" id="wid-id-0" data-widget-editbutton="false">

                        <header>
                            <span class="widget-icon"><i class="fa fa-building"></i></span>
                            <h2>CADASTRO BÁSICO</h2>

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


                                <a class="DTTT_button" id="A1" tabindex="0" aria-controls="datatable_tabletools" href="WEB0002_EDIT.aspx"><span>Novo</span>
                                    <div style="position: absolute; left: 0px; top: 0px; width: 41px; height: 25px; z-index: 99;">
                                    </div>
                                </a>
                            </div>



                            <div id="dvPesq" runat="server">
                                <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                            </div>

                            <div id="dvManut" style="min-height: 400px" runat="server" visible="false">

                                <!-- widget div-->

                                <div>
                                    <%--<!-- widget edit box -->
										<div class="jarviswidget-editbox">
											<!-- This area used as dropdown edit box -->
											<input class="form-control" type="text">
										</div>
										<!-- end widget edit box -->--%>

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
                                                            <asp:TextBox ID="txtCNPJCadastro" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <label class="control-label">IE / RG</label>
                                                            <asp:TextBox ID="txtRG" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <label class="control-label">INSCRIÇÃO MUNICIPAL</label>
                                                            <asp:TextBox ID="txtInscricaoMunicipal" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <label class="control-label">CADASTRO</label>
                                                            <asp:TextBox ID="txtDataCadastro" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>




                                                    </div>
                                                </div>



                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label class="control-label">RAZÃO SOCIAL</label>
                                                            <asp:TextBox ID="txtRazaoSocialNome" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <label class="control-label">Fantasia</label>
                                                            <asp:TextBox ID="txtFantasiaApelido" runat="server" class="form-control input-xs"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtCEP" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-5">
                                                            <label class="control-label">ENDEREÇO</label>
                                                            <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <label class="control-label">NÚMERO</label>
                                                            <asp:TextBox ID="txtNumero" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <label class="control-label">COMPLEMENTO</label>
                                                            <asp:TextBox ID="txtComplemento" runat="server" class="form-control input-xs"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label class="control-label">ESTADO</label>
                                                            <asp:DropDownList ID="cboEstado" runat="server" class="form-control input-xs"></asp:DropDownList>

                                                        </div>

                                                        <div class="col-md-5">
                                                            <label class="control-label">CIDADE</label>
                                                            <asp:DropDownList ID="cboCidade" runat="server" class="form-control input-xs"></asp:DropDownList>

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
                                                        <div class="col-md-1">
                                                            <asp:ImageButton ID="btnNovo" runat="server" />
                                                            <asp:Label ID="lblSequencia" runat="server" Text=""></asp:Label>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="cboTipoDeEndereco" runat="server" class="input-xs"></asp:DropDownList>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtEnderecoMeioDeContato" runat="server" class="form-control input-xs"></asp:TextBox>

                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" />

                                                        </div>
                                                        <div class="col-md-5">                                                           

                                                        </div>


                                                    </div>


                                                </div>


                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                         <asp:GridView ID="grdMeiosDeContato" runat="server" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="TIPODECONTATO" HeaderText="TIPO" />
                                                                    <asp:BoundField DataField="ENDCONTADO" HeaderText="DADO" />
                                                                    <asp:TemplateField></asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>                                                       

                                                    </div>


                                                </div>



                                            </fieldset>


                                              <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                        <input type="submit" name="btnConfirma" value="Confirmar" id="btnConfirma" class="btn btn-primary"/>
                                                        </div>                                                       

                                                    </div>


                                                </div>

                                            
                                              <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                        <input type="submit" name="btnConfirma" value="Confirmar" id="Submit1" class="btn btn-primary"/>
                                                        </div>                                                       

                                                    </div>


                                                </div>

                                            
                                              <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                        <input type="submit" name="btnConfirma" value="Confirmar" id="Submit2" class="btn btn-primary"/>
                                                        </div>                                                       

                                                    </div>


                                                </div>

                                            
                                              <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                        <input type="submit" name="btnConfirma" value="Confirmar" id="Submit3" class="btn btn-primary"/>
                                                        </div>                                                       

                                                    </div>


                                                </div>

                                            
                                              <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                        <input type="submit" name="btnConfirma" value="Confirmar" id="Submit4" class="btn btn-primary"/>
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

                        
                        <uc1:dtrPesquisa runat="server" ID="dtrPesquisa" />
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
    </form>
    <!--================================================== -->

    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
    <script data-pace-options='{ "restartOnRequestAfter": true }' src="js/plugin/pace/pace.min.js"></script>

    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        if (!window.jQuery) {
            document.write('<script src="js/libs/jquery-2.1.1.min.js"><\/script>');
        }
    </script>

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <script>
        if (!window.jQuery.ui) {
            document.write('<script src="js/libs/jquery-ui-1.10.3.min.js"><\/script>');
        }
    </script>

    <!-- IMPORTANT: APP CONFIG -->
    <script src="js/app.config.js"></script>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events-->
    <script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script>

    <!-- BOOTSTRAP JS -->
    <script src="js/bootstrap/bootstrap.min.js"></script>

    <!-- CUSTOM NOTIFICATION -->
    <script src="js/notification/SmartNotification.min.js"></script>

    <!-- JARVIS WIDGETS -->
    <script src="js/smartwidgets/jarvis.widget.min.js"></script>

    <!-- EASY PIE CHARTS -->
    <script src="js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js"></script>

    <!-- SPARKLINES -->
    <script src="js/plugin/sparkline/jquery.sparkline.min.js"></script>

    <!-- JQUERY VALIDATE -->
    <script src="js/plugin/jquery-validate/jquery.validate.min.js"></script>

    <!-- JQUERY MASKED INPUT -->
    <script src="js/plugin/masked-input/jquery.maskedinput.min.js"></script>

    <!-- JQUERY SELECT2 INPUT -->
    <script src="js/plugin/select2/select2.min.js"></script>

    <!-- JQUERY UI + Bootstrap Slider -->
    <script src="js/plugin/bootstrap-slider/bootstrap-slider.min.js"></script>

    <!-- browser msie issue fix -->
    <script src="js/plugin/msie-fix/jquery.mb.browser.min.js"></script>

    <!-- FastClick: For mobile devices -->
    <script src="js/plugin/fastclick/fastclick.min.js"></script>

    <!--[if IE 8]>

		<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>

		<![endif]-->

    <!-- Demo purpose only -->
    <script src="js/demo.min.js"></script>

    <!-- MAIN APP JS FILE -->
    <script src="js/app.min.js"></script>

    <!-- ENHANCEMENT PLUGINS : NOT A REQUIREMENT -->
    <!-- Voice command : plugin -->
    <script src="js/speech/voicecommand.min.js"></script>

    <!-- SmartChat UI : plugin -->
    <script src="js/smart-chat-ui/smart.chat.ui.min.js"></script>
    <script src="js/smart-chat-ui/smart.chat.manager.min.js"></script>

    <!-- PAGE RELATED PLUGIN(S) -->
    <script src="js/plugin/jquery-form/jquery-form.min.js"></script>


    <script src="js/plugin/datatables/jquery.dataTables.min.js"></script>
    <script src="js/plugin/datatables/dataTables.colVis.min.js"></script>
    <script src="js/plugin/datatables/dataTables.tableTools.min.js"></script>
    <script src="js/plugin/datatables/dataTables.bootstrap.min.js"></script>
    <script src="js/plugin/datatable-responsive/datatables.responsive.min.js"></script>


    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!

        $(document).ready(function () {

            pageSetUp();


            /* BASIC ;*/
            var responsiveHelper_dt_basic = undefined;
            var responsiveHelper_datatable_fixed_column = undefined;
            var responsiveHelper_datatable_col_reorder = undefined;
            var responsiveHelper_datatable_tabletools = undefined;

            var breakpointDefinition = {
                tablet: 1024,
                phone: 480
            };

            $('#dt_basic').dataTable({
                "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                    "t" +
                    "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
                "autoWidth": true,
                "oLanguage": {
                    "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
                },
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper_dt_basic) {
                        responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($('#dt_basic'), breakpointDefinition);
                    }
                },
                "rowCallback": function (nRow) {
                    responsiveHelper_dt_basic.createExpandIcon(nRow);
                },
                "drawCallback": function (oSettings) {
                    responsiveHelper_dt_basic.respond();
                }
            });

            /* END BASIC */

            /* COLUMN FILTER  */
            var otable = $('#datatable_fixed_column').DataTable({
                //"bFilter": false,
                //"bInfo": false,
                //"bLengthChange": false
                //"bAutoWidth": false,
                //"bPaginate": false,
                //"bStateSave": true // saves sort state using localStorage
                "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6 hidden-xs'f><'col-sm-6 col-xs-12 hidden-xs'<'toolbar'>>r>" +
                        "t" +
                        "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
                "autoWidth": true,
                "oLanguage": {
                    "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
                },
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper_datatable_fixed_column) {
                        responsiveHelper_datatable_fixed_column = new ResponsiveDatatablesHelper($('#datatable_fixed_column'), breakpointDefinition);
                    }
                },
                "rowCallback": function (nRow) {
                    responsiveHelper_datatable_fixed_column.createExpandIcon(nRow);
                },
                "drawCallback": function (oSettings) {
                    responsiveHelper_datatable_fixed_column.respond();
                }

            });

            // custom toolbar
            $("div.toolbar").html('<div class="text-right"><img src="img/logo.png" alt="SmartAdmin" style="width: 111px; margin-top: 3px; margin-right: 10px;"></div>');

            // Apply the filter
            $("#datatable_fixed_column thead th input[type=text]").on('keyup change', function () {

                otable
                    .column($(this).parent().index() + ':visible')
                    .search(this.value)
                    .draw();

            });
            /* END COLUMN FILTER */

            /* COLUMN SHOW - HIDE */
            $('#datatable_col_reorder').dataTable({
                "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'C>r>" +
                        "t" +
                        "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
                "autoWidth": true,
                "oLanguage": {
                    "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
                },
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper_datatable_col_reorder) {
                        responsiveHelper_datatable_col_reorder = new ResponsiveDatatablesHelper($('#datatable_col_reorder'), breakpointDefinition);
                    }
                },
                "rowCallback": function (nRow) {
                    responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
                },
                "drawCallback": function (oSettings) {
                    responsiveHelper_datatable_col_reorder.respond();
                }
            });

            /* END COLUMN SHOW - HIDE */

            /* TABLETOOLS */
            $('#datatable_tabletools').dataTable({

                // Tabletools options: 
                //   https://datatables.net/extensions/tabletools/button_options
                "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                        "t" +
                        "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
                "oLanguage": {
                    "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
                },
                "oTableTools": {
                    "aButtons": [
                    "copy",
                    "csv",
                    "xls",
                       {
                           "sExtends": "pdf",
                           "sTitle": "SmartAdmin_PDF",
                           "sPdfMessage": "SmartAdmin PDF Export",
                           "sPdfSize": "letter"
                       },
                       {
                           "sExtends": "print",
                           "sMessage": "Generated by SmartAdmin <i>(press Esc to close)</i>"
                       }
                    ],
                    "sSwfPath": "js/plugin/datatables/swf/copy_csv_xls_pdf.swf"
                },
                "autoWidth": true,
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper_datatable_tabletools) {
                        responsiveHelper_datatable_tabletools = new ResponsiveDatatablesHelper($('#datatable_tabletools'), breakpointDefinition);
                    }
                },
                "rowCallback": function (nRow) {
                    responsiveHelper_datatable_tabletools.createExpandIcon(nRow);
                },
                "drawCallback": function (oSettings) {
                    responsiveHelper_datatable_tabletools.respond();
                }
            });

            /* END TABLETOOLS */

        })
        var $checkoutForm = $('#checkout-form').validate({
            // Rules for form validation
            rules: {
                fname: {
                    required: true
                },
                lname: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true
                },
                country: {
                    required: true
                },
                city: {
                    required: true
                },
                code: {
                    required: true,
                    digits: true
                },
                address: {
                    required: true
                },
                name: {
                    required: true
                },
                card: {
                    required: true,
                    creditcard: true
                },
                cvv: {
                    required: true,
                    digits: true
                },
                month: {
                    required: true
                },
                year: {
                    required: true,
                    digits: true
                }
            },

            // Messages for form validation
            messages: {
                fname: {
                    required: 'Please enter your first name'
                },
                lname: {
                    required: 'Please enter your last name'
                },
                email: {
                    required: 'Please enter your email address',
                    email: 'Please enter a VALID email address'
                },
                phone: {
                    required: 'Please enter your phone number'
                },
                country: {
                    required: 'Please select your country'
                },
                city: {
                    required: 'Please enter your city'
                },
                code: {
                    required: 'Please enter code',
                    digits: 'Digits only please'
                },
                address: {
                    required: 'Please enter your full address'
                },
                name: {
                    required: 'Please enter name on your card'
                },
                card: {
                    required: 'Please enter your card number'
                },
                cvv: {
                    required: 'Enter CVV2',
                    digits: 'Digits only'
                },
                month: {
                    required: 'Select month'
                },
                year: {
                    required: 'Enter year',
                    digits: 'Digits only please'
                }
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        var $registerForm = $("#smart-form-register").validate({

            // Rules for form validation
            rules: {
                username: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                password: {
                    required: true,
                    minlength: 3,
                    maxlength: 20
                },
                passwordConfirm: {
                    required: true,
                    minlength: 3,
                    maxlength: 20,
                    equalTo: '#password'
                },
                firstname: {
                    required: true
                },
                lastname: {
                    required: true
                },
                gender: {
                    required: true
                },
                terms: {
                    required: true
                }
            },

            // Messages for form validation
            messages: {
                login: {
                    required: 'Please enter your login'
                },
                email: {
                    required: 'Please enter your email address',
                    email: 'Please enter a VALID email address'
                },
                password: {
                    required: 'Please enter your password'
                },
                passwordConfirm: {
                    required: 'Please enter your password one more time',
                    equalTo: 'Please enter the same password as above'
                },
                firstname: {
                    required: 'Please select your first name'
                },
                lastname: {
                    required: 'Please select your last name'
                },
                gender: {
                    required: 'Please select your gender'
                },
                terms: {
                    required: 'You must agree with Terms and Conditions'
                }
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        var $reviewForm = $("#review-form").validate({
            // Rules for form validation
            rules: {
                name: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                review: {
                    required: true,
                    minlength: 20
                },
                quality: {
                    required: true
                },
                reliability: {
                    required: true
                },
                overall: {
                    required: true
                }
            },

            // Messages for form validation
            messages: {
                name: {
                    required: 'Please enter your name'
                },
                email: {
                    required: 'Please enter your email address',
                    email: '<i class="fa fa-warning"></i><strong>Please enter a VALID email addres</strong>'
                },
                review: {
                    required: 'Please enter your review'
                },
                quality: {
                    required: 'Please rate quality of the product'
                },
                reliability: {
                    required: 'Please rate reliability of the product'
                },
                overall: {
                    required: 'Please rate the product'
                }
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        var $commentForm = $("#comment-form").validate({
            // Rules for form validation
            rules: {
                name: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                url: {
                    url: true
                },
                comment: {
                    required: true
                }
            },

            // Messages for form validation
            messages: {
                name: {
                    required: 'Enter your name',
                },
                email: {
                    required: 'Enter your email address',
                    email: 'Enter a VALID email'
                },
                url: {
                    email: 'Enter a VALID url'
                },
                comment: {
                    required: 'Please enter your comment'
                }
            },

            // Ajax form submition
            submitHandler: function (form) {
                $(form).ajaxSubmit({
                    success: function () {
                        $("#comment-form").addClass('submited');
                    }
                });
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        var $contactForm = $("#contact-form").validate({
            // Rules for form validation
            rules: {
                name: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                message: {
                    required: true,
                    minlength: 10
                }
            },

            // Messages for form validation
            messages: {
                name: {
                    required: 'Please enter your name',
                },
                email: {
                    required: 'Please enter your email address',
                    email: 'Please enter a VALID email address'
                },
                message: {
                    required: 'Please enter your message'
                }
            },

            // Ajax form submition
            submitHandler: function (form) {
                $(form).ajaxSubmit({
                    success: function () {
                        $("#contact-form").addClass('submited');
                    }
                });
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        var $loginForm = $("#login-form").validate({
            // Rules for form validation
            rules: {
                email: {
                    required: true,
                    email: true
                },
                password: {
                    required: true,
                    minlength: 3,
                    maxlength: 20
                }
            },

            // Messages for form validation
            messages: {
                email: {
                    required: 'Please enter your email address',
                    email: 'Please enter a VALID email address'
                },
                password: {
                    required: 'Please enter your password'
                }
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        var $orderForm = $("#order-form").validate({
            // Rules for form validation
            rules: {
                name: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true
                },
                interested: {
                    required: true
                },
                budget: {
                    required: true
                }
            },

            // Messages for form validation
            messages: {
                name: {
                    required: 'Please enter your name'
                },
                email: {
                    required: 'Please enter your email address',
                    email: 'Please enter a VALID email address'
                },
                phone: {
                    required: 'Please enter your phone number'
                },
                interested: {
                    required: 'Please select interested service'
                },
                budget: {
                    required: 'Please select your budget'
                }
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        // START AND FINISH DATE
        $('#startdate').datepicker({
            dateFormat: 'dd.mm.yy',
            prevText: '<i class="fa fa-chevron-left"></i>',
            nextText: '<i class="fa fa-chevron-right"></i>',
            onSelect: function (selectedDate) {
                $('#finishdate').datepicker('option', 'minDate', selectedDate);
            }
        });

        $('#finishdate').datepicker({
            dateFormat: 'dd.mm.yy',
            prevText: '<i class="fa fa-chevron-left"></i>',
            nextText: '<i class="fa fa-chevron-right"></i>',
            onSelect: function (selectedDate) {
                $('#startdate').datepicker('option', 'maxDate', selectedDate);
            }
        });





    </script>

    <!-- Your GOOGLE ANALYTICS CODE Below -->
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-XXXXXXXX-X']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script');
            ga.type = 'text/javascript';
            ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0];
            s.parentNode.insertBefore(ga, s);
        })();

    </script>
</body>
</html>
