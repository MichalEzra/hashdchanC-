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
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendMatchEmailAsync(int idCandidate1, int idCandidate2);
    }
}
