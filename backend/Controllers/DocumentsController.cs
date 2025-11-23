namespace SearchApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using SearchApp.Services;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly DocumentService _documentService;

    public DocumentsController(DocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet("{id}")]
    public IActionResult GetDocument(int id)
    {
        var document = _documentService.GetDocumentById(id);

        if (document == null)
        {
            return NotFound(new { message = $"Document with ID {id} not found" });
        }

        return Ok(document);
    }
}
