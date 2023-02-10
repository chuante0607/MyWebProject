using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Web.Routing;

namespace UCOMProject.Methods
{
    public class AuthorizationFilter : AuthorizeAttribute
    {

        // 核心Function
        // 可以自定義module邏輯，若沒有要自己處理整個module架構，基本上不覆寫
        //public override void OnAuthorization(AuthorizationContext actionContext)
        //{

        //}
        /// <summary>
        /// 驗證邏輯，成功回傳True，失敗回傳False
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //在此寫自己的驗證邏輯，例如判斷使用者是否已登入等
            bool isAuthorized = httpContext.Session["emp"] == null || httpContext.Session.IsNewSession;
            return !isAuthorized;
        }

        /// <summary>
        /// 驗證失敗
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new RedirectResult("~/Views/Home/Login.cshtml");
            //如果使用者没有通过验证，就会进入此方法
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Account",
                        action = "Login"
                    })
                );
        }


    }
}