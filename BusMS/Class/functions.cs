using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Collections;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem;


using BusManagementSystem.Class;
using BusManagementSystem.Catalogos;
using BusManagementSystem._01Catalogos;







/// <summary>
/// Summary description for functions
/// </summary>
public class functions : System.Web.UI.Page
{
    /// <summary>
    /// Convert string into SHA1 encription
    /// </summary>
    /// <param name="str">string to encript</param>
    /// <returns>string encripted</returns>
    public static string GetSHA1(string str)
    {

        SHA1 sha1 = SHA1Managed.Create();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] stream = null;
        System.Text.StringBuilder sb = new StringBuilder();
        stream = sha1.ComputeHash(encoding.GetBytes(str));
        for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
        str = sb.ToString();
        return sb.ToString();


    }

    /// <summary>
    /// Verify if element is marked as block
    /// </summary>
    /// <param name="gvPage">gridview to review data</param>
    /// <param name="pg">Page where to look up</param>
    /// <param name="index">column index to evaluate</param>
    static public void verifyIfBlock(GridView gvPage, Page pg, int index)
    {

        BusManagementSystem.MasterPage master = (BusManagementSystem.MasterPage)pg.Master;
        master.setPanelInfoVisible(false);
        foreach (GridViewRow row in gvPage.Rows)
        {
            if (((CheckBox)gvPage.Rows[row.RowIndex].Cells[index].Controls[0]).Checked)
            {
                master.setPanelInfoVisible(true);
                break;
            }
        }
    }

    /// <summary>
    /// Verify if element is marked as inactive
    /// </summary>
    /// <param name="gvPage">gridview to review data</param>
    /// <param name="pg">Page where to look up</param>
    /// <param name="index">column index to evaluate</param>
    static public void verifyIfInactive(GridView gvPage, Page pg, int index)
    {

        BusManagementSystem.MasterPage master = (BusManagementSystem.MasterPage)pg.Master;
        master.setPanelInfoVisible(false);
        foreach (GridViewRow row in gvPage.Rows)
        {
            if (((CheckBox)gvPage.Rows[row.RowIndex].Cells[index].Controls[0]).Checked == false)
            {
                master.setPanelInfoVisible(true);
                break;
            }
        }
    }

    public static void InsertError(string module, string statement, string description)
    {
        try
        {

            System_Error_Log error = new System_Error_Log();
            error.Module = module;
            error.Statement = statement;
            error.Description = description;
            GenericClass.SQLInsertObj(error);
        }
        //Do Nothing
        catch (Exception)
        {


        }
    }

    static public void ShowMessage(Page getPage, Type getType, string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(getPage, getType, System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }
    static public void changeTheme(Page getPage, Type getType, string color)
    {
        ScriptManager.RegisterStartupScript(getPage, getType, "Script", "themeColorChanger('" + color + "');", true);

    }
    static public string addPound(string color)
    {
        string returnValue = null;
        if (color.Contains("#"))
        {
            returnValue = color;
        }
        else
        {
            returnValue = "#" + color;
        }


        return returnValue;
    }
    static public String GetLanguage(object user)
    {
        string value = null;

        try
        {           
            Users usr = (Users)user;

            DataTable GetTLanguage = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");
            if (GetTLanguage.Rows.Count != 0)
            {

                value = GetTLanguage.Rows[0]["Configuration_Value"].ToString();
                if (value == "ES")
                {
                    value = "Esp";
                }
                else
                {

                    value = "Eng";
                }

            }
            else
            {
                value = "Esp";

            }

        }
        catch (Exception)
        {
            
            
        }
        return value;
    }
    protected void TranslateEachControl(string ResourceName, Control insideControl)
    {
        try
        {

            if (insideControl.HasControls())
            {
                FindControls(ResourceName, insideControl);
            }

            if (insideControl is RequiredFieldValidator)
            {

                ((RequiredFieldValidator)insideControl).ErrorMessage = (string)GetGlobalResourceObject(ResourceName, ((RequiredFieldValidator)insideControl).ID) == null
                    ? ((RequiredFieldValidator)insideControl).ErrorMessage : (string)GetGlobalResourceObject(ResourceName, ((RequiredFieldValidator)insideControl).ID);

            }
            else if (insideControl is CompareValidator)
            {

                ((CompareValidator)insideControl).ErrorMessage = (string)GetGlobalResourceObject(ResourceName, ((CompareValidator)insideControl).ID) == null
                    ? ((CompareValidator)insideControl).ErrorMessage : (string)GetGlobalResourceObject(ResourceName, ((CompareValidator)insideControl).ID);

            }
            else if (insideControl is ValidationSummary)
            {

                ((ValidationSummary)insideControl).HeaderText = (string)GetGlobalResourceObject(ResourceName, ((ValidationSummary)insideControl).ID) == null
                    ? ((ValidationSummary)insideControl).HeaderText : (string)GetGlobalResourceObject(ResourceName, ((ValidationSummary)insideControl).ID);

            }
            else if (insideControl is RegularExpressionValidator)
            {

                ((RegularExpressionValidator)insideControl).ErrorMessage = (string)GetGlobalResourceObject(ResourceName, ((RegularExpressionValidator)insideControl).ID) == null
                    ? ((RegularExpressionValidator)insideControl).ErrorMessage : (string)GetGlobalResourceObject(ResourceName, ((RegularExpressionValidator)insideControl).ID);

            }
            else if (insideControl is Label)
            {

                ((Label)insideControl).Text = (string)GetGlobalResourceObject(ResourceName, ((Label)insideControl).ID) == null
                        ? ((Label)insideControl).Text : (string)GetGlobalResourceObject(ResourceName, ((Label)insideControl).ID);

            }
            else if (insideControl is Button)
            {
                ((Button)insideControl).Text = (string)GetGlobalResourceObject(ResourceName, ((Button)insideControl).ID) == null
                           ? ((Button)insideControl).Text : (string)GetGlobalResourceObject(ResourceName, ((Button)insideControl).ID);
            }
            else if (insideControl is CheckBox)
            {
                ((CheckBox)insideControl).Text = (string)GetGlobalResourceObject(ResourceName, ((CheckBox)insideControl).ID) == null
                           ? ((CheckBox)insideControl).Text : (string)GetGlobalResourceObject(ResourceName, ((CheckBox)insideControl).ID);
            }
        }
        catch (Exception)
        {

            
        }
    }
    protected void FindControls(string ResourceName, Control content)
    {
        foreach (Control insideControl in content.Controls)
        {

                TranslateEachControl(ResourceName, insideControl);          
            
        }
    }

    protected void FindControls(string ResourceName, ContentPlaceHolder content)
    {
        foreach (Control insideControl in content.Controls)
        {
                TranslateEachControl(ResourceName, insideControl);
        }
    }

    protected void FindControls(string ResourceName, Repeater content)
    {
        foreach (RepeaterItem item in content.Items)
        {
            foreach (Control insideControl in item.Controls)
            {
                    TranslateEachControl(ResourceName, insideControl);
            }
        }
    }

    public void languageTranslate(System.Web.UI.MasterPage page, string ResourceName)
    {
        try
        {
            ContentPlaceHolder mpContentPlaceHolder;

            mpContentPlaceHolder = (ContentPlaceHolder)page.FindControl("ContentPlaceHolder1");

            Repeater rControl = (Repeater)page.FindControl("Repeater1");

            if (rControl != null)
            {

                FindControls(ResourceName, rControl);
            }

            if (mpContentPlaceHolder != null)
            {

                FindControls(ResourceName, mpContentPlaceHolder);

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(Page, Page.GetType(), msg, MessageType.Error);

        }
    }

    public void SelectDropdownList(DropDownList ddlToBeSelected, string txtToSearch)
    {
        int row = 0;

        ListItem itemFinded = ddlToBeSelected.Items.FindByText(txtToSearch);
        row = ddlToBeSelected.Items.IndexOf(itemFinded);
        ddlToBeSelected.SelectedIndex = row;
    }



}