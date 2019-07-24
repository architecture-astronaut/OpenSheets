namespace OpenSheets.Contracts.Requests
{
    public class EncryptRequest
    {
        public string Algorithm { get; set; }
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }
        public byte[] ClearText { get; set; }
    }
}