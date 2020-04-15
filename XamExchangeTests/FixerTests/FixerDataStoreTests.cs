namespace XamExchangeTests.FixerTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using XamExchange.Models;
    using XamExchange.Models.FixerModels;
    using XamExchange.Services;

    class FixerDataStoreTests
    {
        private const string EUR = "EUR";
        private const string USD = "USD";
        private const string AED = "AED";
        public FixerDataStore fixerDataStore = null;

        [SetUp]
        public void SetUp() => this.fixerDataStore = new FixerDataStore();

        [Test]
        public void TestInizialization() => Assert.IsNotNull(this.fixerDataStore);

        [Test]
        public async Task TestLatestCurrency()
        {
            var result = await this.fixerDataStore.GetLatestCurrencyExchange();
            Assert.IsTrue(result.IsSuccessful());
            var successful = (Currency)result;
            Assert.AreEqual(EUR, successful.Base);
            Assert.IsNotNull(successful.Date);
            Assert.IsNotNull(successful.Rates);
            Assert.IsNotNull(successful.Timestamp);
        }

        [Test] public async Task TestGetAllCurrencySymbols()
        {
            var result = await this.fixerDataStore.GetAllCurrencySymbols();
            Assert.IsTrue(result.IsSuccessful());
            var success = result as AllSymbols;
            Assert.IsNotNull(success.Symbols);
            var usd = success.Symbols[USD];
            Assert.AreEqual("United States Dollar", usd);
        }

        [Test] public async Task TestFluctuationEndpoint()
        {
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now;
            var result = await this.fixerDataStore.GetFluctuationFromDateToDate(from, to);

            // Must be false because this is a payment function.
            Assert.IsFalse(result.IsSuccessful());
        }

        [Test] public async Task TestAddRealmCurrency()
        {
            var tasks = new List<Task>();
            var symbols = this.fixerDataStore.GetAllCurrencySymbols();
            tasks.Add(symbols);

            var currencies = this.fixerDataStore.GetLatestCurrencyExchange();
            tasks.Add(currencies);

            await Task.WhenAll(tasks);
            Assert.IsTrue(symbols.Result.IsSuccessful());
            Assert.IsTrue(currencies.Result.IsSuccessful());

            var aeds = ((AllSymbols)symbols.Result).Symbols[AED];
            var aedc = ((Currency)currencies.Result).Rates[AED];
            var realmEntity = new CompleteCurrency
            {
                Code = AED,
                Name = aeds,
                Rate = (double)aedc
            };

            var addResult = new CurrencyDataStore().AddOrUpdateItemAsync(realmEntity).Result;
            Assert.IsTrue(addResult);
        }

        [Test] public void TestGetRealmCurrency()
        {
            var usdr = new CurrencyDataStore().GetItemAsync("AED").Result;
            Assert.AreEqual("United Arab Emirates Dirham", usdr.Name);
        }
    }
}
