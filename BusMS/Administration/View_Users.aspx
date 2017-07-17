<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.Master" CodeBehind="View_Users.aspx.cs" Inherits="BusManagementSystem.View_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
         function InitializeDeleteConfirmation() {
             $('#dialog-confirm').dialog({
                 modal: true,
                 autoOpen: false,
                 title: "Confirmacion",
                 width: 350,
                 height: 160,
                 buttons: {
                     "Eliminar": function () {
                         $(this).dialog("close");
                     },
                     Cancel: function () {
                         $(this).dialog("close");
                     }
                 }
             });
         }
         function deleteItem(uniqueID) {
             var dialogTitle = 'Desea eliminar permanentemente eset usuario?';
             $("#dialog-confirm").html();

             $("#dialog-confirm").dialog({
                 title: dialogTitle,
                 modal: true,
                 autoOpen: false,
                 width: 350,
                 height: 170,
                 buttons: {
                     "Eliminar": function () {
                         __doPostBack(uniqueID, '');
                         $(this).dialog("close");
                     },
                     "Cancelar": function () { $(this).dialog("close"); }
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

</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
              <span class="pull-right">
                    <asp:LinkButton runat="server" OnClick="btHelp_Click"><i class="icon-info-sign"></i></asp:LinkButton>
            </span>
            <a href="./View_Users.aspx" title="Refresh Page" class="tip-bottom"><i class="icon-home"></i>
            <asp:Label ID="lbl_UserInformation_Page_Title" runat="server" Text=""></asp:Label></a></div>
    </div>
    <div class="messagealert" id="alert_container">
    </div>
    <div class="container-fluid">

        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5><asp:Label ID="lbl_UserInformation_PageTitle" runat="server" Text=""></asp:Label></h5>
                    </div>
                    <div class="widget-content">
                      <div class="searchGrid">
                            <div class="pull-left" style="width: 50%;">
                                <asp:Panel runat="server" DefaultButton="btSearchCH1">
                                    <span><asp:Label ID="lbl_UserInformation_Seach" runat="server" Text=""></asp:Label></span>
                                    <asp:TextBox ID="tbSearchCH1" style="width: 50%;" runat="server" PlaceHolder="Ingresa Usuario, Correo o FoxconnID"/><asp:LinkButton ID="btSearchCH1" runat="server" CssClass="btn btn-default" CausesValidation="false" OnClick="btSearchCH1_Click"><span class="icon-search"  aria-hidden="true"></span></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div style="float: right">
                                 <asp:Button ID="btExcel" CssClass="btn btn-success" Text="Export to Excel" runat="server" OnClick="btnExcel_Click" />
                            </div>
                        </div>

                        <asp:GridView ID="gridUsers" runat="server" PageSize="15" AllowPaging="True"
                            CellPadding="4" CssClass="table table-bordered data-table"
                            Width="100%" AutoGenerateColumns="false" OnRowDeleting="gridUsers_RowDeleting"
                            OnSelectedIndexChanged="gridUsers_SelectedIndexChanged" AllowSorting="True"
                            EmptyDataText="No records Found" ShowHeaderWhenEmpty="true" 
                            OnSorting="gridUsers_Sorting" OnPageIndexChanging="gridUsers_PageIndexChanging" OnDataBound="gridUsers_DataBound" OnRowDataBound="gridUsers_RowDataBound"
                            >
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btEdit" runat="server"
                                            ImageUrl="~/images/edit.png" border="0" CommandArgument='<%# Eval("User_ID")%>' OnClick="btEdit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btDelete" runat="server"
                                            ImageUrl="~/images/delete_user.png" CausesValidation="false" border="0" OnClientClick="javascript:return deleteItem(this.name);"
                                            CommandName="Delete"
                                             />
                                    </ItemTemplate>
                                </asp:TemplateField>
<%--                                <asp:TemplateField HeaderText="Change Password">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="btChangePass" runat="server"
                                            ImageUrl="~/images/password.png" CausesValidation="false" border="0" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="ID User" SortExpression="User_ID">
                                      <ItemTemplate>
                                          
                                        <asp:Label ID="User_ID" runat="server" Text='<%# Eval("User_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User name" SortExpression="User_name">
                                    <ItemTemplate>
                                        <asp:Label ID="User_name" runat="server" Text='<%# Eval("User_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="Email" runat="server" Text='<%# Eval("Email")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Foxconn ID" SortExpression="Foxconn_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="Foxconn_ID" runat="server" Text='<%# Eval("Foxconn_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor" SortExpression="Vendor_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="Vendor_ID" runat="server" Text='<%# Eval("Vendor_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role" SortExpression="Rol_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="Rol_ID" runat="server" Text='<%# Eval("Rol_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" SortExpression="Is_Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Is_Active" runat="server" Checked='<%# Eval("Is_Active")%>' Enabled="false"></asp:CheckBox>
                                       <%-- <asp:Label ID="Is_Active" runat="server" Text='<%# Eval("Is_Active")%>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Blocked" SortExpression="Is_Block">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Is_Block" runat="server" Checked='<%# Eval("Is_Block")%>' Enabled="false"></asp:CheckBox>
                                        <%--<asp:Label ID="Is_Block" runat="server" Text='<%# Eval("Is_Block")%>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                             <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>


                      
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dialog-confirm" title="Are you sure to delete this item?" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <asp:Label ID="lbl_ViewUsers_DialogConfirm" runat="server" Text=""></asp:Label>
        </p>
    </div>
        <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border:none" width="100%" height="100%" src="/Documentation/User.aspx"></iframe>
    </div>
     <script type="text/javascript">
         $('#cv a[href="#2a"]').tab('show');
    </script>
</asp:Content>
