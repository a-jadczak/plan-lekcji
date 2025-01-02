using Microsoft.AspNetCore.Mvc;
using PlanLekcjiAPI.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using PlanLekcjiAPI.ObjectToJson;

namespace PlanLekcjiAPI.Controllers
{
    [Route("plan-lekcji")]
    [ApiController]
    public class PlanLekcjiController : ControllerBase
    {
        private readonly IPlanLekcjiService _planLekcjiService;

        public PlanLekcjiController(IPlanLekcjiService planLekcjiService)
        {
            _planLekcjiService = planLekcjiService;
        }

        [HttpPost("nowyPlan")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            Console.WriteLine(file.FileName);
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nie przesłano pliku");
            }

            // Sprawdzamy, czy plik jest typu zip
            if (!file.FileName.EndsWith(".zip")) 
            {
                return BadRequest("Rodzaj pliku jest nieporwany. Upewnij się że przesyłasz plik z rozszerzeniem zip");
            }

            var result = await _planLekcjiService.SaveAndUnZipFile(file, AfterExtractionCallback);

            if (result is null)
            {
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania pliku");
            }

            return Ok("Plik przesłano i wypakowano pomyślnie");
        }

        private async Task AfterExtractionCallback(string extractedPath)
        {
            // Przykładowa logika do wykonania po wypakowaniu
            await Task.Run(() =>
            {
                Console.WriteLine($"Files extracted to: {extractedPath}");

                string targetPath = extractedPath + @"/../../data/";
                extractedPath = extractedPath + @"/plany";
                Main.StartConvertingToJson(extractedPath, targetPath);
            });
        }
    }
}
