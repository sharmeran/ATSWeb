using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Enums;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ATSCommon.Utilities
{
    public class LoggingUtil
    {
        #region Private Members
        /// <summary>
        /// template to format the log exception
        /// </summary>
        string logTemplate;

        LogLevelEnum loggerLevel;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public LoggingUtil()
        {
            loggerLevel = LogLevelEnum.Information;
            initLogTemplate();
        }
        public LoggingUtil(LogLevelEnum loggingLevel)
        {
            loggerLevel = loggingLevel;
            initLogTemplate();
        }
        #endregion

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level of message Severity.</param>
        public void LogMessage(string message, LogLevelEnum loglevel)
        {
            // Declaration
            TraceEventType level;

            // Implementation 
            if (loglevel.CompareTo(loggerLevel) >= 0)
            {
                level = ConvertToTraceEventType(loglevel);
                writeLog(message, level);
            }

        }

        /// <summary>
        /// Logs the exception - Critical Level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogException(Exception exception)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.Message = string.Format(logTemplate, exception.Message, exception.StackTrace);
            logEntry.Severity = TraceEventType.Critical;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);
        }

        #region Helper Methods
        private void writeLog(string message, TraceEventType level)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.Message = message;
            logEntry.Severity = level;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);
        }
        private void initLogTemplate()
        {
            logTemplate = "Message: {0}" + Environment.NewLine + "StackTrace: {1}";
        }

        private TraceEventType ConvertToTraceEventType(LogLevelEnum level)
        {
            // Declaration 
            TraceEventType resultLevel;

            // Initialization
            resultLevel = TraceEventType.Information;

            // Implementation 
            switch (level)
            {
                case LogLevelEnum.Information:
                    resultLevel = TraceEventType.Information;
                    break;
                case LogLevelEnum.Warning:
                    resultLevel = TraceEventType.Warning;
                    break;
                case LogLevelEnum.Error:
                    resultLevel = TraceEventType.Error;
                    break;
                case LogLevelEnum.Critical:
                    resultLevel = TraceEventType.Critical;
                    break;
                default:
                    break;
            }

            return resultLevel;
        }
        #endregion
    }
}
