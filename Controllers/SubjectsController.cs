namespace apibase.Controllers
{
    using global::apibase.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace apibase.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class SubjectsController : ControllerBase
        {
            private readonly MyDbContext _dbContext;

            public SubjectsController(MyDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            //// GET: api/subjects/group/5
            //[HttpGet("group/{groupId}")]
            //public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectsByGroup(int groupId)
            //{
            //    // Получаем предметы, которые есть в расписании для данной группы
            //    var subjects = await _dbContext.Schedule
            //        .Where(s => s.Group_Id == groupId)
            //        .Select(s => s.Subject)
            //        .Distinct()
            //        .ToListAsync();

            //    return subjects;
            //}

            // GET: api/subjects
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
            {
                return await _dbContext.Subject.ToListAsync();
            }

            [HttpGet("teacher/{teacherId}")]
            public async Task<ActionResult<IEnumerable<SubjectWithTypeDto>>> GetTeacherSubjects(int teacherId)
            {
                var subjects = await _dbContext.Schedule
                    .Where(s => s.User_Id == teacherId)
                    .Include(s => s.Subject)
                    .ThenInclude(s => s.LessonType)
                    .Select(s => new SubjectWithTypeDto
                    {
                        SubjectId = s.Subject.Subject_Id,
                        SubjectName = s.Subject.Name,
                        LessonTypeId = s.Subject.LessonTypeId,
                        LessonTypeName = s.Subject.LessonType.Name
                    })
                    .Distinct()
                    .ToListAsync();

                return Ok(subjects);
            }
        }

        // DTO для передачи данных
        public class SubjectWithTypeDto
        {
            public int SubjectId { get; set; }
            public string SubjectName { get; set; }
            public int LessonTypeId { get; set; }
            public string LessonTypeName { get; set; }
        }
    }
}
