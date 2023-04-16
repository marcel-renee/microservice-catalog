using Catalog.Query.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        IProductCategoryRepository _repository;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            _repository = productCategoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> ListProducCategory()
        {
            try
            {
                return Ok(await _repository.ListAllAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }

        }
    }
}
