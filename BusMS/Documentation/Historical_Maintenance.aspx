<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historical_Maintenance.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <span class="pull-right">05/22/2017
                </span>
                <h4>Reporte de mantenimientos</h4>
                <div class="text">
                    Reporte que muestra todas las inspecciones que se han realizado a detalle
                    dentro de un rango de fechas
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
                                    En el encabezado se muestra el estatus del Mantenimiento y a la derecha una gráfica 
                                    con el número de revisiones inspeccionadas, las que no han sido inspeccionadas, cuantas tienen incidencia 
                                    media y cuantas son incidencia alta
                                </div>

                                <div class="text">
                                    En el cuerpo del reporte verás a detalle cada rubro revisado y los comentarios cuando se incurrio en alguna
                                    anomalia. Este reporte mostrará en dos páginas por camión que se contenga dentro de los filtros especificados.
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
