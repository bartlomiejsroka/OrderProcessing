using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.WebApi.Responses;
using OrderProcessing.WebApi.Services;

namespace OrderProcessing.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IFileConverterService _fileService;

        public OrdersController(ILogger<OrdersController> logger,
            IFileConverterService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost("test/{x}")]
        public FileResponse ProcessFile([FromForm] IFormFile file, [FromRoute(Name ="x")] int x)
        {
            _logger.LogInformation("Proccesing request");
            var files = _fileService.GetDocumentsFromFile(file, x);
            _logger.LogInformation("Processed request");

            return files;
        }
    }
}