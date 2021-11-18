using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbApi.Entities;

namespace WebbApi.Models.Orders
{
    public class GetOrders
    {

        public int Id { get; set; }
        public int UsersId { get; set; }
        public int UserAdressesId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        //public virtual ICollection<GetOrderLine> OrderLines { get; set; }

        public List<GetOrderLine> OrderLines { get; set; } = new();
    }
}
