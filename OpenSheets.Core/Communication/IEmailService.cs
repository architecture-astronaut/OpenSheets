namespace OpenSheets.Service.Comm
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}