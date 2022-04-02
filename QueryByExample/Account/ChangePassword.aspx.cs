using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QueryByExample;


namespace QueryByExample {
    public partial class ChangePassword : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        
        }

        protected void btnChangePassword_Click(object sender, EventArgs e) {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            IdentityResult result = manager.ChangePassword(User.Identity.GetUserId(), tbCurrentPassword.Text, tbPassword.Text);
            if (result.Succeeded)
            {
                var user = manager.FindById(User.Identity.GetUserId());
                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("~/Account/Manage.aspx?m=ChangePwdSuccess");
            }
            else
            {
                tbPassword.ErrorText = result.Errors.FirstOrDefault();
                tbPassword.IsValid = false;
            }
        }
    }
}