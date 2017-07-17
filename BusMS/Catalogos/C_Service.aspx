<%@ Page Title="Services" Language="C#" AutoEventWireup="true" CodeBehind="C_Service.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem._01Catalogos.C_Service" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link href="/css/select2.min.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
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
            <a href="./C_Service.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_C_Service_Page_Title" runat="server"></asp:Label></a>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_C_Service_Widget_Title1" runat="server"></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_Service_Service_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:TextBox ID="tbName" onkeyup="this.value=this.value.toUpperCase()" runat="server" 
                                            Width="100%" CssClass="span2 m-wrap" placeholder="Enter service name" TabIndex="1" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_Service_tbName"
                                            runat="server"
                                            ControlToValidate="tbName"
                                            ErrorMessage='Name service is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_Service_Minimum_Distance" runat="server"></asp:Label>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:TextBox ID="tbMinDistance" runat="server" Width="100%" CssClass="span2 m-wrap"
                                             placeholder="Enter a minimum distance" TabIndex="2" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_Service_tbMinDistance"
                                            runat="server"
                                            ControlToValidate="tbMinDistance"
                                            ErrorMessage='Minimum Distance is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_C_Service_Maximum_Distance" runat="server"></asp:Label>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:TextBox ID="tbMaxDistance" runat="server" Width="100%" CssClass="span2 m-wrap"
                                             placeholder="Enter a maximum distance" TabIndex="3" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_Service_tbMaxDistance"
                                            runat="server"
                                            ControlToValidate="tbMaxDistance"
                                            ErrorMessage='Maximum Distance is required'
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
                                <asp:Button ID="btAdd" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click" />
                                <asp:Button ID="btReset" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click" />
                                <asp:Button ID="btSave" CssClass="btn btn-info" runat="server" OnClick="btSave_Click" />
                                <asp:Button ID="btDelete" CausesValidation="False" CssClass="btn btn-danger" runat="server" OnClick="btDelete_Click" UseSubmitBehavior="false" />

                            </div>
                            <div id="dialog" style="display: none">
                                <asp:Label ID="lbl_Service_Confirmation" runat="server" />
                            </div>
                            <asp:ValidationSummary
                                ID="val_Service_Summary"
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
                            <asp:Label ID="lbl_C_Service_Widget_Title2" runat="server"></asp:Label></h5>
                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div style="float: left">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_Search" runat="server"></asp:Label>
                                    </span>
                                    <asp:TextBox ID="tbSearchCH1" runat="server" PlaceHolder="Ingresa un Servicio" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />

                            </div>
                        </div>
                        <asp:GridView ID="GridView_Services" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" OnSelectedIndexChanged="GridView_Services_SelectedIndexChanged" AllowSorting="True"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true" PageSize="15"
                            OnSorting="GridView_Services_Sorting" OnPageIndexChanging="GridView_Services_PageIndexChanging">
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
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Service.aspx"></iframe>
    </div>


</asp:Content>


