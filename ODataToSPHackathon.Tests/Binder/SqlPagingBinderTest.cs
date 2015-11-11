using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.OData.Core.UriParser;
using ODataToSPHackathon.Infrastructure;
using FluentAssertions;

namespace ODataToSPHackathon.Tests
{
    [TestClass]
    public class SqlPagingBinderTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePagingOperatorResolverInstance()
        {
            ODataUriParser parser = GetParser("/Enterprises?$top=1&$skip=2");

            ISqlBinder binder = new SqlPagingBinder(parser.ParseTop(), parser.ParseSkip());

            binder.Query.Should().BeOfType<PagingOperatorResolver>();
        }

        [TestMethod]
        public void ShouldNotBeNullPagingOperatorInstance()
        {
            ODataUriParser parser = GetParser("/Enterprises");

            ISqlBinder binder = new SqlPagingBinder(parser.ParseTop(), parser.ParseSkip());

            binder.Query.Should().NotBeNull();
        }
    }
}
