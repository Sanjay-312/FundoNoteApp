using Common_Layer.Models;
using Repository_Layer.Entity;

namespace Bussiness_Layer.Interfaces
{
    public interface ICollabBusiness
    {
        CollabaratorEntity AddCollab(CollabModel model, int noteId, int userId);
        public bool DeletedCollaborator(int collaboratorId);
    }
}