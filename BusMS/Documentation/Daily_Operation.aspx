<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Daily_Operation.aspx.cs" Inherits="BusManagementSystem.Documentation.Daily_Operation" %>


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
                <h4>Operación diaria</h4>
                <div class="text">
                    Este módulo te permite crear una operación diaria para un turno en específico.
                </div>


            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce la página</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">

                            <div class="widget-content">


                                <div class="text">
                                    El módulo cuenta con los siguientes filtros:
                                    <ul>
                                        <li><b>Turnos disponibles:</b> Esta sección te permite elegir el turno a cargar para realizar la operación diaria.  </li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_turnos.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_turnos.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                        <li><b>Lista de observación camión/conductor:</b> Esta sección te permite ver los camiones o conductores que se encuentran bajo las siguientes condiciones:
                                          <ul class="thumbnails">
                                              <li class="span2">
                                                  <a>
                                                      <img src="d_images/Daily_Operation/DO_observacion.png" />
                                                  </a>
                                                  <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_observacion.png"><i class="icon-search"></i></a></div>
                                              </li>
                                          </ul>
                                            <ol>
                                                <li>-	Inactivos/Bloqueados: Camiones y conductores que fueron desactivados o bloqueados y que están asociados al turno que se ha cargado.</li>
                                                <li>-	Revisión: Camiones que se encuentran bajo inspección, es decir, se ha generado una revisión para dicho camión en el módulo de Inspección y esta revisión aún se encuentra abierta.</li>
                                                <li>-	Bajo alerta: Camiones o conductores asociados al turno cargado que tiene una alerta relacionada y se encuentra abierta. Esta alerta puede ser atendida en el módulo de soporte de alertas.</li>
                                            </ol>
                                            <li><b>CheckPoints:</b> Esta sección muestra la información de los 3 puntos (Inicio, medio y fin) que se capturan durante un viaje para el turno cargado y es donde se realiza la mayor parte de las acciones correspondientes a la operación.</li>
                                            <ul class="thumbnails">
                                                <li class="span2">
                                                    <a>
                                                        <img src="d_images/Daily_Operation/DO_checkpoints.png" />
                                                    </a>
                                                    <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_checkpoints.png"><i class="icon-search"></i></a></div>
                                                </li>
                                            </ul>
                                            <li><b>Confirmación/cancelación de la operación:</b> Esta sección muestra dos botones con la posibilidad de confirmar la operación diaria o cancelarla. En caso de no ser administrador, esta actividad requiere la aprobación de un administrador en el módulo de Actividades pendientes.</li>
                                            <ul class="thumbnails">
                                                <li class="span2">
                                                    <a>
                                                        <img src="d_images/Daily_Operation/DO_confirmation.png" />
                                                    </a>
                                                    <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_confirmation.png"><i class="icon-search"></i></a></div>
                                                </li>
                                            </ul>
                                            <li><b>Descripción de colores:</b> Esta sección muestra una breve descripción de lo que representa cada color que puede asignarse a un viaje (de forma automática y por sistema) cuando es cargado un turno. Estos viajes de colores se pueden observar en la sección de CheckPoints.</li>
                                            <ul class="thumbnails">
                                                <li class="span2">
                                                    <a>
                                                        <img src="d_images/Daily_Operation/DO_description.png" />
                                                    </a>
                                                    <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_description.png"><i class="icon-search"></i></a></div>
                                                </li>
                                            </ul>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <%-- </div>--%>
                        <%--                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne2" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Cargar un turno</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne2">

                            <div class="widget-content">

                                <div class="text">
                                    Para cargar un turno y comenzar a trabajar en él, es necesario dirigirnos a la sección de turnos disponibles. Una vez ahí, observaremos una serie de opciones como las siguientes: 
                                   <li><b>Proveedor:</b> Muestra la lista de proveedores asignados al usuario que está visualizando la página. En caso de contar con un perfil interno, podrá visualizar todos los proveedores registrados. Si se tiene asociado un perfil externo, automáticamente visualizara únicamente el proveedor asociado a él sin posibilidad de realizar un cambio. </li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Daily_Operation/DO_proveedor.png" />
                                            </a>
                                            <div class="actions">
                                                <a class="lightbox_trigger" href="d_images/Daily_Operation/DO_proveedor.png"><i class="icon-search"></i></a>
                                            </div>
                                        </li>
                                    </ul>
                                    <li><b>Tipo de turno:</b> Actualmente, únicamente existen 2 tipos de tuno; Turno de entrada y de salida respectivamente. El tipo entrada se utiliza para aquellos turnos que contiene viajes que tienen como origen cualquier lugar (Stop point o punto de parada) en la ciudad y como destino Foxconn PCE. El tipo salida se utiliza para aquellos turnos que contienen viajes que tienen como origen Foxconn PCE y como destino cualquier lugar en la ciudad. </li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Daily_Operation/DO_tipoturno.png" />
                                            </a>
                                            <div class="actions">
                                                <a class="lightbox_trigger" href="d_images/Daily_Operation/DO_tipoturno.png"><i class="icon-search"></i></a>
                                            </div>
                                        </li>
                                    </ul>
                                    <li><b>Nombre de turno:</b> Este campo se habilita una vez que seleccionamos un proveedor y tipo de turno. Básicamente es el turno que estamos deseando cargar. </li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Daily_Operation/DO_nombreturno.png" />
                                            </a>
                                            <div class="actions">
                                                <a class="lightbox_trigger" href="d_images/Daily_Operation/DO_nombreturno.png"><i class="icon-search"></i></a>
                                            </div>
                                        </li>
                                    </ul>
                                    <li><b>Cargar datos:</b> Este botón permite cargar la operación para el turno seleccionado. </li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Daily_Operation/DO_cargardatos.png" />
                                            </a>
                                            <div class="actions">
                                                <a class="lightbox_trigger" href="d_images/Daily_Operation/DO_cargardatos.png"><i class="icon-search"></i></a>
                                            </div>
                                        </li>
                                    </ul>
                                    <li><b>Cambiar turno:</b> Este botón permite cambiar el turno seleccionado y cargar su operación correspondiente. </li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Daily_Operation/DO_cambiarturno.png" />
                                            </a>
                                            <div class="actions">
                                                <a class="lightbox_trigger" href="d_images/Daily_Operation/DO_cambiarturno.png"><i class="icon-search"></i></a>
                                            </div>
                                        </li>
                                    </ul>
                                    <li><b>Exportar a Excel:</b> Este botón permite exportar la información actual a un documento de Excel. </li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Daily_Operation/DO_ExportExcel.png" />
                                            </a>
                                            <div class="actions">
                                                <a class="lightbox_trigger" href="d_images/Daily_Operation/DO_ExportExcel.png"><i class="icon-search"></i></a>
                                            </div>
                                        </li>
                                    </ul>
                                    <h5>Pasos a llevar a cabo para cargar una operación: </h5>
                                    <ol>
                                        <li>Se debe de seleccionar un proveedor</li>
                                        <li>Se debe de seleccionar un tipo de turno</li>
                                        <li>Se debe de seleccionar el turno deseado</li>
                                        <li>Se debe de presionar el botón cargar datos</li>
                                        <li>Si todo ha salido correcto, debemos de visualizar información el CheckPoint 1. En caso de que haya ocurrido un error, favor de visualiza la sección de “Posibles errores que pueden ocurrir” dentro de esta misma sección de ayuda.</li>
                                    </ol>
                                </div>
                            </div>
                        </div>

                        <%--  </div>--%>
                        <%-- <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne3" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Llenar CheckPoint 1</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne3">

                            <div class="widget-content">

                                <div class="text">
                                    Una vez que se ha cargado la operación de un turno, podemos comenzar con el llenado de la información en el CheckPoint 1. Visualizaremos una lista de viajes que contienen la siguiente información:
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Daily_Operation/DO_InfoCheckpoint1.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_InfoCheckpoint1.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li><b>Bus_Driver:</b>Relación Camión/Conductor que tiene un ID único. Se utiliza para “casar” un camión con un conductor y mantener una integridad de la información utilizada para dicho turno.</li>
                                        <li><b>Bus_ID:</b> Numero del camión.</li>
                                        <li><b>Driver:</b> Nombre del conductor.</li>
                                        <li><b>Route:</b> Es la unión de dos puntos (uno de inicio y uno final) mejor conocido como ruta o recorrido.</li>
                                        <li><b>Check_Point_Time:</b> Hora ya establecida en que se debe de comenzar el recorrido</li>
                                        <li><b>Comments:</b> En caso de que exista algún comentario se puede agregar en este campo.</li>
                                        <li><b>Start Time:</b> Tiempo de inicio (real) en que se comenzó con el recorrido. En caso de que sea diferente al ya establecido, la celda se pintara de color.</li>
                                        <li><b># Pass:</b> Cantidad de pasajeros con que se inició el recorrido.</li>
                                    </ul>
                                    <h5>Pasos a llevar a cabo para llenar la información de un viaje: </h5>
                                    <ol>
                                        <li>Localizar el viaje deseado y presionar el botón <b>Edit </b>
                                            <img class="soloImg" src="d_images/Daily_Operation/Do_Edit.png" /></li>
                                        <li>Automáticamente se habilitaran los campos para introducir la información. Ingresar la hora en que se inició el viaje en el campo Start Time</li>
                                        <li>Ingresar la cantidad de pasajeros en el campo # Pass</li>
                                        <li>Ingresar los comentarios correspondientes (en caso de que existan) en el campo Comments.</li>
                                        <li>Presionar el botón Save en caso de que se haya terminado y la información sea correcta.<img class="soloImg" src="d_images/Daily_Operation/Do_save.png" /></li>
                                        <li>En caso de requerir cancelar la actividad, se debe de presionar el botón Cancel.
                                            <img class="soloImg" src="d_images/Daily_Operation/Do_cancel.png" /></li>
                                        <li>Visualizaremos un mensaje similar al siguiente si la información se guardó correctamente: En caso de que haya ocurrido un error, favor de validar la sección “Posibles errores que pueden ocurrir” dentro de esta misma página de ayuda.</li>
                                        <li>La información se refrescara y podremos visualizar el viaje pintado en color verde indicando que ha sido llenado.</li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_recordSaved.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_recordSaved.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                    </ol>
                                    Repetir esta actividad para todos los viajes contenidos en el CheckPoint 1.
                                    </br>
                                  <b>Nota: </b>Es posible cambiar la relación camión/conductor y la ruta en caso de que sean incorrectas al momento de editar un viaje.

                                </div>
                            </div>
                        </div>

                        <%--</div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne4" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Llenar CheckPoint 2</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne4">

                            <div class="widget-content">

                                <div class="text">
                                    El CheckPoint 2 se llena de la misma forma que el punto anterior. Sin embargo existen algunas diferencias:
                                  
                                      <ul>
                                          <li>Es necesario especificar un valor para la opción Stop Point. Aquí únicamente existen dos opciones, las cuales son puntos medios; Anapra y Pemex respectivamente. </li>
                                          <li>La información contendida aquí se va llenando de forma automática conforme se vaya llenando el CheckPoint anterior, que en este caso sería el 1. Por ello, no se visualizan todos los viajes por que deben de pasar esta validación.</li>
                                          <li>El tiempo indicado en CheckPoint Time debe de ser diferente al indicado en el punto anterior, de lo contrario no podrá guardar la información.</li>
                                          <li>A partir de este punto, se puede finalizar un viaje. Por ejemplo, un camión que haya llegado a Pemex con 4 pasajeros y transbordara a otro camión que llegue hasta Foxconn PCE se debe de indicar que su viaje termino en dicho punto para que pueda ser facturado de forma correcta.</li>
                                          <li>Una vez que se ha llenado este punto, no es posible realizar cambios sobre el CheckPoint anterior.</li>
                                          <li>Si la cantidad de pasajeros introducida es menor a la que se indicó en el punto anterior (en este caso el CheckPoint 1) se añadirá un comentario de forma automática similar al siguiente:</li>
                                          <ul class="thumbnails">
                                              <li class="span2">
                                                  <a>
                                                      <img src="d_images/Daily_Operation/DO_Pasajeros.png" />
                                                  </a>
                                                  <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_Pasajeros.png"><i class="icon-search"></i></a></div>
                                              </li>
                                          </ul>
                                      </ul>

                                </div>
                            </div>
                        </div>

                        <%--</div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne5" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Llenar CheckPoint 3</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne5">

                            <div class="widget-content">

                                <div class="text">
                                    El CheckPoint 2 se llena de la misma forma que el punto anterior. Sin embargo existen algunas diferencias:
                                  <ul>
                                      <li>Al ser el punto final, únicamente está disponible la opción PCE- Foxconn para Stop Point.</li>
                                      <li>Puesto que aquí se indica cuando el camión ha llegado a la planta, no existen transbordos. En caso de requerirlo, se debe de realizar en el CheckPoint correcto.</li>
                                      <li>Una vez que se llena este punto, ya no es posible realizar cambios sobre los puntos anteriores.</li>

                                  </ul>


                                </div>
                            </div>
                        </div>

                        <%-- </div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne6" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Agregar un camión o viaje que no se encuentra en la lista</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne6">

                            <div class="widget-content">

                                <div class="text">
                                    Cuando se carga un turno, la lista de viajes que aparecen disponibles a llenar en el CheckPoint 1 viene de la información dada de alta en la plantilla de operación diaria. Esta plantilla se creó para no tener que estar agregando los viajes manualmente cada vez que se cargue la operación de un turno. Es normal que pueda no existir un camión en la lista predefinida por cambios imprevistos o de última hora. En estos casos, se debe de agregar la relación camión/conductor de la siguiente forma:
                                  <ul class="thumbnails">
                                      <li class="span2">
                                          <a>
                                              <img src="d_images/Daily_Operation/DO_Header.png" />
                                          </a>
                                          <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_Header.png"><i class="icon-search"></i></a></div>
                                      </li>
                                  </ul>

                                    <ol>
                                        <li>Ubicar la cabecera de la tabla en el CheckPoint 1 , tal como la imagen.</li>
                                        <li>Elegir una relación camión/conductor disponible en el combo</li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_selectBD.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_selectBD.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                        <li>Elegir una ruta o recorrido disponible en el combo</li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/Do_Route.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/Do_Route.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                        <li>Indicar el tiempo oficial en el que debe de iniciar su viaje desde dicho punto</li>
                                        <li>Indicar comentarios (si es que se tienen)</li>
                                        <li>Si todos los campos se han llenado de forma correcta, presionar el botón Add. En caso de que un campo este vacío o no corresponda al tipo de dato que debe de llevar, se le será mostrado el error a corregir.</li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_addRecord.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_addRecord.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                        <li>Si la información se dio de alta de forma correcta , se podrá observar un mensaje como el siguiente:</li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_msgOk.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_msgOk.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                        <li>La tabla será refrescada y podremos visualizar que el elemento que fue agregado ya se encuentra en ella.</li>
                                    </ol>
                                    <br></br>
                                    <b>Nota:</b> En caso de no encontrar la relación Camión/Conductor o la ruta en los combos de opciones, favor de revisar la sección “Posibles errores que pueden ocurrir” de esta misma página.

                                </div>
                            </div>
                        </div>

                        <%--</div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne7" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Transbordar a otro camión</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne7">

                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Daily_Operation/Do_TransferWindow.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/Do_TransferWindow.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    <ol>
                                        <li>Para transbordar los pasajeros a otro viaje (relación camión/conductor con ruta distinta) se debe de presionar el siguiente botón. 
                                            <img class="soloImg" src="d_images/Daily_Operation/Do_transfer.png" /></li>
                                        <li>Aparecerá la ventana de transbordo,  en la cual se debe de seleccionar la nueva relación camión/conductor a la cual se realizara el transbordo. Únicamente se mostraran los camiones que tienen un viaje activo en ese momento para la operación que se está trabajando. </li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_relationTrans.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_relationTrans.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                        <li>Se debe de seleccionar el motivo o razón por el cual se está realizando el transbordo.</li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_reasons.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_reasons.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>

                                        <li>Se deben especificar los comentarios adicionales para dar más detalle de lo sucedido</li>
                                        <li>Presionar el botón de transferir. Si todo ha salido bien , se mostrara el siguiente mensaje de confirmación:</li>
                                        <ul class="thumbnails">
                                            <li class="span2">
                                                <a>
                                                    <img src="d_images/Daily_Operation/DO_msgOkTrans.png" />
                                                </a>
                                                <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_msgOkTrans.png"><i class="icon-search"></i></a></div>
                                            </li>
                                        </ul>
                                    </ol>

                                    Consideraciones a tomar en cuenta para el transbordo de pasajeros:
                                    <ul>
                                        <li>Si la relación camión/conductor no aparece tanto en el combo o no se tiene agregada en la plantilla, favor de consultar la sección de “Posibles errores que pueden ocurrir” dentro de esta página.</li>
                                        <li>Para poder realizar un transbordo es necesario que el viaje haya comenzado al menos con 1 pasajero. Si esto no se cumple, automáticamente el sistema bloqueara la opción de transbordo.</li>
                                        <li>La suma de pasajeros al realizar el transbordo se hace de forma automática.</li>
                                        <li>Cuando se realice un transbordo, el antiguo camión no desaparecerá de la lista. Este aparecerá en color amarillo (el especificado en la descripción de colores). Además, se indicara a que camión se realizó el transbordo de la siguiente manera (y automáticamente ese viaje quedara marcado como final y podrá ser tomado o no para facturación dependiendo del turno y las reglas de negocio especificadas al sistema.</li>
                                        <li>Los transbordos para el CheckPoint 1 se contemplan a partir del origen o punto inicial, al punto medio, por ejemplo Parajes – Pemex.</li>
                                        <li>Los transbordos para el CheckPoint 2 se contemplan a partir del punto medio al punto final, por ejemplo Pemex – Foxconn PCE. Por tal motivo, el CheckPoint 3 no tiene opción a transbordo.</li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <%--</div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne8" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Finalizar un viaje antes del CheckPoint 3</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne8">

                            <div class="widget-content">

                                <div class="text">
                                    La opción de finalizar un viaje solo está disponible en el CheckPoint 2. Aquí se puede indicar al sistema cuando que el viaje ha terminado en Anapra o Pemex por algún motivo. Los pasos a seguir para finalizar un viaje son los siguientes:
                                     <ul>
                                         <li>Llenar la información correspondiente al viaje en el CheckPoint 2</li>
                                         <li>Presionar sobre el botón End Route 
                                             <img class="soloImg" src="d_images/Daily_Operation/DO_EndPoint.png" /></li>
                                         <li>Aparcera una ventana de confirmación, a la cual debemos de indicar que estamos de acuerdo con realizar ese cambio.</li>
                                         <ul class="thumbnails">
                                             <li class="span2">
                                                 <a>
                                                     <img src="d_images/Daily_Operation/DO_ConfirmEnd.png" />
                                                 </a>
                                                 <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_ConfirmEnd.png"><i class="icon-search"></i></a></div>
                                             </li>
                                         </ul>
                                         <li>Si todo ha salido correcto , se mostrara un mensaje de éxito como el siguiente:</li>
                                         <ul class="thumbnails">
                                             <li class="span2">
                                                 <a>
                                                     <img src="d_images/Daily_Operation/DO_msgEnd.png" />
                                                 </a>
                                                 <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_msgEnd.png"><i class="icon-search"></i></a></div>
                                             </li>
                                         </ul>
                                     </ul>
                                    <br>
                                    Consideraciones a tomar en cuenta para finalizar un viaje:
                                       <ul>
                                           <li>La cantidad de pasajeros no puede ser mayor a 0. Si el viaje tiene pasajeros, entonces para poder finalizarlo se debe de realizar un <b>transbordo</b>.</li>
                                           <li>La opción de finalizar viaje en el CheckPoint 2 fue creada con finalidad de poder terminar un viaje al llegar al punto medio y que no tiene pasajeros , especialmente cuando se trata de turnos de fines de semana (al no haber pasajeros , no se puede realizar un transbordo y el viaje no podría finalizarse).</li>
                                       </ul>
                                </div>
                            </div>
                        </div>

                        <%--</div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne9" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Confirmar/Cancelar una operación diaria</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne9">

                            <div class="widget-content">

                                <div class="text">
                                    Para poder confirmar o cancelar una operación diaria, se debe de dirigir a la penúltima sección de la página en la que encontraremos dos botones como los siguientes:
                                       <ul class="thumbnails">
                                           <li class="span2">
                                               <a>
                                                   <img src="d_images/Daily_Operation/DO_confirmation.png" />
                                               </a>
                                               <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_confirmation.png"><i class="icon-search"></i></a></div>
                                           </li>
                                       </ul>
                                    <ul>
                                        <li>Confirmar operación diaria: Cierra la operación cargada. Primeramente el sistema valida que todos los viajes hayan sido terminados de forma correcta para poder cerrar y confirmar la operación. Una vez que se realiza esto, se crean los datos correspondientes a la facturación y pueden consultarse en los reportes correspondientes. </li>
                                        <li>Cancelar operación diaria: Cancela la operación cargada. La información que se haya ingresado se perderá y se podrá cargar la operación del turno nuevamente. Esta función es ideal para cuando se ha cargado una operación en un horario incorrecto o por alguna razón se desea empezar de nuevo. </li>
                                    </ul>
                                    <h5>Consideraciones a tomar en cuenta para el cierre de la operación:</h5>
                                    <ul>
                                        <li>La operación que se confirme o se cancele es una actividad que debe de ser aprobada por el administrador de la aplicación. Por esta razón, la actividad queda pausada hasta que el administrador autorice la confirmación o cancelación en el módulo de Actividades pendientes.</li>
                                        <li>En caso de que el administrador sea quien este llenando la operación, no es necesaria la autorización de otro administrador del sitio.</li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <%-- </div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne10" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Funciones adicionales</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne10">

                            <div class="widget-content">

                                <div class="text">
                                    La aplicación cuenta con funciones adicionales que serán de utilidad al monitorista a la hora de realizar la operación para un turno, las cuales son las siguientes:
                                 <ul>
                                     <li><b>Búsqueda en CheckPoints:</b> Permite realizar búsquedas en cada uno de los CheckPoints por Camión, conductor o ruta.</li>
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Daily_Operation/DO_search.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_search.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                     <li><b>Refrescar la información:</b> En caso de que por alguna razón la información no se haya actualizado de forma automática, se puede utilizar esta opción para recargar cada uno de los CheckPoints.</li>
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Daily_Operation/DO_refresh.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/DO_refresh.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                     <li><b>Ordenamiento: Posibilidad de ordenar la información según el campo deseado:</b> Para ello es necesario hacer clic sobre el encabezado de cada uno de ellos:</li>
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Daily_Operation/Do_Sort.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/Do_Sort.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                     <li><b>Paginación:</b> Automáticamente la información se muestra por cada 15 registros y es posible navegar por paginas dentro de la misma tabla.</li>
                                     <ul class="thumbnails">
                                         <li class="span2">
                                             <a>
                                                 <img src="d_images/Daily_Operation/Do_pagination.png" />
                                             </a>
                                             <div class="actions"><a class="lightbox_trigger" href="d_images/Daily_Operation/Do_pagination.png"><i class="icon-search"></i></a></div>
                                         </li>
                                     </ul>
                                     <li><b>Visualización directa de elementos que se encuentran bajo alerta, revisión o inactivos:</b> Ofrece una vista rápida de elementos que se encuentran bajo alerta, inspección o que se encuentren bloqueados para no tener que salir de la operación en caso de que se detecte que un viaje tiene determinado color o simbología.</li>

                                 </ul>

                                </div>
                            </div>
                        </div>

                        <%-- </div>
                    <div class="widget-box collapsible">--%>
                        <div class="widget-title">
                            <a href="#collapseOne11" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Posibles errores que pueden ocurrir</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne11">

                            <div class="widget-content">

                                <div class="text">
                                    <h5>No es posible cargar una operación</h5>
                                    <b>Posibles causas: </b>
                                    <ul>
                                        <li>Existió una operación para ese mismo turno y que se encuentra en proceso de ser aprobada o cancelada. </li>
                                        <li>Aun no se cumple el tiempo necesario para poder capturar una nueva operación para el turno solicitado.
                             <br>
                                            <b>Posibles soluciones: </b>
                                            <li>Validar que no existan operaciones pendientes de aprobar o cancelar en el módulo de actividades pendientes. </li>
                                            <li>Validar que el tiempo transcurrido entre la última aprobación realizada y la que se intenta crear sea mayor a 21 horas. </li>
                                    </ul>
                                </div>
                                <div class="text">
                                    <h5>No existe o no se visualiza la relación camión/conductor</h5>
                                    <b>Posibles causas: </b>
                                    <ul>
                                        <li>1- La relación camión/conductor no ha sido dada de alta en la plantilla de operación diaria para el turno solicitado</li>
                                        <li>No existe la relación camión/conductor en el catalogo</li>
                                        <li>No existe el camión en el catalogo</li>
                                        <li>No existe el conductor en el catalogo</li>
                                        <li>La relación camión/conductor se encuentra inactiva</li>
                                        <li>La relación camión/conductor está siendo utilizada  para una ruta diferente</li>

                                        <br>
                                        <b>Posibles soluciones: </b>
                                        <li>Validar que la relación camión/conductor se encuentre dada de alta en la plantilla de operación diaria. En caso de que no exista, agregarla para las futuras cargas de la operación.</li>
                                        <li>Validar que la relación camión/conductor existe en el catálogo para el turno deseado. La relación puede existir más de una vez, pero puede estar asociada a turnos diferentes.</li>
                                        <li>Validar que el camión existe para el Proveedor a quien corresponde la operación en el catálogo de camiones. En caso de que no exista, agregar dicho camión.</li>
                                        <li>Validar que el conductor existe para el Proveedor a quien corresponde la operación en el catálogo de conductores. En caso de que no exista, agregar dicho camión.</li>
                                        <li>Activar la relación camión/conductor en el catálogo. </li>

                                    </ul>
                                </div>

                                <div class="text">
                                    <h5>No existe o no se visualiza la ruta deseada</h5>
                                    <b>Posibles causas: </b>
                                    <ul>
                                        <li>La ruta no existe en el catálogo de rutas</li>
                                        <br>
                                        <b>Posibles soluciones: </b>
                                        <li>Agregar la ruta requerida en el catálogo</li>
                                    </ul>
                                </div>

                                <div class="text">
                                    <h5>No se puede confirmar la operación</h5>
                                    <b>Posibles causas: </b>
                                    <ul>
                                        <li>Existen elementos que se encuentran sin llenar (alguno de sus checkpoints no cuenta con información de hora de llegada o cantidad de pasajeros).</li>
                                        <br>
                                        <b>Posibles soluciones: </b>
                                        <li>Validar que todos los viajes se encuentren llenos hasta el Checkpoint3</li>
                                    </ul>
                                </div>

                                <div class="text">
                                    <h5>La hora introducida es menor o igual a la hora de inicio del viaje. Por favor introduzca una hora valida.</h5>
                                    <b>Posibles causas: </b>
                                    <ul>
                                        <li>Está ingresando una hora menor a la indicada en un CheckPoint anterior</li>
                                        <br>
                                        <b>Posibles soluciones: </b>
                                        <li>Agregar la hora correcta en el CheckPoint indicado</li>
                                    </ul>
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



