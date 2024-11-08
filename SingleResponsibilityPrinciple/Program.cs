using System;
using System.Reflection;
using SingleResponsibilityPrinciple.AdoNet;
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();

            // URL to read trade file from
            string tradeURL = "http://faculty.css.edu/tgibbons/trades4.txt";

            ITradeValidator tradeValidator = new SimpleTradeValidator(logger);

            // Use URLTradeDataProvider to fetch trades from the specified URL
            ITradeDataProvider urlProvider = new URLTradeDataProvider(tradeURL, logger);

            ITradeMapper tradeMapper = new SimpleTradeMapper();
            ITradeParser tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);
            ITradeStorage tradeStorage = new AdoNetTradeStorage(logger);

            // Use URL provider in the TradeProcessor
            TradeProcessor tradeProcessor = new TradeProcessor(urlProvider, tradeParser, tradeStorage);

            // Process trades
            tradeProcessor.ProcessTrades();
        }
    }
}
