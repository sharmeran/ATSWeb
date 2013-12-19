using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum ErrorTypeEnum
    {
        [DataMember]
        Error = 1,
        [DataMember]
        Warning = 2,
        [DataMember]
        Information = 3
    }
}
