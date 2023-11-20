using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository_Layer.Entity;
using System.Collections.Generic;
using System.Linq;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserBussiness userBussiness;
        private readonly ILogger<UserController> Ilogger;
        public UserController(IUserBussiness userBussiness,ILogger<UserController> Ilogger)
        {
            this.userBussiness = userBussiness;
            this.Ilogger = Ilogger;
        }

        [HttpPost]
        [Route("register")]
        //localhost:4000/api/User/register
        public IActionResult Registration(RegisterModel model)
        {
            
            var IsEmailExists=userBussiness.IsEmailExisting(model.Email);
            if (IsEmailExists)
            {
                return Ok(new ResponseModel<string> { status=true,message="Email Already exists",data=model.Email});
            }
            else
            {
                Ilogger.LogInformation("REGISTRAION STARTED");
                var result = userBussiness.UserRegistration(model);
                if (result != null)
                {
                    Ilogger.LogInformation("REGISTRAION SUCCESSFULL");
                    return Ok(new ResponseModel<UserEntity> { status = true, message = "Registration successfull", data = result });
                }
                else
                {
                    Ilogger.LogInformation("REGISTRAION FAILED");
                    return BadRequest(new ResponseModel<UserEntity> { status = false, message = "registraion failed" });
                }

            }                          

        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel login)
        {


            var result = userBussiness.UserLogin(login);
            if (result !=null)
            {
                Ilogger.LogInformation("LOGIN SUCCESSFULL");
                return Ok(new ResponseModel<string> { status = true, message = "login successfull",data=result });
            }
            else
            {
                Ilogger.LogInformation("LOGIN FAILED");
                return BadRequest(new ResponseModel<string> { status = false, message = "login failed" });
            }

        }

        [HttpPost]
        [Route("checkexistence")]
        public IActionResult CheckEmail(string email)
        {
            var result=userBussiness.IsEmailPresented(email);
            if (result)
            {
                return Ok(new ResponseModel<string> { status = true, message = "alreday exists", });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "not exists" });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult getUserData()
        {
            List<UserEntity> UserList = userBussiness.GetUserDetails();
            if (UserList!=null)
            {
                return Ok(new ResponseModel<List<UserEntity>> { status = true, message = "users details",data=UserList });
            }
            else
            {
                return BadRequest(new ResponseModel<List<UserEntity>> { status = false, message = "not exists" });
            }
        }

        [HttpPost]
        [Route("forgotpassword")]
        public IActionResult ForgotPassword(string email)
        {
            var result = userBussiness.ForgetPassword(email);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "mail sent successfullty", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "mail sending failed" });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("resetpassword")]
        public IActionResult ResetPassword(ResetPwdModel reset)
        {
            string email= User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            var result = userBussiness.ResetPassword(email, reset);
            if (result != null)
            {
                return Ok(new ResponseModel<ResetPwdModel> { status = true, message = "password changed successfully" });
            }
            else
            {
                return BadRequest(new ResponseModel<ResetPwdModel> { status = false, message = "password reset failed" });
            }
        }

        [HttpGet]
        [Route("SINGLEUSERINFO")]
        public IActionResult getSingleUserDetails(string firstName)
        {
            UserEntity UserDetails = userBussiness.GetUser(firstName);
            if (UserDetails != null)
            {
                return Ok(new ResponseModel<UserEntity> { status = true, message = "user details", data =UserDetails });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { status = false, message = "user not exists" });
            }
        }

        [HttpGet]
        [Route("UserDetailsById")]
        public IActionResult getUserById(int id)
        {
            UserEntity UserDetails = userBussiness.getUserById(id);
            if (UserDetails != null)
            {
                return Ok(new ResponseModel<UserEntity> { status = true, message = "user details", data = UserDetails });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { status = false, message = "user not exists" });
            }
        }

        [HttpPut]
        [Route("UpdateUserById")]
        public IActionResult updateUserById(int id,RegisterModel model)
        {
            UserEntity UserDetails = userBussiness.UpdateUserById(id, model);
            if (UserDetails != null)
            {
                return Ok(new ResponseModel<UserEntity> { status = true, message = "user updated successfully", data = UserDetails });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { status = false, message = "user not exists" });
            }
        }

        [HttpPost]
        [Route("LoginWithSession")]

        public IActionResult LoginWithSessionManagement(LoginModel model)
        {
            var result=userBussiness.LogginForSessionManagement(model);
            if(result != null)
            {
                HttpContext.Session.SetInt32("UserId", result.UserId);
                return Ok(new ResponseModel<UserEntity> { status=true,message="login successfull",data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { status = false, message = "Login failed" });
            }

        }



    }
}
