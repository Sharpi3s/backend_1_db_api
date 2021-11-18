using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbApi.Models.products;
using WebbApi.Models.Orders;

namespace WebbApi.Models.Orders
{
    public class GetOrderLine
    {
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public int ProductsId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public object Product { get; set; }



        //public List<GetOrderLineProduct> Products { get; set; } = new();

        //public List<GetProducts> Products { get; set; } = new();

    }
}
