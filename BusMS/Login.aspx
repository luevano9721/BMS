<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BusManagementSystem.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>BMS Login</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="css/matrix-login.css" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700,800' rel='stylesheet' type='text/css'>
    <script src="/js/jquery.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>

    <style type="text/css">
        .messagealert {
            width: 50%;
            position: fixed;
            top: 10px;
            z-index: 100000;
            padding: 0px;
            font-size: 15px;
            right: 50px;
        }

        #loading-image {
            transform: translate(-50%, -50%);
            position: fixed;
            top: 50%;
            left: 50%;
            z-index: 1000;
            overflow: visible;
            visibility: visible;
            background: rgba(0,0,0,0) url('images/ring.gif') no-repeat;
        }

        #overlay {
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background-color: #004d99;
            -moz-opacity: 0.80;
            opacity: .80; /* Standards Compliant Browsers */
            filter: alpha(opacity=80); /* IE 7 and Earlier */
            /* Next 2 lines IE8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=80)";
            filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=80);
            z-index: 1001;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#overlay").hide();
        });
        function DisableButton() {            
            document.getElementById("btnSubmit").disabled = true;
            ShowProgress(true);
        } window.onbeforeunload = DisableButton;

        function ShowProgress(show) {
            if (show) {
                $("#loading-image").show('fast');
                $("#overlay").show('fast');
            }
            else {
                $("#loading-image").hide('fast');
                $("#overlay").hide('fast');
            }
        }
    </script>
    <script type="text/javascript">


        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
            window.setTimeout(function () {
                $(".alert").fadeTo(1500, 0).slideUp(500, function () {
                    $(this).remove();
                });
            }, 5000);
        }

    </script>

</head>
<body>
    <div id="overlay">
        <img id="loading-image" src="images/ring.gif" alt="Loading..." style="display: none" />
    </div>

    <div class="messagealert" id="alert_container">
    </div>

    <div id="loginbox">
        <form id="loginform" class="form-vertical" runat="server">

            <div class="control-group normal_text">
                <h3>
                    <img src="images/loginlogo.png" alt="Logo" /></h3>
            </div>
            <div class="control-group">
                <div class="controls">
                    <div class="main_input_box">
                        <span class="add-on bg_lg"><i class="icon-user"></i></span>
                        <asp:TextBox ID="UserIdText" runat="server" placeholder="Username" MaxLength="50" TabIndex="1"
                            CssClass="span11" ToolTip="Ingres el ID de usuario" ValidationGroup="validate"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <div class="main_input_box">
                        <span class="add-on bg_ly"><i class="icon-lock"></i></span>
                        <asp:TextBox ID="PasswordText" CssClass="span11" runat="server" MaxLength="32" TextMode="Password" ToolTip="Pon la contraseña" placeholder="Password" TabIndex="2"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="form-actions" style="border-top: none">
                <span class="pull-left"><a href="#" class="flip-link btn btn-info" id="to-recover">
                    <span class="icon" style="color: white;"><i class="icon-question-sign"></i></span>
                    <asp:Label runat="server" CssClass="control-label" ForeColor="White" TabIndex="4"> <asp:Label ID="lbl_login_LostPassword" runat="server" Text="Contraseña perdida"></asp:Label></asp:Label>

                </a></span>
                <span class="pull-right">
                    <asp:Button ID="LoginButton" class="btn btn-success" runat="server" Text="Entrar" TabIndex="3"
                        OnClick="LoginButton_Click" ValidationGroup="validate"></asp:Button></span>
            </div>
        </form>
        <form id="recoverform" class="form-vertical" action="RecoverPass.aspx" method="get">
            <p class="normal_text">
               
               Ingrese su ID de usuario y dirección de correo electrónico y a continuación le enviaremos instrucciones sobre cómo recuperar una contraseña.
</p>
            <div class="controls">
                <div class="main_input_box">
                    <span class="add-on bg_lo"><i class="icon-user"></i></span>
                    <input type="text" placeholder="ID de usuario" name="tbUser" required />
                </div>
            </div>
            <div class="controls">
                <div class="main_input_box">
                    <span class="add-on bg_lo"><i class="icon-envelope"></i></span>
                    <input type="email" placeholder="Correo Electronico" name="tbEmail" required />
                </div>
            </div>

            <div class="form-actions">
                <span class="pull-left"><a href="#" class="flip-link btn btn-success" id="to-login">&laquo; Regresesar a iniciar sesión</a></span>
                <span class="pull-right">
                    <input type="submit" value="Recuperar" name="submit" id="btnSubmit" class="btn btn-info" /></span>
            </div>
        </form>
    </div>

    <script src="js/jquery.min.js"></script>
    <script src="js/matrix.login.js"></script>

</body>
</html>





