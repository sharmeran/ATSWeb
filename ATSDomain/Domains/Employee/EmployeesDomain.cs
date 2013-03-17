using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Entites.Employee;
using ATSCommon.Enums;
using ATSDataAccess.SQLImlementation.Employee;
using ATSDomain.BaseClasses;

namespace ATSDomain.Domains.Employee
{
    public class EmployeesDomain : BusinessDomainBase<Employees>
    {
        public EmployeesDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new EmployeesRepository();
        }

        public override void Add(Employees entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Employees entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Employees entity)
        {
            throw new NotImplementedException();
        }

        public override List<Employees> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override Employees FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }

        public List<Employees> FindByManagerID(int managerID)
        {
            EmployeesRepository employeesRepository = new EmployeesRepository();
            return employeesRepository.FindByManagerDirectCode(managerID, ActionState);
        }

        public List<Employees> FindByDeptCode(string deptCode)
        {
            EmployeesRepository employeesRepository = new EmployeesRepository();
            return employeesRepository.FindByDeptCode(deptCode, ActionState);
        }

    }
}
