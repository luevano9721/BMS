<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlackList.aspx.cs" Inherits="BusManagementSystem.Documentation.BlackList" %>

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
                <h4>Lista negra</h4>
                <div class="text">
                    En el catálogo de Lista negra se registraran a los conductores o camiones que incumplan con alguna regla establecida
                    por el departamento de traslados, los conductores o camiones registrados en este catálogo serán excluidos de las
                    actividades diarias correspondientemente.
                </div>
            </div>
            <div class="container-fluid">
                <div class="span12">
                    <div class="widget-box collapsible">
                        <div class="widget-title">
                            <a href="#collapseOne" data-toggle="collapse"><span class="icon"><i class="icon-list"></i></span>
                                <h5>Conoce el catálogo</h5>

                            </a>
                        </div>
                        <div class="collapse" id="collapseOne">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BlackList/BlackListCatalog.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BlackList/BlackListCatalog.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Estos son los datos que encontraras en esta pantalla:
                                </div>
                                <div class="text">
                                    <li>ID de lista negra
                                    </li>
                                    <li>fecha de lista negra
                                    </li>
                                    <li>Proveedor
                                    </li>
                                    <li>ID del cambio
                                    </li>
                                    <li>Modelo
                                    </li>
                                    <li>Razón
                                    </li>
                                    <li>Comentarios
                                    </li>
                                </div>
                                <div class="text">
                                    En este catálogo se podrán registrar conductores, camiones y la combinación de los dos conductor/camión.
                                </div>
                                <div class="text">
                                    De agregar algún conductor o camión a la lista negra, no podrán formar parte de las actividades diarias,
                                     ya que quedaran bloqueados para los diferentes catálogos
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
                                    Puedes filtrar por el proveedor correspondiente al conductor o camión.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BlackList/Filtro_Proveedor.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BlackList/Filtro_Proveedor.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Puedes filtrar por la categoría correspondiente a la información buscada: Conductor, Camión o Conductor/Camión.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BlackList/Filtro_Categoria.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BlackList/Filtro_Categoria.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Puedes filtrar buscando alguna palabra y luego haciendo clic en el icono
                                    <img class="soloImg" src="d_images/lens.png" />
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BlackList/Filtro_Buscar.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BlackList/Filtro_Buscar.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>


                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar a la lista negra</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BlackList/BlackListCatalog.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BlackList/Insertar_listaNegra.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Es necesario seleccionar el tipo (Camión, Conductor, Camión/Conductor) para poder agregar algún elemento a la lista negra. 
                                </div>

                                <div class="text">
                                    Después de dar clic en el boton    
                                    <img class="soloImg" src="d_images/add.png" />
                                    se habilitaran los campos del formulario correspondientes a la categoría elegida.
                                </div>
                                <div class="text">
                                    Es necesario llenar los siguientes campos:

                                    <li>Proveedor
                                    </li>
                                    <li>ID del conductor
                                    </li>
                                    <li>ID de camión
                                    </li>
                                    <li>Turno
                                    </li>
                                    <li>Camión/Conductor
                                    </li>
                                    <li>Comentarios
                                    </li>
                                    <li>Razones
                                    </li>

                                    <div class="text">
                                        Una vez capturados todos los campos, puedes dar clic en el botón
                                        <img class="soloImg" src="d_images/save.png" />
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar un elemento de la lista negra</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">

                                <div class="text">
                                    Es necesario hacer clic en el botón
                                    <img class="soloImg" src="d_images/select.png" />
                                    que se encuentra en la parte izquierda de cada registro
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/blackList/Grid.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/blackList/Grid.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Después de seleccionar algún elemento, se llenara el formulario con los datos correspondientes. Únicamente habilitara los campos de Razón
                                    y Comentarios, los cuales son editables.
                                </div>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/BlackList/Select.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/BlackList/Select.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Al finalizar tus cambios, da clic en guardar 
                                        <img class="soloImg" src="d_images/save.png" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar un elemento de Lista negra</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    No es posible eliminar algún elemento de la lista negra, para eliminar registros es necesario acudir con los administradores del sistema.
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


