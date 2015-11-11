using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODataToSPHackathon.Infrastructure;
using FluentAssertions;
using Microsoft.OData.Core.UriParser;

namespace ODataToSPHackathon.Tests.Resolver
{
    [TestClass]
    public class OrderByOperatorResolverTest : BaseTest
    {
        [TestMethod]
        public void ShouldBeOrderByOperatorEmpty()
        {
            IQueryResolver resolver = new OrderByOperatorResolver(null);

            resolver.Resolve().Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldBeOrderByOperatorForOneField()
        {
            ODataUriParser parser = GetParser("/Enterprises?$orderby=EnterpriseName");

            IQueryResolver resolver = new OrderByOperatorResolver(parser.ParseOrderBy());

            resolver.Resolve().Should().Be("ORDER BY EnterpriseName asc");
        }

        [TestMethod]
        public void ShouldBeOrderByOperatorForTwoFields()
        {
            ODataUriParser parser = GetParser("/Enterprises?$orderby=EnterpriseName,PartnerName%20desc");

            IQueryResolver resolver = new OrderByOperatorResolver(parser.ParseOrderBy());

            resolver.Resolve().Should().Be("ORDER BY EnterpriseName asc, PartnerName desc");
        }
    }
}
