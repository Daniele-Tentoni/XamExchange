namespace XamExchange.Models
{
    public class CompleteCurrency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }

        public CompleteCurrency() { }

        public CompleteCurrency(RealmCurrency realm)
        {
            Code = realm.Code;
            Name = realm.Name;
            Rate = realm.Rate;
        }
    }
}
