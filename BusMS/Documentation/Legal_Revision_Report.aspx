<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Legal_Revision_Report.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                <h4>Reporte de Revisión Legal</h4>
                <div class="text">
                    Reporte que muestra todas las inspecciones realizadas a los camiones de los proveedores y que tienen estatus cerrado
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
                                    Las inspecciones marcadas con <img class="soloImg" src="d_images/Legal_Revision/check.png" />
                                    indican que ese rubro fue marcado como inspeccionado.
                                    Cuando un rubro no haya sido inspeccionado se marcará con <img class="soloImg" src="d_images/Legal_Revision/noCheck.png" />
                                </div>
                                <div class="text">
                                   Habrá inspecciones que generen alguna anomalía, el detalle de la misma se encuentra al dar click en la lupa
                                    <img class="soloImg" src="d_images/Legal_Revision/lens.png" />
                                   La imagen siguiente se visualizará con el detalle de la anomalía
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Legal_Revision/detail.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Legal_Revision/detail.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                
                                <div class="text">
                                    Si lo deseas puedes dar click en la imagen para verla a más detalle:
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Legal_Revision/imgDetail.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Legal_Revision/imgDetail.png"><i class="icon-search"></i></a></div>
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