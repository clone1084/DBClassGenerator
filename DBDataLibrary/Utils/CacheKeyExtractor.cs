using System.Linq.Expressions;
using System.Reflection;

namespace DBDataLibrary.Utils
{
    public static class CacheKeyExtractor
    {
        public static bool TryExtractKeyValues<TClass>(Expression<Func<TClass, bool>> expr, List<PropertyInfo> keyProps, out Dictionary<string, object> dict)
        {
            dict = new();

            if (expr.Body is BinaryExpression andExpr && andExpr.NodeType == ExpressionType.AndAlso)
            {
                var stack = new Stack<BinaryExpression>();
                stack.Push(andExpr);

                while (stack.Count > 0)
                {
                    var current = stack.Pop();

                    if (current.Left is BinaryExpression leftBinary && leftBinary.NodeType == ExpressionType.AndAlso)
                        stack.Push(leftBinary);
                    else if (!TryExtractBinary(current.Left, keyProps, ref dict))
                        return false;

                    if (current.Right is BinaryExpression rightBinary && rightBinary.NodeType == ExpressionType.AndAlso)
                        stack.Push(rightBinary);
                    else if (!TryExtractBinary(current.Right, keyProps, ref dict))
                        return false;
                }

                return dict.Count == keyProps.Count;
            }
            else
            {
                return TryExtractBinary(expr.Body, keyProps, ref dict) && dict.Count == keyProps.Count;
            }
        }

        private static bool TryExtractBinary(Expression expr, List<PropertyInfo> keyProps, ref Dictionary<string, object> dict)
        {
            if (expr is BinaryExpression binary && binary.NodeType == ExpressionType.Equal)
            {
                if (binary.Left is MemberExpression member && binary.Right is ConstantExpression constant)
                {
                    var prop = keyProps.FirstOrDefault(p => p.Name == member.Member.Name);
                    if (prop != null)
                    {
                        dict[prop.Name] = constant.Value;
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool TryExtractSingleKey<TClass>(Expression<Func<TClass, bool>> expr, out string propertyName, out object value)
        {
            propertyName = null;
            value = null;

            if (expr.Body is BinaryExpression binary && binary.NodeType == ExpressionType.Equal)
            {
                if (binary.Left is MemberExpression member && binary.Right is ConstantExpression constant)
                {
                    propertyName = member.Member.Name;
                    value = constant.Value;
                    return true;
                }
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
