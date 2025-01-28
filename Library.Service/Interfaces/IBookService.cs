using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model.ViewModel;

namespace Library.Service.Interfaces;
/// <summary>
/// Interface for book service that defines methods for CRUD operations.
/// </summary>
public interface IBookService
{
    Task<BaseResponse<IEnumerable<Book>>> GetBooksAsync(int pageNumber, int pageSize);
    Task<BaseResponse<Book>> GetBookAsync(int id);
    Task<BaseResponse<Book>> AddBookAsync(Book newBook);
    Task<BaseResponse<Book>> UpdateBookAsync(Book updatedBook);
    Task<BaseResponse<string>> DeleteBookAsync(int id);
}

