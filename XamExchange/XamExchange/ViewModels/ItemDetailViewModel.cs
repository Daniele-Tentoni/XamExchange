using System;

using XamExchange.Models;

namespace XamExchange.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Currency Item { get; set; }
        public ItemDetailViewModel(Currency item = null)
        {
            Title = item.Date;
            Item = item;
        }
    }
}
