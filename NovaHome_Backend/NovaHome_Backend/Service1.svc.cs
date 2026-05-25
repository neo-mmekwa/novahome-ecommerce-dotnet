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

        public bool deleteUser(int userId)
        {
            //find user 
            var user = (from u in db.SystemUsers
                        where u.UserId == userId && u.isActive==true
                        select u).FirstOrDefault();

            //check if user exists
            if (user != null)
            {
                db.SystemUsers.DeleteOnSubmit(user);
                try
                {
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

        public bool editUser(int userId, string fName, string lName, string email, string phone)
        {
            //find user 
            var user = (from u in db.SystemUsers
                        where u.UserId == userId && u.isActive == true
                        select u).FirstOrDefault();

            //check if user exists and submit edits 
            if (user != null)
            {
                //assign updated values
                user.FirstName = fName;
                user.LastName = lName;
                user.Email = email;
                user.PhoneNumber = phone;

                try
                {
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
                //user doesnt exist
                return false;
            }
        }

        public string getRole(int roleId)
        {
            //find role
            var role = (from r in db.Roles
                        where r.roleId == roleId
                        select r).FirstOrDefault();

            //check if role exists and return its name
            if (role != null)
            {
                return role.roleName;
            }
            else 
            {
                return null;
            }
        }

        public SystemUserDTO getUser(int userId)
        {
            //find user 
            var user = (from u in db.SystemUsers
                        where u.UserId == userId
                        select new SystemUserDTO
                        { 
                            FirstName = u.FirstName,
                            Email = u.Email
                        }).FirstOrDefault();

            //check if they exist and return them
            if (user != null)
            {
                return user;
            }
            else
            {
                return null; 
            }
        }

        public UserRoleDTO isLoggedIn(string email, string password)
        {
            //find user 
            var user = (from u in db.SystemUsers
                        where u.Email == email && u.Password == password
                        select u).FirstOrDefault();

            //check if user exists 
            if (user != null)
            {
                //create login record - tracking user login activity
                var login = new UserLogin
                {
                    UserId = user.UserId,
                    LoginAt = DateTime.Now
                };
                db.UserLogins.InsertOnSubmit(login);
                db.SubmitChanges();

                //find the user's role and return it
                var usersRole = (from ur in db.UserRoles
                                where ur.userId == user.UserId
                                select new UserRoleDTO
                                {
                                    userId = ur.userId,
                                    roleId = ur.roleId
                                }).FirstOrDefault();

                return usersRole; 
            }
            else
            {
                return null; //user doesnt exist
            }
        }

        public string isReg(SystemUserDTO user)
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
                
                UserRole userRole = new UserRole
                {
                    userId = newUser.UserId,
                    roleId = 1
                };
                //insert user role in db
                db.UserRoles.InsertOnSubmit(userRole);
                db.SubmitChanges();

                return "success";
            }
            catch (Exception ex)
            {
                //catch any errors 
                return "Error: " + ex.Message;
            }

        }

        public bool resetPassword(int userId, string newPassword)
        {
            //find user 
            var user = (from u in db.SystemUsers
                        where u.UserId == userId && u.isActive == true
                        select u).FirstOrDefault();

            //check if user exists
            if (user == null)
            {
                //reset password
                user.Password = newPassword;
                try
                {
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
                //user doesnt exist
                return false;
            }

        }

        public bool setUserRole(int userId, int roleId)
        {
            //find user using id 
            var user = (from u in db.UserRoles
                        where u.userId == userId && u.roleId == roleId
                        select u).FirstOrDefault();

            // if user does not exist
            if(user == null)
            {
                //set the users role 
                UserRole userRole = new UserRole
                {
                    userId = userId,
                    roleId = roleId
                };

                //insert in db 
                db.UserRoles.InsertOnSubmit(userRole);
                try
                {
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
