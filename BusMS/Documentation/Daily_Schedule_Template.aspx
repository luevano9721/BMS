<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Daily_Schedule_Template.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Plantilla de operacion diaria</h4>
                <div class="text">
                   En este catálogo se crea la platilla diaria de operación del servicio de transporte,
                   en esta plantilla se cargara en la operación diaria donde se relaciona el proveedor con los camiones que estarán en operación y por quien serán conducidos. 
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Cargar datos de operación diaria</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">
                                <div class="text">
                                    Cargar la operación diaria depende de dos factores, el proveedor y el turno de la operación.
                                   </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Daily_Schedule_Template/load.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Schedule_Template/load.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Después de elegir el proveedor y el turno es necesario hacer clic en el botón <img class="soloImg" src="d_images/Daily_Schedule_Template/bt_Load.png" /> 
                                    para cargar la ruta y Camión/Conductor asociados a el proveedor y al turno.
                                    </div>
                                <div class="text">
                                    A continuación se visualizara el siguiente formulario:
                                    </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Daily_Schedule_Template/Loaded_with_info.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Schedule_Template/Loaded_with_info.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Si se desea cambiar de turno o proveedor es necesario hacer clic en el botón <img class="soloImg" src="d_images/Daily_Schedule_Template/bt_ChangeShift.png" /> para poder iniciar con el proceso de cargar de nuevo los datos.
                                    </div>
                       
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Agregar un viaje</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">

                                <div class="text">
                                    La finalidad de esta platilla es agregar un viaje asociado a la operación diaria del transporte.
                                     Después de elegir el proveedor y el turno como se muestra en la sección anterior es necesario agregar los viajes que se realizaran en la operación.


                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Daily_Schedule_Template/Loaded.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Schedule_Template/Loaded.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <br />
                                <div class="text">
                                    Para agregar un viaje se necesita elegir un camión/conductor de la lista.<br/>
                                     hacer clic en la lista <img class="soloImg" src="d_images/Daily_Schedule_Template/Bus_Driver.png" /> y después elegir.
                                    </div>
                                <br />
                                <div class="text">
                                    De la misma manera se necesita elegir una ruta, la cual se elige de la lista.<br />
                                    hacer clic en la lista <img class="soloImg" src="d_images/Daily_Schedule_Template/Route.png" /> y después elegir.
                                    </div><br />
                                <div class="text">
                                    También es necesario introducir la hora en la que se comenzara el viaje.<br />
                                      <img src="d_images/Daily_Schedule_Template/time.png" />
                                    </div><br />
                                <div class="text">
                                    Después de seleccionar e introducir la información se necesita hacer clic en el botón <img class="soloImg" src="d_images/add.png" /> para guardar el viaje
                                    </div><br />
                               
                                   

                                    
                                
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseSix" data-toggle="collapse"><span class="icon"><i class="icon-minus-sign"></i></span>
                                <h5>Eliminar o editar un viaje</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseSix">
                            <div class="widget-content">
                                <div class="text">
                                    Es necesario cargar los datos de una operacion, de no tener un operacion cargada.
                                    </div><br />
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Daily_Schedule_Template/Loaded_with_info.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Schedule_Template/Loaded_With_info.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Como se muestra en la imagen anterior, en la fila de cada registro se encuentran los botones <br/><img class="soloImg" src="d_images/edit.png" /> <img class="soloImg" src="d_images/delete.png" />
                                    </div>
                                    <div class="text">
                                    Al hacer clic en el boton <img class="soloImg" src="d_images/delete.png" /> se eliminara el registro selecionado, a continuacion el sistema nos mostrara una alerta de confirmacion, como se muestra a continuacion.
                                        <br/><img class="soloImg" src="d_images/Daily_Schedule_Template/delete.png" />
                                    </div><br />
                                <div class="text">
                                    Al hacer clic en el boton <img class="soloImg" src="d_images/edit.png" /> se habilitaran los campos editables del registro tales como: <br/> <img class="soloImg" src="d_images/Daily_Schedule_Template/Route.png" /><br/><br/>
                                    <img src="d_images/Daily_Schedule_Template/time.png" />
                                    </div><br />
                                        <div class="text">
                                            Despues de editar lo correspondiente es necesario hacer clic en el boton <img class="soloImg" src="d_images/Daily_Schedule_Template/Update.png" /> para guardar los cambios.
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



