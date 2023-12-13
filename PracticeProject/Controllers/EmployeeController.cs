using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Model;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Model.EmployeeCRUD;
using Microsoft.Data.SqlClient;
using PracticeProject.Helpers.QueryData;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using PracticeProject.Services;
using System.IdentityModel.Tokens.Jwt;
using PracticeProject.Model.TokenModel;

namespace PracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private readonly DatabaseContext _dbContext;
        private  IQueryData _queryData = null;

        public EmployeeController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            this._queryData = new QueryData(_dbContext);
        }


        [HttpGet("GetTokenInfo")]
        public async Task<IActionResult> getTokenInfo()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];

            if(string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Unauthorized();
            }

            string token = authorizationHeader.Substring("Bearer ".Length).Trim();
            TokenPayload tp = new TokenPayload();
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                if (jsonToken != null)
                {
                    tp.ClientId = jsonToken.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                    tp.DatabaseName = jsonToken.Claims.FirstOrDefault(c => c.Type == "DatabaseName")?.Value;
                    tp.Issuer = jsonToken.Issuer;
                    tp.Audience = jsonToken.Audiences.FirstOrDefault();
                }

                return Ok(tp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token decoding error: {ex.Message}");
            }

            return Ok();
        }

        [HttpGet("GetDepartmentData")]
        public async Task<IActionResult> getDepartmentData()
        {

            var result = _dbContext.Department.ToList();


            return Ok(result);
        }

       


        [HttpGet("GetPositionData")]
        public async Task<IActionResult> getPositionData()
        {
            var result = _dbContext.Position.ToList();

            return Ok(result);
        }


        [HttpGet("GetEmployeeData")]
        public async Task<IActionResult> getEmployeeData()
        {
            //var result = _dbContext.Employee.ToList();
            var result = EmployeeData();
            
            return Ok(result);
        }


        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(Employee data)
        {
            if(data == null)
            {
                return BadRequest();
            }
            else
            {
                var result = await _dbContext.Employee.AddAsync(data);
                await _dbContext.SaveChangesAsync();

                return Ok(result.Entity);
            }
        }




        // Add And Update Without Using Query
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(Department Dep)
        {
            if (Dep == null)
            {
                return BadRequest();
            }
            else
            {
                var data = new Department
                {
                    Id = Dep.Id,
                    DepartmentName = Dep.DepartmentName
                };

                if(data.Id == 0)
                {
                    var result = await _dbContext.Department.AddAsync(data);
                    await _dbContext.SaveChangesAsync();

                    return Ok(result.Entity);
                }
                else
                {
                    var result = _dbContext.Department.Update(data);
                    await _dbContext.SaveChangesAsync();
                    
                    return Ok(result.Entity);
                }
            }
        }




        // Add And Update Without Using Query
        [HttpPost("AddPosition")]
        public async Task<IActionResult> AddPosition(Position Pos)
        {

            if(Pos == null)
            {
                return BadRequest();
            }
            else
            {
                var data = new Position
                {
                    Id= Pos.Id,
                    PositionName = Pos.PositionName
                };

                if(data.Id == 0)
                {
                    var result = await _dbContext.Position.AddAsync(data);
                    await _dbContext.SaveChangesAsync();
    
                    return Ok(result.Entity);
                }
                else
                {
                    var result =  _dbContext.Position.Update(data);
                    await _dbContext.SaveChangesAsync();

                    return Ok(result.Entity);
                }
            }
        }


        // Update Department Data By using Query
        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartmentData(Department Department)
        {
            if(Department == null)
            {
                return BadRequest();
            }


            bool result = _queryData.UpdateDepartment(Department);

            if(result == true)
            {
                return Ok(new { Message = "Department Data Updated" });
            }
            else
            {
                return Ok(new { Message = "Department Data Not Updated" });

            }
        }


        // Update Position Data By using Query
        [HttpPut("UpdatePosition")]
        public async Task<IActionResult> UpdatePositionData(Position Position)
        {
            if(Position == null)
            {
                return BadRequest();
            }

            bool result = _queryData.UpdatePosition(Position);

            if (result == true)
            {
                return Ok(new { Message = "Position Data Updated" });
            }
            else
            {
                return Ok(new { Message = "Position Data Not Updated" });
            }
        }


        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult>UpdateEmployeeData(Employee Employee)
        {
            if(Employee == null)
            {
                return BadRequest();
            }

            bool result = _queryData.UpdateEmployee(Employee);
            if (result == true)
            {
                return Ok(new { Message = "Employee Data Updated" });
            }
            else
            {
                return Ok(new { Message = "Employee Data Not Updated" });
            }
        }






        // Linq-to-Entities Query
        private IQueryable<EmployeeCRUD> EmployeeData()
        {
            try
            {
                return (from _Employee in _dbContext.Employee
                        join dep in _dbContext.Department on _Employee.departmentId equals dep.Id
                        join pos in _dbContext.Position on _Employee.positionId equals pos.Id
                        select new EmployeeCRUD
                        {
                            Id = _Employee.Id,
                            EmpName = _Employee.EmpName,
                            Age = _Employee.Age,
                            Address = _Employee.Address,
                            Department = dep.DepartmentName,
                            Position = pos.PositionName
                        }).OrderBy(x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
