using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;
using System.Linq;

namespace FundooNote.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBusiness collabBusiness;

        public CollabController(ICollabBusiness collabBusiness)
        {
            this.collabBusiness = collabBusiness;
        }
        [Authorize]

        [HttpPost]
        [Route("ADDCOLLAB")]

        public IActionResult AddCollaborator(CollabModel model, int noteId)
        {
            
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result = collabBusiness.AddCollab(model, noteId, userId);
            if(result != null)
            {
                return Ok(new ResponseModel<CollabaratorEntity> { status = true, message = "Collaborator added", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<CollabaratorEntity> { status = false, message = "Collab not added" });
            }

        }
        [Authorize]
        [HttpDelete]
        [Route("deleteCollaborator")]
        public IActionResult DeleteCollaborator(int noteId)
        {
            var result=collabBusiness.DeletedCollaborator(noteId);
            if (result != null)
            {
                return Ok(new ResponseModel<bool> { status = true,message="Collab deleted",data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { status = false, message = "collab not deleted" });
            }
        }
    }
}
