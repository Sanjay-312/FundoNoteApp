using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Repository_Layer.Services
{

    public class UserRepo : IUserRepo
    {
        private readonly FundoDbContext fundooContext;
        private readonly IConfiguration configuration;

        public UserRepo(FundoDbContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity entity = new UserEntity();
            entity.First_Name = model.First_Name;
            entity.Last_Name = model.Last_Name;
            entity.Email = model.Email;
            entity.Password =EncryptPassword(model.Password);
            entity.CreatedAt = model.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
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
        public bool IsEmailExisting(string email)
        {
            var EmailCount = fundooContext.UserEntity.Where(x => x.Email ==email).Count();
            return EmailCount > 0;
        }

        public string UserLogin(LoginModel login)
        {
            var encryptedPassword=EncryptPassword(login.Password);
            UserEntity checkEmail=fundooContext.UserEntity.Where(x=> x.Email == login.Email).FirstOrDefault();
            UserEntity checkPass =fundooContext.UserEntity.FirstOrDefault(x=>x.Password ==encryptedPassword);
            if (checkEmail != null)
            {
                if (checkPass != null)
                {
                    var token = GenerateToken(checkEmail.Email, checkEmail.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
               
            }
            else
            {
                return null;
            }
        }

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        private string GenerateToken(string email,int user_id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("user_id",user_id.ToString())
                
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public bool IsEmailPresented(string email)
        {
            var emailCount=fundooContext.UserEntity.Where(x=>x.Email==email).Count();
            return emailCount > 0;
        }

        public List<UserEntity> GetUserDetails()
        {
            List<UserEntity> entity = (List<UserEntity>)fundooContext.UserEntity.ToList();
            //var presnet = fundooContext.UserEntity.Where(x => x.Email == email).Count();
            
             return entity;

        }

        public string ForgetPassword(string EmailId)
        {
            try
            {
                var result = fundooContext.UserEntity.FirstOrDefault(x => x.Email == EmailId);
                if (result != null)
                {
                    var token = this.GenerateToken(result.Email, result.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.SendMessage(token, result.Email, result.First_Name);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email, ResetPwdModel reset)
        {
            string encryptedPwd=EncryptPassword(reset.Password);
            string encryptedConfirmPwd = EncryptPassword(reset.ConfirmPassword);
            try
            {
                if (encryptedPwd.Equals(encryptedConfirmPwd))
                {
                    var user = fundooContext.UserEntity.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = encryptedConfirmPwd;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public UserEntity GetUser(string firstName)
        {
            UserEntity entity=fundooContext.UserEntity.FirstOrDefault(x => x.First_Name ==firstName );
            if(entity != null)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public  UserTicket CreateTicketForPassword(string email,string token)
        {
            var result = fundooContext.UserEntity.FirstOrDefault(x => x.Email == email);
            if(result != null)
            {
                UserTicket ticket = new UserTicket
                {
                    firstName = result.First_Name,
                    lastName = result.Last_Name,
                    email = email,
                    token = token,
                    issuedAt = DateTime.Now
                };
                return ticket;
            }
            else
            {
                return null;
            }
        }

        public UserEntity getUserById(int id)
        {
            UserEntity entity=fundooContext.UserEntity.FirstOrDefault(x=>x.UserId== id);
            if(entity != null)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }
        public UserEntity UpdateUserById(int id,RegisterModel model)
        {
            var entity=fundooContext.UserEntity.FirstOrDefault( x=>x.UserId== id);

            if(entity != null)
            {
                entity.First_Name = model.First_Name;
                entity.Last_Name=model.Last_Name;
                entity.Email = model.Email;
                entity.Password =EncryptPassword( model.Password);
                entity.UpdatedAt= DateTime.Now;
                fundooContext.SaveChanges();
                return entity;
            }
            else
            {
                return null;
            }

        }

        public UserEntity LogginForSessionManagement(LoginModel model)
        {
            string encryptedPwd = EncryptPassword(model.Password);
            var result=fundooContext.UserEntity.FirstOrDefault(x=>x.Email== model.Email && x.Password==encryptedPwd);

            if(result != null)
            {
                return result;
            }
            else 
            {
                
                return null;
            }  

        }


    }
}
