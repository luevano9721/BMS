<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buses_Pgs_Overtime_Report.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Informe de pasajeros , camiones y tiempo extra (Daily Transportation Report)</h4>
                <div class="text">
                    Este reporte tiene la finalidad de mostrar el total de pasajeros y camiones agrupados por fecha y turno
                    dependiendo de los filtros seleccionados.
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce el reporte</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">
                                
                            
                                <div class="text">
                                    El reporte cuenta con los siguientes filtros:
                                       <li><b>Fecha inicio:</b> Te permite especificar la fecha a partir de la cual se realizara la consulta
                                        de información con formato MM/DD/YYYY.
                                    </li>
                                    <li><b>Fecha fin:</b> Te permite especificar la ultima fecha que sera tomada en cuenta para realizar la consulta
                                        de información con formato MM/DD/YYYY.
                                    </li>
                                    <li><b>Turno:</b> Te permite filtrar la información según el turno deseado.
                                    </li>
                                   
                                    <li><b>Proveedor:</b> Te permite filtrar la información por proveedor
                                    </li>
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


