using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSampleApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "john", "jack" };
            return Ok(studentNames);
        }
    }
}
