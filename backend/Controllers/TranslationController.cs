using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;
using System.IO;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpPost("text")]
        public async Task<IActionResult> TranslateText([FromBody] TextTranslationRequest request)
        {
            try
            {
                var result = await _translationService.TextTranslateAsync(request.Text, request.From, request.To);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("image")]
        public async Task<IActionResult> TranslateImage([FromForm] ImageTranslationRequest request)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await request.image.CopyToAsync(memoryStream);
                var result = await _translationService.ImageTranslateAsync(
                    memoryStream.ToArray(),
                    request.from.ToLower(),
                    request.to.ToLower(),
                    request.v);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
