using Common_Layer.Models;
using Repository_Layer.Entity;
using System.Collections.Generic;

namespace Bussiness_Layer.Interfaces
{
    public interface IUserBussiness
    {
       public UserEntity UserRegistration(RegisterModel model);
       public string UserLogin(LoginModel login);

        public bool IsEmailExisting(string email);

        public bool IsEmailPresented(string email);
        public List<UserEntity> GetUserDetails();
        public string ForgetPassword(string EmailId);

        public bool ResetPassword(string email, ResetPwdModel reset);

        public UserEntity GetUser(string firstName);

        public UserTicket CreateTicketForPassword(string email, string token);

        public UserEntity getUserById(int id);

        public UserEntity UpdateUserById(int id, RegisterModel model);

        public UserEntity LogginForSessionManagement(LoginModel model);
    }
}