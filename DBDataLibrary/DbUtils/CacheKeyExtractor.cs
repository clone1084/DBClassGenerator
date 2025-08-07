using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.DbUtils
{
    public static class CacheKeyExtractor
    {
        public static bool TryExtractKeyValues<T>(
            Expression<Func<T, bool>> expression,
            IReadOnlyList<PropertyInfo> keyProperties,
            out Dictionary<string, object?> keyValues)
        {
            keyValues = new();
            var visitor = new KeyEqualityVisitor(keyProperties);
            visitor.Visit(expression);

            if (visitor.IsValid)
            {
                keyValues = visitor.KeyValues!;
                return true;
            }

            return false;
        }

        private class KeyEqualityVisitor : ExpressionVisitor
        {
            private readonly IReadOnlyList<PropertyInfo> _keyProps;
            public Dictionary<string, object?>? KeyValues { get; private set; } = new();
            public bool IsValid => KeyValues != null && KeyValues.Count == _keyProps.Count;

            public KeyEqualityVisitor(IReadOnlyList<PropertyInfo> keyProps)
            {
                _keyProps = keyProps;
            }

            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (node.NodeType == ExpressionType.Equal)
                {
                    var member = node.Left as MemberExpression ?? node.Right as MemberExpression;
                    var constant = node.Left as ConstantExpression ?? node.Right as ConstantExpression;

                    if (member != null && constant != null)
                    {
                        var prop = _keyProps.FirstOrDefault(p => p.Name == member.Member.Name);
                        if (prop != null)
                        {
                            KeyValues![prop.Name] = constant.Value;
                        }
                    }

                    // Caso: confronto con closure
                    if (member != null && (node.Left is MemberExpression || node.Right is MemberExpression))
                    {
                        var val = Expression.Lambda(node.Right == member ? node.Left : node.Right).Compile().DynamicInvoke();
                        var prop = _keyProps.FirstOrDefault(p => p.Name == member.Member.Name);
                        if (prop != null)
                        {
                            KeyValues![prop.Name] = val;
                        }
                    }
                }

                return base.VisitBinary(node);
            }

            //protected override Expression VisitBinary(LogicalBinaryExpression node)
            //{
            //    return base.VisitBinary(node);
            //}
        }
    }

}
