using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Constants.Logs;
using ATSCommon.Entites.Devices;
using ATSCommon.Entites.Logs;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.Logs;
using ATSDataAccess.SQLImlementation.Devices;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ATSDataAccess.SQLImlementation.Logs
{
    public class DeviceLogRepository : RepositoryBaseClass<DeviceLog>
    {
        public override void Delete(DeviceLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.Delete);

                database.AddInParameter(cmd, DeviceLogRepositoryConstants.ID, DbType.Int32, entity.ID);


                spResult = database.ExecuteNonQuery(cmd);
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotInsert, CommonConstants.Err_CannotInsert);
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.CannotInsert, ex.Message);
            }
            finally
            {
                cmd = null;
            }
        }

        public override void Insert(DeviceLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.Insert);


                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceID, DbType.Int32, entity.DeviceID.ID);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceLog, DbType.String, entity.DeviceLogEntry);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceErrorCode, DbType.String, entity.DeviceErrorCode);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceErrorType, DbType.Int32, entity.DeviceErrorType);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceLoggingDate, DbType.DateTime, entity.DeviceLoggingDate);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.OperationID, DbType.Int32, entity.OperationID);

                spResult = database.ExecuteNonQuery(cmd);
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotInsert, CommonConstants.Err_CannotInsert);
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.CannotInsert, ex.Message);
            }
            finally
            {
                cmd = null;
            }
        }

        public override void Update(DeviceLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.Update);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.ID, DbType.Int32, entity.ID);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceID, DbType.Int32, entity.DeviceID.ID);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceLog, DbType.String, entity.DeviceLogEntry);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceErrorCode, DbType.String, entity.DeviceErrorCode);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceErrorType, DbType.Int32, entity.DeviceErrorType);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceLoggingDate, DbType.DateTime, entity.DeviceLoggingDate);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.OperationID, DbType.Int32, entity.OperationID);

                spResult = database.ExecuteNonQuery(cmd);
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotInsert, CommonConstants.Err_CannotInsert);
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.CannotInsert, ex.Message);
            }
            finally
            {
                cmd = null;
            }
        }

        public override List<DeviceLog> FindAll(ActionState actionState)
        {
            List<DeviceLog> deviceLogEntityList;
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            deviceLogEntityList = new List<DeviceLog>();
            deviceLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.FindAll);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        deviceLogEntity = DeviceLogHelper(reader);
                        if (deviceLogEntity != null)
                        {
                            deviceLogEntityList.Add(deviceLogEntity);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntityList;
        }

        public override DeviceLog FindByID(int entityID, ActionState actionState)
        {
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            // Initialization
            deviceLogEntity = null;
            cmd = null;

            // Implementation 
            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.FindByID);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.ID, DbType.Int32, entityID);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    if (!reader.HasRows)
                    {
                        actionState.SetFail(ActionStatusEnum.NotFound, CommonConstants.Err_CannotFound);
                    }
                    else
                    {
                        actionState.SetSuccess();
                        reader.Read();
                        deviceLogEntity = DeviceLogHelper(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntity;
        }

       

        public List<DeviceLog> FindByErrorType(LogLevelEnum errorType, ActionState actionState)
        {
            List<DeviceLog> deviceLogEntityList;
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            deviceLogEntityList = new List<DeviceLog>();
            deviceLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.FindByErrorType);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceErrorType, DbType.Int32, Convert.ToInt32(errorType));

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        deviceLogEntity = DeviceLogHelper(reader);
                        if (deviceLogEntity != null)
                        {
                            deviceLogEntityList.Add(deviceLogEntity);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntityList;
        }

        public List<DeviceLog> FindByDate(DateTime startDate, DateTime endDate, ActionState actionState)
        {
            List<DeviceLog> deviceLogEntityList;
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            deviceLogEntityList = new List<DeviceLog>();
            deviceLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.DeviceLogGetByDate);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.StartDate, DbType.DateTime, startDate);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.EndDate, DbType.DateTime, endDate);

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        deviceLogEntity = DeviceLogHelperLight(reader);
                        if (deviceLogEntity != null)
                        {
                            deviceLogEntityList.Add(deviceLogEntity);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntityList;
        }

        public List<DeviceLog> FindByDeviceIDAndDeviceErrorTypeAndOperationID(DeviceLog entity, ActionState actionState)
        {
            List<DeviceLog> deviceLogEntityList;
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            deviceLogEntityList = new List<DeviceLog>();
            deviceLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.FindByErrorType);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceErrorType, DbType.Int32, Convert.ToInt32(entity.DeviceErrorType));
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.OperationID, DbType.Int32, Convert.ToInt32(entity.OperationID));
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.DeviceID, DbType.Int32, entity.DeviceID);

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        deviceLogEntity = DeviceLogHelper(reader);
                        if (deviceLogEntity != null)
                        {
                            deviceLogEntityList.Add(deviceLogEntity);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntityList;
        }

        public List<DeviceLog> DeviceLogFindByIP(string IP, ActionState actionState)
        {
            List<DeviceLog> deviceLogEntityList;
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            deviceLogEntityList = new List<DeviceLog>();
            deviceLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.DeviceLogFindByIP);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.IPParameter, DbType.String, IP);


                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        deviceLogEntity = DeviceLogHelperLight(reader);
                        if (deviceLogEntity != null)
                        {
                            deviceLogEntityList.Add(deviceLogEntity);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntityList;
        }

        public List<DeviceLog> DeviceLogFindByIPAndDate(string IP, DateTime startDate, DateTime endDate, ActionState actionState)
        {
            List<DeviceLog> deviceLogEntityList;
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            deviceLogEntityList = new List<DeviceLog>();
            deviceLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.DeviceLogFindByIPAndDate);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.IPParameter, DbType.String, IP);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.StartDate, DbType.DateTime, startDate);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.EndDate, DbType.DateTime, endDate);

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        deviceLogEntity = DeviceLogHelperLight(reader);
                        if (deviceLogEntity != null)
                        {
                            deviceLogEntityList.Add(deviceLogEntity);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntityList;
        }

        public List<DeviceLog> DeviceLogFindByDeviceCategory(int deviceCategory, ActionState actionState)
        {
            List<DeviceLog> deviceLogEntityList;
            DeviceLog deviceLogEntity;
            DbCommand cmd;

            deviceLogEntityList = new List<DeviceLog>();
            deviceLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(DeviceLogRepositoryConstants.DeviceLogFindByDeviceCategory);
                database.AddInParameter(cmd, DeviceLogRepositoryConstants.CategoryID, DbType.Int32, deviceCategory);

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        deviceLogEntity = DeviceLogHelperLight(reader);
                        if (deviceLogEntity != null)
                        {
                            deviceLogEntityList.Add(deviceLogEntity);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return deviceLogEntityList;
        }

        private DeviceLog DeviceLogHelper(SqlDataReader reader)
        {
            DeviceLog deviceLog = new DeviceLog();
            deviceLog.ID = Convert.ToInt64(reader[DeviceLogConstants.ID]);
            DeviceRepository devicesRepository = new DeviceRepository();
            deviceLog.DeviceID = devicesRepository.FindByID(Convert.ToInt32(reader[DeviceLogConstants.DeviceID]), new ActionState());
            deviceLog.DeviceLogEntry = reader[DeviceLogConstants.DeviceLog].ToString();
            deviceLog.DeviceErrorCode = reader[DeviceLogConstants.DeviceErrorCode].ToString();
            deviceLog.DeviceErrorType = (LogLevelEnum)Convert.ToInt32(reader[DeviceLogConstants.DeviceErrorType]);
            deviceLog.DeviceLoggingDate = Convert.ToDateTime(reader[DeviceLogConstants.DeviceLoggingDate]);
            deviceLog.OperationID = (OperationsEnum)Convert.ToInt32(reader[DeviceLogConstants.OperationID]);
            return deviceLog;
        }

        private DeviceLog DeviceLogHelperLight(SqlDataReader reader)
        {
            DeviceLog deviceLog = new DeviceLog();
            deviceLog.ID = Convert.ToInt64(reader[DeviceLogConstants.ID]);
            deviceLog.DeviceID = new Device();
            deviceLog.DeviceID.IP = reader[DeviceLogRepositoryConstants.IP].ToString();
            deviceLog.DeviceID.Name = reader[DeviceLogRepositoryConstants.Name].ToString();
            deviceLog.DeviceLogEntry = reader[DeviceLogConstants.DeviceLog].ToString();
            deviceLog.DeviceErrorCode = reader[DeviceLogConstants.DeviceErrorCode].ToString();
            deviceLog.DeviceErrorType = (LogLevelEnum)Convert.ToInt32(reader[DeviceLogConstants.DeviceErrorType]);
            deviceLog.DeviceLoggingDate = Convert.ToDateTime(reader[DeviceLogConstants.DeviceLoggingDate]);
            deviceLog.OperationID = (OperationsEnum)Convert.ToInt32(reader[DeviceLogConstants.OperationID]);
            return deviceLog;
        }
    }
}
