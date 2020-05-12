using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Owin.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace User.Tests
{
    public class UserApiTests : IDisposable
    {
        const string ProviderUri = "http://localhost:5000";
        const string PactUri = "http://localhost:9222";
        const string ProviderName = "UserApi";
        const string Consumer = "Billing";
        private IWebHost _webHost { get; }
        private ITestOutputHelper _outputHelper { get; }

        public UserApiTests(ITestOutputHelper output)
        {
            _outputHelper = output;

            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(PactUri)
                .UseStartup<TestStartup>()
                .Build();

            _webHost.Start();
        }

        [Fact]
        public void EnsureUserApiHonoursPactWithBilling()
        {
            // Arrange
            var config = new PactVerifierConfig
            {

                // NOTE: We default to using a ConsoleOutput,
                // however xUnit 2 does not capture the console output,
                // so a custom outputter is required.
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_outputHelper)
                },

                // Output verbose verification logs to the test output
                Verbose = true
            };

            //Act / Assert
            IPactVerifier pactVerifier = new PactVerifier(config);
            //pactVerifier.ProviderState($"{PactUri}/provider-states");
            pactVerifier
                .ServiceProvider(ProviderName, ProviderUri)
                .HonoursPactWith(Consumer)
                .PactUri($@"C:\Users\chandan.vasishta\source\repos\User.Solution\Pacts\{Consumer}-{ProviderName}.json")
                ////or
                //.PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest") //You can specify a http or https uri
                ////or
                //.PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest", new PactUriOptions("someuser", "somepassword")) //You can also specify http/https basic auth details
                ////or
                //.PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest", new PactUriOptions("sometoken")) //Or a bearer token
                ////or (if you're using the Pact Broker, you can use the various different features, including pending pacts)
                //.PactBroker("http://pact-broker", uriOptions: new PactUriOptions("sometoken"), enablePending: true, consumerVersionTags: new List<string> { "master" }, providerVersionTags: new List<string> { "master" }, consumerVersionSelectors: new List<VersionTagSelector> { new VersionTagSelector("master", false, true) })
                .Verify();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //_webHost.StopAsync().GetAwaiter().GetResult();
                    //_webHost.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
