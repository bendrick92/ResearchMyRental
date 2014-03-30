using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Data;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    /// <summary>
    /// Sends a confirmation message upon making a reservation.
    /// </summary>
    /// <param name="recipient">Who to send the message to.</param>
    /// <param name="subject">The subject of the message.</param>
    /// <param name="messageBody">The body of the message.</param>
    public static void sendMessage(List<string> recipients, string subject, string messageIntro, string messageBody)
    {
        string body = "<html>" +
                            "<span>" + messageIntro + "</span>" +
                            "<br/><br/>" +
                            "<span style='white-space: pre-line'>" + messageBody + "</span>" +
                            "<br/><br/>" +
                            "<span style='font-style: italic;'>" + "Problems viewing this e-mail?  Questions or concerns?  Please contact <a href=\"mailto:support@researchmyrental.com\">support@researchmyrental.com</a>." + "</span>" +
                      "</html>";

        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        foreach (var item in recipients)
        {
            message.To.Add(item);
        }
        message.Subject = subject;
        message.From = new System.Net.Mail.MailAddress("walters.benj@gmail.com");
        message.IsBodyHtml = true;

        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
        message.AlternateViews.Add(htmlView);

        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential("walters.benj@gmail.com", "1n9a9n2a");
        smtp.EnableSsl = true;
        smtp.Send(message);
    }
}