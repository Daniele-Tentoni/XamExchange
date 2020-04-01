namespace XamExchange.Models
{
    public enum MenuItemType
    {
        Exchange,
        Currencies,
        About
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
