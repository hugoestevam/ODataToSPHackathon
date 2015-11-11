using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Edm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODataToSPHackathon.Models;
using System;
using System.Web.OData.Builder;

namespace ODataToSPHackathon.Tests
{
    public class BaseTest
    {
        public IEdmModel _model;

        [TestInitialize]
        public void Initialize()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<EnterprisesModel>("Enterprises");
            _model = builder.GetEdmModel();
        }

        protected ODataUriParser GetParser(string url)
        {
            Uri fullUri = new Uri(url, UriKind.Relative);
            ODataUriParser parser = new ODataUriParser(_model, fullUri);
            return parser;
        }
    }
}