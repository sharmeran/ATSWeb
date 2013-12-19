using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum OperationTypeEnum
    {
        [DataMember]
        Add = 1,
        [DataMember]
        Update = 2,
        [DataMember]
        Delete = 3,
        [DataMember]
        View = 4
    }
}
