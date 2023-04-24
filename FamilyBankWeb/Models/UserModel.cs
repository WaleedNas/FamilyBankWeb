using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FamilyBankWeb.Models
{
    public class UserModel
    {

        public int userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly dateOfBirth { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please enter only numbers for the phone number.")]
        public string mobileNumber { get; set; }
    }


}
