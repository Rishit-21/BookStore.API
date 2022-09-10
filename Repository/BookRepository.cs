using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BooksModel>> GetAllBooksAsync()
        {
            //var records = await _context.Books.Select(x => new BooksModel()
            //{
            //    id = x.id,
            //    title = x.title,
            //    description = x.description
            //}
            //).ToListAsync();
            //return records;

            var records = await _context.Books.ToListAsync();
            return _mapper.Map<List<BooksModel>>(records);

        }

        public async Task<BooksModel> GetBookIdAsync(int Id)
        {
            //var records = await _context.Books.Where(x => x.id == Id).Select(x => new BooksModel()
            //{
            //    id = x.id,
            //    title = x.title,
            //    description = x.description
            //}
            //).FirstOrDefaultAsync();
            //return records;

            var book = await _context.Books.FindAsync(Id);
            return _mapper.Map<BooksModel>(book);

        }

        public async Task<int> AddBookAsync(BooksModel booksModel)
        {
            //var book = new Books()
            //{
            //    title = booksModel.title,
            //    description = booksModel.description,

            //};

            var book =  _mapper.Map<Books>(booksModel);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.id;


        }

        public async Task updateBookAsync(int Id, BooksModel booksModel)
        {
            //var book = await _context.Books.FindAsync(Id);
            //if (book != null)
            //{
            //    book.title = booksModel.title;
            //    book.description = booksModel.description;

            //    await _context.SaveChangesAsync();
            //}

            var book = new Books()
            {
                id = Id,
                title = booksModel.title,
                description = booksModel.description,

            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
     

        }

        public async Task updateBookPatchAsync(int Id, JsonPatchDocument booksModel)
        {
            var book = await _context.Books.FindAsync(Id);
            if (book != null)
            {
                booksModel.ApplyTo(book);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteBookAsync(int id)
        {
            // var book = _context.Books.Where(x => x.title == "").FirstOrDefault();

            var book = new Books() { id = id };
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book.id;
        }

    }
}
