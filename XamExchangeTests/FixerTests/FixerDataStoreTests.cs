namespace XamExchangeTests.FixerTests
{
    using System;
    using NUnit.Framework;
    using XamExchange.Models;
    using XamExchange.Models.FixerModels;
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
            var result = this.fixerDataStore.GetLatestCurrencyExchange();
            Assert.IsTrue(result.IsSuccessful());
            var successful = (Currency)result;
            Assert.IsNotNull(successful.Base);
            Assert.IsNotNull(successful.Date);
            Assert.IsNotNull(successful.Rates);
            Assert.IsNotNull(successful.Timestamp);
        }

        [Test] public void TestGetAllCurrencySymbols()
        {
            var result = this.fixerDataStore.GetAllCurrencySymbols();
            Assert.IsTrue(result.IsSuccessful());
            var success = result as AllSymbols;
            Assert.IsNotNull(success.Symbols);
            var usd = success.Symbols["USD"];
            Assert.AreEqual("United States Dollar", usd);
        }

        [Test] public void TestFluctuationEndpoint()
        {
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now;
            var result = this.fixerDataStore.GetFluctuationFromDateToDate(from, to);
            Assert.IsFalse(result.IsSuccessful());
        }

        [Test] public void TestAddRealmCurrency()
        {
            var symbols = this.fixerDataStore.GetAllCurrencySymbols();
            Assert.IsTrue(symbols.IsSuccessful());

            var currencies = this.fixerDataStore.GetLatestCurrencyExchange();
            Assert.IsTrue(currencies.IsSuccessful());

            var aeds = ((AllSymbols)symbols).Symbols["AED"];
            var aedc = ((Currency)currencies).Rates["AED"];
            var realmEntity = new CompleteCurrency
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
            Assert.AreEqual("United Arab Emirates Dirham", usdr.Name);
        }
    }
}
