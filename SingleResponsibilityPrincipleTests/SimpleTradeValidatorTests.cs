using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class SimpleTradeValidatorTests
    {
        [TestMethod()]
        public void TestGoodCurrencyString()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "4444", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestShortCurrencyString()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAA", "4444", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestLongCurrencyString()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAAABBBB", "4444", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestNormalTrade()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "4444", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestAmount999()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "999", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestAmount1000()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "1000", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestAmountNeg10000()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "-10000", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestAmount100k()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "1000000", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestAmount101k()
        {
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "1000001", "1.00" };
            bool result = tradeValidator.Validate(strData);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestEmptyCurrencyString()
        {
            // Test for an empty currency string
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "", "1000", "1.00" }; // Empty currency code
            bool result = tradeValidator.Validate(strData);
            Assert.IsFalse(result, "Validation should fail for an empty currency string.");
        }
    }
}
