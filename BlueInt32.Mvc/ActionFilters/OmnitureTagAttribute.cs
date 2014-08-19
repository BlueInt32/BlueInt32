using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace BlueInt32.Mvc.ActionFilters
{
	public class OmnitureTagAttribute : ActionFilterAttribute
	{
		public string Tag { get; set; }

		public OmnitureTagAttribute()
		{
		}
		public OmnitureTagAttribute(string tag)
		{
			Tag = tag;
		}
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if(string.IsNullOrWhiteSpace(Tag))
			{
				// if filter is not provided with a tag value (filter used as global for instance), action name is taken
				Tag = string.Format("{0}/{1}", filterContext.RouteData.Values["controller"], filterContext.RouteData.Values["action"]);
			}

			filterContext.Controller.ViewBag.OmnitureTag = Tag;
			base.OnActionExecuting(filterContext);
		}
	}
}
