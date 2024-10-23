using API_Task.Data.Interface;
using API_Task.Model.Domain;
using API_Task.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace API_Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _unitOfWork.GetEmpolyees.GetAllAsync();

                if (employees == null || !employees.Any())
                {
                    return NotFound("No employees found.");
                }

                var employeeWithDept = new List<EmployeeDTO>();

                foreach (var e in employees)
                {
                    
                    var department = await _unitOfWork.Departments.GetAsync(d => d.Id == e.Deptid);

                    employeeWithDept.Add(new EmployeeDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        PhoneNumber = e.PhoneNumber,
                        Deptid = e.Deptid,
                        DeptName = department?.Name ?? "Unknown"  
                    });
                }

                return Ok(employeeWithDept);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpPost("Create")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDto)
        {
            if (employeeDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = new EmployeeModel
                {
                    Name = employeeDto.Name,
                    PhoneNumber = employeeDto.PhoneNumber,
                    Deptid = employeeDto.Deptid
                };

                await _unitOfWork.GetEmpolyees.CreateAsync(employee);
                await _unitOfWork.SaveAsync();

                return Ok("Employee created successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the employee.");
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var employee = await _unitOfWork.GetEmpolyees.GetAsync(x => x.Id == id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                _unitOfWork.GetEmpolyees.DeleteAsync(employee);
                await _unitOfWork.SaveAsync();

                return Ok($"Employee with ID {id} deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the employee.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeDTO updatedEmployee)
        {
            if (id != updatedEmployee.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var employee = await _unitOfWork.GetEmpolyees.GetAsync(e => e.Id == id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                employee.Deptid = updatedEmployee.Deptid;
                employee.PhoneNumber = updatedEmployee.PhoneNumber;
                employee.Name = updatedEmployee.Name;

                await _unitOfWork.GetEmpolyees.UpdateAsync(employee);
                await _unitOfWork.SaveAsync();

                return Ok($"Employee with ID {id} updated successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the employee.");
            }
        }

        [HttpGet("GetEmployeeByName/{name}")]
        public async Task<IActionResult> FindEmployeeByName(string name)
        {
            try
            {
                var employee = await _unitOfWork.GetEmpolyees.SearchByColumnAsync(name, "Name");
                if (employee == null || !employee.Any())
                {
                    return NotFound($"No employees found with the name '{name}'.");
                }

                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the employee by name.");
            }
        }

    }
}
