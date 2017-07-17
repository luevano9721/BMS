<%@ Page Language="C#" Title="Drivers" AutoEventWireup="true" CodeBehind="C_Driver.aspx.cs" MasterPageFile="~/MasterPage.master" Culture="auto:en-US" UICulture="auto" Inherits="BusManagementSystem._01Catalogos.C_Driver" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link href="/css/chosen.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/docsupport/prism.css">

    <script type="text/javascript">

        $(document).ready(function () {
            $("#overlay").hide();
        });
        function DisableButton() {
            ShowProgress(true);
        } window.onbeforeunload = DisableButton;

        function ShowProgress(show) {
            if (show) {
                $("#overlay").show(100);
            }
            else {
                $("#overlay").hide(100);
            }
        }

        $(function () {
            $("[id$=tbBirthdate]").datepicker({
                maxDate: '-16Y',
                dateFormat: 'mm/dd/yy',
                changeYear: true
            }).attr('readonly', 'readonly');

            $("[id$=tbHireDate]").datepicker({
                maxDate: 0,
                dateFormat: 'mm/dd/yy',
                changeYear: true
            }).attr('readonly', 'readonly');

            $("[id$=tbLicenseExp]").datepicker({
                maxDate: '+15Y',
                dateFormat: 'mm/dd/yy',
                changeYear: true
            }).attr('readonly', 'readonly');
            $("[id$=cbVendorAdm]").chosen();
            $("[id$=cbVendor2]").chosen();
        });

        function ConfirmWindow1(uniqueID) {
            var dialogTitle = 'Certificados de ruta';
            $("#dialog1").html();

            $("#dialog1").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 330,
                height: 200,
                buttons: {
                    "Confirm": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    "Cancel": function () { $(this).dialog("close"); }
                }
            });
            $('#dialog1').dialog('open');
            return false;
        }

        function ConfirmWindow2(uniqueID) {
            var dialogTitle = 'Certificados de ruta';
            $("#dialog2").html();

            $("#dialog2").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 330,
                height: 200,
                buttons: {
                    "Confirm": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    "Cancel": function () { $(this).dialog("close"); }
                }
            });
            $('#dialog2').dialog('open');
            return false;
        }
        //function closeDialog() {
        //    $('#dialog1').dialog('close');
        //}
        function formProfilePictureChange() {

            $("[id*='form_Profilepicture']").attr("src", '/images/image-not-found.jpg');
        }

        $(function () {
            $("[id*=img_Add_certificates]").removeAttr("onclick");
            $("#dialog2").dialog({
                modal: true,
                autoOpen: false,
                title: "Confirmacion",
                width: 350,
                height: 160,
                buttons: [
                {
                    id: "Yes",
                    text: "Aceptar",
                    click: function () {
                        $("[id*=img_Add_certificates]").attr("rel", "delete");
                        $("[id*=img_Add_certificates]").click();
                    }
                },
                {
                    id: "No",
                    text: "Cancelar",
                    click: function () {
                        $(this).dialog('close');
                    }
                }
                ]
            });
            $("[id*=img_Add_certificates]").click(function () {
                if ($(this).attr("rel") != "delete") {
                    $('#dialog2').dialog('open');
                    return false;
                } else {
                    __doPostBack(this.name, '');

                }
            });
        });
        function ShowInfoToEdit() {
            $('#collapseGOne').collapse('show');
            $('#collapseGOne1').collapse('show');
        }
        function HideInfoEdit() {
            $('#collapseGOne').collapse('hide');
            $('#collapseGOne1').collapse('hide');
        }
        function closeDialog() {
            $('#dialog2').dialog('close');
        }
        function closeDialog() {
            $('#blackListDialog').dialog('close');
        }

        function showUploadDocDialog(uniqueID) {
            var dialogTitle = 'Foto de perfil';
            $("#uploadDocDiv").html();

            $("#uploadDocDiv").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 770,
                height: 500,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#uploadDocDiv').dialog('open');
            return false;
        }

        function closeDialog() {
            $('#uploadDocDiv').dialog('close');
        }
        function showUpdateProfilePicture() {
            var dialogTitle = 'Subir foto de perfil para este conductor';
            $("#uploadProfilePicture").html();

            $("#uploadProfilePicture").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 460,
                height: 550,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#uploadProfilePicture').dialog('open');
            return false;
        }

        function closeDialog() {
            $('#uploadProfilePicture').dialog('close');
        }

        function GotoDownloadPage() {
            window.location = "ExportToExcel.aspx?btn=excel";
            window.setTimeout(function () {
                window.location.href = "C_Driver.aspx";
            }, 4000);
        }

        $(function () {
            $("[id*=lnkDelete]").removeAttr("onclick");
            $("#dialog3").dialog({
                modal: true,
                autoOpen: false,
                title: "Confirmation",
                width: 350,
                height: 160,
                buttons: [
                {
                    id: "Yes",
                    text: "Yes",
                    click: function () {
                        $("[id*=lnkDelete]").attr("rel", "delete");
                        $("[id*=lnkDelete]").click();
                    }
                },
                {
                    id: "No",
                    text: "No",
                    click: function () {
                        $(this).dialog('close');
                    }
                }
                ]
            });
            $("[id*=lnkDelete]").click(function () {
                if ($(this).attr("rel") != "delete") {
                    $('#dialog3').dialog('open');
                    return false;
                } else {
                    __doPostBack(this.name, '');
                }
            });
        });

    </script>
    <script type="text/javascript">
        function openWindow(div, title, width, height) {
            var dialogTitle = title;
            $("#" + div).html();

            $("#" + div).dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: width || 250,
                height: height || 250,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#' + div).dialog('open');
            return false;
        }
        $(function () {
            $("[id$=btDelete]").removeAttr("onclick");
            $("#dialog").dialog({
                modal: true,
                autoOpen: false,
                title: "Confirmación",
                width: 350,
                height: 160,
                buttons: [
                {
                    id: "Yes",
                    text: "Aceptar",
                    click: function () {
                        $("[id$=btDelete]").attr("rel", "delete");
                        $("[id$=btDelete]").click();
                    }
                },
                {
                    id: "No",
                    text: "Cancelar",
                    click: function () {
                        $(this).dialog('close');
                    }
                }
                ]
            });
            $("[id$=btDelete]").click(function () {
                if ($(this).attr("rel") != "delete") {
                    $('#dialog').dialog('open');
                    return false;
                } else {
                    __doPostBack(this.name, '');
                }
            });
        });
        //function ShowInfoToEdit() {
        //    $('#collapseGOne').collapse('show');
        //}

        function showBlacklistDialog(uniqueID) {
            var dialogTitle = 'Confirmar alta de nuevo registro en la lista negra';
            $("#blackListDialog").html();

            $("#blackListDialog").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 330,
                height: 370,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#blackListDialog').dialog('open');
            return false;
        }

        function closeDialog() {
            $('#blackListDialog').dialog('close');
        }
        function BMSHelp(title) {

            $("#dialogHelp").html();

            $("#dialogHelp").dialog({
                autoOpen: false,
                position: 'center',
                title: title,
                width: 700,
                height: 600,
                resizable: true,
                modal: true,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $("#dialogHelp").dialog("open");
            return false;
        }
    </script>
    <style>
        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
        }

        .IMAGE {
            align-content: center;
            align-items: center;
            padding: 15px;
        }


        #overlay {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url(../images/loading.gif) 50% 50% no-repeat rgba(222,222,222, 0.36);
        }
    </style>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="overlay">
    </div>
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./C_Driver.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_C_Driver_Page_Title" runat="server"></asp:Label></a>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span9">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_C_Driver_Widget_Title1" runat="server"></asp:Label></h5>
                        </a>
                    </div>

                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1">
                                        <asp:Label ID="lbl_C_Driver_Driver_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbDriverID" Enabled="false" Width="100%" runat="server" CssClass="span2 m-wrap" placeholder="Driver ID" TabIndex="1"></asp:TextBox>
                                    </div>

                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Name" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbName" onkeyup="this.value=this.value.toUpperCase()" runat="server" Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. Juan Parez" onkeypress="return lettersOnly(event)" TabIndex="2" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_tbName"
                                            runat="server"
                                            ControlToValidate="tbName"
                                            ErrorMessage='Input driver name.'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Vendor" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="cbVendor2" Width="100%" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_cbVendor2R"
                                            runat="server"
                                            ControlToValidate="cbVendor2"
                                            ErrorMessage='Please select a vendor'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="val_Driver_VendorAll" runat="server"
                                            ErrorMessage="Seleccione un proveedor" ValueToCompare="ALL" Operator="NotEqual" ControlToValidate="cbVendor2"
                                            CssClass="span1 m-wrap"
                                            Text="*"
                                            ForeColor="Red"
                                            SetFocusOnError="true">
                                        </asp:CompareValidator>
                                    </div>



                                </div>
                            </div>

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Telephone" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbTelephone" runat="server" Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. 6562335566" onkeypress="return onlyNumbers(event)" TabIndex="3" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_tbTelephone"
                                            runat="server"
                                            ControlToValidate="tbTelephone"
                                            ErrorMessage='Please enter a phone number'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Address" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbAddress" onkeyup="this.value=this.value.toUpperCase()" runat="server" Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. Desierto del sur 1547 Col. Riveras I" TabIndex="4" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_tbAddress"
                                            runat="server"
                                            ControlToValidate="tbAddress"
                                            ErrorMessage='Enter a valid address'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Birthdate" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbBirthdate" runat="server" Width="100%" CssClass="span2 m-wrap" placeholder="mm/dd/YYY" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_tbBirthdateR"
                                            runat="server"
                                            ControlToValidate="tbBirthdate"
                                            ErrorMessage='Input driver birthdate'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Hire_Date" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbHireDate" runat="server" CssClass="span2 m-wrap" Width="100%" placeholder="mm/dd/YYY" TabIndex="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_tbHireDateR"
                                            runat="server"
                                            ControlToValidate="tbHireDate"
                                            ErrorMessage='Input driver hire date'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_License_Number" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbLicenseNo" onkeyup="this.value=this.value.toUpperCase()" Width="100%" runat="server" CssClass="span2 m-wrap" placeholder="Ejem. 151554545556546" TabIndex="7" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_tbLicenseNo"
                                            runat="server"
                                            ControlToValidate="tbLicenseNo"
                                            ErrorMessage='License number is necessary'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_License_Expiration" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbLicenseExp" runat="server" Width="100%" CssClass="span2 m-wrap" placeholder="mm/dd/YYY" TabIndex="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CDriver_tbLicenseExpR"
                                            runat="server"
                                            ControlToValidate="tbLicenseExp"
                                            ErrorMessage='You must enter the license expiration date'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Enabled" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:CheckBox ID="cbActiveItem" runat="server" OnCheckedChanged="cbActiveItem_CheckedChanged" AutoPostBack="true" TabIndex="9" />
                                    </div>
                                    <div class="span1">

                                        <asp:Label ID="lbl_C_Driver_Blacklist" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:CheckBox ID="cbBlackListItem" runat="server" OnCheckedChanged="cbBlackListItem_CheckedChanged" AutoPostBack="true" Enabled="false" TabIndex="10" />
                                    </div>

                                </div>
                            </div>

                            <div class="form-actions">
                                <asp:Button ID="btSave" CssClass="btn btn-info" runat="server" OnClientClick="javascript:return testing();" OnClick="btSave_Click" />
                                <asp:Button ID="btDelete" CausesValidation="False" CssClass="btn btn-danger" runat="server" OnClick="btDelete_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btAdd" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                                <asp:Button ID="btReset" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                                <asp:Button ID="btShowUploads" Text="Upload document" CausesValidation="False" CssClass="btn btn-warning" runat="server" OnClick="btShowUploads_Click" UseSubmitBehavior="false" Visible="false" />
                                <asp:Button ID="btCertificate" CssClass="btn btn-inverse" OnClick="btCertificate_Click" runat="server" Visible="false" />
                                <asp:Button ID="btProfilePicture" CausesValidation="False" CssClass="btn btn-warning" runat="server" OnClick="btProfilePicture_Click" UseSubmitBehavior="false" Visible="false" />

                            </div>

                            <div id="dialog" style="display: none">
                                <asp:Label ID="lbl_CDriver_Dialog1" runat="server" />
                            </div>

                            <div id="dialog3" style="display: none">
                                <asp:Label ID="lbl_CDriver_Dialog2" runat="server" />
                            </div>

                            <asp:ValidationSummary
                                ID="val_CDriver_Summary"
                                runat="server"
                                HeaderText="Following error occurs....."
                                ShowMessageBox="false"
                                DisplayMode="BulletList"
                                BackColor="Snow"
                                ForeColor="Red"
                                Font-Italic="true" />
                        </div>

                    </div>
                </div>
            </div>
            <div class="span3">

                <div class="widget-box">

                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne1" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_C_Driver_WidgetTitle_ProfilePicture" Text="" runat="server"></asp:Label>

                            </h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne1">
                        <div class="widget-content">
                            <asp:Image ID="form_Profilepicture" Width="100%" Height="100%" runat="server" />
                        </div>
                    </div>

                </div>

            </div>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>
                            <asp:Label ID="lbl_C_Driver_Widget_Title2" runat="server"></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">

                            <div style="float: right; padding: 0px 5px;">
                                <asp:Button ID="btExcel" meta:resourceKey="btExcel" CssClass="btn btn-success" CausesValidation="False" UseSubmitBehavior="false" runat="server" OnClick="btExcel_Click" />
                            </div>
                            <div style="float: right; padding: 0px 5px;">
                                <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="1" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="cbVendorAdm_SelectedIndexChanged">
                                </asp:DropDownList>



                            </div>
                            <div style="float: right; vertical-align: central;">
                                <h5>
                                    <asp:Label ID="lbl_C_Driver_Vendor_Grid" runat="server"></asp:Label></h5>
                            </div>

                            <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                <span>
                                    <asp:Label ID="lbl_Search" runat="server"></asp:Label></span>
                                <asp:TextBox ID="tbSearchCH1" Style="width: 30%;" runat="server" PlaceHolder="Ingresa Conductor, Licencia, Dirección" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" OnClick="btSearchCH1_Click" CausesValidation="false"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                            </asp:Panel>

                        </div>
                        <asp:GridView ID="GridView_Drivers" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" OnSelectedIndexChanged="GridView_Drivers_SelectedIndexChanged" OnRowDataBound="GridView_Drivers_RowDataBound" AllowSorting="True"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true" PageSize="15"
                            OnSorting="GridView_Drivers_Sorting" OnPageIndexChanging="GridView_Drivers_PageIndexChanging" OnDataBound="GridView_Drivers_DataBound">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-inverse" />
                            </Columns>
                            <Columns>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>

    </div>


    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Driver.aspx"></iframe>
    </div>

    <div id="blackListDialog" style="display: none">
        <div class="widget-box">
            <div class="widget-content">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div class="control-group">
                            <span>
                                <asp:Label ID="lbl_Bus_Reason" runat="server"></asp:Label></span>
                        </div>
                        <div class="control-group">
                            <asp:TextBox ID="tbReasons" runat="server" TextMode="MultiLine" />
                            <asp:RequiredFieldValidator
                                ID="val_CDriver_tbReasons"
                                runat="server"
                                ControlToValidate="tbReasons"
                                ErrorMessage='Enter your reason'
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                ForeColor="Red"
                                CssClass="span1 m-wrap"
                                ValidationGroup="vBlacklist"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="control-group">
                            <span>
                                <asp:Label ID="lbl_Bus_Comments" runat="server"></asp:Label></span>
                        </div>
                        <div class="control-group">
                            <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" />
                            <asp:RequiredFieldValidator
                                ID="val_CDriver_tbComments"
                                runat="server"
                                ControlToValidate="tbComments"
                                ErrorMessage='Enter your comments'
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                ForeColor="Red"
                                CssClass="span1 m-wrap"
                                ValidationGroup="vBlacklist"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="control-group">

                            <asp:Button ID="btClose" runat="server" OnClick="btClose_Click" ValidationGroup="vBlacklist" />
                        </div>
                        <asp:ValidationSummary
                            ID="vs_CDriver_Summary2"
                            runat="server"
                            HeaderText="Following error occurs....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            BackColor="Snow"
                            ForeColor="Red"
                            Font-Italic="true"
                            ValidationGroup="vBlacklist" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btClose" />

                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>

    <div id="dialog1" style="display: none">
        <asp:Label ID="lbl_CDriver_DialogC1" runat="server" />
    </div>

    <div id="dialog2" style="display: none">
        <asp:Label ID="lbl_CDriver_DialogC2" runat="server" />
    </div>

    <div id="certificateWindow" style="display: none">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">

                        <span class="icon"><i class="icon-user-md"></i></span>
                        <h5>
                            <asp:Label ID="lbl_Driver_WidgetTitle" runat="server" Text=""></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-content">
                        <div class="row-fluid">
                            <div class="control-group">
                                <asp:Label ID="lbl_Driver_Name" runat="server" Text="" CssClass="span4"></asp:Label>
                                <asp:Label ID="lbl_driver_Name_Text" runat="server" Text="" CssClass="span8"></asp:Label>

                            </div>

                            <div class="control-group">
                                <asp:Label ID="lbl_Driver_ID_text" runat="server" Text="" CssClass="span4"></asp:Label>
                                <asp:Label ID="lbl_Driver_ID_Window" runat="server" Text="" CssClass="span8"></asp:Label>
                            </div>


                        </div>

                    </div>
                </div>
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>
                            <asp:Label ID="lbl_Certificates" runat="server"></asp:Label></h5>
                    </div>
                    <div class="widget-content">
                        <div class="table">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="searchGrid">
                                        <asp:Panel runat="server" DefaultButton="btn_Search_certificates">
                                            <span>
                                                <asp:Label ID="lbl_Search1" runat="server"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txt_Search_certificates" runat="server" Width="22%" /><asp:LinkButton ID="btn_Search_certificates" CausesValidation="false" runat="server" OnClick="btn_Search_certificates_Click" CssClass="btn btn-default" Enabled="false"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton><%--OnClick="btSearchCH2_Click"--%>
                                        </asp:Panel>
                                    </div>

                                    <asp:GridView ID="dtg_Add_Certificates" AllowPaging="true" AllowSorting="true"
                                        EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                                        OnPageIndexChanging="dtg_Add_Certificates_PageIndexChanging" OnSorting="dtg_Add_Certificates_Sorting"
                                        PageSize="5" CssClass="table table-bordered data-table" AutoGenerateColumns="false" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Route" SortExpression="Route_ID" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img_Add_certificates" CommandArgument='<%# Bind("Certify_Route_ID") %>' OnClientClick="javascript:return ConfirmWindow2(this.name);" ImageAlign="Middle" ImageUrl="~/img/Delete.png" OnClick="img_Add_certificates_Click" runat="server" Width="75%" Height="15%" />
                                                </ItemTemplate>
                                                <HeaderStyle Width=".07%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Route" SortExpression="Route" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Route" runat="server" Text='<%# Bind("Route") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="15%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    </asp:GridView>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>
                            <asp:Label ID="lbl_avalible_certificates" runat="server"></asp:Label></h5>
                    </div>
                    <div class="widget-content">
                        <div class="table">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="searchGrid">
                                        <asp:Panel runat="server" DefaultButton="bt_Search">
                                            <span>
                                                <asp:Label ID="lbl_Search2" runat="server"></asp:Label>: </span>
                                            <asp:TextBox ID="txt_Search" runat="server" Width="22%" /><asp:LinkButton ID="bt_Search"
                                                CausesValidation="false" runat="server" OnClick="bt_Search_Click" CssClass="btn btn-default" Enabled="false">
                                                <span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                        </asp:Panel>
                                    </div>
                                    <asp:GridView ID="dtg_Routes" AllowPaging="true" AllowSorting="true" OnSorting="dtg_Routes_Sorting"
                                        EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                                        PageSize="5" OnPageIndexChanging="dtg_Routes_PageIndexChanging"
                                        CssClass="table table-bordered data-table" AutoGenerateColumns="false" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img_Routes" CommandArgument='<%# Bind("Route_ID") %>'
                                                        OnClientClick="javascript:return ConfirmWindow1(this.name);" ImageAlign="Middle" ImageUrl="~/img/add1.png"
                                                        OnClick="img_Routes_Click" runat="server" Width="75%" Height="15%" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width=".07%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Route" Visible="false" SortExpression="Route_ID" HeaderStyle-Width="15%">
                                                <HeaderTemplate>

                                                    <asp:LinkButton ID="Bus_Driver_ID_text" runat="server" Text="Route" CommandName="Sort" CommandArgument="Route_ID"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Routes_ID" runat="server" Text='<%# Bind("Route_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Route" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" SortExpression="Route" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Routes" runat="server" Text='<%# Bind("Route") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="15%" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="uploadDocDiv" style="display: none">
                <div class="widget-box">
                    <div class="widget-content">

                        <div class="control-group">
                            <asp:Label ID="lbl_CDriver_Document" runat="server" CssClass="control-label" />
                            <div class="controls">
                                <asp:DropDownList ID="cbDocType" runat="server">
                                    <asp:ListItem Text=" Licencia de conducir" Value="LICENCIA" />
                                    <asp:ListItem Text=" Carta de no antecedentes penales" Value="CARTA NO-ANTECEDENTES" />
                                    <asp:ListItem Text=" Curso anti-estres" Value="CURSO ANTI-STRESS" />
                                    <asp:ListItem Text=" Antidoping" Value="ANTIDOPING" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="control-group">
                            <asp:Label ID="lbl_CDriver_SelectDoc" runat="server" CssClass="control-label" />
                            <div class="controls">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="FileUpload1"
                                    runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="validateUpload" />
                                <asp:Button ID="btUpload" runat="server" CssClass="btn btn-info" Text="Subir" OnClick="btUpload_Click" ValidationGroup="validateUpload" />
                            </div>
                        </div>

                        <div style="background-color: beige;">
                            <asp:Label ID="lbl_CDriver_TypesDoc" runat="server" />
                        </div>
                        <div style="background-color: beige;">
                            <asp:Label ID="lbl_CDriver_ValidationDoc" runat="server" />
                        </div>
                        <br>
                        <div class="table">
                            <asp:GridView ID="GridView_Documents" runat="server" AutoGenerateColumns="false" EmptyDataText="No hay archivos subidos"
                                CssClass="table table-bordered data-table" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="tbFile_Name" runat="server" Text='<%# Eval("File_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category file">
                                        <ItemTemplate>
                                            <asp:Label ID="tbCategory_file" runat="server" Text='<%# Eval("Category_file")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="lnkDownload" Text="Descargar" ForeColor="White" CssClass="btn btn-inverse" CommandArgument='<%# Eval("Driver_Documents_ID") %>' runat="server" OnClick="lnkDownload_Click" ImageUrl=""></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="lnkDelete" Text="Eliminar" CssClass="btn btn-danger" ForeColor="White" CommandArgument='<%# Eval("Driver_Documents_ID") %>' runat="server" OnClick="lnkDelete_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btUpload" />

        </Triggers>
    </asp:UpdatePanel>

    <div id="uploadProfilePicture" style="display: none">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-content">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="control-group">

                                    <asp:Image ID="profilePictureDialog" CssClass="IMAGE" Width="93%" Height="100%" runat="server" />
                                </div>
                                <div class="control-group">
                                    <asp:Label ID="lbl_CDriver_SelectImage" runat="server" CssClass="control-label" />
                                    <div class="controls">
                                        <asp:FileUpload ID="FileUpload2" runat="server" />
                                        <asp:RequiredFieldValidator ErrorMessage="Selecciona una imagen" ControlToValidate="FileUpload2"
                                            runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="validateUpload2" />
                                        <div style="background-color: beige;">
                                            <asp:Label ID="lbl_CDriver_TypesPic" runat="server" />
                                        </div>
                                        <div style="background-color: beige;">
                                            <asp:Label ID="lbl_CDriver_ValidationPic" runat="server" />
                                        </div>
                                    </div>



                                </div>
                                <div class="form-actions">
                                    <asp:Button ID="btUploadProfilePicture" runat="server" CssClass="btn btn-info" Text="" OnClick="btUploadProfilePicture_Click" ValidationGroup="validateUpload2" />
                                    <asp:Button ID="btDeleteProfilePicture" runat="server" CssClass="btn btn-danger" Text="Eliminar foto" OnClick="btDeleteProfilePicture_Click" />
                                    <asp:Button ID="btChangePicture" runat="server" CssClass="btn btn-primary" Text="Cambiar foto" OnClick="btChangePicture_Click" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btUploadProfilePicture" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script src="/js/chosen.jquery.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/init.js" type="text/javascript" charset="utf-8"></script>
</asp:Content>

