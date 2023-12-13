using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Model
{
    public class Position
    {
        [Key]
        public int Id {  get; set; }
        public string PositionName {  get; set; }
    }
}
