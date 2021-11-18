using System;
using System.Collections.Generic;

#nullable disable

namespace WebbApi.Entities
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Admin { get; set; }
        public int UserAdressesId { get; set; }

        public virtual UserAdress UserAdresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
