using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.OData.Core.UriParser;
using ODataToSPHackathon.Infrastructure;
using Microsoft.OData.Core.UriParser.Semantic;
using FluentAssertions;

namespace ODataToSPHackathon.Tests.Resolver
{
    [TestClass]
    public class SingleValueFunctionResolverTest : BaseTest
    {
        [TestMethod]
        public void ShouldBeLikeOperatorEmpty()
        {
            IQueryResolver resolver = new SingleValueFunctionResolver(null);

            resolver.Resolve().Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldBeLikeOperatorForContainsString()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=contains(EnterpriseName,%27520%27)");

            IQueryResolver resolver = new SingleValueFunctionResolver(parser.ParseFilter().Expression as SingleValueFunctionCallNode);

            resolver.Resolve().Should().Be("WHERE EnterpriseName LIKE '%520%'");
        }

        [TestMethod]
        public void ShouldBeLikeOperatorForStartsWithString()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=startswith(EnterpriseName,%27NP%27)");

            IQueryResolver resolver = new SingleValueFunctionResolver(parser.ParseFilter().Expression as SingleValueFunctionCallNode);

            resolver.Resolve().Should().Be("WHERE EnterpriseName LIKE 'NP%'");
        }

        [TestMethod]
        public void ShouldBeLikeOperatorForEndsWithString()
        {
            ODataUriParser parser = GetParser("/Enterprises?$filter=endswith(EnterpriseName,%27520%27)");

            IQueryResolver resolver = new SingleValueFunctionResolver(parser.ParseFilter().Expression as SingleValueFunctionCallNode);

            resolver.Resolve().Should().Be("WHERE EnterpriseName LIKE '%520'");
        }

        //To be continued...
    }
}
