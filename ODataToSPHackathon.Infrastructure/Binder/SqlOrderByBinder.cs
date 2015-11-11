using Microsoft.OData.Core.UriParser.Semantic;

namespace ODataToSPHackathon.Infrastructure
{
    public class SqlOrderByBinder : ISqlBinder
    {
        public IQueryResolver Query { get; set; }
       
        public SqlOrderByBinder(OrderByClause orderByClause)
        {
            //TODO: Resolver posteriormente de maneira mais elegante  
            if (orderByClause != null)
                Query = new OrderByOperatorResolver(orderByClause);               
            
        }

        public string Resolve()
        {
            if (Query != null)
                return Query.Resolve();
            return string.Empty;
        }
    }
}