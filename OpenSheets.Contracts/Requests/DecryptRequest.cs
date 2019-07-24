namespace OpenSheets.Contracts.Requests
{
    public class DecryptRequest
    {
        public string Algorithm { get; set; }
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }
        public byte[] CipherText { get; set; }
    }
}