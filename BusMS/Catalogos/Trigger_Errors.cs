using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Trigger_Errors
    {
        public Trigger_Errors()
        {

        }

        private int trigger_Error_ID;

        public int Trigger_Error_ID
        {
            get { return trigger_Error_ID; }
            set { trigger_Error_ID = value; }
        }

        private string error_Spanish;

        public string Error_Spanish
        {
            get { return error_Spanish; }
            set { error_Spanish = value; }
        }
        private string error_English;

        public string Error_English
        {
            get { return error_English; }
            set { error_English = value; }
        }
    }
}