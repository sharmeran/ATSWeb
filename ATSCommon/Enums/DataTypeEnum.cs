using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum DataTypeEnum
    {
        [DataMember]
        Int32 = 1,
        [DataMember]
        Int64 = 2,
        [DataMember]
        String = 3,
        [DataMember]
        Boolean = 4,
        [DataMember]
        Double = 5,
        [DataMember]
        Float = 6
    }
}
