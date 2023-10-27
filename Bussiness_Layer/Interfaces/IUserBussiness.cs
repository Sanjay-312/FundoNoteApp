using Common_Layer.Models;
using Repository_Layer.Entity;

namespace Bussiness_Layer.Interfaces
{
    public interface IUserBussiness
    {
        UserEntity UserRegistration(RegisterModel model);
       public string UserLogin(LoginModel login);

        
    }
}