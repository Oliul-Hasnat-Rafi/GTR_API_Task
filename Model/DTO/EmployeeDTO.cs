using API_Task.Model.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Task.Model.DTO
{
    public class EmployeeDTO
    {


        public string Id { get; set; }


        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public int Deptid { get; set; }
        public string DeptName { get; set; } = String.Empty;





    }
}
