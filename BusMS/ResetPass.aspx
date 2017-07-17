<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPass.aspx.cs" Inherits="BusManagementSystem.ResetPass" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>BMS Reset Password</title>
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

        body {
            margin-top: 5%;
        }

       
    </style>
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
                        <h5 style="color: white; margin: 0px;">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>Restablecer su contraseña</h5>
                        <br />

                        <span class="add-on bg_lg"><i class="icon-key"></i></span>
                        <asp:TextBox ID="currentPassword" runat="server" placeholder="Actual contraseña" MaxLength="50" TabIndex="1"
                            CssClass="span11" ToolTip="Introduzca la contraseña del correo electrónico" ValidationGroup="validate"></asp:TextBox>
                        <h6 style="color: white; margin: 0px;">La contraseña actual es la contraseña que enviamos previamente a su correo electrónico</h6>
                        <asp:RequiredFieldValidator runat="server" Display="dynamic"
                            ControlToValidate="currentPassword" ForeColor="#cc0000"
                            ErrorMessage="Se requiere la contraseña enviada al correo electrónico"
                            EnableClientScript="true"
                            SetFocusOnError="true"
                            CssClass="span6 m-wrap"
                            Text=" "
                            ValidationGroup="ValidatePassword"> 
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <div class="main_input_box">
                        <span class="add-on bg_db"><i class="icon-lock"></i></span>
                        <asp:TextBox ID="newPassword" CssClass="span11" runat="server" MaxLength="32" TextMode="Password" ToolTip="Ponga su nueva contraseña" placeholder="Nueva contraseña" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" Display="dynamic"
                            ControlToValidate="newPassword" ForeColor="#f4f4f4"
                            ErrorMessage="Se requiere una nueva contraseña."
                            EnableClientScript="true"
                            SetFocusOnError="true"
                            CssClass="span4 m-wrap"
                            Text=" "
                            ValidationGroup="ValidatePassword"> 
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" Display="dynamic"
                            ControlToValidate="newPassword"
                            ErrorMessage="La contraseña debe tener al menos una letra mayúscula, una letra minúscula y tener de 6 a 10 letras"
                            ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$" ForeColor="#f4f4f4"
                            EnableClientScript="true"
                            SetFocusOnError="true"
                            CssClass="span4 m-wrap"
                            Text=" "
                            ValidationGroup="ValidatePassword" />
                    </div>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <div class="main_input_box">
                        <span class="add-on bg_db"><i class="icon-lock"></i></span>
                        <asp:TextBox ID="confirmPassword" runat="server" placeholder="Confirmar contraseña" MaxLength="50" TabIndex="3" TextMode="Password"
                            CssClass="span11" ToolTip="Confirmar nueva contraseña" ValidationGroup="validate"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" Display="dynamic"
                            ControlToValidate="confirmPassword" ForeColor="#f4f4f4"
                            ErrorMessage="Confirme la contraseña requerida."
                            EnableClientScript="true"
                            SetFocusOnError="true"
                            CssClass="span4 m-wrap"
                            Text=" "
                            ValidationGroup="ValidatePassword"> 
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" Display="dynamic"
                            ControlToValidate="confirmPassword"
                            ErrorMessage="La contraseña debe tener al menos una letra mayúscula, una letra minúscula y tener de 6 a 10 letras"
                            ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$" ForeColor="#f4f4f4"
                            EnableClientScript="true"
                            SetFocusOnError="true"
                            CssClass="span4 m-wrap"
                            Text=" "
                            ValidationGroup="ValidatePassword" />
                        <asp:CompareValidator runat="server" Display="dynamic"
                            ControlToValidate="confirmPassword"
                            ControlToCompare="newPassword"
                            ErrorMessage="Passwords do not match."
                            Text=" "
                            ForeColor="#f4f4f4"
                            EnableClientScript="true"
                            SetFocusOnError="true"
                            CssClass="span4 m-wrap"
                            ValidationGroup="ValidatePassword" />
                    </div>
                </div>
            </div>
            <div class="form-actions" style="border-top: none">
                <span class="pull-right">
                    <asp:Button ID="LoginButton" class="btn btn-success" runat="server" Text="Cambiar contraseña y entra" TabIndex="3"
                        ValidationGroup="ValidatePassword" OnClick="LoginButton_Click"></asp:Button></span>

            </div>
            <asp:ValidationSummary
                ID="ValidationSummary2"
                runat="server"
                HeaderText="Se producen los siguientes errores....."
                ShowMessageBox="false"
                DisplayMode="BulletList"
                BackColor="Snow"
                ForeColor="Red"
                Font-Italic="true" />
        </form>
    </div>
    <script src="js/jquery.min.js"></script>
    <script src="js/matrix.login.js"></script>

</body>
</html>

