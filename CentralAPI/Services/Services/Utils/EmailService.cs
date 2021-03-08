using CentralAPI.Services.IServices;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace CentralAPI.Services.Services
{
    public class EmailService
    {
        private readonly IUserService _userService;
        
        public EmailService(IUserService userService)
        {
            _userService = userService;
        }

        public async System.Threading.Tasks.Task SendQRToEmailAsync(byte[] qr, string id, string ID)
        {
            var user = await _userService.GetUserById(id);
            string from = "ingloriousdevelopers.upskill@gmail.com";
            string to = user.Value.Email.ToString();
            var attachment = new Attachment(new MemoryStream(qr), "QrForReservation", "image/png");
            using (MailMessage mail = new MailMessage(from, to))
            {
                mail.Subject = "Reservation " + ID + " was successful!";
                mail.Body = "Hello! \n We from Inglorious Developers are happy to tell you that your reservation " 
                    + ID + " was successful! \n You will find atached to this email your own qr code with your reservation info. " +
                    "\n Please present it when you use the Parking Spot you picked!";
                mail.Attachments.Add(attachment);

                mail.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from, "ThisIsForTesting123");
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
                
            }
        }
    }
}
