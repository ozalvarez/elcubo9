using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace elcubo9.bll.Utils
{
    public enum Template
    {
        Layout
    }
    public class EmailService
    {
        private const String LAYOUT = "e48a29ff-99e4-42b5-b5d1-18a49bfdb48a";
        private bool EmailEnabled
        {
            get
            {
                var _emailEnabled = System.Configuration.ConfigurationManager.AppSettings["EmailEnabled"];
                if (!String.IsNullOrEmpty(_emailEnabled))
                {
                    if (_emailEnabled.Equals("true"))
                        return true;
                }
                return false;

            }
        }
        private SendGridMessage GetSendGridMessage(IEnumerable<string> To,string FromAddress, string FromDisplayName, string Subject, string ReplyToEmail, string Html)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(To);
            myMessage.From = new MailAddress(FromAddress, FromDisplayName);
            myMessage.Subject = Subject;
            myMessage.Html = Html;
            myMessage.EnableClickTracking(true);
            if (!string.IsNullOrEmpty(ReplyToEmail))
            {
                var _RT = new MailAddress(ReplyToEmail, FromDisplayName);
                myMessage.ReplyTo = new MailAddress[] { _RT };
            }
            return myMessage;
        }
        private void Deliver(SendGridMessage myMessage)
        {
            var credentials = new NetworkCredential("azure_cef20b4d17625ef6b4b89b663ead4fc3@azure.com", "gfu7rv725B7EgRp");
            var transportWeb = new Web(credentials);
            transportWeb.Deliver(myMessage);
        }
        public void Send(IEnumerable<string> To, string FromAddress, string FromDisplayName, string Subject, string ReplyToEmail, string Html, Template Template)
        {
            if (EmailEnabled)
            {
                var myMessage = GetSendGridMessage(To, FromAddress, FromDisplayName, Subject, ReplyToEmail, Html);
                switch (Template)
                {
                    case Template.Layout:
                        myMessage.EnableTemplateEngine(LAYOUT);
                        break;
                }
                Deliver(myMessage);
            }
        }
        public void Send(IEnumerable<string> To, string FromAddress, string FromDisplayName, string Subject, string ReplyToEmail, string Html)
        {
            if (EmailEnabled)
                Deliver(GetSendGridMessage(To, FromAddress, FromDisplayName, Subject, ReplyToEmail, Html));
        }
        public void Send(IEnumerable<string> To, string FromAddress, string FromDisplayName, string Subject, string Html)
        {
            Send(To, FromAddress, FromDisplayName, Subject, null, Html);
        }
        public void Send(IEnumerable<string> To, string FromAddress, string FromDisplayName, string Subject, string Html, Template Template)
        {
            Send(To, FromAddress, FromDisplayName, Subject, null, Html, Template);
        }
    }
}
