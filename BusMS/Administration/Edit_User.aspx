<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_User.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.Edit_User" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
        $(function () {
            $("[id*=tbBirthdate]").datepicker({
                maxDate: '-16Y',
                dateFormat: 'mm/dd/yy',
                changeYear: true
            }).attr('readonly', 'readonly');
        });
        $(function () {
            $("[id*=tbHireDate]").datepicker({
                maxDate: 0,
                dateFormat: 'mm/dd/yy',
                changeYear: true
            }).attr('readonly', 'readonly');
        });
        $(function () {
            $("[id*=tbExpiration]").datepicker({
                minDate: 0,
                dateFormat: 'mm/dd/yy',
                changeYear: true
            }).attr('readonly', 'readonly');
        });
        function checkEnabled(checkbox) {
            if (checkbox.checked) {
                document.getElementById("<%=checkBlacklist.ClientID%>").checked = false;
            }
        }
        function checkBlock(checkbox) {
            if (checkbox.checked) {
                document.getElementById("<%=checkEnabled.ClientID%>").checked = false;
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

    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right"><asp:LinkButton ID="btHelp" runat="server" OnClick="btHelp_Click" CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton></span>
            <a href="./View_Users.aspx" title="Go to Users Page" class="tip-bottom" style="float: right;"><i class="icon-user"></i>Back to Users</a>
            <a href="./Edit_User.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_EditUser_PageTitle" runat="server" Text=""></asp:Label></a>
        </div>

    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span6">
                <div class="widget-box">
                    <div class="widget-title">
                        <h5><asp:Label ID="lbl_EditUser_widgettitle" runat="server" Text=""></asp:Label></h5>
                    </div>

                    <div class="widget-content">
                        <div class="form-1">
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_BMSID" runat="server" Text=""></asp:Label> </label>
                                <div class="controls">
                                    <asp:TextBox ID="tbUsername" runat="server" CssClass="span6" placeholder="Enter BMS username" TabIndex="1" MaxLength="50" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_BMSID"
                                        runat="server"
                                        ControlToValidate="tbUsername"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="tbUsername" ID="REV_EditUser_UserName" ValidationExpression="^[\s\S]{3,}$" runat="server" ErrorMessage="BMS ID requires at least 3 characters" Text="*" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_CompleteUserName" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbName" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="span6" placeholder="Enter complete name" onkeypress="return lettersOnly(event)" TabIndex="2" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_CompleteName"
                                        runat="server"
                                        ControlToValidate="tbName"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_EmployeeID" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbfoxconnID" runat="server" CssClass="span6" placeholder="Example: 12589" onkeypress="return onlyNumbers(event)" TabIndex="3" MaxLength="15"></asp:TextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_departament" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbDepartment" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="span6" placeholder="Enter user deparment" TabIndex="6" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_Birthdare" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbBirthdate" runat="server" data-date-format="mm/dd/yyyy" placeholder="Select a date" TabIndex="7"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_BirthDate"
                                        runat="server"
                                        ControlToValidate="tbBirthdate"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_HireDate" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbHireDate" runat="server" data-date-format="mm/dd/yyyy" CssClass="datepicker span6" placeholder="Select a date" TabIndex="8"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_HireDate"
                                        runat="server"
                                        ControlToValidate="tbHireDate"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_Address" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbAddress" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="span6" placeholder="Enter Address" TabIndex="9" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label"><asp:Label ID="lbl_NewUser_Telephone" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbTelephone" runat="server" CssClass="span6" placeholder="Enter phone number" onkeypress="return onlyNumbers(event)" TabIndex="10" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_Telephone"
                                        runat="server"
                                        ControlToValidate="tbTelephone"
                                        ErrorMessage=""
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REV_EditUser_Telephone" runat="server" Text="*"
                                        ControlToValidate="tbTelephone" ErrorMessage="Enter a valid phone number (10 digits)"
                                        ValidationExpression="[0-9]{10}" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_ExpirationPasswordDate" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbExpiration" runat="server" data-date-format="mm/dd/yyyy" CssClass="span6" placeholder="Select a date" TabIndex="11"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_ExpirationPassword"
                                        runat="server"
                                        ControlToValidate="tbExpiration"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_Profile" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:DropDownList runat="server" ID="cbProfile" AppendDataBoundItems="true" TabIndex="12" OnSelectedIndexChanged="cbProfile_SelectedIndexChanged"  AutoPostBack="True">
                                         <asp:ListItem Value="NA" Text="Select a value" />
                                        <asp:ListItem Value="INTERNAL" Text="Internal" />
                                        <asp:ListItem Value="EXTERNAL" Text="External" />
                                    </asp:DropDownList>
                            </div>
                                   </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_Role" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:DropDownList runat="server" ID="cbRole" AppendDataBoundItems="true" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="cbRole_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_Role"
                                        runat="server"
                                        ControlToValidate="cbRole"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_Vendor" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:DropDownList runat="server" ID="cbVendor" AppendDataBoundItems="true" TabIndex="13">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_Vendor"
                                        runat="server"
                                        ControlToValidate="cbVendor"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_NewUser_Email" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox ID="tbemail" runat="server" placeholder="example@foxconn.com" TabIndex="14" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RV_EditUser_Email"
                                        runat="server"
                                        ControlToValidate="tbemail"
                                        ErrorMessage=''
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REV_EditUser_Email" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbEmail" ErrorMessage="Invalid Email Format" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_C_Driver_Enabled" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:CheckBox ID="checkEnabled" runat="server" onclick="checkEnabled(this)" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    <asp:Label ID="lbl_C_Bus_Blacklist" runat="server" Text="Label"></asp:Label></label>
                                <div class="controls">
                                    <asp:CheckBox ID="checkBlacklist" runat="server" onclick="checkBlock(this)" />
                                </div>
                            </div>
                            <asp:ValidationSummary
                                ID="VS_EditUser"
                                runat="server"
                                HeaderText=""
                                ShowMessageBox="false"
                                DisplayMode="BulletList"
                                BackColor="Snow"
                                ForeColor="Red"
                                Font-Italic="true" />
                            <div class="form-actions">
                                <asp:Button ID="btnUpdate" runat="server" Text=""
                                    CssClass="btn btn-success" OnClick="btnUpdate_Click" />
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
                        <%--                        <div style="padding: 10px; background-color: aliceblue; margin: 10px">
                            <h4 class="alert-heading">
                                <asp:Label ID="lbInfo" runat="server" Text="First , add a user"></asp:Label></h4>
                            <asp:Label ID="lbInfo2" runat="server" Text="You need add a user to select permissions and privileges!"></asp:Label>
                            <h5 class="alert-heading">
                                <asp:Label ID="lbUser" runat="server"></asp:Label><asp:Label ID="labelUser" runat="server" Visible="false"></asp:Label></h5>
                            <h5 class="alert-heading">
                                <asp:Label ID="lbrol" runat="server"></asp:Label><asp:Label ID="labelRole" runat="server" Visible="false"></asp:Label></h5>
                        </div>--%>


                        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" ShowCheckBoxes="All" NodeIndent="15">
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="1px" VerticalPadding="2px"></NodeStyle>
                            <ParentNodeStyle Font-Bold="False" />
                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="1px"
                                VerticalPadding="0px" />
                        </asp:TreeView>
                        <div style="margin: 10px;">
                            <asp:Button ID="btSaveRol" runat="server" Text="Save permissions"
                                CssClass="btn btn-success" Visible="false" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>Registered drivers</h5>
                        <div style="float: right;  padding: 0px 5px;">
                            <asp:DropDownList runat="server" ID="cbVendorAdm" AppendDataBoundItems="true" class="span3 m-wrap" TabIndex="1" Width="150px" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div style="float: right; vertical-align: central;">
                            <span class="icon"><i class="icon-filter"></i></span>
                            <h5>Vendor</h5>
                        </div>
                    </div>
                    <div class="widget-content">
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border:none" width="100%" height="100%" src="/Documentation/Edit_User.aspx"></iframe>
    </div>
        <script type="text/javascript">
            $('#cv a[href="#2a"]').tab('show');
    </script>
</asp:Content>
