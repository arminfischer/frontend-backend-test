namespace SearchApp.Services;

using SearchApp.Models;

public class DocumentService
{
    private static readonly List<Document> _documents = new()
    {
        new Document
        {
            Id = 1,
            Title = "Getting Started with .NET 9.0",
            Description = "A comprehensive guide to building modern applications with .NET 9.0",
            Content =
                "This document covers the fundamentals of .NET 9.0, including new features, performance improvements, and best practices for building scalable applications.",
            CreatedDate = new DateTime(2024, 11, 1),
            Author = "John Doe",
            Tags = new List<string> { "dotnet", "tutorial", "backend" },
        },
        new Document
        {
            Id = 2,
            Title = "React Best Practices 2024",
            Description = "Modern React development patterns and best practices",
            Content =
                "Learn about hooks, context API, performance optimization, and the latest React patterns for building maintainable frontend applications.",
            CreatedDate = new DateTime(2024, 10, 15),
            Author = "Jane Smith",
            Tags = new List<string> { "react", "frontend", "javascript" },
        },
        new Document
        {
            Id = 3,
            Title = "Material UI Design System",
            Description = "Building beautiful UIs with Material UI components",
            Content =
                "A deep dive into Material UI's component library, theming capabilities, and how to create consistent, accessible user interfaces.",
            CreatedDate = new DateTime(2024, 9, 20),
            Author = "Mike Johnson",
            Tags = new List<string> { "material-ui", "design", "frontend" },
        },
        new Document
        {
            Id = 4,
            Title = "Azure Web Apps Deployment",
            Description = "Deploy full-stack applications to Azure Web Apps",
            Content =
                "Step-by-step guide for deploying .NET and React applications to Azure Web Apps, including CI/CD setup and configuration management.",
            CreatedDate = new DateTime(2024, 11, 10),
            Author = "Sarah Williams",
            Tags = new List<string> { "azure", "deployment", "devops" },
        },
        new Document
        {
            Id = 5,
            Title = "RESTful API Design Principles",
            Description = "Design clean and maintainable REST APIs",
            Content =
                "Learn the principles of RESTful API design, including proper HTTP methods, status codes, versioning, and documentation strategies.",
            CreatedDate = new DateTime(2024, 8, 5),
            Author = "David Brown",
            Tags = new List<string> { "api", "rest", "backend" },
        },
    };

    public List<SearchResult> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return _documents
                .Select(d => new SearchResult
                {
                    Id = d.Id,
                    Title = d.Title,
                    Description = d.Description,
                    Relevance = 1.0,
                })
                .ToList();
        }

        var lowerQuery = query.ToLower();
        var results = _documents
            .Select(d =>
            {
                var relevance = 0.0;
                if (d.Title.ToLower().Contains(lowerQuery))
                    relevance += 2.0;
                if (d.Description.ToLower().Contains(lowerQuery))
                    relevance += 1.5;
                if (d.Content.ToLower().Contains(lowerQuery))
                    relevance += 1.0;
                if (d.Tags.Any(t => t.ToLower().Contains(lowerQuery)))
                    relevance += 1.0;

                return new SearchResult
                {
                    Id = d.Id,
                    Title = d.Title,
                    Description = d.Description,
                    Relevance = relevance,
                };
            })
            .Where(r => r.Relevance > 0)
            .OrderByDescending(r => r.Relevance)
            .ToList();

        return results;
    }

    public Document? GetDocumentById(int id)
    {
        return _documents.FirstOrDefault(d => d.Id == id);
    }
}
