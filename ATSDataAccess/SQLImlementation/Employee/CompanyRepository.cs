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
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.Employee;
using Oracle.DataAccess.Client;

namespace ATSDataAccess.SQLImlementation.Employee
{
    public class CompanyRepository : RepositoryBaseClass<Company>
    {
        public override void Delete(Company entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(CompanyRepositoryConstants.Delete, con);
                con.Open();
                com.CommandType = CommandType.StoredProcedure;

                OracleParameter idParameter = new OracleParameter();
                idParameter.OracleDbType = OracleDbType.Int32;
                idParameter.Direction = ParameterDirection.Input;
                com.Parameters.Add(idParameter);

                com.ExecuteNonQuery();
                actionState.SetSuccess();
            }
            catch(Exception ex)
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

        public override void Insert(Company entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(CompanyRepositoryConstants.Insert, con);
                con.Open();
                com.CommandType = CommandType.StoredProcedure;

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.OracleDbType = OracleDbType.NVarchar2;
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.Value = entity.Name;
                com.Parameters.Add(nameParameter);

                OracleParameter descriptionParameter = new OracleParameter();
                descriptionParameter.OracleDbType = OracleDbType.NVarchar2;
                descriptionParameter.Direction = ParameterDirection.Input;
                descriptionParameter.Value = entity.Description;
                com.Parameters.Add(descriptionParameter);

                OracleParameter addressParameter = new OracleParameter();
                addressParameter.OracleDbType = OracleDbType.NVarchar2;
                addressParameter.Direction = ParameterDirection.Input;
                addressParameter.Value = entity.Address;
                com.Parameters.Add(addressParameter);

                OracleParameter phoneParameter = new OracleParameter();
                phoneParameter.OracleDbType = OracleDbType.NVarchar2;
                phoneParameter.Direction = ParameterDirection.Input;
                phoneParameter.Value = entity.Phone;
                com.Parameters.Add(phoneParameter);

                OracleParameter idParameter = new OracleParameter();
                idParameter.OracleDbType = OracleDbType.Int32;
                idParameter.Direction = ParameterDirection.Output;                
                com.Parameters.Add(idParameter);

                com.ExecuteNonQuery();
                entity.ID = Convert.ToInt32(com.Parameters[4].Value);
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

        public override void Update(Company entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(CompanyRepositoryConstants.Update, con);
                con.Open();
                com.CommandType = CommandType.StoredProcedure;

                OracleParameter idParameter = new OracleParameter();
                idParameter.OracleDbType = OracleDbType.Int32;
                idParameter.Direction = ParameterDirection.Input;
                idParameter.Value = entity.ID;
                com.Parameters.Add(idParameter);

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.OracleDbType = OracleDbType.NVarchar2;
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.Value = entity.Name;
                com.Parameters.Add(nameParameter);

                OracleParameter descriptionParameter = new OracleParameter();
                descriptionParameter.OracleDbType = OracleDbType.NVarchar2;
                descriptionParameter.Direction = ParameterDirection.Input;
                descriptionParameter.Value = entity.Description;
                com.Parameters.Add(descriptionParameter);

                OracleParameter addressParameter = new OracleParameter();
                addressParameter.OracleDbType = OracleDbType.NVarchar2;
                addressParameter.Direction = ParameterDirection.Input;
                addressParameter.Value = entity.Address;
                com.Parameters.Add(addressParameter);

                OracleParameter phoneParameter = new OracleParameter();
                phoneParameter.OracleDbType = OracleDbType.NVarchar2;
                phoneParameter.Direction = ParameterDirection.Input;
                phoneParameter.Value = entity.Phone;
                com.Parameters.Add(phoneParameter);                

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

        public override List<Company> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Company> companyList = new List<Company>();
            Company company = new Company();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(CompanyRepositoryConstants.FindAll, con);
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
                        company = CompanyHelper(reader);
                        if (company != null)
                        {
                            companyList.Add(company);
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
            return companyList;
        }

        public override Company FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Company company = new Company();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(CompanyRepositoryConstants.FindByID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.ParameterName = CompanyRepositoryConstants.ID;
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Double;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (OracleDataReader reader = ((OracleDataReader)(com.ExecuteReader())))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        company = CompanyHelper(reader);
                    }
                    else
                    {
                        company = null;
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
            return company;
        }

        private Company CompanyHelper(OracleDataReader reader)
        {
            Company company = new Company();
            company.ID = Convert.ToInt32(reader[CompanyConstants.ID]);
            company.Name = reader[CompanyConstants.Name].ToString();
            company.Description = reader[CompanyConstants.Description].ToString();
            company.Address = reader[CompanyConstants.Address].ToString();
            company.Phone = reader[CompanyConstants.Phone].ToString();
            return company;
        }
    }
}
