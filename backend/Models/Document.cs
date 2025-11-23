namespace SearchApp.Models;

public class Document
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string Author { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}
