using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FamilyBankWeb.Models
{

    public class AccountModel
    {
        public int accountID { get; set; }
        public int creatorID { get; set; }
        public double balance { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly createdAt { get; set; }
    }

}
