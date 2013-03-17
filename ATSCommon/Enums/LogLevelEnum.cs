using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum LogLevelEnum
    {
        [DataMember]
        Information = 0,

        [DataMember]
        Warning = 1,

        [DataMember]
        Error = 2,

        [DataMember]
        Critical = 3,
    }
}
