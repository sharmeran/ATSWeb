using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum LanguagesEnum
    {
        [DataMember]
        English = 0,

        [DataMember]
        Arabic = 1,

        [DataMember]
        Spanish = 2,

        [DataMember]
        Russian = 3,

        [DataMember]
        French = 4,

        [DataMember]
        German = 5
    }
}
