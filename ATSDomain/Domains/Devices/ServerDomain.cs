using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Entites.Devices;
using ATSCommon.Enums;
using ATSDataAccess.SQLImlementation.Devices;
using ATSDomain.BaseClasses;

namespace ATSDomain.Domains.Devices
{
    public class ServerDomain : BusinessDomainBase<Server>
    {
        public ServerDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new ServerRepository();
        }

        public override void Add(Server entity)
        {
            DBRepository.Insert(entity, ActionState);
        }

        public override void Delete(Server entity)
        {
            DBRepository.Delete(entity, ActionState);
        }

        public override void Update(Server entity)
        {
            DBRepository.Update(entity, ActionState);
        }

        public override List<Server> FindAll()
        {
           return DBRepository.FindAll(ActionState);
        }

        public override Server FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }
    }
}
