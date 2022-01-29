using Main.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Threading.Tasks;

namespace Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResolutionController : ControllerBase
    {
        private readonly ILogger<ResolutionController> _logger;
        private readonly IResolutionService _resolutionService;

        public ResolutionController(ILogger<ResolutionController> logger, IResolutionService resolutionService)
        {
            _logger = logger;
            _resolutionService = resolutionService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Save(ResolutionDto dto)
        {
            _logger.LogInformation("Add Resolution = {resolution}.", dto);

            dto.Validate();

            var id = await _resolutionService.UpSert(dto.To());

            _logger.LogInformation("Resolution = {resolution} updated successfully.", dto);

            return CreatedAtAction(nameof(Save), id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            _logger.LogInformation("Get Resolution for Id = {id}.", id);

            Guard.Id(id);

            var user = await _resolutionService.Find(id);

            if (user == null)
            {
                _logger.LogInformation("Did not find the Resolution for Id = {id}.", id);
                return NotFound();
            }

            _logger.LogInformation("Find the Resolution = {resolution} for Id = {id}.", user, id);
            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Delete Resolution for Id = {id}.", id);

            Guard.Id(id);

            if (await _resolutionService.Delete(id))
            {
                _logger.LogInformation("Deleted the Resolution for Id = {id}.", id);
                return Ok();
            }

            _logger.LogInformation("Did not find the Resolution for Id = {id}.", id);
            return new ObjectResult($"Failed to delete Resolution for Id = {id}")
            {
                StatusCode = StatusCodes.Status417ExpectationFailed
            };
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAll(Guid id)
        {
            _logger.LogInformation("Delete Resolution for Id = {id}.", id);

            Guard.Id(id);

            if (await _resolutionService.Delete(id))
            {
                _logger.LogInformation("Deleted the Resolution for Id = {id}.", id);
                return Ok();
            }

            _logger.LogInformation("Did not find the Resolution for Id = {id}.", id);
            return new ObjectResult($"Failed to delete Resolution for Id = {id}")
            {
                StatusCode = StatusCodes.Status417ExpectationFailed
            };
        }
    }
}