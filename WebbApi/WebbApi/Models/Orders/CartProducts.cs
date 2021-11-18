using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbApi.Models.Orders
{
    public class CartProducts
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int SubCategoriesId { get; set; }
        public string ImgUrl { get; set; }

        //public List<OrderLines> OrderLines { get; set; }
    }
}
