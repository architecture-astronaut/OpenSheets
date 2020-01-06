namespace OpenSheets.Service.Comm
{
    public class CustomEmailMessage : EmailMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}