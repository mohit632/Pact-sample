using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Billing.Postgres.DomainModel;
using Billing.Postgres.Interfaces;
using Billing.Postgres.Models;

namespace Billing.Postgres.Implementations
{
    public class BillingRepository : IBillingRepository
    {
        private readonly BillingContext _context;
        public BillingRepository(BillingContext context)
        {
            _context = context;
        }

        public BillModel GetBill(int billId)
        {
            var bill = _context.BilledUsers.First(p => p.Id == billId);
            var plan = _context.Plan.First(p => p.Id == bill.PlanId);
            return new BillModel
            {
                Id = billId,
                UserId = bill.UserId,
                PlanType = plan.Type,
                Description = plan.Descripton
            };
        }
    }
}
