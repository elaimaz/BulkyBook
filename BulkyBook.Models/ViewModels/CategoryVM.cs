﻿using System.Collections.Generic;

namespace BulkyBook.Models.ViewModels
{
    public class CategoryVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public PaginInfo PaginInfo { get; set; }
    }
}
