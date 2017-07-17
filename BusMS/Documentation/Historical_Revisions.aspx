<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historical_Revisions.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                    05/23/2017 
                </span>
                <h4>Reporte historico de inspecciones</h4>
                <div class="text">
                    Reporte que muestra las inspecciones de mantenimiento abiertas o cerradas durante el ultiimo mes.
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Uso del reporte</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">
                                
                             

                                <div class="text">
                                    Por default el reporte carga los datos de la siguiente manera:
                                   El proveedor tiene los siguientes datos:
                                    <li>Fechas: El primer día del mes, hasta la fecha de hoy
                                    </li>
                                    <li>Proveedores: Todos los proveedores a los que tengas acceso para ver su información
                                    </li>
                                    <li>Estatus: Puede ser Abierto o Cerrado
                                    </li>
                                    <li>Turno: Te permite filtrar la información según el turno deseado
                                    </li>
                                    <br />
                                    Puedes cambiar estos filtros y así ver información más precisa.
                                    En el reporte se muestra una gráfica que resume el resultado de los filtros aplicados separado por proveedor, 
                                    te muestra cuantas inspecciones abiertas o cerradas existen. 
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


