using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfasces
{
    public interface IEmailService
    {
        // יממש כל מי ששולח אימייל
        Task SendEmailAsync(string toEmail, string subject, string body, byte[]? attachment = null, string? attachmentName = null);
        Task SendMatchEmailAsync(int idCandidate1, int idCandidate2);
        Task SendSingleMatchEmailAsync(int senderId, int receiverId);
        Task SendEmailToAdminForMatchmakerApproval(string fullName, string userEmail, string phoneNumber, Common.Dto.UserType userType);
        Task SendApprovalEmailToMatchmaker(string fullName, string email);


    }
}
