using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Constants.UserLogs;
using ATSCommon.Entites.UserLogs;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.UserLogs;
using ATSDataAccess.SQLImlementation.Devices;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ATSDataAccess.SQLImlementation.UserLogs
{
    public class CheatUsersRepository : RepositoryBaseClass<CheatUsers>
    {
        public override void Delete(CheatUsers entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(CheatUsersRepositoryConstants.Delete);

                database.AddInParameter(cmd, CheatUsersRepositoryConstants.ID, DbType.Int32, entity.ID);


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

        public override void Insert(CheatUsers entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(CheatUsersRepositoryConstants.Insert);

                database.AddInParameter(cmd, CheatUsersRepositoryConstants.UserID, DbType.Int64, entity.UserID);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.LogingType, DbType.Int32, Convert.ToInt32(entity.LoggingType));
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.LogingDateTime, DbType.DateTime, entity.LoggingDate);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.DeviceID, DbType.Int32, entity.Device.ID);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.CatchDate, DbType.DateTime, entity.CatchDate);

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

        public override void Update(CheatUsers entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(CheatUsersRepositoryConstants.Update);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.ID, DbType.Int32, entity.ID);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.UserID, DbType.Int64, entity.UserID);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.LogingType, DbType.Int32, Convert.ToInt32(entity.LoggingType));
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.LogingDateTime, DbType.DateTime, entity.LoggingDate);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.DeviceID, DbType.DateTime, entity.Device.ID);
                database.AddInParameter(cmd, CheatUsersRepositoryConstants.CatchDate, DbType.DateTime, entity.CatchDate);

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

        public override List<CheatUsers> FindAll(ActionState actionState)
        {
            List<CheatUsers> cheatUsersList;
            CheatUsers cheatUsers;
            DbCommand cmd;

            cheatUsersList = new List<CheatUsers>();
            cheatUsers = null;

            try
            {
                cmd = database.GetStoredProcCommand(CheatUsersRepositoryConstants.FindAll);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        cheatUsers = CheatUsersHelper(reader);
                        if (cheatUsers != null)
                        {
                            cheatUsersList.Add(cheatUsers);
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
            return cheatUsersList;
        }

        public override CheatUsers FindByID(int entityID, ActionState actionState)
        {
            throw new NotImplementedException();
        }

      
        private CheatUsers CheatUsersHelper(SqlDataReader reader)
        {
            CheatUsers cheatUsers = new CheatUsers();
            cheatUsers.ID = Convert.ToInt64(reader[CheatUsersConstants.ID]);
            cheatUsers.UserID = Convert.ToInt64(reader[CheatUsersConstants.UserID]);
            cheatUsers.LoggingType = (UserLogTypeEnum)Convert.ToInt32(reader[CheatUsersConstants.LoggingType]);
            cheatUsers.LoggingDate = Convert.ToDateTime(reader[CheatUsersConstants.LogingDate]);
            DeviceRepository devicesRepository = new DeviceRepository();
            cheatUsers.Device = devicesRepository.FindByID(Convert.ToInt32(reader[CheatUsersConstants.DeviceID]), new ActionState());
            return cheatUsers;
        }
    }
}
