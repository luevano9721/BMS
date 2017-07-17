<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activities_Awaiting.aspx.cs" Inherits="BusManagementSystem.Documentation.Activities_Awaiting" %>

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
                <h4>Actividades Pendientes</h4>
               
                <div class="text">
                    En esta sección se administran las tareas pendientes de los administradores 
                    que se generan en los diferentes módulos, como lo son: <a href="Bus.aspx">Camiones</a>, <a href="Driver.aspx">conductores</a>, 
                    <a href="BusDriver.aspx">Relación Camión/Conductor</a> y <a href="Daily_Operation.aspx">Operación diaria</a>. 

                </div>
                <div class="text">
                    Esta página de sistema es únicamente utilizada por los administradores para aceptar 
                    o no los diferentes cambios generados por los usuarios del sistema, en los módulos ya antes mencionados.
                    En caso de no ser administrador, lo que podrás hacer en esta sección será solo de consulta.
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce la sección</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Activities_Awaiting/activitiesAwaiting.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Activities_Awaiting/activitiesAwaiting.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Al cargar la página, se te mostrará la siguiente información:

                                   <li>Fecha de la actividad .- Es la fecha en la que el usuario del sistema realizo una actividad</li>
                                    <li>Solicitado por.- Es el número de empleado de quien realizo la actividad que esta pendiente por aprobar</li>
                                    <li>Tipo .-	Es el tipo de actividad </li>
                                    <div class="text">
                                        <li>Actualizar: el usuario actualizo información existente en alguno de los módulos</li>
                                        <li>Insertar: el usuario creo un nuevo registro en alguno de los módulos</li>
                                        <li>Eliminar: el usuario elimino algún registro de alguno de los módulos</li>
                                    </div>

                                    <li>Modulo.- Se establece al módulo al que pertenece la actividad</li>
                                    <li>Nuevos valores.- Se visualiza la información de la actividad pendiente</li>
                                    <li>Aprobado por .- El número de empleado administrador quien aprobó la actividad</li>


                                    <li>Código (autogenerado)</li>
                                    <li>Elemento de alerta (numérico)</li>
                                    <li>Descripción </li>
                                    <li>Acción</li>
                                    <li>Periodo (cada cuantos días desea recibir el correo)</li>
                                    <li>Prioridad</li>
                                    <li>Fecha de creación</li>
                                    <li>Ultima vez que se envió correo</li>
                                    <li>Activo</li>
                                    <li>Fecha de apagado de alerta</li>
                                    <li>Proveedor al que aplica la alerta</li>
                                    <li>Tipo de alerta (Manual por default)</li>


                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-upload-alt"></i></span>
                                <h5>Filtrar información</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">


                                <div class="text">
                                    Con el icono    
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    puedes desplegar las opciones que existen para filtrar la informacion.
                                </div>
                                <div class="text">
                                    La sección cuenta con los siguientes filtros:
                                </div>
                                <div class="text">
                                    Fechas:
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Activities_Awaiting/filterDate_Alerts.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Activities_Awaiting/filterDate_Alerts.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                </div>
                                <div class="text">
                                    Este filtro sirve para mostrar las actividades que se encuentran estre una fecha y otra. 
                                </div>
                                <div class="text">
                                    Usuario:
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Activities_Awaiting/filterUsers_Alerts.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Activities_Awaiting/filterUsers_Alerts.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                </div>
                                <div class="text">
                                    Este filtro nos muestra a todos los usuarios que realizaron actividades y 
                                    podemos filtrar las actividades para cualquier de los usuarios en la lista. 
                                </div>
                                <div class="text">
                                    Transacción:
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Activities_Awaiting/filterTransaction_Alerts.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Activities_Awaiting/filterTransaction_Alerts.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                </div>
                                <div class="text">
                                    Este filtro nos sirve para escoger el tipo de transacción que deseamos ver (Insertar, actualizar, eliminar).
                                </div>
                                <div class="text">
                                    Estatus:
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Activities_Awaiting/filterStatus_Alerts.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Activities_Awaiting/filterStatus_Alerts.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                </div>
                                <div class="text">
                                    Este filtro permite obtener todas las actividades dependiendo del estatus 
                                    en el que se encuentren: Actividades pendientes, cancelada y confirmada.
                                </div>
                                <div class="text">
                                    Después de elegir los filtros, es necesario hacer clic en el botón 
                                    <img class="soloImg" src="d_images/Activities_Awaiting/applyfilters.png" />
                                    para hacer que nos muestre la información que elegimos mediante los filtros.
                                    Este botón se encuentra en sección de filtros al extremo derecho.

                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Confirmar una actividad</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <div class="text">
                                    Permitir que la actividad que realizo el usuario se efectué en el sistema.
                                </div>
                                <div class="text">
                                    En la sección de actividades pendientes de aprobación, en la parte izquierda de cada 
                                    registro de actividades se encuentra el botón   
                                    <img class="soloImg" src="d_images/confirm.png" />
                                    el cual es necesario hacer clic si se está conforme con aprobar la actividad.
                                </div>


                                <div class="text">
                                    Después de hacer clic en sistema nos mostrara una ventana emergente, 
                                    como medida de seguridad para que el administrador tenga la posibilidad de retractarse de ser necesario.

                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/confirmActivity.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/confirmActivity.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Cancelar una actividad</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">

                                <div class="text">
                                    En la sección de actividades pendientes de aprobación, en la parte izquierda
                                    de cada registro de actividades se encuentra el botón   
                                    <img class="soloImg" src="d_images/cancel.png" />
                                    el cual es necesario hacer clic si se está conforme con aprobar la actividad.
                                </div>
                                <div class="text">
                                    Después de hacer clic en sistema nos mostrara una ventana emergente, como medida de 
                                    seguridad para que el administrador tenga la posibilidad de retractarse de ser necesario.
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/cancelActivity.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/cancelActivity.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Visualizar el reporte de operación diaria</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">

                                <div class="text">
                                    Esta opción está diseñada para que el administrador pueda 
                                    visualizar la actividad de operación diaria que se dispone a cancelar o eliminar, según sea el caso.
                                </div>
                                 <div class="text">
                                    Para visualizar el reporte es necesario hacer clic en el icono   
                                      <img class="soloImg" src="d_images/lens.png" />
                                     el cual hace que desplaye una ventana emergente que contiene el reporte correspondiente a la actividad seleccionada.
                                </div>
                                <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Activities_Awaiting/dailyoperation_Preview.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Activities_Awaiting/dailyoperation_Preview.png"><i class="icon-search"></i></a></div>
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



