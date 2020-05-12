using System.Collections.Generic;
using Billing.Postgres.Interfaces;
using BillingAPI.Client.ServiceInterface;
using BillingAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : Controller
    {
        private readonly IBillingRepository _repository;
        private readonly IUserServiceProxy _proxy;
        public BillingController(IBillingRepository repository, IUserServiceProxy proxy)
        {
            _repository = repository;
            _proxy = proxy;
        }

        // GET: api/<controller>
        [HttpGet("planId")]
        public GetBillDto GetBillDetails([FromRoute]int planId)
        {
            var bill = _repository.GetBill(planId);
            var user = _proxy.GetUserDetails(bill.UserId);
            return new GetBillDto()
            {
                PlanType = bill.PlanType,
                PlanDescription = bill.Description,
                UserAddress = user.UserAddress,
                UserFirstName = user.UserFirstName,
                UserLastName = user.UserLastName
            };
        }

    }
}
