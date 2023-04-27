namespace FamilyBankWeb.Models
{


    public class TransactionModel
    {
        public int id { get; set; } 
        public int accountId { get; set; }
        public int userId { get; set; }
        public bool debit { get; set; }
        public int amount { get; set; }
        public DateTime createdDate { get; set; }
    }


}
