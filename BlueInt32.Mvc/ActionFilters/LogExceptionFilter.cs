
using BlueInt32.Core.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace BlueInt32.Mvc.ActionFilters
{
	public class LogExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in filterContext.Controller.ControllerContext.RouteData.Values)
			{
				sb.Append(string.Format("{0}:{1}/", keyValuePair.Key, keyValuePair.Value));
			}

			StringBuilder sbExceptionMessages = new StringBuilder();
			sbExceptionMessages.AppendFormat("Level 0: {0}", filterContext.Exception.Message);
			int level = 1;
			Exception exc = filterContext.Exception.InnerException;
			while(exc != null)
			{
				sbExceptionMessages.AppendLine(string.Format("Level {0} : {1}", level++, exc.Message));
				exc = exc.InnerException;
			}
			Log.Error(sb.ToString(), sbExceptionMessages.ToString() + " --- " + filterContext.Exception.StackTrace);
			
		}
	}
}
