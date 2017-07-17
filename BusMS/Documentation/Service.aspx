<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="BusManagementSystem.Documentation.Service" %>

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
                <h4>Servicios</h4>
                <div class="text">
                    El catálogo de servicios es donde se registran los tamaños o 
                    tipos de servicios que habrá en el sistema; como lo son el servicio Largo, Corto, Mediano, Transbordo, etc. 
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
                                    Es importante tener registrados los servicios antes de empezar cualquier operacion, 
                                    para asi poder calcular los precios que se deben de cobrar por cada servicio.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Service/service.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Service/service.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Un servicio esta compuesto por los siguientes campos:
                                    <li>Identificador del servicio
                                    </li>
                                    <li>Mínima distancia
                                    </li>
                                    <li>Máxima distancia 
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
                                     introduces el servicio y va mostrando los resultados parecidos al texto escrito.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar un nuevo servicio</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Service/new_Service.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Service/new_Service.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Recuerda dar clic en el icono    
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    para ver los campos para capturar el nuevo registro, como se muestra en la imagen. 
                                </div>

                                <div class="text">
                                    El identificador del servicio debe de ser unico, es decir que no puede llamarse igual a uno ya existente.
                                    Las distancias que se registren comienzan en cero y no tiene limite.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un servicio</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Service/edit_Service.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Service/edit_Service.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Al seleccionar un elemento para su edición, aparecerán habilitados los mismos 
                                    campos que se habilitaron para insertar un nuevo servicio.
                                     Con la diferencia que en el espacio de ID de servicio, tendrá el ID, del servicio que seleccionaste para editar.
                                </div>
                                <div class="text">

                                    <li>Identificador del servicio
                                    </li>
                                    <li>Mínima distancia
                                    </li>
                                    <li>Máxima distancia 
                                    </li>
                                </div>
                                <div class="text">
                                    Con la diferencia que en el espacio de ID de Ruta, tendrá el ID, de la ruta que seleccionaste para editar.
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
                                <h5>Eliminar un servicio</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    Un servicio solo podrá ser eliminado si no es utilizado en una <a href="Fare.aspx">tarifa</a>,
                                     <a href="Route.aspx">ruta</a> o algún otro registro, 
                                    de lo contrario un mensaje de error aparecerá y no permitirá continuar con el proceso de eliminación

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
