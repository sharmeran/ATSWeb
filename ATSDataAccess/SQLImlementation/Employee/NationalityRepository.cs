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
    public class NationalityRepository : RepositoryBaseClass<Nationality>
    {
        public override void Delete(Nationality entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Nationality entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Update(Nationality entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override List<Nationality> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Nationality> nationalityList = new List<Nationality>();
            Nationality nationality = new Nationality();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(NationalityRepositoryConstants.FindAll, con);
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
                        nationality = NationalityHelper(reader);
                        if (nationality != null)
                        {
                            nationalityList.Add(nationality);
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
            return nationalityList;
        }

        public override Nationality FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Nationality nationality = new Nationality();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(NationalityRepositoryConstants.FindByID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.ParameterName = NationalityRepositoryConstants.ID;
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Double;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (OracleDataReader reader = ((OracleDataReader)(com.ExecuteReader())))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        nationality = NationalityHelper(reader);
                    }
                    else
                    {
                        nationality = null;
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
            return nationality;
        }

        private Nationality NationalityHelper(OracleDataReader reader)
        {
            Nationality nationality = new Nationality();
            nationality.ID = Convert.ToDecimal(reader[NationalityConstants.ID]);
            nationality.Name = reader[NationalityConstants.Name].ToString();
            return nationality;
        }
    }
}
