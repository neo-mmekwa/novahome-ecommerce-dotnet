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


        string IService1.isReg(SystemUserDTO user)
        {
            try
            {
                //check if user exists by email 
                var existingEmail = (from u in db.SystemUsers
                                     where u.Email == user.Email
                                     select u).FirstOrDefault();

                //email exists return false
                if (existingEmail != null)
                    return "Email already in use";

                //check if user exists by phone number
                var existingNumber = (from u in db.SystemUsers
                                      where u.PhoneNumber == user.PhoneNumber
                                      select u).FirstOrDefault();
                //phone number exists return false
                if (existingNumber != null)
                    return "Phone number already in use";

                //user doesnt exist then create new user
                SystemUser newUser = new SystemUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    isActive = user.isActive,
                    DateAdded = DateTime.Now
                };

                //insert new user to db
                db.SystemUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                //set user role to customer by default
                /*
                UserRole userRole = new UserRole
                {
                    userId = newUser.UserId,
                    roleId = 1
                };
                //insert user role in db
                db.UserRoles.InsertOnSubmit(userRole);
                db.SubmitChanges();*/

                return "success";
            }
            catch (Exception ex)
            {
                //catch any errors 
                return "Error: " + ex.Message;
            }

        }

        bool IService1.setUserRole(int userId, int roleId)
        {
            //find user using id 
            var user = (from u in db.UserRoles
                        where u.userId == userId && u.roleId == roleId
                        select u).FirstOrDefault();

            // if user does not exist
            if(user == null)
            {
                //set the users additional role 
                UserRole userRole = new UserRole
                {
                    userId = userId,
                    roleId = roleId
                };

                //insert in db 
                db.UserRoles.InsertOnSubmit(userRole);
                try
                {
                    //submit chnages 
                    db.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
