using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? Telephone { get; set; }

        public string Email { get; set; } = null!;

        public DateTime? CustomerBirthday { get; set; }

        public byte? CustomerStatus { get; set; }

        public string? Password { get; set; }
    }
}
