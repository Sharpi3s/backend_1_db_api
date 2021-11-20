﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebbApi.Entities
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
