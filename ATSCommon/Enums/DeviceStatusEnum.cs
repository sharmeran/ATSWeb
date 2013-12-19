using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
   public enum DeviceStatusEnum
    {
       [DataMember]
         OnLine=1,
       [DataMember]
        OffLine=2,
       [DataMember]
        Bussy=3
    }
}
