using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Model
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
