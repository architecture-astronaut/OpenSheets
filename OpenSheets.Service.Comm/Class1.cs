using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using OpenSheets.Contracts.Commands.Communication;
using SparkPost;

namespace OpenSheets.Service.Comm
{
    public class SendEmailConsumer : IConsumer<SendCustomEmail>
    {
        private readonly IEmailService _emailService;

        public SendEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Consume(ConsumeContext<SendCustomEmail> context)
        {
            throw new NotImplementedException();
        }
    }

    public class SendTemplateEmailConsumer : IConsumer<SendTemplateEmail>
    {
        public Task Consume(ConsumeContext<SendTemplateEmail> context)
        {
            throw new NotImplementedException();
        }
    }

    public class SparkPostEmailService : IEmailService
    {
        void Send(EmailMessage message)
        {
            Transmission transmission = new Transmission();

            transmission.Content.Subject = message.Subject;
            transmission.Content.From = new Address(message.From);
            transmission.Content.Attachments.Add(new Attachment(){});

        }

        void Send(TemplateEmailMessage message)
        {

        }
    }

    public interface IEmailService
    {
        
    }

    public class EmailMessage
    {
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<(string, string, byte[])> Attachments { get; set; }
        public bool IsHtml { get; set; }
    }

    public class TemplateEmailMessage
    {
        public string TemplateId { get; set; }
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string From { get; set; }
        public Dictionary<string, object> TemplateData { get; set; }
    }
}
