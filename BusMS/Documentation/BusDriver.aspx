<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusDriver.aspx.cs" Inherits="BusManagementSystem.Documentation.BusDriver" %>

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
                <h4>Camión / Conductor</h4>
                <div class="text">
                    El catálogo de Camión/Conductor es una relación entre el camión y el conductor, 
                   necesaria para controlar en que camión viene el conductor y los cambios que se hagan mediante la ruta. 
                   Así podemos controlar quien entra a las instalaciones de la compañía y verificar que el conductor no 
                   esté utilizando camiones no autorizados para su recorrido.
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
                                            <img src="d_images/BusDriver/busDriver.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BusDriver/busDriver.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    En este catálogo veremos la relación del camión/conductor, al proveedor que pertenece y en que turno está registrado.
                                    Los elementos en color amarillo son identificados como inactivos y las posibles causas son:
                                </div>
                                <div class="text">
                                    <li>El usuario inactivo la relación de forma directa
                                    </li>
                                    <li>El camión o el conductor fueron inactivados por medio del sistema
                                    </li>
                                    <li>El camión o el conductor fueron inactivados por el mismo usuario
                                    </li>
                                    <li>El camión o el conductor fueron agregados a la Lista negra del sistema
                                    </li>
                                </div>
                                <div class="text">
                                    Si un camión o un conductor son inactivados, automáticamente todas las relaciones que utilicen ese
                                     camión o conductor aparecerán como inactivas. 
                                    En esta pantalla podremos ver el estatus del camión y del conductor, que están
                                     señalados en las columnas de BusActive y DriverActive
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
                                            <img src="d_images/BusDriver/filter_BusDriver.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BusDriver/filter_BusDriver.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    La imagen siguiente muestra los dos filtros que la página tiene; Turno y Proveedor. 
                                   Los datos que se muestren dependen de los filtros que se elijan; por ejemplo al elegir el 1er turno,
                                   se desplegaran solo los registros camión/conductor que pertenezcan a ese turno, y según el proveedor que este seleccionado.
                                </div>
                                <div class="text">
                                    Si eres un usuario con rol de administrador, podrás seleccionar cualquiera de los proveedores que existen en el sistema.
                                   Para los usuarios con otros roles el filtro de proveedor se verá deshabilitado.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar un nuveo Camión/Conductor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BusDriver/new_BusDriver.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BusDriver/new_BusDriver.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Al insertar un nuevo registro, recuerda dar clic en el icono   
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    para ver los campos para capturar el nuevo registro, como se muestra en la siguiente imagen. 
                                </div>
                                <div class="text">
                                    Al presionar el botón  
                                    <img class="soloImg" src="d_images/add.png" />
                                    se habilitaran los campos para su captura: No. Camión, Conductor, Turno y por default el estatus estará marcado como activo.
                                    Los campos necesarios para insertar un nuevo registro son:

                                    <li>Proveedor</li>
                                    <li>Camión</li>
                                    <li>Conductor</li>
                                    <li>Turno</li>
                                    <li>Estatus (default: Activo)</li>

                                    <div class="text">
                                        Una vez capturados todos los campos necesarios, puedes dar clic en guardar
                                        <img class="soloImg" src="d_images/save.png" />
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un Camión/Conductor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BusDriver/edit_BusDriver.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BusDriver/edit_BusDriver.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Después de haber seleccionado el elemento que deseas editar. En este caso, 
                                    solo podrás editar el estatus de la relación camión/conductor, ya que los demás campos se mostraran deshabilitados. 
                                </div>
                                <div class="text">
                                    Al terminar tus cambios, presiona el botón de guardar 
                                    <img class="soloImg" src="d_images/save.png" />
                                </div>

                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar un Camión/Conductor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    La acción de eliminar un elemento en el catálogo de camión/conductor, no está habilitada. 
                                    Dado que estos registros mantienen relaciones con datos históricos que son necesarios existan para consultas posteriores.
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

