using NovaHome_Frontend.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NovaHome_Frontend
{
    public partial class profile : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProfile();
            }
        }

        private void LoadProfile() 
        {
            //get the users details to display in the text boxes 
            int userId = Convert.ToInt32(Session["UserId"]);
            var user = client.getUser(userId);

            if(user != null)
            {
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtEmail.Text = user.Email;
                txtPhone.Text = user.PhoneNumber;
            }
        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            lblResponseProfile.Text = "";
            int userId = Convert.ToInt32(Session["UserId"]);

            //ask service if edits are submitted successfully
            bool edited = client.editUser (
                userId,
                txtFirstName.Text,
                txtLastName.Text,
                txtEmail.Text,
                txtPhone.Text
            );

            //check if if successful and display message 
           if(edited == true)
            {
                lblResponseProfile.Text = "Profile Edit Successful";
                //update session variables 
                Session["Email"] = txtEmail.Text;
                Session["Name"] = txtFirstName.Text;
            }
            else
            {
                lblResponseProfile.Text = "Profile Edit Unsuccessful";
            }

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            //check if password match 
            if (txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                //hash password 
                string passHash = Secrecy.HashPassword(txtPassword.Text);
                //ask service if password is changed 
                bool reset = client.resetPassword(userId, passHash);
                if(reset == true)
                {

                    lblResponsePass.Text = "Password Reset successful";
                }
                else
                {
                    lblResponsePass.Text = "Password Reset Unsuccessful";
                }
            }
            else 
            {
                lblResponsePass.Text = "Passwords don't match";
            }

        }

        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            //hash password
            string passHash = Secrecy.HashPassword(txtDeletePass.Text);

            //ask service if account is deleted and log user out 
            bool deleted = client.deleteUser(userId, passHash);

            //check if delete is successful
            if(deleted == true)
            {
                lblResponseDelete.Text = "Account Deleted successfully";
                Response.Redirect("logout.aspx");
            }
            else
            {
                lblResponseDelete.Text = "Account Deletion Unsuccessful";
            }

            
        }
    }
}