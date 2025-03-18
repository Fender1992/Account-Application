namespace AccountAPI.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public double Balance { get; set; }
        public string CurrencyCode { get; set; }
        public bool CanEdit { get; set; }
        public bool Status { get; set; }
    }
}
