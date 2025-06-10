using Google.Cloud.Language.V1;
using System;

class Program
{
    static void Main(string[] args)
    {
        // הגדרת משתנה הסביבה למפתח השירות שלך
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\JSON\keywordextractionproject-3ab70ea3b3c3.json");

        // יצירת לקוח ל-Natural Language API
        var client = LanguageServiceClient.Create();

        // הטקסט לניתוח
        string text = "I am a smart, efficient, and hardworking person with many friends who loves hiking and dancing.";
        var document = new Document
        {
            Content = text,
            Type = Document.Types.Type.PlainText,
            Language = "en"
        };

        // קריאה לניתוח הסנטימנט
        var response = client.AnalyzeSentiment(document);

        // הדפסת התוצאות
        Console.WriteLine($"Text: {text}");
        Console.WriteLine($"Sentiment score: {response.DocumentSentiment.Score}");
        Console.WriteLine($"Sentiment magnitude: {response.DocumentSentiment.Magnitude}");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
