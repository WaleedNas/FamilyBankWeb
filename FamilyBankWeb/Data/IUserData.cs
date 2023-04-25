using FamilyBankWeb.Models;

namespace FamilyBankWeb.Data
{
    public interface IUserData
    {
        Task<HttpResponseMessage> CreateUser(UserModel userModel);
        Task<UserModel> GetUser(int id);
        Task<UserModel> GetUserFromContext(string claim);
    }
}