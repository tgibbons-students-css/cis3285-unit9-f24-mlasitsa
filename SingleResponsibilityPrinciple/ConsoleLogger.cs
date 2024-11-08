using SingleResponsibilityPrinciple.Contracts;
using System;
using System.IO;
using System.Xml.Linq;

namespace SingleResponsibilityPrinciple
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _logFilePath = "TradeLogs.xml";

        public ConsoleLogger()
        {
            // Initialize the XML log file with a root element if it doesn't exist
            if (!File.Exists(_logFilePath))
            {
                var root = new XElement("logs");
                root.Save(_logFilePath);
            }
        }

        public void LogInfo(string message)
        {
            Console.WriteLine("INFO: " + message);
            LogToXml("INFO", message);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine("WARN: " + message);
            LogToXml("WARN", message);
        }

        private void LogToXml(string type, string message)
        {
            var logEntry = new XElement("log",
                new XElement("type", type),
                new XElement("message", message)
            );

            // Load the existing XML file and add the new log entry
            var doc = XDocument.Load(_logFilePath);
            doc.Root.Add(logEntry);
            doc.Save(_logFilePath);
        }
    }
}
