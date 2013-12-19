using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum OperationsEnum
    {
        [DataMember]
        Ping = 1,
        [DataMember]
        Connect = 2,
        [DataMember]
        GetLog = 3,
        [DataMember]
        ConnectDataBase = 4,
        [DataMember]
        InsertTodataBase = 5,

    }
}
