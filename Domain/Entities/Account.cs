using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account : BaseEntity
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public double Balance { get; set; }
        public string CurrencyCode { get; set; }
        public bool CanEdit { get; set; }
        public User User { get; set; }

    }
}
