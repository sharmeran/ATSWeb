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
    /// Summary description for DeviceCategoryWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DeviceCategoryWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]        
        public EntityResult<DeviceCategory> Add(string name, string description)
        {
            DeviceCategory entity = new DeviceCategory();
            entity.Name = name;
            entity.Description = description;
            EntityResult<DeviceCategory> entityResult = new EntityResult<DeviceCategory>();
            DeviceCategoryDomain deviceCategoryDomain = new DeviceCategoryDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                deviceCategoryDomain.Add(entity);
                entityResult.ReturnedEntity = entity;
                if (deviceCategoryDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceCategoryDomain.ActionState.Result;
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
        public EntityResult<DeviceCategory> Delete(int id)
        {
            DeviceCategory entity = new DeviceCategory();
            entity.ID = id;            
            EntityResult<DeviceCategory> entityResult = new EntityResult<DeviceCategory>();
            DeviceCategoryDomain deviceCategoryDomain = new DeviceCategoryDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                deviceCategoryDomain.Delete(entity);
                entityResult.ReturnedEntity = entity;
                if (deviceCategoryDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceCategoryDomain.ActionState.Result;
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
        public EntityResult<DeviceCategory> Update(string name, string description, int id)
        {
            DeviceCategory entity = new DeviceCategory();
            entity.Name = name;
            entity.Description = description;
            entity.ID = id;
            EntityResult<DeviceCategory> entityResult = new EntityResult<DeviceCategory>();
            DeviceCategoryDomain deviceCategoryDomain = new DeviceCategoryDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                deviceCategoryDomain.Update(entity);
                entityResult.ReturnedEntity = entity;
                if (deviceCategoryDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceCategoryDomain.ActionState.Result;
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
        public EntityResult<DeviceCategory> FindByID(int entityID)
        {
            EntityResult<DeviceCategory> entityResult = new EntityResult<DeviceCategory>();
            DeviceCategoryDomain deviceCategoryDomain = new DeviceCategoryDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = deviceCategoryDomain.FindByID(entityID);
                if (deviceCategoryDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = deviceCategoryDomain.ActionState.Result;
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
        public EntityListResult<DeviceCategory> FindAll()
        {
            DeviceCategoryDomain deviceCategoryDomain = new DeviceCategoryDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<DeviceCategory> entityListResult = new EntityListResult<DeviceCategory>();
            try
            {
                entityListResult.ReturnedEntities = deviceCategoryDomain.FindAll();
                if (deviceCategoryDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = deviceCategoryDomain.ActionState.Result;
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
