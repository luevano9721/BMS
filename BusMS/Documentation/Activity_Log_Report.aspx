<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activity_Log_Report.aspx.cs" Inherits="BusManagementSystem.Documentation.Vendor" %>

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
                    05/02/2017
                </span>
                <h4>Log de actividades</h4>
                <div class="text">
                    Este reporte muestra cada una de las actividades que se han realizado por el sistema en la base de datos.
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
                                    <li>Id Usuario: Todos los usuarios registrados con actividades en el sistema
                                    </li>
                                    <li>Modulo: Todos los modulos en los que se han realiado actividades
                                    </li>
                                    <li>Transacción: Insertar un nuevo registro, editar un registro existente o eliminar un registro
                                    </li>
                                    <li>Fechas: último día del mes pasada, hasta la fecha</li>

                                </div>
                                <div class="text">
                                    Puedes cambiar estos filtros y así ver información más precisa.
                                    Cabe mencionar que los datos aqui mostrados, principalmente en la columna 
                                    "nuevos valores" y "condición" contienen datos crudos, es decir 
                                    que no estan traducidos para la lectura comúun.

                                    
                                </div>
                                <div class="text">
                                    Sin embargo este reporte sirve para poderse comunicar el administrador con los
                                    desarrolladores o soporte y pueda solicitar se revise el log.
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
