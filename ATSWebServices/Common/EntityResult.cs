using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ATSWebServices.Common
{
    [DataContract]
    public class EntityResult<T>
    {
        [DataMember]
        public T ReturnedEntity { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}