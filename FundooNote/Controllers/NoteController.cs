using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBussiness noteBussiness;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBussiness noteBussiness,IDistributedCache distributedCache)
        {
            this.noteBussiness = noteBussiness;
            this.distributedCache = distributedCache;
        }

        
        [HttpPost]
        [Route("addNote")]
        public IActionResult AddNote(NoteModel model)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            //int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result = noteBussiness.AddNote(model, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { status = true, message = "Note details", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { status = false, message = "Notes not exists" });
            }

        }
        
        [HttpGet]
        [Route("getallnotes")]
        public IActionResult GetAllNotes()
        {
            List<NoteEntity> result = noteBussiness.GetAllNotes();
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { status = true, message = "Notes details", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { status = false, message = "Notes not exists" });
            }

        }
       
        [HttpPut]
        [Route("updateNote")]
        public IActionResult updateNote(int noteId, NoteModel model)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var note = noteBussiness.updateNote(noteId, userId, model);
            if (note != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "note updated successfully" });

            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "note updation failed" });
            }

        }
        
        [HttpDelete]
        [Route("deleteNote")]
        public IActionResult deleteNote(int noteId)
        {
            var result = noteBussiness.DeleteNote(noteId);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "Note deleted succesfully" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "Note deletion failed" });
            }
        }
       
        [HttpPut]
        [Route("IsPin")]
        public IActionResult IsPinOrNot(int noteId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result = noteBussiness.IsPinOrNot(noteId, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "Pinned" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = true, message = "Not Pinned" });
            }
        }
        
        [HttpPut]
        [Route("IsArchive")]
        public IActionResult IsArchiveOrNot(int noteId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result = noteBussiness.IsAracheiveOrNot(noteId, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "Pinned" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = true, message = "Not Pinned" });
            }
        }
        
        [HttpPut]
        [Route("IsTrash")]
        public IActionResult IsTrashOrNot(int noteId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));

            //int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result = noteBussiness.IsTrashOrNot(noteId, userId);
            if (result == true)
            {
                return Ok(new ResponseModel<string> { status = true, message = "Trashed" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "Not In trash" });
            }
        }
        
        [HttpDelete]
        [Route("Permanent Delete")]
        public IActionResult DeleteForever(int noteId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result = noteBussiness.DeleteNoteForever(noteId, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "Note deleted Permanatly" });

            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "Note deletion failed" });
            }
        }
        [HttpGet]
        [Route("getNoteById")]
        public IActionResult getNoteById(int noteId)
        {
            NoteEntity entity = noteBussiness.getNoteById(noteId);
            if (entity != null)
            {
                return Ok(new ResponseModel<NoteEntity> { status = true, message = "note details", data = entity });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { status = false, message = "note not exists" });
            }

        }

        [HttpPut]
        [Route("UploadImage")]

        public IActionResult UploadImage(int noteId, IFormFile imageUrl)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var result = noteBussiness.UploadImage(noteId, imageUrl, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "image uploaded successfully", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "image not uploaded" });
            }


        }
        
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedis()
        {
            try
            {
                var CacheKey = "NotesList";
                List<NoteEntity> NoteList;
                byte[] RedishNoteList = await distributedCache.GetAsync(CacheKey);
                if (RedishNoteList != null)
                {
                    var serializedNoteList = Encoding.UTF8.GetString(RedishNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);

                }
                else
                {
                    NoteList = (List<NoteEntity>)noteBussiness.GetAllNotes();
                    var SerializedNoteList = JsonConvert.SerializeObject(NoteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(SerializedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(15)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(CacheKey, redisNoteList, options);
                }
                return Ok(NoteList);
            }catch (Exception ex)
            {
                return BadRequest(new { status = false, Message = ex.Message });
            }
        }
    }
}
