using API_Task.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetDept()
        {
            try
            {
                var departments = await _unitOfWork.Departments.GetAllAsync();

                if (departments == null || !departments.Any())
                {
                    return Ok("No Departments found.");
                }

                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
