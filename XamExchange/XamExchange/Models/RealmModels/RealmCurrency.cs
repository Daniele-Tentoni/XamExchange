namespace XamExchange.Models
{
    using Realms;

    public class RealmCurrency: RealmObject
    {
        [PrimaryKey, Indexed]
        public string Code { get; set; }

        public string Name { get; set; }

        public double Rate { get; set; }

        public RealmCurrency() { }

        public RealmCurrency(CompleteCurrency currency)
        {
            Code = currency.Code;
            Name = currency.Name;
            Rate = currency.Rate;
        }
    }
}
