using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Devices;
using ATSCommon.Entites.Devices;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.Devices;
using Oracle.DataAccess.Client;

namespace ATSDataAccess.SQLImlementation.Devices
{
   public class DeviceCategoryRepository : RepositoryBaseClass<DeviceCategory>
    {
        public override void Delete(DeviceCategory entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceCategoryRepositoryConstants.Delete, con);
                con.Open();
                com.CommandType = CommandType.StoredProcedure;

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Int32;
                uniqueCodeParameter.Value = entity.ID;
                com.Parameters.Add(uniqueCodeParameter);

                com.ExecuteNonQuery();
                actionState.SetSuccess();


            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);

            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();

            }
        }

        public override void Insert(DeviceCategory entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceCategoryRepositoryConstants.Insert, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter iPParameter = new OracleParameter();
                iPParameter.Direction = ParameterDirection.Input;
                iPParameter.OracleDbType = OracleDbType.Varchar2;
                iPParameter.Value = entity.Name;
                com.Parameters.Add(iPParameter);

                OracleParameter descriptionParamete = new OracleParameter();
                descriptionParamete.Direction = ParameterDirection.Input;
                descriptionParamete.OracleDbType = OracleDbType.Varchar2;
                descriptionParamete.Value = entity.Description;
                com.Parameters.Add(descriptionParamete); 

                com.ExecuteNonQuery();                
                actionState.SetSuccess();

            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);

            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();

            }
        }

        public override void Update(DeviceCategory entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceCategoryRepositoryConstants.Update, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter iDParameter = new OracleParameter();
                iDParameter.Direction = ParameterDirection.Input;
                iDParameter.OracleDbType = OracleDbType.Int32;
                iDParameter.Value = entity.ID;
                com.Parameters.Add(iDParameter);

                OracleParameter iPParameter = new OracleParameter();
                iPParameter.Direction = ParameterDirection.Input;
                iPParameter.OracleDbType = OracleDbType.Varchar2;
                iPParameter.Value = entity.Name;
                com.Parameters.Add(iPParameter);

                OracleParameter descriptionParamete = new OracleParameter();
                descriptionParamete.Direction = ParameterDirection.Input;
                descriptionParamete.OracleDbType = OracleDbType.Varchar2;
                descriptionParamete.Value = entity.Description;
                com.Parameters.Add(descriptionParamete);

                com.ExecuteNonQuery();
                actionState.SetSuccess();


            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);

            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();

            }
        }

        public override List<DeviceCategory> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<DeviceCategory> deviceCategoryList = new List<DeviceCategory>();
            DeviceCategory deviceCategory = new DeviceCategory();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceCategoryRepositoryConstants.FindAll, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        deviceCategory = DeviceCategoryHelper(reader, true);
                        if (deviceCategory != null)
                        {
                            deviceCategoryList.Add(deviceCategory);
                        }
                    }
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ATSCommon.Enums.ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();
            }
            return deviceCategoryList;
        }

        public override DeviceCategory FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            DeviceCategory DeviceCategory = new DeviceCategory();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceCategoryRepositoryConstants.FindByID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Int32;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (OracleDataReader reader = ((OracleDataReader)(com.ExecuteReader())))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        DeviceCategory = DeviceCategoryHelper(reader, false);
                    }
                    else
                    {
                        DeviceCategory = null;
                    }


                }
                actionState.SetSuccess();

            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);

            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();

            }
            return DeviceCategory;
        }

        private DeviceCategory DeviceCategoryHelper(OracleDataReader reader, bool isFull)
        {
            DeviceCategory deviceCategory = new DeviceCategory();
            deviceCategory.ID = Convert.ToInt32(reader[DeviceCategoryConstants.ID]);
            deviceCategory.Name = reader[DeviceCategoryConstants.Name].ToString();
            deviceCategory.Description = reader[DeviceCategoryConstants.Description].ToString();
            if (isFull)
            {
                DeviceRepository deviceRepository = new DeviceRepository();
                deviceCategory.DeviceList = deviceRepository.FindByDeviceCategory(deviceCategory.ID, new ATSCommon.ActionState());
            }
            return deviceCategory;
        }
    }
}
