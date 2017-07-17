<%@ Page Title="Maintenance Scheduled" Language="C#" AutoEventWireup="true" CodeBehind="Maintenace_Program.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.Maintenance.Maintenace_Program" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="/js/bootstrap-colorpicker.js"></script>
    <script src="/js/bootstrap-datepicker.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/select2.min.js"></script>

     
 
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />

    <script>
        $(function () {
            $("[id*=date]").datepicker();
        });
        function ShowInfoToEdit() {
            $('#collapseGOne').collapse('show');
        }
        function confirmItem(uniqueID) {
            var dialogTitle = 'Confirm you want to save this item?';
            $("#dialog-confirm").html();

            $("#dialog-confirm").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 330,
                height: 150,
                buttons: {
                    "Confirm": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    "Cancel": function () { $(this).dialog("close"); }
                }
            });

            $('#dialog-confirm').dialog('open');
            return false;
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
    <script src="/js/bootstrap-colorpicker.js"></script>
    <script src="/js/bootstrap-datepicker.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/select2.min.js"></script>
    <script src="/js/matrix.form_common.js"></script>
    <style>
        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
            height: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
            <asp:LinkButton ID="btHelp"  OnClick="btHelp_Click" runat="server" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
                </span>
            <a href="Maintenace_Program.aspx" title="Go to Home" class="tip-bottom"><i class="icon-home"></i>
            <asp:Label ID="lbl_MaintenanceI_PageTitle" runat="server" Text=""></asp:Label></a></div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_MaintenanceI_MaintenanceProgram" runat="server" Text=""></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">

                             <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                         <asp:Label ID="lbl_MaintenaceProgram_VendorID" runat="server" Text="Label"></asp:Label>
                                        </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" Enabled="false" ID="cbVendor" Width="100%" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="cbVendor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RV_MaintenanceProgram_VendorID"
                                                runat="server"
                                                ControlToValidate="cbVendor"
                                                ErrorMessage=''
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                CssClass="span1 m-wrap"
                                                Text="*">
                                            </asp:RequiredFieldValidator>
                                              <asp:CompareValidator ID="val_Maintenance_VendorAll" runat="server"
                                            ErrorMessage="Seleccione un proveedor" ValueToCompare="ALL" Operator="NotEqual" ControlToValidate ="cbVendor" 
                                            CssClass="span1 m-wrap"
                                            Text="*"
                                            ForeColor="Red"
                                            SetFocusOnError="true"
                                            >
                                        </asp:CompareValidator>
                                        </div>
                                    <div class="span1 m-wrap">
                                         <asp:Label ID="lbl_MaintenaceProgram_BusID" runat="server" Text=""></asp:Label>
                                        </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" Enabled="false" ID="cbBus" Width="100%" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="cbBus_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RV_MaintenanceProgram_BusID"
                                                runat="server"
                                                ControlToValidate="cbBus"
                                                ErrorMessage=''
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                CssClass="span1 m-wrap"
                                                Text="*">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    <div class="span1 m-wrap">
                                         <asp:Label ID="lbl_MaintenaceProgram_Alert" runat="server" Text=""></asp:Label>
                                        </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" Enabled="false" ID="cbAlert" Width="100%" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="6">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RV_MaintenanceProgram_Alert"
                                                runat="server"
                                                ControlToValidate="cbAlert"
                                                ErrorMessage=''
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                CssClass="span1 m-wrap"
                                                Text="*">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                 </div>
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                         <asp:Label ID="lbl_MaintenaceProgram_Comments" runat="server" Text=""></asp:Label>
                                        </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="commenst" onkeyup="this.value=this.value.toUpperCase()" runat="server" Enabled="false" Width="100%" CssClass="span3 m-wrap" placeholder="Enter comments" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RV_MaintenanceProgram_Comments"
                                                runat="server"
                                                ControlToValidate="commenst"
                                                ErrorMessage=''
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                CssClass="span1 m-wrap"
                                                Text="*">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    <div class="span1 m-wrap">
                                         <asp:Label ID="lbl_MaintenaceProgram_Priority" runat="server" Text=""></asp:Label>
                                        </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" Enabled="false" ID="cbPriority" Width="100%" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="cbPriority_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">LOW</asp:ListItem>
                                                <asp:ListItem>MEDIUM </asp:ListItem>
                                                <asp:ListItem>HIGH</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RV_MaintenanceProgram_Priority"
                                                runat="server"
                                                ControlToValidate="cbPriority"
                                                ErrorMessage=''
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                CssClass="span1 m-wrap"
                                                Text="*">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    <div class="span1 m-wrap">
                                         <asp:Label ID="lbl_MaintenaceProgram_Expiration" runat="server" Text=""></asp:Label>
                                        </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="tbExpireddate" runat="server" Enabled="false" Width="100%" data-date-format="mm/dd/yyyy" CssClass="datepicker span2 m-wrap" placeholder="Select a date" TabIndex="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RV_MaintenanceProgramExpiration"
                                                runat="server"
                                                ControlToValidate="tbExpireddate"
                                                ErrorMessage=''
                                                EnableClientScript="true"
                                                SetFocusOnError="true"
                                                ForeColor="Red"
                                                CssClass="span1 m-wrap"
                                                Text="*">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                 </div>


                            <div class="form-actions">
                                <asp:Button ID="btAdd" Text="Add" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                                <asp:Button ID="btReset" Text="Reset" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                                <asp:Button ID="btSave" Text="Save" CssClass="btn btn-info" runat="server" OnClick="btSave_Click" OnClientClick="javascript:return confirmItem(this.name);" />
                                <asp:Button ID="btDelete" Text="Delete" CausesValidation="False" CssClass="btn btn-danger" runat="server" UseSubmitBehavior="false" />

                            </div>

                            <div id="dialog" style="display: none">
                                <asp:Label ID="bl_MaintenanceProgram_Dialog" runat="server" Text=""></asp:Label>
                            </div>

                            <asp:ValidationSummary
                                ID="VS_MaintenanceProgram1"
                                runat="server"
                                HeaderText=""
                                ShowMessageBox="false"
                                DisplayMode="BulletList"
                                BackColor="Snow"
                                ForeColor="Red"
                                Font-Italic="true" />
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
                        <h5><asp:Label ID="lbl_MaintenanceProgram_Widget2" runat="server" Text=""></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div style="float: left;width: 50%;">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1" > 
                                    <span>
                                        <asp:Label ID="lbl_Search" runat="server" Text=""></asp:Label></span>
                                    <asp:TextBox ID="tbSearchCH1" runat="server" PlaceHolder="Ingresa Camión o Comentarios" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                            </div>
                                                    <div style="float: right;  padding: 0px 5px;">
                            <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="1" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="cbVendorAdm_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div style="float: right; vertical-align: central;">
                            <h5><asp:Label ID="lbl_Vendor" runat="server" Text=""></asp:Label></h5>
                        </div>
                        </div>
                        <asp:GridView ID="GridView_alert" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" AllowSorting="True"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true" 
                            OnSelectedIndexChanged="GridView_alert_SelectedIndexChanged" OnPageIndexChanging="GridView_alert_PageIndexChanging" 
                            OnRowDataBound="GridView_alert_RowDataBound"
                            OnSorting="GridView_alert_Sorting" PageSize="15">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-inverse" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Maintenance_Program.aspx"></iframe>
    </div>
    <div id="dialog-confirm" title="" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_MaintenanceProgram_EmergentText" runat="server" Text=""></asp:Label>
            
        </p>
    </div>
</asp:Content>
