using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ATSCommon.Entites.Devices;
using ATSDomain.Domains.Devices;
using ATSWebServices.Common;

namespace ATSWebServices.Services.Devices
{
    /// <summary>
    /// Summary description for DeviceWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DeviceWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Device> Add(Device entity)
        {
            EntityResult<Device> entityResult = new EntityResult<Device>();
            DeviceDomain deviceDomain = new DeviceDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                deviceDomain.Add(entity);
                entityResult.ReturnedEntity = entity;
                if (deviceDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityResult.Message = ex.Message;
            }
            return entityResult;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Device> Delete(Device entity)
        {
            EntityResult<Device> entityResult = new EntityResult<Device>();
            DeviceDomain deviceDomain = new DeviceDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                deviceDomain.Delete(entity);
                entityResult.ReturnedEntity = entity;
                if (deviceDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityResult.Message = ex.Message;
            }
            return entityResult;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Device> Update(Device entity)
        {
            EntityResult<Device> entityResult = new EntityResult<Device>();
            DeviceDomain deviceDomain = new DeviceDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                deviceDomain.Update(entity);
                entityResult.ReturnedEntity = entity;
                if (deviceDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityResult.Message = ex.Message;
            }
            return entityResult;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Device> FindByID(int entityID)
        {
            EntityResult<Device> entityResult = new EntityResult<Device>();
            DeviceDomain deviceDomain = new DeviceDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = deviceDomain.FindByID(entityID);
                if (deviceDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityResult.Message = ex.Message;
            }
            return entityResult;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityListResult<Device> FindAll()
        {
            DeviceDomain DeviceDomain = new DeviceDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Device> entityListResult = new EntityListResult<Device>();
            try
            {
                entityListResult.ReturnedEntities = DeviceDomain.FindAll();
                if (DeviceDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = DeviceDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityListResult.Message = ex.Message;
            }
            return entityListResult;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityListResult<Device> FindByServerID(int serverID)
        {
            DeviceDomain DeviceDomain = new DeviceDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Device> entityListResult = new EntityListResult<Device>();
            try
            {
                entityListResult.ReturnedEntities = DeviceDomain.FindByServerID(serverID);
                if (DeviceDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = DeviceDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityListResult.Message = ex.Message;
            }
            return entityListResult;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityListResult<Device> FindByCategoryID(int categoryID)
        {
            DeviceDomain DeviceDomain = new DeviceDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Device> entityListResult = new EntityListResult<Device>();
            try
            {
                entityListResult.ReturnedEntities = DeviceDomain.FindByCategoryID(categoryID);
                if (DeviceDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = DeviceDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityListResult.Message = ex.Message;
            }
            return entityListResult;
        }

      
    }
}
