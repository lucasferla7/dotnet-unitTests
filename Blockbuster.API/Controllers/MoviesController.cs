using Blockbuster.API.Requests;
using Blockbuster.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blockbuster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(
            CreateMovieRequest request,
            CancellationToken cancellationToken,
            [FromServices] IMovieService service)
        {
            var movie = await service.CreateAsync(
                request,
                cancellationToken);

            return Created($"{movie.Id}", movie);
        }
    }
}
