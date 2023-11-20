using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_Layer.Services
{
    public class CollabBusiness : ICollabBusiness
    {
        private readonly ICollabRepo collabRepo;
        public CollabBusiness(ICollabRepo collabRepo)
        {
            this.collabRepo = collabRepo;
        }

        public CollabaratorEntity AddCollab(CollabModel model, int noteId,int userId)
        {
            return collabRepo.AddCollab(model, noteId,userId);
        }

        public bool DeletedCollaborator(int collaboratorId)
        {
            return collabRepo.DeletedCollaborator(collaboratorId);
        }
    }
}
