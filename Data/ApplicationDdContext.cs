using API_Task.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Data
{
   

        public class ApplicationDbContext : DbContext
        {

            public ApplicationDbContext(DbContextOptions options) : base(options)
            {

            }
            public DbSet<EmployeeModel> GetEmpolyees { get; set; }
            public DbSet<DepartmentModel> GetDepartment { get; set; }
        }
    


}
