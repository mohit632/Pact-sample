using System;
using System.Collections.Generic;

namespace Billing.Postgres.Models
{
    public partial class BilledUsers
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int UserId { get; set; }
    }
}
