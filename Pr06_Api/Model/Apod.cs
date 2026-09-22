namespace Pr06_Api.Model
{
    public class Apod
    {
        public string Title { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? Hdurl { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Media_type { get; set; } = string.Empty;
    }
}