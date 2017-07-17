<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Daily_Operation_Report.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Reporte de operación diaria</h4>
                <div class="text">
                    Reporte que muestra todos los viajes realizados por turno y proveedor en un rango
                    de fechas (por default un día)
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
                                    <li>Proveedores: El primer proveedor de la lista (ordenados alfabéticamente)
                                    </li>
                                    <li>Turno: El primer turno de la lista (ordenados alfabéticamente)
                                    </li>
                                    <li>Estatus: Puede ser Abierto, Cerrado, Cancelado, Pendiente de aprobación o Pendiente de cancelación
                                    </li>
                                    <li>Fechas: 24 horas</li>
                                    <li>Grupos de Viajes: Estos se refieren a que el mismo turno pudo haber sido cargado varias veces en un
                                        rango de fechas y por tanto se agrupan en forma numérica "-" estatus
                                    </li>

                                </div>
                                <div class="text">
                                    Puedes cambiar estos filtros y así ver información más precisa.
                                </div>
                                <div class="text">
                                    La información mostrada en el reporte, es la correspondiente a la que se captura en la
                                     <a href="Daily_Operation.aspx">operación diaria</a> y sirve como referencia para conocer los datos 
                                    que se están generando para poder aprobar o cancelar la operación.
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
