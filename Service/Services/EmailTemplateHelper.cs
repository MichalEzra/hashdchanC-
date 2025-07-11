using Repository.Entities;
using Service.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{

    public static class EmailTemplateHelper
    {
        public static async Task<string> GenerateMatchEmailBody(IMyDetails<Candidate> candidateService, Candidate receiver, Candidate proposedMatch, string callbackUrl)
        {
            var generalInfo = await candidateService.GetGeneralCandidateInfoAsync(proposedMatch);

            return $@"
    <html>
    <head>
        <style>
            body {{
                font-family: 'Arial', sans-serif;
                background-color: #f9f9f9;
                padding: 20px;
                text-align: center;
            }}
            .container {{
                max-width: 500px;
                margin: auto;
                background: #ffffff;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }}
            h2 {{
                color: #2c3e50;
            }}
            p {{
                font-size: 16px;
                color: #34495e;
                line-height: 1.5;
            }}
            .details {{
                background: #ecf0f1;
                padding: 10px;
                border-radius: 8px;
                margin: 15px 0;
                font-size: 14px;
                color: #2c3e50;
                white-space: pre-line;
                text-align: right;
            }}
            .button {{
                display: inline-block;
                background-color: #3498db;
                color: white;
                padding: 12px 24px;
                text-decoration: none;
                font-size: 18px;
                font-weight: bold;
                border-radius: 5px;
                margin-top: 20px;
            }}
            .button:hover {{
                background-color: #2980b9;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 12px;
                color: #7f8c8d;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>✨ יש לך התאמה חדשה! ✨</h2>
            <p>שלום {receiver.FirstName} {receiver.LastName},</p>
            <p>מצאנו עבורך התאמה פוטנציאלית!</p>
            
            <div class='details'>
                <p><strong>פרטים על ההתאמה:</strong></p>
                <p>{generalInfo}</p>
            </div>

            <p>לחץ על הכפתור לקבלת פרטים נוספים:</p>
            <a class='button' href='{callbackUrl}'>🔍 אני מעוניין לקבל פרטים נוספים</a>

            <div class='footer'>
                <p>💌 הודעה זו נשלחה אליך על ידי מערכת השידוכים שלנו.</p>
                <p>אם קיבלת הודעה זו בטעות, אנא התעלם ממנה.</p>
            </div>
        </div>
    </body>
    </html>";
        }
        public static async Task<string> CreateEmailFullDetails(IMyDetails<Candidate> candidateService, Candidate receiver, Candidate proposedMatch, string callbackUrl)
        {
            var generalInfo = await candidateService.GetAllCandidateInfoAsync(proposedMatch);

            return $@"
    <html>
    <head>
        <style>
            body {{
                font-family: 'Arial', sans-serif;
                background-color: #f9f9f9;
                padding: 20px;
                text-align: center;
            }}
            .container {{
                max-width: 500px;
                margin: auto;
                background: #ffffff;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }}
            h2 {{
                color: #2c3e50;
            }}
            p {{
                font-size: 16px;
                color: #34495e;
                line-height: 1.5;
            }}
            .details {{
                background: #ecf0f1;
                padding: 10px;
                border-radius: 8px;
                margin: 15px 0;
                font-size: 14px;
                color: #2c3e50;
                white-space: pre-line;
                text-align: right;
            }}
            .button {{
                display: inline-block;
                background-color: #3498db;
                color: white;
                padding: 12px 24px;
                text-decoration: none;
                font-size: 18px;
                font-weight: bold;
                border-radius: 5px;
                margin-top: 20px;
            }}
            .button:hover {{
                background-color: #2980b9;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 12px;
                color: #7f8c8d;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>✨ המשך פרטים ! ✨</h2>
            <p>שלום {receiver.FirstName} {receiver.LastName},</p>
            
            <div class='details'>
                <p><strong>פרטים מלאים :</strong></p>
                <p>{generalInfo}</p>
            </div>


            <div class='footer'>
                <p>💌 הודעה זו נשלחה אליך על ידי מערכת השידוכים שלנו.</p>
                <p>אם קיבלת הודעה זו בטעות, אנא התעלם ממנה.</p>
            </div>
        </div>
    </body>
    </html>";
        }


        public static string GenerateAdminApprovalTemplate(string fullName, string email)
        {
            return $@"
    <html>
    <body style='font-family: Arial; text-align: right; direction: rtl;'>
        <h2>שדכן חדש נרשם למערכת</h2>
        <p>שם: {fullName}</p>
        <p>אימייל: {email}</p>
        <p>עליך לבדוק ולאשר את הבקשה דרך מערכת הניהול.</p>
        <br />
        <p style='color: gray; font-size: 12px;'>הודעה זו נשלחה אוטומטית ממערכת השידוכים</p>
    </body>
    </html>";
        }

    }

}
