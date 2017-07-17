<%@ Page Title="Fares" Language="C#" AutoEventWireup="true" CodeBehind="C_Fare.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem._01Catalogos.C_Fare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
   <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" />
    <script src="/js/select2.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=btDelete]").removeAttr("onclick");
            $("#dialog").dialog({
                modal: true,
                autoOpen: false,
                title: "Confirmacion",
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>
            </span>
            <a href="./C_Fare.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_C_Fare_Page_Title" runat="server"></asp:Label></a>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_C_Fare_Widget_Title1" runat="server"></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span2 m-wrap">
                                        <asp:Label ID="lbl_C_Fare_Fare_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="tbFareID" runat="server" Width="100%" CssClass="span2 m-wrap" Enabled="false" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:Label ID="lbl_C_Fare_Unit_Cost" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="tbUnitCost" runat="server" Width="100%" CssClass="span2 m-wrap" placeholder="0.0" onkeypress="return onlyNumbersWidthDot(event)" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_Fare_tbUnitCost"
                                            runat="server"
                                            ControlToValidate="tbUnitCost"
                                            ErrorMessage='Service is required'
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
                                    <div class="span2 m-wrap">
                                        <asp:Label ID="lbl_C_Fare_Vendor" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" Width="100%" ID="cbVendor" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="4" OnSelectedIndexChanged="cbVendor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_Fare_cbVendor"
                                            runat="server"
                                            ControlToValidate="cbVendor"
                                            ErrorMessage='Vendor is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                         <asp:CompareValidator ID="val_Bus_VendorAll" runat="server"
                                            ErrorMessage="Seleccione un proveedor" ValueToCompare="ALL" Operator="NotEqual" ControlToValidate ="cbVendor" 
                                            CssClass="span1 m-wrap"
                                            Text="*"
                                            ForeColor="Red"
                                            SetFocusOnError="true"
                                            >
                                        </asp:CompareValidator>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:Label ID="lbl_C_Fare_Service" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList runat="server" Width="100%" ID="cbService" AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_Fare_cbService"
                                            runat="server"
                                            ControlToValidate="cbService"
                                            ErrorMessage='Service is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <div class="form-actions">
                                <asp:Button ID="btAdd" Text="Add" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                                <asp:Button ID="btReset" Text="Reset" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                                <asp:Button ID="btSave" Text="Save" CssClass="btn btn-info" runat="server" OnClick="btSave_Click" />
                                <asp:Button ID="btDelete" Text="Delete" CausesValidation="False" CssClass="btn btn-danger" runat="server" OnClick="btDelete_Click" UseSubmitBehavior="false" />
                            </div>
                            <div id="dialog" style="display: none">
                                <asp:Label ID="lbl_Fare_Delete" runat="server" />
                            </div>
                            <asp:ValidationSummary
                                ID="val_Fare_summary"
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
                            <asp:Label ID="lbl_C_Fare_Widget_Title2" runat="server"></asp:Label></h5>


                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div style="float: left">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_Search" runat="server"></asp:Label></span>
                                    <asp:TextBox ID="tbSearchCH1" runat="server" PlaceHolder="Ingresa Servicio o Costo" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />

                            </div>
                            <div style="float: right;  padding: 0px 5px;">
                                <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="1" Width="150px" OnSelectedIndexChanged="cbVendorAdm_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div style="float: right; vertical-align: central;">
                                <h5>
                                    <asp:Label ID="lbl_C_Fare_Vendor_Grid" runat="server"></asp:Label></h5>
                            </div>
                        </div>
                        <asp:GridView ID="GridView_Fares" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" OnSelectedIndexChanged="GridView_Fares_SelectedIndexChanged" AllowSorting="True"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true" PageSize="15"
                            OnSorting="GridView_Fares_Sorting" OnPageIndexChanging="GridView_Fares_PageIndexChanging">
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

    <script type="text/javascript">
        $(document).ready(function () {
            $("select.[id$=cbVendor]").select2();
            $("select.[id$=cbVendorAdm]").select2();
            $("[id$=cbService]").select2();
            
        })

    </script>

    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Fare.aspx"></iframe>
    </div>

</asp:Content>


