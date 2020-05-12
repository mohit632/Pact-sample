using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.Dto
{
    public class GetBillDto
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddress { get; set; }
        public string PlanType { get; set; }
        public string PlanDescription { get; set; }
    }
}
