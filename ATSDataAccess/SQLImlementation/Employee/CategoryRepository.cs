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
    public class CategoryRepository : RepositoryBaseClass<Category>
    {
        public override void Delete(Category entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Category entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Update(Category entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override List<Category> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Category> categoryList = new List<Category>();
            Category category = new Category();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(CategoryRepositoryConstants.FindAll, con);
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
                        category = CategoryHelper(reader);
                        if (category != null)
                        {
                            categoryList.Add(category);
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
            return categoryList;
        }

        public override Category FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;            
            Category category = new Category();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(CategoryRepositoryConstants.FindByID, con);
                con.Open();
                
                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.ParameterName = CategoryRepositoryConstants.ID;
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Double;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (OracleDataReader reader = ((OracleDataReader)(com.ExecuteReader())))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        category = CategoryHelper(reader);
                    }
                    else
                    {
                        category = null;
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
            return category;
        }

        private Category CategoryHelper(OracleDataReader reader)
        {
            Category category = new Category();
            category.ID = (float)Convert.ToDouble(reader[CategoryConstants.ID]);
            category.Name = reader[CategoryConstants.Description].ToString();
            return category;
        }
    }
}
