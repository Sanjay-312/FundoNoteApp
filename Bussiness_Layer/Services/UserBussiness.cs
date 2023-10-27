using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_Layer.Services
{
    public class UserBussiness : IUserBussiness
    {
        private readonly IUserRepo userRepo;
        public UserBussiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            return userRepo.UserRegistration(model);
        }

        public string UserLogin(LoginModel login)
        {
            return userRepo.UserLogin(login);
        }
        


    }
}
