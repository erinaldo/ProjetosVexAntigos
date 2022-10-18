<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="Sistecno.UI.Web.frmLogin" %>


<!DOCTYPE html>

<html lang="pt-br" id="extr-page">
<head> 
    <meta charset="utf-8">
    <title>Bem Vindo  - Login</title>
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">

    <!-- #CSS Links -->
    <!-- Basic Styles -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" media="screen" href="css/font-awesome.min.css">

    <!-- SmartAdmin Styles : Caution! DO NOT change the order -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production-plugins.min.css">
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production.min.css">
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-skins.min.css">

    <!-- SmartAdmin RTL Support -->
    <%--<link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-rtl.min.css">--%>

    <!-- We recommend you use "your_style.css" to override SmartAdmin
		     specific styles this will also ensure you retrain your customization with each SmartAdmin update.
		<link rel="stylesheet" type="text/css" media="screen" href="css/your_style.css"> -->

    <!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/demo.min.css">
</head>

<body>
    <form id="form1" runat="server" class="smart-form client-form" >
        <div>
            <header>
                Login
            </header>

            <fieldset>

                <section>
                    <label class="label">E-mail</label>
                    <label class="input">
                        <i class="icon-append fa fa-user"></i>
                        <%--<input type="email" name="email">--%>
                        <asp:TextBox ID="txtEmail" runat="server" type="email" name="email"></asp:TextBox>
                        <b class="tooltip tooltip-top-right"><i class="fa fa-user txt-color-teal"></i>Entre com seu e-mail</b></label>
                </section>

                <section>
                    <label class="label">Senha</label>
                    <label class="input">
                        <i class="icon-append fa fa-lock"></i>
                        <%--<input type="password" name="password">--%>
                        <asp:TextBox ID="txtSenha" runat="server" type="password" name="password"></asp:TextBox>
                        <b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i>Entre com a senha</b>
                    </label>
                    <div class="note">
                        <a href="forgotpassword.html">Esqueceu a senha?</a>
                    </div>
                </section>

                <section>
                    <label class="checkbox">
                        <input type="checkbox" name="remember" checked="">
                        <i></i>Manter Logado</label>
                </section>
            </fieldset>
            <footer>
                <asp:Button ID="btnLogar" runat="server" Text="Logar" class="btn btn-primary" OnClick="btnLogar_Click" />
                <asp:Label ID="lblErro" runat="server"></asp:Label>
            </footer>
        </div>
 </form>


    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
    <script src="js/plugin/pace/pace.min.js"></script>

    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script> if (!window.jQuery) { document.write('<script src="js/libs/jquery-2.1.1.min.js"><\/script>'); } </script>

    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <script> if (!window.jQuery.ui) { document.write('<script src="js/libs/jquery-ui-1.10.3.min.js"><\/script>'); } </script>

    <!-- IMPORTANT: APP CONFIG -->
    <script src="js/app.config.js"></script>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events 		
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- BOOTSTRAP JS -->
    <script src="js/bootstrap/bootstrap.min.js"></script>

    <!-- JQUERY VALIDATE -->
    <script src="js/plugin/jquery-validate/jquery.validate.min.js"></script>

    <!-- JQUERY MASKED INPUT -->
    <script src="js/plugin/masked-input/jquery.maskedinput.min.js"></script>

    <!--[if IE 8]>
			
			<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>
			
		<![endif]-->

    <!-- MAIN APP JS FILE -->
    <script src="js/app.min.js"></script>

    <script type="text/javascript">
        runAllForms();

        $(function () {
            // Validation
            $("#login-form").validate({
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
                        required: 'Informe seu e-mail',
                        email: 'Informe o e-mail válido'
                    },
                    password: {
                        required: 'Insira a sua senha'
                    }
                },

                // Do not change code below
                errorPlacement: function (error, element) {
                    error.insertAfter(element.parent());
                }
            });
        });
    </script>

</body>
</html>
