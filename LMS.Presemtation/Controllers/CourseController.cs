using LMS.Shared.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CourseController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetOneCourse(int id)
    {
        var courseDto = await _serviceManager.CourseService.GetCourseByIdAsync(id);
        return Ok(courseDto);
    }

    [HttpGet("user")]
    public async Task<ActionResult<CourseDto>> GetCourseForCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var courseDto = await _serviceManager.CourseService.GetCourseByUserIdAsync(userId);
        return Ok(courseDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
    {
        var courseDtos = await _serviceManager.CourseService.GetAllCoursesAsync();
        return Ok(courseDtos);
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto dto)
    {
        var createdCourseDto = await _serviceManager.CourseService.CreateCourseAsync(dto);
        return CreatedAtAction(nameof(GetOneCourse), new { id = createdCourseDto.CourseId }, createdCourseDto);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchCourse(int id, JsonPatchDocument<UpdateCourseDto> patchDocument)
    {
        if (patchDocument is null) return BadRequest();

        var updatedCourse = await _serviceManager.CourseService.UpdateCourseAsync(id, patchDocument);
        return Ok(updatedCourse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        await _serviceManager.CourseService.DeleteCourseAsync(id);
        return NoContent();
    }
}