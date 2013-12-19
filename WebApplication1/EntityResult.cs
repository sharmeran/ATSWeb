using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
  
    public class EntityResult<T>
    {
       
        public T ReturnedEntity { get; set; }
        
        public string Message { get; set; }
    }
}