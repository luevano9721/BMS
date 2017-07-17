using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Driver_Documents
    {
        int driver_Documents_ID;

        public int Driver_Documents_ID
        {
            get { return driver_Documents_ID; }
            set { driver_Documents_ID = value; }
        }
        int driver_ID;

        public int Driver_ID
        {
            get { return driver_ID; }
            set { driver_ID = value; }
        }
        string file_Name;

        public string File_Name
        {
            get { return file_Name; }
            set { file_Name = value; }
        }
        string file_Path;

        public string File_Path
        {
            get { return file_Path; }
            set { file_Path = value; }
        }

        string upload_by;

        public string Upload_by
        {
            get { return upload_by; }
            set { upload_by = value; }
        }
        DateTime upload_date;

        public DateTime Upload_date
        {
            get { return upload_date; }
            set { upload_date = value; }
        }

        string category_file;

        public string Category_file
        {
            get { return category_file; }
            set { category_file = value; }
        }
        string short_Path;

        public string Short_Path
        {
            get { return short_Path; }
            set { short_Path = value; }
        }

    }
}