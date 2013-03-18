using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Enums;

namespace ATSCommon.Entites.Logs
{
    public class ServicesLog : BaseClass
    {
        Int64 iD;
        ServicesEnum service;
        ServicesActionsEnum action;
        DateTime loggingDate;
        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime LoggingDate
        {
            get { return loggingDate; }
            set { loggingDate = value; }
        }

        public ServicesActionsEnum Action
        {
            get { return action; }
            set { action = value; }
        }

        public ServicesEnum Service
        {
            get { return service; }
            set { service = value; }
        }

        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }

    }
}
