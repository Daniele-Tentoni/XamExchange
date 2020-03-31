namespace XamExchange.Services
{
    using Realms;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using XamExchange.Models;

    class RateDataStore : IDataStore<Rate>
    {
        public async Task<bool> AddOrUpdateItemAsync(Rate item)
        {
            var result = true;
            using (var realm = await Realm.GetInstanceAsync())
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
                return result;
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            using(var realm = await Realm.GetInstanceAsync())
            {
                var item = realm.All<Rate>().First(w => w.Name == id);
                if (item == null) return false;

                using (var transition = realm.BeginWrite())
                {
                    realm.Remove(item);
                    transition.Commit();
                    return true;
                }
            }
        }

        public async Task<Rate> GetItemAsync(string id)
        {
            using (var realm = await Realm.GetInstanceAsync())
                return realm.All<Rate>().FirstOrDefault(f => f.Name == id);
        }

        public async Task<IEnumerable<Rate>> GetItemsAsync(bool forceRefresh = false)
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
                return realm.All<Rate>();

        }
    }
}
