namespace OpenSheets.Contracts.Requests
{
    public class DecodeTokenRequest
    {
        public byte[] Data { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public string Algorithm { get; set; }
    }
}