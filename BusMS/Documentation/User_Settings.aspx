<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Settings.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <span class="pull-right">
                    05/18/2017
                </span>
                <h4>Ajustes de usuario</h4>
                <div class="text">
                    Esta página te permite cambiar las configuraciones relacionadas a tu usuario, tal como cambios de contraseña, 
                    cambios de correo electrónico , color de tema e idioma utilizado.
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
                                            <img src="d_images/User_settings/User_settings.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/User_settings/User_settings.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                   La página de ajustes de usuario esta divida en 3 secciones:
                                    <li><b>Cambiar contraseña:</b> Permite cambiar la contraseña del usuario actual que está utilizando la aplicación.
                                    </li>
                                    <li><b>Cambiar correo electrónico:</b> Permite cambiar el correo electrónico del usuario actual que está utilizando la aplicación.
                                    </li>
                                    <li><b>Cambiar color y lenguaje:</b> Permite seleccionar un color y aplicarlo al diseño del sitio, así como elegir el lenguaje de preferencia
                                        (español e inglés).
                                    </li>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-key"></i></span>
                                <h5>Cambiar contraseña</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">
                                  <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/User_settings/cambiar_pass.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/User_settings/cambiar_pass.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Esta opcion te permitira cambiar tu contraseña de forma rapida sin tener que solicitarla al
                                    administrador del sitio (sin embargo , el puede cambiarla o hacer que expire sin previo aviso debido
                                    a los permisos que tiene). 

                                    Consideraciones a tomar en cuenta:
                                     <li>La contraseña debe tener al menos una letra mayúscula, una letra minúscula y tener de 6 a 10 letras
                                     </li>
                                    <li>El campo de nueva contraseña y confirmar contraseña deben de concordar
                                    </li>
                                    <li>Ejemplo de contraseña valida: Bms1234
                                    </li>

                                    Para guardar los cambios debe de presionar el siguiente boton:       <img class="soloImg" src="d_images/User_settings/save_button.png" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-envelope"></i></span>
                                <h5>Cambiar correo electrónico</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/User_settings/cambiar_email.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/User_settings/cambiar_email.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                   Esta opción te permite cambiar tu correo electrónico. Para ello debes de rellenar el campo necesario con un
                                    correo electrónico valido y presionar sobre el botón   <img class="soloImg" src="d_images/User_settings/save_button.png" />
                                  
                                </div>
                     
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-magic"></i></span>
                                <h5>Cambiar color y lenguaje:</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/User_settings/cambiar_color.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/User_settings/cambiar_color.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                   Aquí puedes cambiar el color de la aplicación y lenguaje según tus gustos o necesidades. En cuanto al color, se debe de presionar
                                    sobre el pequeño cuadro pintado de color negro y se mostrara una paleta de colores que puedes seleccionar.
                                    <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/User_settings/colors.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/User_settings/colors.png"><i class="icon-search"></i></a></div>
                                    </li>
                                    </ul>
                                </div>
                               
                                <div class="text">
                                 En cuanto al idioma , solo es necesario seleccionar una de las dos opciones disponibles en el combo:
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/User_settings/idioma.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/User_settings/idioma.png"><i class="icon-search"></i></a></div>
                                    </li>
                                    </ul>
                                     </div>

                                      <div class="text">
                                  Para guardar la configuración tenemos las siguientes opciones:
                                     <li><b>Aplicar en la configuración  global:</b> Al presionar este botón , los cambios seran guardados en tu usuario aun asi inicies en otra
                                         computadora diferente o cierres tu sesión.
                                     </li>
                                    <li><b>Aplicar en la sesión:</b> Los cambios solo duraran mientras se este activo en la aplicación. Una vez que salga de ella o utilice
                                        otro navegador diferente , los cambios realizados no se reflejaran (son temporales).
                                    </li>
                                    <li><b>Restablecer la configuración predeterminada:</b> Restablece la configuración de color e idioma a la default o predeterminada para tu usuario.
                                        
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


