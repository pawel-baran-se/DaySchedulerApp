using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.DTOs.Assignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DaySchedulerApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        // GET: api/<AssignmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> Get()
        {
            var assignments = await _assignmentService.GetAssignments();
            return Ok(assignments);
        }

        // GET api/<AssignmentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentDetailDto>> Get(string id)
        {
            var assignment = await _assignmentService.GetAssignment(id);
            return Ok(assignment);
        }

        // POST api/<AssignmentsController>
        [HttpPost]
        public async Task<ActionResult<AssignmentDto>> Post([FromBody] CreateAssignmentDto dto)
        {
            var assignment = await _assignmentService.CreateAssignment(dto);
            return Ok(assignment);
        }

        // PUT api/<AssignmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateAssignmentDto dto)
        {
            await _assignmentService.UpdateAssignment(id, dto);
            return NoContent();
        }

        [HttpPut("changenotification/{id}")]
        public async Task<ActionResult> ChangeSendNotificationSettings(string id, [FromBody] ChangeNotificationSettingsDto dto)
        {
            await _assignmentService.UpdateNoificationSettings(id, dto);
            return NoContent();
        }

        // DELETE api/<AssignmentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _assignmentService.DeleteAssignment(id);
            return NoContent();
        }
    }
}
