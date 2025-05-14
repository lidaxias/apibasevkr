// ScheduleController.cs
using apibase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apibase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public ScheduleController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetTeacherSchedule(int teacherId, [FromQuery] DateTime date)
        {
            try
            {
                var schedule = await _dbContext.Schedule
                    .Include(s => s.Subject)
                    .ThenInclude(subject => subject.LessonType)
                    .Include(s => s.Classroom)
                    .Include(s => s.Group)
                    .Where(s => s.User_Id == teacherId &&
                               s.Start_Time.Date == date.Date)
                    .OrderBy(s => s.Start_Time)
                    .Select(s => new
                    {
                        s.Schedule_Id,
                        SubjectName = s.Subject.Name,
                        LessonType = s.Subject.LessonType.Name,
                        Classroom = s.Classroom.Name,
                        GroupName = s.Group.Name,
                        s.Start_Time,
                        s.End_Time
                    })
                    .ToListAsync();

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}