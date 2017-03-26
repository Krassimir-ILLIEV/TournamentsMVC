using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TournamentsMVC.Models;
using TournamentsMVC.ViewModels.Models;
using TournamentsMVC.Data;
using TournamentsMVC.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TournamentsMVC
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            CreateRolesAndUsers();
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(TournamentSystemDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        private void CreateRolesAndUsers()
        {
            var tournamentDbContext = TournamentSystemDbContext.Create();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(tournamentDbContext));
            var userManager = new UserManager<User>(new UserStore<User>(tournamentDbContext));

            if (!roleManager.RoleExists(RoleNames.Admin))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleNames.Admin;
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new User();
                user.UserName = "shanu";
                user.Email = "syedshanumcain@gmail.com";

                string userPWD = "A@Z200711";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, role.Name);
                }
                //tournamentDbContext.SaveChanges();
            }

            // creating Creating Coach role    
            if (!roleManager.RoleExists(RoleNames.Coach))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleNames.Coach;
                roleManager.Create(role);

            }

            // creating Creating Player role    
            if (!roleManager.RoleExists(RoleNames.Player))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleNames.Player;
                roleManager.Create(role);
            }

            // creating Creating Sponsor role    
            if (!roleManager.RoleExists(RoleNames.Sponsor))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleNames.Sponsor;
                roleManager.Create(role);
            }
        }
    }
}