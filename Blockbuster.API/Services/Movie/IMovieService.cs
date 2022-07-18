using Blockbuster.API.Requests;
using Blockbuster.API.Responses;

namespace Blockbuster.API.Services
{
    public interface IMovieService
    {
        Task<CreatedMovieResponse> CreateAsync(
            CreateMovieRequest request,
            CancellationToken cancellationToken);
    }
}
