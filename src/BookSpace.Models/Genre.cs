﻿using System.Collections.Generic;

namespace BookSpace.Models
{
    public class Genre
    {
        public string GenreId { get; set; }

        public string  Name { get; set; }

        public ICollection<BookGenre> GenreBooks { get; set; }
    }
}
