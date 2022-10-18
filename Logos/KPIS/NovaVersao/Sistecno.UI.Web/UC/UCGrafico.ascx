<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGrafico.ascx.cs" Inherits="Sistecno.UI.Web.UC.UCGrafico" %>
<html>
<head>
    <meta charset="utf-8">
    <!--<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">-->

    <title>SmartAdmin </title>
    <meta name="description" content="">
    <meta name="author" content="">

    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">

    <!-- Basic Styles -->
    <link rel="stylesheet" type="text/css" media="screen" href="../css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" media="screen" href="../css/font-awesome.min.css">

    <!-- SmartAdmin Styles : Caution! DO NOT change the order -->
    <link rel="stylesheet" type="text/css" media="screen" href="../css/smartadmin-production-plugins.min.css">
    <link rel="stylesheet" type="text/css" media="screen" href="../css/smartadmin-production.min.css">
    <link rel="stylesheet" type="text/css" media="screen" href="../css/smartadmin-skins.min.css">

    <!-- SmartAdmin RTL Support -->
    <link rel="stylesheet" type="text/css" media="screen" href="../css/smartadmin-rtl.min.css">

    <!-- We recommend you use "your_style.css" to override SmartAdmin
		     specific styles this will also ensure you retrain your customization with each SmartAdmin update.
		<link rel="stylesheet" type="text/css" media="screen" href="../css/your_style.css"> -->

    <!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
    <link rel="stylesheet" type="text/css" media="screen" href="../css/demo.min.css">

    <!-- FAVICONS -->
    <link rel="shortcut icon" href="img/favicon/favicon.ico" type="image/x-icon">
    <link rel="icon" href="img/favicon/favicon.ico" type="image/x-icon">

    <!-- GOOGLE FONT -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">

    <!-- Specifying a Webpage Icon for Web Clip 
			 Ref: https://developer.apple.com/library/ios/documentation/AppleApplications/Reference/SafariWebContent/ConfiguringWebApplications/ConfiguringWebApplications.html -->
    <link rel="apple-touch-icon" href="img/splash/sptouch-icon-iphone.png">
    <link rel="apple-touch-icon" sizes="76x76" href="img/splash/touch-icon-ipad.png">
    <link rel="apple-touch-icon" sizes="120x120" href="img/splash/touch-icon-iphone-retina.png">
    <link rel="apple-touch-icon" sizes="152x152" href="img/splash/touch-icon-ipad-retina.png">

    <!-- iOS web-app metas : hides Safari UI Components and Changes Status Bar Appearance -->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">

    <!-- Startup image for web apps -->
    <link rel="apple-touch-startup-image" href="img/splash/ipad-landscape.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)">
    <link rel="apple-touch-startup-image" href="img/splash/ipad-portrait.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)">
    <link rel="apple-touch-startup-image" href="img/splash/iphone.png" media="screen and (max-device-width: 320px)">
</head>

<body>
    <div class="row">

        <div class="col-sm-12">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
    </div>
  <%--  <div class="row">

    <div class="col-sm-12">

        <div class="well">
            <ul class="list-inline">
                <li>&nbsp;&nbsp;&nbsp;
									<div class="easy-pie-chart txt-color-red easyPieChart" data-percent="50" data-size="180" data-pie-size="50">
                                        <span class="percent percent-sign txt-color-red font-xl semi-bold">50</span>
                                        <canvas height="180" width="180"></canvas>
                                    </div>
                    &nbsp;&nbsp;&nbsp;
                </li>
                <li>&nbsp;&nbsp;&nbsp;
									<div class="easy-pie-chart txt-color-blue easyPieChart" data-percent="36" data-pie-size="180">
                                        <span class="percent percent-sign txt-color-blue font-xl semi-bold">36</span>
                                        <canvas height="180" width="180"></canvas>
                                    </div>
                    &nbsp;&nbsp;&nbsp;
                </li>
                <li>&nbsp;&nbsp;&nbsp;
									<div class="easy-pie-chart txt-color-pinkDark easyPieChart" data-percent="46" data-pie-size="160">
                                        <span class="percent percent-sign txt-color-pinkDark font-lg semi-bold">46</span>
                                        <canvas height="160" width="160"></canvas>
                                    </div>
                    &nbsp;&nbsp;&nbsp;
                </li>
                <li>&nbsp;&nbsp;&nbsp;
									<div class="easy-pie-chart txt-color-greenLight easyPieChart" data-percent="56" data-pie-size="110">
                                        <span class="percent percent-sign txt-color-greenLight font-md">56</span>
                                        <canvas height="110" width="110"></canvas>
                                    </div>
                    &nbsp;&nbsp;&nbsp;
                </li>
                <li>&nbsp;&nbsp;&nbsp;
									<div class="easy-pie-chart txt-color-orange easyPieChart" data-percent="66" data-pie-size="60">
                                        <span class="percent percent-sign txt-color-orange">66</span>
                                        <canvas height="60" width="60"></canvas>
                                    </div>
                    &nbsp;&nbsp;&nbsp;
                </li>
                <li>&nbsp;&nbsp;&nbsp;
									<div class="easy-pie-chart txt-color-darken easyPieChart" data-percent="76" data-pie-size="45">
                                        <span class="percent percent-sign font-sm">76</span>
                                        <canvas height="45" width="45"></canvas>
                                    </div>
                    &nbsp;&nbsp;&nbsp;
                </li>
                <li>&nbsp;&nbsp;&nbsp;
									<div class="easy-pie-chart txt-color-blue easyPieChart" data-percent="86" data-pie-size="35">
                                        <span class="percent percent-sign font-xs">86</span>
                                        <canvas height="35" width="35"></canvas>
                                    </div>
                    &nbsp;&nbsp;&nbsp;
                </li>
            </ul>

        </div>

    </div>
</div>--%>

    <br />

    <script src="../js/libs/jquery-2.1.1.min.js"></script>




    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
    <script data-pace-options='{ "restartOnRequestAfter": true }' src="../js/plugin/pace/pace.min.js"></script>

    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        if (!window.jQuery) {
            document.write('<script src="../js/libs/jquery-2.1.1.min.js"><\/script>');
        }
    </script>

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <script>
        if (!window.jQuery.ui) {
            document.write('<script src="../js/libs/jquery-ui-1.10.3.min.js"><\/script>');
        }
    </script>

    <!-- IMPORTANT: APP CONFIG -->
    <script src="../js/app.config.js"></script>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events-->
    <script src="../js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script>

    <!-- BOOTSTRAP JS -->
    <script src="../js/bootstrap/bootstrap.min.js"></script>

    <!-- CUSTOM NOTIFICATION -->
    <script src="../js/notification/SmartNotification.min.js"></script>

    <!-- JARVIS WIDGETS -->
    <script src="../js/smartwidgets/jarvis.widget.min.js"></script>

    <!-- EASY PIE CHARTS -->
    <script src="../js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js"></script>

    <!-- SPARKLINES -->
    <script src="../js/plugin/sparkline/jquery.sparkline.min.js"></script>

    <!-- JQUERY VALIDATE -->
    <script src="../js/plugin/jquery-validate/jquery.validate.min.js"></script>

    <!-- JQUERY MASKED INPUT -->
    <script src="../js/plugin/masked-input/jquery.maskedinput.min.js"></script>

    <!-- JQUERY SELECT2 INPUT -->
    <script src="../js/plugin/select2/select2.min.js"></script>

    <!-- JQUERY UI + Bootstrap Slider -->
    <script src="../js/plugin/bootstrap-slider/bootstrap-slider.min.js"></script>

    <!-- browser msie issue fix -->
    <script src="../js/plugin/msie-fix/jquery.mb.browser.min.js"></script>

    <!-- FastClick: For mobile devices -->
    <script src="../js/plugin/fastclick/fastclick.min.js"></script>

    <!--[if IE 8]>

		<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>

		<![endif]-->

    <!-- Demo purpose only -->
    <script src="../js/demo.min.js"></script>

    <!-- MAIN APP JS FILE -->
    <script src="../js/app.min.js"></script>

    <!-- ENHANCEMENT PLUGINS : NOT A REQUIREMENT -->
    <!-- Voice command : plugin -->
    <script src="../js/speech/voicecommand.min.js"></script>

    <!-- SmartChat UI : plugin -->
    <script src="../js/smart-chat-ui/smart.chat.ui.min.js"></script>
    <script src="../js/smart-chat-ui/smart.chat.manager.min.js"></script>

    <script type="text/javascript">
        // DO NOT REMOVE : GLOBAL FUNCTIONS!
        $(document).ready(function () {
            pageSetUp();
        })


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
