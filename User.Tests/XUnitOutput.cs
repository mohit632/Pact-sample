using System;
using System.Collections.Generic;
using System.Text;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace User.Tests
{
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper _output;

        public XUnitOutput(ITestOutputHelper output)
        {
            _output = output;
        }

        public void WriteLine(string line)
        {
            _output.WriteLine(line);
        }
    }
}
