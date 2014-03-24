using System;
using System.Text;
using System.Web;
using StructureMap;

namespace Keramatian.Infrastructure.Logging
{
    public static class LogUtility
    {
        public static string BuildExceptionMessage(Exception x)
        {
            var logException = x;

            if (x.InnerException != null)
                logException = x.InnerException;

            var errorMessage = new StringBuilder();
            errorMessage.AppendFormat("Error in Path : {0}", HttpContext.Current.Request.Path).AppendLine();

            //Get the QueryString allong with the Virtual Path
            errorMessage.AppendFormat("Raw Url : {0}", HttpContext.Current.Request.RawUrl).AppendLine();

            //Get the error message
            errorMessage.AppendFormat("Message : {0}", logException.Message).AppendLine();

            //Source of the message
            errorMessage.AppendFormat("Source : {0}", logException.Source).AppendLine();

            //Stack trace of the error
            errorMessage.AppendFormat("Stack Trace : {0}", logException.StackTrace).AppendLine();

            //UpdateData where the error occurred
            errorMessage.AppendFormat("TargetSite : {0}", logException.TargetSite);

            return errorMessage.ToString();
        }

        public static ILogger Log
        {
            get { return ObjectFactory.GetInstance<ILogger>(); }
        }
    }

}