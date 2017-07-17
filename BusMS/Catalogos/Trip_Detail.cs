using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Trip_Detail
    {
        public Trip_Detail()
        {
                
        }
        int trip_hrd_id;

        public int Trip_Hrd_ID
        {
            get { return trip_hrd_id; }
            set { trip_hrd_id = value; }
        }
        int sequence;

        public int Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }

        TimeSpan trip_start;

        public TimeSpan Trip_Start
        {
            get { return trip_start; }
            set { trip_start = value; }
        }
        TimeSpan trip_end;

        public TimeSpan Trip_End
        {
            get { return trip_end; }
            set { trip_end = value; }
        }
        int psg_init;

        public int Psg_Init
        {
            get { return psg_init; }
            set { psg_init = value; }
        }
        int psg_audit;

        public int Psg_Audit
        {
            get { return psg_audit; }
            set { psg_audit = value; }
        }
        Boolean end_point;

        public Boolean End_Point
        {
            get { return end_point; }
            set { end_point = value; }
        }

        string trip_id_transbord;

        public string Trip_ID_Transbord
        {
            get { return trip_id_transbord; }
            set { trip_id_transbord = value; }
        }

        string comment; 

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        string stop_point_ID;

        public string Stop_point_ID
        {
            get { return stop_point_ID; }
            set { stop_point_ID = value; }
        }

    }
}