using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Models;
using Application.CQRS.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            IEnumerable<CategoryDto> categories = await Mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }
    }
}