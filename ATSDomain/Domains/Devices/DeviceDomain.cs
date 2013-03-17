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
    public class DeviceDomain : BusinessDomainBase<Device>
    {
        public DeviceDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new DeviceRepository();
        }

        public override void Add(Device entity)
        {
            DBRepository.Insert(entity, ActionState);
        }

        public override void Delete(Device entity)
        {
            DBRepository.Insert(entity, ActionState);
        }

        public override void Update(Device entity)
        {
            DBRepository.Insert(entity, ActionState);
        }

        public override List<Device> FindAll()
        {
           return DBRepository.FindAll(ActionState);
        }

        public override Device FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }

        public List<Device> FindByServerID(int serverID)
        {
            DeviceRepository deviceRepository = new DeviceRepository();
            return deviceRepository.FindByServer(serverID, ActionState);
        }

        public List<Device> FindByCategoryID(int categoryID)
        {
            DeviceRepository deviceRepository = new DeviceRepository();
            return deviceRepository.FindByDeviceCategory(categoryID, ActionState);
        }
    }
}
