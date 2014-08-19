using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Rapp.Communication.Facebook
{
	public class FacebookState
	{
		/// <summary>
		/// Returns the facebook fan gate state from signedRequest and fbAppSecret
		/// </summary>
		/// <param name="signedRequest"> Get it with HttpContext.Current.Request.Params["signed_request"]</param>
		/// <param name="fbAppSecret">Supposedly in config ConfigurationManager.AppSettings["FbAppSecret"]</param>
		/// <returns>FacebookState Enum : OutsideOfFacebook, PageLiked or PageNotLiked</returns>
		public static FacebookStateEnum Resolve(string signedRequest, string fbAppSecret)
		{
			if (signedRequest == null)
				return FacebookStateEnum.OutsideOfFacebook;
			var fb = new FacebookClient();
			var sr = (IDictionary<string, object>)fb.ParseSignedRequest(fbAppSecret, signedRequest);
			if (sr.ContainsKey("page") && (IDictionary<string, object>)sr["page"] != null)
			{
				var page = (IDictionary<string, object>)sr["page"];
				return (bool)page["liked"] ? FacebookStateEnum.PageLiked : FacebookStateEnum.PageNotLiked;
			}
			return FacebookStateEnum.OutsideOfFacebook;
		}


		/// <summary>
		/// Returns the facebook fan gate state from signedRequest and fbAppSecret
		/// </summary>
		/// <param name="signedRequest"> Get it with HttpContext.Current.Request.Params["signed_request"]</param>
		/// <param name="fbAppSecret">Supposedly in config ConfigurationManager.AppSettings["FbAppSecret"]</param>
		/// <returns>FacebookState Enum : OutsideOfFacebook, PageLiked or PageNotLiked</returns>
		public static dynamic GetFacebookId(string accessToken)
		{
			if (string.IsNullOrWhiteSpace(accessToken))
				throw new Exception("Nope.");

			var fbClient = new FacebookClient(accessToken);
			dynamic me = fbClient.Get("me");
			return me;
		}
	}
}
