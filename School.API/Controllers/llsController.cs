using Microsoft.AspNetCore.Mvc;
using School.API.Data;
using School.API.Models;

namespace School.API.Controllers
{
    [Route("api/lls")]
    public class llsController : Controller
    {
        public SchoolDbContext schoolDbContext { get; set; }
        public llsController(SchoolDbContext context) 
        {
            this.schoolDbContext = context;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginResponse response)
        {
            Admin? admin = schoolDbContext.admins.ToList().Where(x => x.Name == response.username && x.Password == response.password).FirstOrDefault();
            if (admin == null)
            {
                Teacher? teacher = schoolDbContext.teachers.ToList().Where(x => x.Name == response.username && x.Password == response.password).FirstOrDefault();
                if (teacher == null)
                {
                    Student? student = schoolDbContext.students.ToList().Where(x => x.Name == response.username && x.Password == response.password).FirstOrDefault();
                    if (student == null)
                    {
                        return Ok("reject");
                    } else
                    {
                        APIKey key = new APIKey();
                        key.UserId = student.Id;
                        key.Id = Guid.NewGuid();
                        key.Key = Guid.NewGuid();
                        key.UserRole = "STUDENT";
                        schoolDbContext.apiKeys.Add(key);
                        return Ok(key);
                    }
                    
                } else
                {
                    APIKey key = new APIKey();
                    key.UserId = teacher.Id;
                    key.Id = Guid.NewGuid();
                    key.Key = Guid.NewGuid();
                    key.UserRole = "TEACHER";
                    schoolDbContext.apiKeys.Add(key);
                    return Ok(key);
                }
                
            }
            else
            {
                APIKey key = new APIKey();
                key.UserId = admin.Id;
                key.Id = Guid.NewGuid();
                key.Key = Guid.NewGuid();
                key.UserRole = "TEACHER";
                schoolDbContext.apiKeys.Add(key);
                return Ok(key);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout([FromBody] Guid key)
        {
            APIKey? apiKey = schoolDbContext.apiKeys.ToList().Where(x => x.Key == key).FirstOrDefault();
            if (apiKey == null)
            {
                return Ok("reject");
            } else
            {
                schoolDbContext.apiKeys.Remove(apiKey);
            }
            return Ok("ok");
        }

        [HttpPost("Singup")]
        public IActionResult Singup([FromBody] SingupResponse response)
        {
            if (response == null)
            {
                return Ok("reject");
            } else
            {
                if (!response.isTeacher)
                {
                    if (response.person == null)
                    {
                        return Ok("reject");
                    } else
                    {
                        Student student = new Student();
                        student.Name = response.person.Name != null ? response.person.Name : "";
                        student.Email = response.person.Email != null ? response.person.Email : "";
                        student.Surname = response.person.Surname != null ? response.person.Surname : "";
                        student.Password = response.person.Password != null ? response.person.Password : "";
                        student.Phone = response.person.Phone != null ? response.person.Phone : "";
                        if (student.Name == "" && 
                            student.Surname == "" && 
                            student.Email == "" &&
                            student.Password == "")
                        {
                            return Ok("reject");
                        }
                        schoolDbContext.students.Add(student);
                    }
                } else
                {
                    if (response.person == null)
                    {
                        return Ok("reject");
                    }
                    else
                    {
                        Teacher teacher = new Teacher();
                        teacher.Name = response.person.Name != null ? response.person.Name : "";
                        teacher.Email = response.person.Email != null ? response.person.Email : "";
                        teacher.Surname = response.person.Surname != null ? response.person.Surname : "";
                        teacher.Password = response.person.Password != null ? response.person.Password : "";
                        teacher.Phone = response.person.Phone != null ? response.person.Phone : "";
                        if (teacher.Name == "" &&
                            teacher.Surname == "" &&
                            teacher.Email == "" &&
                            teacher.Password == "")
                        {
                            return Ok("reject");
                        }
                        schoolDbContext.teachers.Add(teacher);
                    }
                }
            }
            return Ok("ok");
        }
    }
}
