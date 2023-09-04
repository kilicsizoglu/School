using Microsoft.AspNetCore.Mvc;
using School.API.Data;
using School.API.Models;

namespace School.API.Controllers
{
    [Route("api/student")]
    public class StudentController : Controller
    {
        public SchoolDbContext schoolDbContext { get; set; }
        public StudentController(SchoolDbContext context)
        {
            this.schoolDbContext = context;
        }

        [HttpGet("Get")]
        public IActionResult Get([FromBody] Guid Key)
        {
            APIKey? key = schoolDbContext.apiKeys.ToList().Where(x => x.Key == Key).FirstOrDefault();
            if (key == null)
            {
                return Ok("reject");
            } else
            {
                List<Student>? students = schoolDbContext.students.ToList();
                return Ok(students);
            }
        }

        [HttpGet("GetId")]
        public IActionResult GetId([FromBody] GetIdResponse response)
        {
            APIKey? key = schoolDbContext.apiKeys.ToList().Where(x => x.Key == response.Key).FirstOrDefault();
            if (key == null)
            {
                return Ok("reject");
            }
            else
            {
                Student? student = schoolDbContext.students.ToList().FirstOrDefault(x => x.Id == response.Id);
                return Ok(student);
            }
        }

        [HttpPut("Remove")]
        public IActionResult Remove([FromBody] RemoveResponse response)
        {
            APIKey? key = schoolDbContext.apiKeys.ToList().Where(x => x.Key == response.Key).FirstOrDefault();
            if (key == null)
            {
                return Ok("reject");
            }
            else
            {
                Student? student = schoolDbContext.students.ToList().FirstOrDefault(x => x.Id == response.Id);
                if (student == null)
                {
                    return Ok("reject");
                }
                else
                {
                    schoolDbContext.students.Remove(student);
                    return Ok("ok");
                }
            }
        }

        [HttpPatch("Edit")]
        public IActionResult Edit([FromBody] EditResponse response)
        {
            APIKey? key = schoolDbContext.apiKeys.ToList().Where(x => x.Key == response.Key).FirstOrDefault();
            if (key == null)
            {
                return Ok("reject");
            }
            else
            {
                schoolDbContext.students.Update(response.student);
                return Ok("ok");
            }
        }
    }
}
