namespace FamilyBankWeb.Models
{

    public class AccountUserModel
    {
        public int accountID { get; set; }
        public int userID { get; set; }
        public bool isMain { get; set; }
        public double balanceAvailable { get; set; }
    }

}
