<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historical_Invoice.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Reporte Histórico de facturación</h4>
                <div class="text">
                    Reporte que muestra cada uno de los viajes que se han cobrado
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
                                    <li>Proveedores: todos los proveedores en el sistema
                                    </li>
                                    <li>Turno: todos los turnos en el sistema
                                    </li>
                                    <li>Fechas: Del Lunes pasado al lunes de esta semana </li>
                                    <br />
                                    Este reporte es meramente informativo y para poder detectar anomalías de cobro.

                                </div>
                                <div class="text">
                                    Si se usan los mismos parámetros para los otros reportes:
                                    <li><a href="../Reportes/R_Invoice_Payment.aspx">Formato de facturación</a></li>
                                    <li><a href="../Reportes/R_DailyOperation.aspx">Reporte de operación diaria</a></li>

                                    <br />
                                    La información debe de ser consistente.
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