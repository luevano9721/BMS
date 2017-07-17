using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace BusManagementSystem.Class
{
    public class ReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        private string userName;
        private string passWord;
        private string domainName;

        public ReportCredentials(string userName, string passWord, string domainName)
        {
            this.userName = userName;
            this.passWord = passWord;
            this.domainName = domainName;
        }

        #region IReportServerCredentials

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = password = authority = null;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            // not use ImpersonationUser
            get { return null; }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            // use NetworkCredentials
            get { return new NetworkCredential(userName, passWord, domainName); }
        }

        #endregion
    }
}