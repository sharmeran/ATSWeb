using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
   public static class CategoryRepositoryConstants
    {
       public const string FindAll = "CATIGORY_FIND_ALL";
       public const string FindByID = "CATIGORY_FIND_BY_ID";

       public const string ID = CommonConstants.Post_SP + CategoryConstants.ID;
      
    }
}
