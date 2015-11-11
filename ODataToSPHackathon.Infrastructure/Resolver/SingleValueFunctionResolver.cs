using Microsoft.OData.Core.UriParser.Semantic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace ODataToSPHackathon.Infrastructure
{
    public class SingleValueFunctionResolver : IQueryResolver
    {
        private string Name = string.Empty;
        private string Property;
        private QueryNodeKind Query;
        private string Value;

        //$filter=contains(EnterpriseName,%27NPDB%27)
        //$filter=startswith(EnterpriseName,%27NPDB%27)
        //$filter=endswith(EnterpriseName,%27NPDB%27)
        public SingleValueFunctionResolver(SingleValueFunctionCallNode function)
        {
            if (function != null)
            {
                var property = function.Parameters.First() as SingleValuePropertyAccessNode;
                var constant = function.Parameters.Last() as ConstantNode;

                Name = function.Name;
                Query = function.Kind;

                if (property != null && property.Property != null && constant != null && constant.Value != null)
                {
                    Property = property.Property.Name;
                    Value = constant.Value.ToString();
                }
            }
        }

        public string Resolve()
        {
            string result = string.Empty;

            switch (Name)
            {
                case "contains":
                    result = string.Format("WHERE {0} LIKE '%{1}%'", Property, Value);
                    break;
                case "startswith":
                    result = string.Format("WHERE {0} LIKE '{1}%'", Property, Value);
                    break;
                case "endswith":
                    result = string.Format("WHERE {0} LIKE '%{1}'", Property, Value);
                    break;
                default:

                    break;
            }

            return result;
        }
    }
}
