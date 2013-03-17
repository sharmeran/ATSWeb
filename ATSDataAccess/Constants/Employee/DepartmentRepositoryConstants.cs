using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
   public static class DepartmentRepositoryConstants
    {
       public const string Code = CommonConstants.Post_SP + DepartmentConstants.Code;
       public const string Name = CommonConstants.Post_SP + DepartmentConstants.Name;
       public const string ShortName = CommonConstants.Post_SP + DepartmentConstants.ShortName;
       public const string ShortCode = CommonConstants.Post_SP + DepartmentConstants.ShortCode;


       public const string FindAll = "DEPARTMENT_FIND_ALL";
       public const string FindByID = "DEPARTMENT_FIND_BY_ID";


    }
}
