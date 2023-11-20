using Common_Layer.Models;
using Repository_Layer.Entity;
using System.Collections.Generic;

namespace Repository_Layer.Interfaces
{
    public interface ILableRepo
    {
        public LableEntity AddLable(LableModel model, int noteId, int userId);
        public List<LableEntity> GetAllLable();

        public bool UpdateLable(int lableId, LableModel model, int userId);

        public bool DeleteLable(int lableId,int userId);
    }
}