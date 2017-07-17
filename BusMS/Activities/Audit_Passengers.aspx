<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Audit_Passengers.aspx.cs" Inherits="BusManagementSystem.Activities.Audit_Passengers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/matrix.form_common.js"></script>
    <script src="/js/select2.min.js"></script>
    <style>
      

       .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
            height: 30px;
        }
    </style>
    <script type="text/JavaScript">
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
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./Audit_Passengers.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_Audit_breadcrumb" runat="server" /></a>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-info-sign"></i></span>
                    <h5>
                        <asp:Label ID="lbl_Audit_title" runat="server" /></h5>
                </div>
                <div class="widget-content">
                    <div class="controls controls-row">
                        <div class="span12">
                            <div class="span1 m-wrap">
                                <strong>
                                    <asp:Label ID="lbl_Audit_Vendor" runat="server" /></strong>
                            </div>
                            <div class="span2 m-wrap">
                                <asp:DropDownList runat="server" ID="ddl_VendorAdm" AppendDataBoundItems="true" TabIndex="1" Width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="span1 m-wrap">
                                <strong>
                                    <asp:Label ID="lbl_Audit_Shift" runat="server" /></strong>
                            </div>
                            <div class="span2 m-wrap">
                                <asp:DropDownList runat="server" ID="ddl_ShiftType" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddl_ShiftType_SelectedIndexChanged">
                                    <asp:ListItem Value="NONE" Text="SELECCIONAR" />
                                    <asp:ListItem Value="0" Text="ENTRY" />
                                    <asp:ListItem Value="1" Text="EXIT" />
                                </asp:DropDownList>
                            </div>
                            <div class="span1 m-wrap">
                                <strong>
                                    <asp:Label ID="lbl_Audit_ShiftName" runat="server" /></strong>
                            </div>
                            <div class="span2 m-wrap">
                                <asp:DropDownList runat="server" ID="ddl_Shift" AppendDataBoundItems="true" TabIndex="3" Width="100%">
                                </asp:DropDownList>
                            </div>

                        </div>

                    </div>
                    <div class="controls controls-row">

                        <div class="span12">
                            <div id="additionalInfo" style="background: #f2fcdf;" class="span6 m-wrap">

                                <span style="font-weight: bold; padding: 30px;">
                                    <asp:Label ID="lbl_Audit_VendorInfo" runat="server" /></span>
                                <asp:Label ID="lbVendorInfo" runat="server" />
                                <span style="font-weight: bold; padding: 30px;">
                                    <asp:Label ID="lbl_Audit_ShiftInfo" runat="server" /></span>
                                <asp:Label ID="lbShiftInfo" runat="server" />
                            </div>
                            <div class="span2 m-wrap">
                                <asp:Button ID="bt_Load_Data" runat="server" CssClass="btn btn-success" Enabled="false" OnClick="bt_Load_Data_Click" />
                            </div>
                            <div class="span4 m-wrap">
                                <asp:Button ID="bt_Switch" runat="server" OnClick="bt_Switch_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>
                                <asp:Label ID="lbl_Audit_AuditTitle" runat="server" /></h5>
                        </div>

                        <div class="widget-content">

                            <div class="searchGrid">
                                 <div class="pull-left" style="width: 50%;">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_Audit_SearchID" runat="server" /></span>
                                    <asp:TextBox ID="tbSearchCH1" style="width: 50%;" runat="server"  PlaceHolder="Ingresa Camión, Conductor o Ruta"/>
                                    <asp:LinkButton ID="btSearchCH1" CausesValidation="false" runat="server" OnClick="btSearchCH1_Click" 
                                        CssClass="btn btn-default" Enabled="false">
                                                <span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                                     </div>
                                <div style="float: right">
                                    <asp:Button ID="btExcel" CssClass="btn btn-success" Text="Export to Excel" runat="server" OnClick="btExcel_Click" Enabled="false" />
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>


                                    <asp:GridView ID="dtg_Audit_Passengers" runat="server" OnRowEditing="dtg_Audit_Passengers_RowEditing"
                                        OnRowCancelingEdit="dtg_Audit_Passengers_RowCancelingEdit" OnRowUpdating="dtg_Audit_Passengers_RowUpdating"
                                        AllowPaging="True" AllowSorting="True" CssClass="table table-bordered data-table"
                                        OnPageIndexChanging="dtg_Audit_Passengers_PageIndexChanging" OnSelectedIndexChanged="dtg_Audit_Passengers_SelectedIndexChanged"
                                        OnSorting="dtg_Audit_Passengers_Sorting" AutoGenerateColumns="False"
                                        EmptyDataText="No records Found" ShowHeaderWhenEmpty="true">
                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bus/Driver" SortExpression="Bus_Driver_ID" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Bus_Driver_ID" runat="server" Text='<%# Bind("Bus_Driver_ID") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Bus_ID" SortExpression="Bus_ID" HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Bus_ID" runat="server" Text='<%# Bind("Bus_ID") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Driver" SortExpression="Driver" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Driver" runat="server" Text='<%# Bind("Driver") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Route" SortExpression="Route" HeaderStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Route" runat="server" Text='<%# Bind("Route") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Start time" SortExpression="Point_Time" HeaderStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Start_Time" runat="server" Text='<%# Bind("Point_Time") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Passengers" SortExpression="Psg_Init">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Passengers" runat="server" Text='<%# Bind("Psg_Init") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Audit" SortExpression="psg_audit">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Audit" runat="server" MaxLength="3" onkeypress="return onlyNumbers(event)" Text='<%# Bind("psg_audit") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator9"
                                                        runat="server"
                                                        ControlToValidate="txt_Audit"
                                                        ErrorMessage='Passengers  quantity is required'
                                                        EnableClientScript="true"
                                                        SetFocusOnError="true"
                                                        ForeColor="Red"
                                                        CssClass="span1 m-wrap"
                                                        Text="*"
                                                        ValidationGroup="editModeCP3">
                                                    </asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Audit" runat="server" Text='<%# Bind("psg_audit") %>'></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>

                                            <asp:CommandField ShowEditButton="True" ButtonType="Image" ValidationGroup="editModeCP3" EditImageUrl="~/images/edit-bus1.png" UpdateImageUrl="~/images/save1.png" CancelImageUrl="~/images/cancel-edit1.png" />

                                            <asp:TemplateField HeaderText="Trip_Hrd_ID" SortExpression="Trip_Hrd_ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Trip_Hrd_ID" runat="server" Text='<%# Bind("Trip_Hrd_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sequence" SortExpression="Sequence" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Sequence" runat="server" Text='<%# Bind("Sequence") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dtg_Audit_Passengers" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="../Documentation/AuditPassengers.aspx"></iframe>
    </div>

</asp:Content>

