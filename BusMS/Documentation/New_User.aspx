<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_User.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <span class="pull-right">05/17/2017
                </span>
                <h4>Nuevo Usuario</h4>
                <div class="text">
                    Agregar un usuario en la aplicación se conforma de dos secciones:
                    <li>Agregar los datos del usuario
                    </li>
                    <li>modificar sus permisos.
                    </li>
                    Para comenzar, es necesario agregar primero el usuario para que se carguen sus permisos y puedas editar el nivel de acceso que tendrá
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-user"></i></span>
                                <h5>Agrear Usuario</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/New_User/add_User.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/New_User/add_User.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Toma en cuenta los siguientes aspectos para poder crear un nuevo usuario
                                    <li>La contraseña debe tener al menos una letra mayúscula, una letra minúscula y tener de 6 a 10 letras
                                    </li>
                                    <li>ID BMS es el id con el que se entra a la aplicación
                                    </li>
                                    <li>Perfil (Interno o externo) Definirá la información que el usuario puede visualizar, un perfil externo estará ligado a un
                                        proveedor, mientras que el interno tiene acceso a la información de todos los proveedores
                                    </li>
                                    <li>Rol (De preferencia, el rol de administrador, debería pertenecer a una sola persona)
                                    </li>
                                </div>
                                <div class="text">
                                    Una vez que hayas guardado <img class="soloImg" src="d_images/save.png" />
                                    la información del usuario, se cargará por default los permisos que tiene
                                    asignado el usuario debido a su rol, estos permisos pueden ser editados en la sección de la derecha
                                </div>
                                  <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/New_User/modify_Pages.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/New_User/modify_Pages.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class=" icon-star"></i></span>
                                <h5>Permisos</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">

                                 <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/New_User/permissions.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/New_User/permissions.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Las secciones marcadas serán aquellas a las que el usuario tenga acceso.
                                    En ciertos casos esas secciones tiene privilegios que también pueden ser controlados, ya sea que no tenga el permiso de eliminar
                                    o de insertar datos.
                                    Para guardar tus cambios en los privilegios, da click en el botón de actualizar
                                    <img class="soloImg" src="d_images/New_User/updateUser.png" />
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



