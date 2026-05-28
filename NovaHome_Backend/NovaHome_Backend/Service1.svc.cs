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


        //===========================================================================================================
        //USER MANAGEMENT 
        //===========================================================================================================
        public bool deleteUser(int userId, string password)
        {
            //find user 
            var user = (from u in db.SystemUsers
                        where u.UserId == userId && u.Password == password && u.isActive == true
                        select u).FirstOrDefault();


            //check if user exists
            if (user != null)
            {
                //set user activity to false
                user.isActive = false;

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
                        where u.UserId == userId && u.isActive == true
                        select new SystemUserDTO
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            isActive = u.isActive
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
                        where u.Email == email && u.Password == password && u.isActive == true
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
                                     where u.Email == user.Email && u.isActive == true
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
            if (user != null)
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
            if (user == null)
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

       
        //===========================================================================================================
        //PRODUCT MANAGEMENT
        //===========================================================================================================
        public bool createProduct(ProductDTO product)
        {
            try
            {
                //check if product already exists  
                var prod = (from p in db.Products
                            where p.ProductName == product.ProductName && p.isActive == true
                            select p).FirstOrDefault();


                //product exists 
                if (prod !=  null)
                    return false;

                //create new prod
                Product newProduct = new Product
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountPercent = product.DiscountPercent,
                    StockQuantity = product.StockQuantity,
                    ImageURL = product.ImageURL,
                    isActive = true,
                    DateAdded = DateTime.Now
                };

                //insert prod and submit to db
                db.Products.InsertOnSubmit(newProduct);
                db.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteProduct(int prodId)
        {
            //find user 
            var prod = (from p in db.Products
                        where p.ProductId == prodId && p.isActive == true
                        select p).FirstOrDefault();


            //check if prod exists
            if (prod != null)
            {
                //set prod activity to false
                prod.isActive = false;

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

        public bool editProduct(int prodId, string name, string description, decimal price, int discount, int quantity, string image)
        {
            try
            {
                //find prod 
                var prod = (from p in db.Products
                            where p.ProductId == prodId && p.isActive == true
                            select p).FirstOrDefault();

                //check if user exists and submit edits 
                if (prod != null)
                {
                    //assign updated values
                    prod.ProductName = name;
                    prod.Description = description;
                    prod.Price = price;
                    prod.DiscountPercent = discount;
                    prod.StockQuantity = quantity;
                    prod.ImageURL = image;

                    //submit changes
                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    //prod doesnt exist
                    return false;
                }
            } 
            catch 
            {
                return false;
            }
            
        }

        public ProductDTO getProduct(int prodId)
        {
            //check if prod exists 
            var prod = (from p in db.Products
                        where p.ProductId == prodId && p.isActive == true
                        select new ProductDTO
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Price = p.Price,
                            DiscountPercent = p.DiscountPercent,
                            StockQuantity = p.StockQuantity,
                            ImageURL = p.ImageURL
                        }).FirstOrDefault();

            //check if prod exists and return them
            if (prod != null)
            {
                return prod;
            }
            else
            {
                return null;
            }
        }

        public List<ProductDTO> getProducts()
        {
            //find active prods
            var prods = (from p in db.Products
                        where p.isActive == true
                        select new ProductDTO
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Price = p.Price,
                            DiscountPercent = p.DiscountPercent,
                            StockQuantity = p.StockQuantity,
                            ImageURL = p.ImageURL
                        });

            //return list of prods
            return prods.ToList();
        }

        //===========================================================================================================
        //CART MANAGEMENT 
        //===========================================================================================================
        public bool addToCart(int userId, int prodId, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool deleteCartItem(int cartItemId)
        {
            throw new NotImplementedException();
        }

        public List<CartItemDTO> getCartItems(int userId)
        {
            throw new NotImplementedException();
        }

        public int getOrCreateCart(int userId)
        {
            throw new NotImplementedException();
        }

        public void updateQuantity(int cartItemId, int newQuantity)
        {
            throw new NotImplementedException();
        }

        //===========================================================================================================
        //WISHLIST MANAGEMENT
        //===========================================================================================================
        public bool addToWishlist(int userId, int prodId)
        {
            throw new NotImplementedException();
        }

        public bool deleteWishlistItem(int wishlistItemId)
        {
            throw new NotImplementedException();
        }

        public int getOrCreateWishlist(int userId)
        {
            throw new NotImplementedException();
        }

    }
}
