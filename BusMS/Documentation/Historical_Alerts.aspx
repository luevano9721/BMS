<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historical_Alerts.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Reporte histórico de alertas</h4>
                <div class="text">
                    Reporte que muestra todas las alertas registradas en el sistema
                    durante todo el mes actual
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
                                    Por default el reporte carga los datos de la siguiente manera:
                                    <li>Proveedores: Todos los proveedores a los que tengas acceso para ver su información
                                    </li>
                                    <li>Alerta: Todos los tipos de alerta que existen en el sistema
                                    </li>
                                    <li>Estatus: Puede ser Abierto, Cerrado o en Proceso
                                    </li>
                                    <li>Prioridad: Alta, Media o Baja</li>
                                    <li>Fechas: El primer día del mes, hasta la fecha de hoy</li>

                                </div>
                                <div class="text">
                                    Puedes cambiar estos filtros y así ver información más precisa.
                                </div>
                                <div class="text">
                                    En el reporte se muestra una gráfica que resume el resultado de los filtros aplicados
                                    separado por proveedor y la prioridad de la alerta, te muestra cuantas alertas abiertas y cerradas existen.
                                    En la sección de detalle, se visualiza el detalle de cada alerta agrupada por; tipo de alerta, estatus y proveedor
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
