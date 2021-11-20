using System;
using System.Collections.Generic;

#nullable disable

namespace WebbApi.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int UsersId { get; set; }
        public int UserAdressesId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual UserAdress UserAdresses { get; set; }
        public virtual User Users { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
