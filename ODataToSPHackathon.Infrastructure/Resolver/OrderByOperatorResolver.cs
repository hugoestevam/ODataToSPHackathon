using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser;
using System.Collections.Generic;

namespace ODataToSPHackathon.Infrastructure
{
    public class OrderByOperatorResolver : IQueryResolver
    {
        private List<string> fields;

        //?$orderby=%20PartnerName%20asc,EnterpriseName%20asc
        public OrderByOperatorResolver(OrderByClause orderByClause)
        {
            if (orderByClause != null)
            {
                fields = new List<string>();                
                FillList(orderByClause);
            }
        }

        private void FillList(OrderByClause orderByClause)
        {
            var property = orderByClause.Expression as SingleValuePropertyAccessNode;

            string field = string.Format("{0} {1}", property.Property.Name, orderByClause.Direction == OrderByDirection.Ascending ? "asc" : "desc");
            fields.Add(field);

            if (orderByClause.ThenBy != null)
                FillList(orderByClause.ThenBy);
        }


        public string Resolve()
        {
            if (fields!=null)
                return string.Format("ORDER BY {0}", string.Join(", ", fields.ToArray()));
            return string.Empty;
        }
    }
}