using API_Task.Model.Domain;
using System.Drawing;

namespace API_Task.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
       IGenericRepository<DepartmentModel> Departments { get; }
        IGenericRepository<EmployeeModel> GetEmpolyees { get; }
     

        Task SaveAsync();
        Task ExecuteProcedureWithoutResult(string query);
    }
}
