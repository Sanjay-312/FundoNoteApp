using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserBussiness userBussiness;
        public UserController(IUserBussiness userBussiness)
        {
            this.userBussiness = userBussiness;
        }

        [HttpPost]
        [Route("register")]
        //localhost:4000/api/User/register
        public IActionResult Registration(RegisterModel model)
        {
             
                var result = userBussiness.UserRegistration(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { status = true, message = "Registration successfull", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { status = false, message = "registraion failed" });
                }

            
            
            

        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel login)
        {


            var result = userBussiness.UserLogin(login);
            if (result == "Login Successfull")
            {
                return Ok(new ResponseModel<string> { status = true, message = "login successfull", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "login failed" });
            }

        }
    }
}
