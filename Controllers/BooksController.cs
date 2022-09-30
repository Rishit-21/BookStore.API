using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBookId([FromRoute]int Id)
        {
            var book = await _bookRepository.GetBookIdAsync(Id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBook([FromBody] BooksModel booksModel)
        {
            var id = await _bookRepository.AddBookAsync(booksModel);
            return CreatedAtAction(nameof(GetBookId),
                new { id=id,controller ="books"},id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BooksModel booksModel,[FromRoute]int id)
        {
            await _bookRepository.updateBookAsync(id,booksModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument booksModel, [FromRoute] int id)
        {
            await _bookRepository.updateBookPatchAsync(id, booksModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }
    }
}
