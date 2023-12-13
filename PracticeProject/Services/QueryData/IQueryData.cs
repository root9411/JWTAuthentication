using Microsoft.EntityFrameworkCore;
using PracticeProject.Model;
using PracticeProject.Model.EmployeeCRUD;

namespace PracticeProject.Helpers.QueryData
{
    public interface IQueryData 
    {
        bool UpdateDepartment(Department Department);
        bool UpdatePosition(Position Position);
        bool UpdateEmployee(Employee Employee);
    }
}
