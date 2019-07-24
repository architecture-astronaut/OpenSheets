namespace OpenSheets.Contracts.Responses
{
    public class EncryptResponse
    {
        public byte[] IV { get; set; }
        public byte[] CipherText { get; set; }
    }
}