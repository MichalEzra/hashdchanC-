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
using Repository.Repositories;
using Repository.Interfaces;
using UserType = Common.Dto.UserType;

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
        private readonly IService<UserDto> _userService; // שירות לשליפת משתמשים לפי ID (DTO)
        private readonly IMyDetails<Candidate> _candidateMyDetails; // שירות לשליפת פרטי מועמד
        private readonly IServiceMatch _serviceMatch; // שירות להתעסקות עם הצעות שידוך
        private readonly IUserRepository<User> _userRepository; // שירות להתעסקות עם הצעות שידוך

        //private readonly Lazy<IService<UserDto>> _userService;


        // קונסטרקטור שמקבל תלות בהגדרות ובשירותים הדרושים
        public SmtpEmailService(
    IConfiguration configuration,
    IService<CandidateDto> candidateService,
    IMapper mapper,
    IMyDetails<Candidate> candidateMyDetails,
    IServiceMatch serviceMatch,
    IService<UserDto> userService,
    IUserRepository<User> userRepository)
        {
            _smtpServer = configuration["Gmail:SmtpServer"];
            _port = int.Parse(configuration["Gmail:Port"]);
            _senderEmail = configuration["Gmail:SenderEmail"];
            _appPassword = configuration["Gmail:AppPassword"];
            _candidateService = candidateService;
            _mapper = mapper;
            _candidateMyDetails = candidateMyDetails;
            _serviceMatch = serviceMatch;
            _userService = userService;
            _userRepository = userRepository;
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
        public async Task SendSingleMatchEmailAsync(int senderId, int receiverId)
        {

            var receiverEntity = await _candidateService.GetById(receiverId);

            if (receiverEntity == null)
                throw new Exception($"מועמד עם מזהה {receiverId} לא נמצא.");


            int userId = receiverEntity.UserId;
            if (userId <= 0)
                throw new Exception("מזהה המשתמש אינו תקין");

            var userEntity = await _userService.GetById(userId);


            if (string.IsNullOrWhiteSpace(userEntity.Email))
                throw new Exception("למשתמש אין כתובת אימייל");

            var senderDto = await _candidateService.GetById(senderId);
            var sender = _mapper.Map<Candidate>(senderDto);
            Candidate receiver = _mapper.Map<Candidate>(receiverEntity);


            string baseUrl = "http://localhost:5245/api/Match/confirm";
            string callbackUrl = $"{baseUrl}?candidateId={receiverEntity.Id}&matchId={sender.Id}";

            string emailBody = await EmailTemplateHelper.GenerateMatchEmailBody(_candidateMyDetails, receiver, sender, callbackUrl);

            Console.WriteLine($"שולח מייל אל: {userEntity.Email}");
            await SendEmailAsync(userEntity.Email, "הצעת שידוך חדשה", emailBody);

        }
        public async Task SendEmailToAdminForMatchmakerApproval(string fullName, string userEmail, string phoneNumber, UserType userType)
        {
            var admins = await _userRepository.GetUsersByTypeFromRepository(Repository.Entities.UserType.ADMIN);
            var subject = "בקשה לאישור שדכן חדש";

            var body = $@"
        <div style='font-family:Arial,sans-serif;'>
            <h2 style='color:#2c3e50;'>בקשה לאישור שדכן חדש</h2>
            <p>התקבלה בקשה להירשם כ<strong>שדכן</strong>. להלן פרטי המשתמש:</p>
            <ul>
                <li><strong>שם מלא:</strong> {fullName}</li>
                <li><strong>אימייל:</strong> {userEmail}</li>
                <li><strong>טלפון:</strong> {phoneNumber}</li>
            </ul>
            <p>לאישור השדכן, לחצו על הכפתור הבא:</p>
            <a href='https://your-domain.com/approve-matchmaker?email={Uri.EscapeDataString(userEmail)}' 
               style='display:inline-block;padding:10px 20px;background-color:#28a745;color:white;
                      text-decoration:none;border-radius:5px;font-weight:bold;'>
                אשר שדכן
            </a>
            <p style='margin-top:20px;'>אם אינך מזהה בקשה זו, ניתן להתעלם מהמייל.</p>
        </div>";

            foreach (var admin in admins)
            {
                await SendEmailAsync(admin.Email, subject, body);
            }
        }
        public async Task SendApprovalEmailToMatchmaker(string fullName, string email)
        {
            var subject = "האישור שלך התקבל!";
            var body = $@"
        <div style='font-family:Arial;'>
            <h2 style='color:#28a745;'>שדכן יקר, בקשתך אושרה ✅</h2>
            <p>שלום {fullName},</p>
            <p>הבקשה שלך להירשם כשדכן אושרה על ידי המנהל. כעת באפשרותך להתחיל להשתמש במערכת.</p>
            <p>בהצלחה!</p>
        </div>";

            await SendEmailAsync(email, subject, body);
        }


        //public async Task SendEmailToAdminForMatchmakerApproval(string matchmakerFullName, string matchmakerEmail)
        //{
        //    string subject = "בקשת הרשמה של שדכן חדש";
        //    string body = EmailTemplateHelper.GenerateAdminApprovalTemplate(matchmakerFullName, matchmakerEmail);

        //    string adminEmail = "g0556780416@gmail.com"; // שנה לכתובת המייל האמיתית של המנהל או טען מ־appsettings

        //    await SendEmailAsync(adminEmail, subject, body);
        //}



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
