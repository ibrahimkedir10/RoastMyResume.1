using resume_image.Models;

namespace resume_image.IRepository
{
    public interface IUserRepository
    {
        User Save(User user);

        User GetSaveUser();
    }
}
