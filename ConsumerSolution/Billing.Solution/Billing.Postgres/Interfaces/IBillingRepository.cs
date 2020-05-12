using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Billing.Postgres.DomainModel;

namespace Billing.Postgres.Interfaces
{
    public interface IBillingRepository
    {
        BillModel GetBill(int billId);
    }
}
