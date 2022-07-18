using Blockbuster.API.Contexts;
using Blockbuster.API.Domain;

namespace Blockbuster.API.Repositories
{
    public class MovieRepository :
        Repository<Movie>,
        IMovieRepository
    {
        public MovieRepository(BlockbusterContext context)
            : base(context)
        {
        }
    }
}
