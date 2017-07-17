<%@ Page Title="Revision" Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Maintenance_I.aspx.cs" Inherits="BusManagementSystem.Maintenance.Maintenance_I" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />

    <script src="/js/select2.min.js"></script>
    <script type="text/javascript">
        function CloseItem(uniqueID) {
            var dialogTitle = 'Seguro que quieres cerrar esta revision?';
            $("#dialog-close").html();

            $("#dialog-close").dialog({
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

            $('#dialog-close').dialog('open');
            return false;
        }

        function confirmItem(uniqueID) {
            var dialogTitle = 'Do you want save this information?';
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
        $(function () {
            $("[id*=btDelete]").removeAttr("onclick");
            $("#dialog").dialog({
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
                        $("[id*=btDelete]").attr("rel", "delete");
                        $("[id*=btDelete]").click();
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
            $("[id*=btDelete]").click(function () {
                if ($(this).attr("rel") != "delete") {
                    $('#dialog').dialog('open');
                    return false;
                } else {
                    __doPostBack(this.name, '');
                }
            });
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
    <style>
        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
            height: 30px;
        }
    </style>
    <script src="/js/bootstrap-colorpicker.js"></script>
    <script src="/js/bootstrap-datepicker.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/select2.min.js"></script>
    <script src="/js/matrix.form_common.js"></script>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content-header">
        <div id="breadcrumb">
             <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./Maintenance_I.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
            <asp:Label ID="lbl_MaintenanceI_PageTitle" runat="server" Text=""></asp:Label></a></div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_MaintenanceI_WidgetTitle" runat="server" Text=""></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">

                        <div class="widget-content">

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Vendor_ID" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                         <asp:DropDownList ID="DD_Vendor" runat="server" class="span2 m-wrap" Width="100%" Enabled="false" AutoPostBack="false" OnSelectedIndexChanged="DD_Vendor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Licenseplate" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="TB_Plaque" runat="server" class="span2 m-wrap" Width="90%" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_GPS" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:CheckBox ID="CB_GPS" runat="server" class="span2 m-wrap" Width="100%" Enabled="false" />
                                    </div>

                                </div>
                            </div>
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Shift" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                         <asp:DropDownList ID="DD_Shift" runat="server" Width="100%" class="span2 m-wrap" Enabled="false" OnSelectedIndexChanged="DD_Shift_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Serial" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                         <asp:TextBox ID="TB_SN" runat="server" Width="90%" class="span2 m-wrap" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Model" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="TB_MY" runat="server" Width="90%" class="span2 m-wrap" Enabled="false"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Route" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" ID="DD_Route" Width="100%" class="span2 m-wrap" Enabled="false" >
                                            </asp:DropDownList>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_License" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="TB_License" runat="server" Enabled="false" Width="90%" class="span2 m-wrap"></asp:TextBox>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_PCE" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:CheckBox ID="CB_I_As_Pce" runat="server" Width="100%" class="span2 m-wrap" Enabled="false" />
                                    </div>

                                </div>
                            </div>
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Driver" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" ID="Bus_Driver_ID" Width="100%" class="span2 m-wrap" AutoPostBack="True" OnSelectedIndexChanged="Bus_Driver_ID_SelectedIndexChanged" Enabled="False" ViewStateMode="Enabled">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Map" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:CheckBox ID="CB_Travel_Map" runat="server" Width="100%" class="span2 m-wrap" Enabled="false" />
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Lintern" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:CheckBox ID="CB_Lintern" runat="server" Width="100%" class="span2 m-wrap" Enabled="false" />
                                    </div>

                                </div>
                            </div>
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Week" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="TB_Week" runat="server" Width="90%" class="span2 m-wrap" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_Radio" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:CheckBox ID="CB_Radio" runat="server" Enabled="false" />
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_MaintenaceI_reflectors" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:CheckBox ID="CB_Sfty_reflectors" runat="server" Enabled="false" />
                                    </div>

                                </div>
                            </div>




                            <div class="form-actions">
                                <asp:Button ID="btAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClick="btAdd_Click" CausesValidation="false" />
                                <asp:Button ID="btReset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="btReset_Click" CausesValidation="false" />
                                <asp:Button ID="btEdit" runat="server" Text="Edit" OnClick="btEdit_Click" CssClass="btn btn-inverse" Enabled="false" />
                                <asp:Button ID="btSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btSave_Click" OnClientClick="javascript:return confirmItem(this.name);" CausesValidation="true" />


                                <asp:Button ID="btClose" runat="server" Text="Close revision" CssClass="btn btn-warning" OnClick="btClose_Click" OnClientClick="javascript:return CloseItem(this.name);" />

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
                    <h5><asp:Label ID="lbl_MaintenaceI_WidgetTitle2" runat="server" Text=""></asp:Label></h5>

                </div>



                <div class="widget-content ">
                    <div class="searchGrid">
                        <div class="pull-left" style="width: 45%;" >
                            <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                <span>
                                    <asp:Label ID="lbl_Search" runat="server" Text=""></asp:Label> </span>
                                <asp:TextBox ID="tbSearchCH1" style="width: 50%;"  runat="server" PlaceHolder="Ingresa Camión, Conductor o Turno" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                            </asp:Panel>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />

                        </div>
                        <div style="float: right;  padding: 0px 5px;">
                            <asp:DropDownList runat="server" ID="DD_Grid_Vendor" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="1" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="DD_Grid_Vendor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div style="float: right; vertical-align: central;">
                            <h5 style=" margin: 5px 0;">
                                <asp:Label ID="lbl_Vendor" runat="server" Text=""></asp:Label></h5>
                        </div>
                        <div style="float: right;  padding: 0px 5px;">
                            <asp:DropDownList runat="server" ID="DD_Grid_Status" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="1" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="DD_Grid_Status_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div style="float: right; vertical-align: central;">
                            <h5 style=" margin: 5px 0;">
                                <asp:Label ID="lbl_Status" runat="server" Text=""></asp:Label></h5>
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AllowPaging="true" AllowSorting="true" OnSorting="GridView1_Sorting"
                        EmptyDataText="No Records found" ShowHeaderWhenEmpty="true"
                        CssClass="table table-bordered data-table" OnPageIndexChanged="GridView1_PageIndexChanged"
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1"
                        OnRowCommand="GridView1_RowCommand" HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-inverse" HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle CssClass="btn btn-inverse"></ControlStyle>



                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:CommandField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="IbtnConfirm" runat="server"
                                        CommandArgument='<%# Eval("Legal_Revision_ID") %>'
                                        OnClick="IbtnConfirm_Click"
                                        CssClass="btn btn-mini" AlternateText="Confirm"
                                        ImageUrl="~/images/magnifier.png" CausesValidation="false" border="0" Width="30" Height="30" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle HorizontalAlign="Center" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Center" />
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>



                </div>
            </div>
        </div>
    </div>
    </div>
     <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="../Documentation/Revision.aspx"></iframe>
    </div>
    <div id="dialog-confirm" title="Do you want save this information?" style="display: none;">
        <p>
            <asp:Label ID="lbl_maitenanceI_dialogConfirm" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div id="dialog-close" title="Do you want close this revision?" style="display: none;">
        <p>
            <asp:Label ID="lbl_maitenanceI_DialogClose" runat="server" Text=""></asp:Label>
        </p>

        <div id="dialog" style="display: none" onclick="btAdd_Click">
            <asp:Label ID="lbl_maitenanceI_dialog" runat="server" Text=""></asp:Label>
        </div>
    </div>
    
</asp:Content>
