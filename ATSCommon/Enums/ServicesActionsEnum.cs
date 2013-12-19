using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum ServicesActionsEnum
    {
        [DataMember]
        StartService = 1,
        [DataMember]
        StopService = 2,
        [DataMember]
        StartTimer = 3,
        [DataMember]
        EndTimer = 4,
        [DataMember]
        StartOperation = 5,
        [DataMember]
        EndOperation = 6,
        [DataMember]
        Error = 7,
        [DataMember]
        ServiceBussy = 8
    }
}
