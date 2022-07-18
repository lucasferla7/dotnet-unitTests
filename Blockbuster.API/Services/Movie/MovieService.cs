using Blockbuster.API.Domain;
using Blockbuster.API.Repositories;
using Blockbuster.API.Requests;
using Blockbuster.API.Responses;


namespace Blockbuster.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<CreatedMovieResponse> CreateAsync(
            CreateMovieRequest request,
            CancellationToken cancellationToken)
        {
            Movie movie = new()
            {
                Name = request.Name,
                Genre = request.Genre,
                ReleaseDate = request.ReleaseDate,
                Synopsis = request.Synopsis,
            };

            var entry = await _movieRepository.AddAsync(movie, cancellationToken);
            await _movieRepository.SaveChangesAsync(cancellationToken);

            CreatedMovieResponse response = new()
            {
                Id = entry.Entity.Id,
                Name = entry.Entity.Name,
                Genre = entry.Entity.Genre,
                ReleaseDate = entry.Entity.ReleaseDate,
                Synopsis = entry.Entity.Synopsis
            };

            return response;
        }
    }
}
