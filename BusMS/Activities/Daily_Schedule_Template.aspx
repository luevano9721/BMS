<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Daily_Schedule_Template.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.Activities.Daily_Schedule_Template" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/chosen.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/docsupport/prism.css">
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />

    <script>
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker2);
            bindPicker2();
        });
        function bindPicker2() {
            $("[id$=cbRoute]").chosen();
            $("[id$=cbBusDriver]").chosen();
            $("[id*=cbRoute1]").chosen();
        }

        function validateHhMm(inputField) {
            var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField.value);

            if (isValid) {
                inputField.style.backgroundColor = '#bfa';
            } else {
                inputField.style.backgroundColor = '#fba';
            }

            return isValid;
        }

        $(function () {
            InitializeDeleteConfirmation();
        });
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
        function InitializeDeleteConfirmation() {
            $('#dialog-confirm').dialog({
                autoOpen: false,
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Delete": function () {
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
        function deleteItem(uniqueID) {
            var dialogTitle = 'Esta seguro de eliminar este elemento?';
            $("#dialog-confirm").html();

            $("#dialog-confirm").dialog({
                title: dialogTitle,
                resizable: false,
                height: 200,
                modal: true,
                buttons: {
                    "Eliminar": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    "Cancelar": function () { $(this).dialog("close"); }
                }
            });

            $('#dialog-confirm').dialog('open');
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

            .searchGrid span {
                padding-right: 5px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton ID="btHelp" runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton></span>
            <a href="./Daily_Schedule_Template.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_DailySchedule_Title" runat="server" /></a>
        </div>

    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
            </div>
            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-info-sign"></i></span>
                    <h5>
                        <asp:Label ID="lbl_DailySchedule_Available" runat="server" /></h5>
                </div>

                <div class="widget-content">
                    <div class="controls controls-row">
                        <div class="span12">
                            <div class="span1 m-wrap">
                                <strong>
                                    <asp:Label ID="lbl_DailySchedule_Vendor" runat="server" />
                                </strong>
                            </div>
                            <div class="span3 m-wrap">
                                <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" TabIndex="1" Width="100%" CssClass="chosen-select">
                                </asp:DropDownList>
                            </div>
                            <div class="span1 m-wrap">
                                <strong>
                                    <asp:Label ID="lbl_DailySchedule_Shift" runat="server" />
                                </strong>
                            </div>
                            <div class="span3 m-wrap">
                                <asp:DropDownList runat="server" ID="cbShiftType" AppendDataBoundItems="true" TabIndex="2" OnSelectedIndexChanged="cbShiftType_SelectedIndexChanged" AutoPostBack="true" CssClass="chosen-select">
                                    <asp:ListItem Value="NONE" Text="Seleccionar" />
                                    <asp:ListItem Value="0" Text="Entrada" />
                                    <asp:ListItem Value="1" Text="Salida" />
                                </asp:DropDownList>
                            </div>
                            <div class="span1 m-wrap">
                                <strong>
                                    <asp:Label ID="lbl_DailySchedule_ShiftName" runat="server" /></strong>
                            </div>
                            <div class="span3 m-wrap">
                                <asp:DropDownList runat="server" ID="cbShift" AppendDataBoundItems="true" TabIndex="3" CssClass="chosen-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: right; margin: 10px;">
                            <asp:Button ID="btLoadData" CausesValidation="False" OnClick="loadData" CssClass="btn btn-success" runat="server" Enabled="false" />
                            <asp:Button ID="btChange" CausesValidation="False" CssClass="btn btn-primary" runat="server" UseSubmitBehavior="false" OnClick="changeShift" Enabled="false" />
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
                            <asp:Label ID="lbl_DailySchedule_Widget" runat="server" />
                        </h5>

                    </div>

                    <div class="widget-content">
                        <div class="searchGrid">
                            <div class="pull-left" style="width: 50%;">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_Search" runat="server" Text="" /></span>
                                    <asp:TextBox ID="tbSearchCH1"  style="width: 50%;" runat="server" PlaceHolder="Ingresa Camión, Conductor o Ruta" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click1"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                            </div>
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="searchGrid">
                                    <asp:Label ID="lbl_DST_Camion" runat="server"></asp:Label>
                                    <asp:DropDownList runat="server" ID="cbBusDriver" CssClass="chosen-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="val_DST_Camion"
                                        runat="server"
                                        ControlToValidate="cbBusDriver"
                                        ErrorMessage='Bus-Driver is required'
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*"
                                        ValidationGroup="addModeCT1">
                                    </asp:RequiredFieldValidator>
                                    <asp:Label ID="lbl_DST_Recorrido" runat="server"></asp:Label>
                                    <asp:DropDownList ID="cbRoute" runat="server" CssClass="chosen-select">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="val_DST_Recorrido"
                                        runat="server"
                                        ControlToValidate="cbRoute"
                                        ErrorMessage='Route is required'
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*"
                                        ValidationGroup="addModeCT1">
                                    </asp:RequiredFieldValidator>
                                    <asp:Label ID="lbl_DST_CheckpointTim" runat="server"></asp:Label>
                                    <asp:TextBox ID="tbCheckPoint" runat="server" Width="10%" placeholder="00:00" onkeypress="return onlyNumbersWidthColon(event)" onchange="validateHhMm(this);" MaxLength="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="val_DST_Checkpoint"
                                        runat="server"
                                        ControlToValidate="tbCheckPoint"
                                        ErrorMessage='Checkpoint time is required'
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*"
                                        ValidationGroup="addModeCT1">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="val_DST_Formato" runat="server" ErrorMessage="Format time 00:00"
                                        ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbCheckPoint" Text="*" ValidationGroup="addModeCT1">
                                    </asp:RegularExpressionValidator>
                                    <asp:Button ID="btAddToGrid" runat="server" ValidationGroup="addModeCT1"
                                        OnClick="btAddTripToCheckPoint1_Click" CssClass="btn btn-success" />
                                </div>
                                <asp:GridView ID="GridView_Template" runat="server"
                                    AutoGenerateColumns="false" CssClass="table table-bordered data-table"
                                    AllowPaging="true" AllowSorting="true"
                                    OnRowEditing="editTemplate" 
                                    OnRowUpdating="updateTemplate" OnRowCancelingEdit="CancelEdit"
                                    PageSize="15" OnRowDeleting="GridView_Template_RowDeleting"
                                    OnSorting="GridView_Template_Sorting" OnPageIndexChanging="GridView_Template_PageIndexChanging"
                                    EmptyDataText="No records Found" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DST ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server"
                                                    Text='<%# Eval("dst_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bus-Driver" SortExpression="Bus_Driver_ID">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="Bus_Driver_ID_text" runat="server" Text="Bus_Driver" CommandName="Sort" CommandArgument="Bus_Driver_ID"></asp:LinkButton>
                                                <br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbBusDriver" runat="server"
                                                    Text='<%# Eval("Bus_Driver_ID")%>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bus ID" SortExpression="Bus_ID">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="Bus_ID_text" runat="server" Text="Bus_ID" CommandName="Sort" CommandArgument="Bus_ID"></asp:LinkButton>
                                                <br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbBus" runat="server" Text='<%# Eval("Bus_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Driver" SortExpression="Driver">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="Driver_text" runat="server" Text="Driver" CommandName="Sort" CommandArgument="Driver"></asp:LinkButton>
                                                <br />
                                                <asp:Label ID="lbDriverH" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbDriverName" runat="server"
                                                    Text='<%# Eval("Driver")%>'></asp:Label>
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
                                                <asp:DropDownList ID="cbRoute1" runat="server" CssClass="chosen-select">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Checkpoint Time" ControlStyle-Width="80%" SortExpression="Check_Point_Time">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="Check_Point_Time_text" runat="server" Text=" Checkpoint Time" CommandName="Sort" CommandArgument="Check_Point_Time"></asp:LinkButton>
                                                <br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbCheckpoint" runat="server"
                                                    Text='<%# Eval("Check_Point_Time")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="tbCheckPoint1" runat="server" placeholder="00:00" onkeypress="return onlyNumbersWidthColon(event)" onchange="validateHhMm(this);" MaxLength="5"
                                                    Text='<%# Eval("Check_Point_Time")%>'></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator6"
                                                    runat="server"
                                                    ControlToValidate="tbCheckPoint1"
                                                    ErrorMessage='Checkpoint Time is required'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*"
                                                    ValidationGroup="editModeCT1">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RF6" runat="server" ErrorMessage="Format time 00:00"
                                                    ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbCheckPoint1" Text="*" ValidationGroup="editModeCT1">
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" EditText="Editar" UpdateText="Actualizar" CancelText="Cancelar" ControlStyle-CssClass="btn btn-primary" ValidationGroup="editModeCT1" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="IBtnDelete" runat="server"
                                                    CommandArgument='<%# Eval("DST_ID") %>'
                                                    OnClientClick="javascript:return deleteItem(this.name);"
                                                    CssClass="btn btn-danger" AlternateText="Eliminar"
                                                    CommandName="Delete" ImageUrl="~/images/delete.png" CausesValidation="false" border="0" Width="50" Height="21" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView_Template" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div id="dialog" style="display: none">
                            <asp:Label ID="lbl_DST_DeleteDialog" runat="server"></asp:Label>
                        </div>
                        <asp:ValidationSummary
                            ID="ValidationSummary1"
                            runat="server"
                            HeaderText="Following error occurs....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            BackColor="Snow"
                            ForeColor="Red"
                            Font-Italic="true"
                            ValidationGroup="addModeCT1" />

                        <asp:ValidationSummary
                            ID="ValidationSummary2"
                            runat="server"
                            HeaderText="Following error occurs....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            BackColor="Snow"
                            ForeColor="Red"
                            Font-Italic="true"
                            ValidationGroup="editModeCT1" />

                    </div>

                </div>
            </div>
        </div>

    </div>

    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Daily_Schedule_Template.aspx"></iframe>
    </div>
    <div id="dialog-confirm" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_DST_ConfirmDialog" runat="server"></asp:Label>

        </p>
    </div>

    <script src="/js/chosen.jquery.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/init.js" type="text/javascript" charset="utf-8"></script>
</asp:Content>

