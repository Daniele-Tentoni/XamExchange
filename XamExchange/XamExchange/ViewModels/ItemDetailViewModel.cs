namespace XamExchange.ViewModels
{
    using XamExchange.Models;

    public class ItemDetailViewModel : BaseViewModel
    {
        public CompleteCurrency Item { get; set; }
        public ItemDetailViewModel(CompleteCurrency item = null)
        {
            Title = item.Name;
            Item = item;
        }
    }
}
