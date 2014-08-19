using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BlueInt32.Mvc.Extensions
{
	public static class FormExtensions
	{
		/// <summary>
		/// Usage : @Html.RadioButtonListFor(m => m.AcceptNewsletter,new Dictionary<Civilite, string> { { Civilite.Mlle, "Mlle" }, { Civilite.Mme, "Mme" }, { Civilite.Mr, "M." } },"{0} <label for='{1}'>{1}</label>")
		/// Or : @Html.RadioButtonListFor(m => m.AcceptNewsletter,new Dictionary<bool, string> { { true, "Oui" }, { false, "Non!" } })
		/// </summary>
		/// <typeparam name="TModel">@model type</typeparam>
		/// <typeparam name="TProperty">Model property Mapped Type</typeparam>
		/// <param name="htmlHelper">@Html extension</param>
		/// <param name="expression">lambda to model's property</param>
		/// <param name="listOfValues"> dictionary describing model value to label</param>
		/// <param name="formater">custom format with {0}=input field and {1}=label text</param>
		/// <returns></returns>
		public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			Dictionary<TProperty, string> listOfValues,
			string formater = "  {0} {1}")
		{
			StringBuilder output = new StringBuilder();
			foreach (KeyValuePair<TProperty, string> item in listOfValues)
			{
				var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
				
				var id = string.Format("{0}_{1}", metaData.PropertyName, item.Key);
				var radio = htmlHelper.RadioButtonFor(expression, item.Key, new { id }).ToHtmlString();
				output.AppendFormat(formater, radio, item.Value);
			}
			return new MvcHtmlString(output.ToString());
		}
	}
}
