<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditPassengers.aspx.cs" Inherits="BusManagementSystem.Documentation.AuditPassengers" %>

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
                <span class="pull-right">05/16/2017
                </span>
                <h4>Auditar Passajeros</h4>

                <div class="text">
                    Sección dedicada para el proceso de auditar autobuses para verificar que en efecto, 
                    se tengan la cantidad de pasajeros que reportan.
                </div>

            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Cargar lista de camiones a auditar</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">
                                <div class="text">
                                    La lista de camiones que se carga dependen de dos factores;
                                    El proveedor y el turno que quieres revisar
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AuditPassengers/filter_Audit1.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AuditPassengers/filter_Audit1.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Elige el tipo de turno que quieres cargar para que se habilite el campo de turno
                                    y puedas seleccionar aquel que requieras.
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AuditPassengers/filter_Audit2.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AuditPassengers/filter_Audit2.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Presiona el boton de cargar 
                                    <img class="soloImg" src="d_images/AuditPassengers/loadInfo.png" />
                                    y todos los camiones registrados para ese Proveedor y turno, que no han sido auditados
                                    aparecerán, ordenados del más reciente al menos.                       

                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AuditPassengers/load_audit.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AuditPassengers/load_audit.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class=" icon-ok-circle"></i></span>
                                <h5>Auditar camión</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">


                                <div class="text">
                                    Con el icono    
                                    <img class="soloImg" src="d_images/AuditPassengers/edit_Audit.png" />
                                    se habilitará el campo para que ingreses los pasajeros que en realidad tiene el camion
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/AuditPassengers/enterValue_Audit.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/AuditPassengers/enterValue_Audit.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Da click en el boton    
                                    <img class="soloImg" src="d_images/AuditPassengers/save_Audit.png" />
                                    para guardar tus cambios y automaticamente
                                  el camión que acabas de auditar ya no aparecerá en la lista.
                                    Si deseas visualizar los camiones auditados, puedes ir al reporte de <a href="Audit_Historical.aspx">Historico de auditorias</a>
                                </div>
                                <div class="text">
                                    En caso que no quieras editar ese camión, puedes dar click en el boton de cancelar 
                                    <img class="soloImg" src="d_images/AuditPassengers/cancel_Audit.png" />
                                    y los campos volveran a la forma inicial
                                   

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



