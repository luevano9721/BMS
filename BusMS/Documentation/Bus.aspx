<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bus.aspx.cs" Inherits="BusManagementSystem.Documentation.Bus" %>

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
                <h4>Camiones</h4>
                <div class="text">
                    En el catálogo de camiones registraras todos los camiones que se utilizaran para los recorridos de la operación diaria.
                    Solo los camiones registrados en esta sección podrán ser utilizados en los demás módulos del sistema, 
                    por lo tanto debes de tomarlo en cuenta al momento de hacer tu <a href="Daily_Operation.aspx">operación diaria</a>.
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
                                            <img src="d_images/Bus/bus.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Bus/bus.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Estos son los datos que encontraras en esta pantalla:
                                </div>
                                <div class="text">
                                    <li>Id del camión
                                    </li>
                                    <li>Proveedor
                                    </li>
                                    <li>VIN
                                    </li>
                                    <li>Capacidad del camión
                                    </li>
                                    <li>Modelo
                                    </li>
                                    <li>Marca
                                    </li>
                                    <li>Año
                                    </li>
                                    <li>Placas
                                    </li>
                                    <li>Id de GPS
                                    </li>
                                    <li>Activo
                                    </li>
                                    <li>En lista negra
                                    </li>
                                </div>
                                <div class="text">
                                    En color amarillo estarán aquellos camiones que se encuentran inactivos, 
                                    ya sea por caso de mantenimiento y revisión o algún motivo de alerta.
                                </div>
                                <div class="text">
                                    En color rojo estarán aquellos camiones que se agregaron a la<a href="BlackList.aspx"> lista negra</a> 
                                    y no pueden ser utilizados nuevamente.
                                </div>
                                <div class="text">
                                    Para los elementos en color azul, significa que están bajo una alerta, para identificar 
                                    que alertas tiene activas ese elemento, necesitas entrar al módulo de <a href="AlertSupport.aspx">soporte de alertas </a>
                                    y filtrar por el número de camión que deseas consultar
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
                                    Puedes filtrar por el proveedor de cada camión que se tiene registrado. Si no tienes permisos de administrador,
                                     el filtro de proveedor aparecerá deshabilitado y solo se mostrara la información del proveedor con el que estas registrado.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar un nuveo Camión</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Bus/new_Bus.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Bus/new_Bus.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Después de dar clic en el icono    
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    se mostrará una pantalla como la siguiente
                                </div>
                                <div class="text">
                                   Presiona el botón   
                                    <img class="soloImg" src="d_images/add.png" />
                                    para habilitar los campos a registrar: 

                                    <li>No. De camión (No debe de contener    “-“, y acepta números y letras)
                                    </li>
                                    <li>Proveedor (Si eres administrador, podrás escoger cualquiera de los proveedores registrados) 
                                    </li>
                                    <li>VIN
                                    </li>
                                    <li>Capacidad del camión (Acepta solo números)
                                    </li>
                                    <li>Modelo del camión
                                    </li>
                                    <li>Marca del camión
                                    </li>
                                    <li>Año del camión
                                    </li>
                                    <li>Placas
                                    </li>
                                    <li>Id de GPS
                                    </li>
                                    <li>Activo (Por default está activo)
                                    </li>
                                    <li>En lista negra
                                    </li>

                                    <div class="text">
                                        Una vez capturados todos los campos necesarios, puedes dar clic en guardar
                                        <img class="soloImg" src="d_images/save.png" />
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un Camión</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                
                                <div class="text">
                                    En la pantalla podemos ver que todos los campos están disponibles a edición, con excepción del número del camión.  
                                    Todos los campos aceptan letras y números, menos el año y la capacidad del camión. 
                                </div>
                                <div class="text">
                                    Si cambias el camión a que este Bloqueado, una ventana se abrirá preguntando el motivo y
                                     comentarios acerca de esta acción, ya que al marcar un camión bloqueado, este será agregado a la<a href="BlackList.aspx"> lista negra</a>.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Bus/edit_Bus.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Bus/edit_Bus.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                       Al finalizar tus cambios, da clic en guardar 
                                        <img class="soloImg" src="d_images/save.png" />
                                    </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar un Camión</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    La acción de eliminar un elemento en el catálogo de camiones está habilitada, 
                                    sin embargo se podrán borrar solo aquellos camiones que no tienen registrados viajes en su <a href="Daily_Operation.aspx">operación diari</a>a, revisiones, 
                                    en relación camión/conductor o en alertas. 
                                    Estos registros mantienen relaciones con datos históricos que son necesarios existan para consultas posteriores.
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

