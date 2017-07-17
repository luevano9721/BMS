<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activities_Awaiting.aspx.cs" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Inherits="BusManagementSystem.Activities.Activities_Awaiting" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />

    <script src="/js/select2.min.js"></script>

    <script type="text/JavaScript">


        function confirmItem(uniqueID) {
            var dialogTitle = 'Esta seguro de confirmar esta actividad?';
            $("#dialog-confirm").html();

            $("#dialog-confirm").dialog({
                title: dialogTitle,
                autoOpen: false,
                resizable: false,
                height: 160,
                modal: true,
                buttons: {
                    "Confirm": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    Cancel: function () { $(this).dialog("close"); }
                }
            });

            $('#dialog-confirm').dialog('open');
            return false;
        }
        function cancelItem(uniqueID) {
            var dialogTitle = 'Esta seguro de cancelar esta actividad?';
            $("#dialog-cancel").html();

            $("#dialog-cancel").dialog({
                title: dialogTitle,
                autoOpen: false,
                resizable: false,
                height: 160,
                modal: true,
                buttons: {
                    "Confirm": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    Cancel: function () { $(this).dialog("close"); }
                }
            });

            $('#dialog-cancel').dialog('open');
            return false;
        }
        function ShowInfoToEdit() {
            $('#collapseGOne').collapse('show');
        }

        function daily_operation_report() {
            var dialogTitle = 'Daily operation report';
            $("#Daily_operation_Report").html();

            $("#Daily_operation_Report").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 1290,
                height: 700,
                appendTo: "form"
            }).parent().appendTo($("form:first"));

            $('#Daily_operation_Report').dialog('open');
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
        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
            height: 30px;
        }

        .fillTotalScreen {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            background-color: rgba(0, 0, 0, 0.55);
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
    </style>
    <script src="/js/bootstrap-colorpicker.js"></script>
    <script src="/js/bootstrap-datepicker.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/select2.min.js"></script>
    <script src="/js/matrix.form_common.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UPWorking" runat="server" AssociatedUpdatePanelID="processingPanel">
        <ProgressTemplate>
            <div class="fillTotalScreen">
                <div class="centerWaitingImage">
                    <img alt="" src="../images/Processing.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./Activities_Awaiting.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_Activities_Title" runat="server" /></a>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_Activities_Filters" runat="server" /></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">

                        <div class="widget-content">
                            <div class="controls controls-row">
                                <div class="span12">

                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_Activities_From" runat="server" />
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:TextBox ID="FromDate" runat="server" Width="100%" data-date-format="mm/dd/yyyy" CssClass="datepicker span2 m-wrap" placeholder="Select a date" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_Activities_To" runat="server" />
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:TextBox ID="ToDate" runat="server" Width="100%" data-date-format="mm/dd/yyyy" CssClass="datepicker span2 m-wrap" placeholder="Select a date" TabIndex="2"></asp:TextBox>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_Activities_Transaction" runat="server" />
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:DropDownList runat="server" ID="cbType" AppendDataBoundItems="true" TabIndex="5">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_Activities_User" runat="server" />
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:DropDownList runat="server" ID="cbVendor" AppendDataBoundItems="true" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>

                                <div class="form-actions">

                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_Activities_Status" runat="server" />
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:DropDownList runat="server" ID="cbStatus" AppendDataBoundItems="true" TabIndex="6" OnSelectedIndexChanged="cbStatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="P" Selected="True" Text="Pending Activities"> Actividades pendientes</asp:ListItem>
                                            <asp:ListItem Value="0">Canceladas</asp:ListItem>
                                            <asp:ListItem Value="1">Confirmadas</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_Activities_Actions" runat="server" />
                                    </div>


                                    <div class="span3 m-wrap">
                                        <asp:Button ID="btnApply" CssClass="btn btn-success" runat="server" OnClick="btnApply_Click" />
                                    </div>


                                </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-ok"></i></span>
                        <h5>
                            <asp:Label ID="lbl_Activities_TitleWig" runat="server" /></h5>
                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div class="pull-left" style="width: 50%;">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_Search" runat="server" /></span>
                                    <asp:TextBox ID="tbSearchCH1" Style="width: 50%;" runat="server" PlaceHolder="Ingresa tipo, Modulo o Comentario" />
                                    <asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click">
                                        <span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" CssClass="btn btn-success" Text="Export to Excel" runat="server" OnClick="btnExcel_Click" />
                            </div>
                        </div>
                        <asp:UpdatePanel ID="processingPanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView_AdminApprove" runat="server"
                                    CssClass="table table-bordered data-table"
                                    AllowPaging="True" OnRowDataBound="GridView_AdminApprove_RowDataBound" AllowSorting="True"
                                    EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                                    OnSorting="GridView_AdminApprove_Sorting" OnSorted="GridView_AdminApprove_Sorted"
                                    OnRowDeleting="GridView_AdminApprove_RowDeleting"
                                    OnRowUpdating="GridView_AdminApprove_RowUpdating" OnPageIndexChanging="GridView_AdminApprove_PageIndexChanging">
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="IbtnConfirm" runat="server"
                                                    CommandArgument='<%# Eval("Activity_ID") %>'
                                                    OnClientClick="javascript:return confirmItem(this.name);"
                                                    CssClass="btn btn-info btn-mini" AlternateText="Confirm"
                                                    CommandName="Update" ImageUrl="~/images/confirm.png"
                                                    CausesValidation="false" border="0" Width="50" Height="21" />

                                                <asp:ImageButton ID="IbtnCanel" runat="server"
                                                    CommandArgument='<%# Eval("Activity_ID") %>'
                                                    OnClientClick="javascript:return cancelItem(this.name);"
                                                    CssClass="btn btn-inverse btn-mini" AlternateText="Cancel"
                                                    CommandName="Delete" ImageUrl="~/images/cancel.png" CausesValidation="false" border="0" Width="50" Height="21" />


                                                <asp:ImageButton ID="IB_View" runat="server" ImageUrl="~/images/magnifier.png"
                                                    Width="21" Height="21" CommandArgument='<%# Eval("Activity_ID") %>' OnClick="IB_View_Click1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dialog-confirm" title="Esta seguro de confirmar esta actividad?" style="display: none;">
        <p>
            <asp:Label ID="lbl_ActivitiesAwait_dialogConfirm" runat="server" />

        </p>
    </div>
    <div id="dialog-cancel" title="Esta seguro de cancelar esta actividad?" style="display: none;">
        <p>
            <asp:Label ID="lbl_ActivitiesAwait_dialogCancel" runat="server" />
        </p>
    </div>
    <div id="Daily_operation_Report" style="display: none">
        <div class="widget-box">
            <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="100%" runat="server" BackColor="#D6DBE9" InternalBorderColor="ActiveBorder" SplitterBackColor="White" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="11pt" ZoomPercent="75">
            </rsweb:ReportViewer>

        </div>
    </div>

    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="../Documentation/Activities_Awaiting.aspx"></iframe>
    </div>
</asp:Content>
