<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopPoint.aspx.cs" Inherits="BusManagementSystem.Documentation.StopPoint" %>

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
                    05/02/2017
                </span>
                <h4>Puntos de parada</h4>
                <div class="text">
                    Esta sección del sistema es la encargada de administrar los puntos de parada de las rutas, en esta sección se 
                    pueden agregar nuevos puntos de parada, eliminarlos, editarlos y exportar la información de los puntos de parada existentes. . 
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
                                            <img src="d_images/StopPoint/stoppoint.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/StopPoint/stoppoint.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                   El punto de parada está compuesto por:
                                    <li>Identificador de la parada
                                    </li>
                                    <li>Nombre de la parada
                                    </li>
                                    <li>Dirección
                                    </li>
                                    <li>Coordenadas
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
                                    Este catálogo no cuenta con filtros como tal, sin embargo existe una barra de búsqueda,
                                     introduces el nombre del punto de parada y va mostrando los resultados parecidos al texto escrito.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar un nuevo punto de parada</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/StopPoint/new_StopPoint.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/StopPoint/new_StopPoint.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Recuerda dar clic en el icono    
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    para ver los campos para capturar el nuevo registro, como se muestra en la imagen. 
                                </div>

                                <div class="text">
                                   Al presionar el botón  
                                    <img class="soloImg" src="d_images/add.png" />
                                     se habilitaran los siguientes campos:
                                    <li>Identificador de la parada(Abreviatura o identificador del punto de parada)
                                    </li>
                                    <li>Nombre de la parada (Es el nombre que se le asignara al punto de parada)
                                    </li>
                                    <li>Dirección
                                    </li>
                                    <li>Coordenadas (coordenadas del punto de parada)
                                    </li>

                                </div>
                                <div class="text">
                                    Después de seleccionar todos los campos, haz clic en guardar, <img class="soloImg" src="d_images/save.png" />
                                    en el caso contrario hacer clic en el botón    <img class="soloImg" src="d_images/reset.png" />
                                    para cancelar el registro de una nueva ruta.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un punto de parada</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/StopPoint/edit_StopPoint.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/StopPoint/edit_StopPoint.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Al seleccionar un elemento para su edición, aparecerán habilitados 
                                    los mismos campos que se habilitaron para insertar un nuevo punto:
                                </div>
                                <div class="text">

                                    <li>Identificador de la parada
                                    </li>
                                    <li>Nombre de la parada
                                    </li>
                                    <li>Dirección
                                    </li>
                                    <li>Coordenadas
                                    </li>
                                </div>
                                <div class="text">
                                    Con la diferencia que en el espacio de ID de punto de parada, tendrá el ID, del punto que seleccionaste para editar.
                                    Al terminar tus cambios, presiona el botón de guardar, 
                                    <img class="soloImg" src="d_images/save.png" />
                                    en el caso contrario haz clic en el botón   
                                    <img class="soloImg" src="d_images/reset.png" />
                                    para cancelar los cambios al registro.
                                </div>

                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar un punto de parada</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    Al seleccionar un elemento para eliminarlo la información se pasa al formulario, es necesario hacer clic en el botón   
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

