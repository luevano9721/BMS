<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_Payment_Format.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <span class="pull-right">05/18/2017
                </span>
                <h4>Reporte de facturación</h4>
                <div class="text">
                    Reporte que muestra todos los viajes realizados por los proveedores en un rango de fechas
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">

                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-upload-alt"></i></span>
                                <h5>Uso del reporte</h5>
                            </a>
                        </div>
                        <div class="collapse in " id="collapseFive">
                            <div class="widget-content">

                                <div class="text">
                                    <b>Concentrado  de facturación</b>
                                </div>
                                <div class="text">
                                    Muestra la cantidad de viajes que se han hecho por cada servicio, separados por el turno y 
                                    proveedor al que pertenecen, junto al total del costo sin IVA y con el IVA ya aplicado.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Invoice_Payment/Summary.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Invoice_Payment/Summary.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                   Dentro del concentrado verás los nombres de los proveedores en formato de un link 
                                    <img class="soloImg" src="d_images/Invoice_Payment/link.png" />
                                    Esto quiere decir que puedes dar click en el nombre y te llevará al formato de pago
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Invoice_Payment/invoiceFormat.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Invoice_Payment/invoiceFormat.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    <b>Formato de pago</b>
                                </div>
                                <div class="text">
                                    En este formato se cargarán los costos automáticamente, sin embargo puedes modificar algunos aspectos:
                                   Charge to y Po number pueden ser editados para indicar lo que se requiera vaya en esos campos.
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Invoice_Payment/filter_InvoiceFormat.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Invoice_Payment/filter_InvoiceFormat.png"><i class="icon-search"></i></a></div>
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