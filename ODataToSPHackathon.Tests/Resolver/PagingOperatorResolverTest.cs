using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODataToSPHackathon.Infrastructure;
using FluentAssertions;
using Microsoft.OData.Core.UriParser;

namespace ODataToSPHackathon.Tests.Resolver
{
    [TestClass]
    public class PagingOperatorResolverTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePagingOperatorDefault()
        {
            IQueryResolver resolver = new PagingOperatorResolver(0, 0);

            resolver.Resolve().Should().Be("ROWNUMBER BETWEEN 0 AND 10");
        }

        [TestMethod]
        public void ShouldBePagingOperatorForOnlyTopTen()
        {
            ODataUriParser parser = GetParser("/Enterprises?$top=10");

            IQueryResolver resolver = new PagingOperatorResolver(parser.ParseTop() ?? 0, parser.ParseSkip() ?? 0);

            resolver.Resolve().Should().Be("ROWNUMBER BETWEEN 0 AND 10");
        }

        [TestMethod]
        public void ShouldBePagingOperatorForTopAndSkip()
        {
            ODataUriParser parser = GetParser("/Enterprises?$top=2&$skip=2");

            IQueryResolver resolver = new PagingOperatorResolver(parser.ParseTop() ?? 0, parser.ParseSkip() ?? 0);

            resolver.Resolve().Should().Be("ROWNUMBER BETWEEN 2 AND 4");
        }

        [TestMethod]
        public void ShouldBePagingOperatorForOnlySkip()
        {
            ODataUriParser parser = GetParser("/Enterprises?$skip=10");

            IQueryResolver resolver = new PagingOperatorResolver(parser.ParseTop() ?? 0, parser.ParseSkip() ?? 0);

            resolver.Resolve().Should().Be("ROWNUMBER BETWEEN 10 AND 20");
        }
    }
}
