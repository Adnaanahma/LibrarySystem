using Library.Model.ViewModel;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Retrieves a paginated list of books.
    /// </summary>
    /// <param name="pageNumber">The page number (default is 1).</param>
    /// <param name="pageSize">The size of the page (default is 10).</param>
    /// <returns>A list of books.</returns>
    [HttpGet("GetPaginatedLists")]
    public async Task<IActionResult> GetBooks(int pageNumber = 1, int pageSize = 10)
    {
        var result = await _bookService.GetBooksAsync(pageNumber, pageSize);
        if (result != null)
        {
            return Ok(result);
        }
        // Return internal server error with the error message
        return BadRequest(result);
    }

    /// <summary>
    /// Retrieves details of a specific book.
    /// </summary>
    /// <param name="id">The ID of the book.</param>
    /// <returns>The book details.</returns>
    [HttpGet("GetById")]
    public async Task<IActionResult> GetBook(int id)
    {
        var result = await _bookService.GetBookAsync(id);
        if (result != null)
        {
            // Return not found with the error message
            return Ok(result);
        }
        // Return OK with success message and data
        return BadRequest(result);
    }

    /// <summary>
    /// Adds a new book to the library collection.
    /// </summary>
    /// <param name="newBook">The new book to add.</param>
    /// <returns>The added book.</returns>
    [HttpPost("AddNewBook")]
    public async Task<IActionResult> AddBook(Book newBook)
    {


        var result = await _bookService.AddBookAsync(newBook);
        if (result != null)
        {
            // Return success message
            return Ok(result);
        }
        // Return error message
        return BadRequest(result);
    }

    /// <summary>
    /// Updates details of an existing book.
    /// </summary>
    /// <param name="id">The ID of the book to update.</param>
    /// <param name="updatedBook">The updated book details.</param>
    /// <returns>No content.</returns>
    [HttpPut("UpdateById")]
    public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
    {

        var result = await _bookService.UpdateBookAsync(updatedBook);
        if (result != null)
        {
            // Return success message
            return Ok(result);
        }

        // Return error message
        return BadRequest(result);
    }

    /// <summary>
    /// Deletes a book from the library system.
    /// </summary>
    /// <param name="id">The ID of the book to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("DeleteById")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);
        if (result != null)
        {
            // Return sucess message
            return Ok(result);
        }

        // Return error message
        return BadRequest(result);
    }
}

