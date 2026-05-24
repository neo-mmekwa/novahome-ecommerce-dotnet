using NovaHome_Frontend.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NovaHome_Frontend
{

    public partial class Master : System.Web.UI.MasterPage
    {
        Service1Client client = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            //role based access control

            //logged in user 
            if (Session["UserId"] != null) 
            {
                //custom nav 
                lblName.Text = "Hi, " + Session["Name"].ToString();

                //customer 
                if (Session["Role"].ToString().Equals("Customer"))
                {
                    liHome.Visible = true;
                    ddShop.Visible = true;
                    liAbout.Visible = true;
                    liContact.Visible = true;
                    liWishlist.Visible = true;
                    liCart.Visible = true;
                    liLogin.Visible = true;
                    ddMyAccount.Visible = true;
                    ddProdManagement.Visible = false;
                    ddUserManagement.Visible = false;
                    ddOrderManagement.Visible = false;
                }
                //staff
                if (Session["Role"].ToString().Equals("Staff"))
                {
                    liHome.Visible = true;
                    ddShop.Visible = true;
                    liAbout.Visible = true;
                    liContact.Visible = true;
                    liWishlist.Visible = true;
                    liCart.Visible = true;
                    liLogin.Visible = true;
                    ddMyAccount.Visible = true;
                    ddProdManagement.Visible = false;
                    ddUserManagement.Visible = false;
                    ddOrderManagement.Visible = true;
                }
                //manager 
                if (Session["Role"].ToString().Equals("Manager"))
                {
                    liHome.Visible = true;
                    ddShop.Visible = true;
                    liAbout.Visible = true;
                    liContact.Visible = true;
                    liWishlist.Visible = true;
                    liCart.Visible = true;
                    liLogin.Visible = true;
                    ddMyAccount.Visible = true;
                    ddProdManagement.Visible = true;
                    ddUserManagement.Visible = false;
                    ddOrderManagement.Visible = true;
                }
                //admin
                if (Session["Role"].ToString().Equals("Admin"))
                {
                    liHome.Visible = true;
                    ddShop.Visible = true;
                    liAbout.Visible = true;
                    liContact.Visible = true;
                    liWishlist.Visible = true;
                    liCart.Visible = true;
                    liLogin.Visible = true;
                    ddMyAccount.Visible = true;
                    ddProdManagement.Visible = false;
                    ddUserManagement.Visible = true;
                    ddOrderManagement.Visible = false;
                }
            }
            else
            {
                //default 
                liHome.Visible = true;
                ddShop.Visible = true;
                liAbout.Visible = true;
                liContact.Visible = true;
                liWishlist.Visible = true;
                liCart.Visible = true;
                liLogin.Visible = true;
                ddMyAccount.Visible = false;
                ddProdManagement.Visible = false;
                ddUserManagement.Visible = false;
                ddOrderManagement.Visible = false;
            }

        }
    }
}