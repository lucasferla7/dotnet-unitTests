namespace Blockbuster.API.Requests
{
    public struct CreateMovieRequest
    {
        public string Name { get; init; }
        public string Synopsis { get; init; }
        public string Genre { get; init; }
        public DateTime ReleaseDate { get; init; }
    }
}
