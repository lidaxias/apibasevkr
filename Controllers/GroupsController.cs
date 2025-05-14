namespace apibase.Controllers
{
    using global::apibase.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public GroupsController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetAllGroups()
        {
            return await _dbContext.Group.ToListAsync();
        }

        // GET: api/groups/semester/{semesterId}
        [HttpGet("semester/{semesterId}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsBySemester(int semesterId)
        {
            return await _dbContext.Semester
                .Where(s => s.Semester_Id == semesterId)
                .Select(s => new GroupDto
                {
                    GroupId = s.Group.Group_Id,
                    GroupName = s.Group.Name
                })
                .Distinct()
                .ToListAsync();
        }

        // GET: api/groups/semesters
        [HttpGet("semester")]
        public async Task<ActionResult<IEnumerable<Semester>>> GetAllSemesters()
        {
            return await _dbContext.Semester.ToListAsync();
             
        }


    }

    public class GroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}