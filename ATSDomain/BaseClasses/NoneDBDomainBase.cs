using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Enums;
using ATSCommon.Utilities;

namespace ATSDomain.BaseClasses
{
    public class NoneDBDomainBase
    {
        public NoneDBDomainBase(int userID, LanguagesEnum language)
        {
            actionState = new ActionState();
            actionState.OwnerID = userID;
            this.language = language;
        }

        #region Member Variables

        private ActionState actionState;
        protected LanguagesEnum language;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the return result class.
        /// </summary>
        /// <value>The return result class.</value>
        public ActionState ActionState
        {
            set { actionState = value; }
            get { return actionState; }
        }

        #endregion

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogException(Exception exception)
        {
            LoggingUtil logger = new LoggingUtil();
            logger.LogException(exception);
            logger = null;
        }

    }
}
