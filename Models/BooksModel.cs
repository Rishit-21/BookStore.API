using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class BooksModel
    {
        public int id { get; set; }

        [Required]
        public string title { get; set; }
        public string description { get; set; }
    }
}
