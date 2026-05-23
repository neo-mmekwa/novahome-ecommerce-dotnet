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
        [OperationContract]
        string isReg(SystemUserDTO user);

        [OperationContract]
        bool isLoggedIn(string email, string password);

        [OperationContract]
        bool setUserRole(int userId, int roleId);
    }

    [DataContract]
    public class SystemUserDTO
    {
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
    }

    public class UserRoleDTO
    { 
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int RoldId { get; set; }
    }

}
