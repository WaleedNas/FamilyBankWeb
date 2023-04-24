namespace FamilyBankWeb.Models
{

    public class CardModel
    {
        public int cardID { get; set; }
        public int accountID { get; set; }
        public string cardNumber { get; set; }
        public string cardHolderName { get; set; }
        public string cardType { get; set; }
        public string cardCVV { get; set; }
        public DateTime expiryDate { get; set; }
        public DateTime createdDate { get; set; }
    }

}
