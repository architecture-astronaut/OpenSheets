using System;
using SparkPost;

namespace OpenSheets.Service.Comm
{
    public class SparkPostEmailService : IEmailService
    {
        private readonly IClient _client;

        public SparkPostEmailService(IClient client)
        {
            _client = client;
        }

        public void Send(EmailMessage message)
        {
            switch (message)
            {
                case CustomEmailMessage custom:
                    Send(custom);
                    break;
                case TemplateEmailMessage template:
                    Send(template);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Send(CustomEmailMessage message)
        {
            Transmission transmission = new Transmission();

            transmission.Content.Subject = message.Subject;
            transmission.Content.From = new Address(message.From);

            foreach (string to in message.To)
            {
                transmission.Recipients.Add(new Recipient(){ Address = new Address(to), Type = RecipientType.To});
            }

            foreach (string cc in message.Cc)
            {
                transmission.Recipients.Add(new Recipient() { Address = new Address(cc), Type = RecipientType.CC });
            }

            foreach (string bcc in message.Bcc)
            {
                transmission.Recipients.Add(new Recipient() { Address = new Address(bcc), Type = RecipientType.BCC });
            }

            foreach (var attachment in message.Attachments)
            {
                transmission.Content.Attachments.Add(new Attachment()
                {
                    Name = attachment.Item1,
                    Type = attachment.Item2,
                    Data = Convert.ToBase64String(attachment.Item3)
                });
            }

            transmission.Id = message.Id.ToString("N");

            if (message.IsHtml)
            {
                transmission.Content.Html = message.Body;
            }
            else
            {
                transmission.Content.Text = message.Body;
            }

            _client.Transmissions.Send(transmission);
        }

        public void Send(TemplateEmailMessage message)
        {
            Transmission transmission = new Transmission();

            transmission.Content.From = new Address(message.From);

            foreach (string to in message.To)
            {
                transmission.Recipients.Add(new Recipient() { Address = new Address(to), Type = RecipientType.To });
            }

            foreach (string cc in message.Cc)
            {
                transmission.Recipients.Add(new Recipient() { Address = new Address(cc), Type = RecipientType.CC });
            }

            foreach (string bcc in message.Bcc)
            {
                transmission.Recipients.Add(new Recipient() { Address = new Address(bcc), Type = RecipientType.BCC });
            }

            transmission.Content.TemplateId = message.TemplateId;

            foreach (var data in message.TemplateData)
            {
                transmission.SubstitutionData.Add(data);
            }

            foreach (var attachment in message.Attachments)
            {
                transmission.Content.Attachments.Add(new Attachment()
                {
                    Name = attachment.Item1,
                    Type = attachment.Item2,
                    Data = Convert.ToBase64String(attachment.Item3)
                });
            }

            transmission.Id = message.Id.ToString("N");

            _client.Transmissions.Send(transmission);
        }
    }
}