namespace XamExchange.Models
{
    using Realms;

    public class Rate : RealmObject
    {
        [PrimaryKey, Indexed]
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
