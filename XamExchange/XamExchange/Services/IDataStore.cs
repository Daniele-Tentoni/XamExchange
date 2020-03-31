using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamExchange.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddOrUpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
