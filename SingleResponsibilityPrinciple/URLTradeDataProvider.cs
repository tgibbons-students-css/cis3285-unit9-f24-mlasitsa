using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLTradeDataProvider : ITradeDataProvider
    {
        private readonly string _url;
        private readonly ILogger _logger;

        public URLTradeDataProvider(string url, ILogger logger)
        {
            _url = url;
            _logger = logger;
        }

        public async Task<IEnumerable<string>> GetTradeDataAsync()
        {
            List<string> tradeData = new List<string>();
            _logger.LogInfo("Reading trades from URL: " + _url);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_url);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInfo("Failed to fetch trade data from URL: " + _url);  // Using LogInfo as a substitute for LogError
                    throw new Exception("Error fetching trade data");
                }

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        tradeData.Add(line);
                    }
                }
            }

            return tradeData;
        }

        public IEnumerable<string> GetTradeData()
        {
            return GetTradeDataAsync().GetAwaiter().GetResult();
        }
    }
}
