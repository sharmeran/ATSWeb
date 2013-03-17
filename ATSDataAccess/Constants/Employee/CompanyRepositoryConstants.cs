using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
   public static class CompanyRepositoryConstants
    {
       public const string ID = CommonConstants.Post_SP + CompanyConstants.ID;
       public const string Name = CommonConstants.Post_SP + CompanyConstants.Name;
       public const string Description = CommonConstants.Post_SP + CompanyConstants.Description;
       public const string Address = CommonConstants.Post_SP + CompanyConstants.Address;
       public const string Phone = CommonConstants.Post_SP + CompanyConstants.Phone;


       public const string Insert = "COMPANIES_INSERT";
       public const string Update = "COMPANIES_UPDATE";
       public const string Delete = "COMPANIES_DELETE";
       public const string FindAll = "COMPANIES_FIND_ALL";
       public const string FindByID = "COMPANIES_FIND_BY_ID";
    }
}
