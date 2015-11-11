using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.OData.Builder;
using ODataToSPHackathon.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using ODataToSPHackathon.Infrastructure;
using FluentAssertions;

namespace ODataToSPHackathon.Tests
{
    [TestClass]
    public class SqlFilterBinderTest : BaseTest
    {
        [TestMethod]
        public void ShouldBeBinaryOperatorResolverInstance()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=CorporateName%20eq%20%27Diego%27");

            ISqlBinder binder = new SqlFilterBinder(parser.ParseFilter());

            binder.Query.Should().BeOfType<BinaryOperatorResolver>();
        }        

        [TestMethod]
        public void ShouldBeSingleValueFunctionOperatorResolverInstance()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=contains(EnterpriseName,%27520%27)");

            ISqlBinder binder = new SqlFilterBinder(parser.ParseFilter());

            binder.Query.Should().BeOfType<SingleValueFunctionResolver>();
        }

        [TestMethod]
        public void ShouldBeNullOrderByOperatorInstance()
        {
            ODataUriParser parser = GetParser("/Enterprises");

            ISqlBinder binder = new SqlFilterBinder(parser.ParseFilter());

            binder.Query.Should().BeNull();
        }
    }
}
