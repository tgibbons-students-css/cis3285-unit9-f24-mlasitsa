using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;
using System.Collections.Generic;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class URLTradeDataProviderTests
    {
        private int countStrings(IEnumerable<string> collectionOfStrings)
        {
            // Count the trades
            int count = 0;
            foreach (var trade in collectionOfStrings)
            {
                count++;
            }
            return count;
        }

        [TestMethod()]
        public void TestSize1()
        {
            // Arrange
            ILogger logger = new ConsoleLogger();
            string tradeURL = "http://faculty.css.edu/tgibbons/trades4.txt";

            ITradeDataProvider tradeProvider = new URLTradeDataProvider(tradeURL, logger);

            // Act
            IEnumerable<string> trades = tradeProvider.GetTradeData(); // Use synchronous method

            // Assert
            Assert.AreEqual(countStrings(trades), 4, "Expected 4 trades to be returned.");
        }
    }
}
