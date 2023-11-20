using Common_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class CollabRepo : ICollabRepo
    {

        private readonly FundoDbContext fundoocontext;

        public CollabRepo(FundoDbContext fundoocontext)
        {
            this.fundoocontext = fundoocontext;
        }

        public CollabaratorEntity AddCollab(CollabModel model, int noteId,int userId)
        {
            
            CollabaratorEntity collaborator = new CollabaratorEntity();
            collaborator.CollaboratorEmail =model.collabEmail;
            collaborator.NoteId = noteId;
            collaborator.UserId = userId;
            fundoocontext.Collabarators.Add(collaborator);
            var result = fundoocontext.SaveChanges();

            if (result > 0)
            {
                return collaborator;
            }
            else
            {
                return null;
            }
        }
        public bool DeletedCollaborator(int collaboratorId)
        {
            var result=fundoocontext.Collabarators.FirstOrDefault(x=>x.Id==collaboratorId);
            if(result != null)
            {
                fundoocontext.Collabarators.Remove(result);
                fundoocontext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
