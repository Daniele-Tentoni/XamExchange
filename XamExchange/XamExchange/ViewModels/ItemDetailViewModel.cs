namespace XamExchange.ViewModels
{
    using XamExchange.Models;

    public class ItemDetailViewModel : BaseViewModel
    {
        public CompleteCurrency Item { get; set; }
        public ItemDetailViewModel(CompleteCurrency item = null)
        {
            this.Title = item.Name;
            this.Item = item;
        }
    }
}
