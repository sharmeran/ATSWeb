using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Enums;

namespace ATSCommon.Entites.Logs
{
    public class GeneralLog : BaseClass
    {
        Int64 iD;
        string errorName;
        string errorDescription;
        LogLevelEnum errorType;
        DateTime errorDate;
        OperationSourceEnum errorTarget;

        public OperationSourceEnum ErrorTarget
        {
            get { return errorTarget; }
            set { errorTarget = value; }
        }

        public DateTime ErrorDate
        {
            get { return errorDate; }
            set { errorDate = value; }
        }

        public LogLevelEnum ErrorType
        {
            get { return errorType; }
            set { errorType = value; }
        }

        public string ErrorDescription
        {
            get { return errorDescription; }
            set { errorDescription = value; }
        }

        public string ErrorName
        {
            get { return errorName; }
            set { errorName = value; }
        }
        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
