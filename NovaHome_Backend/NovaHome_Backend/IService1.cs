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
        UserRoleDTO isLoggedIn(string email, string password);

        [OperationContract]
        SystemUserDTO getUser(int userId);

        [OperationContract]
        string getRole(int roleId);

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

    [DataContract]
    public class UserRoleDTO
    {
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public int roleId { get; set; }
    }
}
