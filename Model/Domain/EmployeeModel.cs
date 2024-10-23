using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Task.Model.Domain
{
    public class EmployeeModel
    {
        
            [Key]
            public string Id { get; set; } = Guid.NewGuid().ToString();

            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [Phone]
            public string PhoneNumber { get; set; }

            public int Deptid { get; set; }

        

    }
}
