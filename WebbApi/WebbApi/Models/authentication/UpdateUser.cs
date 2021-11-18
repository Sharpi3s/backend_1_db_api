using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbApi.Models.authentication
{
    public class UpdateUser
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public bool? Admin { get; set; } = false;
        public string Adress { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        //public string Adress { get; set; }
        //public string Zip { get; set; }
        //public string City { get; set; }
    }
}
