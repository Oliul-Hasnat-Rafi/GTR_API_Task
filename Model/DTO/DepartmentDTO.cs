using API_Task.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace API_Task.Model.DTO
{
    public class DepartmentDTO
    {
       
          
            public int Id { get; set; }
            public string Name { get; set; }
        public ICollection<EmployeeModel>? Employee { get; set; }



    }
}
