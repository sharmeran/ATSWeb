using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSDataAccess.Constants.Shifts
{
   public static class ShiftRepositoryConstants
    {
       public const string Delete = "SHIFTS_DELETE";
       public const string Insert = "SHIFTS_INSERT";
       public const string Update = "SHIFTS_UPDATE";
       public const string FindAll = "SHIFTS_FIND_ALL";
       public const string FindByID = "SHIFTS_FIND_BY_ID";

       public const string FindByDeptCode = "SHIFTS_FIND_BY_DEPT_CODE";       
       public const string FindByUserID = "SHIFTS_FIND_BY_USERID";
       public const string FindByUserIDAndDate = "SHIFTS_FIND_BY_USERID_DATE";
       public const string FindDefaultByUserID = "SHIFTS_FIND_DEFAULT_BY_USERID";

    }
}
    