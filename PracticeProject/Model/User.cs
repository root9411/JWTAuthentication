namespace PracticeProject.Model
{
    public class User
    {
        public string ClientId { get; set; }
        public string Password { get; set; }
        public User(string clientId, string password)
        {
            ClientId = clientId; 
            Password = password;
        }
    }
}
