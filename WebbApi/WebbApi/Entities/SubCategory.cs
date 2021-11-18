using System;
using System.Collections.Generic;

#nullable disable

namespace WebbApi.Entities
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoriesId { get; set; }

        public virtual Category Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
