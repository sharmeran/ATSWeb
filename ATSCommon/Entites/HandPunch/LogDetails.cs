using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.HandPunch
{
    [DataContract]
    public class LogDetails : BaseClass
    {
        int iD;
        int logMainID;
        DateTime operationDate;
        int operationType;
        bool isWrong;
        int deviceID;
        [DataMember]
        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }
        [DataMember]
        public bool IsWrong
        {
            get { return isWrong; }
            set { isWrong = value; }
        }
        [DataMember]
        public int OperationType
        {
            get { return operationType; }
            set { operationType = value; }
        }
        [DataMember]
        public DateTime OperationDate
        {
            get { return operationDate; }
            set { operationDate = value; }
        }
        [DataMember]
        public int LogMainID
        {
            get { return logMainID; }
            set { logMainID = value; }
        }
        [DataMember]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
