<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Settings.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.User_Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />

    <script src="js/bootstrap-colorpicker.js"></script>
    <script src="js/masked.js"></script>
    <script src="js/jquery.uniform.js"></script>
    <script src="js/select2.min.js"></script>
    <script src="js/matrix.form_common.js"></script>

    
       

    <script type="text/javascript">
        function reloadPage() {
            window.location.reload()
        }
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
                    <asp:LinkButton runat="server" OnClick="btHelp_Click"><i class="icon-info-sign"></i></asp:LinkButton>
            </span>
            <a href="../MenuPortal.aspx" title="Go to Home" class="tip-bottom"><i class="icon-home"></i>
            <asp:Label ID="lbl_UserSettings_PageTitle" runat="server" Text=""></asp:Label></a></div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">

            <div class="widget-box">

                <div class="widget-title">
                    <a data-parent="#collapse-group" href="#collapseGOne" data-toggle="collapse"><span class="icon"><i class="icon-cogs"></i></span>
                        <h5><asp:Label ID="lbl_UserSettings_WidgetTitle1" runat="server" Text=""></asp:Label></h5>
                    </a>
                </div>

                <div class="accordion-body in collapse" id="collapseGOne" style="height: auto;">

                    <div class="widget-content">

                        <div class="control-group">
                            <div class="span4">
                                <label class="control-label" style="padding-right: 10px;">
                                    <asp:Label ID="lbl_UserSettings_CurrentPassword" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox runat="server" TabIndex="1" ID="currentPassword" TextMode="Password" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="span4">
                                <label class="control-label" style="padding-right: 10px;"><asp:Label ID="lbl_UserSettings_NewPassword" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox runat="server" TabIndex="2" ID="newPassword" TextMode="Password"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RV_UserSettings_NewPassword" runat="server" Display="dynamic"
                                    ControlToValidate="newPassword" ForeColor="#cc0000"
                                    ErrorMessage=""
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    CssClass="span6 m-wrap"
                                    Text=" "
                                    ValidationGroup="ValidatePassword"> 
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REV_UserSettings_NewPassword" runat="server" Display="dynamic"
                                    ControlToValidate="newPassword"
                                    ErrorMessage=""
                                    ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$" ForeColor="#cc0000"
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    CssClass="span6 m-wrap"
                                    Text=" "
                                    ValidationGroup="ValidatePassword" />
                            </div>
                            <div class="span4">
                                <label class="control-label" style="padding-right: 10px;">
                                    <asp:Label ID="lbl_UserSettings_ConfirmPassword" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox runat="server" TabIndex="3" ID="confirmPassword" TextMode="Password"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RV_UserSettings_ConfirmPassword" runat="server" Display="dynamic"
                                    ControlToValidate="confirmPassword" ForeColor="#cc0000"
                                    ErrorMessage=""
                                    EnableClientScript="true"
                                    SetFocusOnError="true"
                                    CssClass="span6 m-wrap"
                                    Text=" "
                                    ValidationGroup="ValidatePassword"> 
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REV_UserSettings_ConfirmPassword" runat="server" Display="dynamic"
                                    ControlToValidate="confirmPassword"
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
                        </div>


                        <div class="form-actions">
                            <asp:Button ID="btPassword" Text="" CssClass="btn btn-info" runat="server" ValidationGroup="ValidatePassword" OnClick="btPassword_Click" />

                        </div>
                        <asp:ValidationSummary
                            ID="VS_UserSettings_Password"
                            runat="server"
                            HeaderText=""
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            BackColor="Snow"
                            ForeColor="Red"
                            Font-Italic="true"
                            ValidationGroup="ValidatePassword" />
                    </div>
                </div>


            </div>




            <div class="widget-box">
                <div class="widget-title">
                    <a data-parent="#collapse-group" href="#collapseGTwo" data-toggle="collapse"><span class="icon"><i class="icon-envelope-alt"></i></span>
                        <h5><asp:Label ID="lbl_UserSettings_WidgetTitle2" runat="server" Text=""></asp:Label></h5>
                    </a>
                </div>

                <div class="accordion-body in collapse" id="collapseGTwo" style="height: auto;">

                    <div class="widget-content">

                        <div class="control-group">
                            <div class="span6">
                                <label class="control-label" style="padding-right: 10px;">
                                    <asp:Label ID="lbl_UserSettings_CurrentEmail" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox runat="server" TabIndex="4" ID="currentEmail" Enabled="false" Text="test@mail.com"></asp:TextBox>
                                </div>
                            </div>
                            <div class="span6">
                                <label class="control-label" style="padding-right: 10px;">
                                    <asp:Label ID="lbl_UserSettings_EnterNewEmail" runat="server" Text=""></asp:Label></label>
                                <div class="controls">
                                    <asp:TextBox runat="server" TabIndex="5" ID="newEmail"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RV_UserSettings_EnterNewEmail" runat="server" Display="dynamic"
                                        ControlToValidate="newEmail" ForeColor="#cc0000"
                                        ErrorMessage=""
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        CssClass="span6 m-wrap"
                                        Text=" "
                                        ValidationGroup="ValidateEmail"> 
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REV_UserSettings_NewEmail"
                                        runat="server"
                                        ErrorMessage=""
                                        ForeColor="#cc0000"
                                        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="newEmail"
                                        EnableClientScript="true"
                                        SetFocusOnError="true"
                                        Text=" "
                                        CssClass="span6 m-wrap"
                                        ValidationGroup="ValidateEmail">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>



                        <div class="form-actions">
                            <asp:Button ID="btSave" Text="" CssClass="btn btn-info" runat="server" ValidationGroup="ValidateEmail" OnClick="btSave_Click" />

                        </div>
                    </div>
                    <asp:ValidationSummary
                        ID="VS_UserSettings_Email"
                        runat="server"
                        HeaderText=""
                        ShowMessageBox="false"
                        DisplayMode="BulletList"
                        BackColor="Snow"
                        ForeColor="Red"
                        Font-Italic="true"
                        ValidationGroup="ValidateEmail" />
                </div>
            </div>






            <div class="widget-box">

                <div class="widget-title">
                    <a data-parent="#collapse-group" href="#collapseG3" data-toggle="collapse"><span class="icon"><i class="icon-dashboard"></i></span>
                        <h5><asp:Label ID="lbl_UserSettings_LanguageNThemeColor" runat="server" Text=""></asp:Label></h5>
                    </a>

                </div>


                <div class="accordion-body in collapse" id="collapseG3" style="height: auto;">
                    <div class="widget-content">

                        <div class="control-group">
                            <div class="span6">
                                <label class="control-label" style="padding-right: 10px;">
                                    <asp:Label ID="lbl_UserSettings_WidgetTitle3" runat="server" Text=""></asp:Label></label>

                                <div class="controls">
                                    <div data-color-format="hex" data-color="#000000" class="input-append color colorpicker">
                                        <asp:TextBox runat="server" TabIndex="3" ID="txt_themeColorValue" value="#000000" class="span11"></asp:TextBox>
                                        <span class="add-on"><i style="background-color: #000000"></i></span>

                                    </div>
                                </div>
                            </div>
                            <div class="span6">
                                <label class="control-label" style="padding-right: 10px;">
                                    <asp:Label ID="lblUserSettings_Language" runat="server" Text=""></asp:Label> </label>

                                <div class="controls">

                                    <asp:DropDownList runat="server" TabIndex="3" ID="txtlanguage" value="Select language" class="span2"></asp:DropDownList>

                                </div>
                            </div>
                        </div>


                        <div class="form-actions">
                            <asp:Button runat="server" ID="btGLobal" Text="" CssClass="btn btn-success" OnClick="btnSaveGlobal_Click" />
                            <asp:Button runat="server" ID="btSession" Text="" CssClass="btn btn-info" OnClick="btnSaveSession_Click" />
                            <asp:Button ID="btDesafult" runat="server" Text="" CssClass="btn btn-info" OnClick="imb_DefaultSettings_Click" />
                        </div>

                    </div>


                </div>
            </div>







        </div>
    </div>


    <div id="dialog" style="display: none">
        <asp:Label ID="lbl_UserSettings_Dialog" runat="server" Text=""></asp:Label>
        
    </div>
     <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border:none" width="100%" height="100%" src="/Documentation/User_settings.aspx"></iframe>
    </div>
    <script type="text/javascript">

        $('a[href="#SystemSettings"]').tab('show');
    </script>
</asp:Content>
