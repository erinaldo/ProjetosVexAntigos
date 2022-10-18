<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEB0012.aspx.cs" Inherits="Sistecno.UI.Web.WEB0012" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sietecno</title>
    <meta charset="utf-8" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">-->

    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />

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


</head>



<body class="">
    <form id="form1" runat="server">

        <!-- MAIN CONTENT -->
        <div id="content">

            <div class="row">
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                    <h3 class="page-title txt-color-blueDark"><i class="fa fa-random "></i>IMPORTAÇÃO DE XML <span></span></h3>
                </div>
            </div>

            <!-- widget grid -->
            <section id="widget-grid" class="">

                <!-- row -->
                <div class="row">
                    <article class="col-sm-3">
                    </article>

                    <!-- NEW WIDGET START -->
                    <article class="col-sm-5">
                        <%--						<article class="col-xs-5 col-sm-5 col-md-5 col-lg-5">--%>

                        <!-- Widget ID (each widget will need unique ID)-->
                        <div class="jarviswidget jarviswidget-color-blueDark jarviswidget-sortable" id="wid-id-0" data-widget-editbutton="false">
                            <!-- widget options:
					usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
					
					data-widget-colorbutton="false"	
					data-widget-editbutton="false"
					data-widget-togglebutton="false"
					data-widget-deletebutton="false"
					data-widget-fullscreenbutton="false"
					data-widget-custombutton="false"
					data-widget-collapsed="true" 
					data-widget-sortable="false"
					
				-->
                            <header>
                                <span class="widget-icon"><i class="fa fa-random "></i></span>
                                <h2>IMPORTAÇÃO</h2>

                            </header>

                            <!-- widget div-->
                            <div>

                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropdown edit box -->
                                    <input type="text" />
                                </div>

                                <div class="form-group">
                                    <div class="row" style="min-height: 250px">
                                        <div class="col-md-12 selectContainer has-feedback">                                        

                                            <asp:FileUpload ID="FileUpload1" runat="server" />

                                        </div>
                                    </div>

                                 
                                    <div class="row">

                                        <div class="col-md-12" style="text-align: right; margin-top: 25px">
                                            <a href="javascript:$('#<%=FileUpload1.ClientID%>').fileUploadStart()" class="btn btn-primary">Confirma</a>
                                            <%--<asp:Button ID="btnConfirma" runat="server" Text="Confirmar" class="btn btn-primary" OnClientClick="javascript:$('#<%=FileUpload1.ClientID%>').fileUploadStart()" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <!-- end widget content -->

                        </div>
                        <!-- end widget div -->
                    </article>
                </div>
                <!-- end widget -->


            </section>

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

    <script src="js/jquery.uploadify.min.js"></script>
    <script src="js/jquery.uploadify.js"></script>
    <link href="css/uploadify.css" rel="stylesheet" />


    <script type="text/javascript">
        $(window).load(
         function () {
             $("#<%=FileUpload1.ClientID %>").fileUpload({
                 'uploader': 'js/uploader.swf',
                 'cancelImg': 'cancel.png',
                 'buttonText': 'Selecionar',
                 'script': 'Upload.ashx',
                 'folder': 'TMP',
                 'fileDesc': 'Arquivos XML',
                 'fileExt': '*.xml',
                 'multi': true,
                 'auto': false
             });
         }
        );

    </script>
    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!



        $(document).ready(function () {
            pageSetUp();
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
