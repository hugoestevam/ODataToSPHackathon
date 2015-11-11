using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace ODataToSPHackathon.Infrastructure
{
    public class BinaryOperatorResolver : IQueryResolver
    {
        private BinaryOperatorKind Operator;
        private string Property;
        private string Value;

        //$filter=EnterpriseName%20eq%20%27DIEGO_520%27
        public BinaryOperatorResolver(BinaryOperatorNode binaryOperator)
        {
            if (binaryOperator != null)
            {
                var property = binaryOperator.Left as SingleValuePropertyAccessNode ?? binaryOperator.Right as SingleValuePropertyAccessNode;
                var constant = binaryOperator.Left as ConstantNode ?? binaryOperator.Right as ConstantNode;

                if (property != null && property.Property != null && constant != null && constant.Value != null)
                {
                    Property = property.Property.Name;
                    Operator = binaryOperator.OperatorKind;
                    Value = constant.LiteralText;
                } 
            }
        }

        public string Resolve()
        {
            //Aqui o bicho pega, tem que prever todas os operadores lógicos + tipos de dados
            //Melhorar pra não ficar uma cascatinha de IF
            if (Operator == BinaryOperatorKind.Equal)
                return string.Format("WHERE {0} LIKE {1}", Property, Value);
            else if (Operator == BinaryOperatorKind.GreaterThan)
                return string.Format("WHERE {0} > {1}", Property, Value);
            else if (Operator == BinaryOperatorKind.LessThan)
                return string.Format("WHERE {0} < {1}", Property, Value);
            return string.Empty;
        }
    }
}
