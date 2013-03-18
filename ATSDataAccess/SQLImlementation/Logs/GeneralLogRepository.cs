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
using ATSCommon.Entites.Logs;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.Logs;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ATSDataAccess.SQLImlementation.Logs
{
    public class GeneralLogRepository : RepositoryBaseClass<GeneralLog>
    {
        public override void Delete(GeneralLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(GeneralLogRepositoryConstants.Delete);

                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ID, DbType.Int32, entity.ID);


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

        public override void Insert(GeneralLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(GeneralLogRepositoryConstants.Insert);

                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorDate, DbType.DateTime, entity.ErrorDate);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorDescription, DbType.String, entity.ErrorDescription);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorName, DbType.String, entity.ErrorName);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorTarget, DbType.Int32, Convert.ToInt32(entity.ErrorTarget));
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorType, DbType.Int32, Convert.ToInt32(entity.ErrorType));


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

        public override void Update(GeneralLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(GeneralLogRepositoryConstants.Update);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ID, DbType.Int32, entity.ID);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorDate, DbType.DateTime, entity.ErrorDate);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorDescription, DbType.String, entity.ErrorDescription);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorName, DbType.String, entity.ErrorName);
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorTarget, DbType.Int32, Convert.ToInt32(entity.ErrorTarget));
                database.AddInParameter(cmd, GeneralLogRepositoryConstants.ErrorType, DbType.Int32, Convert.ToInt32(entity.ErrorType));

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

        public override List<GeneralLog> FindAll(ActionState actionState)
        {
            List<GeneralLog> generalLogList;
            GeneralLog generalLog;
            DbCommand cmd;

            generalLogList = new List<GeneralLog>();
            generalLog = null;

            try
            {
                cmd = database.GetStoredProcCommand(GeneralLogRepositoryConstants.FindAll);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        generalLog = GeneralLogHelper(reader);
                        if (generalLog != null)
                        {
                            generalLogList.Add(generalLog);
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
            return generalLogList;
        }

        public override GeneralLog FindByID(int entityID, ActionState actionState)
        {
            throw new NotImplementedException();
        }

      
        public GeneralLog GeneralLogHelper(SqlDataReader reader)
        {
            GeneralLog generalLog = new GeneralLog();
            generalLog.ErrorDate = Convert.ToDateTime(reader[GeneralLogConstants.ErrorDate]);
            generalLog.ErrorDescription = reader[GeneralLogConstants.ErrorDescription].ToString();
            generalLog.ErrorName = reader[GeneralLogConstants.ErrorName].ToString();
            generalLog.ErrorType = (LogLevelEnum)Convert.ToInt32(reader[GeneralLogConstants.ErrorType]);
            generalLog.ErrorTarget = (OperationSourceEnum)Convert.ToInt32(reader[GeneralLogConstants.ErrorTarget]);
            return generalLog;
        }
    }
}
