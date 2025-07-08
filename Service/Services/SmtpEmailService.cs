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
using System.Net.Mail;

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

        //// שליחת מייל כללי
        //public async Task SendEmailAsync(string toEmail, string subject, string body ,byte[]? attachment = null, string? attachmentName = null)
        //{
        //    using var client = new SmtpClient(); // יוצר לקוח SMTP חדש
        //    await client.ConnectAsync(_smtpServer, _port, SecureSocketOptions.StartTls); // מתחבר לשרת המייל בצורה מאובטחת
        //    await client.AuthenticateAsync(_senderEmail, _appPassword); // מבצע התחברות עם השולח

        //    var message = new MimeMessage(); // יוצר אובייקט מייל חדש
        //    message.From.Add(new MailboxAddress("שידוכים פלוס", _senderEmail)); // מוסיף את כתובת השולח למייל
        //    message.To.Add(new MailboxAddress(toEmail, toEmail)); // מוסיף את כתובת הנמען
        //    message.Subject = subject; // נושא ההודעה
        //    message.Body = new TextPart("html") { Text = body }; // גוף ההודעה בפורמט HTML

        //    await client.SendAsync(message); // שולח את המייל
        //    Console.WriteLine($"נשלח מייל אל: {toEmail}"); // מדפיס ללוג שהמייל נשלח
        //    await client.DisconnectAsync(true); // מתנתק מהשרת
        //}
        public async Task SendEmailAsync(string toEmail, string subject, string body, byte[]? attachment = null, string? attachmentName = null)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_smtpServer, _port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_senderEmail, _appPassword);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("השדכן", _senderEmail));
            message.To.Add(new MailboxAddress(toEmail, toEmail));
            message.Subject = subject;

            if (attachment != null && !string.IsNullOrEmpty(attachmentName))
            {
                var bodyPart = new TextPart("html")
                {
                    Text = body
                };

                var attachmentPart = new MimePart()
                {
                    Content = new MimeContent(new MemoryStream(attachment)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = attachmentName
                };

                var multipart = new Multipart("mixed");
                multipart.Add(bodyPart);
                multipart.Add(attachmentPart);

                message.Body = multipart;
            }
            else
            {
                message.Body = new TextPart("html") { Text = body };
            }

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }


        // שליחת מייל כאשר יש התאמה בין שני מועמדים
        public async Task SendMatchEmailAsync(int idCandidate1, int idCandidate2)
        {
            // המתנה לתוצאה מהשירות
            var c1Dto = await _candidateService.GetById(idCandidate1);
            var c2Dto = await _candidateService.GetById(idCandidate2);

            // מיפוי ל־Entity
            Candidate c1 = _mapper.Map<Candidate>(c1Dto);
            Candidate c2 = _mapper.Map<Candidate>(c2Dto);

            if (c1 == null || c2 == null)
                throw new Exception("One or both candidates were not found.");

            string baseUrl = "http://localhost:5245/api/Match/confirm";
            string callbackUrlC1 = $"{baseUrl}?candidateId={c1.Id}&matchId={c2.Id}";
            string callbackUrlC2 = $"{baseUrl}?candidateId={c2.Id}&matchId={c1.Id}";

            string emailBodyC1 = await EmailTemplateHelper.GenerateMatchEmailBody(_candidateMyDetails, c1, c2, callbackUrlC1);
            string emailBodyC2 = await EmailTemplateHelper.GenerateMatchEmailBody(_candidateMyDetails, c2, c1, callbackUrlC2);

            await SendEmailAsync(c1Dto.Email, "הצעת שידוך", emailBodyC1);
            await SendEmailAsync(c2Dto.Email, "הצעת שידוך", emailBodyC2);
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
