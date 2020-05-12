using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BillingAPI.Client.ServiceInterface;
using BillingAPI.Dto;
using Newtonsoft.Json;

namespace BillingAPI.Client.Implementation
{
    public class UserClient : IUserServiceProxy
    {
        public string BaseUri { get; } = "http://localhost:58414/";
        public UserClient(string baseUri = null)
        {
            BaseUri = baseUri ?? BaseUri;
        }
        public GetUserDetailsDto GetUserDetails(int userId)
        {
            using var client = new HttpClient { BaseAddress = new Uri(BaseUri) };
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/user/{userId}");
            request.Headers.Add("Accept", "application/json");

            var response = client.SendAsync(request);
            var content = response.Result.Content.ReadAsStringAsync().Result;
            var status = response.Result.StatusCode;

            string reasonPhrase = response.Result.ReasonPhrase;

            if (status == HttpStatusCode.OK)
            {
                return !string.IsNullOrEmpty(content)
                    ? JsonConvert.DeserializeObject<GetUserDetailsDto>(content)
                    : null;
            }
            throw new Exception(reasonPhrase);
        }
    }
}
