<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GlobalConfigurations.aspx.cs" Inherits="BusManagementSystem.Administration.GlobalConfigurations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <script src="/js/select2.min.js"></script>
    <script src="/js/bootstrap-colorpicker.js"></script>
    <script src="/js/bootstrap-datepicker.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/select2.min.js"></script>
    <script src="/js/matrix.form_common.js"></script>
    <script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="GlobalConfigurations.aspx" title="Go to Home" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_GlobalConfiguration_PageTitle" runat="server" Text=""></asp:Label></a>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span6">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class=" icon-envelope"></i></span>
                        <h5>
                            <asp:Label ID="lbl_GlobalConfiguration_WidgetTitle1" runat="server" Text=""></asp:Label></h5>
                    </div>
                    <div class="widget-content">

                        <div class="control-group">
                            <label class="control-label" style="padding-right: 10px;">
                                <asp:Label ID="lbl_GlobalConfiguration_Vendor" runat="server" Text=""></asp:Label></label>
                            <div class="controls">

                                <asp:DropDownList runat="server" ID="ddlVendor" value="Select Vendor" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                            </div>

                        </div>

                        <div class="control-group">
                            <label class="control-label" style="padding-right: 10px;">
                                <asp:Label ID="lbl_GlobalConfiguration_DailyOperation" runat="server" Text=""></asp:Label></label>
                            <div class="controls">

                                <asp:TextBox runat="server" TabIndex="3" ID="txtDailyOperation" value="Emails" class="span8" Style="resize: none" TextMode="multiline" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REV_GlobalConfigurations_DailyOperation"
                                    ControlToValidate="txtDailyOperation"
                                    Text=""
                                    ErrorMessage=""
                                    ForeColor="Red"
                                    ValidationExpression="^(\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]{2,4}\s*?,?\s*?)+$"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="control-group">
                            <label class="control-label" style="padding-right: 10px;">
                                <asp:Label ID="lbl_GlobalConfiguration_Alerts" runat="server" Text=""></asp:Label></label>
                            <div class="controls">

                                <asp:TextBox runat="server" TabIndex="3" ID="txtAlerts" value="Emails" class="span8" Style="resize: none" TextMode="multiline" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REV_GlobalConfigurations_Alerts"
                                    ControlToValidate="txtAlerts"
                                    Text=""
                                    ErrorMessage=""
                                    ForeColor="Red"
                                    ValidationExpression="^(\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]{2,4}\s*?,?\s*?)+$"
                                    runat="server" />

                            </div>

                        </div>


                        <div class="control-group">
                            <label class="control-label" style="padding-right: 10px;">
                                <asp:Label ID="lbl_GlobalConfiguration_Maintenance" runat="server" Text=""></asp:Label></label>
                            <div class="controls">

                                <asp:TextBox runat="server" TabIndex="3" ID="txtMaintenance" value="Emails" Style="resize: none" class="span8" TextMode="multiline" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REV_GlobalConfigurations_Maintenance"
                                    ControlToValidate="txtMaintenance"
                                    Text=""
                                    ErrorMessage=""
                                    ForeColor="Red"
                                    ValidationExpression="^(\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]{2,4}\s*?,?\s*?)+$"
                                    runat="server" />

                            </div>

                        </div>


                        <div class="control-group">
                            <label class="control-label" style="padding-right: 10px;">
                                <asp:Label ID="lbl_GlobalConfiguration_Catalogs" runat="server" Text="Label"></asp:Label></label>
                            <div class="controls">

                                <asp:TextBox runat="server" TabIndex="3" ID="txtCatalogs" value="Emails" class="span8" Style="resize: none" TextMode="multiline" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REV_GlobalConfigurations_Catalogs"
                                    ControlToValidate="txtCatalogs"
                                    ErrorMessage=""
                                    Text=""
                                    ForeColor="Red"
                                    ValidationExpression="^(\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]{2,4}\s*?,?\s*?)+$"
                                    runat="server" />

                            </div>

                        </div>

                    </div>
                    <div class="form-actions">
                        <asp:Button runat="server" ID="btnEmail" Text="" CssClass="btn btn-success" OnClick="btnEmail_Click" />
                    </div>
                </div>
            </div>

            <div class="span6">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-legal"></i></span>
                        <h5>
                            <asp:Label ID="lbl_GlobalConfiguration_WidgetTitle2" runat="server" Text=""></asp:Label></h5>
                    </div>
                    <div class="widget-content">

                        <div class="control-group">
                            <asp:Label ID="lbl_GlobalConfiguration_CurrentIVA" class="control-label" Style="padding-right: 10px;" runat="server" Text=""></asp:Label>

                            <asp:TextBox ID="txt_CurrentIVA" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="control-group">
                            <asp:Label ID="lbl_GlobalConfiguration_LastModification" class="control-label" Style="padding-right: 10px;" runat="server" Text=""></asp:Label>

                            <asp:TextBox ID="txt_LastModification" Enabled="false" runat="server"></asp:TextBox>
                        </div>


                        <div class="control-group">
                            <asp:Label class="control-label" ID="lbl_GlobalConfigurations_modifyIVA" Style="padding-right: 10px;" runat="server" Text=""></asp:Label>

                            <div class="controls">

                                <asp:TextBox ID="txt_IVAValue" runat="server" Visible="true" MaxLength="5" onkeypress="return onlyNumbersWidthDot(event)" placeholder="Enter new value "></asp:TextBox>

                            </div>

                        </div>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btSaveNewValue" runat="server" Text="Save new value" CssClass="btn btn-info" Visible="true" OnClick="bt_SaveIVAValue_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="../Documentation/Global_Configuration.aspx"></iframe>
    </div>

</asp:Content>
