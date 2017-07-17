using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Revision_Note
    {
        int revision_note_id;

        public int Revision_note_id
        {
            get { return revision_note_id; }
            set { revision_note_id = value; }
        }
        int legal_revision_id;

        public int Legal_revision_id
        {
            get { return legal_revision_id; }
            set { legal_revision_id = value; }
        }
        int revision_case_id;

        public int Revision_case_id
        {
            get { return revision_case_id; }
            set { revision_case_id = value; }
        }
        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        Boolean is_checked;

        public Boolean Is_Checked
        {
            get { return is_checked; }
            set { is_checked = value; }
        }
        DateTime proposal_date;

        public DateTime Proposal_date
        {
            get { return proposal_date; }
            set { proposal_date = value; }
        }
        string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
    }
}