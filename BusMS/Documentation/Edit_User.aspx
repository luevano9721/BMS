<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_User.aspx.cs" Inherits="BusManagementSystem.Documentation.Edit_User" %>

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
                    05/19/2017
                </span>
                <h4>Editar usuario</h4>
                <div class="text">
                    Después de entrar a la sección de <a href="View_Users.aspx">usuarios</a> y hacer clic en el icono <img class="soloImg" src="d_images/Edit_user/icono_Editar.png" /> nos mostrara la sección donde se editan los datos propios de los usuarios. 
                    también se editan los permisos y privilegios correspondientes a las diferentes páginas del sistema.
                </div>
            </div>
    <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                         <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce el catálogo</h5>

                            </a>
                        </div>
                    
                     <div class="collapse" id="collapseOne">
                            <div class="widget-content">
                                  <div class="text">
                                   En este catálogo podemos encontrar dos secciones principales:
                                </div>
                                <br/>
                                <div class="text">
                                    Información de usuario: en esta sección se editan los datos del usuario.
                                    </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Edit_user/Edit_User.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Edit_user/Edit_User.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                   Permisos y privilegios: en esta sección se agregan o eliminan permisos y privilegios que corresponden al sistema.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                            <a>
                                            <img src="d_images/Edit_user/Permisos_Privilegios.png" />
                                            </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Edit_user/Permisos_Privilegios.png"><i class="icon-search"></i></a></div>
                                       </li>
                                    </ul>
                          


                            </div>

                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="iicon-edit"></i></span>
                                <h5>Editar información de usuario</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <div class="text">
                                    Después de seleccionar algún usuario se habilitaran los siguientes registros para ser editados:
                                    <li>ID de BMS
                                    </li>
                                    <li>Nombre completo del usuario
                                    </li>
                                    <li>Numero de empleado
                                    </li>
                                    <li>Departamento
                                    </li>
                                    <li>Fecha de nacimiento
                                    </li>
                                    <li>Fecha de contratación
                                    </li>
                                    <li>Dirección
                                    </li>
                                    <li>Teléfono 
                                    </li>
                                    <li>Fecha de expiración de la contraseña
                                    </li>
                                    <li>Perfil
                                    </li>
                                    <li>Rol
                                    </li>
                                    <li>Proveedor
                                    </li>
                                    <li>Correo electrónico
                                    </li>
                                    <li>Habilitado
                                    </li>
                                    <li>Lista Negra
                                    </li>
                              

                                    
                                    </div>
                                <div class="text">
                                    Despues de editar los registros necesario es necesario hacer clic en el botón <img class="soloImg" src="d_images/btActualizar.png" /> para guardar los datos.
                                    </div>
                                </div>
                            </div>
                        <div class="widget-title">
                            <a href="#collapse3" data-toggle="collapse"><span class="icon"><i class="icon-list-ol"></i></span>
                                <h5>Editar permisos y privilegios</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapse3">
                            <div class="widget-content">
                                <div class="text">
                                    Para agregar o quitar algún permiso de acceso a una página es necesarios hacer clic en el icono <img class="soloImg" src="d_images/Edit_user/Check.png" />, cuando no se tiene agregado algún acceso se vera de la siguiente manera <img class="soloImg" src="d_images/Edit_user/uncheck.png" />, es necesario hacer clic para agregarlo.
                                    </div>
                                <div class="text">
                                    En la sección de permisos las diferentes paginas están seccionadas por catálogos los cuales su muestran en la imagen siguiente:

                                    </div>
                                 <ul class="thumbnails">
                                    <li class="span2">
                                            <a>
                                            <img src="d_images/Edit_user/Permisos_Privilegios.png" />
                                            </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Edit_user/Permisos_Privilegios.png"><i class="icon-search"></i></a></div>
                                       </li>
                                    </ul>
                                 <div class="text">
                                     Es necesario hacer clic en el icono <img class="soloImg" src="d_images/Edit_user/Plus.png" /> para poder visualizar las páginas del catálogo y tener la opción de quitar o agregar el acceso a una página en específico. Se vera de la siguiente manera:

                                     </div>
                                    <ul class="thumbnails">
                                    <li class="span2">
                                            <a>
                                            <img src="d_images/Edit_user/Permisos_Privilegios_Plus.png" />
                                            </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Edit_user/Permisos_Privilegios_Plus.png"><i class="icon-search"></i></a></div>
                                       </li>
                                    </ul>
                                    <div class="text">
                                        Si es necesario quitar o agregar privilegios de una página es necesario hacer clic en el icono <img class="soloImg" src="d_images/Edit_user/Plus.png" /> para visualizar los privilegios que tiene la pagina.
                                        
                                        </div>
                                     <ul class="thumbnails">
                                    <li class="span2">
                                            <a>
                                            <img src="d_images/Edit_user/Permisos_Privilegios_Plus_Plus.png" />
                                            </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Edit_user/Permisos_Privilegios_Plus_Plus.png"><i class="icon-search"></i></a></div>
                                       </li>
                                    </ul>
                                <div class="text">
                                    Cada página contiene los siguientes privilegios:
                                     <li>Insert
                                    </li>
                                    <li>Update
                                    </li>
                                    <li>Delete
                                    </li>
                                    <li>Export
                                    </li>
                                    
                                    </div>
                                <div class="text">
                                    los cuales se pueden quitar o agregar haciendo clic en el siguiente icono <img class="soloImg" src="d_images/Edit_user/Check.png" />
                                    </div>
                                     <br />
                               <div class="text">
                                    Después de editar los registros necesario es necesario hacer clic en el botón <img class="soloImg" src="d_images/btActualizar.png" /> para guardar los datos.
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

