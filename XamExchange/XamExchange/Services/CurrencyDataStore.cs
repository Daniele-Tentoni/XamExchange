namespace XamExchange.Services
{
    using Realms;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using XamExchange.Models;

    public class CurrencyDataStore : IDataStore<CompleteCurrency>
    {
        public Task<bool> AddOrUpdateItemAsync(CompleteCurrency item)
        {
            var result = true;
            using (var realm = Realm.GetInstance())
            using (var transition = realm.BeginWrite())
            {
                try
                {
                    realm.Add(new RealmCurrency(item), true);
                    transition.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Exception thrown by {nameof(realm)}: {e.Message}");
                    transition.Rollback();
                    result = false;
                }
                finally
                {
                    Debug.WriteLine($"Add Rate {item}");
                }
                return Task.FromResult(result);
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            using(var realm = await Realm.GetInstanceAsync())
            {
                var item = realm.All<RealmCurrency>().First(f => f.Code == id);
                if (item == null) return false;

                using (var transition = realm.BeginWrite())
                {
                    realm.Remove(item);
                    transition.Commit();
                    return true;
                }
            }
        }

        public async Task<CompleteCurrency> GetItemAsync(string id)
        {
            using (var realm = await Realm.GetInstanceAsync())
            {
                var elem = realm.All<RealmCurrency>().FirstOrDefault(f => f.Code == id);
                return new CompleteCurrency(elem);
            }
        }

        public async Task<IEnumerable<CompleteCurrency>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var realm = await Realm.GetInstanceAsync())
                return realm.All<RealmCurrency>().Select(s => new CompleteCurrency(s));

        }
    }
}
