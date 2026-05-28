using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NovaHome_Backend
{
    [ServiceContract]
    public interface IService1
    {
        //USER MANAGEMENT 

        [OperationContract]
        string isReg(SystemUserDTO user);

        [OperationContract]
        UserRoleDTO isLoggedIn(string email, string password);

        [OperationContract]
        SystemUserDTO getUser(int userId);

        [OperationContract]
        string getRole(int roleId);

        [OperationContract]
        bool setUserRole(int userId, int roleId);

        [OperationContract]
        bool resetPassword(int userId, string newPassword);

        [OperationContract]
        bool editUser(int userId, string fName, string lName, string email, string phone);

        [OperationContract]
        bool deleteUser(int userId, string password);

        //PRODUCT MANAGEMENT 

        [OperationContract]
        ProductDTO getProduct(int prodId);

        [OperationContract]
        List<ProductDTO> getProducts();

        [OperationContract]
        bool createProduct(ProductDTO product);

        [OperationContract]
        bool editProduct(int prodId, string name, string description, decimal price, int discount, int quantity, string image);

        [OperationContract]
        bool deleteProduct(int prodId);


        //CART MANAGEMENT
        [OperationContract]
        int getOrCreateCart(int userId);

        [OperationContract]
        bool addToCart(int userId, int prodId, int quantity);

        [OperationContract]
        void updateQuantity(int cartItemId, int newQuantity);

        [OperationContract]
        bool deleteCartItem(int cartItemId);

        [OperationContract]
        List<CartItemDTO> getCartItems(int userId);


        //WISHLIST 
        [OperationContract]
        int getOrCreateWishlist(int userId);

        [OperationContract]
        bool addToWishlist(int userId, int prodId);

        [OperationContract]
        bool deleteWishlistItem(int wishlistItemId);

    }


    //USER MANAGEMENT 
    [DataContract]
    public class SystemUserDTO
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool isActive { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
    }

    [DataContract]
    public class UserRoleDTO
    {
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public int roleId { get; set; }
    }
    
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int DiscountPercent { get; set; }

        [DataMember]
        public int StockQuantity { get; set; }

        [DataMember]
        public string ImageURL { get; set; }

        [DataMember]
        public bool isActive { get; set; }

        [DataMember]
        public DateTime DateAdded { get; set; }
    }

    [DataContract]
    public class CategoryDTO
    {
        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public string CategoryType { get; set; }

        [DataMember]
        public bool isActive { get; set; }
    }

    [DataContract]
    public class ProductCategoryDTO
    {
        [DataMember]
        public int ProductCategoryId { get; set; }

        [DataMember]
        public int ProductId { get; set; }
        
        [DataMember]
        public int CategoryId { get; set; }
    }

    [DataContract]
    public class CartItemDTO
    {
        [DataMember]
        public int CartItemId { get; set; }

        [DataMember]
        public int CartId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public decimal TotalPrice { get; set; }
    }

    [DataContract]
    public class WishlistDTO
    {
        [DataMember]
        public int WishlistId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }
    }

    [DataContract]
    public class WishlistItemDTO
    {
        [DataMember]
        public int WishlistItemId { get; set;}

        [DataMember]
        public int WishlistId { get; set; }

        [DataMember]
        public int ProductId { get; set; }
    }
}
