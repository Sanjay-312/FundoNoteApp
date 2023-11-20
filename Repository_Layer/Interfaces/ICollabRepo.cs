using Common_Layer.Models;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
    public interface ICollabRepo
    {
        CollabaratorEntity AddCollab(CollabModel model, int noteId,int userId);

        public bool DeletedCollaborator(int collaboratorId);
    }
}