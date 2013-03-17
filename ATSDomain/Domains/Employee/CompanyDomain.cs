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
    public class CompanyDomain : BusinessDomainBase<Company>
    {
        public CompanyDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new CompanyRepository();
        }

        public override void Add(Company entity)
        {
            DBRepository.Insert(entity, ActionState);
        }

        public override void Delete(Company entity)
        {
            DBRepository.Delete(entity, ActionState);
        }

        public override void Update(Company entity)
        {
            DBRepository.Update(entity, ActionState);
        }

        public override List<Company> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override Company FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }
    }
}
