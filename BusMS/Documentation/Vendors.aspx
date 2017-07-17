<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vendors.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendors" %>

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
                <h4>Proveedor</h4>
                <div class="text">
                    En este catálogo se agregan los distintos proveedores de camiones que tendrá el sistema.
                    Debido a que los demás catálogos dependen de este, es necesario que se capture la información 
                    de los proveedores antes que cualquier otra información.
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
                                            <img src="d_images/Vendor/vendor.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Vendor/vendor.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                   El proveedor tiene los siguientes datos:
                                    <li>Identificador de proveedor
                                    </li>
                                    <li>Nombre
                                    </li>
                                    <li>Dirección
                                    </li>
                                    <li>Teléfono
                                    </li>
                                    <li>RFC
                                    </li>
                                    <li>Nombre de contacto
                                    </li>
                                    <li>Nombre Fiscal
                                    </li>
                                    <li>Edificio de facturación
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
                                    Este catálogo no cuenta con filtros como tal, sin embargo existe una barra de búsqueda, introduces el nombre del proveedor, 
                                    nombre de contacto o el edificio de facturación y va mostrando los resultados parecidos al texto escrito.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar un nuevo proveedor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Vendor/new_Vendor.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Vendor/new_Vendor.png"><i class="icon-search"></i></a></div>
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
                                    <li>Identificador de proveedor (Tres letras significativas)
                                    </li>
                                    <li>Nombre
                                    </li>
                                    <li>Dirección
                                    </li>
                                    <li>Teléfono
                                    </li>
                                    <li>RFC
                                    </li>
                                    <li>Nombre de contacto
                                    </li>
                                    <li>Nombre Fiscal
                                    </li>
                                    <li>Edificio de facturación
                                    </li>

                                </div>
                                <div class="text">
                                    Después de seleccionar todos los campos, haz clic en guardar, <img class="soloImg" src="d_images/save.png" />
                                    en el caso contrario hacer clic en el botón    <img class="soloImg" src="d_images/reset.png" />
                                    para cancelar el registro de un nuevo proveedor.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un proveedor</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Vendor/edit_Vendor.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Vendor/edit_Vendor.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Selecciona el proveedor al que deseas editar su información y a continuación veras 
                                    la pantalla con los mismos campos habilitados 
                                    que al insertar un nuevo proveedor, con la diferencia que el ID del proveedor no puede ser editado.
                                </div>
                               
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
                                <h5>Eliminar un proveedor</h5>
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
                                <div class="text">
                                    A pesar de que esta acción está habilitada, si el proveedor tiene alguna <a href="Fare.aspx">tarifa</a> registrada, 
                                    <a href="Driver.aspx">conductor</a>, <a href="Bus.aspx">camión</a> o algún otro registro ligado a este proveedor, la eliminación no será exitosa.
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


