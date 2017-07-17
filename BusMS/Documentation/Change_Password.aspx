<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Change_Password.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bus Management System</title>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery.ui.custom.js"></script>
    <script src="../js/matrix.js"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="~/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="~/css/matrix-style.css" />
    <link rel="stylesheet" href="~/css/matrix-media.css" />
    <link href="~/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700,800' rel='stylesheet' type='text/css' />

    <style>
        body {
            height: auto;
        }
    </style>


</head>
<body>
    <form id="frmHelp" runat="server">
        <div class="content">
            <div class=" content-header">
                <span class="pull-right">05/18/2017
                </span>
                <h4>Cambio de contraseña del usuario</h4>
                <div class="text">
                    Esta página te permite cambiar la contraseña del usuario, así como la fecha de expiración de la misma.
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce la página</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Change_password/Change_password.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Change_password/Change_password.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    La página esta divida en 3 secciones:
                                    <li><b>Seleccionar usuario:</b> Te proporciona toda la lista de usuarios registrados en la aplicacion y que puedes seleccionar.
                                    </li>
                                    <li><b>Cambiar contraseña:</b> Te permite ingresar la informacion correspondiente para el cambio de contraseña.
                                    </li>
                                    <li><b>Detalles de usuario:</b> Muestra la informacion actual del usuario.
                                    </li>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-user"></i></span>
                                <h5>Seleccionar usuario</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">

                                <div class="text">
                                    Debido a que esta página es únicamente accesible para el administrador, muestra todos los usuarios registrados en la aplicación
                                    Sin importar su rol o perfil. El combo de opciones te permite buscar por nombre o ID de usuario. Basta con presionar
                                    el botón
                                    <img class="soloImg" src="d_images/Change_password/select_button.png" />
                                    para poder realizar cambios sobre un usuario en específico.
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Change_password/select_user.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Change_password/select_user.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                    Cuando esto sucede, podremos observar que a nuestro lado derecho el widget de detalles de usuario muestra la información
                                    correspondiente al usuario:
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Change_password/user_details.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Change_password/user_details.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-key"></i></span>
                                <h5>Cambiar contraseña</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Change_password/password.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Change_password/password.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Una vez que se ha seleccionado un usuario, los siguientes campos se habilitan:
                                     <li><b>Nueva contraseña:</b> Aquí se especifica la nueva contraseña que se desea utilizar para el usuario.
                                     </li>
                                    <li><b>Nueva contraseña:</b> Aquí se especifica la nueva contraseña que se desea utilizar para el usuario.
                                    </li>
                                    <li><b>Fecha de expiración:</b> Si se habilita este campo y se pone una fecha, este usuario se verá obligado a cambiar 
                                        la contraseña una vez que se llegue a la fecha indicada.
                                    </li>

                                </div>

                                <div class="text">
                                    Al presionar el botón  
                                    <img class="soloImg" src="d_images/Change_password/save.png" />
                                    se realizara el cambio de contraseña , mostrando el siguiente mensaje de exito:
                                    

                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Change_password/success.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Change_password/success.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-eye-open"></i></span>
                                <h5>Consideraciones a tomar en cuenta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">

                                <div class="text">
                                    Para cambiar la contraseña se aplican las siguientes reglas:
                                       <li>La contraseña debe tener al menos una letra mayúscula, una letra minúscula y tener de 6 a 10 letras
                                       </li>
                                    <li>El campo de nueva contraseña y confirmar contraseña deben de concordar
                                    </li>
                                    <li>Ejemplo de contraseña valida: Bms1234
                                    </li>
                                </div>



                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>


