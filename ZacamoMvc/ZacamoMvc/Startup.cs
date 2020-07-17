using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(ZacamoMvc.Startup))]

namespace ZacamoMvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Security/Login"),
                CookieSecure = CookieSecureOption.SameAsRequest
            });
        }
    }
}