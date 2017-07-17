<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Driver.aspx.cs" Inherits="BusManagementSystem.Documentation.Driver" %>

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
                <span class="pull-right">05/02/2017
                </span>
                <h4>Conductores</h4>
                <div class="text">
                    En esta sección se realizan las tareas relacionadas con la administración de los conductores, como lo son;
                     Agregar un nuevo conductor, Eliminar conductor, editar datos del conductor, inactivar o bloquear al conductor.
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
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Driver/driver.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/driver.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    En esta parte podemos agregar un nuevo conductor, editar los datos de un conductor, inhabilitar a un conductor y agregarlo a la<a href="BlackList.aspx"> lista negra</a>. 
                                    visualizar una lista que contiene los datos de todos los conductores registrados.
                                     Los conductores se pueden filtrar por proveedor al que están asociados. 
                                    Los registros en la lista están ordenados del registro más nuevo al registro más viejo y
                                     nos podemos mover entre ellos mediante la paginación que está en la parte inferior de la lista.
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
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/filterActive_Bus.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/filterActive_Bus.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/filterInactive_Bus.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/filterInactive_Bus.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Puedes filtrar por el proveedor de cada conductor que se tiene registrado. Si no tienes permisos de administrador,
                                     el filtro de proveedor aparecerá deshabilitado y solo se mostrara la información del proveedor con el que estas registrado.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar un nuevo Conductor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Driver/new_Driver.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/new_Driver.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Se debe de dar clic en el icono      
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    para que se desplaye la ventana donde está el formulario  y 
                                    poder comenzar con el proceso de agregar un nuevo conductor, esto mostrara la siguiente pantalla:
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Driver/edit_Driver.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/edit_Driver.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    A continuación se necesita hacer clic en el botón   
                                    <img class="soloImg" src="d_images/add.png" />
                                    para poder habilitar los campos del formulario y llenarlos respectivamente, según se comenta en la siguiente lista: 

                                    <li>Nombre  del conductor
                                    </li>
                                    <li>Proveedor
                                    </li>
                                    <li>Fecha de nacimiento
                                    </li>
                                    <li>Fecha de contratación
                                    </li>
                                    <li>Dirección
                                    </li>
                                    <li>Numero de licencia
                                    </li>
                                    <li>Fecha de expiración de licencia
                                    </li>
                                    <li>Teléfono 
                                    </li>
                                    <li>Activo
                                    </li>
                                    <li>Bloqueado
                                    </li>


                                    <div class="text">
                                        Si se llenaron todos los campos solicitados se puede proceder a guardar los cambios haciendo clic en el botón 
                                        <img class="soloImg" src="d_images/save.png" />
                                    </div>

                                    <div class="text">
                                        Si todo es correcto, la página nos mostrara un mensaje con el nombre del conductor, como en la siguiente imagen:                                        
                                    </div>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/successAdd.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/successAdd.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <div class="text">
                                        En el caso contrario, si hace falta llenar un campo del formulario la página no nos permitirá continuar,
                                         y en la parte inferior del formulario nos mostrara un mensaje de error, en el cual incluirá el campo o los campos 
                                        que nos hace falta llenar, también pondrá un asterisco de color rojo debajo de cada campo que se necesita llenar.
                                    </div>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Driver/error_Driver.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/error_Driver.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <div class="text">
                                        Si se desea no continuar con el registro de un conductor es necesario hacer clic en el botón de restablecer
                                        <img class="soloImg" src="d_images/reset.png" />
                                        Este botón regresa el formulario a su estado original.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un Conductor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">

                                <div class="text">
                                    Después de seleccionar un conductor, los campos del formulario quedaran disponibles para que se pueda editar la información, 
                                    si después de editar se desea guardar los cambios es necesario hacer clic en el botón,   
                                    <img class="soloImg" src="d_images/save.png" />
                                    en el caso contrario hacer clic en el botón  
                                    <img class="soloImg" src="d_images/reset.png" />
                                    el cual regresara el formulario a su estado original y los cambios no se guardaran.
                                </div>

                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar un Conductor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    Después de seleccionar un conductor como se explicó anteriormente, en la parte inferior del formulario aparecerá 
                                    un botón de eliminar
                                    <img class="soloImg" src="d_images/delete.png" />
                                    con el que borraremos al conductor haciendo clic en el mismo.
                                </div>
                                <div class="text">
                                    Solo los conductores sin relaciones como Camión/Conductor o que no hayan sido nunca usados en la <a href="Daily_Operation.aspx">operación diaria</a>, 
                                    van a poder ser eliminados exitosamente.
                                </div>
                            </div>
                        </div>

                        <div class="widget-title">
                            <a href="#collapseSeven" data-toggle="collapse"><span class="icon"><i class="icon-folder-open"></i></span>
                                <h5>Agregar Documentos</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseSeven">
                            <div class="widget-content">

                                <div class="text">
                                    Después de seleccionar un conductor, el botón de documentos aparecerá,  
                                    <img class="soloImg" src="d_images/Driver/documents.png" />
                                    Al dar click se mostrará una ventana emergente como la siguiente:
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Driver/new_document.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/new_document.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    En la cual podrás agregar los documentos que necesites para el conductor.

                                    <div class="text">
                                        Los archivos que podamos incluir en el sistema deben de cumplir con estos requisitos <br />
                                        <i>Tipos de archivo soportados: "bmp", "gif", "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "pdf"</i><br />
                                        <i>Archivos no mayores a 5 MB</i>
                                    </div>
                                </div>
                                <div class="text">
                                    Puedes descargar los archivos con el botón de descargar
                                    <img class="soloImg" src="d_images/Driver/download.png" />
                                    o bien eliminarlos si se ha subido un archivo incorrecto
                                    <img class="soloImg" src="d_images/Driver/delete.png" />
                                </div>

                                 <div class="text">
                                    Si deseas agregar la foto de perfil, primero selecciona el conductor al que deseas editar
                                    y con el botón  <img class="soloImg" src="d_images/Driver/profile.png" />
                                    se mostrará la siguiente interfaz, donde podrás elegir la foto del conductor que deseas agregar.

                                     <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Driver/add_picture.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/add_picture.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>

                                    La imagen de perfil debe cumplir con las mismas restricciones que los documentos.
                                </div>

                            </div>
                        </div>


                        <div class="widget-title">
                            <a href="#collapseSix" data-toggle="collapse"><span class="icon"><i class="icon-road"></i></span>
                                <h5>Rutas Certificadas</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseSix">
                            <div class="widget-content">

                                <div class="text">
                                    Después de seleccionar un conductor, el botón de documentos aparecerá,  
                                    <img class="soloImg" src="d_images/Driver/certify_routes.png" />
                                    Al dar click se mostrará una ventana emergente como la siguiente:
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Driver/add_certify_route.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/add_certify_routes.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                                                       
                                </div>
                                <div class="text">
                                    Compuesta por dos secciones principales<br />
                                    <b>Ver rutas certificadas por el conductor</b> 
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Driver/routes.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/routes.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <div class="text">
                                        Aquí se muestran las rutas que el conductor ya tiene certificadas
                                        y con el icono <img class="soloImg" src="d_images/Driver/remove.png" /> puedes quitar rutas de la lista
                                    </div>
                                    <b>Agregar rutas certificadas</b>
                                     <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Driver/add_route.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Driver/add_route.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                     <div class="text">
                                        Aquí se muestran todas las rutas disponibles para que se agreguen como certificadas por el conductor
                                        y con el icono <img class="soloImg" src="d_images/Driver/add.png" /> puedes agregarlas a la lista de rutas certificadas
                                    </div>
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
