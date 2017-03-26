using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace TournamentsMVC.Identity
{
    public class UserManager
    {
        public void GetUserId()
        {
            HttpContext.Current.User.Identity.GetUserId();
        }
        
    }
}