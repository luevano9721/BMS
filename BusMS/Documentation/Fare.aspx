<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fare.aspx.cs" Inherits="BusManagementSystem.Documentation.Fare" %>

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
                <h4>Tarifa</h4>
                <div class="text">
                    Esta página del sistema administra lo relacionado con la tarifa de los servicios suministrados por el proveedor de transporte, 
                    las tarifas tienen 2 relaciones muy importantes, el proveedor al que pertenecen y el tipo de servicio que es. 
                    En base al tipo de servicio y al proveedor se determina un costo,
                     los servicios especiales llevan un costo diferente y se debe de tomar en cuenta al momento de agregar un nuevo servicio. 
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
                                            <img src="d_images/Fare/Fare.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Fare/Fare.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Podemos apreciar en la imagen que esta sección sirve para visualizar las tarifas registradas en el sistema,
                                     y también nos da la opción de seleccionar alguna tarifa, editarla o eliminarla según sea el caso.
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
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/filterActive_Bus.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/filterActive_Bus.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/filterInactive_Bus.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/filterInactive_Bus.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    Estas tarifas pueden ser filtradas de acuerdo al proveedor que se seleccione, 
                                    si no tienes permisos de administrador, solo podrás ver la información perteneciente al proveedor con el que estas registrado.
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseTwo" data-toggle="collapse"><span class="icon"><i class="icon-plus-sign"></i></span>
                                <h5>Insertar una nueva tarifa</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseTwo">
                            <div class="widget-content">
                                <ul class="thumbnails">
                                    <li class="span2">
                                        <a>
                                            <img src="d_images/Fare/new_fare.png" />
                                        </a>
                                        <div class="actions"><a class="lightbox_trigger" href="d_images/Fare/new_fare.png"><i class="icon-search"></i></a></div>
                                    </li>
                                </ul>
                                <div class="text">
                                    En la página, es necesario hacer clic en el icono   
                                    <img class="soloImg" src="d_images/toggle.png" />
                                    para poder desplazar el formulario mostrado en la imagen anterior.  
                                </div>

                                <div class="text">
                                    Después de hacer que el formulario se visualice, es necesario hacer clic en el botón   
                                    <img class="soloImg" src="d_images/add.png" />
                                    para poder habilitar el formulario y poder llenar los siguientes campos: 

                                    <li>Costo unitario.- Es el costo en pesos que se le asignara al servicio
                                    </li>
                                    <li>Proveedor.- Es el proveedor de servicios de transporte al que pertenece el servicio
                                    </li>
                                    <li>Servicio.- Es el nombre del servicio, los datos del servicio se dan de alta en la sección del sistema llamada servicio
                                    </li>
                                    <div class="text">
                                        Después de llenar debidamente los campos del formulario y se quiere continuar con el proceso de registrar una nueva tarifa, 
                                        es necesario hacer clic en el  botón 
                                        <img class="soloImg" src="d_images/save.png" />
                                        para guardar los cambios, en el caso contrario es necesario hacer clic en el botón   
                                        <img class="soloImg" src="d_images/reset.png" />
                                        y de esta manera los datos se perderán, también dejara al formulario en su estado original.
                                    </div>

                                    <div class="text">
                                        Si el registro de la tarifa es exitoso nos mostrara una notificación como se muestra a continuación.                                      
                                    </div>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/successAdd.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/successAdd.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <div class="text">
                                        En el caso contrario, el mensaje de error nos indicara que la tarifa que registramos 
                                        ya existe con ese mismo nombre de servicio y perteneciente al mismo proveedor. 
                                        El mensaje de error que nos muestra es el sistema es el siguiente:
                                    </div>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Fare/error_Fare.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Fare/error_Fare.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                    <div class="text">
                                        Si al momento de llenar los campos del formulario se deja vacío alguno de ellos, el sistema no nos permitirá continuar con el proceso
                                         y nos mostrara un mensaje de error como se muestra a continuación:
                                    </div>
                                    <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/Fare/error2_Fare.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/Fare/error2_Fare.png"><i class="icon-search"></i></a></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseThree" data-toggle="collapse"><span class="icon"><i class="icon-edit"></i></span>
                                <h5>Editar una tarifa</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseThree">
                            <div class="widget-content">

                                <div class="text">
                                    Después de seleccionar una tarifa, la información estará en el formulario de información de la tarifa, 
                                    los campos del formulario estarán habilitados para que puedan ser editados, 
                                    si se realizaron cambios y se desean guardar, es necesario hacer clic en el  botón  
                                    <img class="soloImg" src="d_images/save.png" />
                                    para guardar los cambios, en el caso contrario es necesario hacer clic en el botón 
                                    <img class="soloImg" src="d_images/reset.png" />
                                    y de esta manera los datos se perderán, también dejara al formulario en su estado original.  
                                </div>

                            </div>
                        </div>
                        <div class="widget-title">
                            <a href="#collapseFour" data-toggle="collapse"><span class="icon"><i class="icon-remove-sign"></i></span>
                                <h5>Eliminar una tarifa</h5>
                            </a>
                        </div>
                        <div class="collapse" id="collapseFour">
                            <div class="widget-content">
                                <div class="text">
                                    Después de seleccionar una Tafira, es necesario hacer clic en el botón  
                                    <img class="soloImg" src="d_images/delete.png" />
                                    para eliminar la tarifa del sistema. 
                                    Si se realizó la eliminación exitosamente el sistema arrojara una notificación como la siguiente:

                                </div>
                                 <ul class="thumbnails">
                                        <li class="span2">
                                            <a>
                                                <img src="d_images/successDelete.png" />
                                            </a>
                                            <div class="actions"><a class="lightbox_trigger" href="d_images/successDelete.png"><i class="icon-search"></i></a></div>
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

