using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.BaseClasses;
using ATSCommon.Enums;
using ATSCommon.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Win32;

namespace ATSDataAccess.BaseClasses
{
    public abstract class RepositoryBaseClass<T> where T : BaseClass
    {
        RegistryKey objRegistryKey = Registry.LocalMachine;
        protected Database database;
        protected LanguagesEnum language;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase&lt;T&gt;"/> class.
        /// </summary>
        protected RepositoryBaseClass()
        {
            database = new SqlDatabase(ConfigurationManager.ConnectionStrings["SQL"].ConnectionString);
        }

       
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        protected void LogException(Exception exception)
        {
            LoggingUtil logger = new LoggingUtil();
            logger.LogException(exception);
            logger = null;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userID">Id of the who will delete this entity</param>
        /// <param name="actionState">Return result which contains the error massage</param>
        public abstract void Delete(T entity, ActionState actionState);

        /// <summary>
        /// Finds the by ID.
        /// </summary>
        /// <param name="resourceMessageEntityID">The entity ID.</param>
        /// <param name="actionState">Return result which contains the error massage</param>
        /// <returns>The founded entity</returns>
        public abstract T FindByID(int entityID, ActionState actionState);

        /// <summary>
        /// Inserts the specified activeSession entity.
        /// </summary>
        /// <param name="userEntity">The activeSession entity.</param>
        /// <param name="userID">Id of the who will create this record</param>
        /// <param name="actionState">Return result which contains the error massage</param>
        public abstract void Insert(T entity, ActionState actionState);

        /// <summary>
        /// Updates the specified activeSession entity.
        /// </summary>
        /// <param name="entity">The activeSession entity.</param>
        /// <param name="userID">Id os the user who update this entity</param>
        /// <param name="actionState">Return result which contains the error massage</param>
        public abstract void Update(T entity, ActionState actionState);        

        /// <summary>
        /// Finds all entities.
        /// </summary>
        /// <returns>list of entities of type T</returns>
        public abstract List<T> FindAll(ActionState actionState);
    }
}