using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATSWebServices.Common
{
    public class EntityListResult<T>
    {
        public List<T> ReturnedEntities { get; set; }
        public string Message { get; set; }
    }
}