using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BooksModel>> GetAllBooksAsync();
        Task<BooksModel> GetBookIdAsync(int Id);
        Task<int> AddBookAsync(BooksModel booksModel);
        Task updateBookAsync(int Id, BooksModel booksModel);
        Task updateBookPatchAsync(int Id, JsonPatchDocument booksModel);
        Task<int> DeleteBookAsync(int id);
    }
}
