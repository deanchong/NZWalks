using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{

    // https://localhost:7107/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:7107/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "Daivd" };

            return Ok(studentNames);
        }
    }
}
