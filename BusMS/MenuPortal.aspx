<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="MenuPortal.aspx.cs" Inherits="BusManagementSystem.MenuPortal" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $("#iconHelp").off("click");
        $(document).on("click", "#iconHelp", function () {
            BMSHelp("FAQ BMS")
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
    <style type="text/css">
        .style1 {
            width: 306px;
        }

        .style2 {
            width: 353px;
            height: 35px;
        }

        .style4 {
            width: 306px;
            height: 400px;
        }

        .style7 {
            width: 266px;
        }
    </style>
</asp:Content>

<asp:Content ID="Body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">


    <!--First , we have to include this div to display the section name in the top-->
    <div id="content-header">
        <div id="breadcrumb">
            <span class="pull-right">
                    <asp:LinkButton runat="server" OnClick="btHelp_Click"><i class="icon-info-sign"></i></asp:LinkButton>
            </span>
            <a href="./MenuPortal.aspx" title="Go to Home" class="tip-bottom"><i class="icon-home"></i>
                <asp:Label ID="lbl_MenuPortal_Welcome" runat="server" Text=""></asp:Label>
            </a>

        </div>

    </div>

    <div class="container-fluid">
        <div class="quick-actions_homepage">
            <ul class="quick-actions">

                <asp:Literal ID="ltMenu" runat="server" />
            </ul>

            <!--End-Action boxes-->
        </div>



        <div class="row-fluid">

            <div class="span6">

                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-ok"></i></span>
                        <h5>
                            <asp:Label ID="lbl_MenuPortal_UltimasAlertas" runat="server" Text=""></asp:Label></h5>

                    </div>
                    <div class="widget-content">
                        <div class="todo">
                            <ul>
                                <asp:Literal ID="litTable" runat="server" />
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="widget-box">
                    <div class="widget-title bg_lo">
                        <span class="icon"><i class="icon-chevron-down"></i></span>
                        <h5>
                            <asp:Label ID="lbl_MenuPortal_OperacionesRecientes" runat="server" Text=""></asp:Label></h5>

                    </div>
                    <div class="widget-content nopadding">
                        <asp:Literal ID="ltRecentOp" runat="server" />
                    </div>
                </div>

            </div>
            <div class="span6">
                <div class="widget-box">

                    <div class="widget-title">
                        <span class="icon"><i class="icon-ok"></i></span>
                        <h5>
                            <asp:Label ID="lbl_MebuPortal_PendingActivities" runat="server" Text=""></asp:Label></h5>
                    </div>
                    <div class="widget-content">
                        <div class="todo">
                            <ul>
                                <asp:Literal ID="ltPendingAct" runat="server" />
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="widget-box">
                    <div class="widget-title bg_lo" data-toggle="collapse" href="#collapseG3">
                        <span class="icon"><i class="icon-chevron-down"></i></span>
                        <h5>
                            <asp:Label ID="lbl_MenuPortal_RevisionesRecientes" runat="server" Text="Label"></asp:Label></h5>
                    </div>
                    <div class="widget-content nopadding updates collapse in" id="collapseG3">

                        <asp:Literal ID="ltRecentRev" runat="server" />
                    </div>
                </div>


            </div>
        </div>

    </div>

    <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border:none" width="100%" height="100%" src="/Documentation/GeneralUse.aspx"></iframe>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

</asp:Content>



