<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlertSupport.aspx.cs" Inherits="BusManagementSystem.Documentation.AlertSupport" %>

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
                <h4>Alertas</h4>
                <div class="text">
                    En esta sección se muestran todas las alertas que se han generado dentro del sistema,
                    tanto manuales como programadas.
                    Podrás agregar comentarios, indicar que se ha comenzado a trabajar en la alerta o que ya se encuentra terminada.
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
                                            <img src="d_images/AlertSupport/alert_support.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AlertSupport/alert_support.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    En esta sección podemos observar de manera rápida los siguientes campos
                                    <li>Código de alerta
                                    </li>
                                    <li>Descripción
                                    </li>
                                    <li>Acción
                                    </li>
                                    <li>Fecha de creación
                                    </li>
                                    <li>Camión
                                    </li>
                                    <li>Conductor
                                    </li>
                                    <li>Prioridad
                                    </li>
                                    <li>Estatus
                                    </li>

                                </div>

                                <div class="text">
                                    Después de estos datos tenemos la columna de "Más información"
                                    <img class="soloImg" src="d_images/AlertSupport/more_Info.png" />
                                    al dar click en el icono, se visualizará la siguiente ventana con detalles sobre dicha alerta
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AlertSupport/moreInfo_Alert.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AlertSupport/moreInfo_Alert.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    "Levantado por" Indica que parte del sistema levanto la alerta 
                                    o si fue un usuario quien la solicito
                                </div>
                                <div class="text">
                                    "Comentarios" Es la sección donde el usuario puede agregar información 
                                    adicional a la alerta para que ésta sea mas clara.
                                </div>
                                <div class="text">
                                    "Cerrado Por" Es Id del usuario administrador que cerro la alerta, 
                                    este campo tendrá el ID únicamente cuando la alerta se encuentre cerrada.
                                </div>
                                <div class="text">
                                    "Ultima modificación" Es referente al seguimiento que se le tiene a la alerta
                                    y cuando fue la última vez que se agregó un comentario a la misma.
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
                                            <img src="d_images/AlertSupport/filter_AlertSupport.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AlertSupport/filter_AlertSupport.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Podemos filtrar por el estatus que tenga la alerta (Abierto, Trabajando, Terminado o  Cerrado) 
                                    y por el tipo de proveedor que estemos buscando
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Agregar comentarios</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AlertSupport/addComment_Alert.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AlertSupport/addComment_Alert.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Da click en el botón  
                                    <img class="soloImg" src="d_images/AlertSupport/addComment.png" />
                                    para mostrar la imagen arriba mostrada y poder agregar tus comentarios sobre la alerta.
                                </div>
                                 <div class="text">
                                    Escribes el comentario que quieras agregar y presionas el botón 
                                    <img class="soloImg" src="d_images/AlertSupport/btAddComment.png" />
                                </div>


                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Cambiar estatus</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">

                                <div class="text">
                                   Si deseas cambiar el estatus de la alerta, solo es necesario 
                                    dar click en el icono 
                                    <img class="soloImg" src="d_images/AlertSupport/takeAction.png" />
                                    y la siguiente ventana se visualizará
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AlertSupport/takeAction_Alert.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AlertSupport/takeAction_Alert.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Donde tendrás la opción de cambiar a "Trabajando" o a "Terminado" <br />
                                    Seleccionas una opción y das click en  <img class="soloImg" src="d_images/save.png" />
                                </div>
                               
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Cerrar alerta</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                 <div class="text">
                                   Cerrar alerta solo es posible para un usuario con el rol de administrador, 
                                     al igual que en cambiar el estatus, hay que 
                                    dar click en el icono 
                                    <img class="soloImg" src="d_images/AlertSupport/endAlert.png" />
                                    para que la siguiente ventana se muestre.
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AlertSupport/end_Alert.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AlertSupport/end_Alert.png"><i class="icon-search"></i></a></div>
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

