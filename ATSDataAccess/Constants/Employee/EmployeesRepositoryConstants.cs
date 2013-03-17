using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSDataAccess.Constants.Employee
{
    public static class EmployeesRepositoryConstants
    {
        public const string FindAll = "EMPLOYEE_FIND_ALL";
        public const string FindByManagerDeptCode = "EMPLOYEE_FIND_BY_DEPT_CODE";
        public const string FindByDirectManagerID = "EMPLOYEE_FIND_BY_MANAGER_ID";
        public const string FindByPersonnelID = "EMPLOYEE_FIND_BY_PERSONNEL_ID";
    }
}
