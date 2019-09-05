using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class OwnerAccount
    {
        public Guid AccountId { get; set; }
        public string AccountType { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid OwnerId { get; set; }
        public string Owner { get; set; }
    }
}
