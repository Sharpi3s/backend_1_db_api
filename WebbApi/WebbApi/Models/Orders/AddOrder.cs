using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbApi.Models.Orders
{
    public class AddOrder
    {
        //public int Id { get; set; }
        public int UsersId { get; set; }
        public DateTime OrderDate => DateTime.Now;
        public string Status => "Recived";
        public decimal TotalAmount { get; set; }
        public List<CartProducts> CartProducts { get; set; }

    }
}
