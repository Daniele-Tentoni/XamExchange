namespace XamExchange.Services
{
    using Realms;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class CurrencyDataStore<T> : IDataStore<T> where T: RealmObject
    {
        public Task<bool> AddOrUpdateItemAsync(T item)
        {
            var result = true;
            using (var realm = Realm.GetInstance())
            using (var transition = realm.BeginWrite())
            {
                try
                {
                    realm.Add(item, true);
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
                var item = realm.All<T>().First();
                if (item == null) return false;

                using (var transition = realm.BeginWrite())
                {
                    realm.Remove(item);
                    transition.Commit();
                    return true;
                }
            }
        }

        public async Task<T> GetItemAsync(string id)
        {
            using (var realm = await Realm.GetInstanceAsync())
                return realm.All<T>().FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            /*
            var rateList = new List<Rate>();
            if (forceRefresh)
            {
                var fixer = new FixerDataStore();
                var latest = await fixer.GetLatestCurrencyExchange();
                if (latest.Success)
                    rateList = ((LatestCurrency)latest).Rates.RateList;
                else {
                    var error = (FixerResponseError)latest;
                    Debug.WriteLine($"Error received from")
            }
            */
            using (var realm = await Realm.GetInstanceAsync())
                return realm.All<T>();

        }
    }
}
