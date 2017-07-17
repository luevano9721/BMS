<%@ Page Language="C#" Title="Buses" AutoEventWireup="true" CodeBehind="C_Bus.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem._01Catalogos.C_Bus" %>

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
            $("[id$=cbVendor]").chosen();
        });

        function openGenericDialog(element, width, height, title) {
            var dialogTitle = title;
            $(element).html();
            $(element).dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: width,
                height: height,
                appendTo: "form",
                close: function () {

                    if (element == "#blackListDialog") {
                        var tbReasons = $("[id$=tbReasons]").val();
                        var tbComments = $("[id$=tbComments]").val();
                        if (tbReasons == "" || tbComments == "") {
                            $("[id*=cbBlackListItem]").prop("checked", false);
                            $("[id$=tbReasons]").val("");
                            $("[id$=tbComments]").val("");

                        }
                    }
                    
                }
            }).parent().appendTo($("form:first"));

            $(element).dialog('open');
            return false;
        }

        function confirmItem(uniqueID) {
            var dialogTitle = 'Esta seguro de realizar esta acción?';
            $("#dialog").html();

            $("#dialog").dialog({
                title: dialogTitle,
                modal: true,
                autoOpen: false,
                width: 330,
                height: 150,
                buttons: {
                    "Confirmar": function () {
                        __doPostBack(uniqueID, '');
                        $(this).dialog("close");
                    },
                    "Cancelar": function () { $(this).dialog("close"); }
                }
            });


            $('#dialog').dialog('open');
            return false;
        }

        function ShowInfoToEdit() {
            $('#collapseGOne').collapse('show');
        }

        function closeDialog() {
            $('#blackListDialog').dialog('close');
        }
        function checkBlock(checkbox) {
            if (checkbox.checked) {
                document.getElementById("<%=cbActiveItem.ClientID%>").checked = false;
            }
        }
        function checkEnabled(checkbox) {
            if (checkbox.checked) {
                document.getElementById("<%=cbBlackListItem.ClientID%>").checked = false;
            }
        }

        function GotoDownloadPage() {
            window.location = "ExportToExcel.aspx?btn=excel";
            window.setTimeout(function () {
                window.location.href = "C_Bus.aspx";
            }, 4000);
        }
    </script>
    <style>
        .searchGrid {
            padding: 10px;
            background: #eee;
            margin-bottom: 10px;
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
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./C_Bus.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_C_Bus_Page_Title" runat="server"></asp:Label></a>
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
                                        <asp:Label ID="lbl_C_Bus_Widget_Title1" runat="server"></asp:Label></h5>
                                </a>
                            </div>
                            <div class="collapse" id="collapseGOne">
                                <div class="widget-content">

                                    <div class="controls control-group">
                                        <div class="span12">
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Bus_ID" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="busID" onkeyup="this.value=this.value.toUpperCase()" runat="server"
                                                    Width="100%" CssClass="span3 m-wrap" placeholder="Ejem. 5236" TabIndex="1" MaxLength="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_BusID"
                                                    runat="server"
                                                    ControlToValidate="busID"
                                                    ErrorMessage='Bus ID is required'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator
                                                    ID="val_Bus_BusIDInvalid"
                                                    runat="server"
                                                    ControlToValidate="busID"
                                                    ErrorMessage="Bus ID has invalid characters"
                                                    ValidationExpression="^[a-zA-Z0-9]*$"
                                                    ForeColor="Red"
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    CssClass="span1 m-wrap"
                                                    Text="*" />
                                            </div>

                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus__VIN" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="tbVIN" onkeyup="this.value=this.value.toUpperCase()"
                                                    runat="server" Width="100%" CssClass="span3 m-wrap" placeholder="Ejem. 5N1AR18WX5C781331" TabIndex="2" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_VIN"
                                                    runat="server"
                                                    ControlToValidate="tbVIN"
                                                    ErrorMessage='Enter VIN'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Vendor" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:DropDownList runat="server" ID="cbVendor" Width="100%" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="3">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_Vendor"
                                                    runat="server"
                                                    ControlToValidate="cbVendor"
                                                    ErrorMessage='Input vendor name.'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="val_Bus_VendorAll" runat="server"
                                                    ErrorMessage="Seleccione un proveedor" ValueToCompare="ALL" Operator="NotEqual" ControlToValidate="cbVendor"
                                                    CssClass="span1 m-wrap"
                                                    Text="*"
                                                    ForeColor="Red"
                                                    SetFocusOnError="true">
                                                </asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="controls control-group">
                                        <div class="span12">
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Model" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="tbModel" onkeyup="this.value=this.value.toUpperCase()" runat="server"
                                                    Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. 2000" TabIndex="4" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator3"
                                                    runat="server"
                                                    ControlToValidate="tbModel"
                                                    ErrorMessage='Enter a model'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Maker" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="tbMaker" onkeyup="this.value=this.value.toUpperCase()" runat="server"
                                                    Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. Ford" TabIndex="5" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_Maker"
                                                    runat="server"
                                                    ControlToValidate="tbMaker"
                                                    ErrorMessage='Enter a maker'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Line" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="tbLine" onkeyup="this.value=this.value.toUpperCase()" runat="server"
                                                    Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. L1" TabIndex="6" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_Line"
                                                    runat="server"
                                                    ControlToValidate="tbLine"
                                                    ErrorMessage='Enter a line'
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
                                                <asp:Label ID="lbl_C_Bus_License_plate" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="tbLicenseP" onkeyup="this.value=this.value.toUpperCase()" runat="server"
                                                    Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. ABS583GH" TabIndex="7" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_License"
                                                    runat="server"
                                                    ControlToValidate="tbLicenseP"
                                                    ErrorMessage='Enter a license plate'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_GPS_ID" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="tbGPS" onkeyup="this.value=this.value.toUpperCase()" runat="server"
                                                    Width="100%" CssClass="span2 m-wrap" placeholder="Ejem. 15852" TabIndex="8" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_GPS"
                                                    runat="server"
                                                    ControlToValidate="tbGPS"
                                                    ErrorMessage='Enter a GPS ID'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Capacity" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:TextBox ID="tbCapacity" runat="server" Width="100%" CssClass="span1 m-wrap"
                                                    placeholder="Ejem. 50" TabIndex="9" onkeypress="return onlyNumbers(event)" MaxLength="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="val_Bus_Capacity"
                                                    runat="server"
                                                    ControlToValidate="tbCapacity"
                                                    ErrorMessage='Enter capacity'
                                                    EnableClientScript="true"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red"
                                                    CssClass="span1 m-wrap"
                                                    Text="*">
                                                </asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="val_Bus_CapacityNumber" runat="server" Operator="DataTypeCheck" Type="Integer"
                                                    ControlToValidate="tbCapacity" ErrorMessage="Value must be a whole number" ForeColor="Red" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="controls control-group">
                                        <div class="span12">
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Active" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:CheckBox runat="server" ID="cbActiveItem" Onclick="checkEnabled(this)" />
                                            </div>
                                            <div class="span1 m-wrap">
                                                <asp:Label ID="lbl_C_Bus_Blacklist" runat="server"></asp:Label>
                                            </div>
                                            <div class="span3 m-wrap">
                                                <asp:CheckBox runat="server" ID="cbBlackListItem" Onclick="checkBlock(this)" OnCheckedChanged="cbBlackListItem_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-actions">
                                        <asp:Button ID="btAdd" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                                        <asp:Button ID="btReset" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                                        <asp:Button ID="btSave" CssClass="btn btn-info" runat="server" OnClick="btSave_Click" />
                                        <asp:Button ID="btDelete" CausesValidation="False" CssClass="btn btn-danger" runat="server" OnClick="btDelete_Click" UseSubmitBehavior="false" OnClientClick="javascript:return confirmItem(this.name);" />

                                    </div>

                                    <div id="dialog" style="display: none">
                                        <asp:Label ID="lbl_Bus_Dialog" runat="server" />
                                    </div>

                                    <asp:ValidationSummary
                                        ID="vs_Bus_Summary"
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
                                    <asp:Label ID="lbl_C_Bus_Widget_Title2" runat="server"></asp:Label></h5>

                            </div>
                            <div class="widget-content">
                                <div class="searchGrid">
                                    <div class="pull-right" style="padding: 0px 5px;">
                                        <asp:Button ID="btExcel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                                    </div>

                                    <div class="pull-right" style="padding: 0px 5px;">
                                        <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="1" Width="150px" OnSelectedIndexChanged="cbVendorAdm_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pull-right">
                                        <h5>
                                            <asp:Label ID="lbl_C_Bus_Vendor_Grid" runat="server"></asp:Label></h5>
                                    </div>

                                    <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                        <span>
                                            <asp:Label ID="lbl_Search" runat="server"></asp:Label></span>
                                        <asp:TextBox ID="tbSearchCH1" Style="width: 30%;" runat="server" PlaceHolder="Ingresa Camión o Placas" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" OnClick="btSearchCH1_Click" CausesValidation="false"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                    </asp:Panel>
                                </div>

                                <asp:GridView ID="GridView_buses" runat="server"
                                    CssClass="table table-bordered data-table"
                                    AllowPaging="True" OnSelectedIndexChanged="GridView_buses_SelectedIndexChanged" AllowSorting="True"
                                    EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                                    OnSorting="GridView_buses_Sorting" OnPageIndexChanging="GridView_buses_PageIndexChanging" OnDataBound="GridView_buses_DataBound" OnRowDataBound="GridView_buses_RowDataBound">
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

    <div id="blackListDialog" style="display: none">
        <div class="widget-box">
            <div class="widget-content">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div class="control-group">
                            <span>
                                <asp:Label ID="lbl_Bus_Reason" runat="server" /></span>
                        </div>
                        <div class="control-group">
                            <asp:TextBox ID="tbReasons" runat="server" TextMode="MultiLine" />
                            <asp:RequiredFieldValidator
                                ID="val_Bus_BLReason"
                                runat="server"
                                ControlToValidate="tbReasons"
                                ErrorMessage='Enter your reason'
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                ForeColor="Red"
                                CssClass="span1 m-wrap"
                                ValidationGroup="vBlacklist"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="control-group">
                            <span>
                                <asp:Label ID="lbl_Bus_Comments" runat="server" /></span>
                        </div>
                        <div class="control-group">
                            <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" />
                            <asp:RequiredFieldValidator
                                ID="val_Bus_BLComments"
                                runat="server"
                                ControlToValidate="tbComments"
                                ErrorMessage='Enter your comments'
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                ForeColor="Red"
                                CssClass="span1 m-wrap"
                                ValidationGroup="vBlacklist"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="control-group">

                            <asp:Button ID="btClose" runat="server" OnClick="btClose_Click" ValidationGroup="vBlacklist" />
                        </div>
                        <asp:ValidationSummary
                            ID="vs_Bus_BLSummary"
                            runat="server"
                            HeaderText="Following error occurs....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            BackColor="Snow"
                            ForeColor="Red"
                            Font-Italic="true"
                            ValidationGroup="vBlacklist" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btClose" />

                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Bus.aspx"></iframe>
    </div>
    <script src="/js/chosen.jquery.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/init.js" type="text/javascript" charset="utf-8"></script>
</asp:Content>
