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
            if (Session["UserId"] != null) 
            {
                //custom nav 
                lblName.Text = "Hi, " + Session["Name"].ToString();
            }

        }
    }
}