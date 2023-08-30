﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondBook.Services.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int Price { get; set; }
        public CategoryDTO Category { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
