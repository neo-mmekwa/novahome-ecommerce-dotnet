using NovaHome_Frontend.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NovaHome_Frontend
{
    public partial class login : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //hash password 
            var pass = Secrecy.HashPassword(password.Value);

            //ask service if user is logged in 
            var ur = client.isLoggedIn(email.Value, pass);

            //redierect user
            if(ur != null)
            {
                //user info
                int userId = ur.userId;
                int roleId = ur.roleId;
                string name = client.getUser(userId).FirstName;
                string email = client.getUser(userId).Email;
                string role = client.getRole(roleId);

                //create session variables
                Session["UserId"] = userId;
                Session["Name"] = name;
                Session["Email"] = email;
                Session["Role"] = role;

                //redirect to index
                Response.Redirect("index.aspx");
            }
            else
            {
                lblResponse.Text = "Log in failed";
            }

            
        }
    }
}