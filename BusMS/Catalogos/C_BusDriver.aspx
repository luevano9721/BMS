<%@ Page Language="C#" Title="Bus-Driver" AutoEventWireup="true" CodeBehind="C_BusDriver.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem._01Catalogos.C_BusDriver" %>

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
            $("[id$=cbVendorAdm]").chosen();
            $("[id$=cbShift]").chosen();
            $("[id$=cbDriverId]").chosen();
            $("[id$=cbBusId]").chosen();
            $("[id$=cbVendor]").chosen();
            $("[id$=cbShiftID]").chosen();

        });

        function ShowInfoToEdit() {
            $('#collapseGOne').collapse('show');
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

        function GotoDownloadPage() {
            window.location = "ExportToExcel.aspx?btn=excel";
            window.setTimeout(function () {
                window.location.href = "C_BusDriver.aspx";
            }, 4000);
        }
    </script>
    <style>
        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
            height: 30px;
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
                <asp:LinkButton runat="server" OnClick="btHelp_Click"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./C_BusDriver.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_C_BusDriver_Page_Title" runat="server"></asp:Label></a>
        </div>
    </div>
     <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_C_BusDriver_Widget_Title1" runat="server"></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content" style="overflow:auto;">

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_BusDriver_BusDriver_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:TextBox ID="tbBusDriverID" Enabled="false" Width="100%" runat="server" CssClass="span2 m-wrap" placeholder="" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_BusDriver_Vendor" runat="server"></asp:Label>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:DropDownList runat="server" Enabled="false" ID="cbVendor" Width="100%" 
                                            AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="3"
                                            AutoPostBack="true" OnSelectedIndexChanged="cbVendor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_BusDriver_cbVendorR"
                                            runat="server"
                                            ControlToValidate="cbVendor"
                                            ErrorMessage='Please select a vendor'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_BusDriver_Shift_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" ID="cbShiftID" Width="100%" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_BusDriver_cbShiftIDR"
                                            runat="server"
                                            ControlToValidate="cbShiftID"
                                            ErrorMessage='Select the Shift'
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
                                        <asp:Label ID="lbl_C_BusDriver_Bus_ID" runat="server"></asp:Label>

                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:DropDownList runat="server" ID="cbBusId" Width="100%" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_BusDriver_cbBusIdR"
                                            runat="server"
                                            ControlToValidate="cbBusId"
                                            ErrorMessage='Please select a Bus'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_BusDriver_Driver" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" ID="cbDriverId" Width="100%" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_BusDriver_cbDriverIdR"
                                            runat="server"
                                            ControlToValidate="cbDriverId"
                                            ErrorMessage='Please select a Driver'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_BusDriver_Active" runat="server"></asp:Label>
                                    </div>

                                    <div class="span2 m-wrap">
                                        <asp:CheckBox runat="server" ID="cbActive" Width="100%" class="span2 m-wrap" TabIndex="3"></asp:CheckBox>

                                    </div>

                                </div>
                            </div>
                              <div style="height:50px;"></div>
                                
                             <div class="form-actions">
                            <asp:Button ID="btAdd" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                            <asp:Button ID="btReset" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                            <asp:Button ID="btSave" CssClass="btn btn-info" runat="server" OnClick="btSave_Click" />
                        </div>
 
                        <asp:ValidationSummary
                            ID="val_BusDriver_Summary"
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
        </div>


        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">

                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>
                            <asp:Label ID="lbl_C_BusDriver_Widget_Title2" runat="server"></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div class="pull-left" style="width: 30%;" >
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_Search" runat="server"></asp:Label></span>
                                    <asp:TextBox ID="tbSearchCH1" style="width: 65%;"  runat="server" PlaceHolder="Ingresa Camión o Conductor " />
                                    <asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click">
                                    <span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                            </div>
                            <div style="float: right; padding: 0px 5px;">
                                <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" CssClass="span3 m-wrap" TabIndex="1" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="cbVendorAdm_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div style="float: right; vertical-align: central;">

                                <h5>
                                    <asp:Label ID="lbl_C_BusDriver_Vendor_Grid" runat="server"></asp:Label></h5>
                            </div>
                            <div style="float: right; padding: 0px 5px;">
                                <asp:DropDownList runat="server" ID="cbShift" AppendDataBoundItems="true" CssClass="span3 m-wrap" TabIndex="2" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="cbShift_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div style="float: right; vertical-align: central;">

                                <h5>
                                    <asp:Label ID="lbl_C_BusDriver_Shift" runat="server"></asp:Label></h5>
                            </div>



                        </div>
                        <asp:GridView ID="GridView_Drivers" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" OnSelectedIndexChanged="GridView_Drivers_SelectedIndexChanged" AllowSorting="True" OnDataBound="GridView_Drivers_DataBound"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true" PageSize="15"
                            OnSorting="GridView_Drivers_Sorting" OnPageIndexChanging="GridView_Drivers_PageIndexChanging" OnRowDataBound="GridView_Drivers_RowDataBound">
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
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/BusDriver.aspx"></iframe>
    </div>
    <div id="dialog" style="display: none">
        <asp:Label ID="lbl_BusDriver_Confirmation" runat="server" />
    </div>
        <script src="/js/chosen.jquery.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/init.js" type="text/javascript" charset="utf-8"></script>
</asp:Content>

