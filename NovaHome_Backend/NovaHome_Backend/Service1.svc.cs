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
            //find prod 
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
            //check if quantity is 0 or more
            if(quantity <= 0)
            {
                return false;
            }

            //get cart belonging to user 
            int cartID = getOrCreateCart(userId);

            //find product 
            var prod = (from p in db.Products
                        where p.ProductId == prodId
                        select p).FirstOrDefault();

            //return false if prod doesnt exist 
            if (prod == null)
            {
                return false;
            }

            //get product price 
            decimal unitPrice = prod.Price;

            //find item to be added 
            var item = (from ci in db.CartItems
                        where ci.CartId == cartID && ci.ProductId == prodId
                        select ci).FirstOrDefault();
            //check if items exist 
            if (item != null)
            {
                //incremenet quantity and recalc total price 
                item.Quantity += quantity;
                item.TotalPrice = item.Quantity * unitPrice;
            }else
            {
                //create new item & add to table 
                var newItem = new CartItem
                {
                    CartId = cartID,
                    ProductId = prodId,
                    Quantity = quantity,
                    TotalPrice = unitPrice * quantity
                };

                db.CartItems.InsertOnSubmit(newItem);
            }
            //try to save changes
            try
            {
                //changes submitted successfully
                db.SubmitChanges();
                return true;
            }
            catch 
            {
                //failure in submitting changes 
                return false;
            }
        }

        public bool deleteCartItem(int cartItemId)
        {
            //find item 
            var item = (from i in db.CartItems
                        where i.CartItemId == cartItemId
                        select i).FirstOrDefault();


            //check if prod exists
            if (item != null)
            {
                //delete item & submit changes to db
                db.CartItems.DeleteOnSubmit(item);
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

        public List<CartItemDTO> getCartItems(int userId)
        {
            //get or create cart for specified user 
            int cartId = getOrCreateCart(userId);

            //find items that match cartid 
            var items = (from ci in db.CartItems
                         where ci.CartId == cartId
                         select new
                         {
                             ci.CartItemId,
                             ci.CartId,
                             ci.ProductId,
                             ci.Quantity,
                             ci.TotalPrice
                         }).ToList();

            //get prods 
            var prods = db.Products.ToList();

            //get cart items 
            var cartItems = items.Select(i =>
            {
                //get prod
                var prod = (from p in db.Products
                            where p.ProductId == i.ProductId
                            select p).FirstOrDefault();

                //return cartitem 
                return new CartItemDTO
                {
                    CartItemId = i.CartItemId,
                    CartId = i.CartId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice,

                    //if prod isnt null return prod
                    Product = prod != null ? new ProductDTO
                    {
                        ProductId = prod.ProductId,
                        ProductName = prod.ProductName,
                        Price = prod.Price,
                        ImageURL = prod.ImageURL,
                        Description = prod.Description,
                    } : null
                };
            
            }).ToList();

            return cartItems;
        }

        public int getOrCreateCart(int userId)
        {
            //get the cart 
            var cart = (from c in db.Carts
                        where c.UserId == userId
                        select c).FirstOrDefault();
            //check if cart exists
            if(cart != null)
            {
                //get existing cart 
                return cart.CartId;
            }
            else
            {
                //create a new cart
                Cart newCart = new Cart
                {
                    UserId = userId
                };

                //add cart to table 
                db.Carts.InsertOnSubmit(newCart);

                //try to save changes 
                try
                {
                    db.SubmitChanges();
                    return newCart.CartId;
                }
                catch
                {
                    return -1;
                }
            }

        }

        public void updateQuantity(int cartItemId, int newQuantity)
        {
            //find the item 
            var item = (from i in db.CartItems
                        where i.CartItemId == cartItemId
                        select i).FirstOrDefault();

            //check if it exists 
            if (item == null)
            {
                return;
            }

            //check quantity
            if(newQuantity <= 0)
            {
                //delete item
                db.CartItems.DeleteOnSubmit(item);
            }else if(newQuantity >- 1)
            {
                decimal unitPrice = 0;

                //check if item has prodid
                if (item.ProductId != null)
                {
                    //find prod
                    var prod = (from p in db.Products
                                where p.ProductId == item.ProductId
                                select p).FirstOrDefault();

                    //set unit price to prods price 
                    if (prod != null)
                    {
                        unitPrice = prod.Price;
                    }
                }
                //set items quanitty and total price to new values
                item.Quantity = newQuantity;
                item.TotalPrice = unitPrice * newQuantity;
            }
            //update db
            db.SubmitChanges();
        }

        //===========================================================================================================
        //WISHLIST MANAGEMENT
        //===========================================================================================================
        public bool addToWishlist(int userId, int prodId)
        {

            //get wishlist  belonging to user 
            int wishId = getOrCreateWishlist(userId);

            //find product 
            var prod = (from p in db.Products
                        where p.ProductId == prodId
                        select p).FirstOrDefault();

            //return false if prod doesnt exist 
            if (prod == null)
            {
                return false;
            }

            //find item to be added 
            var item = (from wi in db.WishlistItems
                        where wi.WishlistId == wishId && wi.ProductId == prodId
                        select wi).FirstOrDefault();


            //create new item & add to table 
            var newItem = new WishlistItem
            {
                WishlistId = wishId,
                ProductId = prodId,
            };

            db.WishlistItems.InsertOnSubmit(newItem);

            //try to save changes
            try
            {
                //changes submitted successfully
                db.SubmitChanges();
                return true;
            }
            catch
            {
                //failure in submitting changes 
                return false;
            }
        }

        public bool deleteWishlistItem(int wishlistItemId)
        {
            //find item 
            var item = (from i in db.WishlistItems
                        where i.WishlistItemId == wishlistItemId
                        select i).FirstOrDefault();


            //check if prod exists
            if (item != null)
            {
                //delete item & submit changes to db
                db.WishlistItems.DeleteOnSubmit(item);
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

        public int getOrCreateWishlist(int userId)
        {
            //get the wishlist 
            var wish = (from w in db.Wishlists
                        where w.UserId == userId
                        select w).FirstOrDefault();
            //check if wishlist exists
            if (wish != null)
            {
                //get existing wishlist 
                return wish.WishlistId;
            }
            else
            {
                //create a new wishlist
                Wishlist newWish = new Wishlist
                {
                    UserId = userId
                };

                //add wishlist to db table 
                db.Wishlists.InsertOnSubmit(newWish);

                //try to save changes 
                try
                {
                    db.SubmitChanges();
                    return newWish.WishlistId;
                }
                catch
                {
                    return -1;
                }
            }
        }

    }
}
