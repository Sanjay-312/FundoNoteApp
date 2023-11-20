using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bussiness_Layer.Services
{
    public class NoteBussiness:INoteBussiness
    {
        private readonly INoteRepo noteRepo;

       public NoteBussiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }

        public NoteEntity AddNote(NoteModel model, int userId)
        {
            return noteRepo.AddNote(model, userId);
        }
        public List<NoteEntity> GetAllNotes()
        {
            return noteRepo.GetAllNotes();
        }

        public bool updateNote(int noteId, int userId, NoteModel model)
        {
            return noteRepo.updateNote(noteId,userId,model);
        }
        public bool DeleteNote(int noteId)
        {
            return noteRepo.DeleteNote(noteId);
        }

        public bool IsPinOrNot(int noteId, int userId)
        {
            return noteRepo.IsPinOrNot(noteId, userId);
        }

        public bool IsAracheiveOrNot(int noteId,int userId)
        {
            return noteRepo.IsAracheiveOrNot(noteId,userId);
        }
        public bool IsTrashOrNot(int noteId,int userId)
        {
            return noteRepo.IsTrashOrNot(noteId, userId);
        }

        public bool DeleteNoteForever(int noteId,int userId)
        {
            return noteRepo.DeleteNoteForever(noteId,userId);
        }

        public NoteEntity getNoteById(int noteId)
        {
            return noteRepo.getNoteById(noteId);
        }

        public string UploadImage(int noteId, IFormFile image, int userId)
        {
            return noteRepo.UploadImage(noteId, image, userId);
        }
    }
}
