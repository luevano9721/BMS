<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Daily_Operation_Changes.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Reporte de Cambios en la operación diaria</h4>
                <div class="text">
                    Reporte que muestra todos los transbordos, cancelaciones y fallas mecánicas 
                    registradas en la operación diaria
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
                                    La gráfica divide por proveedor y camión todos los tipos de cambios que hay en las operaciones del sistema.
                                    Cada transbordo, cancelación y fallas registradas, se verán resumidas en la gráfica
                                </div>

                                <div class="text">
                                   En el detalla viene especificado el tipo de cambio que se realizó y una sección de comentarios para 
                                    especificar los motivos del cambio.
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
