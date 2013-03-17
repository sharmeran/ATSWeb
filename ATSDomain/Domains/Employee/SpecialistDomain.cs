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
    public class SpecialistDomain : BusinessDomainBase<Specialist>
    {
        public SpecialistDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new SpecialistRepository();
        }

        public override void Add(Specialist entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Specialist entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Specialist entity)
        {
            throw new NotImplementedException();
        }

        public override List<Specialist> FindAll()
        {
           return DBRepository.FindAll(ActionState);
        }

        public override Specialist FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }
    }
}
