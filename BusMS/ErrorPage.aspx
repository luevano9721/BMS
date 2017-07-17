<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="BusManagementSystem.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>Error unhandled</h5>
                    </div>
                    <div class="widget-content">
                        <div class="error_ex">
                            <h1>:'(</h1>
                            <h3>
                                <asp:Label ID="FriendlyErrorMsg" runat="server" Text="Label" Font-Size="Large" Style="color: red"></asp:Label></h3>
                            <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="false">
                                <p>&nbsp;</p>
                                <h4>Detailed Error:</h4>
                                <p>
                                    <asp:Label ID="ErrorDetailedMsg" runat="server" Font-Size="Small" /><br />
                                </p>

                                <h4>Error Handler:</h4>
                                <p>
                                    <asp:Label ID="ErrorHandler" runat="server" Font-Size="Small" /><br />
                                </p>

                                <h4>Detailed Error Message:</h4>
                                <p>
                                    <asp:Label ID="InnerMessage" runat="server" Font-Size="Small" /><br />
                                </p>
                                <p>
                                    <asp:Label ID="InnerTrace" runat="server" />
                                </p>
                            </asp:Panel>
                            <a class="btn btn-warning btn-big" href="MenuPortal.aspx">Back to Home</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
