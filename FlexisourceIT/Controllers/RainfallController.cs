using FlexisourceIT.Models;
using FlexisourceIT.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlexisourceIT.Controllers
{
    public class RainfallController : Controller
    {
        private readonly IRainfallService _rainfallService;

        public RainfallController(IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet("rainfall/id/{stationId}/readings")]
        public async Task<ActionResult<RainfallReadingResponse>> GetRainfallReadings(string stationId, [FromQuery, Range(1, 100)] int count = 10)
        {
            // Check if model state is valid after model binding
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (string.IsNullOrEmpty(stationId))
                {
                    return BadRequest(new ErrorResponse { Message = "Invalid request: stationId cannot be empty" });
                }

                var readings = await _rainfallService.GetRainfallReadingsAsync(stationId, count);
                if (readings == null || readings.Count == 0)
                {
                    return NotFound(new ErrorResponse { Message = "No readings found for the specified stationId" });
                }

                return new RainfallReadingResponse { Readings = readings };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
