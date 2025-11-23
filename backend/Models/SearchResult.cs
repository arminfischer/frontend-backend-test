namespace SearchApp.Models;

public class SearchResult
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Relevance { get; set; }
}
