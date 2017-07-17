<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Route.aspx.cs" Inherits="BusManagementSystem.Documentation.Route" %>

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
                <h4>Rutas</h4>
                <div class="text">
                    El catálogo de rutas es donde se registran
                    las diferentes rutas que se realizan para transportar personal, 
                    tanto para la entrada a la empresa, como al salir. 
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
                                    Un requisito para poder utilizar el catálogo de rutas es que ya existan
                                     <a href="Service.aspx">servicios</a> capturados en el sistema, y 
                                     <a href="StopPoint.aspx">los puntos de parada</a> pues son campos requerido para saber 
                                    cómo calcular el costo de la ruta, al momento de realizar la <a href="Invoice_Payment_Format.aspx">facturación</a>.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Route/route.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Route/route.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    La ruta está compuesta por los siguientes campos:
                                    <li>Origen
                                    </li>
                                    <li>Destino
                                    </li>
                                    <li>Tiempo estimado de viaje
                                    </li>
                                    <li>Servicio
                                    </li>
                                    <li>Zona
                                    </li>
                                </div>
                                <div class="text">
                                    Los registros que se capturen en esta pantalla, aparecerán reflejados al momento de capturar la operación diaria.
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
                                            <img src="d_images/Route/filter_Route.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Route/filter_Route.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Puedes filtrar ya sea por el tipo de ruta (Entrada o Salida) y por el tipo de zona
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar una nueva ruta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Route/new_Route.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Route/new_Route.png"><i class="icon-search"></i></a></div>
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

                                    <li>Origen (Elemento de <a href="StopPoint.aspx">Puntos de Parada</a>)
                                    </li>
                                    <li>Destino  (Elemento de <a href="StopPoint.aspx">Puntos de Parada</a>)
                                    </li>
                                    <li>Tiempo estimado de viaje (en horas:minutos)
                                    </li>
                                    <li>Servicio (Elemento del catálogo de <a href="Service.aspx">Servicio</a>)
                                    </li>
                                    <li>Zona (No. de zona donde inicia la ruta)
                                    </li>
                                    <div class="text">
                                        Después de seleccionar todos los campos, haz clic en guardar 
                                        <img class="soloImg" src="d_images/save.png" />
                                        es necesario hacer clic en el botón   
                                        <img class="soloImg" src="d_images/reset.png" />
                                        para dejar al formulario en su estado original.
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar una ruta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Route/edit_Route.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Route/edit_Route.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Al seleccionar un elemento para su edición, 
                                    aparecerán habilitados los mismos campos que se habilitaron para insertar una nueva ruta:
                                </div>
                                <div class="text">

                                    <li>Origen (Elemento de <a href="StopPoint.aspx">Puntos de Parada</a>)
                                    </li>
                                    <li>Destino  (Elemento de <a href="StopPoint.aspx">Puntos de Parada</a>)
                                    </li>
                                    <li>Tiempo estimado de viaje (en horas:minutos)
                                    </li>
                                    <li>Servicio (Elemento del catálogo de <a href="Service.aspx">Servicio</a>)
                                    </li>
                                    <li>Zona (No. de zona donde inicia la ruta)
                                    </li>
                                </div>
                                <div class="text">
                                    Con la diferencia que en el espacio de ID de Ruta, tendrá el ID, de la ruta que seleccionaste para editar.
                                    Al terminar tus cambios, presiona el botón de guardar 
                                    <img class="soloImg" src="d_images/save.png" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar una ruta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    Al igual que en otros catálogos, la eliminación de rutas se encuentra habilitada, mas solo se podrá eliminar aquellas rutas 
                                    que no tengan relación con algún otro registro, es decir, aquellas que jamás se hayan utilizado en la operación diaria.

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
