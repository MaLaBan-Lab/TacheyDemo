using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using System.Configuration;
using Owin;
using Owin.Security.Middleware.Line;
using Tachey001.Models;
using Line.Login.Models;

namespace Tachey001
{
    public partial class Startup
    {
        // 如需設定驗證的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // 設定資料庫內容、使用者管理員和登入管理員，以針對每個要求使用單一執行個體
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // 讓應用程式使用 Cookie 儲存已登入使用者的資訊
            // 並使用 Cookie 暫時儲存使用者利用協力廠商登入提供者登入的相關資訊；
            // 在 Cookie 中設定簽章
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // 讓應用程式在使用者登入時驗證安全性戳記。
                    // 這是您變更密碼或將外部登入新增至帳戶時所使用的安全性功能。  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 讓應用程式在雙因素驗證程序中驗證第二個因素時暫時儲存使用者資訊。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 讓應用程式記住第二個登入驗證因素 (例如電話或電子郵件)。
            // 核取此選項之後，將會在用來登入的裝置上記住登入程序期間的第二個驗證步驟。
            // 這類似於登入時的 RememberMe 選項。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // 註銷下列各行以啟用利用協力廠商登入提供者登入
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
               appId: "924538288391157",
               appSecret: "aeb19976ac9b6f4423232f1daf9620fe");
            //var options = new FacebookAuthenticationOptions
            //{
            //    AppId = "522413578931098",
            //    AppSecret = "aa407cbe0f0b05bbadba8914df91c586",
            //    CallbackPath = new PathString("/Account/ExternalLoginCallback/"),
            //    Provider = new FacebookAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the username
            //            string facebookUserName = context.UserName;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseFacebookAuthentication(options);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "1037350215092-9rcm57k133ffatrhs0i5tkanv0ppdppp.apps.googleusercontent.com",
                ClientSecret = "QgQK1ahv7zUXBA5DqXv4NVHq"
            });

            app.UseLineAuthentication(new LineAuthenticationOptions(
                channelId: "1656163525",
                channelSecret: "87a29eb6c97c02a8f1c649a8f2a8da22",
                redirectUri: "https://localhost:44394/line-signin",
                scope: Scope.OpenId | Scope.Profile | Scope.Email
            ));
        }
    }
}