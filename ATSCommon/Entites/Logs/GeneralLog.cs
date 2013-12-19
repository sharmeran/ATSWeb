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
    public class GeneralLog : BaseClass
    {
        Int64 iD;
        string errorName;
        string errorDescription;
        LogLevelEnum errorType;
        DateTime errorDate;
        OperationSourceEnum errorTarget;
        [DataMember]
        public OperationSourceEnum ErrorTarget
        {
            get { return errorTarget; }
            set { errorTarget = value; }
        }
        [DataMember]
        public DateTime ErrorDate
        {
            get { return errorDate; }
            set { errorDate = value; }
        }
        [DataMember]
        public LogLevelEnum ErrorType
        {
            get { return errorType; }
            set { errorType = value; }
        }
        [DataMember]
        public string ErrorDescription
        {
            get { return errorDescription; }
            set { errorDescription = value; }
        }
        [DataMember]
        public string ErrorName
        {
            get { return errorName; }
            set { errorName = value; }
        }
        [DataMember]
        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
