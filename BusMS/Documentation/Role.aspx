<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                    05/22/2017
                </span>
                <h4>Roles</h4>
                <div class="text">
                   En esta sección se administra lo relacionado con los roles asignados a un tipo de usuario. Se crean nuevos roles, se editan y se eliminan.
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class=" icon-plus-sign"></i></span>
                                <h5>Agregar un nuevo rol</h5>
                                 
                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">
                                
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Rol/New_Rol.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Rol/New_Rol.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    En esta sección se agregan los roles nuevos haciendo clic en el botón <img class="soloImg" src="d_images/add.png" /> 
                                    que se muestra en la imagen anterior. Después de hacer click se habilitara el campo ID ROL en el cual es necesario ingresar el nombre del nuevo rol.

                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Rol/new_Rol_Add.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Rol/new_Rol_Add.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Se necesita hacer click en el botón <img class="soloImg" src="d_images/save.png" /> 
                                    para guardar los cambios, en el caso contrario hacer click en el botón <img class="soloImg" src="d_images/reset.png" /> 
                                    para cancelar los cambios.
                                    </div><br />
                                <div class="text">
                                    Al hacer click en el botón <img class="soloImg" src="d_images/save.png" /> aparecerán los permisos y privilegios, 
                                    que editarse dependiendo de las necesidades del nuevo rol.
                                    </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Rol/Privileges.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Rol/Privileges.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Haz click en el botón <img class="soloImg" src="d_images/Rol/btSavePermisions.png" />
                                     para guardar los cambios, de lo contrario click en el botón <img class="soloImg" src="d_images/reset.png" /> para cancelar los cambios.
                                    </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un rol</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">

                                <div class="text">
                                   Para editar algún rol primero debemos hacer clic en el botón <img class="soloImg" src="d_images/select.png" /> correspondiente a la fila del rol escogido.

                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Rol/Roles.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Rol/Roles.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                   Después de seleccionar el rol deseado, nos brindara la posibilidad de editar sus permisos y privilegios.

                                </div>
                                 <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Rol/Permisions_N_privileges.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Rol/Permisions_N_privileges.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                 <div class="text">
                                    Para agregar o quitar algún permiso de acceso a una página es necesarios hacer clic en el icono
                                      <img class="soloImg" src="d_images/Edit_user/Check.png" />, cuando no se tiene agregado algún acceso se vera de la siguiente manera 
                                     <img class="soloImg" src="d_images/Edit_user/uncheck.png" />, es necesario hacer clic para agregarlo.
                                    </div>
                                <div class="text">
                                      Después de editar los permisos y privilegios hacer clic en el boton <img class="soloImg" src="d_images/Rol/btSavePermisions.png" />
                                     para guardar los cambios, de lo contrario hacer clic en el botón <img class="soloImg" src="d_images/reset.png" /> para cancelar los cambios.
                                    </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-minus-sign"></i></span>
                                <h5>Eliminar un rol</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <div class="text">
                                   Para eliminar algún rol primero debemos hacer clic en el botón <img class="soloImg" src="d_images/select.png" /> correspondiente a la fila del rol escogido.

                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Rol/Delete_Rol.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Rol/Delete_Rol.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                   Al seleccionar un elemento para eliminarlo es necesario hacer clic en el botón   
                                    <img class="soloImg" src="d_images/delete.png" />
                                    para eliminar el registro, en el caso contrario hacer clic en el botón   
                                    <img class="soloImg" src="d_images/reset.png" />
                                    para cancelar los cambios al registro.
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



