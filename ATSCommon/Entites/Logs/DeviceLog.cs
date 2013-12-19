using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Entites.Devices;
using ATSCommon.Enums;

namespace ATSCommon.Entites.Logs
{
    [DataContract]
    public class DeviceLog : BaseClass
    {
        Int64 iD;
        Device deviceID;
        string deviceLogEntry;
        string deviceErrorCode;
        LogLevelEnum deviceErrorType;
        DateTime deviceLoggingDate;
        OperationsEnum operationID;
        [DataMember]
        public OperationsEnum OperationID
        {
            get { return operationID; }
            set { operationID = value; }
        }
        [DataMember]
        public DateTime DeviceLoggingDate
        {
            get { return deviceLoggingDate; }
            set { deviceLoggingDate = value; }
        }
        [DataMember]
        public LogLevelEnum DeviceErrorType
        {
            get { return deviceErrorType; }
            set { deviceErrorType = value; }
        }
        [DataMember]
        public string DeviceErrorCode
        {
            get { return deviceErrorCode; }
            set { deviceErrorCode = value; }
        }
        [DataMember]
        public string DeviceLogEntry
        {
            get { return deviceLogEntry; }
            set { deviceLogEntry = value; }
        }
        [DataMember]
        public Device DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        [DataMember]
        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
