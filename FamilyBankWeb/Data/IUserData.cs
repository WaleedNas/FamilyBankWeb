using FamilyBankWeb.Models;

namespace FamilyBankWeb.Data
{
    public interface IUserData
    {
        Task<bool> IsUserLoggedIn();
        Task<HttpResponseMessage> CreateUser(UserModel userModel);
        Task<UserModel> GetUser(int id);
    }
}