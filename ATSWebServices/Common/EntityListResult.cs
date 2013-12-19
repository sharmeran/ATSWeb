using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ATSWebServices.Common
{
    [DataContract]
    public class EntityListResult<T>
    {
        [DataMember]
        public List<T> ReturnedEntities { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}