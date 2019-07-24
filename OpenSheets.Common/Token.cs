using System;

namespace OpenSheets.Common
{
    public class Token
    {
        public TokenType Type { get; set; }
        public Guid PrincipalId { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }
        public string Data { get; set; }
        public string InitValue { get; set; }

        public override string ToString()
        {
            return $"{Type.ToString("G").ToLower()} {Data} {InitValue}";
        }

        public static Token Parse(string str)
        {
            string[] parts = str.Split(' ');

            Token token = new Token();

            token.Type = (TokenType) Enum.Parse(typeof(TokenType), parts[0], true);

            token.Data = parts[1];

            token.InitValue = parts[2];

            return token;
        }
    }
}