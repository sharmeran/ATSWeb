using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ATSCommon.BaseClasses;
using ATSCommon.Enums;
using ATSDataAccess.Constants.Common;

namespace ATSDomain.Domains.Logs
{
   public class XMLSerelizeDomain<T> where T:BaseClass
    {
       public void SaveXMLFile(T entity, XMLLoggingTypeEnum xMLLoggingTypeEnum)
       {
           var stringwriter = new System.IO.StringWriter();
           var serializer = new XmlSerializer(entity.GetType());
           serializer.Serialize(stringwriter, entity);
           XmlDocument xmlDocument = new XmlDocument();
           xmlDocument.LoadXml(stringwriter.ToString());

           string locationName = string.Empty;
           if (xMLLoggingTypeEnum == XMLLoggingTypeEnum.EntityLogging)
           {
               locationName = CommonConstants.XmlLogDirectoryLocation;
               if (Directory.Exists(ConfigurationManager.AppSettings[locationName]) != true)
               {
                   Directory.CreateDirectory(ConfigurationManager.AppSettings[locationName]);
               }
           }
           else if (xMLLoggingTypeEnum == XMLLoggingTypeEnum.ErrorLogging)
           {
               locationName = CommonConstants.XmlErrorLogDirectoryLocation;
               if (Directory.Exists(ConfigurationManager.AppSettings[locationName]) != true)
               {
                   Directory.CreateDirectory(ConfigurationManager.AppSettings[locationName]);
               }
           }
           else
           {
               locationName = CommonConstants.XmlLostLogDirectoryLocation;
               if (Directory.Exists(ConfigurationManager.AppSettings[locationName]) != true)
               {
                   Directory.CreateDirectory(ConfigurationManager.AppSettings[locationName]);
               }
           }
           xmlDocument.Save(ConfigurationManager.AppSettings[locationName] +"\\"+ DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + ".xml");
       }
    }
}
