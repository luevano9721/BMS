<%@ Page Title="Shift" Language="C#" AutoEventWireup="true" CodeBehind="C_Shift.aspx.cs" MasterPageFile="~/MasterPage.Master" Inherits="BusManagementSystem.Catalogos.C_Shift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/css/timepicki.css">
    <script src="/js/timepicki.js"></script>
    <script>
        $(function () {
            $('input[id*=tbTime]').timepicki({ show_meridian: true });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=btDelete]").removeAttr("onclick");
            $("#dialog").dialog({
                modal: true,
                autoOpen: false,
                title: "Confirmation",
                width: 350,
                height: 200,
                buttons: [
                {
                    id: "Yes",
                    text: "Confirm",
                    click: function () {
                        $("[id*=btDelete]").attr("rel", "delete");
                        $("[id*=btDelete]").click();
                    }
                },
                {
                    id: "No",
                    text: "Cancel",
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
        $(function () {

            $("[id*=chkSpecial]").change(function () {
                var cbAns = ($("[id*=chkSpecial]").is(':checked')) ? 1 : 0;

                if (cbAns) {
                    showNewFareDialog();
                }
            });

            
            
        });
        function showNewFareDialog(uniqueID) {
            var dialogTitle = 'Turno especial';
            $("#newFareDialog").html();

            $("#newFareDialog").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 330,
                height: 470,
                appendTo: "form",
                close: function () {

                    var tb_ServiceName = $("[id$=tbSpecialService]").val();
                    var tb_UnicCost = $("[id$=tbUnitCost]").val();
                    if(tb_ServiceName == "" || tb_UnicCost == "")
                    {
                        $("[id*=chkSpecial]").prop("checked", false);
                        $("[id$=tbSpecialService]").val("");
                        $("[id$=tbUnitCost]").val("");
                      
                    }
                }
            }).parent().appendTo($("form:first"));

            $('#newFareDialog').dialog('open');
            return false;
        }

        function closeDialog() {
            $('#newFareDialog').dialog('close');
        };
       
        //$('#newFareDialog').on('dialogclose', function (event) {
        //    alert('closed');
        //});
        
    </script>
    <style>
        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content-header">
        <div id="breadcrumb"><a href="./C_Shift.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i><asp:Label ID="lbl_C_Shift_Page_Title" runat="server" Text=""></asp:Label></a></div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5><asp:Label ID="lbl_C_Shift_Widget_Title1" runat="server" Text=""></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1"><asp:Label ID="lbl_C_Shift_Shift_ID" runat="server" Text="Label"></asp:Label></div>
                                    <div class="span2">
                                        <asp:TextBox ID="tbShiftID" Enabled="false" onkeyup="this.value=this.value.toUpperCase()" 
                                            Width="100%" runat="server" CssClass="span2 m-wrap" placeholder="Shift ID" TabIndex="1" MaxLength="2" ></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="RV_C_Shift_ID"
                                            runat="server"
                                            ControlToValidate="tbShiftID"
                                            ErrorMessage=''
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1"><asp:Label ID="lbl_C_Shift_Shift_Name" runat="server" Text=""></asp:Label></div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbShiftName" onkeyup="this.value=this.value.toUpperCase()" Enabled="false"
                                             Width="100%" runat="server" CssClass="span2 m-wrap" placeholder="Shift Name" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="RV_C_Shift_Name"
                                            runat="server"
                                            ControlToValidate="tbShiftName"
                                            ErrorMessage=''
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="span1 m-wrap"><asp:Label ID="lbl_C_Shift_Start_Time" runat="server" Text=""></asp:Label></div>
                                    <div class="span2 m-wrap">
                                        <asp:TextBox ID="tbTime" Enabled="false" runat="server" Width="100%" CssClass="span2 m-wrap" 
                                            placeholder="00:00" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="RV_C_Shift_Time"
                                            runat="server"
                                            ControlToValidate="tbTime"
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
                                    <div class="span1"><asp:Label ID="lbl_C_Shift_Invoice_Group" runat="server" Text=""></asp:Label></div>
                                    <div class="span2">
                                        <asp:TextBox ID="tbInvoiceGroup" onkeyup="this.value=this.value.toUpperCase()" Enabled="false"
                                             Width="100%" runat="server" CssClass="span2 m-wrap" placeholder="Group Name for Invoice" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="RV_C_Shift_InvoiceGroup"
                                            runat="server"
                                            ControlToValidate="tbInvoiceGroup"
                                            ErrorMessage=''
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1 m-wrap"><asp:Label ID="lbl_C_Shift_Exit_Shift" runat="server" Text=""></asp:Label></div>
                                    <div class="span2 m-wrap">
                                        <asp:CheckBox ID="chkExit" Enabled="false" runat="server" placeholder="" TabIndex="5" />
                                    </div>
                                    <div class="span1 m-wrap"><asp:Label ID="lbl_C_Shift_Special_Shift" runat="server" Text=""></asp:Label></div>
                                    <div class="span2 m-wrap">
                                        <asp:CheckBox ID="chkSpecial" Enabled="false" runat="server" placeholder="" TabIndex="6"  />
                                    </div>


                             
                                </div>
                                </div>
                            <div class="form-actions">
                                <asp:Button ID="btAdd" Text="Add" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                                <asp:Button ID="btReset" Text="Reset" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                                <asp:Button ID="btSave" Text="Save" CssClass="btn btn-info" runat="server" OnClick="btSave_Click" />
                                <asp:Button ID="btDelete" Text="Delete" CausesValidation="False" CssClass="btn btn-danger" runat="server" OnClick="btDelete_Click" UseSubmitBehavior="false" />

                            </div>
                            <asp:ValidationSummary
                                ID="VS_C_Shift"
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
                        <h5><asp:Label ID="lbl_C_Shift_Widget_Title2" runat="server" Text=""></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                <span><asp:Label ID="lbl_Search" runat="server" Text=""></asp:Label> </span>
                                <asp:TextBox ID="tbSearch" style="width: 40%;"  runat="server" PlaceHolder="Ingresa Turno, Grupo de Facturación u Hora de Inicio"/><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" OnClick="btSearchCH1_Click1" CausesValidation="false"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                <div style="float: right;">
                                    <asp:Button ID="btExcel" Text="EXCEL" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                                </div>
                            </asp:Panel>

                        </div>
                        <asp:GridView ID="dtgShift" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" OnSelectedIndexChanged="dtgShift_SelectedIndexChanged" AllowSorting="True"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                            OnSorting="dtgShift_Sorting" OnPageIndexChanging="dtgShift_PageIndexChanging">
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

    <div id="dialog" style="display: none" align="center">
        <asp:Label ID="lbl_C_Shift_Dialog" runat="server" Text=""></asp:Label>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div id="newFareDialog" style="display: none">
        
        <div class="widget-box">

            <div class="widget-content">

                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div class="controls controls-row">
                            <span><asp:Label ID="lbl_C_Shift_ServiceCost" runat="server" Text=""></asp:Label></span>
                            
                        </div>
                        <div class="controls controls-row">
                            <div class="span3 m-wrap">
                                <asp:Label ID="lbl_C_Shift_ServiceGroup" runat="server" Text=""></asp:Label></div>
                            </div>
                         <div class="controls controls-row">
                            <div class="span2 m-wrap">
                                <asp:TextBox ID="tbSpecialService"  Width="100%"
                                    onkeyup="this.value=this.value.toUpperCase()"
                                    runat="server" CssClass="span2 m-wrap" placeholder="Special Service Name" TabIndex="7"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RV_C_Shift_SpecialService"
                                    runat="server"
                                    ControlToValidate="tbSpecialService"
                                    ErrorMessage=''
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    CssClass="span1 m-wrap"
                                    ValidationGroup="vFare"
                                    Text="*">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="controls controls-row">
                            <div class="span2 m-wrap">
                                <asp:Label ID="lbl_C_Shift_ServiceCost_Emergent" runat="server" Text=""></asp:Label></div>
                            </div>
                        <div class="controls controls-row">
                            <div class="span2 m-wrap">
                                <asp:TextBox ID="tbUnitCost" Width="100%" runat="server" onkeypress="return onlyNumbersWidthDot(event)" placeholder="0.00" CssClass="span1" />
                                <asp:RequiredFieldValidator
                                    ID="RV_C_Shift_unitCost"
                                    runat="server"
                                    ControlToValidate="tbUnitCost"
                                    ErrorMessage=''
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    ForeColor="Red"
                                    CssClass="span1 m-wrap"
                                    ValidationGroup="vFare"
                                    Text="*">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="controls controls-row">
                            <asp:Button ID="btSaveUnitCost" runat="server" OnClick="btSaveUnitCost_Click" Text="Save" ValidationGroup="vFare" CssClass="btn btn-info" />
                        </div>
                        <asp:ValidationSummary
                            ID="VS_C_Shift_Sunmary2"
                            runat="server"
                            HeaderText=""
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            BackColor="Snow"
                            ForeColor="Red"
                            Font-Italic="true"
                            ValidationGroup="vFare" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btSaveUnitCost" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>

        </div>

    </div>

</asp:Content>

