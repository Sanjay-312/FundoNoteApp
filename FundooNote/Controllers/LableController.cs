using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        private readonly ILableBussiness lablebussiness;
        public LableController(ILableBussiness lablebussiness)
        {
            this.lablebussiness = lablebussiness;
        }

        [HttpPost]
        [Route("AddLable")]
        public IActionResult AddLable(LableModel model,int noteId)
        {
            int userId =Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result=lablebussiness.AddLable(model, noteId, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<LableEntity> { status = true, message = "lable added successfully", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<LableEntity> { status=false,message="lable adding failed"});
            }

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllLables()
        {
            var result=lablebussiness.GetAllLable();
            if(result != null)
            {
                return Ok(new ResponseModel<List<LableEntity>> { status=true, message="All lables details",data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<List<LableEntity>> { status = false, message = "Not exists" });
            }
        }

        [HttpPut]
        [Route("updateLable")]
        public IActionResult UpdateLable(int lableId,LableModel model)
        {
            int userId=Convert.ToInt32(User.Claims.FirstOrDefault(x=>x.Type == "user_id").Value);
            var result=lablebussiness.UpdateLable(lableId,model, userId);
            if(result != null)
            {
                return Ok(new ResponseModel<bool> { status=true,message="Lable updated",data = result});
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { status = false, message = "Lable not updated", });
            }

        }
        [HttpDelete]
        [Route("deleteLable")]
        public IActionResult DeleteLable(int lableId)
        {
            int userId=Convert.ToInt32(User.Claims.FirstOrDefault(x=>x.Type== "user_id").Value); 
            var result=lablebussiness.DeleteLable(lableId, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<bool> { status=true,message="Lable deleted successfully",data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { status = false, message = "Lable not deleted" });
            }
        }
    }
}
