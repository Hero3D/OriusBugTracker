using DataLibrary.BuisnessLogic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orius.Scripts
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var username = httpContext.User.Identity.Name;
            var userRole = RoleProcessor.GetUserRole(username).FirstOrDefault();

            return (userRole != null && allowedroles.Contains(userRole));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}