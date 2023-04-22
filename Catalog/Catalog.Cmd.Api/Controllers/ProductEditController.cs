using Catalog.Cmd.Command.Commands;
using Catalog.Cmd.Command.DTOs;
using Catalog.Common.DTOs;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Cmd.Command.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductEditController : ControllerBase
    {
        private readonly ILogger<ProductEditController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public ProductEditController(ILogger<ProductEditController> logger, ICommandDispatcher commandDispatcher)
        {
            this._logger = logger;
            this._commandDispatcher = commandDispatcher;
        }

        [HttpPut]
        public async Task<ActionResult> ProductEdit(ProductEditCommand command)
        {
            try
            {
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status200OK, new NewProductResponse()
                {
                    Id = command.Id,
                    Message = "Product information was upated succesfully"
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
