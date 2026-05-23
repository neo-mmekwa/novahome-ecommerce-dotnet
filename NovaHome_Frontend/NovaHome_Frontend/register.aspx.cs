using NovaHome_Frontend.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NovaHome_Frontend
{
    public partial class register : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //check if password match 
            if (password.Value.Equals(cPassword.Value))
            {
                //hash password 
                var pass = Secrecy.HashPassword(password.Value);

                //create user 
                var newUser = new SystemUserDTO
                {
                    FirstName = fName.Value,
                    LastName = lName.Value,
                    Email = email.Value,
                    PhoneNumber = phone.Value,
                    Password = pass,
                    isActive = true,
                };

                //ask service if user is registered 
                var isReg = client.isReg(newUser);

                //error handling
                if(isReg.Equals("success"))
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    lblResponse.Text = isReg;
                }

            }
            
        }
    }
}