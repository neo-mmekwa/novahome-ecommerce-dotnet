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
}
