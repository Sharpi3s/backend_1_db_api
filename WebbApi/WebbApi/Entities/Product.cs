using System;
using System.Collections.Generic;

#nullable disable

namespace WebbApi.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public int SubCategoriesId { get; set; } 
        public string ImgUrl { get; set; }
        public virtual SubCategory SubCategories { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
