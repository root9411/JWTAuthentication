using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PracticeProject.Model
{
    public class Customer
    {
        
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }

    }
}
