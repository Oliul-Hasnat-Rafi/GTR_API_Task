using API_Task.Data.Interface;
using API_Task.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace API_Task.Data.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        GenericRepository<EmployeeModel> _Employees;
        GenericRepository<DepartmentModel> _Departments;
       


       
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<EmployeeModel> GetEmpolyees => _Employees ??= new GenericRepository<EmployeeModel>(_context);
        public IGenericRepository<DepartmentModel> Departments => _Departments ??= new GenericRepository<DepartmentModel>(_context);

       // public IGenericRepository<DepartmentModel> Departments => throw new NotImplementedException();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                _context.Dispose();
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ExecuteProcedureWithoutResult(string query)
        {
            await _context.Database.ExecuteSqlRawAsync(query);
        }
    }
}
