using System;
using System.Collections.Generic;

namespace Billing.Postgres.Models
{
    public partial class Plan
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Descripton { get; set; }
    }
}
