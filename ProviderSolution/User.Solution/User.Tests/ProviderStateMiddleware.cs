using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Owin;
using Newtonsoft.Json;
using static System.String;

namespace User.Tests
{
    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "Billing";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action> _providerStates;
        private readonly TestServer _testServer;

        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    $"There is a user with id '1' and first name 'Chandan'",
                    AddTesterIfItDoesntExist
                },
                {
                    $"The user with id '1' does not exist.",
                    DeleteTesterIfItExists
                }
            };
        }

        private void AddTesterIfItDoesntExist()
        {
            //Add code to insert the tester data
        }

        private void DeleteTesterIfItExists()
        {
            //Add code to delete the tester data
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                this.HandleProviderStatesRequest(context);
                await context.Response.WriteAsync(String.Empty);
            }
            else
            {
                await this._next(context);
            }
        }

        private void HandleProviderStatesRequest(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.OK;

            if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                context.Request.Body != null)
            {
                string jsonRequestBody = String.Empty;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    jsonRequestBody = reader.ReadToEnd();
                }

                var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                //A null or empty provider state key must be handled
                if (providerState != null && !String.IsNullOrEmpty(providerState.State) &&
                    providerState.Consumer == ConsumerName)
                {
                    _providerStates[providerState.State].Invoke();
                }
            }
        }
    }
}