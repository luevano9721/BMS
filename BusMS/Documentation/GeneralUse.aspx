<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralUse.aspx.cs" Inherits="BusManagementSystem.Documentation.GeneralUse" %>

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
                <h4>Inicio del sistema</h4>
               <div class="text">
                   Selecciona una de las opciones abajo mostradas para mostrar su contenido.
               </div>
            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce el menú</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">
                            <div class="widget-content">
                                <div class="text">
                                    La pantalla principal está dividida en dos partes principales; la parte derecha es donde podrás realizar 
                             operaciones dentro del sistema y la parte izquierda contiene todos los módulos a los que tienes acceso:
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/menu.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/menu.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>

                                <div class="text">
                                    Cuando no tengas acceso a un módulo, este simplemente no aparecerá en el menú del sistema, al igual que
                             si intentas realizar algún cambio y no tienes permisos de administrador, veras el siguiente mensaje:
                                </div>

                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/warningAdmin.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/warningAdmin.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Esto quiere decir que tienes que esperar a que un administrador apruebe los cambios que quieres hacer. 
                                    Puedes ver estos cambios en la sección de <a href="Activities_Awaiting.aspx">Actividades pendientes</a>.
                                </div>
                                <div class="text">
                                    Este menú se divide en dos grandes secciones, operación del sistema y administración del sistema.
                                     Dentro de estas dos secciones podrás realizar todas las acciones del sistema, ya sea operativas o de control de usuario y 
                                    sus privilegios. Conocerás cada sección más a detalle en las próximas páginas.
                                </div>
                                <div class="text">
                                    En la parte superior de la página podrás ver un acceso a la pantalla de inicio, como cerrar sesión del sistema y 
                                     una sección de configuración del usuario, la cual permite cambiar ciertas preferencias del sistema, o de tu propia cuenta.
                                     Encontraras más detalle sobre esto en la sección de <a href="Global_Configuration.aspx">configuración</a>.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseSeven" data-toggle="collapse"><span class="icon"><i class="icon-zoom-in"></i></span>
                                <h5>Buscar información</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseSeven">
                            <div class="widget-content">
                                <div class="text">
                                   La barra de busqueda podrás utilizarla para encontrar información más fácil.
                                    
                                </div>
                                <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/buscar.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/buscar.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                <div class="text">
                                    Solo es necesario escribir el texto que deseas encontrar, 
                                    presionar la tecla enter o el boton a un lado de la barra, para ver filtrada la información.
                                </div>

                                <div class="text">
                                    Para ver toda la información como al inicio es necesario borrar el texto escrito y volver a presionar enter o el boton de busqueda.
                                </div>


                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar un registro</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <div class="text">
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/insert.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/insert.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    Puedes insertar un nuevo registro en cualquier catalogo al que tengas acceso y permiso; siguiendo los siguientes pasos: 
                                </div>
                                <div class="text">
                                    <li>Haz clic en el icono
                                        <img class="soloImg" src="d_images/toggle.png" />
                                        para mostrar la información que se necesita capturar.</li>
                                    <li>Después  da clic en el botón de agregar  
                                        <img class="soloImg" src="d_images/add.png" />
                                        para habilitar las áreas donde teclear la información.</li>
                                    <li>Justo donde veas alguno de los siguientes elementos
                                        <img class="soloImg" src="d_images/filter1.png" />
                                        <img class="soloImg" src="d_images/filter2.png" />
                                        habrá información precargada para que elijas el dato que necesitas,
                                    solo debes de comenzar a teclear 
                                    el nombre de la información que deseas y se ira filtrando tu búsqueda o bien acercando en orden alfabético
                                    hacia lo que haz tecleado.</li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/exampleFilter1.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/exampleFilter1.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/exampleFilter2.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/exampleFilter2.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <li>Completa todos los campos requeridos y después da clic en el botón de salvar
                                        <img class="soloImg" src="d_images/save.png" /></li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/validation.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/validation.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <div class="text">Si alguna información tecleada es inválida, una leyenda con las correcciones necesarias aparecerá, de lo contrario veras un mensaje de operación exitosa en la pantalla.</div>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/successAdd.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/successAdd.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un registro</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/exampleSelect.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/exampleSelect.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    En la lista que aparece abajo del catálogo, en la parte izquierda de la información se encontrara el botón
                                    <img class="soloImg" src="d_images/select.png" />
                                    para cada fila de registros,  para seleccionar es necesario hacer clic en el botón correspondiente. Después de hacer clic, 
                                    la información se pasara al formulario superior para que pueda ser editado o eliminado.
                                </div>
                                <div class="text">
                                    Puedes editar registros cuando los campos están habilitados para su edición,
                                   y tienes acceso a la página y sus privilegios. Sigue los siguientes pasos para editar un registro:
                                </div>
                                <div class="text">
                                    <li>Primero selecciona el elemento que deseas editar. 
                                         En seguida te aparecerá una pantalla como la siguiente para que puedas editar tus campos.
                                    </li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/exampleEdit.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/exampleEdit.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <li>Solo los campos habilitados para escritura son los que están permitidos editar. 
                                        Al terminar los cambios haz clic en el botón de guardar
                                        <img class="soloImg" src="d_images/save.png" />
                                        y un mensaje de operación exitosa aparecerá.</li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/successUpdate.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/successUpdate.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <li>Si al dar clic en salvar
                                        <img class="soloImg" src="d_images/save.png" />
                                        no existe ningún cambio en la información, un mensaje se mostrará informando que no se hicieron cambios.</li>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/noChanges.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/noChanges.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar un registro</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    Puedes eliminar un registro siguiendo los siguientes pasos: 
                                </div>
                                <li>Haz clic en el botón de seleccionar  
                                        <img class="soloImg" src="d_images/select.png" />
                                    al elemento que deseas eliminar. </li>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/exampleEdit.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/exampleEdit.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <li>Después clic en el botón de eliminar,
                                        <img class="soloImg" src="d_images/delete.png" />
                                    esto hará que un mensaje de confirmación aparezca, preguntando si estás seguro que deseas eliminar dicho elemento.</li>
                                <div class="text">
                                    Si el elemento es correctamente eliminado, un mensaje de confirmación se verá en pantalla.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/successDelete.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/successDelete.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Solo es posible eliminar registros que no tienen referencia en otros catálogos o procesos del Sistema, 
                                     si intentas eliminar un registro que tiene dichas relaciones, un mensaje de error con la relación que no se puede eliminar, será mostrado.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/errorReference.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/errorReference.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFive" data-toggle="collapse"><span class="icon"><i class="icon-upload-alt"></i></span>
                                <h5>Exportar</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFive">
                            <div class="widget-content">
                                <div class="text">
                                    Siempre que veas el botón
                                    <img class="soloImg" src="d_images/excel.png" />
                                    podrás descargar la información mostrada en pantalla a un archivo de Excel.
                                </div>
                                <div class="text">
                                    Puedes encontrar esos botones en cualquiera de las siguientes secciones:
                                </div>
                                <li>En las secciones de insertar, editar y eliminar</li>
                                <li>Dentro de la sección de reportes, al seleccionar el icono de guardar</li>
                                <li>En los filtros a aplicar en la página de <a href="Activities_Awaiting.aspx">Actividades pendientes</a> </li>
                                <li>Dentro de la sección de <a href="Daily_Operation.aspx">operación diaria</a>, justo después de seleccionar el turno que quieres cargar en la operación.</li>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
