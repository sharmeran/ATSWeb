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
    public class QualificationsDomain : BusinessDomainBase<Qualifications>
    {
        public QualificationsDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new QualificationsRepository();
        }

        public override void Add(Qualifications entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Qualifications entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Qualifications entity)
        {
            throw new NotImplementedException();
        }

        public override List<Qualifications> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override Qualifications FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }
    }
}
