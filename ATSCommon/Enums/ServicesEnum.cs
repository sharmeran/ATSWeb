using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum ServicesEnum
    {
        [DataMember]
        HandPunchService = 1,
        [DataMember]
        LostUserService = 2,
        [DataMember]
        ClientService = 3
    }
}