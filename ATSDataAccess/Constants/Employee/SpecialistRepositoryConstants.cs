using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
   public static class SpecialistRepositoryConstants
    {
       public const string ID = CommonConstants.Post_SP + SpecialistConstants.ID;
       public const string Name = CommonConstants.Post_SP + SpecialistConstants.Name;

       public const string FindAll = "SPECIALITIES_FIND_ALL";
       public const string FindByID = "SPECIALITIES_FIND_BY_ID";

    }
}
