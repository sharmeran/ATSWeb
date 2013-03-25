using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Shifts;
using ATSCommon.Entites.Shifts;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Shifts;
using Oracle.DataAccess.Client;

namespace ATSDataAccess.SQLImlementation.Shifts
{
    public class ShiftRepository : RepositoryBaseClass<Shift>
    {
        public override void Delete(Shift entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.Delete, con);
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

        public override void Insert(Shift entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.Insert, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.Value = entity.Name;
                nameParameter.OracleDbType = OracleDbType.Varchar2;
                com.Parameters.Add(nameParameter);

                OracleParameter description = new OracleParameter();
                description.Direction = ParameterDirection.Input;
                description.Value = entity.Description;
                description.OracleDbType = OracleDbType.Varchar2;
                com.Parameters.Add(description);

                OracleParameter inTime = new OracleParameter();
                inTime.Direction = ParameterDirection.Input;
                inTime.Value = entity.InTime;
                inTime.OracleDbType = OracleDbType.Date;
                com.Parameters.Add(inTime);

                OracleParameter outTime = new OracleParameter();
                outTime.Direction = ParameterDirection.Input;
                outTime.Value = entity.OutTime;
                outTime.OracleDbType = OracleDbType.Date;
                com.Parameters.Add(outTime);

                OracleParameter inAllowTime = new OracleParameter();
                inAllowTime.Direction = ParameterDirection.Input;
                inAllowTime.Value = entity.InAllowTime;
                inAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(inAllowTime);

                OracleParameter outAllowTime = new OracleParameter();
                outAllowTime.Direction = ParameterDirection.Input;
                outAllowTime.Value = entity.OutAllowTime;
                outAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(outAllowTime);

                OracleParameter deptCode = new OracleParameter();
                deptCode.Direction = ParameterDirection.Input;
                deptCode.Value = entity.DeptCode;
                deptCode.OracleDbType = OracleDbType.Long;
                com.Parameters.Add(deptCode);

                OracleParameter dayID = new OracleParameter();
                dayID.Direction = ParameterDirection.Input;
                dayID.Value = entity.DayID;
                dayID.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(dayID);

                OracleParameter isFormalVacations = new OracleParameter();
                isFormalVacations.Direction = ParameterDirection.Input;
                isFormalVacations.Value =Convert.ToInt32( entity.IsFormalVacations);
                isFormalVacations.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(isFormalVacations);


                OracleParameter isNormalShift = new OracleParameter();
                isNormalShift.Direction = ParameterDirection.Input;
                isNormalShift.Value =Convert.ToInt32( entity.IsNormalShift);
                isNormalShift.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(isNormalShift);

                OracleParameter maxAllowTime = new OracleParameter();
                maxAllowTime.Direction = ParameterDirection.Input;
                maxAllowTime.Value = entity.MaxAllowWorkTime;
                maxAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(maxAllowTime);

                OracleParameter minAllowTime = new OracleParameter();
                minAllowTime.Direction = ParameterDirection.Input;
                minAllowTime.Value = entity.MinAllowWorkTime;
                minAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(minAllowTime);

                OracleParameter isOffDay = new OracleParameter();
                isOffDay.Direction = ParameterDirection.Input;
                isOffDay.Value =Convert.ToInt32( entity.IsOffDay);
                isOffDay.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(isOffDay);

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

        public override void Update(Shift entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.Update, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter iDParameter = new OracleParameter();
                iDParameter.Direction = ParameterDirection.Input;
                iDParameter.OracleDbType = OracleDbType.Int32;
                iDParameter.Value = entity.ID;
                com.Parameters.Add(iDParameter);

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.Value = entity.Name;
                nameParameter.OracleDbType = OracleDbType.Varchar2;
                com.Parameters.Add(nameParameter);

                OracleParameter description = new OracleParameter();
                description.Direction = ParameterDirection.Input;
                description.Value = entity.Description;
                description.OracleDbType = OracleDbType.Varchar2;
                com.Parameters.Add(description);

                OracleParameter inTime = new OracleParameter();
                inTime.Direction = ParameterDirection.Input;
                inTime.Value = entity.InTime;
                inTime.OracleDbType = OracleDbType.Date;
                com.Parameters.Add(inTime);

                OracleParameter outTime = new OracleParameter();
                outTime.Direction = ParameterDirection.Input;
                outTime.Value = entity.OutTime;
                outTime.OracleDbType = OracleDbType.Date;
                com.Parameters.Add(outTime);

                OracleParameter inAllowTime = new OracleParameter();
                inAllowTime.Direction = ParameterDirection.Input;
                inAllowTime.Value = entity.InAllowTime;
                inAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(inAllowTime);

                OracleParameter outAllowTime = new OracleParameter();
                outAllowTime.Direction = ParameterDirection.Input;
                outAllowTime.Value = entity.OutAllowTime;
                outAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(outAllowTime);

                OracleParameter deptCode = new OracleParameter();
                deptCode.Direction = ParameterDirection.Input;
                deptCode.Value = entity.DeptCode;
                deptCode.OracleDbType = OracleDbType.Long;
                com.Parameters.Add(deptCode);

                OracleParameter dayID = new OracleParameter();
                dayID.Direction = ParameterDirection.Input;
                dayID.Value = entity.DayID;
                dayID.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(dayID);

                OracleParameter isFormalVacations = new OracleParameter();
                isFormalVacations.Direction = ParameterDirection.Input;
                isFormalVacations.Value =Convert.ToInt32(entity.IsFormalVacations);
                isFormalVacations.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(isFormalVacations);


                OracleParameter isNormalShift = new OracleParameter();
                isNormalShift.Direction = ParameterDirection.Input;
                isNormalShift.Value =Convert.ToInt32(entity.IsNormalShift);
                isNormalShift.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(isNormalShift);

                OracleParameter maxAllowTime = new OracleParameter();
                maxAllowTime.Direction = ParameterDirection.Input;
                maxAllowTime.Value = entity.MaxAllowWorkTime;
                maxAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(maxAllowTime);

                OracleParameter minAllowTime = new OracleParameter();
                minAllowTime.Direction = ParameterDirection.Input;
                minAllowTime.Value = entity.MinAllowWorkTime;
                minAllowTime.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(minAllowTime);

                OracleParameter isOffDay = new OracleParameter();
                isOffDay.Direction = ParameterDirection.Input;
                isOffDay.Value = Convert.ToInt32(entity.IsOffDay);
                isOffDay.OracleDbType = OracleDbType.Int32;
                com.Parameters.Add(isOffDay);

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

        public override List<Shift> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Shift> shiftList = new List<Shift>();
            Shift shift = new Shift();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.FindAll, con);
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
                        shift = ShiftHelper(reader);
                        if (shift != null)
                        {
                            shiftList.Add(shift);
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
            return shiftList;
        }

        public List<Shift> FindByDeptCode(string deptCode, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Shift> shiftList = new List<Shift>();
            Shift shift = new Shift();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.FindByDeptCode, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter deptCodeParameter = new OracleParameter();
                deptCodeParameter.OracleDbType = OracleDbType.Long;
                deptCodeParameter.Direction = ParameterDirection.Input;
                deptCodeParameter.Value = deptCode;
                com.Parameters.Add(deptCodeParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        shift = ShiftHelper(reader);
                        if (shift != null)
                        {
                            shiftList.Add(shift);
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
            return shiftList;
        }

        public List<Shift> FindByUserID(int userID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Shift> shiftList = new List<Shift>();
            Shift shift = new Shift();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.FindByUserID, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter userIDParameter = new OracleParameter();
                userIDParameter.OracleDbType = OracleDbType.Int32;
                userIDParameter.Direction = ParameterDirection.Input;
                userIDParameter.Value = userID;
                com.Parameters.Add(userIDParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        shift = ShiftHelper(reader);
                        if (shift != null)
                        {
                            shiftList.Add(shift);
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
            return shiftList;
        }

        public List<Shift> FindByUserIDAndDate(int userID,DateTime date, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Shift> shiftList = new List<Shift>();
            Shift shift = new Shift();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.FindByUserIDAndDate, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter userIDParameter = new OracleParameter();
                userIDParameter.OracleDbType = OracleDbType.Int32;
                userIDParameter.Direction = ParameterDirection.Input;
                userIDParameter.Value = userID;
                com.Parameters.Add(userIDParameter);

                OracleParameter dateParameter = new OracleParameter();
                dateParameter.OracleDbType = OracleDbType.Date;
                dateParameter.Direction = ParameterDirection.Input;
                dateParameter.Value = date;
                com.Parameters.Add(dateParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        shift = ShiftHelper(reader);
                        if (shift != null)
                        {
                            shiftList.Add(shift);
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
            return shiftList;
        }

        public List<Shift> FindDefaultByUserID(int userID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Shift> shiftList = new List<Shift>();
            Shift shift = new Shift();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.FindDefaultByUserID, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter userIDParameter = new OracleParameter();
                userIDParameter.OracleDbType = OracleDbType.Int32;
                userIDParameter.Direction = ParameterDirection.Input;
                userIDParameter.Value = userID;
                com.Parameters.Add(userIDParameter);

              

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        shift = ShiftHelper(reader);
                        if (shift != null)
                        {
                            shiftList.Add(shift);
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
            return shiftList;
        }

        public override Shift FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Shift shift = new Shift();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ShiftRepositoryConstants.FindByID, con);
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
                        shift = ShiftHelper(reader);
                    }
                    else
                    {
                        shift = null;
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
            return shift;
        }

        private Shift ShiftHelper(OracleDataReader reader)
        {
            Shift shift = new Shift();
            shift.DayID = Convert.ToInt32(reader[ShiftConstants.DayID]);
            shift.DeptCode = reader[ShiftConstants.DeptCode].ToString();
            shift.Description = reader[ShiftConstants.Description].ToString();
            shift.ID = Convert.ToInt32(reader[ShiftConstants.ID]);
            shift.InAllowTime = Convert.ToInt32(reader[ShiftConstants.InAllowTime]);
            shift.InTime = Convert.ToDateTime(reader[ShiftConstants.InTime]);
            shift.IsFormalVacations = Convert.ToBoolean(reader[ShiftConstants.IsFormalVacation]);
            shift.IsNormalShift = Convert.ToBoolean(reader[ShiftConstants.IsNormalShift]);
            shift.MaxAllowWorkTime = Convert.ToInt32(reader[ShiftConstants.MaxAllowWorkTime]);
            shift.MinAllowWorkTime = Convert.ToInt32(reader[ShiftConstants.MinAllowWorkTime]);
            shift.Name = reader[ShiftConstants.Name].ToString();
            shift.OutAllowTime = Convert.ToInt32(reader[ShiftConstants.OutAllowTime]);
            shift.OutTime = Convert.ToDateTime(reader[ShiftConstants.OutTime]);
            shift.IsOffDay = Convert.ToBoolean(reader[ShiftConstants.IsOffDay]);
            return shift;
        }
    }
}
