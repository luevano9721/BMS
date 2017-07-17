<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRevision.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.Maintenance.EditRevision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #tbSecurity th {
            text-align: left;
            background-color: #eee;
        }

        #tbMaintenance th {
            text-align: left;
            background-color: #eee;
        }

        .ui-dialog-titlebar-close {
            display: none;
        }

        .imageUpload {
            padding-left:10px;
            padding-bottom:10px;
        }
    </style>

    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        
       


            function uploadImageFunction(Param1, Param2) {
                var Param1 = getSelectedImages();
                var lbRevisionID = $("[id$=lbRevisionCaseID]").html();
                var lbRevisionNoteID = $("[id$=lbRevisionNoteID]").html();
                // alert(name);
                PageMethods.deleteImg(Param1, lbRevisionID, lbRevisionNoteID,  onSucceed, onError);
            }
            //CallBack method when the page call success
            function onSucceed(results, currentContext, methodName) {
                $('#uploadImageDiv').dialog('destroy');
                uploadImageDialog();
                ShowMessage('Image deleted successfully', 'Success');


            }
            //CallBack method when the page call fails due to internal, server error 
            function onError(results, currentContext, methodName) {
                ShowMessage('Image was not deleted , please try again', 'Error');
            }
            $(function () {
                $("[id*=tbproposalDate]").datepicker();
            });

            //This function is for uncheck a checkbox if the user close alert confirm window

            function unCheckControl(controlName) {
                $("[id*=" + controlName + "]").prop("checked", false);
                __doPostBack();
            };

            $(document).ready(function () {
                $("[id*=chCheckAllTab1]").click(function () {
                    var checked = false;
                    if (this.checked) {
                        checked = true;
                    }
                    //chaloneacionItem2
                    var checkboxesTab1 = ['chalineacionItem1', 'chalineacionItem2', 'chFrenosItem1',
                    'chLlantasItem1',
                    'chLucesItem1',
                    'chLucesItem2',
                    'chDireccionItem1',
                    'chLucesIntItem1',
                    'chLucesFrenItem1',
                    'chbotiquinItem1',
                    'chExtintorItem1',
                    'chPuertaEmerItem1'];
                    $.each(checkboxesTab1, function (index, value) {
                        $("[id*=" + value + "]").prop("checked", checked);
                        // return (value !== 'three');
                    });

                });
            
                $("[id*=chCheckAllTab2]").click(function () {
                    var checked = false;
                    if (this.checked) {
                        checked = true;
                    }
                    var checkboxesTab2 = [
                    'chPuertaPrinItem1',
                    'chEscalonItem1',
                    'chPasamanosItem1',
                    'chRotulacionItem1',
                    'chVentanasItem1',
                    'chCalefaccionItem1',
                    'chAsientosItem1',
                    'chAsientosItem2',
                    'chCristalItem1',
                    'chPisoItem1'];
                    $.each(checkboxesTab2, function (index, value) {
                        $("[id*=" + value + "]").prop("checked", checked);
                        // return (value !== 'three');
                    });

                });
            });
        
    </script>
    <script>
        function uploadImageDialog(title) {
            var dialogTitle = title;
            $("#uploadImageDiv").html();

            $("#uploadImageDiv").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 950,
                height: 500,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#uploadImageDiv').dialog('open');
            return false;
        }


        function confirmAlertDialog(title) {
            var dialogTitle = title;
            $("#confirmAlert").html();

            $("#confirmAlert").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,

                width: 300,
                height: 430,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#confirmAlert').dialog('open');
            return false;
        }

        function noFoundDialog(title) {
            var dialogTitle = title;
            $("#noFoundData").html();

            $("#noFoundData").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 300,
                height: 250,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#noFoundData').dialog('open');
            return false;
        }



        function getSelectedImages() {
            var imageNames = [];
            $("#uploadImageDiv").find(':checkbox:checked').each(function (i) {
                var str = $(this).attr("ID");
                var res = str.split("#");
                imageNames.push(res[1]);
            });
            return imageNames;
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-header">
        <div id="breadcrumb">
            <a href="./Maintenance_I.aspx" title="Go to Revision Page" class="tip-bottom" style="float: right;">
                <asp:Label ID="lbl_BackToRevision" runat="server" Text=""></asp:Label></a>
            <a href="#" class="tip-bottom"><i class="icon-edit"></i>
                <asp:Label ID="lbl_EditRevision" runat="server" Text=""></asp:Label></a>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="container-fluid">

        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-check"></i></span>
                        <h5>
                            <asp:Label ID="lbl_EditRevison_LegalRevision" runat="server" Text=""></asp:Label></h5>
                    </div>

                    <div class="widget-content">
                        <asp:HiddenField ID="hfLegalRevisionID" runat="server" />

                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div class="controls control-group">
                                    <div class="span12">

                                        <div class="span1 m-wrap">
                                            <asp:Label ID="lbl_EditRevison_Vendor" runat="server" Text=""></asp:Label>
                                        </div>

                                        <div class="span3 m-wrap">
                                            <asp:Label ID="lbl_Proveedor" runat="server" Text='<%# Eval("Proveedor") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_LicensePlates" runat="server" Text=""></asp:Label>

                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_Placas" runat="server" Text='<%# Eval("Placas") %>' />

                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_GPS" runat="server" Text=""></asp:Label>

                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_Sistema_GPS" runat="server" Text='<%# Eval("Sistema_GPS") %>' />

                                        </div>
                                    </div>
                                </div>

                                <div class="controls control-group">
                                    <div class="span12">
                                        <div class="span1 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_Route" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span3 m-wrap">
                                            <asp:Label ID="lbl_Route_Name" runat="server" Text='<%# Eval("Route_Name") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_SerialNo" runat="server" Text=""></asp:Label>

                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_No_Serie" runat="server" Text='<%# Eval("No_Serie") %>' />

                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_Model" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_Modelo" runat="server" Text='<%# Eval("Modelo") %>' />

                                        </div>
                                    </div>
                                </div>

                                <div class="controls control-group">
                                    <div class="span12">
                                        <div class="span1 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_EconomicNo" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span3 m-wrap">
                                            <asp:Label ID="lbl_Bus_ID" runat="server" Text='<%# Eval("Bus_ID") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_License" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_Licencia" runat="server" Text='<%# Eval("Licencia") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_IdentifiedPCE" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_PCI" runat="server" Text='<%# Eval("PCI") %>' />
                                        </div>
                                    </div>
                                </div>
                                <div class="controls control-group">
                                    <div class="span12">
                                        <div class="span1 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_Shift" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span3 m-wrap">
                                            <asp:Label ID="lbl_Shift_Name" runat="server" Text='<%# Eval("Shift_Name") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_RouteMap" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_Mapa" runat="server" Text='<%# Eval("Mapa") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_Lintern" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_Linterna" runat="server" Text='<%# Eval("Linterna") %>' />

                                        </div>
                                    </div>
                                </div>
                                <div class="controls control-group">
                                    <div class="span12">
                                        <div class="span1 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_week" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span3 m-wrap">
                                            <asp:Label ID="lbl_Week" runat="server" Text='<%# Eval("Week") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_Radio" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_Radio" runat="server" Text='<%# Eval("Radio") %>' />
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="lbl_EditRevision_EmergencyReflectors" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="span2 m-wrap">
                                            <asp:Label ID="_lbl_Reflectores" runat="server" Text='<%# Eval("Reflectores") %>' />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>


                    </div>
                </div>


                <div class="widget-box">
                    <asp:Panel ID="PanelControls" runat="server">
                        <div class="widget-title">
                            <ul class="nav nav-tabs">

                                <li class="active"><a data-toggle="tab" href="#tab1">
                                    <asp:Label ID="lbl_EditRevision_SecurityRevision" runat="server" Text=""></asp:Label></a></li>
                                <li><a data-toggle="tab" href="#tab2">
                                    <asp:Label ID="lbl_EditRevision_MaintenanceRevision" runat="server" Text=""></asp:Label></a></li>

                            </ul>
                        </div>
                        
                        <div class="widget-content tab-content">
                            <div id="tab1" class="tab-pane active">

                                <table class="table table-bordered table-striped " id="tbSecurity">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:CheckBox ID="chCheckAllTab1" runat="server" />
                                                Metodo de Inspeccion</th>
                                            <th>Revision</th>
                                            <th>Reparar si..</th>
                                            <th>Revision</th>
                                            <th>Subir Imagen</th>
                                            <th>Fuera de servicio si..</th>
                                            <th>Revision</th>
                                            <th>Subir Imagen</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <th colspan="8" style="text-align: left;">alíneación:</th>
                                        <tr>
                                            <td>Revisar las llantas para detectar cualquier anomalía obvia o desgaste de uso</td>
                                            <td>
                                                <asp:CheckBox ID="chalineacionItem1" runat="server" /></td>
                                            <td>Cada llanta frontal desgastada indica un problema de alíneación</td>
                                            <td>
                                                <asp:CheckBox ID="chalineacionMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btalineacionMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>
                                            <td rowspan="2">Si el camión esta completamente inclinado, y es considerablemente peligroso manejarlo.</td>
                                            <td rowspan="2">
                                                <asp:CheckBox ID="chalineacionHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td rowspan="2">
                                                <asp:ImageButton ID="btalineacionHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>
                                        </tr>
                                        <tr>
                                            <td>Revisar cualquier problema de alíneación visible</td>
                                            <td>
                                                <asp:CheckBox ID="chalineacionItem2" runat="server" /></td>
                                            <td>Cualquier problema de alinacion que no sea causado por componentes defectuosos</td>
                                            <td>
                                                <asp:CheckBox ID="chalineacionMedium2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btalineacionMedium2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>
                                        </tr>

                                        <th colspan="8">Frenos:</th>
                                        <tr>
                                            <td>Revisar que el pedal de frenos es consistente al momento de presionar</td>
                                            <td>
                                                <asp:CheckBox ID="chFrenosItem1" runat="server" /></td>
                                            <td>Los frenos al presionar se va hasta el fondo, se aligera al soltarse o no hay presiónal bombear</td>

                                            <td>
                                                <asp:CheckBox ID="chFrenosMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btFrenosMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Es evidente que la conexión tiene una fuga de presiónde fluido o aire, cualquier manguera flexible de freno esta torcida, agrietada colapsada, tiene abultamiento, esta en capas o espinal, o esta dañada por debajo de la cubierta exterior</td>

                                            <td>
                                                <asp:CheckBox ID="chFrenosHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btFrenosHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Llantas:</th>
                                        <tr>

                                            <td>Inspeccionar para encontrar daños en los rines o llantas, tomar medidas sobre las líneas del dibujo 2/32 de pulgadas (1.6mm)</td>
                                            <td>
                                                <asp:CheckBox ID="chLlantasItem1" runat="server" /></td>

                                            <td>Llantas lisas, hay material expuesto,  desgarre u objetos extraños en las llantas que podrían causar daño o perdida de presiónde aire</td>
                                            <td>
                                                <asp:CheckBox ID="chLLantasMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLLantasMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Hay algún  corte, abrasion u otros daños a la pared lateral del neumático que resulte expuesta, hay algún a grieta que se extiende al rededor de la cuenta o la pared lateral del neumático, abolladuras o dobleces en una llanta, lo que podría provocar un fallo de la llanta o la separación del neumático del rin</td>
                                            <td>
                                                <asp:CheckBox ID="chLlantasHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLlantasHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Luces frontales:</th>
                                        <tr>

                                            <td>Revisar que todas las luces enciendan, esten en posicion correcta y bien sujetas</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesItem1" runat="server" /></td>

                                            <td>Si la base es diferente una de otra o no estan bien sujetas</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLucesMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si algún foco no prende</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLucesHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>
                                        <tr>

                                            <td>Controlar luces de circulacion diurna ( si esta equipado) para un funcionamiento correcto</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesItem2" runat="server" /></td>
                                            <td>Cualquier foco se empaña, esta agrietado o la luz es tenue</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesMedium2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLucesMedium2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Las luces se apagan después de esta en un breve periodo de tiempo o el funcionamiento es intermitente</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesHigh2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLucesHigh2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>
                                        <th colspan="8">Direccionales:</th>
                                        <tr>

                                            <td>Revisar las condiciones y buen funcionamiento de las direccionales</td>
                                            <td>
                                                <asp:CheckBox ID="chDireccionItem1" runat="server" /></td>
                                            <td>Si cualquier direccional esta agrietada y la luz no es visible o si las direccionales no señalan correctamente (Izquierda o derecha)</td>
                                            <td>
                                                <asp:CheckBox ID="chDireccionMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btDireccionMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si algún a direccional no parpadea o es tenue o no parpadea entre 60 y 120 veces por minuto</td>
                                            <td>
                                                <asp:CheckBox ID="chDireccionHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btDireccionHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Luces intermitentes:</th>
                                        <tr>

                                            <td>Revisar las condiciones y buen funcionamiento de las luces intermitentes</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesIntItem1" runat="server" /></td>
                                            <td>Si algún  foco esta roto o en mal estado</td>
                                            <td>
                                                <asp:CheckBox ID="chlucesIntMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btlucesIntMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Luces de emergencia de cuatro vias no funciona</td>
                                            <td>
                                                <asp:CheckBox ID="chlucesIntHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btlucesIntHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Luces de los frenos:</th>
                                        <tr>

                                            <td>Revisar las condiciones y buen funcionamiento de las luces de los frenos</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesFrenItem1" runat="server" /></td>
                                            <td>Si cualquier foco de los frenos esta agrietado o en mal estado</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesFrenMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLucesFrenMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>La mitad o mas de la O.E.M. luces de freno regulares instalados no funcionan cuando se pisa el pedal de freno .</td>
                                            <td>
                                                <asp:CheckBox ID="chLucesFrenHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btLucesFrenHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">botiquín:</th>
                                        <tr>

                                            <td>Revisar las condiciones de los materiales del botiquín de primeros auxilios</td>
                                            <td>
                                                <asp:CheckBox ID="chbotiquinItem1" runat="server" /></td>
                                            <td>Si no esta completo, cerrado, que la caja del botiquín no este resistente al agua o al polvo, que el contenido sea inaccesible por las condiciones de la caja</td>
                                            <td>
                                                <asp:CheckBox ID="chbotiquinMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btbotiquinMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <%--<td>Si el camión esta completamente inclinado , y es considerablemente peligroso.</td>
                                            <td>
                                                <asp:CheckBox ID="chbotiquínHigh1" runat="server" /></td>
                                            <td><asp:ImageButton ID="btbotiquínHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>
                                            --%>
                                        </tr>

                                        <th colspan="8">Extintor:</th>
                                        <tr>

                                            <td rowspan="2">Revisar que este en un lugar accesible</td>
                                            <td rowspan="2">
                                                <asp:CheckBox ID="chExtintorItem1" runat="server" /></td>
                                            <td>Cada llanta frontal desgastada indica un problema de alíneación.</td>
                                            <td>
                                                <asp:CheckBox ID="chExtintorMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btExtintorMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si la base o soporte del extintor esta suelto o flojo</td>
                                            <td>
                                                <asp:CheckBox ID="chExtintorHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btExtintorHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>
                                        <tr>

                                            <td>Si no hay presion</td>
                                            <td>
                                                <asp:CheckBox ID="chExtintorMedium2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btExtintorMedium2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si la presiónesta por encima o por debajo de la zona verde</td>
                                            <td>
                                                <asp:CheckBox ID="chExtintorHigh2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btExtintorHigh2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>
                                        <th colspan="8">Puerta de emergencia:</th>
                                        <tr>

                                            <td>Revisar que las puertas de emergencia funcionen correctamente</td>
                                            <td>
                                                <asp:CheckBox ID="chPuertaEmerItem1" runat="server" /></td>
                                            <td>Si las puertas de emergencia estan obstruidas con cuerdas o cualquier otro material que impide el funcionamiento correcto de la puerta</td>
                                            <td>
                                                <asp:CheckBox ID="chPuertaEmerMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btPuertaEmerMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td rowspan="2">Si la puerta de emergencia es dificil o no se puede abrir al menos 90 grados afuera del autobus</td>
                                            <td>
                                                <asp:CheckBox ID="chPuertaEmerHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btPuertaEmerHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                    </tbody>
                                </table>
                            </div>

                            <div id="tab2" class="tab-pane">
                                <table class="table table-bordered table-striped " id="tbMaintenance">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:CheckBox ID="chCheckAllTab2" runat="server" />
                                                Metodo de Inspeccion</th>
                                            <th>Revision</th>
                                            <th>Reparar si..</th>
                                            <th>Revision</th>
                                            <th>Subir Imagen</th>
                                            <th>Fuera de servicio si..</th>
                                            <th>Revision</th>
                                            <th>Subir Imagen</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <th colspan="8" style="text-align: left;">Puerta principal:</th>
                                        <tr>

                                            <td>Inspeccione las condiciones de la puerta, que su montaje sea correcto con sus respectivos sellos</td>
                                            <td>
                                                <asp:CheckBox ID="chPuertaPrinItem1" runat="server" /></td>
                                            <td>Si la bisagra, puerta, cerradura y /o sello estan sueltas o dañadas, pero aun funcionan</td>
                                            <td>
                                                <asp:CheckBox ID="chPuertaPrinMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btPuertaPrinMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si la bisagra, puerta, cerradura y /o sello estan sueltas o dañadas y no funcionan o faltan</td>
                                            <td>
                                                <asp:CheckBox ID="chPuertaPrinHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btPuertaPrinHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Escalones:</th>
                                        <tr>

                                            <td>Revisar especificaciones de los escalones, condicion y material antiderrapante</td>
                                            <td>
                                                <asp:CheckBox ID="chEscalonItem1" runat="server" /></td>
                                            <td>Si el material antiderrapante no es seguro</td>
                                            <td>
                                                <asp:CheckBox ID="chEscalonMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btEscalonMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si el material antiderrapante no es seguro y se encuentra colocado en otra ara del escalon</td>
                                            <td>
                                                <asp:CheckBox ID="chEscalonHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btEscalonHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Pasamanos:</th>
                                        <tr>

                                            <td>Revisar que haya pasamanos y que este en la posicion correcta</td>
                                            <td>
                                                <asp:CheckBox ID="chPasamanosItem1" runat="server" /></td>
                                            <td>El pasamanos esta flojo o suelto</td>
                                            <td>
                                                <asp:CheckBox ID="chPasamanosMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btPasamanosMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si el pasamanos esta dañado o tiene modificaciones no autorizadas </td>
                                            <td>
                                                <asp:CheckBox ID="chPasamanosHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btPasamanosHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Rotulacion:</th>
                                        <tr>

                                            <td rowspan="2">Comprobar que la tipografia de la leyenda "PCE" Sea la correcta en tamaño, color y ubicacion</td>
                                            <td rowspan="2">
                                                <asp:CheckBox ID="chRotulacionItem1" runat="server" /></td>
                                            <td>Si no esta rotulado con "PCE"</td>
                                            <td>
                                                <asp:CheckBox ID="chRotulacionMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btRotulacionMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>


                                            <td rowspan="2"></td>
                                            <td rowspan="2"></td>
                                        </tr>

                                        <tr>

                                            <td>Si el numero economico no esta escrito o no se ve bien</td>
                                            <td>
                                                <asp:CheckBox ID="chRotulacionMedium2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btRotulacionMedium2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Ventanas:</th>
                                        <tr>

                                            <td>Revisar que cuenten con su seguro y que suban y bajen</td>
                                            <td>
                                                <asp:CheckBox ID="chVentanasItem1" runat="server" /></td>
                                            <td>El cristal esta dañado</td>
                                            <td>
                                                <asp:CheckBox ID="chVentanasMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btVentanasMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td></td>
                                            <td></td>
                                        </tr>

                                        <th colspan="8">Calefacciones:</th>
                                        <tr>

                                            <td>Revisar las condiciones de la calefaccion para que la temperatura se matnenga agradable en el interior del camion</td>
                                            <td>
                                                <asp:CheckBox ID="chCalefaccionItem1" runat="server" /></td>
                                            <td>Si los ductos o las calefacciones les faltan componentes, estan dañados, flojos u obstruidos</td>
                                            <td>
                                                <asp:CheckBox ID="chCalefaccionMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btCalefaccionMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Cualquier parte del sistema de calefaccion en la zona de pasajeros crea bordes afilados, proyecciones u otros peligros para pasajero o conductor</td>
                                            <td>
                                                <asp:CheckBox ID="chCalefaccionHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btCalefaccionHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Asientos:</th>
                                        <tr>

                                            <td>Inspeccionar estructuras de los asientos de pasajeros y la soldaduras, tubos y tornillos</td>
                                            <td>
                                                <asp:CheckBox ID="chAsientosItem1" runat="server" /></td>
                                            <td>Si las estructuras de los asientos estan sueltos o necesitan soldarse</td>
                                            <td>
                                                <asp:CheckBox ID="chAsientosMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btAsientosMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Si las abrazaderas de los espejos estan dobladas o rotas, o la instalacion es insegura y el espejo no se queda en la posicion ajustada o no se puede ajustar</td>
                                            <td>
                                                <asp:CheckBox ID="chAsientosHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btAsientosHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <tr>

                                            <td>Inspeccionar bases de los asientos que esten fijados y en condicion de uso </td>
                                            <td>
                                                <asp:CheckBox ID="chAsientosItem2" runat="server" /></td>
                                            <td>Cualquier parte inferior del asiento no esta firmemente anclada al bastidor del asiento</td>
                                            <td>
                                                <asp:CheckBox ID="chAsientosMedium2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btAsientosMedium2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td>Cualquier relleno de fondo de asiento o cojin tiene deterioro o daño significativo</td>
                                            <td>
                                                <asp:CheckBox ID="chAsientosHigh2" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btAsientosHigh2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                        </tr>

                                        <th colspan="8">Cristales:</th>
                                        <tr>

                                            <td>Inspeccionar parabrisas en busca de grietas y otros daños</td>
                                            <td>
                                                <asp:CheckBox ID="chCristalItem1" runat="server" /></td>
                                            <td>Si faltan vidrios o estan rotos</td>
                                            <td>
                                                <asp:CheckBox ID="chCristalMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btCristalMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td></td>
                                            <td></td>
                                        </tr>

                                        <th colspan="8">Piso:</th>
                                        <tr>

                                            <td rowspan="2">Inspeccionar revestimiento de suelo, pasillo y las tiras de moldura concava, la adhesion y /o agujeros de fijacion o grietas, y de goma acanalada en pasillo</td>
                                            <td rowspan="2">
                                                <asp:CheckBox ID="chPisoItem1" runat="server" /></td>
                                            <td>La cubierta del suelo esta suelta, deteriorada o agrietada</td>
                                            <td>
                                                <asp:CheckBox ID="chPisoMedium1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:ImageButton ID="btPisoMedium1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>

                                            <td rowspan="2">hay agujeros no sellados o grietas a traves  de la parte inferior del camion</td>
                                            <td rowspan="2">
                                                <asp:CheckBox ID="chPisoHigh1" runat="server" OnCheckedChanged="checkboxLevel2And3_CheckedChanged" AutoPostBack="true" /></td>
                                            <td rowspan="2">
                                                <asp:ImageButton ID="btPisoHigh1" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>
                                        </tr>

                                        <%-- <tr>

                                            <td>1) Revisar las llantas para detectar cualquier anomalia obvia o desgaste de uso</td>
                                            <td>
                                                <asp:CheckBox ID="chPisoMedium2" runat="server" /></td>
                                            <td><asp:ImageButton ID="btPisoMedium2" runat="server" ImageUrl="~/images/upload-image.png" OnClick="btAllUploadImage_Click" CausesValidation="false" /></td>
                                           
                                        </tr>--%>
                                    </tbody>
                                </table>
                                </div>
                            </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div style="float: right">
            <asp:Button ID="btSaveData" runat="server" Text="" OnClick="btSaveData_Click" CssClass="btn btn-info" CausesValidation="false"></asp:Button>
        </div>

    </div>
    <%--</div>
            </div>--%>


    <div id="uploadImageDiv" style="display: none">
        <div class="widget-content">
            <div class="form-1">
                <p>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>

                            <div>
                                <h4 class="alert-heading">
                                    <asp:Label ID="lbl_EditRevision_MetodoInspec" Text="" runat="server" /></h4>
                                <asp:Label ID="lblEditRevision_Case" Text="" runat="server" />

                            </div>

                            <div class="control-group">
                            </div>
                            <br></br>
                            Revision Case ID: <asp:Label ID="lbRevisionCaseID" runat="server" Text="NA"  />
                            Legal Revision ID: <asp:Label ID="lbRevisionNoteID" runat="server" Text=""  />
                            <table class="table table-bordered table-striped " id="tbImages">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:Label ID="lbl_EditRevisions_uploadImageDiv_State" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lbl_EditRevisions_uploadImageDiv_Image" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lbl_EditRevisions_uploadImageDiv_Select_Image" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lbl_EditRevisions_uploadImageDiv_Upload" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lbl_EditRevisions_uploadImageDiv_Delete" runat="server" Text=""></asp:Label>
                                        </th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <%--  <th colspan="8" style="text-align: left;">alíneación:</th>--%>
                                    <tr>

                                        <td>
                                            <asp:Label ID="lbl_EditRevisions_uploadImageDiv_Before" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:PlaceHolder ID="phImageContentAntes" runat="server" />
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <asp:RequiredFieldValidator ID="RV_EditRevisions_Required1" ErrorMessage="" ControlToValidate="FileUpload1"
                                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="Before" />
                                            <asp:RegularExpressionValidator ID="REV_EditRevisions1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.png|.gif|.jpeg)$"
                                                ControlToValidate="FileUpload1" runat="server" ForeColor="Red" ErrorMessage=""
                                                Display="Dynamic" ValidationGroup="Before" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btUploadImage" runat="server" Text="" CssClass="btn btn-info" OnClick="btUploadImage_Click" ValidationGroup="Before"></asp:Button></td>
                                        <td>
                                            <asp:Button ID="btDeleteImage" runat="server" Text="" CssClass="btn btn-danger" OnClientClick="javascript:return uploadImageFunction('before');" OnClick="btDeleteImage_Click1" ValidationGroup="Before" CausesValidation="false"></asp:Button></td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:Label ID="lbl_EditRevisions_uploadImageDiv_After" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:PlaceHolder ID="phImageContentdespues" runat="server" />
                                            <td>
                                                <asp:FileUpload ID="FileUpload2" runat="server" />

                                                <asp:RequiredFieldValidator ID="RV_EditRevisions_Required" ErrorMessage="" ControlToValidate="FileUpload2"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="After" />
                                                <asp:RegularExpressionValidator ID="REV_EditRevision2" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.png|.gif|.jpeg)$"
                                                    ControlToValidate="FileUpload2" runat="server" ForeColor="Red" ErrorMessage=""
                                                    Display="Dynamic" ValidationGroup="After" />
                                            </td>

                                        <td>
                                            <asp:Button ID="btUploadImageAfter" runat="server" Text="" CssClass="btn btn-info" OnClick="btUploadImageAfter_Click" ValidationGroup="After"></asp:Button></td>
                                        <td>
                                            <asp:Button ID="btDeleteImageAfter" runat="server" Text="" CssClass="btn btn-danger" OnClientClick="javascript:return uploadImageFunction('after');" OnClick="btDeleteImageAfter_Click" ValidationGroup="After" CausesValidation="false"></asp:Button></td>

                                    </tr>
                                </tbody>
                            </table>

                            <br></br>
                            <%--      <div class="center">

                                       <asp:Button ID="btTransferCH1" runat="server" CssClass="btn btn-success" Text="Transfer"  />
                                       <asp:Button ID="btCancelCH1" runat="server" CssClass="btn btn-danger" Text="Cancel"  />

                              
                                      </div>--%>
                            <asp:Button ID="btCloseUploadImage" runat="server" Text="Cerrar" CausesValidation="false" CssClass="btn btn-inverse" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btUploadImage" />
                            <asp:PostBackTrigger ControlID="btDeleteImage" />
                            <asp:PostBackTrigger ControlID="btCloseUploadImage" />
                            <asp:PostBackTrigger ControlID="btUploadImageAfter" />
                            <asp:PostBackTrigger ControlID="btDeleteImageAfter" />
                        </Triggers>
                    </asp:UpdatePanel>
                </p>
            </div>
        </div>
    </div>



    <div id="confirmAlert" style="display: none">
        <div class="widget-content">
            <div class="form-1">
                <p>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:Label ID="ControlNameAlert" Text="NA" runat="server" Visible="false" />
                            <div>
                                <h4 class="alert-heading">
                                    <asp:Label ID="lbl_EditRevisions_confirmAlert_Heading" Text="" runat="server" /></h4>
                                <asp:Label ID="lbl_EditRevisions_confirmAlert_ThisAction" Text="" runat="server" />
                                <asp:HiddenField ID="hfRevisionCaseId" runat="server" />
                            </div>
                            <br></br>
                            <div class="control-group">
                                <asp:Label ID="lbl_EditRevisions_confirmAlert_Proposal" Text="" runat="server" />
                                <asp:TextBox ID="tbproposalDate" runat="server" data-date-format="mm/dd/yyyy" placeholder="Select a date" />
                            </div>
                            <br></br>
                            <div class="control-group">
                                <asp:Label ID="lbl_EditRevisions_confirmAlert_Action" Text="" runat="server" />
                                <asp:TextBox ID="tbAction" runat="server" TextMode="MultiLine" Columns="3" />
                            </div>
                            <br></br>
                            <div class="control-group">
                                <asp:Button ID="btconfirmAlert" runat="server" Text="" OnClick="btconfirmAlert_Click" CausesValidation="false" />
                                <asp:Button ID="btCancelAlert" runat="server" Text="" OnClick="btCancelAlert_Click" CausesValidation="false" />
                            </div>




                            <%--      <div class="center">

                                       <asp:Button ID="btTransferCH1" runat="server" CssClass="btn btn-success" Text="Transfer"  />
                                       <asp:Button ID="btCancelCH1" runat="server" CssClass="btn btn-danger" Text="Cancel"  />

                              
                                      </div>--%>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btconfirmAlert" />
                            <asp:PostBackTrigger ControlID="btCancelAlert" />
                        </Triggers>
                    </asp:UpdatePanel>
                </p>
            </div>
        </div>
    </div>


    <div id="noFoundData" style="display: none">
        <div class="widget-content">
            <div class="form-1">
                <p>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div>
                                <h4 class="alert-heading">
                                    <asp:Label ID="lbl_EditRevison_NoFound_Text1" Text="" runat="server" /></h4>
                                <asp:Label ID="lbl_EditRevison_NoFound_Text2" Text="" runat="server" />
                                <asp:Button ID="btCloseNoFoundButton" runat="server" Text="" CausesValidation="false" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <br></br>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btCloseNoFoundButton" />

                        </Triggers>
                    </asp:UpdatePanel>
                </p>
            </div>
        </div>
    </div>

    <%-- <script type="text/javascript">

        $('a[href="#EditRevisions"]').tab('show');
        </script>--%>
</asp:Content>
