using ProjectManagment.Domain.Entities.Identity;

namespace ProjectManagment.Domain.Interfaces.ICore
{
    public interface IIdentityProvider
    {
        User GetUser();
    }
}