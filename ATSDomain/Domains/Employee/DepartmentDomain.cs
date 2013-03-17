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
    public class DepartmentDomain : BusinessDomainBase<Department>
    {
        public DepartmentDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new DepartmentRepository();
        }

        public override void Add(Department entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Department entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Department entity)
        {
            throw new NotImplementedException();
        }

        public override List<Department> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override Department FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }

        public List<Department> FindStartWith(string deptCode)
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();
            return departmentRepository.FindStartWith(deptCode, ActionState);
        }
    }
}
