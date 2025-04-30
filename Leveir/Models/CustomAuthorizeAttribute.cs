using System.Web.Mvc;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        var actionName = filterContext.ActionDescriptor.ActionName;
        var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

        if (controllerName == "Account" && (actionName == "AdminLogin" || actionName == "AdminLogout"))
        {
            return;
        }

        if (filterContext.HttpContext.Session["UserId"] == null)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "AdminLogout" }
                });
        }
    }
}
