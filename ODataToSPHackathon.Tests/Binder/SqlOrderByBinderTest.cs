using FluentAssertions;
using Microsoft.OData.Core.UriParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODataToSPHackathon.Infrastructure;
using System;

namespace ODataToSPHackathon.Tests
{
    [TestClass]
    public class SqlOrderByBinderTest : BaseTest
    {
        
        [TestMethod]
        public void ShouldBeOrderByOperatorResolverInstance()
        {
            ODataUriParser parser = GetParser("/Enterprises?$orderby=EnterpriseName");

            ISqlBinder binder = new SqlOrderByBinder(parser.ParseOrderBy());

            binder.Query.Should().BeOfType<OrderByOperatorResolver>();
        }

        [TestMethod]
        public void ShouldBeNullOrderByOperatorInstance()
        {
            ODataUriParser parser = GetParser("/Enterprises");

            ISqlBinder binder = new SqlOrderByBinder(parser.ParseOrderBy());

            binder.Query.Should().BeNull();
        }
    }
}
