namespace PracticeProject.Model.EmployeeCRUD
{
    public class EmployeeCRUD
    {
        public int Id { get; set; }
        public string EmpName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }


        //public static implicit operator EmployeeCRUD(Employee _Employee)
        //{
        //    return new EmployeeCRUD
        //    {
        //        Id = _Employee.Id,
        //        EmpName = _Employee.EmpName,
        //        Age = _Employee.Age,
        //        Address = _Employee.Address,
        //    };
        //}


        //public static implicit operator Employee(EmployeeCRUD vm)
        //{
        //    return new Employee
        //    {
        //        Id=vm.Id,
        //        EmpName = vm.EmpName,
        //        Age = vm.Age,
        //        Address = vm.Address,
        //        //department = vm.Department,
        //        //position = vm.Position
        //    };
        //}



    }
}
