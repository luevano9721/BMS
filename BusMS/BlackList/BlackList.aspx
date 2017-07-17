<%@ Page Title="BlackList" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BlackList.aspx.cs" Inherits="BusManagementSystem.BlackList.BlackList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" />
    <script src="/js/select2.min.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    <script src="/js/matrix.form_common.js"></script>

    <style type="text/css">
        .auto-style2 {
            width: 6%;
        }

        .auto-style7 {
            width: 5%;
        }

        .auto-style8 {
            width: 19%;
        }

        .auto-style9 {
            width: 17%;
        }

        .auto-style10 {
            width: 18%;
        }

        .auto-style11 {
            width: 9%;
        }
    </style>
    <script type="text/javascript">

        function ShowInfoToEdit() {
            $('#collapseGOne').collapse('show');
            $('#collapseGOTwo').collapse('show');
        }

        function confirmItem(uniqueID) {
            var dialogTitle = 'Confirme que desea agrear este elemento a Lista negra?';
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
    <%--    <script src="/js/bootstrap-colorpicker.js"></script>
    <script src="/js/bootstrap-datepicker.js"></script>
    <script src="/js/jquery.uniform.js"></script>
    
    <script src="/js/matrix.form_common.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton ID="btHelp" OnClick="btHelp_Click" runat="server" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton>

            </span>
            <a href="./BlackList.aspx" title="Refresh Page" class="tip-bottom"><i class=" icon-remove-sign"></i>
                <asp:Label ID="lbl_BlackList_Page_Title" runat="server" Text=""></asp:Label></a>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span9">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_BlackList_Widget_Title1" runat="server" Text=""></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="lbl_BlackList_Type" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        <asp:Label ID="lbl_BlackList_Form_vendor" runat="server" Font-Bold="true" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_BlackList_BlackList_ID" runat="server" Font-Bold="true" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <asp:DropDownList ID="DD_Category" runat="server" Width="100%" Visible="true" Enabled="true" OnSelectedIndexChanged="DD_Category_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="Select" />
                                            <asp:ListItem Text="Driver" Value="DRIVER" />
                                            <asp:ListItem Text="Bus" Value="BUS" />
                                            <asp:ListItem Text="Bus/Driver" Value="BUS_DRIVER" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="span4 m-wrap">
                                        <asp:Button ID="btAdd" runat="server" Text="Add"
                                            CssClass="btn btn-success" Visible="false" Enabled="false"
                                            OnClick="bt_Add_Click" CausesValidation="False" />
                                        <asp:Button ID="btReset" runat="server" Text="Reset"
                                            CssClass="btn btn-primary" Visible="false" Enabled="false"
                                            OnClick="bt_Reset_Click" CausesValidation="False" />
                                        <asp:Button ID="btSave" runat="server" Text="Save"
                                            CssClass="btn btn-info" Visible="false" Enabled="false"
                                            OnClick="bt_Save_Click" OnClientClick="javascript:return confirmItem(this.name);" />

                                    </div>
                                    <%--OnClientClick="javascript:return confirmItem(this.name);"--%>
                                </div>
                            </div>
                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <strong>
                                            <asp:Label ID="lbl_BlackList_vendor" runat="server" Text=""></asp:Label>
                                        </strong>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList ID="DD_Vendor" runat="server" CssClass="span2 m-wrap" Width="100%" Enabled="false" OnSelectedIndexChanged="DD_Vendor_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                            <div class="controls control-group">

                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="DD_Dinamic_Label1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList ID="DD_Dinamic1" runat="server" CssClass="span2 m-wrap" Width="100%" Enabled="false" OnSelectedIndexChanged="DD_Dinamic1_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="span1 m-wrap">
                                        <asp:Label ID="DD_Dinamic_Label2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </div>
                                    <div class="span3 m-wrap">
                                        <asp:DropDownList ID="DD_Dinamic2" runat="server" Enabled="false" Visible="false" OnSelectedIndexChanged="DD_Dinamic2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                    </div>

                                </div>
                            </div>
                            <div class="controls control-group">

                                <div class="span12">
                                    <div class="span1 m-wrap">
                                        <strong>
                                            <asp:Label ID="lbl_BlackList_Reson" runat="server" Text=""></asp:Label></strong>
                                    </div>
                                    <div class="span4 m-wrap">
                                        <asp:TextBox ID="txt_Reason" runat="server" Width="100%" Height="60" Style="resize: none" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="RV_Blacklist_txt_Reason"
                                            runat="server"
                                            ControlToValidate="txt_Reason"
                                            ErrorMessage=''
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span2 m-wrap">
                                        <strong>
                                            <asp:Label ID="lbl_BlackList_Comments" runat="server" Text="Label"></asp:Label></strong>
                                    </div>
                                    <div class="span4 m-wrap">
                                        <asp:TextBox ID="txt_Comments" runat="server" Width="100%" Height="60" Style="resize: none" TextMode="MultiLine" Wrap="False"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="RV_Blacklist_txt_Comments"
                                            runat="server"
                                            ControlToValidate="txt_Comments"
                                            ErrorMessage=''
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <asp:ValidationSummary
                                ID="VS_Blacklist_msg"
                                runat="server"
                                HeaderText="aaa"
                                ShowMessageBox="false"
                                DisplayMode="BulletList"
                                BackColor="Snow"
                                ForeColor="Red"
                                Font-Italic="true" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="span3">

                <div class="widget-box">

                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOTwo" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_C_Driver_WidgetTitle_ProfilePicture" Text="" runat="server"></asp:Label>

                            </h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOTwo">
                        <div class="widget-content">
                            <asp:Image ID="form_Profilepicture" Width="100%" Height="100%" runat="server" ImageUrl="~/images/image-not-found.jpg" />
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
                            <asp:Label ID="lbl_BlackList_Widget_Title2" runat="server" Text="Label"></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div style="float: left">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_BlackList_Search" runat="server" Text="" PlaceHolder="Ingresa Camión, Conductor o Commentario"></asp:Label>
                                    </span>
                                    <asp:TextBox ID="tbSearchCH1" runat="server" PlaceHolder="Enter Bus ID , Driver or Reason" /><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False"
                                    CssClass="btn btn-success" runat="server" UseSubmitBehavior="false"
                                    OnClick="btExcel_Click" />
                            </div>
                            <div style="float: right; padding: 0px 5px;">
                                <asp:DropDownList runat="server" ID="DD_Grid_Vendor" AppendDataBoundItems="true" CssClass="span3 m-wrap" TabIndex="1" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="DD_Grid_Vendor_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div style="float: right; vertical-align: central;">
                                <h5>
                                    <asp:Label ID="lbl_BlackList_Grid_vendor" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div style="float: right; padding: 0px 5px;">
                                <asp:DropDownList runat="server" ID="DD_Category_Grid" AppendDataBoundItems="true" CssClass="span3 m-wrap" AutoPostBack="true" OnSelectedIndexChanged="DD_Category_Grid_SelectedIndexChanged" TabIndex="1" Width="150px">
                                    <asp:ListItem Text="Select" Value="Select" />
                                    <asp:ListItem Text="Driver" Value="DRIVER" />
                                    <asp:ListItem Text="Bus" Value="BUS" />
                                    <asp:ListItem Text="Bus/Driver" Value="BUS_DRIVER" />
                                </asp:DropDownList>
                            </div>
                            <div style="float: right; vertical-align: central;">
                                <h5>
                                    <asp:Label ID="lbl_BlackList_category" runat="server" Text=""></asp:Label></h5>
                            </div>

                        </div>


                        <asp:GridView ID="GridViewBlackList" runat="server" AllowSorting="true" OnSorting="GridViewBlackList_Sorting" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" HorizontalAlign="Center"
                            CssClass="table table-bordered data-table" AllowPaging="True" PageSize="15"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                            OnPageIndexChanged="GridViewBlackList_PageIndexChanged" OnPageIndexChanging="GridViewBlackList_PageIndexChanging" OnSelectedIndexChanged="GridViewBlackList_SelectedIndexChanged" OnRowDataBound="GridViewBlackList_RowDataBound">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-inverse" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle CssClass="btn btn-inverse"></ControlStyle>

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:CommandField>
                            </Columns>

                            <RowStyle HorizontalAlign="Center" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dialog-confirm" title="Seguro que desea agregar este registro?" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_BlackList_msgConfirm" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/BlackList.aspx"></iframe>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("select.[id$=DD_Vendor]").select2();
            $("[id$=DD_Category]").select2();
            $("[id$=DD_Dinamic1]").select2();
            $("[id$=DD_Dinamic2]").select2();
            $("[id$=DD_Category_Grid]").select2();
            $("[id$=DD_Grid_Vendor]").select2();
        })

    </script>


</asp:Content>

