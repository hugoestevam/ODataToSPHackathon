using System;

namespace ODataToSPHackathon.Infrastructure
{
    public class SqlPagingBinder : ISqlBinder
    {
        
        public SqlPagingBinder(long? topClause, long? skipClause)
        {
            Query = new PagingOperatorResolver(topClause ?? 0, skipClause ?? 0);
        }

        public IQueryResolver Query { get; set; }

        public string Resolve()
        {
            return Query.Resolve();
        }
    }
}