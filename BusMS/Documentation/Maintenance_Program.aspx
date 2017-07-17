<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Maintenance_Program.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                    05/19/2017
                </span>
                <h4>Inspecion programada</h4>
                <div class="texto">
                    En este catálogo se crean las alertas de mantenimiento para los diferentes camiones,
                     las alertas indican los datos relacionados con el mantenimiento programado de los camiones. 
                    
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
                                            <img src="d_images/Maintenance_Program/Maintenance_Program.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Maintenance_Program/Maintenance_Program.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                   Las alertas tienen los siguientes elementos:
                                    <li>ID de proveedor
                                    </li>
                                    <li>ID de camión
                                    </li>
                                    <li>Alerta
                                    </li>
                                    <li>Comentarios
                                    </li>
                                    <li>Prioridad
                                    </li>
                                    <li>Fecha de expiración
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
                                    Puedes filtrar los datos por el proveedor correspondiente al  camión.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Filtro_Proveedor.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Filtro_Proveedor.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Puedes filtrar buscando alguna palabra y luego haciendo clic en el icono <img class="soloImg" src="d_images/lens.png" /> 
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Filtro_Buscar.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Filtro_Buscar.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar una nueva alerta de mantenimiento </h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <div class="text">
                                    Una vez presionado el icono   
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    veras los campos de mantenimiento programado, como se muestra en la siguiente imagen. 
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Maintenance_Program/Agregar_Maintenance_Program.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Maintenance_Program/Agregar_Maintenance_Program.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                   Al presionar el botón  
                                    <img class="soloImg" src="d_images/add.png" />
                                     se habilitaran los siguientes campos:
                                     <li>ID de proveedor
                                    </li>
                                    <li>ID de camión
                                    </li>
                                    <li>Alerta
                                    </li>
                                    <li>Comentarios
                                    </li>
                                    <li>Prioridad
                                    </li>
                                    <li>Fecha de expiración
                                    </li>
                                </div>
                                <div class="text">
                                    Después de seleccionar todos los campos, haz clic en el botón <img class="soloImg" src="d_images/save.png" />
                                    en el caso contrario hacer clic en el botón    <img class="soloImg" src="d_images/reset.png" />
                                    para cancelar el registro de una nueva alerta de mantenimiento.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un mantenimiento programado </h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                

                                <div class="text">
                                    Es necesario hacer clic en el botón <img class="soloImg" src="d_images/select.png" /> que se encuentra en la parte izquierda de cada registro
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Maintenance_Program/Grid.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Maintenance_Program/Grid.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                   Después de seleccionar algún elemento, se llenara el formulario con los datos correspondientes. Únicamente habilitara los campos que son editables.
                                </div>
                               <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Maintenance_Program/Form.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Maintenance_Program/Form.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
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
                                <h5>Eliminar una alarma de mantenimiento programado</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">                           
                                <div class="text">
                                   Este catálogo no permite eliminar las alertas, 
                                    es necesario ir al catálogo de soporte de alertas y cerrar la alerta correspondiente.
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


 

