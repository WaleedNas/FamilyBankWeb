namespace FamilyBankWeb.Models
{
    public class LoginResponse
    {
        public string message { get; set; }
        public UserModel user { get; set; }
        public string token { get; set; }
    }
}
