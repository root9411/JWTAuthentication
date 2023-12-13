using PracticeProject.Utilities;

namespace PracticeProject.Services
{
    public class SharedService
    {
        //const string sharedSecret = "FE835FC7-7EAA-4734-AE97-11BCD83A970C";

        public string EncryptText(string plainText,string sharedSecret)
        {
            return SecurityOperation.EncryptStringAES(plainText, sharedSecret);
        }

        public string DecryptText(string encryptedText, string sharedSecret)
        {
            return SecurityOperation.DecryptStringAES(encryptedText, sharedSecret);
        }
    }
}
