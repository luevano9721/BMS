using BusManagementSystem._01Catalogos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace BusManagementSystem.Class
{
    public class SendEmail
    {
        public static void mailSMTP(string emailTo, string body, string subject)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(Properties.Settings.Default.ServerSMTP, Properties.Settings.Default.ServerPort);

                smtpClient.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.UserEmail, Properties.Settings.Default.PassEmail);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = false;
                MailMessage mail = new MailMessage();

                //Setting From , To and CC
                mail.From = new MailAddress(Properties.Settings.Default.FromEmail, "BMS Sitio");
                mail.To.Add(new MailAddress(emailTo));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.CC.Add(new MailAddress("MyEmailID@gmail.com"));

                smtpClient.Send(mail);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public static bool RequestNewPassword(string user, string email)
        {
            try
            {
                string newPassword = Membership.GeneratePassword(18, 1);
                Users bmsUser = new Users();
                DataTable dt = new DataTable();

                Dictionary<string, dynamic> dicPass = new Dictionary<string, dynamic>();
                dicPass.Add("Password", functions.GetSHA1(newPassword));
                dicPass.Add("Reset_Password", true);
                dt = GenericClass.SQLUpdateObj(bmsUser, dicPass, WhereClause: "Where User_ID='" + user + "'");
                string body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("./EmailTemplates/resetPassBody.txt"));
                body = body.Replace("<userid>", user);
                body = body.Replace("<npass>", newPassword);
                mailSMTP(email, body, "BMS Reseteo de password");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool sendEmailToAdmin(string user , Admin_Approve aprobacion)
        {
             try
            {
            List<string> emails = new List<string>();
            Users usuario = new Users();
            DataTable dtAdmin = new DataTable();
            dtAdmin = GenericClass.SQLSelectObj(usuario, WhereClause: "Where Rol_ID='ADMIN'");
            foreach (DataRow row in dtAdmin.Rows)
            {
                emails.Add(row["Email"].ToString());
            }
            
                string body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/approveActivity.txt"));
                body = body.Replace("<userid>", user);
                body = body.Replace("<date>", aprobacion.Activity_Date.ToString());
                body = body.Replace("<tipo>", aprobacion.Type);
                body = body.Replace("<module>", aprobacion.Module);
                body = body.Replace("<oldvalues>", aprobacion.Old_Values);
                body = body.Replace("<newvalues>", aprobacion.New_Values);
                body = body.Replace("<comments>", aprobacion.Comments);
                foreach (string correo in emails)
                { 
                 mailSMTP(correo, body, "Modificación aprobada");
                }
               
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool StatusActivity(string usrID, string activityID, string comments, string admin, string status)
        {
            try
            {
                Users usr = new Users();
                DataTable dt = GenericClass.SQLSelectObj(usr, WhereClause: "Where User_ID='" + usrID + "'");
                string body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/statusAction.txt"));
                body = body.Replace("<userID>", dt.Rows[0]["User_ID"].ToString());
                body = body.Replace("<activityID>", activityID);
                body = body.Replace("<comments>", comments);
                body = body.Replace("<admin>", admin);
                body = body.Replace("<status>", status);
                mailSMTP(dt.Rows[0]["Email"].ToString(), body, "BMS Estatus de actividad");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static bool sendEmailToDistributionList(string VendorID, string Configuration_Name, Admin_Approve aprobacion, string user)
        {
            //Get  email list
            string body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/approveWaiting.txt"));
            body = body.Replace("<userid>", user);
            body = body.Replace("<date>", aprobacion.Activity_Date.ToString());
            body = body.Replace("<tipo>", aprobacion.Type);
            body = body.Replace("<module>", aprobacion.Module);
            body = body.Replace("<oldvalues>", aprobacion.Old_Values);
            body = body.Replace("<newvalues>", aprobacion.New_Values);
            body = body.Replace("<comments>", aprobacion.Comments);

            DataTable getList = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "WHERE Configuration_Name='" + Configuration_Name + "'" + " AND Vendor_ID='" + VendorID + "'");
            if (getList.Rows.Count > 0)
            {
                string emailList = getList.Rows[0]["CONFIGURATION_VALUE"].ToString();
                List<string> emails = validateAndGetSeparatedEmails(emailList);
                foreach (string correo in emails)
                {
                    mailSMTP(correo, body, "Nuevo movimiento en espera de aprobación");
                }

            }
            return true;
        }

        public static List<string> validateAndGetSeparatedEmails(string emailList)
        {
            List<string> list = new List<string>();
            string[] separatedEmails = emailList.Split(',');

            foreach (string e in separatedEmails)
            {
                string email = e.Trim();
                if (email.Length > 0) // if valid == false, no reason to continue checking
                {
                    // isnotblank = true;
                    if (IsValidEmailAddress(email))
                    {
                        list.Add(email);
                    }
                }
            }

            return list;
        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            var valid = true;
            var isnotblank = false;

            var email = emailAddress.Trim();
            if (email.Length > 0)
            {
                // Email Address Cannot start with period.
                // Name portion must be at least one character
                // In the Name, valid characters are:  a-z 0-9 ! # _ % & ' " = ` { } ~ - + * ? ^ | / $
                // Cannot have period immediately before @ sign.
                // Cannot have two @ symbols
                // In the domain, valid characters are: a-z 0-9 - .
                // Domain cannot start with a period or dash
                // Domain name must be 2 characters.. not more than 256 characters
                // Domain cannot end with a period or dash.
                // Domain must contain a period
                isnotblank = true;
                valid = Regex.IsMatch(email, @"\A([\w!#%&'""=`{}~\.\-\+\*\?\^\|\/\$])+@{1}\w+([-.]\w+)*\.\w+([-.]\w+)*\z", RegexOptions.IgnoreCase) &&
                    !email.StartsWith("-") &&
                    !email.StartsWith(".") &&
                    !email.EndsWith(".") &&
                    !email.Contains("..") &&
                    !email.Contains(".@") &&
                    !email.Contains("@.");
            }

            return (valid && isnotblank);
        }
    }
}