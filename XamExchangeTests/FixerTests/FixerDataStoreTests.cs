namespace XamExchangeTests.FixerTests
{
    using NUnit.Framework;
    using XamExchange.Models;
    using XamExchange.Services;

    class FixerDataStoreTests
    {
        public FixerDataStore fixerDataStore = null;

        [SetUp]
        public void SetUp() => this.fixerDataStore = new FixerDataStore();

        [Test]
        public void TestInizialization() => Assert.IsNotNull(this.fixerDataStore);

        [Test]
        public void TestLatestCurrency()
        {
            var result = this.fixerDataStore.GetLatestCurrencyExchange().Result;
            Assert.IsTrue(result.IsSuccessful());
            var successful = (Currency)result;
            Assert.IsNotNull(successful.Base);
            Assert.IsNotNull(successful.Date);
            Assert.IsNotNull(successful.Rates);
            Assert.IsNotNull(successful.Timestamp);
        }

        [Test] public void TestGetAllCurrencySymbols()
        {
            var result = this.fixerDataStore.GetAllCurrencySymbols().Result;
            Assert.IsTrue(result.IsSuccessful());
            var success = result as Symbols;
            Assert.IsNotNull(success.SymbolDictionary);
            var usd = success.SymbolDictionary["USD"];
            Assert.AreEqual("United States Dollar", usd);
        }

        [Test] public void TestAddRealmCurrency()
        {
            var symbols = this.fixerDataStore.GetAllCurrencySymbols().Result;
            Assert.IsTrue(symbols.IsSuccessful());

            var currencies = this.fixerDataStore.GetLatestCurrencyExchange().Result;
            Assert.IsTrue(currencies.IsSuccessful());

            var aeds = ((Symbols)symbols).SymbolDictionary["AED"];
            var aedc = ((Currency)currencies).Rates["AED"];
            var realmEntity = new RealmCurrency
            {
                Code = "AED",
                Name = aeds,
                Rate = (double)aedc
            };

            var addResult = new CurrencyDataStore().AddOrUpdateItemAsync(realmEntity).Result;
            Assert.IsTrue(addResult);
        }

        [Test] public void TestGetRealmCurrency()
        {
            var usdr = new CurrencyDataStore().GetItemAsync("AED").Result;
            Assert.AreEqual("United Arab Emirates Dirham", usdr.Rate);
        }
    }
}
