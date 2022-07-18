namespace Blockbuster.API.Responses
{
    public struct CreatedMovieResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Synopsis { get; init; }
        public string Genre { get; init; }
        public DateTime ReleaseDate { get; init; }
    }
}
