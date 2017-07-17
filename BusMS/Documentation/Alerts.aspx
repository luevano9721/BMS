<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alerts.aspx.cs" Inherits="BusManagementSystem.Documentation.Alerts" %>

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
                <h4>Alertas</h4>
                <div class="text">
                    Las alertas son recordatorios que se pueden ingresar en el sistema para que le sean recordados
                     al usuario periódicamente por medio de un correo electrónico. Un ejemplo seria el recordar cada semana que se
                     debe de realizar el mantenimiento de los camiones o realizar la facturación de la semana. Cualquier tipo de 
                    recordatorio puede ser capturado aquí, 
                    en la sección de comentarios podrás indicar cualquier situación específica que necesites sea recordada.
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
                                            <img src="d_images/Alerts/alerts.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Alerts/alerts.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Datos que se verás en alertas:
                                   <li>Código (autogenerado)</li>
                                    <li>Elemento de alerta (numérico)</li>
                                    <li>Descripción </li>
                                    <li>Acción</li>
                                    <li>Periodo (cada cuantos días desea recibir el correo)</li>
                                    <li>Prioridad</li>
                                    <li>Fecha de creación</li>
                                    <li>Ultima vez que se envió correo</li>
                                    <li>Activo</li>
                                    <li>Fecha de apagado de alerta</li>
                                    <li>Proveedor al que aplica la alerta</li>
                                    <li>Tipo de alerta (Manual por default)</li>


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
                                    EEsta pantalla solo cuenta con un filtro que es el proveedor al que aplicar cada alerta, 
                                    la opción de “ALL” te mostrara todas las alertas que están registradas como para todos los proveedores.
                                    Las demás opciones serán los proveedores que se tengan registrados en el sistema.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar una nueva alerta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Alerts/new_Alert.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Alerts/new_Alert.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Una vez presionado el icono   
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    veras los campos del proveedor como se muestra en la siguiente imagen. 
                                </div>

                                <div class="text">
                                    Al presionar el botón  
                                    <img class="soloImg" src="d_images/add.png" />
                                    se habilitaran los siguientes campos:
                                    <li>Código (autogenerado)</li>
                                    <li>Elemento de alerta</li>
                                    <li>Descripción </li>
                                    <li>Acción</li>
                                    <li>Periodo (en días, máximo 365)</li>
                                    <li>Prioridad</li>
                                    <li>Activo</li>
                                    <li>Fecha de cerrado automático</li>
                                    <li>Proveedor al que aplica la alerta</li>

                                </div>
                                <div class="text">
                                    Al presionar el botón <img class="soloImg" src="d_images/add.png" />  esos campos se habilitaran
                                    Después de llenar todos los campos, haz clic en guardar, 
                                    <img class="soloImg" src="d_images/save.png" />
                                    de ser necesario hacer clic en el botón 
                                    <img class="soloImg" src="d_images/reset.png" />
                                    para cancelar el registro de una nueva alerta.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar una alerta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Alerts/new_Alert.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Alerts/new_Alert.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Presiona el botón de seleccionar   
                                    <img class="soloImg" src="d_images/select.png" />
                                    en la alerta que quieres editar.
                                    Ya terminados tus cambios, haz clic en guardar
                                    <img class="soloImg" src="d_images/save.png" />
                                       o bien clic en el botón   
                                    <img class="soloImg" src="d_images/reset.png" />para cancelar.
                                </div>



                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar una alerta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">

                                <div class="text">
                                   Esta acción no está habilitada para este catálogo, ya que las alertas registradas se utilizan para reportes históricos.

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


