using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
     public class TextAnalyticsService
    {
        private readonly TextAnalyticsClient _client;

        public TextAnalyticsService(IConfiguration configuration)
        {
            // קריאה להגדרות מתוך appsettings.json
            var endpoint = configuration["AzureTextAnalytics:Endpoint"];
            var apiKey = configuration["AzureTextAnalytics:ApiKey"];
            var credentials = new AzureKeyCredential(apiKey);

            // יצירת לקוח שמחובר לשירות של Azure
            _client = new TextAnalyticsClient(new Uri(endpoint), credentials);
        }

        // פונקציה שמחלצת ביטויי מפתח מתוך טקסט
        public List<string> ExtractKeyPhrases(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            var result = _client.ExtractKeyPhrases(text);//מחלץ ביטויים משמעותיים מתוך טקסט
            return result.Value.ToList();
        }
    }
}
