<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alerts_Log.aspx.cs" Inherits="BusManagementSystem.Activities.Alerts_Log" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <script src="/js/select2.min.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/matrix.form_common.js"></script>
    <script>
        function openWindow(div, title, width, height) {
            var dialogTitle = title;
            $("#" + div).html();

            $("#" + div).dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: width || 500,
                height: height || 500,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#' + div).dialog('open');
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
    <style>
        .form-1 {
            padding-bottom: 15px;
            padding-right: 50px;
        }

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
            <a href="./Alerts_Log.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_AlertInformation_page" runat="server" Text=""></asp:Label></a>
        </div>
    </div>
    <div class="messagealert" id="alert_container"></div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>
                            <asp:Label ID="lbl_AlerInformation_page_Title" runat="server" Text=""></asp:Label></h5>
                    </div>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class=" widget-content">
                        <div class="searchGrid">
                            <div class="pull-left" style="width: 40%;">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_AlertInformation_Search" runat="server" Text=""></asp:Label></span>
                                    <asp:TextBox ID="tbSearchCH1" style="width: 70%;" runat="server" PlaceHolder="Ingresa Codigo, Descripción, Camión o Conductor" />
                                    <asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" OnClick="btSearchCH1_Click">
                                            <span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>

                                </asp:Panel>
                            </div>
                            <div style="float: right;">
                                <asp:Button ID="btExcel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                            </div>
                            <div style="float: right;">
                                <div style="float: right; padding: 0px 5px;">
                                    <asp:DropDownList ID="cbFilterStatus" runat="server" OnSelectedIndexChanged="cbFilterStatus_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Abierto" Value="OPEN"></asp:ListItem>
                                        <asp:ListItem Text="Trabajando" Value="WORKING"></asp:ListItem>
                                        <asp:ListItem Text="Terminado" Value="DONE"></asp:ListItem>
                                        <asp:ListItem Text="Cerrado" Value="CLOSED"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right;">
                                    <h5 style="margin: 5px 0;">
                                        <asp:Label ID="lbl_AlertInformation_Filter" runat="server" Text=""></asp:Label></h5>
                                </div>
                                <div style="float: right; padding: 0px 5px;">
                                    <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="8" Width="150px" OnSelectedIndexChanged="cbVendorAdm_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right;">
                                    <h5 style=" margin: 5px 0;">
                                        <asp:Label ID="lbl_C_Alerts_VendorFilter" runat="server" />
                                    </h5>
                                </div>
                            </div>



                        </div>
                        <asp:GridView ID="GridView_AlertsLog" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                            OnSorted="GridView_AlertsLog_Sorted" PageSize="15"
                            OnRowDataBound="GridView_AlertsLog_RowDataBound" OnPageIndexChanging="GridView_AlertsLog_PageIndexChanged"
                            OnSorting="GridView_AlertsLog_Sorting">
                            <Columns>
                                <%--     <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-inverse" />--%>
                                <asp:TemplateField HeaderText="Alert_log_ID" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lbAlert_log_ID" runat="server"
                                            Text='<%# Eval("Alert_log_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" SortExpression="Code">

                                    <ItemTemplate>
                                        <asp:Label ID="lbCode" runat="server"
                                            Text='<%# Eval("Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description">

                                    <ItemTemplate>
                                        <asp:Label ID="lbDescription" runat="server"
                                            Text='<%# Eval("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" SortExpression="Action">

                                    <ItemTemplate>
                                        <asp:Label ID="lbAction" runat="server"
                                            Text='<%# Eval("Action")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Create Date" SortExpression="Create_Date">

                                    <ItemTemplate>
                                        <asp:Label ID="lbCreatedate" runat="server"
                                            Text='<%# Eval("Create_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bus_ID" SortExpression="Bus_ID">

                                    <ItemTemplate>
                                        <asp:Label ID="lbBusID" runat="server"
                                            Text='<%# Eval("Bus_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Driver_ID" SortExpression="Driver_ID">

                                    <ItemTemplate>
                                        <asp:Label ID="lbDriverID" runat="server"
                                            Text='<%# Eval("Driver_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority" SortExpression="Priority">

                                    <ItemTemplate>
                                        <asp:Label ID="lbPriority" runat="server"
                                            Text='<%# Eval("Priority")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Status">

                                    <ItemTemplate>
                                        <asp:Label ID="lbStatus" runat="server"
                                            Text='<%# Eval("Status")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="More Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btMoreInfo" runat="server" OnClick="btMoreInfo_Click" CommandArgument='<%# Eval("Alert_log_ID")%>'
                                            border="0">
                                            <asp:Image ID="imageMoreInfo" runat="server" ImageUrl="~/images/more_info.png" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Following">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btFollowing" runat="server" OnClick="btFollowing_Click" CommandArgument='<%# Eval("Alert_log_ID")%>'
                                            border="0">
                                            <asp:Image ID="imageComments" runat="server" ImageUrl="~/images/add_comments.png" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Take action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btTakeAction" runat="server" OnClick="btTakeAction_Click" CommandArgument='<%# Eval("Alert_log_ID")%>'
                                            border="0">
                                            <asp:Image ID="imageActions" runat="server" ImageUrl="~/images/change_status.png" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Alert">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btEndAlert" runat="server" OnClick="btEndAlert_Click" CommandArgument='<%# Eval("Alert_log_ID")%>'
                                            border="0">
                                            <asp:Image ID="imageConfirm" runat="server" ImageUrl="~/images/Confirm-alert.png" Style="border-width: 0px;" Enabled="false" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>


                    </div>
                </div>



            </div>
        </div>
    </div>

    <div id="moreInfoWindow" style="display: none">
        <div class="widget-content">
            <div class="form-1">
                <p>
                    <asp:UpdatePanel ID="upMoreInfo" runat="server">
                        <ContentTemplate>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_AlertID" runat="server" /></strong></span>
                                <asp:Label ID="lbAlertIDMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Code" runat="server" /></strong></span>
                                <asp:Label ID="lbCodeMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Description" runat="server" /></strong></span>
                                <asp:Label ID="lbDescriptionMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Action" runat="server" /></strong></span>
                                <asp:Label ID="lbActionMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_CreateDate" runat="server" /></strong></span>
                                <asp:Label ID="lbDateMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_BusID" runat="server" /></strong></span>
                                <asp:Label ID="lbBusIDMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_DriverID" runat="server" /></strong></span>
                                <asp:Label ID="lbDriverIDMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Rissed" runat="server" /></strong></span>
                                <asp:Label ID="lbRissedMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Comments" runat="server" /></strong></span>
                                <asp:Label ID="lbCommentsMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Priority" runat="server" /></strong></span>
                                <asp:Label ID="lbPriorityMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Status" runat="server" /></strong></span>
                                <asp:Label ID="lbStatusMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_Closed" runat="server" /></strong></span>
                                <asp:Label ID="lbClosedMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_LastCheck" runat="server" /></strong></span>
                                <asp:Label ID="lbLastCheckMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            <div class="control-group">
                                <span class="control-label"><strong>
                                    <asp:Label ID="lbl_AlerInformation_VendorID" runat="server" /></strong></span>
                                <asp:Label ID="lbVendorIDMI" runat="server" class="control-label"></asp:Label>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </p>
            </div>
        </div>
    </div>

    <div id="followingWindow" style="display: none">

                <div class="widget-box">
                    <div class="widget-title">
                        <h5>
                            <asp:Label ID="lblComments_Title" runat="server"></asp:Label></h5>
                    </div>
                    <div class="widget-content">


                        <asp:HiddenField ID="idFollowingSelected" runat="server"></asp:HiddenField>
                        <asp:UpdatePanel ID="upFollowing" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gdFollowing" runat="server"
                                    CssClass="table table-bordered data-table"
                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false"
                                    EmptyDataText="No records Found" ShowHeaderWhenEmpty="true" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="User" SortExpression="User">
                                            <ItemTemplate>
                                                <asp:Label ID="lbFollowUS" runat="server" Text='<%# Eval("User_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" SortExpression="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbFollowDate" runat="server" Text='<%# Eval("CDate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comment" SortExpression="Comment">
                                            <ItemTemplate>
                                                <asp:Label ID="lbFollowComment" runat="server" Text='<%# Eval("Comment")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="widget-box">
                    <div class="widget-title">
                        <h5>
                            <asp:Label ID="lblAddcomment" runat="server"></asp:Label></h5>
                    </div>
                    <div class="widget-content">

                        <div class="control-group">
                            <asp:Label ID="lbl_AlerInformation_WriteCmts" runat="server" />
                            <asp:TextBox ID="tbFollowComments" runat="server" Width="98%" TextMode="MultiLine" ValidationGroup="comment"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RV_Blacklist_txt_Comments"
                                runat="server"
                                ControlToValidate="tbFollowComments"
                                ErrorMessage=''
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                ForeColor="Red"
                                CssClass="span2 m-wrap"
                                Text="*"
                                ValidationGroup="comment">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="form-actions">
                            <div class="pull-right">
                                <asp:Button ID="btAddComment" CssClass="btn btn-success" runat="server" OnClick="btAddComment_Click" ValidationGroup="comment" />

                            </div>
                        </div>
                        <asp:ValidationSummary
                            ID="vs_Bus_Summary"
                            runat="server"
                            HeaderText="Following error occurs....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            BackColor="Snow"
                            ForeColor="Red"
                            Font-Italic="true"
                            ValidationGroup="comment" />
                    </div>
                </div>


    </div>

    <div id="takeActionWindow" style="display: none">
        <asp:HiddenField ID="idTakeActionSelected" runat="server"></asp:HiddenField>
        <div class="content">
            <div class="widget-box">
                <div class="widget-content">

                    <div class="controls controls-row">

                        <asp:Label ID="lbl_AlerInformation_ActualStatus" runat="server" />
                        <h5 style="margin: 0px 0px;">
                            <asp:Label ID="lbTakeActionStatus" runat="server"></asp:Label></h5>
                    </div>

                    <div class="controls controls-row">

                        <asp:Label ID="lbl_AlerInformation_Select" runat="server" />
                        <asp:DropDownList ID="cbAlertStatus" runat="server">
                            <asp:ListItem Value="WORKING">WORKING</asp:ListItem>
                            <asp:ListItem Value="DONE">DONE</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-actions">
                        <asp:Button ID="btChangeStatus" Text="Save" CausesValidation="False" CssClass="btn btn-info" runat="server" OnClick="btChangeStatus_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="endAlertWindow" style="display: none">
        <asp:HiddenField ID="idEndAlertWindow" runat="server"></asp:HiddenField>
        <div class="content">
            <div class="widget-box">
                <div class="widget-content">

                    <div class="controls controls-row">

                        <asp:Label ID="lbl_AlerInformation_Actual" runat="server" />
                        <h5 style="margin: 0px 0px;">
                            <asp:Label ID="lbEndAlertWindow" runat="server"></asp:Label></h5>
                    </div>
                    <div class="controls controls-row">

                        <asp:Label ID="lbl_AlerInformation_NewStatus" runat="server" />
                        <asp:DropDownList ID="cbStatusClosed" runat="server">
                            <asp:ListItem Value="CLOSED">CLOSED</asp:ListItem>
                        </asp:DropDownList>
                    </div>


                    <div class="form-actions">
                        <asp:Button ID="btClose" Text="End Alert" CausesValidation="False" CssClass="btn btn-info" runat="server" OnClick="btClose_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="../Documentation/AlertSupport.aspx"></iframe>
    </div>

</asp:Content>

