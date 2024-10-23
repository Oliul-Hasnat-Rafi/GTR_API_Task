using System.ComponentModel.DataAnnotations;

namespace API_Task.Model.Domain
{
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

    }
}
