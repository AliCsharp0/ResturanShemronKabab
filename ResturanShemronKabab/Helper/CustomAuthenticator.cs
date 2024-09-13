using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Security.ApplicationServiceContract.Services;
using Security.Domain.DTO.User;

namespace ResturanShemronKabab.Helper
{
    public class CustomAuthenticator : ActionFilterAttribute
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountBuss;


        public CustomAuthenticator(IAuthHelper authHelper, IAccountApplication _acountbuss)
        {
            _authHelper = authHelper;
            this._accountBuss = _acountbuss;


        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var username = context.HttpContext.User.Identity.Name;//Logined User UserName
            //if you have auhthentication cookie
            if (!context.HttpContext.User.Identity.IsAuthenticated)//a Boolean that determines if you logged in
            {
               //This command navigates request to login page
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "controller", "account" },
                        { "action", "login" }
                    });
                //context.HttpContext.Response.Redirect("/Account/Login");

            }
            //Getting Requested ControllerName
            var ControllerName = context.RouteData.Values["Controller"].ToString();
            //Getting Requested ActionName
            var ActionName = context.RouteData.Values["Action"].ToString();

            var userInfo = _authHelper.GetCurrentUserInfo();

            //Checking if cookie user name is empty
            
            if (string.IsNullOrEmpty(userInfo.UserName))
            {
                //context.HttpContext.Response.Redirect("/Account/Login");
                //This command navigates request to login page
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
            { "controller", "account" },
            { "action", "login" }
        });

            }

           CheckPermission permission = new CheckPermission
            {
                UserName = username
                ,
                Controller = ControllerName
                ,
                ActionName = ActionName
            };
            //Checks if User Has Registerd pernission in DataBase?
            if (!_accountBuss.CheckIfUserHasaccess(permission))
            {
                //context.HttpContext.Response.Redirect("/Account/Login");
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
            { "controller", "account" },
            { "action", "login" }
        });

            }

            base.OnActionExecuting(context);
        }
    }
}