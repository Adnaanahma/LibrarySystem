using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model.ViewModel;
using Library.Service.Interfaces;

namespace Library.Service.Services;

public class BookService : IBookService
{
    // List to store books. In a real application, this would be replaced with a database.
    private readonly List<Book> books;

    public BookService()
    {
        // Initialize with some sample data (optional)
        books = new List<Book>();
    }

    /// <summary>
    /// Retrieves a paginated list of books with title, author, genre, and publication date.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <returns>BaseResponse with a list of books.</returns>
    public async Task<BaseResponse<IEnumerable<Book>>> GetBooksAsync(int pageNumber, int pageSize)
    {
        try
        {
            // Perform pagination using LINQ
            var pagedBooks = await Task.Run(() => books
                .Skip((pageNumber - 1) * pageSize)  // Skip books based on the current page number
                .Take(pageSize)                      // Take a subset of books based on the page size
                .ToList());                          // Convert the result to a list

            // Return success response with paginated books
            return new BaseResponse<IEnumerable<Book>>
            {
                IsSuccess = true,
                Message = "Books retrieved successfully",
                Data = pagedBooks  // The Data property contains the paginated list of books
            };
        }
        catch (Exception ex)
        {
            // Return error response if an exception occurs
            return new BaseResponse<IEnumerable<Book>>
            {
                IsSuccess = false,
                Message = $"An error occurred while retrieving books: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Retrieves a specific book by ID.
    /// </summary>
    /// <param name="id">The ID of the book.</param>
    /// <returns>BaseResponse with the book details.</returns>
    public async Task<BaseResponse<Book>> GetBookAsync(int id)
    {
        var book = await Task.Run(() => books.FirstOrDefault(b => b.Id == id));
        if (book != null)
        {
            return new BaseResponse<Book>
            {
                IsSuccess = true,
                Message = "Book retrieved successfully",
                Data = book
            };
        }

        else return new BaseResponse<Book>
        {
            IsSuccess = false,
            Message = "Book Not Found",
            Data = null
        };
    }

    /// <summary>
    /// Adds a new book to the library.
    /// </summary>
    /// <param name="newBook">The new book to be added.</param>
    /// <returns>BaseResponse with the added book.</returns>
    public async Task<BaseResponse<Book>> AddBookAsync(Book newBook)
    {
        return await Task.Run(() =>
        {
            try
            {
                // Assign a new ID to the book
                newBook.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
                // Add the book to the list
                books.Add(newBook);

                // Return success response with the added book
                return new BaseResponse<Book>
                {
                    IsSuccess = true,
                    Message = "Book added successfully",
                    Data = newBook
                };
            }
            catch (Exception ex)
            {
                // Return error response if an exception occurs
                return new BaseResponse<Book>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while adding the book: {ex.Message}",
                    Data = null
                };
            }
        });
    }

    /// <summary>
    /// Updates an existing book in the library.
    /// </summary>
    /// <param name="updatedBook">The updated book details.</param>
    /// <returns>BaseResponse with the updated book.</returns>
    public async Task<BaseResponse<Book>> UpdateBookAsync(Book updatedBook)
    {
        try
        {
            return await Task.Run(() =>
            {
                var book = books.FirstOrDefault(b => b.Id == updatedBook.Id);
                if (book != null)
                {
                    // Update the book details
                    book.Title = updatedBook.Title;
                    book.Author = updatedBook.Author;
                    book.Genre = updatedBook.Genre;
                    book.PublicationDate = updatedBook.PublicationDate;
                    book.AvailabilityStatus = updatedBook.AvailabilityStatus;
                    book.Edition = updatedBook.Edition;
                    book.Summary = updatedBook.Summary;



                }
                return new BaseResponse<Book>
                {
                    IsSuccess = true,
                    Message = "Book updated successfully",
                    Data = book
                };


            });
        }
        // Return error response message if the book is not found
        catch (Exception ex)
        {
            return new BaseResponse<Book>
            {
                IsSuccess = false,
                Message = $"An error occurred while adding the book: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Deletes a book from the library.
    /// </summary>
    /// <param name="id">The ID of the book to be deleted.</param>
    /// <returns>BaseResponse indicating the result of the deletion.</returns>
    public async Task<BaseResponse<string>> DeleteBookAsync(int id)
    {
        try
        {
            return await Task.Run(() =>
            {
                var book = books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    // Remove the book from the list
                    books.Remove(book);

                    // Return success response indicating deletion

                }
                return new BaseResponse<string>
                {
                    IsSuccess = true,
                    Message = "Book deleted successfully",

                };


            });
        }
        catch (Exception ex)
        {

            return new BaseResponse<string>
            {
                IsSuccess = false,
                Message = "Book not found",

            };
        }

    }
}

