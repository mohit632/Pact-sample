using System;
using System.Collections.Generic;
using System.Text;

namespace Billing.Postgres.DomainModel
{
    public class BillModel
    {
        public int Id { get; set; }
        public string PlanType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
