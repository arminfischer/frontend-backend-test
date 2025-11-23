namespace SearchApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using SearchApp.Services;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly DocumentService _documentService;

    public SearchController(DocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string? query)
    {
        var results = _documentService.Search(query ?? string.Empty);
        return Ok(results);
    }
}
