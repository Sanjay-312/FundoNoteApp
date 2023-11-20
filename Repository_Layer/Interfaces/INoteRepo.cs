using Common_Layer.Models;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface INoteRepo
    {
        public NoteEntity AddNote(NoteModel model, int userId);
        public List<NoteEntity> GetAllNotes();

        public bool updateNote(int noteId, int userId, NoteModel model);
        public bool DeleteNote(int noteId);

        public bool IsPinOrNot(int noteId, int userId);

        public bool IsAracheiveOrNot(int noteId, int userId);

        public bool IsTrashOrNot(int noteId, int userId);

        public bool DeleteNoteForever(int noteId,int userId);

        public NoteEntity getNoteById(int noteId);

        public string UploadImage(int noteId, IFormFile image, int userId);

    }
}
