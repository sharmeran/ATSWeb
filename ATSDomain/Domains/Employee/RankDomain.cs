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
    public class RankDomain : BusinessDomainBase<Rank>
    {
        public RankDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new RankRepository();
        }

        public override void Add(Rank entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Rank entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Rank entity)
        {
            throw new NotImplementedException();
        }

        public override List<Rank> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override Rank FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }

    }
}
