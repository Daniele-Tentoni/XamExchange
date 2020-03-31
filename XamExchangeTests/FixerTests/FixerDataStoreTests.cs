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
            Assert.IsTrue(result.Success);
            var suc = result as LatestCurrency;
            var successful = (LatestCurrency)result;
            Assert.IsNotNull(successful.Base);
            Assert.IsNotNull(successful.Date);
            Assert.IsNotNull(successful.Rates);
            Assert.IsNotNull(successful.Timestamp);
        }
    }
}
