<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Revision.aspx.cs" Inherits="BusManagementSystem.Documentation.Revision" %>

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
                <span class="pull-right">05/16/2017
                </span>
                <h4>Revisiones</h4>
                <div class="text">
                    En esta sección se registran las revisiones que se hacen de forma periódica
                    a los camiones y sus conductores
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce la sección</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/revision.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/revision.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Los datos de revisión que se muestran al cargar la página son:
                                    <li>Identificador de revisión
                                    </li>
                                    <li>Estatus
                                    </li>
                                    <li>Proveedor
                                    </li>
                                    <li>Fecha de creación
                                    </li>
                                    <li>RFC
                                    </li>
                                    <li>Conductor y camión
                                    </li>
                                    <li>Turno
                                    </li>

                                </div>

                                <div class="text">
                                    Solamente las revisiones con estatus de abierto, tendrán 
                                    la imagen de lupa
                                    <img class="soloImg" src="d_images/magnifier.png" />
                                    para poder editar los casos de revisión que tienen registrados
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
                                            <img src="d_images/Revision/filter_Revision.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/filter_Revision.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Podemos filtrar por el estatus que tenga la revisión (Abierto o Cerrado) 
                                    y por el tipo de proveedor que estemos buscando
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar una nueva revisión</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/new_Revision.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/new_Revision.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Al presionar el botón  
                                    <img class="soloImg" src="d_images/add.png" />
                                    se habilitaran los siguientes campos:
                                    <li>Proveedor
                                    </li>
                                    <li>Turno 
                                    </li>
                                    <li>Ruta
                                    </li>
                                    <li>
                                    Relación Camión/Conductor


                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/add_revision1.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/add_revision1.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Primero se debe de seleccionar el proveedor y el turno en donde
                                     se encuentra el camión al que queremos registrar la revisión
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/add_revision2.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/add_revision2.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Después de seleccionar ambos campos, se habilitará la siguiente serie de campos; 
                                    ruta y la relación Camión/Conductor
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/add_revision3.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/add_revision3.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Justo después de seleccionar el camión, los demás datos se cargarán de forma
                                     automática, dejando únicamente libres los campos de checklist
                                     para que se indique si el conductor cuenta con cada uno de los siguientes campos:
                                </div>

                                <div class="text">
                                    <li>Mapa de ruta
                                    </li>
                                    <li>Radio 
                                    </li>
                                    <li>GPS
                                    </li>
                                    <li>Identificado PCE</li>
                                    <li>Linterna</li>
                                    <li>Focos de emergencia</li>
                                </div>

                                <div class="text">
                                    Presiona el botón de guardar
                                    <img class="soloImg" src="d_images/save.png" />
                                    para poder editar las inspecciones individuales que se le han hecho
                                </div>

                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar una revisión</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">

                                <div class="text">
                                    Recuerda que solo las revisiones con estatus de abierto, tendrán 
                                    la imagen de lupa
                                    <img class="soloImg" src="d_images/magnifier.png" />
                                    con la que al dar click se mostrará una nueva página con todos los
                                     campos a inspeccionar en la revisión del camión, tal y como se visualiza en la imagen siguiente:
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/edit_Revision.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/edit_Revision.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Esta sección se ve dividida en dos tipos de revisión (Seguridad y Mantenimiento)
                                     y 3 columnas principales, 
                                    la primer columna es para indicar que ese rubro 
                                    ya ha sido inspeccionado por el inspector
                                </div>
                                <div class="text">
                                    Si deseas seleccionar todos los rubros, da click en el siguiente botón                                                                     
                                     <img class="soloImg" src="d_images/Revision/select_all.png" />
                                </div>

                                <div class="text">
                                    La segunda columna indica que son anomalías de severidad media y la tercer columna son las
                                    anomalías de severidad alta.

                                </div>
                                <div class="text">
                                    Al presionar el botón de check la siguiente pantalla aparecerá, solicitando que 
                                    se indique la fecha de propuesta para reparar la anomalía y la acción que el proveedor debe de tomar.                                    

                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/revision_note.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/revision_note.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    En el icono de la imagen
                                    <img class="soloImg" src="d_images/image.png" />
                                    puedes subir 
                                     las imágenes que servirán como evidencia de la anomalía identificada.

                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/upload_image.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/upload_image.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    <b>Es necesario dar click en guardar para que tus cambios de la página queden salvados           </b>
                                    <img class="soloImg" src="d_images/save.png" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Cerrar una revisión</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    Si deseas cerrar una revisión, primero es necesario seleccionar 
                                    con el botón
                                    <img class="soloImg" src="d_images/select.png" />
                                    la revisión que quieres dar por terminado y que tiene estatus "OPEN"
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/close_Revision.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/close_Revision.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Una vez que te aparezca la opción de cerrar la revisión, das click en el botón 
                                     <img class="soloImg" src="d_images/Revision/close.png" />
                                    y un mensaje de confirmación se mostrará
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/successClose.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/successClose.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Podrás cerrar una revisión una vez que todos 
                                    los rubros de la primer columna estén palomeados, de lo contrario se mostrará el siguiente mensaje
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Revision/errorClose.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Revision/errorClose.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>



