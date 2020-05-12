using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingAPI.Dto;

namespace BillingAPI.Client.ServiceInterface
{
    public interface IUserServiceProxy
    {
        GetUserDetailsDto GetUserDetails(int userId);
    }
}
