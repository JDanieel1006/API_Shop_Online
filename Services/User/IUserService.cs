using API_Shop_Online.Dto.v1.User;

namespace API_Shop_Online.Services.User
{
    public interface IUserService
    {
        Task<string> AuthUser(string email, string password);

    }
}
