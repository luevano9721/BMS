<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="R_BusStandard.aspx.cs"   MasterPageFile="~/MasterPage.master" Inherits="BusManagementSystem.Reportes.R_BusStandard" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="/js/jquery-ui.js" type="text/javascript"></script>
    <link href="/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        // This function is a workaround to fix the compatibility issue with calendar buttons in Chrome when the page is a Report (Reporting Services)
        $(document).ready(function () {
            initDatePickers();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(applicationInitHandler);

            function applicationInitHandler() {
                initDatePickers();
            }

            function initDatePickers() {
                var isChrome = !!window.chrome && !!window.chrome.webstore; // To know if the machine is using Chrome
                if (isChrome) {
                    $('[id*=ParameterTable] td span:contains("Fecha")') // Get all Date inputs in the page
                    .each(function () {
                        var td = $(this).parent().next();
                        $('input', td).datepicker(); // Apply datepicket function (only when is Chrome Browser. If the User is using IE , then the original controls not be replaced)
                    });
                }
            }
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
<style type="text/css">
        #ReportViewer1_ctl04_ctl11_divDropDown
       {
            text-align:left;
           
        }
        .MultipleValues{
            overflow-x:auto;
        }

        .MultipleValues label 
        {
            display: inline;
            font-size: 12PX;
            font-variant-position: sub;
            
        }
</style><div class="MultipleValues">
    <span class="badge  pull-right" style="background-color:white;"><asp:LinkButton runat="server"  OnClick="btHelp_Click"  CausesValidation="false"><i class="icon-question-sign"></i></asp:LinkButton></span>
    <rsweb:ReportViewer ID="ReportViewer1"  Width="100%" Height="100%"  runat="server" BackColor="#D6DBE9" InternalBorderColor="ActiveBorder" SplitterBackColor="White" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="11pt" ZoomPercent="75" ></rsweb:ReportViewer>
    <asp:ScriptManager ID="ScriptManager_ActLog" runat="server"></asp:ScriptManager>
    </div>
      <div id="dialogHelp" style="display: none;" title="Help">
        <iframe style="border:none" width="100%" height="100%" src="../Documentation/Bus_Standard_Report.aspx"></iframe>
    </div>
</asp:Content>
