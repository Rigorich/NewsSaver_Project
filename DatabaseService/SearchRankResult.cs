using ServiceStack.DataAnnotations;

namespace DatabaseService
{
    class SearchRankResult
    {
        [AutoIncrement]
        public long Id { get; set; }

        public int ArtId { get; set; }
        public float Rank { get; set; }
        public string Query { get; set; }
    }
}