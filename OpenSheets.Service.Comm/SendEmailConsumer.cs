using System.Text;
using System.Threading.Tasks;
using MassTransit;
using OpenSheets.Contracts.Commands.Communication;

namespace OpenSheets.Service.Comm
{
    public class SendEmailConsumer : IConsumer<EmailMessage>
    {
        private readonly IEmailService _emailService;

        public SendEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Consume(ConsumeContext<EmailMessage> context)
        {
            _emailService.Send(context.Message);

            return Task.CompletedTask;
        }
    }
}
