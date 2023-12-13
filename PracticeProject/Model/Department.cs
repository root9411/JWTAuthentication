using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
