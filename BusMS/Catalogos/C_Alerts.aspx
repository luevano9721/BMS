<%@ Page Title="Alerts" Language="C#" AutoEventWireup="true" CodeBehind="C_Alerts.aspx.cs" Inherits="BusManagementSystem.Catalogos.C_Alerts" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
     <script src="/js/select2.min.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/matrix.form_common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=tbOffDate]").datepicker();
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



    </script>
     <script src="/js/bootstrap-colorpicker.js"></script>
    <script src="/js/bootstrap-datepicker.js"></script>
    <script src="/js/jquery.uniform.js"></script>

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
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./C_Alerts.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i><asp:Label ID="lbl_C_Alerts_Page_Title" runat="server"></asp:Label></a></div>
    </div>
    <div class="messagealert" id="alert_container"></div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5><asp:Label ID="lbl_C_Alerts_Widget_Title1_Alerts" runat="server" ></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Code" runat="server" ></asp:Label></div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbCode" Width="100%" runat="server" CssClass="span2 m-wrap" placeholder="Alert code" TabIndex="0" MaxLength="5"></asp:TextBox>
                                    </div>

                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Automatic_close_date" runat="server" ></asp:Label></div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbOffDate" runat="server" Width="100%" data-date-format="mm/dd/yyyy"
                                            CssClass="datepicker span2 m-wrap" placeholder="Select a date" TabIndex="1" MaxLength="3"></asp:TextBox>
                                    </div>
                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Period_to_execute" runat="server"></asp:Label></div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbPeriod" runat="server" Width="100%" CssClass="span2 m-wrap" placeholder="Max. 365 days"
                                            TabIndex="2" MaxLength="3" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CAlert_tbPeriod"
                                            runat="server"
                                            ControlToValidate="tbPeriod"
                                            ErrorMessage='Period is requiered'
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
                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Priority" runat="server"></asp:Label> </div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="cbPriority" Width="100%" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="3">
                                            <asp:ListItem Text="LOW" Value="LOW" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="MEDIUM" Value="MEDIUM"></asp:ListItem>
                                            <asp:ListItem Text="HIGH" Value="HIGH"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_CAlert_cbPriority"
                                            runat="server"
                                            ControlToValidate="cbPriority"
                                            ErrorMessage='Priority is requiered'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Alert_item" runat="server"></asp:Label> </div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="cbAlertItem" Width="100%" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_CAlert_cbAlertItem"
                                            runat="server"
                                            ControlToValidate="cbAlertItem"
                                            ErrorMessage='Alert item is requiered'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Vendor" runat="server" ></asp:Label></div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="cbVendor" Width="100%" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_CAlert_cbVendor"
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

                                </div>

                            </div>
                            <div class="controls control-group">
                                <div class="span12">

                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Description" runat="server" ></asp:Label></div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbDescription" runat="server" Width="100%" CssClass="span2 m-wrap" TextMode="MultiLine"
                                            placeholder="Enter alert description" TabIndex="6" MaxLength="300"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CAlert_tbDescription"
                                            runat="server"
                                            ControlToValidate="tbDescription"
                                            ErrorMessage='Description is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Action" runat="server" ></asp:Label></div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbAction" runat="server" Width="100%" CssClass="span2 m-wrap" TextMode="MultiLine"
                                            placeholder="Enter an action" TabIndex="7" MaxLength="300"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_CAlert_tbAction"
                                            runat="server"
                                            ControlToValidate="tbAction"
                                            ErrorMessage='Action is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1"><asp:Label ID="lbl_C_Alerts_Enabled" runat="server" ></asp:Label> </div>
                                    <div class="span1">
                                        <asp:CheckBox ID="cbEnabled" runat="server" Checked="true" />
                                    </div>

                                </div>

                            </div>

                            <div class="form-actions">
                                <asp:Button ID="btAdd"  CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                                <asp:Button ID="btReset"  CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                                <asp:Button ID="btSave"  CssClass="btn btn-info" runat="server" OnClick="btSave_Click" />

                            </div>
                            <div id="dialog" style="display: none">
                              <asp:Label ID="lbl_C_Alerts_Dialog" runat="server" />  
                            </div>
                            <asp:ValidationSummary
                                ID="vs_CAlert_Summary"
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
                        <h5><asp:Label ID="lbl_C_Alerts_Widget_Title2_Alerts" runat="server" ></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div class="pull-left" style="width: 50%;">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span><asp:Label ID="lbl_Search" runat="server"></asp:Label>  </span>
                                    <asp:TextBox ID="tbSearchCH1" style="width: 50%;" runat="server" PlaceHolder="Ingresa Código o Descripción"/><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                            </div>
                            <div style="float: right;  padding: 0px 5px;">
                                <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="8" Width="150px" OnSelectedIndexChanged="cbVendorAdm_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div style="float: right; vertical-align: central;">
                                <h5> <asp:Label ID="lbl_C_Alerts_VendorFilter" runat="server" />  </h5>
                            </div>
                        </div>
                        <asp:GridView ID="GridView_Alerts" runat="server" OnSelectedIndexChanged="GridView_Alerts_SelectedIndexChanged"
                            CssClass="table table-bordered data-table" PageSize="15"
                            AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView_Alerts_PageIndexChanging" OnSorting="GridView_Alerts_Sorting"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true">
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
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Alerts.aspx"></iframe>
    </div>
</asp:Content>
