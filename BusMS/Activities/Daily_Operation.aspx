<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Daily_Operation.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.Activities.Daily_Operation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />

    <link href="/css/chosen.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/docsupport/prism.css">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <style>
        .fillTotalScreen {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            background-color: rgba(0, 0, 0, 0.55);
        }


        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
            height: 30px;
        }

        .centerWaitingImage {
            z-index: 1000;
            margin-left: 35%;
            margin-top: 15%;
        }

            .centerWaitingImage img {
                height: 204px;
                width: 372px;
            }

        .form-1 label {
            padding: 20px;
        }

        .form-1 {
            padding-bottom: 15px;
        }

        .center {
            margin: auto;
            width: 50%;
            padding: 10px;
        }

        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker2);
            bindPicker2();

            $("[id$=ddl_BD_New_Trip]").chosen();
            $("[id$=ddl_Route_NewTrip]").chosen();
            $("[id$=cbTransferCH1]").chosen();
            $("[id$=cbTransferCH2]").chosen();
            $("[id$=cbReasons]").chosen();
            $("[id$=cbReasons2]").chosen();
            $("[id*=cbCheck2Edit]").chosen();
            $("[id*=cbCheck3Edit]").chosen();


        });
        function bindPicker2() {
            $("[id*=cbBusDriver1]").chosen();
            $("[id*=cbRoute1]").chosen();
            $("[id$=ddl_BD_New_Trip]").chosen();
            $("[id$=ddl_Route_NewTrip]").chosen();
            $("[id$=cbTransferCH1]").chosen();
            $("[id$=cbTransferCH2]").chosen();
            $("[id$=cbReasons]").chosen();
            $("[id$=cbReasons2]").chosen();
            $("[id*=cbCheck2Edit]").chosen();
            $("[id*=cbCheck3Edit]").chosen();
        }

        $(document).ready(function () {
            $('.tooltips').tooltip();
        });

    </script>
    <script type="text/JavaScript">

        function validateHhMm(inputField) {
            var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField.value);

            if (isValid) {
                inputField.style.backgroundColor = '#bfa';
            } else {
                inputField.style.backgroundColor = '#fba';
            }

            return isValid;
        }

        function get_time(id) {
            var dt = new Date();
            var minutes;
            var elem = $("#" + id + "").val();
            if (dt.getMinutes() < 10)
            { minutes = '0' + dt.getMinutes(); } else { minutes = dt.getMinutes(); }
            if (elem.trim().length == 0) {
                var time = dt.getHours() + ":" + minutes;
                //":" + (
                //dt.getMinutes() < 10 ? '0' : '' + dt.getMinutes());
                document.getElementById(id).value = time;
            }
        }

        function ShowInfoToEdit() {
            $('#collapseGOne').collapse('show');
        }


        function confirmOperation(uniqueID, title, div , width , height) {
            var dialogTitle = title;
            $(div).html();

            $(div).dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: width,
                height: height,
                buttons: {
                    "Accept": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    "Cancel": function () { $(this).dialog("close"); }
                }
            });

            $(div).dialog('open');
            return false;
        }

      

        function openGenericDialog(element , width , height , title) {
            var dialogTitle = title;
            $(element).html();
            $(element).dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: width,
                height: height,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $(element).dialog('open');
            return false;
        }

      
        function genericCloseDialog(element) {
            $(element).dialog("close");
        }


    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UPWorking" runat="server" AssociatedUpdatePanelID="processingPanel">
        <ProgressTemplate>
            <div class="fillTotalScreen">
                <div class="centerWaitingImage">
                    <img alt="" src="../images/Processing.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./Daily_Operation.aspx" title="Daily Operation" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_DailyOperation_Title" runat="server" /></a>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span8">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>
                            <asp:Label ID="lbl_DailySchedule_Available" runat="server" /></h5>
                    </div>

                    <div class="widget-content">
                        <div class="controls controls-row">
                            <div class="span12">

                                <div class="span3 m-wrap">
                                    <strong>
                                        <asp:Label ID="lbl_DailySchedule_Vendor" runat="server" /></strong>
                                    <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" TabIndex="1" Width="100%" CssClass="chosen-select">
                                    </asp:DropDownList>
                                </div>

                                <div class="span3 m-wrap">
                                    <strong>
                                        <asp:Label ID="lbl_DailySchedule_Shift" runat="server" /></strong>
                                    <asp:DropDownList runat="server" ID="cbShiftType" AppendDataBoundItems="true" TabIndex="2" OnSelectedIndexChanged="cbShiftType_SelectedIndexChanged" AutoPostBack="true" Width="100%" CssClass="chosen-select">
                                        <asp:ListItem Value="NONE" Text="Seleccionar" />
                                        <asp:ListItem Value="0" Text="Entrada" />
                                        <asp:ListItem Value="1" Text="Salida" />
                                    </asp:DropDownList>
                                </div>

                                <div class="span3 m-wrap">
                                    <strong>
                                        <asp:Label ID="lbl_DailySchedule_ShiftName" runat="server" /></strong>
                                    <asp:DropDownList runat="server" ID="cbShift" AppendDataBoundItems="true" TabIndex="3" Width="100%" CssClass="chosen-select">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="controls controls-row">
                            <div class="span12">


                                <div class="span3 ">
                                    <asp:Button ID="btLoadData" CausesValidation="False" OnClientClick="javascript:return confirmOperation(this.name, 'Confirmacion Carga de turno' , '#dialog-ChargeShift' , 330 , 200);" OnClick="loadData" CssClass="btn btn-success" runat="server" TabIndex="4" />
                                </div>
                                <div class="span3 ">
                                    <asp:Button ID="btChange" CausesValidation="False" CssClass="btn btn-primary" TabIndex="5"
                                        runat="server" UseSubmitBehavior="false" OnClick="btChange_Click" Enabled="false" />
                                </div>

                                <div class="span3 ">
                                    <asp:Button ID="btExcel" CausesValidation="False" CssClass="btn btn-success" TabIndex="6"
                                         runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                                </div>

                            </div>
                        </div>
                        <div class="controls controls-row">
                            <div id="additionalInfo" style="background: #f2fcdf; padding: 5px;" class="span12 m-wrap">
                                <span style="font-weight: bold; padding: 15px;">
                                    <asp:Label ID="lbl_DailyOperation_Date" runat="server" /></span>
                                <asp:Label ID="lbDate" runat="server" />
                                <span style="font-weight: bold; padding: 15px;">
                                    <asp:Label ID="lbl_DailyOperation_Proveedor" runat="server" /></span>
                                <asp:Label ID="lbVendorInfo" runat="server" />
                                <span style="font-weight: bold; padding: 15px;">
                                    <asp:Label ID="lbl_DailyOperation_Shift" runat="server" /></span>
                                <asp:Label ID="lbShiftInfo" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="span4">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-ban-circle"></i></span>
                        <h5>
                            <asp:Label ID="lbl_DailyOperation_Observation" runat="server" /></h5>
                    </div>
                    <div class="widget-content">
                        <div class="controls controls-row">
                            <div class="span12">
                                <asp:Button ID="btInactiveDetail" runat="server" CssClass="btn btn-warning " TabIndex="7"
                                     OnClick="btInactiveDetail_Click" />
                                <span>
                                    <asp:Label ID="inactiveCount" runat="server" CssClass="badge badge-inverse" Text="0"></asp:Label></span>
                            </div>
                        </div>
                        <div class="controls controls-row">
                            <div class="span12">
                                <asp:Button ID="btRevisionDetail" runat="server" CssClass="btn btn-info " TabIndex="8" OnClick="btRevisionDetail_Click" />
                                <span>
                                    <asp:Label ID="revisionCount" runat="server" CssClass="badge badge-inverse" Text="0"></asp:Label></span>
                            </div>
                        </div>
                        <div class="controls controls-row">
                            <div class="span12">
                                <asp:Button ID="btAlertDetail" runat="server" CssClass="btn btn-danger "  TabIndex="9" OnClick="btAlertDetail_Click" />
                                <span>
                                    <asp:Label ID="AlertCount" runat="server" CssClass="badge badge-inverse" Text="0"></asp:Label></span>
                            </div>
                        </div>
                        <div class="controls controls-row">
                        </div>
                    </div>
                </div>
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-box">
                        <div class="widget-title">
                            <ul class="nav nav-tabs">
                                <li class="active"><a data-toggle="tab" href="#tab1">
                                    <asp:Label ID="lbl_DailyOperation_CH1" runat="server" /></a></li>
                                <li><a data-toggle="tab" href="#tab2">
                                    <asp:Label ID="lbl_DailyOperation_CH2" runat="server" /></a></li>
                                <li><a data-toggle="tab" href="#tab3">
                                    <asp:Label ID="lbl_DailyOperation_CH3" runat="server" /></a></li>
                            </ul>

                        </div>
                        <div class="widget-content tab-content">
                            <div id="tab1" class="tab-pane active">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:HiddenField ID="masterTripIDHidden" runat="server" />
                                <asp:HiddenField ID="statusMasterTripHidden" runat="server" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <%--    <div class="searchGrid">
                                            <span>Search: </span>
                                            <asp:TextBox ID="tbSearchCH1" runat="server" Width="25%" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" Enabled="false" OnClick="btSearchCH1_Click1"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                        </div>--%>
                                        <div class="searchGrid">
                                            <div class="pull-left" style="width: 50%;">
                                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                                    <span>
                                                        <asp:Label ID="lbl_DailyOperation_SearchCH1" runat="server" /></span>
                                                    <asp:TextBox ID="tbSearchCH1"  style="width: 50%;" runat="server" 
                                                        PlaceHolder="Ingresa Camión, Conductor o Ruta" /><asp:LinkButton ID="btSearchCH1" runat="server"
                                                             CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click1"><span class="icon-search" 
                                                                  aria-hidden="true"></span></asp:LinkButton>
                                                </asp:Panel>
                                            </div>
                                            <div style="float: right">
                                                <asp:Button ID="btRefreshCH1" CausesValidation="False" CssClass="btn btn-primary" runat="server" TabIndex="10"
                                                     UseSubmitBehavior="false" OnClick="btRefreshCH1_Click" Enabled="false" />
                                            </div>
                                        </div>
                                        <div class="searchGrid">
                                            <asp:Label ID="lbl_DST_Camion" runat="server"></asp:Label>
                                            <asp:DropDownList runat="server" ID="ddl_BD_New_Trip" Width="150px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="val_DailyOperation_rFcbBusDriverHeader"
                                                runat="server"
                                                ControlToValidate="ddl_BD_New_Trip"
                                                ErrorMessage='Relacion camion-conductor es requerida'
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                Text="*"
                                                ValidationGroup="addModeCT1">
                                            </asp:RequiredFieldValidator>
                                            <asp:Label ID="lbl_DST_Recorrido" runat="server"></asp:Label>
                                            <asp:DropDownList runat="server" ID="ddl_Route_NewTrip" AppendDataBoundItems="true" TabIndex="11" Width="115px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="val_DailyOperation_rFcbRouteHeader"
                                                runat="server"
                                                ControlToValidate="ddl_Route_NewTrip"
                                                ErrorMessage='La ruta es requerida'
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                Text="*"
                                                ValidationGroup="addModeCT1">
                                            </asp:RequiredFieldValidator>
                                            <asp:Label ID="lbl_DST_CheckpointTim" runat="server"></asp:Label>
                                            <asp:TextBox ID="tbCheckPoint3" runat="server" Width="10%" PlaceHolder="00:00"  TabIndex="12" onkeypress="return onlyNumbersWidthColon(event)"
                                                onFocus="get_time(this.id)" onchange="validateHhMm(this);" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="val_DailyOperation_rFtbCheckPoint3"
                                                runat="server"
                                                ControlToValidate="tbCheckPoint3"
                                                ErrorMessage='Tiempo de Checkpoint es requerido'
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                Text="*"
                                                ValidationGroup="addModeCT1">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RF1" runat="server" ErrorMessage="Format time 00:00"
                                                ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbCheckPoint3" Text="*" ValidationGroup="addModeCT1">
                                            </asp:RegularExpressionValidator>
                                            <asp:Label ID="lbl_DO_Comments" runat="server"></asp:Label>
                                            <asp:TextBox ID="tbComments" runat="server" Width="10%" MaxLength="150"></asp:TextBox>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" OnClick="btAddHeader" ValidationGroup="addModeCT1"
                                                CssClass="btn btn-success" />
                                        </div>
                                        <asp:GridView ID="Grid_Checkpoint1" runat="server"
                                            AutoGenerateColumns="false" CssClass="table table-bordered data-table" OnRowDataBound="Grid_Checkpoint1_RowDataBound"
                                            OnRowEditing="Grid_Checkpoint1_RowEditing" AllowSorting="true"
                                            OnRowUpdating="Grid_Checkpoint1_RowUpdating" OnRowCancelingEdit="Grid_Checkpoint1_CancelingEdit"
                                            AllowPaging="true" OnRowDeleting="Grid_Checkpoint1_RowDeleting"
                                            EmptyDataText="Por el momento no hay informacion." ShowHeaderWhenEmpty="true" PageSize="15"
                                            OnSorting="Grid_Checkpoint1_Sorting" OnPageIndexChanging="Grid_Checkpoint1_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Trip ID" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbTripHD" runat="server"
                                                            Text='<%# Eval("Trip_Hrd_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus-Driver ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbBusDriverID" runat="server"
                                                            Text='<%# Eval("Bus_Driver_ID")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End_Point1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="lbEndPoint" runat="server"
                                                            Checked='<%# Eval("End_Point")%>'></asp:CheckBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transbord1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbTransbordCH1" runat="server"
                                                            Text='<%# Eval("TransbordCH1")%>'></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus-Driver" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbBusDriverHide" runat="server" Text='<%# Eval("Bus_Driver")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus-Driver" SortExpression="Bus-Driver">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="Bus_Driver_ID_text" runat="server" Text="Bus_Driver" CommandName="Sort" CommandArgument="Bus_Driver"></asp:LinkButton>
                                                        <br />

                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbBusDriver" runat="server" Text='<%# Eval("Bus_Driver")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="cbBusDriver1" OnSelectedIndexChanged="cbBusDriver1_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="true" Width="100%">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFcbBusDriverEdit"
                                                            runat="server"
                                                            ControlToValidate="cbBusDriver1"
                                                            ErrorMessage='Relacion camion-conductor es requerida'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus ID" SortExpression="Bus_ID">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="Bus_ID_text" runat="server" Text="Bus_ID" CommandName="Sort" CommandArgument="Bus_ID"></asp:LinkButton>
                                                        <br />
                                                        <%--
                                                 <asp:TextBox ID="tbBusIDH" runat="server" Enabled="false" Width="50%" ></asp:TextBox>--%>
                                                        <asp:Label ID="lbBusIDH" runat="server" Visible="false"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgAlertBus" runat="server" ImageUrl="~/images/warning.png" Visible="false" />
                                                        <asp:Label ID="lbBus" runat="server" Text='<%# Eval("Bus_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Driver" SortExpression="Driver">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="Driver_text" runat="server" Text="Driver" CommandName="Sort" CommandArgument="Driver"></asp:LinkButton>
                                                        <br />
                                                        <%--  <asp:TextBox ID="tbDriverH" runat="server" Enabled="false" Width="70%" ></asp:TextBox>--%>
                                                        <asp:Label ID="lbDriverH" runat="server" Visible="false"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgAlertDriver" runat="server" ImageUrl="~/images/warning.png" Visible="false" ImageAlign="Middle" />
                                                        <br></br>
                                                        <asp:Label ID="lbDriverName" runat="server"
                                                            Text='<%# Eval("Driver")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Driver_ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbDriverID" runat="server"
                                                            Text='<%# Eval("Driver_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbRouteHide" runat="server"
                                                            Text='<%# Eval("Route")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route" SortExpression="Route">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="Route_text" runat="server" Text="Route" CommandName="Sort" CommandArgument="Route"></asp:LinkButton>
                                                        <br />

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbRoute" runat="server"
                                                            Text='<%# Eval("Route")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="cbRoute1" AppendDataBoundItems="true" TabIndex="6" Width="100%">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFcbRouteEdit"
                                                            runat="server"
                                                            ControlToValidate="cbRoute1"
                                                            ErrorMessage='La ruta es requerida'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Checkpoint Time" ControlStyle-Width="80%" SortExpression="Check_Point_Time">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="Check_Point_Time_text" runat="server" Text="Check_Point_Time" CommandName="Sort" CommandArgument="Check_Point_Time"></asp:LinkButton>
                                                        <br />

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbCheckpoint1" runat="server"
                                                            Text='<%# Eval("Check_Point_Time")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbCheckPoint2" runat="server" Width="100%"
                                                            Text='<%# Eval("Check_Point_Time")%>' placeholder="00:00" onkeypress="return onlyNumbersWidthColon(event)" onchange="validateHhMm(this);" MaxLength="5"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFtbCheckPoint2"
                                                            runat="server"
                                                            ControlToValidate="tbCheckPoint2"
                                                            ErrorMessage='Tiempo de Checkpoint es requerido'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*"
                                                            ValidationGroup="editModeCT1">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RF6" runat="server" ErrorMessage="Format time 00:00"
                                                            ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbCheckPoint2" Text="*" ValidationGroup="editModeCT1">
                                                        </asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comments" ControlStyle-Width="85%">
                                                    <HeaderTemplate>
                                                        Comments
                                                    <br />

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbComments" runat="server"
                                                            Text='<%# Eval("Comment")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbComments" runat="server" Width="140px" Text='<%# Eval("Comment")%>' MaxLength="150"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Time" ControlStyle-Width="80%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbHoraIni" runat="server" Text='<%# Eval("Trip_Start")%>' Enabled="false">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>

                                                        <asp:TextBox ID="tbHoraIni2" runat="server" Text='<%# Eval("Trip_Start")%>' PlaceHolder="00:00" onkeypress="return onlyNumbersWidthColon(event)" onchange="validateHhMm(this);" onFocus="get_time(this.id)" MaxLength="5" Width="90%" ToolTip=""></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RF7" runat="server" ErrorMessage="Format time 00:00"
                                                            ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbHoraIni2" Text="*" ValidationGroup="editModeCT1">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFtbHoraIni2"
                                                            runat="server"
                                                            ControlToValidate="tbHoraIni2"
                                                            ErrorMessage='Tiempo de inicio es requerido'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*"
                                                            ValidationGroup="editModeCT1">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="# Pass" ControlStyle-Width="50%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbNoPassengers" runat="server" Text='<%# Eval("Psg_Init")%>' Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbNoPassengers2" runat="server" Text='<%# Eval("Psg_Init")%>' onkeypress="return onlyNumbersWidthColon(event)" MaxLength="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFtbNoPassengers2"
                                                            runat="server"
                                                            ControlToValidate="tbNoPassengers2"
                                                            ErrorMessage='Cantidad de pasajeros es requerida'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*"
                                                            ValidationGroup="editModeCT1">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" ButtonType="Image" ValidationGroup="editModeCT1" EditImageUrl="~/images/edit-bus1.png" UpdateImageUrl="~/images/save1.png" CancelImageUrl="~/images/cancel-edit1.png" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <br />

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkRemove" runat="server"
                                                    CommandArgument='<%# Eval("Trip_Hrd_ID")%>'
                                                    Text="Delete" OnClientClick = "return confirm('Do you want to delete?')" OnClick="btDelete_Click" CssClass="btn btn-danger" CausesValidation="False"></asp:LinkButton>
                                                        --%>
                                                        <asp:ImageButton ID="IBtnDelete" runat="server"
                                                            CommandArgument='<%# Eval("Trip_Hrd_ID") %>'
                                                            OnClientClick="javascript:return confirmOperation(this.name, 'Desea eliminar este elemento?' , '#dialog-confirm' , 330 , 200);"
                                                            AlternateText="Delete"
                                                            CommandName="Delete" ImageUrl="~/images/delete-bus1.png" CausesValidation="false" BorderWidth="0" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkRemove" runat="server"
                                                    CommandArgument='<%# Eval("Trip_Hrd_ID")%>'
                                                    Text="Delete" OnClientClick = "return confirm('Do you want to delete?')" OnClick="btDelete_Click" CssClass="btn btn-danger" CausesValidation="False"></asp:LinkButton>
                                                        --%>
                                                        <asp:ImageButton ID="btTransfer" runat="server"
                                                            CommandArgument='<%# Eval("Trip_Hrd_ID") %>'
                                                            OnClick="btTransfer_Click"
                                                            CommandName="Transfer" ImageUrl="~/images/transfer1.png" CausesValidation="false" BorderWidth="0" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:ValidationSummary
                                            ID="valSumAddMode"
                                            runat="server"
                                            HeaderText="Ocurrieron los siguientes errores....."
                                            ShowMessageBox="false"
                                            DisplayMode="BulletList"
                                            BackColor="Snow"
                                            ForeColor="Red"
                                            Font-Italic="true"
                                            ValidationGroup="addModeCT1" />

                                        <asp:ValidationSummary
                                            ID="valSumEditMode"
                                            runat="server"
                                            HeaderText="Ocurrieron los siguientes errores....."
                                            ShowMessageBox="false"
                                            DisplayMode="BulletList"
                                            BackColor="Snow"
                                            ForeColor="Red"
                                            Font-Italic="true"
                                            ValidationGroup="editModeCT1" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Grid_Checkpoint1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div id="tab2" class="tab-pane">

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <%-- <div class="searchGrid">
                                            <span>Search: </span>
                                            <asp:TextBox ID="tbSearchCH2" runat="server" Width="25%" /><asp:LinkButton ID="btSearchCH2" runat="server" CssClass="btn btn-default" Enabled="false" OnClick="btSearchCH2_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                        </div>--%>
                                        <div class="searchGrid">
                                            <div class="pull-left" style="width: 50%;">
                                                <asp:Panel runat="server" DefaultButton="btSearchCH2">
                                                    <span>
                                                        <asp:Label ID="lbl_DailyOperation_Search2" runat="server" /></span>
                                                    <asp:TextBox ID="tbSearchCH2" style="width: 50%;" runat="server" PlaceHolder="Ingresa Camión, Conductor o Ruta" /><asp:LinkButton ID="btSearchCH2" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH2_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                                </asp:Panel>
                                            </div>
                                            <div style="float: right">
                                                <asp:Button ID="btRefreshCH2" CausesValidation="False" CssClass="btn btn-primary" runat="server" UseSubmitBehavior="false" OnClick="btRefreshCH2_Click" Enabled="false" />
                                            </div>
                                        </div>
                                        <asp:GridView ID="Grid_CheckPoint2" runat="server"
                                            AutoGenerateColumns="false" CssClass="table table-bordered data-table"
                                            OnRowEditing="Grid_CheckPoint2_RowEditing" AllowSorting="true" OnSorting="Grid_CheckPoint2_Sorting"
                                            OnRowUpdating="Grid_CheckPoint2_RowUpdating" OnRowCancelingEdit="Grid_CheckPoint2_RowCancelingEdit"
                                            AllowPaging="true" OnRowDataBound="Grid_CheckPoint2_RowDataBound" OnPageIndexChanging="Grid_CheckPoint2_PageIndexChanging"
                                            EmptyDataText="Por el momento no hay informacion." ShowHeaderWhenEmpty="true" PageSize="15">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Trip ID" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbTripHD" runat="server"
                                                            Text='<%# Eval("Trip_Hrd_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bus ID" SortExpression="Bus_ID">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="Bus_ID_text" runat="server" Text="Bus_ID" CommandName="Sort" CommandArgument="Bus_ID">

                                                        </asp:LinkButton>
                                                        <br />
                                                        <%--
                                                 <asp:TextBox ID="tbBusIDH" runat="server" Enabled="false" Width="50%" ></asp:TextBox>--%>
                                                        <asp:Label ID="lbBusIDH" runat="server"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbBus" runat="server" Text='<%# Eval("Bus_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Driver" SortExpression="Name">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbDriverName" runat="server"
                                                            Text='<%# Eval("Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End_Point2" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="lbEndPointCH2" runat="server"
                                                            Checked='<%# Eval("End_Point2")%>'></asp:CheckBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transbord2" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbTransbordCH2" runat="server"
                                                            Text='<%# Eval("TransbordCH2")%>'></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stop Point" SortExpression="Stop_Point_ID">

                                                    <ItemTemplate>
                                                        <%--  <asp:Label ID="lbRoute" runat="server"
                                                        Text='<%# Eval("Route")%>'></asp:Label>--%>
                                                        <asp:Label runat="server" ID="cbCheck2Item" Text='<%# Eval("Stop_Point_ID")%>' CssClass="text-center">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="cbCheck2Edit" AppendDataBoundItems="true" CssClass="chosen-select">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFcbCheck2EdiR"
                                                            runat="server"
                                                            ControlToValidate="cbCheck2Edit"
                                                            ErrorMessage='La ruta es requerida'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Checkpoint Time" ControlStyle-Width="50%" SortExpression="Trip_Start">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tbHoraCheck" runat="server" Text='<%# Eval("Trip_Start")%>' Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbHoraCheck2" runat="server" Text='<%# Eval("Trip_Start")%>' placeholder="00:00" onkeypress="return onlyNumbersWidthColon(event)" onchange="validateHhMm(this);" onFocus="get_time(this.id)" MaxLength="5"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFtbHoraCheck2"
                                                            runat="server"
                                                            ControlToValidate="tbNoPassengersCheck2"
                                                            ErrorMessage='Tiempo en CheckPoint es requerido'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*"
                                                            ValidationGroup="editModeCP2" />
                                                        <asp:RegularExpressionValidator ID="RF7" runat="server" ErrorMessage="Format time 00:00"
                                                            ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbHoraCheck2" Text="*" ValidationGroup="editModeCP2">
                                                        </asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="# Passengers" ControlStyle-Width="50%" SortExpression="Psg_Init">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tbNoPassengersCheck" runat="server" Text='<%# Eval("Psg_Init")%>' Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbNoPassengersCheck2" runat="server" Text='<%# Eval("Psg_Init")%>' onkeypress="return onlyNumbers(event)" MaxLength="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFtbNoPassengersCheck2"
                                                            runat="server"
                                                            ControlToValidate="tbNoPassengersCheck2"
                                                            ErrorMessage='Cantidad de pasajeros es requerida'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*"
                                                            ValidationGroup="editModeCP2">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comment" SortExpression="CommentsCH2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbComment" runat="server"
                                                            Text='<%# Eval("CommentsCH2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="lbCommentEdit" runat="server" Text='<%# Eval("CommentsCH2")%>' MaxLength="150"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" ButtonType="Image" ValidationGroup="editModeCP2" EditImageUrl="~/images/edit-bus1.png" UpdateImageUrl="~/images/save1.png" CancelImageUrl="~/images/cancel-edit1.png" />

                                                <asp:TemplateField>

                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkRemove" runat="server"
                                                    CommandArgument='<%# Eval("Trip_Hrd_ID")%>'
                                                    Text="Delete" OnClientClick = "return confirm('Do you want to delete?')" OnClick="btDelete_Click" CssClass="btn btn-danger" CausesValidation="False"></asp:LinkButton>
                                                        --%>
                                                        <asp:ImageButton ID="btTransfer2" runat="server"
                                                            CommandArgument='<%# Eval("Trip_Hrd_ID") %>'
                                                            OnClick="btTransfer2_Click"
                                                            AlternateText="Transfer"
                                                            CommandName="Transfer" ImageUrl="~/images/transfer1.png" CausesValidation="false" BorderWidth="0" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btEndPoint" runat="server"
                                                            CommandArgument='<%# Eval("Trip_Hrd_ID") %>'
                                                            OnClick="btEndPoint_Click"
                                                            OnClientClick="javascript:return confirmOperation(this.name, 'Desea finalizar este viaje?', '#dialog-confirmEndPoint' , 330 , 200);"
                                                            AlternateText="End Route"
                                                            CommandName="EndRoute" ImageUrl="~/images/endPoint.png" CausesValidation="false" BorderWidth="0" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:ValidationSummary
                                            ID="val_DailyOperation_vSeditModeCP2"
                                            runat="server"
                                            HeaderText="Ocurrieron los siguientes errores....."
                                            ShowMessageBox="false"
                                            DisplayMode="BulletList"
                                            BackColor="Snow"
                                            ForeColor="Red"
                                            Font-Italic="true"
                                            ValidationGroup="editModeCP2" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Grid_CheckPoint2" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                            <div id="tab3" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <%--    <div class="searchGrid">
                                            <span>Search: </span>
                                            <asp:TextBox ID="tbSearchCH3" runat="server" Width="25%" /><asp:LinkButton ID="btSearchCH3" runat="server" CssClass="btn btn-default" Enabled="false" OnClick="btSearchCH3_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                        </div>--%>
                                        <div class="searchGrid">
                                            <div class="pull-left" style="width: 50%;">
                                                <asp:Panel runat="server" DefaultButton="btSearchCH3">
                                                    <span>
                                                        <asp:Label ID="lbl_DailyOperation_Search3" runat="server" /></span>
                                                    <asp:TextBox ID="tbSearchCH3" style="width: 50%;" runat="server" PlaceHolder="Ingresa Camión, Conductor o Ruta" /><asp:LinkButton ID="btSearchCH3" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH3_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                                </asp:Panel>
                                            </div>
                                            <div style="float: right">
                                                <asp:Button ID="btRefreshCH3" CausesValidation="False" CssClass="btn btn-primary" runat="server" UseSubmitBehavior="false" OnClick="btRefreshCH3_Click" Enabled="false" />
                                            </div>
                                        </div>
                                        <asp:GridView ID="Grid_CheckPoint3" runat="server"
                                            AutoGenerateColumns="false" CssClass="table table-bordered data-table"
                                            OnRowEditing="Grid_CheckPoint3_RowEditing" AllowSorting="true"
                                            OnRowUpdating="Grid_CheckPoint3_RowUpdating" OnRowCancelingEdit="Grid_CheckPoint3_RowCancelingEdit"
                                            AllowPaging="true" OnRowDataBound="Grid_CheckPoint3_RowDataBound" OnPageIndexChanging="Grid_CheckPoint3_PageIndexChanging"
                                            EmptyDataText="Por el momento no hay informacion." ShowHeaderWhenEmpty="true" PageSize="15" OnSorting="Grid_CheckPoint3_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Trip ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbTripHD3" runat="server"
                                                            Text='<%# Eval("Trip_Hrd_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus ID" SortExpression="Bus_ID">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="Bus_ID_text3" runat="server" Text="Bus_ID" CommandName="Sort" CommandArgument="Bus_ID"></asp:LinkButton>
                                                        <br />
                                                        <asp:Label ID="lbBusID3" runat="server"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbBus3" runat="server" Text='<%# Eval("Bus_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Driver" SortExpression="Name">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbDriverName3" runat="server"
                                                            Text='<%# Eval("Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End_Point3" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="lbEndPoint3" runat="server"
                                                            Checked='<%# Eval("End_Point3")%>'></asp:CheckBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transbord3" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbTransbordCH3" runat="server"
                                                            Text='<%# Eval("TransbordCH3")%>'></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stop Point" SortExpression="Stop_Point_ID">

                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="cbCheck3Item" Text='<%# Eval("Stop_Point_ID")%>' CssClass="text-center">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="cbCheck3Edit" AppendDataBoundItems="true"  CssClass="chosen-select">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFcbCheck3EdiR"
                                                            runat="server"
                                                            ControlToValidate="cbCheck3Edit"
                                                            ErrorMessage='La ruta es requerida'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Checkpoint Time" ControlStyle-Width="50%" SortExpression="Trip_Start">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tbHoraCheck3" runat="server" Text='<%# Eval("Trip_Start")%>' Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbHoraCheckEdit3" runat="server" Text='<%# Eval("Trip_Start")%>' PlaceHolder="00:00" onkeypress="return onlyNumbersWidthColon(event)" onchange="validateHhMm(this);" onFocus="get_time(this.id)" MaxLength="5"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFtbHoraCheckEdit3"
                                                            runat="server"
                                                            ControlToValidate="tbHoraCheckEdit3"
                                                            ErrorMessage='Tiempo de Checkpoint es requerido'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*"
                                                            ValidationGroup="editModeCP3" />
                                                        <asp:RegularExpressionValidator ID="RF7" runat="server" ErrorMessage="Format time 00:00"
                                                            ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbHoraCheckEdit3" Text="*" ValidationGroup="editModeCP3">
                                                        </asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="# Passengers" ControlStyle-Width="50%" SortExpression="Psg_Init">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tbNoPassengersCheck3" runat="server" Text='<%# Eval("Psg_Init")%>' Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbNoPassengersCheckEdit3" runat="server" Text='<%# Eval("Psg_Init")%>' onkeypress="return onlyNumbers(event)" MaxLength="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="val_DailyOperation_rFtbNoPassengersCheckEdit3"
                                                            runat="server"
                                                            ControlToValidate="tbNoPassengersCheckEdit3"
                                                            ErrorMessage='Cantidad de pasajeros es requerida'
                                                            EnableClientScript="true"
                                                            SetFocusOnError="true"
                                                            ForeColor="Red"
                                                            CssClass="span1 m-wrap"
                                                            Text="*"
                                                            ValidationGroup="editModeCP3">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbComment3" runat="server"
                                                            Text='<%# Eval("CommentsCH3")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="lbCommentEdit3" runat="server" Text='<%# Eval("CommentsCH3")%>' MaxLength="150"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField ShowEditButton="True" ButtonType="Image" ValidationGroup="editModeCP3" EditImageUrl="~/images/edit-bus1.png" UpdateImageUrl="~/images/save1.png" CancelImageUrl="~/images/cancel-edit1.png" />

                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:ValidationSummary
                                            ID="val_DailyOperation_vSeditModeCP3"
                                            runat="server"
                                            HeaderText="Ocurrieron los siguientes errores....."
                                            ShowMessageBox="false"
                                            DisplayMode="BulletList"
                                            BackColor="Snow"
                                            ForeColor="Red"
                                            Font-Italic="true"
                                            ValidationGroup="editModeCP3" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Grid_CheckPoint3" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                        </div>
                    </div>


                </div>
            </div>
            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-info-sign"></i></span>
                    <h5>
                        <asp:Label ID="lbl_DailyOperation_Confirmation" runat="server" /></h5>
                </div>
                <div class="widget-content tab-content">
                    <asp:UpdatePanel ID="processingPanel" runat="server">
                        <ContentTemplate>
                            <div style="float: right;">
                                <asp:Button ID="btConfirmOperation" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return confirmOperation(this.name, 'Desea confirmar esta operacion' , '#dialog-confirmOperation' , 300 , 200);" OnClick="btConfirmOperation_Click" Enabled="false" CausesValidation="false"></asp:Button>
                                <asp:Button ID="btCancelOperation" runat="server" CssClass="btn btn-danger" OnClientClick="javascript:return confirmOperation(this.name , 'Desea cancelar esta operacion' , '#dialog-cancelOperation' , 300 , 200);" OnClick="btCancelOperation_Click" Enabled="false" CausesValidation="false"></asp:Button>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>


            <asp:Panel ID="pnlInfo" runat="server" HorizontalAlign="Center" BackColor="White" BorderColor="White" BorderWidth="3px">
                <button type="button" id="example4" data-content="Elementos de este color estan completos" 
                    data-placement="top" data-toggle="popover" class="btn highlightRowGreen" data-original-title="Viajes completos"></button>
                <span><asp:Label ID="lbl_DailyOPeration_Complete" runat="server" /></span>
                <button type="button" id="example2" data-content="Elementos de este color transbordaron" 
                    data-placement="top" data-toggle="popover" class="btn highlightRowYellow" data-original-title="Viajes transbordados"></button>
                <span><asp:Label ID="lbl_DailyOPeration_Transfered" runat="server" /></span>
                <button type="button" id="example3" data-content="Elementos de este color terminaron antes del CheckPoint 3" 
                    data-placement="top" data-toggle="popover" class="btn highlightRowOrange" data-original-title="Viajes terminados"></button>
                <span><asp:Label ID="lbl_DailyOPeration_Ended" runat="server" /></span>
                <button type="button" id="example5" data-content="Elementos de este color empezaron con mas de 10 minutos de diferencia" 
                    data-placement="top" data-toggle="popover" class="btn CellColorStartdif" data-original-title="Diferencia de tiempo en CheckPoint"></button>
                <span><asp:Label ID="lbl_DailyOPeration_CHTime" runat="server" /></span>
                <button type="button" id="example6" data-content="El chofer tiene 10 o mas viajes en las ultimas 24 horas" 
                    data-placement="top" data-toggle="popover" class="btn highlightRowRed0" data-original-title="+10 viajes"></button>
                <span><asp:Label ID="lbl_DailyOPeration_10Trips" runat="server" /></span>
                <button type="button" id="example7" data-content="El chofer tiene 25 o mas viajes en las ultimas 72 horas" 
                    data-placement="top" data-toggle="popover" class="btn highlightRowRed1" data-original-title="+20 viajes"></button>
                <span><asp:Label ID="lbl_DailyOPeration_20Trips" runat="server" /></span>
                <button type="button" id="example1" data-content="El chofer tiene 35 o mas viajes en las ultimas 120 horas" 
                    data-placement="top" data-toggle="popover" class="btn highlightRowRed2" data-original-title="+30 viajes"></button>
                <span><asp:Label ID="lbl_DailyOPeration_30Trips" runat="server" /></span>
            </asp:Panel>
        </div>
    </div>
    <div id="dialog-confirm" title="Estas seguro de eliminar este elemento?" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_DailyOPeration_DialogConfirm" runat="server" />

        </p>
    </div>
    <div id="dialog-transferConfirm" title="Estas seguro de realizar un transbordo?" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_DailyOPeration_transferConfirm" runat="server" />

        </p>
    </div>
      <div id="dialog-ChargeShift" title="Estas seguro de cargar este turno?" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_DailyOperation_ConfirmShift" runat="server" />

        </p>
    </div>
    <div id="dialog-confirmOperation" title="Confirmar operacion" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_DailyOPeration_confirmOperation" runat="server" />
        </p>
    </div>
    <div id="dialog-confirmEndPoint" title="Confirmar fin del viaje" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_DailyOPeration_confirmEndPoint" runat="server" />

        </p>
    </div>
    <div id="dialog-cancelOperation" title="Cancelar operacion" style="display: none">

        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        <p style="color: red;">
            <asp:Label ID="lbl_DailyOPeration_cancelOperation" runat="server" />
        </p>

    </div>
    <div id="transferCheckPoint2" style="display: none">
        <div class="widget-content">
            <div class="form-1">
                <p>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="control-group">
                                <asp:Label ID="lbl_DailyOperation_BDSelected" runat="server" CssClass="control-label"></asp:Label>
                                <asp:Label ID="lbInfoBusDriver2" runat="server" CssClass="control-label"></asp:Label>
                             
                            </div>
                            <br></br>
                            <div class="control-group">
                                <asp:Label ID="lbl_DailyOperation_BDRelation" runat="server"></asp:Label>
                                <asp:DropDownList ID="cbTransferCH2" runat="server" AutoPostBack="true" CssClass="chosen-select" />
                                <asp:Label ID="hiddenCurrentBusCH2" runat="server" Visible="false" />
                                <asp:Label ID="hiddenValueCH2" runat="server" Visible="false" />
                                    <asp:RequiredFieldValidator
                                    ID="val_DO_Trans_cbTransferCH2R"
                                    runat="server"
                                    ControlToValidate="cbTransferCH2"
                                    ErrorMessage='Relacion camion-conductor es requerida'
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    Text="*"
                                    ValidationGroup="TransCH2">
                                </asp:RequiredFieldValidator>
                            </div>
                            <br></br>
                            <div class="control-group">
                                <asp:Label ID="lbl_DailyOperation_Selreason" runat="server"></asp:Label>
                                <asp:DropDownList ID="cbReasons2" runat="server" AppendDataBoundItems="true" CssClass="chosen-select">
                                </asp:DropDownList>
                                       <asp:RequiredFieldValidator
                                    ID="val_DO_cbReasons2R"
                                    runat="server"
                                    ControlToValidate="cbReasons2"
                                    ErrorMessage='Relacion camion-conductor es requerida'
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    Text="*"
                                    ValidationGroup="TransCH2">
                                </asp:RequiredFieldValidator>
                            </div>
                            <br></br>
                            <div class="control-group">
                                <asp:Label ID="lbl_DailyOperation_WriteCmts" runat="server"></asp:Label>
                                <asp:TextBox ID="tbCommentsTCH2" runat="server" TextMode="multiline" Width="100%" Rows="4" />
                                        <asp:RequiredFieldValidator
                                    ID="val_DO_Trans_tbCommentsTCH2R"
                                    runat="server"
                                    ControlToValidate="tbCommentsTCH2"
                                    ErrorMessage='Relacion camion-conductor es requerida'
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    Text="*"
                                    ValidationGroup="TransCH2">
                                </asp:RequiredFieldValidator>
                            </div>
                               <asp:ValidationSummary
                                    ID="val_Sum_Trans_CH2"
                                    runat="server"
                                    HeaderText="Ocurrieron los siguientes errores....."
                                    ShowMessageBox="false"
                                    DisplayMode="BulletList"
                                    BackColor="Snow"
                                    ForeColor="Red"
                                    Font-Italic="true"
                                    ValidationGroup="TransCH2" />
       
                            <div class="control-group">
                                <asp:Button ID="btTransferCH2" runat="server" ValidationGroup="TransCH2" CssClass="btn btn-success" OnClientClick="javascript:return confirmOperation(this.name, 'Confirmacion de transbordo' , '#dialog-transferConfirm' , 330 , 200);" OnClick="btTransferCH2_Click" />
                                <asp:Button ID="btCancelCH2" runat="server" CssClass="btn btn-danger" OnClick="btCancelCH2_Click" CausesValidation="false" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </p>
            </div>
        </div>
    </div>

    <div id="transferCheckPoint1" style="display: none">
        <div class="widget-content">
            <div class="form-1">
                <p>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="control-group">
                                
                                <asp:Label ID="lbl_DailyOperation_BDSelected2" runat="server" CssClass="control-label"></asp:Label>
                                <asp:Label ID="lbInfoBusDriver" runat="server" CssClass="control-label"></asp:Label>
                         
                                  </div>
                            <br></br>
                            <div class="control-group">
                                  <div class="controls">
                                <asp:Label ID="lbl_DailyOperation_BDRelation2" runat="server"></asp:Label>
                                <asp:DropDownList ID="cbTransferCH1" runat="server" AutoPostBack="true" CssClass="chosen-select" />
                                <asp:Label ID="hiddenCurrentBusCH1" runat="server" Visible="false" />
                                <asp:Label ID="hiddenValueCH1" runat="server" Visible="false" />
                                <asp:RequiredFieldValidator
                                    ID="val_DO_Trans_cbTransferCH1R"
                                    runat="server"
                                    ControlToValidate="cbTransferCH1"
                                    ErrorMessage='Relacion camion-conductor es requerida'
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    Text="*"
                                    ValidationGroup="TransCH1">
                                </asp:RequiredFieldValidator>
                                       </div>
                            </div>
                            <br></br>
                            <div class="control-group">
                                 <div class="controls">
                                <asp:Label ID="lbl_DailyOperation_Selreason2" runat="server"></asp:Label>
                                <asp:DropDownList ID="cbReasons" runat="server" AppendDataBoundItems="true" CssClass="chosen-select">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator
                                    ID="val_DO_Trans_cbReasonsR"
                                    runat="server"
                                    ControlToValidate="cbReasons"
                                    ErrorMessage='Una razon es requerida'
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    Text="*"
                                    ValidationGroup="TransCH1">
                                </asp:RequiredFieldValidator>
                            </div> </div>
                            <br></br>
                            <div class="control-group">
                                <div class="controls">
                                <asp:Label ID="lbl_DailyOperation_WriteCmts2" runat="server"></asp:Label>
                                <asp:TextBox ID="tbCommentsTCH1" runat="server" TextMode="multiline" Width="100%" Rows="4" />
                                <asp:RequiredFieldValidator
                                    ID="val_DO_Trans_tbCommentsTCH1"
                                    runat="server"
                                    ControlToValidate="tbCommentsTCH1"
                                    ErrorMessage='Comentarios son requeridos'
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    Text="*"
                                    ValidationGroup="TransCH1">
                                </asp:RequiredFieldValidator>
                                     </div>
                            </div>
                            <div class="control-group">
                                <asp:ValidationSummary
                                    ID="val_Sum_Trans_CH1"
                                    runat="server"
                                    HeaderText="Ocurrieron los siguientes errores....."
                                    ShowMessageBox="false"
                                    DisplayMode="BulletList"
                                    BackColor="Snow"
                                    ForeColor="Red"
                                    Font-Italic="true"
                                    ValidationGroup="TransCH1" />
                            </div>
                            <div class="control-group">

                                <asp:Button ID="btTransferCH1" runat="server" ValidationGroup="TransCH1" CssClass="btn btn-success" OnClientClick="javascript:return confirmTransfer(this.name);" OnClick="btTransferCH1_Click" />
                                <asp:Button ID="btCancelCH1" runat="server" CssClass="btn btn-danger" OnClick="btCancelCH1_Click" CausesValidation="false" />

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </p>
            </div>
        </div>
    </div>

    <div id="InactiveDetails" style="display: none">
        <div class="widget-box">
            <div class="widget-content">
                <div class="alert alert-block">
                    <h4 class="alert-heading">
                        <asp:Label ID="lbl_DailyOperation_Warning" runat="server" /></h4>
                    <asp:Label ID="lbl_DailyOperation_WarningDesc" runat="server" />
                    <br />
                    <a href="../Catalogos/C_BusDriver.aspx" target="_blank">
                        <asp:Label ID="lbl_DailyOperation_EnterBDCatalog" runat="server" /></a>,
                    <a href="../Catalogos/C_Bus.aspx" target="_blank">
                        <asp:Label ID="lbl_DailyOperation_BusCatalog" runat="server" /></a>
                    <asp:Label ID="lbl_DailyOperation_or" runat="server" />
                    <a href="../Catalogos/C_Driver.aspx" target="_blank">
                        <asp:Label ID="lbl_DailyOperation_DriverCatalog" runat="server" /></a>
                    <asp:Label ID="lbl_DailyOperation_toChangeStatus" runat="server" />
                </div>

                <div class="table">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="searchGrid">
                                <span>
                                    <asp:Label ID="lbl_DailyOperation_SearchID" runat="server" /></span>
                                <asp:TextBox ID="searchInactive" runat="server" Width="25%" />
                                <asp:LinkButton ID="LinkButtonInactive" runat="server" CssClass="btn btn-default" Enabled="true" OnClick="LinkButtonInactive_Click1"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                            </div>
                            <asp:GridView ID="GridView_InactiveDetails" runat="server" Width="100%"
                                CssClass="table table-bordered data-table"
                                AllowPaging="True"
                                EmptyDataText="No hay informacion."
                                ShowHeaderWhenEmpty="true"
                                OnPageIndexChanging="GridView_InactiveDetails_PageIndexChanging"
                                PageSize="15">
                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="RevisionDetails" style="display: none">
        <div class="widget-box">
            <div class="widget-content">
                <div class="alert alert-block">
                    <h4 class="alert-heading">
                        <asp:Label ID="lbl_DailyOperation_WarningRD" runat="server" /></h4>
                    <asp:Label ID="lbl_DailyOperation_RevDetails" runat="server" />
                    <br />
                    <a href="../Maintenance/Maintenance_I.aspx" target="_blank">
                        <asp:Label ID="lbl_DailyOperation_RevisionPage" runat="server" /></a>,
                    <asp:Label ID="lbl_DailyOperation_RevisionOr" runat="server" />
                    <a href="Alerts_Log.aspx" target="_blank">
                        <asp:Label ID="lbl_DailyOperation_RevisionAlerts" runat="server" /></a>
                    <asp:Label ID="lbl_DailyOperation_RevisionChange" runat="server" />
                </div>

                <div class="table">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <div class="searchGrid">
                                <span>
                                    <asp:Label ID="lbl_DailyOperation_SearchRD" runat="server" /></span>
                                <asp:TextBox ID="searchRevision" runat="server" Width="25%" />
                                <asp:LinkButton ID="btSearchRevision" runat="server" CssClass="btn btn-default" Enabled="true" OnClick="btSearchRevision_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                            </div>
                            <asp:GridView ID="GridView_Revision" runat="server" Width="100%"
                                CssClass="table table-bordered data-table"
                                AllowPaging="True"
                                EmptyDataText="No hay informacion"
                                ShowHeaderWhenEmpty="true"
                                OnPageIndexChanging="GridView_Revision_PageIndexChanging"
                                PageSize="15">
                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="AlertDetails" style="display: none">
        <div class="widget-box">
            <div class="widget-content">
                <div class="alert alert-block">
                    <h4 class="alert-heading">
                        <asp:Label ID="lbl_DailyOperation_WarningAD" runat="server" /></h4>
                    <asp:Label ID="lbl_DailyOperation_WarningDetails" runat="server" />
                    <br />
                    <a href="Alerts_Log.aspx" target="_blank">
                        <asp:Label ID="lbl_DailyOperation_AlertPage" runat="server" /></a>
                    <asp:Label ID="lbl_DailyOperation_AlertChangeStatus" runat="server" />
                </div>

                <div class="table">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="searchGrid">
                                <span>
                                    <asp:Label ID="lbl_DailyOperation_AlertSearch" runat="server" /></span>
                                <asp:TextBox ID="searchAlert" runat="server" Width="25%" />
                                <asp:LinkButton ID="btSearchAlert" runat="server" CssClass="btn btn-default" Enabled="true" OnClick="btSearchAlert_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                            </div>
                            <asp:GridView ID="GridView_Alerts" runat="server" Width="100%"
                                CssClass="table table-bordered data-table"
                                AllowPaging="True"
                                EmptyDataText="No hay informacion"
                                OnPageIndexChanging="GridView_Alerts_PageIndexChanging"
                                ShowHeaderWhenEmpty="true"
                                PageSize="15">
                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="../Documentation/Daily_Operation.aspx"></iframe>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <script src="/js/chosen.jquery.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/init.js" type="text/javascript" charset="utf-8"></script>
</asp:Content>

