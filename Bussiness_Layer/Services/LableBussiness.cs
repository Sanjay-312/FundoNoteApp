using Bussiness_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_Layer.Services
{
    public class LableBussiness : ILableBussiness
    {
        private readonly ILableRepo lableRepo;
        public LableBussiness(ILableRepo lableRepo)
        {
            this.lableRepo = lableRepo;
        }

        public LableEntity AddLable(LableModel model, int noteId, int userId)
        {
            return lableRepo.AddLable(model,noteId,userId);
        }
        public List<LableEntity> GetAllLable()
        {
            return lableRepo.GetAllLable();
        }

        public bool UpdateLable(int lableId, LableModel model, int userId)
        {
            return lableRepo.UpdateLable(lableId, model,userId);
        }

        public bool DeleteLable(int lableId,int userId)
        {
            return lableRepo.DeleteLable(lableId,userId);
        }
    }
}
