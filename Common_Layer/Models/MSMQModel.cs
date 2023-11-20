using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Common_Layer.Models
{
    public class MSMQModel
    {
        MessageQueue mq = new MessageQueue();
        private string receiverEmail;
        private string receiverName;

        public void SendMessage(string token, string emailId, string name)
        {
            receiverEmail = emailId;
            receiverName = name;
            mq.Path = @".\Private$\Token";
            try
            {
                if (!MessageQueue.Exists(mq.Path))
                {
                    MessageQueue.Create(mq.Path);
                }
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                mq.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                mq.Send(token);
                mq.BeginReceive();
                mq.Close();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = mq.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                MailMessage mailMsg = new MailMessage();
                SmtpClient smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("sanjeevabodagalla@gmail.com", "ovww eegn bwit hmex"),
                };
                mailMsg.From = new MailAddress("sanjeevabodagalla@gmail.com");
                mailMsg.To.Add(new MailAddress(receiverEmail));
                string mailBody = $"<!DOCTYPE html>" +
                                    $"<html>" +
                                    $"<style>" +
                                    $".blink" +
                                    $"</style>" +
                                    $"<body style=\"background-color:#DBFF73;text-align:center;padding:5px\">" +
                                    $"<h1 style=\"color:#6A8D02;border-bottom:3px solid #84AF08;margin-top:5px;\"> Dear <b>{receiverName}</b></h1>\n" +
                                    $"<h3 style=\"color:#8AB411;\"> For Resetting Password The Below Link Is Issued<h3>" +
                                    $"<h3 style=\"color:#8AB411;\"> Please Click The link below to reset your password<h3>" +
                                    $"<a style=\"color:00802b; text-decoration:none; font-size:20px;\" href='http://localhost:4200/resetpassword/{token}'>Click me</a>\n" +
                                    $"<h3 style=\"color:#8AB411;margin-bottom:5px;\"><blink>This token will be valid for next 6 hours<blink></h3>" +
                                    $"</body>" +
                                    $"</html>";
                mailMsg.Body = mailBody;
                mailMsg.IsBodyHtml = true;
                mailMsg.Subject = "Fundoo Notes Password Reset Link";
                smptClient.Send(mailMsg);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }

}

        



