using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.OData.Core.UriParser;
using ODataToSPHackathon.Infrastructure;
using Microsoft.OData.Core.UriParser.Semantic;
using FluentAssertions;

namespace ODataToSPHackathon.Tests
{
    [TestClass]
    public class BinaryOperatorResolverTest : BaseTest
    {
        [TestMethod]
        public void ShouldBeEqualOperatorEmpty()
        {
            IQueryResolver resolver = new BinaryOperatorResolver(null);

            resolver.Resolve().Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldBeEqualOperatorForString()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=CorporateName%20eq%20%27Diego%27");

            IQueryResolver resolver = new BinaryOperatorResolver(parser.ParseFilter().Expression as BinaryOperatorNode);

            resolver.Resolve().Should().Be("WHERE CorporateName LIKE 'Diego'");
        }

        [TestMethod]
        public void ShouldBeGreaterThanOperatorForInt()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=MonitoredPrinters%20gt%20135");

            IQueryResolver resolver = new BinaryOperatorResolver(parser.ParseFilter().Expression as BinaryOperatorNode);

            resolver.Resolve().Should().Be("WHERE MonitoredPrinters > 135");
        }

        [TestMethod]
        public void ShouldBeLessThanOperatorForInt()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=MonitoredPrinters%20lt%20135");

            IQueryResolver resolver = new BinaryOperatorResolver(parser.ParseFilter().Expression as BinaryOperatorNode);

            resolver.Resolve().Should().Be("WHERE MonitoredPrinters < 135");
        }

        //To be continued...
    }
}
