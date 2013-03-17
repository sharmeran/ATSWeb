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
    public class DeviceCategoryDomain : BusinessDomainBase<DeviceCategory>
    {
        public DeviceCategoryDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new DeviceCategoryRepository();
        }

        public override void Add(DeviceCategory entity)
        {
            DBRepository.Insert(entity, ActionState);
        }

        public override void Update(DeviceCategory entity)
        {
            DBRepository.Update(entity, ActionState);
        }

        public override void Delete(DeviceCategory entity)
        {
            DBRepository.Delete(entity, ActionState);
        }

        public override List<DeviceCategory> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override DeviceCategory FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }

        
    }
}
