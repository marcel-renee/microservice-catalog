using Catalog.Cmd.Command.Commands;
using Catalog.Cmd.Command.DTOs;
using Catalog.Common.DTOs;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Cmd.Command.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductCategoryCreateController : ControllerBase
    {
        private readonly ILogger<ProductCategoryCreateController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public ProductCategoryCreateController(ILogger<ProductCategoryCreateController> logger, ICommandDispatcher commandDispatcher)
        {
            this._logger = logger;
            this._commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> NewProductAsync(ProductCategoryCreateCommand command)
        {
            try
            {
                var id = Guid.NewGuid();
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new NewProductResponse()
                {
                    Id= id,
                    Message = "New category was added succesfully"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Client have made a bad request");
                return BadRequest(new BaseResponse()
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string ERROR_MESSAGE = "Error occur when processing the request";
                _logger.Log(LogLevel.Error, ex, ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse()
                {
                    Message = ERROR_MESSAGE,
                });
            }
        }
    }
}
