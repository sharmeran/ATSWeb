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
    public class TitleDomain : BusinessDomainBase<Title>
    {
        public TitleDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new TitleRepository();
        }

        public override void Add(Title entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Title entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Title entity)
        {
            throw new NotImplementedException();
        }

        public override List<Title> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override Title FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }
    }
}
