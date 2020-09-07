using HRA.Common.Helpers;
using HRA.Common.Models;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HRA.Common.Interfaces;

namespace HRA.BLL
{
    public class EmailService: IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        
        public async Task<bool> SendEmail(MailRequest mailRequest)
        {
            try
            {
                var smtp = new SmtpClient();
                var message = new MailMessage {From = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName)};
                message.To.Add(new MailAddress(mailRequest.ToEmail));
                message.Subject = mailRequest.Subject;
                if (mailRequest.Attachments != null)
                {
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length <= 0) continue;
                        await using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        var att = new Attachment(new MemoryStream(fileBytes), file.FileName);
                        message.Attachments.Add(att);
                    }
                }

                message.IsBodyHtml = false;
                message.Body = mailRequest.Body;
                smtp.Port = _mailSettings.Port;
                smtp.Host = _mailSettings.Host;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Log the exception here
            }

            return false;
        }
         
    }
}
