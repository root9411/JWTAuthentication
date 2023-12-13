using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Model;
using PracticeProject.Model.EmployeeCRUD;

namespace PracticeProject.Helpers.QueryData
{
    public class QueryData : IQueryData
    {
        private readonly DbContext _dbContext;
        public QueryData(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool UpdateDepartment(Department Department)
        {
            try
            {
                var query = "UPDATE Department SET DepartmentName = @DepartmentName WHERE Id = @Id";

                _dbContext.Database.ExecuteSqlRaw(query,
                    new SqlParameter("@DepartmentName", Department.DepartmentName),
                    new SqlParameter("@Id", Department.Id));

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        public bool UpdatePosition(Position Position)
        {
            try
            {
                var query = "UPDATE Position SET PositionName = @PositionName WHERE Id = @Id";
                _dbContext.Database.ExecuteSqlRaw(query,
                    new SqlParameter("@PositionName",Position.PositionName),
                    new SqlParameter("@Id",Position.Id));

                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public bool UpdateEmployee(Employee Employee)
        {
            try
            {
                var query = "update Employee SET EmpName = @EmpName , Age=@Age,Address=@Address,departmentId=@Did,positionId=@Pid WHERE Id=@Id";
                _dbContext.Database.ExecuteSqlRaw(query,
                    new SqlParameter("@EmpName", Employee.EmpName),
                    new SqlParameter("@Age",Employee.Age),
                    new SqlParameter("@Address",Employee.Address),
                    new SqlParameter("@Did",Employee.departmentId),
                    new SqlParameter("@Pid", Employee.positionId),
                    new SqlParameter("@Id", Employee.Id));

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }





    }
}
