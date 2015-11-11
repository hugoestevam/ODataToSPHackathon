using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataToSPHackathon.Infrastructure
{
    public class SqlFilterBinder : ISqlBinder
    {

        public IQueryResolver Query { get; set; }

        public SqlFilterBinder(FilterClause filterClause)
        {
            if (filterClause != null)
            {
                //TODO: Resolver posteriormente de maneira mais elegante                
                if (filterClause.Expression is BinaryOperatorNode)
                    Query = new BinaryOperatorResolver(filterClause.Expression as BinaryOperatorNode);

                else if (filterClause.Expression is SingleValueFunctionCallNode)
                    Query = new SingleValueFunctionResolver(filterClause.Expression as SingleValueFunctionCallNode);
            }
        }

        public string Resolve()
        {
            if(Query != null)
                return Query.Resolve();
            return string.Empty;
        }
    }
}
