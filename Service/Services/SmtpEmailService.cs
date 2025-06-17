using AutoMapper;
using Common.Dto;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Repository.Entities;
using Service.Interfaces;
using Service.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly string _smtpServer; // כתובת שרת ה-SMTP לשליחת מיילים
        private readonly int _port; // הפורט של שרת ה-SMTP
        private readonly string _senderEmail; // כתובת האימייל ששולחת את ההודעות
        private readonly string _appPassword; // סיסמת האפליקציה (לא סיסמה רגילה) עבור האימייל
        private readonly IMapper _mapper; // Mapper של AutoMapper להמרת DTO ל-Entity
        private readonly IService<CandidateDto> _candidateService; // שירות לשליפת מועמדים לפי ID (DTO)
        private readonly IMyDetails<Candidate> _candidateMyDetails; // שירות לשליפת פרטי מועמד
        private readonly IServiceMatch _serviceMatch; // שירות להתעסקות עם הצעות שידוך

        // קונסטרקטור שמקבל תלות בהגדרות ובשירותים הדרושים
        public SmtpEmailService(IConfiguration configuration, IService<CandidateDto> candidateService, IMapper mapper, IMyDetails<Candidate> candidateMyDetails, IServiceMatch serviceMatch)
        {
            _smtpServer = configuration["Gmail:SmtpServer"]; // טוען את כתובת שרת ה-SMTP מה- appsettings
            _port = int.Parse(configuration["Gmail:Port"]); // טוען את הפורט מה- appsettings
            _senderEmail = configuration["Gmail:SenderEmail"]; // טוען את כתובת השולח
            _appPassword = configuration["Gmail:AppPassword"]; // טוען את סיסמת האפליקציה
            _candidateService = candidateService; // מאחסן את השירות של המועמדים
            _mapper = mapper; // מאחסן את הממפה
            _candidateMyDetails = candidateMyDetails; // מאחסן את השירות שמביא מידע נוסף על מועמדים
            _serviceMatch = serviceMatch; // מאחסן את השירות של השידוכים
        }

        // שליחת מייל כללי
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var client = new SmtpClient(); // יוצר לקוח SMTP חדש
            await client.ConnectAsync(_smtpServer, _port, SecureSocketOptions.StartTls); // מתחבר לשרת המייל בצורה מאובטחת
            await client.AuthenticateAsync(_senderEmail, _appPassword); // מבצע התחברות עם השולח

            var message = new MimeMessage(); // יוצר אובייקט מייל חדש
            message.From.Add(new MailboxAddress("שידוכים פלוס", _senderEmail)); // מוסיף את כתובת השולח למייל
            message.To.Add(new MailboxAddress(toEmail, toEmail)); // מוסיף את כתובת הנמען
            message.Subject = subject; // נושא ההודעה
            message.Body = new TextPart("html") { Text = body }; // גוף ההודעה בפורמט HTML

            await client.SendAsync(message); // שולח את המייל
            Console.WriteLine($"נשלח מייל אל: {toEmail}"); // מדפיס ללוג שהמייל נשלח
            await client.DisconnectAsync(true); // מתנתק מהשרת
        }

        // שליחת מייל כאשר יש התאמה בין שני מועמדים
        public async Task SendMatchEmailAsync(int idCandidate1, int idCandidate2)
        {
            Candidate c1 = _mapper.Map<Candidate>(_candidateService.GetById(idCandidate1)); // ממפה את המועמד הראשון מה-DTO
            Candidate c2 = _mapper.Map<Candidate>(_candidateService.GetById(idCandidate2)); // ממפה את המועמד השני מה-DTO

            if (c1 == null || c2 == null) // בדיקה ששני המועמדים קיימים
                throw new Exception("One or both candidates were not found."); // זריקת שגיאה אם לא

            string baseUrl = "https://localhost:7242/api/Match/confirm"; // URL בסיסי להמשך טיפול בהתאמה
            string callbackUrlC1 = $"{baseUrl}?candidateId={c1.Id}&matchId={c2.Id}"; // קישור עבור המועמד הראשון
            string callbackUrlC2 = $"{baseUrl}?candidateId={c2.Id}&matchId={c1.Id}"; // קישור עבור המועמד השני

            // יצירת גוף המייל בהתאמה אישית לשני המועמדים
            string emailBodyC1 = EmailTemplateHelper.GenerateMatchEmailBody(_candidateMyDetails, c1, c2, callbackUrlC1);
            string emailBodyC2 = EmailTemplateHelper.GenerateMatchEmailBody(_candidateMyDetails, c2, c1, callbackUrlC2);

            await SendEmailAsync(c1.User.Email, "הצעת שידוך", emailBodyC1); // שליחת מייל למועמד הראשון
            await SendEmailAsync(c2.User.Email, "הצעת שידוך", emailBodyC2); // שליחת מייל למועמד השני
        }

        //// שליחת מייל לכל השדכנים הפעילים עם תזכורת לעדכון הצעות
        //public async Task sendEmailToMatchmakerActiveMatch()
        //{
        //    List<Matchmaker> matchmakers = _serviceMatch.GetAllMatchmakerActives(); // שליפת כל השדכנים הפעילים
        //    foreach (Matchmaker matchmaker in matchmakers) // מעבר על כל שדכן
        //    {
        //        await SendEmailAsync(matchmaker.Email, "עדכן את ההצעה", // שליחת מייל עם הודעת תזכורת
        //            "שלום " + matchmaker.LastName + " " + matchmaker.FirstName + ", עדכן את השידוכים שלך באתר.");
        //    }
        //}
    }
}
