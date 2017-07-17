<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_Rol.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.Administration.New_Rol" %>


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
                title: "Confirmación",
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


    <script type="text/javascript">
        $(function () {
            $("[id*=TreeView1] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                    var parentDIV = $(this).closest("DIV");
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");

                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");

                    }

                }
            });
        })
    </script>

    <style>
        .form-1 label {
            padding: 10px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right"><asp:LinkButton ID="btHelp" runat="server"  OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton></span>
            <a href="./View_Users.aspx" title="Go to Users Page" class="tip-bottom" style="float: right;"><i class="icon-user"></i>Back to Users</a>
            <a href="./New_Rol.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_NewRol_PageTitle" runat="server" Text=""></asp:Label></a></div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span6">
                <div class="widget-box">
                    <div class="widget-title">
                        <h5><asp:Label ID="lbl_NewRol_WidgetTitle1" runat="server" Text=""></asp:Label></h5>
                    </div>

                    <div class="widget-content">
                        <div class="form-1">
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewRol_ROLEID" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbRolID" runat="server" CssClass="span6" Enabled="false" placeholder="Enter Rol ID " TabIndex="1" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_NewRol_1"
                                        runat="server"
                                        ControlToValidate="tbRolID"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="tbRolID" ID="REV_NewRol_1" ValidationExpression="^[\s\S]{3,}$" runat="server" ErrorMessage="" Text="*" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>                                                     
                           
                            <asp:ValidationSummary
                                ID="VS_NewRol"
                                runat="server"
                                HeaderText=""
                                ShowMessageBox="false"
                                DisplayMode="BulletList"
                                BackColor="Snow"
                                ForeColor="Red"
                                Font-Italic="true" />
                            <div class="form-actions">
                                 <asp:Button ID="btAdd" Text="Add" CausesValidation="False" CssClass="btn btn-success" runat="server" OnClick="btAdd_Click"   />
                                 <asp:Button ID="btReset" Text="Reset" Visible="false" CausesValidation="False" CssClass="btn btn-primary" runat="server" OnClick="btReset_Click"  />
                                <asp:Button ID="btSave" runat="server" Text="" Visible="false" CssClass="btn btn-success" OnClick="btnSave_Click" />
                                <asp:Button ID="btDelete" Text="Delete" CausesValidation="False" CssClass="btn btn-danger" runat="server" Visible="false" OnClick="btDelete_Click" UseSubmitBehavior="false" />
                            </div>
                            <div id="dialog" style="display: none">
                                <asp:Label ID="lbl_Fare_Delete" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>



                <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5><asp:Label ID="lbl_NewRol_WidgetTitle2" runat="server" Text=""></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="GridView_roles" runat="server"
                            CssClass="table table-bordered data-table"
                            AllowPaging="True" OnSelectedIndexChanged="GridView_roles_SelectedIndexChanged" AllowSorting="True"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="True"
                            OnSorting="GridView_buses_Sorting" OnPageIndexChanging="GridView_roles_PageIndexChanging" OnDataBound="GridView_roles_DataBound" OnRowDataBound="GridView_roles_RowDataBound">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-inverse" >
<ControlStyle CssClass="btn btn-inverse"></ControlStyle>
                                </asp:CommandField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
                        

            </div>
        </div>

            </div>


            <div class="span6">
                <div class="widget-box">
                    <div class="widget-title">
                        <h5><asp:Label ID="lbl_NewRol_WidgetTitle3" runat="server" Text=""></asp:Label></h5>
                    </div>

                    <div class="widget-content">
                        <div style="padding: 10px; background-color: aliceblue; margin: 10px">
                            <h4 class="alert-heading">
                                <asp:Label ID="lbl_NewRol_Info" runat="server" Text=""></asp:Label></h4>
                            <asp:Label ID="lbl_NewRol_Info2" runat="server" Text=""></asp:Label>
                            <h5 class="alert-heading">
                                <asp:Label ID="lbUser" runat="server"></asp:Label><asp:Label ID="labelUser" runat="server" Visible="false"></asp:Label></h5>
                            <h5 class="alert-heading">
                                <asp:Label ID="lbrol" runat="server"></asp:Label><asp:Label ID="labelRole" runat="server" Visible="false"></asp:Label></h5>
                        </div>


                        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" ShowCheckBoxes="All" NodeIndent="15" >
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="1px" VerticalPadding="2px"></NodeStyle>
                            <ParentNodeStyle Font-Bold="False" />
                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="1px"
                                VerticalPadding="0px" />
                        </asp:TreeView>
                        <div style="margin: 10px;">
                            

                             <asp:Button ID="btSaveRolPermissions" Text="" runat="server" 
                                   CssClass="btn btn-success" Visible="false" CausesValidation="false" OnClick="btSaveRolPermissions_Click"  />
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border:none" width="100%" height="100%" src="/Documentation/Role.aspx"></iframe>
    </div>
    <script type="text/javascript">
        $('#cv a[href="#2a"]').tab('show');
    </script>
</asp:Content>