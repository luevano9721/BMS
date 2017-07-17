<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Global_Configuration.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                    05/17/2017
                </span>
                <h4>Configuración Global</h4>
                <div class="text">
                    Las configuraciones que se registren en esta sección tienen impacto a nivel global 
                    y por lo tanto es recomendable que solo usuarios con rol administrador accedan a la página
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-group"></i></span>
                                <h5>Lista de distribución</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">
                                
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Global_Config/distributionlist.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Global_Config/distributionlist.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                   En la lista de distribución podemos configurar a quienes llegarán los correos de las distintas 
                                    secciones que se muestran en la imagen. 
                                    Todos los correos que se generen en el módulo de operación diaria, se distribuirán 
                                    a los administradores y a los que estén registrados en esta lista.
                                    Igualmente para el módulo de alertas, mantenimiento y catálogos
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-legal"></i></span>
                                <h5>Configuración del IVA</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">
                                 <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Global_Config/IVA.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Global_Config/IVA.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Esta sección es para configurar el IVA con el que se calculará la facturación.
                                    Solo se ingresa el nuevo IVA que se desea configurar, damos click en 
                                     <img class="soloImg" src="d_images/save.png" />
                                    y los datos se verán actualizados en todo el sistema.
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
