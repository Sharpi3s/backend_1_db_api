using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbApi.Models.UserModels
{
    public class GetUsers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? Admin { get; set; }
        public int UserAdressesId { get; set; }
    }
}
