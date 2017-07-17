<%@ Page Title="Routes" Language="C#" AutoEventWireup="true" CodeBehind="C_Route.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem._01Catalogos.C_Route" %>


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

        function validateHhMm(inputField) {
            var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField.value);

            if (isValid) {
                inputField.style.backgroundColor = '#bfa';
            } else {
                inputField.style.backgroundColor = '#fba';
            }

            return isValid;
        }

        function get_time(id) {
            var dt = new Date();
            var minutes;
            var elem = $("#" + id + "").val();
            if (dt.getMinutes() < 10)
            { minutes = '0' + dt.getMinutes(); } else { minutes = dt.getMinutes(); }
            if (elem.trim().length == 0) {
                var time = dt.getHours() + ":" + minutes;
                //":" + (
                //dt.getMinutes() < 10 ? '0' : '' + dt.getMinutes());
                document.getElementById(id).value = time;
            }
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
            <a href="./C_Route.aspx" title="Refresh" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_C_Route_Page_Title" runat="server"></asp:Label></a>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-ok"></i></span>
                            <h5>
                                <asp:Label ID="lbl_C_Route_Widget_Title1" runat="server"></asp:Label></h5>
                        </a>
                    </div>
                    <div class="collapse" id="collapseGOne">
                        <div class="widget-content">
                            <div class="controls control-group">
                                <div class="span12">

                                    <div class="span2">
                                        <asp:Label ID="lbl_C_Route_Route_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbRoute" name="tbRoute" Width="100%" Enabled="false" data-provide="timepicker" runat="server" onkeypress="return onlyNumbers(event)"
                                            CssClass="span2 m-wrap" placeholder="Enter Route ID" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_Route_tbRoute"
                                            runat="server"
                                            ControlToValidate="tbRoute"
                                            ErrorMessage='Route ID is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="span2">
                                        <asp:Label ID="lbl_C_Route_Travel_Time" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:TextBox ID="tbTime" runat="server" Width="100%" CssClass="span2 m-wrap"
                                             placeholder="00:00" TabIndex="2"
                                            onFocus="get_time(this.id)" 
                                             onkeypress="return onlyNumbersWidthColon(event)" 
                                            onchange="validateHhMm(this);" ></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="val_Route_tbTime"
                                            runat="server"
                                            ControlToValidate="tbTime"
                                            ErrorMessage='Travel time is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RF1" runat="server" ErrorMessage="Format time 00:00"
                                                            ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" ControlToValidate="tbTime" Text="*">
                                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="controls control-group">
                                <div class="span12">
                                    <div class="span2">
                                        <asp:Label ID="lbl_C_Route_Origin_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="cbOrigin" Width="100%"
                                            AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_Route_cbOrigin"
                                            runat="server"
                                            ControlToValidate="cbOrigin"
                                            ErrorMessage='Origin ID is required'
                                            EnableClientScript="true"
                                            SetFocusOnError="true"
                                            ForeColor="Red"
                                            CssClass="span1 m-wrap"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="span2">
                                        <asp:Label ID="lbl_C_Route_Destination_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="cbDestination" Width="100%" AppendDataBoundItems="true"
                                            class="span2 m-wrap" TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_Route_cbDestination"
                                            runat="server"
                                            ControlToValidate="cbDestination"
                                            ErrorMessage='Destination ID is required'
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
                                    <div class="span2">
                                        <asp:Label ID="lbl_C_Route_Service_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="cbService" Width="100%" AppendDataBoundItems="true"
                                            class="span2 m-wrap" TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_Route_cbService"
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

                                    <div class="span2">
                                        <asp:Label ID="lbl_C_Route_Zone_ID" runat="server"></asp:Label>
                                    </div>
                                    <div class="span3">
                                        <asp:DropDownList runat="server" ID="ddlZoneID" Width="100%"
                                            AppendDataBoundItems="true" class="span2 m-wrap" TabIndex="6">
                                         <%--   <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="val_Route_ddlZoneID"
                                            runat="server"
                                            ControlToValidate="cbDestination"
                                            ErrorMessage='Destination ID is required'
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
                                <asp:Button ID="btnUpdate_Route" CssClass="btn btn-info" runat="server" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btDelete" CausesValidation="False" CssClass="btn btn-danger" runat="server" OnClick="btDelete_Click" UseSubmitBehavior="false" />

                            </div>
                            <div id="dialog" style="display: none;">
                                <asp:Label ID="lbl_Route_Dialog" runat="server" />
                            </div>
                            <asp:ValidationSummary
                                ID="vs_Route_Summary"
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
                            <asp:Label ID="lbl_C_Route_Widget_Title2" runat="server"></asp:Label></h5>
                    </div>
                    <div class="widget-content">
                        <div class="searchGrid">
                            <div style="float: left">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span>
                                        <asp:Label ID="lbl_Search" runat="server"></asp:Label>
                                    </span>
                                    <asp:TextBox ID="tbSearchCH1" runat="server" PlaceHolder="Ingresa origen, destino o servicio" />
                                    <asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right; margin: 0px 5px;">
                                <asp:Button ID="btExcel" Text="Export to Excel" CausesValidation="False" CssClass="btn btn-success" runat="server" UseSubmitBehavior="false" OnClick="btExcel_Click" />
                            </div>
                            <div style="float: right; margin: 0px 5px;">
                                <asp:DropDownList runat="server" ID="ddlZone"
                                    AppendDataBoundItems="true" CssClass="span3 m-wrap"
                                    TabIndex="1" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="cbZone_SelectedIndexChanged">
                                    <%--<asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>

                            <div style="float: right;">
                               <h5 style=" margin: 5px 0;">
                                    <asp:Label ID="lbl_C_Route_Zone" runat="server"></asp:Label></h5>
                            </div>
                            <div style="float: right; margin: 0px 5px;">
                                <asp:DropDownList runat="server" ID="ddlType"
                                    AppendDataBoundItems="true" CssClass="span3 m-wrap"
                                    TabIndex="2" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="cbType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">Entrada</asp:ListItem>
                                    <asp:ListItem Value="2">Salida</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div style="float: right;"">
                                <h5 style=" margin: 5px 0;">
                                    <asp:Label ID="lbl_C_Route_Type" runat="server"></asp:Label></h5>
                            </div>

                        </div>
                        <asp:GridView ID="GridView_Routes" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" OnSelectedIndexChanged="GridView_Routes_SelectedIndexChanged"
                            AllowSorting="True" EmptyDataText="No records Found" ShowHeaderWhenEmpty="true"
                            OnSorting="GridView_Routes_Sorting" OnPageIndexChanging="GridView_Routes_PageIndexChanging">
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
            $("select.[id$=ddlZoneID]").select2();
            $("select.[id$=cbOrigin]").select2();
            $("select.[id$=cbDestination]").select2();
            $("select.[id$=ddlType]").select2();
            $("select.[id$=ddlZone]").select2();
            $("[id$=cbService]").select2();



        })

    </script>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Route.aspx"></iframe>
    </div>
</asp:Content>
