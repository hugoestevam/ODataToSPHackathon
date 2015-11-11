using System;

namespace ODataToSPHackathon.Infrastructure
{
    public class PagingOperatorResolver : IQueryResolver
    {
        private long _top;
        private long _skip;

        public PagingOperatorResolver(long top, long skip)
        {
            _top = top == 0 ? 10 : top; //Default value
            _skip = skip; 
        }

        public string Resolve()
        {
            return string.Format("ROWNUMBER BETWEEN {0} AND {1}", _skip, _skip + _top);
        }
    }
}