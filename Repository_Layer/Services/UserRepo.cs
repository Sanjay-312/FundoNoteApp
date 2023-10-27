using Common_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Repository_Layer.Services
{

    public class UserRepo : IUserRepo
    {
        private readonly FundoDbContext fundooContext;

        public UserRepo(FundoDbContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity entity = new UserEntity();
            entity.First_Name = model.First_Name;
            entity.Last_Name = model.Last_Name;
            entity.Email = model.Email;
            entity.Password = model.Password;
            fundooContext.UserEntity.Add(entity);
            var result = fundooContext.SaveChanges();
            if (result > 0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public string UserLogin(LoginModel login)
        {
            UserEntity checkEmail=fundooContext.UserEntity.FirstOrDefault(x=> x.Email == login.Email);
            UserEntity checkPass =fundooContext.UserEntity.FirstOrDefault(x=>x.Password == login.Password);
            if (checkEmail != null)
            {
                if (checkPass != null)
                {
                    return "Login Successfull";
                }
                else
                {
                    return null;
                }
               
            }
            else
            {
                return "Login failed";
            }
        }

        

        

    }
}
