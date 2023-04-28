﻿namespace FamilyBankWeb.Models
{



    public class TransactionModel
    {
        public int transactionID { get; set; }
        public int accountID { get; set; }
        public int userID { get; set; }
        public bool debit { get; set; }
        public double amount { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime transactionDate { get; set; }
    }



}
