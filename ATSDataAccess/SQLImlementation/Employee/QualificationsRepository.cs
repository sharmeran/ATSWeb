using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSCommon.Entites.Employee;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Employee;
using Oracle.DataAccess.Client;

namespace ATSDataAccess.SQLImlementation.Employee
{
    public class QualificationsRepository : RepositoryBaseClass<Qualifications>
    {
        public override void Delete(Qualifications entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Qualifications entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Update(Qualifications entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override List<Qualifications> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Qualifications> qualificationsList = new List<Qualifications>();
            Qualifications qualifications = new Qualifications();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(QualificationsRepositoryConstants.FindAll, con);
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
                        qualifications = QualificationsHelper(reader);
                        if (qualifications != null)
                        {
                            qualificationsList.Add(qualifications);
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
            return qualificationsList;
        }

        public override Qualifications FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Qualifications qualifications = new Qualifications();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(QualificationsRepositoryConstants.FindByID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.ParameterName = QualificationsRepositoryConstants.ID;
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Double;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (OracleDataReader reader = ((OracleDataReader)(com.ExecuteReader())))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        qualifications = QualificationsHelper(reader);
                    }
                    else
                    {
                        qualifications = null;
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
            return qualifications;
        }

        private Qualifications QualificationsHelper(OracleDataReader reader)
        {
            Qualifications qualifications = new Qualifications();
            qualifications.ID = Convert.ToDecimal(reader[QualificationsConstants.ID]);
            qualifications.Name = reader[QualificationsConstants.Name].ToString();
            return qualifications;
        }
    }
}
