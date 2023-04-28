namespace FamilyBankWeb.Models
{

    public class ScheduledTransactions
    {
        public int scheduledTransactionID { get; set; }
        public int accountID { get; set; }
        public int userID { get; set; }
        public bool debit { get; set; }
        public double amount { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime transactionDate { get; set; }
        public int transactionFrequency { get; set; }
        public DateTime endDate { get; set; }
        public bool isCompleted { get; set; }
    }

}
