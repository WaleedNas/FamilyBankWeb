namespace FamilyBankWeb.Models
{

    public class TransferModel
    {
        public int transferID { get; set; }
        public int fromUserID { get; set; }
        public int fromAccountID { get; set; }
        public int toUserID { get; set; }
        public int toAccountID { get; set; }
        public double amount { get; set; }
        public DateTime transferDate { get; set; }
        public int transferFrequency { get; set; }
        public DateTime endDate { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isCompleted { get; set; }
    }

}
