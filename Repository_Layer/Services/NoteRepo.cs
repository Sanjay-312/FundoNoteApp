using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common_Layer.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Repository_Layer.Services
{
    public class NoteRepo:INoteRepo
    {
        private readonly FundoDbContext fundooContext;
        private readonly IConfiguration configuration;

        public NoteRepo(FundoDbContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }   

        public NoteEntity AddNote(NoteModel model,int userId)
        {
            NoteEntity note = new NoteEntity();
            note.Title = model.Title;
            note.Note=model.Note;
            note.Reminder = model.Reminder;
            note.Color = model.Color;
            note.Image = model.Image;
            note.IsArchieve=model.IsArchieve;
            note.IsPin=model.IsPin;
            note.IsTrash=model.IsTrash;
            note.CreatedAt = model.CreatedAt;
            note.UpdatedAt = DateTime.Now;
            note.UserId = userId;

            fundooContext.Note.Add(note);
            var result=fundooContext.SaveChanges();

            if (result>0)
            {
                return note;

            }
            else
            {
                return null;

            }

        }

        public List<NoteEntity> GetAllNotes() 
        {
        
            List<NoteEntity> notes=(List<NoteEntity>)fundooContext.Note.ToList();
            return notes;
        }

        public bool updateNote(int noteId, int userId, NoteModel model)
        {
            try
            {
                var result = fundooContext.Note.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {

                    if (model.Title != null)
                    {
                        result.Title = model.Title;
                    }
                    if (model.Note != null)
                    {
                        result.Note = model.Note;
                    }
                    result.UpdatedAt = DateTime.Now;
                    fundooContext.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception )
            {
                throw;
            }
            

        }

        public bool DeleteNote(int noteId)
        {
            try
            {
                var result = fundooContext.Note.FirstOrDefault(x => x.NoteId == noteId);
                if (result != null)
                {
                    fundooContext.Note.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsPinOrNot(int noteId,int userId)
        {
            NoteEntity entity = fundooContext.Note.FirstOrDefault(x=>x.NoteId == noteId && x.UserId==userId);
            try
            {
                if (entity != null)
                {
                    if (entity.IsPin == true)
                    {
                        entity.IsPin = false;
                        fundooContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        entity.IsPin = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
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

        public bool IsAracheiveOrNot(int noteId,int UserId)
        {
            NoteEntity entity= fundooContext.Note.FirstOrDefault(x=>x.NoteId==noteId && x.UserId==UserId);
            try
            {
                if (entity != null)
                {
                    if (entity.IsArchieve == true)
                    {
                        entity.IsArchieve = false;
                        fundooContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        entity.IsArchieve = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool IsTrashOrNot(int noteId,int userId)
        {
            NoteEntity entity = fundooContext.Note.FirstOrDefault(x=>x.NoteId== noteId && x.UserId==userId);
            try
            {


                if (entity != null)
                {
                    if (entity.IsTrash == true)
                    {
                        entity.IsTrash = false;
                        fundooContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        entity.IsTrash = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }catch (Exception)
            {
                
                throw;
            
            }
        }

        public bool DeleteNoteForever(int noteId,int userId)
        {
            var result=fundooContext.Note.FirstOrDefault(x=>x.NoteId== noteId && x.UserId==userId);
            if(result.IsTrash ==true )
            {
                fundooContext.Remove(result);
                fundooContext.SaveChanges();    
                return false;

            }
            result.IsTrash = true;
            fundooContext.SaveChanges();
            return true;

        }

        public NoteEntity ChangeColor(int noteId,string color)
        {
            NoteEntity entity = fundooContext.Note.FirstOrDefault(x => x.NoteId == noteId);
            if (entity.Color != null)
            {
                entity.Color = color;
                fundooContext.SaveChanges() ;
                return entity;
            }
            return null;

        }

        public NoteEntity Reminder(int noteId,DateTime remind)
        {
            NoteEntity entity=fundooContext.Note.FirstOrDefault(x=>x.NoteId== noteId);
            if (entity.Reminder != null)
            {
                entity.Reminder = remind;
                fundooContext.SaveChanges();
                return entity;
            }
            return null;

        }

        public NoteEntity getNoteById(int noteId)
        {
            NoteEntity entity=fundooContext.Note.FirstOrDefault(x=>x.NoteId == noteId);
            if (entity != null)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public string UploadImage(int noteId,IFormFile image,int userId)
        {
            var result=fundooContext.Note.FirstOrDefault(x=> x.NoteId == noteId && x.UserId==userId);
            if(result != null)
            {
                Account account=new Account(
                    configuration["CloudinarySettings:CloudName"],
                    configuration["CloudinarySettings:ApiKey"],
                    configuration["CloudinarySettings:ApiSecret"]);

                Cloudinary cloudinary=new Cloudinary(account);
                var uploadParameters = new ImageUploadParams()
                {
                    File=new FileDescription(image.FileName,image.OpenReadStream()),
                };

                var uploadResult=cloudinary.Upload(uploadParameters);
                string imagePath=uploadResult.Url.ToString();
                result.Image = imagePath;
                fundooContext.SaveChanges();
                return "Image uploaded Successfully";

            }
            else
            {
                return null;
            }
        }

        //public NoteEntity getNoteByName(string name)
        //{
        //    NoteEntity entity = fundooContext.Note.FindAll(x => x.Contains(name));
        //    if (entity != null)
        //    {
        //        return entity;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


    }
}
