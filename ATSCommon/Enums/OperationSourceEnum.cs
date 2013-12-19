using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum OperationSourceEnum
    {
        [DataMember]
        OracleDB = 1,
        [DataMember]
        InternalDB = 2,
        [DataMember]
        XmlFile = 3,
        [DataMember]
        HPDevice = 4,
        [DataMember]
        Others = 5
    }
}
