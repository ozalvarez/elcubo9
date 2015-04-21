using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using elcubo9.api.Providers;
using elcubo9.data.Models;
using Microsoft.Owin.Security.Facebook;
using elcubo9.bll.Utils;

namespace elcubo9.api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //ENABLE CORS
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(120),
               // AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(10),
                AllowInsecureHttp = true,
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
                
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
           

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");
            var _FacebookAuthenticationOptions = new FacebookAuthenticationOptions();
            _FacebookAuthenticationOptions.Scope.Add("email");
            _FacebookAuthenticationOptions.AppId = APP.FacebookAPPID;
            _FacebookAuthenticationOptions.AppSecret = APP.FacebookAPPSecret;
            _FacebookAuthenticationOptions.Provider = new FacebookAuthProvider();
            app.UseFacebookAuthentication(_FacebookAuthenticationOptions);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = System.Configuration.ConfigurationManager.AppSettings["app.google.ClientID"],
                ClientSecret = System.Configuration.ConfigurationManager.AppSettings["app.google.ClientSecret"],
                Provider = new GoogleAuthProvider()
            });
        }
    }
}
