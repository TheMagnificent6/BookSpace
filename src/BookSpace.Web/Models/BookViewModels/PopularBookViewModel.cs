﻿using System;
using System.Collections.Generic;

namespace BookSpace.Web.Models.BookViewModels
{
    public class PopularBookViewModel
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public decimal Rating { get; set; }
        public string CoverUrl { get; set; }
        public string Author { get; set; }
    }
}
