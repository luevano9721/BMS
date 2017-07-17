<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Usuarios</h4>
                <div class="text">
                    Esta pantalla te permite visualizar todos los usuarios registrados en la aplicación con su principal Información. 
                    Únicamente el administrador del sitio puede ver esta página.
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
                                            <img src="d_images/Users/view_users.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Vendor/vendor.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                   Se pueden visualizar de forma rápida los siguientes datos:
                                    <li>ID User: Nombre del usuario con el cual inicia sesión en BMS
                                    </li>
                                    <li>User name: Nombre del usuario 
                                    </li>
                                    <li>Email: Correo electrónico del usuario
                                    </li>
                                    <li>Foxconnn ID: Numero de reloj del usuario
                                    </li>
                                    <li>Vendor: Proveedor al que pertenece el usuario (En caso de que tenga un perfil externo).
                                    </li>
                                    <li>Role: Rol (Conjunto de accesos a páginas  y permisos asignados al usuario).
                                    </li>
                                    <li>Active: Estatus actual del usuario
                                    </li>
                                    <li>Blocked: Indica si el usuario ha sido bloqueado de la aplicación o no.
                                    </li>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-upload-alt"></i></span>
                                <h5>Filtrar información</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">

                                <div class="text">
                                    Este catálogo no cuenta con filtros como tal, sin embargo existe una barra de búsqueda, puedes introducir el User ID (BMS), 
                                    nombre de usuario , correo electronico o Foxconn ID y va mostrando los resultados parecidos al texto escrito.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un usuario</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Users/edit_users.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Users/edit_users.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Para editar un usuario es necesario presionar sobre el botón llamado <b>Edit</b> sobre el elemento deseado. Automáticamente
                                    Serás redirigido a la página de <a href="Edit_User.aspx">edición de usuario</a>s.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-minus-sign"></i></span>
                                <h5>Eliminar un usuario</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Users/delete_users.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Users/delete_users.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Para eliminar un usuario es necesario presionar sobre el botón llamado <b>Delete</b> sobre el elemento deseado. Posterior a ello , 
                                    se mostrar la siguiente ventana de confirmacion:
                                    <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Users/confirmation.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Users/confirmation.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                               
                                   <li> Botón  eliminar: Elimina el usuario de forma permanente. </li>
                                    <li>Botón  cancelar: No realiza la actividad.  </li>
                           
                                    Si la eliminación del usuario se realiza de forma exitosa, podrá visualizar un mensaje como el siguiente:
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Users/msg_successful.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Users/msg_successful.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>

                                    <b>Nota Importante:</b> La opción para eliminar usuario se encuentra bloqueada para usuarios que tienen rol ADMIN.
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


