using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Enums;

namespace ATSCommon.Entites.Logs
{
    [DataContract]
    public class ServicesLog : BaseClass
    {
        Int64 iD;
        ServicesEnum service;
        ServicesActionsEnum action;
        DateTime loggingDate;
        string description;
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        [DataMember]
        public DateTime LoggingDate
        {
            get { return loggingDate; }
            set { loggingDate = value; }
        }
        [DataMember]
        public ServicesActionsEnum Action
        {
            get { return action; }
            set { action = value; }
        }
        [DataMember]
        public ServicesEnum Service
        {
            get { return service; }
            set { service = value; }
        }
        [DataMember]
        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }

    }
}
