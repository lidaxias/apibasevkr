namespace apibase.Controllers
{
    using global::apibase.Models;
    // VisitsController.cs
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
        public class VisitsController : ControllerBase
        {
            private readonly MyDbContext _dbContext;

            public VisitsController(MyDbContext dbContext)
            {
                _dbContext = dbContext;
            }


            //[HttpGet]
            //public async Task<IActionResult> GetVisits([FromQuery] int groupId, [FromQuery] int subjectId, [FromQuery] DateTime date)
            //{
            //    try
            //    {
            //        var visits = await _dbContext.Visits
            //            .Include(v => v.Student)
            //            .Where(v => v.Group_Id == groupId &&
            //                       v.Date.Date == date.Date)
            //            .Select(v => new
            //            {
            //                v.Student_Id,
            //                //student_name = v.Student.Name,
            //                v.Student.Name,
            //                v.Status,
            //                v.Visit_Id
            //            })
            //            .ToListAsync();

            //        return Ok(visits);
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(500, $"Internal server error: {ex.Message}");
            //    }
            //}

            [HttpGet]
            public async Task<IActionResult> GetVisits([FromQuery] int groupId, [FromQuery] int subjectId, [FromQuery] DateTime date)
            {
                try
                {
                    var visits = await _dbContext.Visits
                        .Include(v => v.Student)
                        .Where(v => v.Group_Id == groupId && v.Date.Date == date.Date)
                        .Select(v => new
                        {
                            Student_Id = v.Student_Id,
                            student_name = v.Student.Name, // явно указываем имя поля
                            Status = v.Status,
                            Visit_Id = v.Visit_Id
                        })
                        .ToListAsync();

                    return Ok(visits);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }


            [HttpPost]
            public async Task<IActionResult> SaveVisits([FromBody] List<VisitModel> visits)
            {
                try
                {
                    foreach (var visit in visits)
                    {
                        if (visit.Visit_Id == 0)
                        {
                            _dbContext.Visits.Add(visit);
                        }
                        else
                        {
                            _dbContext.Visits.Update(visit);
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }
}
