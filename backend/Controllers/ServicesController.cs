using backend.Application.Interfaces;
using backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Service service)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _serviceService.AddServiceAsync(service);
            return CreatedAtAction(nameof(GetById), new { id = service.ServiceId }, service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Service updatedService)
        {
            if (id != updatedService.ServiceId && updatedService.ServiceId != 0)
                return BadRequest("ID mismatch.");

            var existingService = await _serviceService.GetServiceByIdAsync(id);
            if (existingService == null)
                return NotFound();

            // Update main service properties
            existingService.ServiceName = updatedService.ServiceName;
            existingService.ServiceDate = updatedService.ServiceDate;

            // Remove old tasks
            existingService.Tasks.Clear();

            // Add new tasks (FK ServiceId will be auto-bound by EF)
            foreach (var task in updatedService.Tasks)
            {
                existingService.Tasks.Add(new Task
                {
                    TaskName = task.TaskName,
                    Description = task.Description,
                    Remarks = task.Remarks
                });
            }

            await _serviceService.UpdateServiceAsync(existingService);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound();

            await _serviceService.DeleteServiceAsync(id);
            return NoContent();
        }
    }
}
