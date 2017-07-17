<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $("[id*=btPassword]").removeAttr("onclick");
            $("#dialog").dialog({
                modal: true,
                autoOpen: false,
                title: "Confirmacion de actualizacion de contraseña",
                width: 350,
                height: 160,
                buttons: [
                {
                    id: "Yes",
                    text: "Aceptar",
                    click: function () {
                        $("[id*=btPassword]").attr("rel", "Save");
                        $("[id*=btPassword]").click();
                    }
                },
                {
                    id: "No",
                    text: "Cancelar",
                    click: function () {
                        $(this).dialog('close');
                    }
                }
                ]
            });
            $("[id*=btPassword]").click(function () {
                if ($(this).attr("rel") != "Save") {
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
            $("[id*=tbExpiration]").datepicker();
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
    </script>
    <style>
        .checkbox_pass {
        padding-left:5px;
        }

        </style>
    <link href="/css/chosen.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/docsupport/prism.css">
    <%--     <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                <asp:LinkButton runat="server" OnClick="btHelp_Click"><i class="icon-info-sign"></i></asp:LinkButton>
            </span>
            <a href="./View_Users.aspx" title="Go to Users Page" class="tip-bottom" style="float: right;"><i class="icon-user"></i>Back to Users</a>
            <a href="./ChangePassword.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_UserChangePassword_PageTitle" runat="server" Text=""></asp:Label></a>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <h5>
                            <asp:Label ID="lbl_UserChangePassword_widget_Title1" runat="server" Text=""></asp:Label></h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="control-group">
     
                            <asp:Label ID="lbl_UserChangePassword_UserID" class="control-label" Style="padding-right: 5px;" runat="server" Text=""></asp:Label>
                            <div class="controls">
                                <asp:DropDownList runat="server" ID="cbUsers" OnSelectedIndexChanged="cbUsers_SelectedIndexChanged" AutoPostBack="true" CssClass="chosen-select"></asp:DropDownList>
                                <asp:Button runat="server" ID="btSelectUser" Text="Select" CssClass="btn btn-info" OnClick="btSelectUser_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span6">
                <div class="widget-box">
                    <div class="accordion-heading">
                        <div class="widget-title">

                            <h5>
                                <asp:Label ID="lbl_UserChangePassword_ChangePassword" runat="server" Text=""></asp:Label></h5>

                        </div>
                    </div>

                    <div class="widget-content nopadding">
                        <div class="control-group">
        
                            <asp:Label ID="lbl_UserChangePassword_NewPassword" runat="server" class="control-label" Style="padding-right: 10px;" Text=""></asp:Label>
                            <div class="controls">
                                <asp:TextBox runat="server" TabIndex="2" ID="newPassword" TextMode="Password" Enabled="false" MaxLength="10"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="RV_ChangePassword_NewPassword" runat="server" Display="dynamic"
                                ControlToValidate="newPassword" ForeColor="#cc0000"
                                ErrorMessage=""
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                CssClass="span6 m-wrap"
                                Text=" "
                                ValidationGroup="ValidatePassword"> 
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REV_ChangePassword_CaseLetters" runat="server" Display="dynamic"
                                ControlToValidate="newPassword"
                                ErrorMessage=""
                                ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$" ForeColor="#cc0000"
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                CssClass="span6 m-wrap"
                                Text=" "
                                ValidationGroup="ValidatePassword" />
                        </div>
                        <div class="control-group">

                            <asp:Label ID="lbl_UserChangePassword_ConfirmPassword" class="control-label" Style="padding-right: 10px;" runat="server" Text=""></asp:Label>
                            <div class="controls">
                                <asp:TextBox runat="server" TabIndex="3" ID="confirmPassword" TextMode="Password" Enabled="false" MaxLength="10"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="RV_ChangePassword_ConfirmPassword" runat="server" Display="dynamic"
                                ControlToValidate="confirmPassword" ForeColor="#cc0000"
                                ErrorMessage=""
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                CssClass="span6 m-wrap"
                                Text=" "
                                ValidationGroup="ValidatePassword"> 
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REV_ChangePassword_CaseLetters1" runat="server" Display="dynamic"
                                ControlToValidate="newPassword"
                                ErrorMessage=""
                                ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$" ForeColor="#cc0000"
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                CssClass="span6 m-wrap"
                                Text=" "
                                ValidationGroup="ValidatePassword" />
                            <asp:CompareValidator runat="server" Display="dynamic"
                                ControlToValidate="confirmPassword"
                                ControlToCompare="newPassword"
                                ErrorMessage="Passwords do not match."
                                Text=" "
                                ForeColor="#cc0000"
                                EnableClientScript="true"
                                SetFocusOnError="true"
                                CssClass="span6 m-wrap"
                                ValidationGroup="ValidatePassword" />
                        </div>
                        <div class="control-group" style="padding-right: 15px;">
                            <asp:Label ID="lbl_checkPassExpiration" runat="server" class="control-label" />
                           
                            <div class="controls">
                                 <asp:CheckBox runat="server" ID="checkPassExpiration" CssClass="checkbox_pass" />
                                <asp:TextBox runat="server" ID="tbExpirationDate" CssClass="datepicker" Enabled="false" data-date-format="mm/dd/yyyy" />
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btPassword" Text="Save" CssClass="btn btn-info" runat="server" ValidationGroup="ValidatePassword" UseSubmitBehavior="false" Enabled="false" OnClick="btPassword_Click" />
                            <asp:ValidationSummary
                                ID="VS_ChangePassword"
                                runat="server"
                                HeaderText="Following error occurs....."
                                ShowMessageBox="false"
                                DisplayMode="BulletList"
                                BackColor="Snow"
                                ForeColor="Red"
                                Font-Italic="true"
                                ValidationGroup="ValidatePassword" />
                        </div>

                    </div>

                </div>

            </div>

            <div class="span6">
                <div class="widget-box">
                    <div class="accordion-heading">
                        <div class="widget-title">
                            <h5>
                                <asp:Label ID="lbl_UserChangePassword_WidgetTitle1" runat="server" Text=""></asp:Label></h5>
                        </div>
                    </div>

                    <div class="widget-content nopadding">
                        <div class="control-group">
                            
                            <asp:Label ID="lbl_UserChangePassword_UserID1" class="control-label" Style="padding-right: 5px; font-weight: bold;" runat="server" Text="Label"></asp:Label>
                            <asp:Label runat="server" ID="lbUserId" class="control-label"></asp:Label>


                        </div>
                        <div class="control-group">
                            
                            <asp:Label ID="lbl_UserChangePassword_UserName" class="control-label" Style="padding-right: 5px; font-weight: bold;" runat="server" Text="Label"></asp:Label>
                            <asp:Label runat="server" ID="lbusername" class="control-label"></asp:Label>


                        </div>
                      <%--  <div class="control-group">
                            
                            <asp:Label ID="lbl_UserChangePassword_CurrentPassword" class="control-label" Style="padding-right: 5px; font-weight: bold;" runat="server" Text="Label"></asp:Label>
                            <asp:Label runat="server" ID="lbCurrentPassword" class="control-label"></asp:Label>


                        </div>--%>
                        <div class="control-group">
                            
                            <asp:Label ID="lbl_UserChangePassword_ExpirationDatePassowrd" class="control-label" Style="padding-right: 5px; font-weight: bold;" runat="server" Text="Label"></asp:Label>
                            <asp:Label runat="server" ID="lbExpirationDate" class="control-label"></asp:Label>


                        </div>
                        <div class="form-actions">
                        </div>
                    </div>


                </div>

            </div>
        </div>

    </div>
    <div id="dialog" style="display: none">
        <asp:Label ID="lbl_ChangePassword_Dialog" runat="server" Text=""></asp:Label>
    </div>
    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border: none" width="100%" height="100%" src="/Documentation/Change_Password.aspx"></iframe>
    </div>
    <script type="text/javascript">
        $('#cv a[href="#2a"]').tab('show');
    </script>
    <script src="/js/chosen.jquery.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script src="/css/docsupport/init.js" type="text/javascript" charset="utf-8"></script>
</asp:Content>
