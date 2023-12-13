namespace PracticeProject.Model.TokenModel
{
    public class TokenPayload
    {
        public string ClientId { get; set; }
        public string DatabaseName { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
