using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string EmpName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int positionId { get; set; }
        public int departmentId { get; set; }
    }
}
