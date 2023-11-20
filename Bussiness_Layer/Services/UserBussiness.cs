using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Context;
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

        public bool IsEmailExisting(string email)
        {
            return  userRepo.IsEmailExisting(email);
        }

        public bool IsEmailPresented(string email)
        {
            return userRepo.IsEmailPresented(email);
        }

        public List<UserEntity> GetUserDetails()
        {
            return userRepo.GetUserDetails();
        }

        public string ForgetPassword(string EmailId)
        {
            return userRepo.ForgetPassword(EmailId);
        }

        public bool ResetPassword(string email, ResetPwdModel reset)
        {
            return userRepo.ResetPassword(email, reset);
        }

        public UserEntity GetUser(string firstName)
        {
            return userRepo.GetUser(firstName);
        }
        public UserTicket CreateTicketForPassword(string email, string token)
        {
            return userRepo.CreateTicketForPassword(email, token);
        }
        public UserEntity getUserById(int id)
        {
            return userRepo.getUserById(id);
        }

        public UserEntity UpdateUserById(int id, RegisterModel model)
        {
            return userRepo.UpdateUserById(id, model);
        }
        public UserEntity LogginForSessionManagement(LoginModel model)
        {
            return userRepo.LogginForSessionManagement(model);
        }
    }
}
