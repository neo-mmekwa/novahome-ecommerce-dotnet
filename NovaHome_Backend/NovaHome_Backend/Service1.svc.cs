using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NovaHome_Backend
{
   public class Service1 : IService1
    {
        //link db with service 
        DataClasses1DataContext db = new DataClasses1DataContext();
        public List<Product> getProducts()
        {
            //find active prods and turn into a list
            dynamic prods = (from p in db.Products
                             where p.isActive.Equals(1)
                             select p).ToList();

            //return prods
            return prods;
            
        }

        public bool isLoggedIn(string email, string password)
        {
            //find user 
            var user = (from u in db.SystemUsers
                        where u.Email == email && u.Password == password
                        select u).FirstOrDefault();

            //check if user exists 
            if(user != null)
            {
                //create login record - tracking user login activity
                var login = new UserLogin
                {
                    UserId = user.UserId,
                    LoginAt = DateTime.Now
                };
                db.UserLogins.InsertOnSubmit(login);
                db.SubmitChanges();
               
                return true;//user exists
            }
            else
            {
                return false; //user doesnt exist
            }
        }

        public bool isReg(SystemUser user)
        {
            //insert to db 
            db.SystemUsers.InsertOnSubmit(user);
            //submit changes to db
            try
            {
                //successful registration
                db.SubmitChanges();
                return true;
            }
            catch
            {
                //unsuccessful registration
                return false;
            }
        }
    }
}
