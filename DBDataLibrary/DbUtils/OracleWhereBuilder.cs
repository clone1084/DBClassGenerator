using DBDataLibrary.Attributes;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DBDataLibrary.DbUtils
{


    public class OracleWhereBuilder : ExpressionVisitor
    {
        private StringBuilder _sb = new();
        private readonly Dictionary<string, object?> _parameters = new();
        private int _paramCounter = 0;

        public (string whereClause, Dictionary<string, object?> parameters) BuildWhere<T>(Expression<Func<T, bool>> expression)
        {
            _sb.Clear();
            _parameters.Clear();
            Visit(expression.Body);
            return (_sb.ToString(), _parameters);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _sb.Append("(");
            Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if (IsNullConstant(node.Right))
                        _sb.Append(" IS ");
                    else
                        _sb.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    if (IsNullConstant(node.Right))
                        _sb.Append(" IS NOT ");
                    else
                        _sb.Append(" <>");
                    break;
                case ExpressionType.GreaterThan:
                    _sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _sb.Append(" >= ");
                    break;
                case ExpressionType.LessThan:
                    _sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _sb.Append(" <= ");
                    break;
                case ExpressionType.AndAlso:
                    _sb.Append(" AND ");
                    break;
                case ExpressionType.OrElse:
                    _sb.Append(" OR ");
                    break;
                default:
                    throw new NotSupportedException($"Operatore {node.NodeType} non supportato.");
            }

            Visit(node.Right);
            _sb.Append(")");
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ParameterExpression)
            {
                var columnName = GetColumnName(node.Member);
                _sb.Append(columnName);
                return node;
            }

            if (node.Expression is ConstantExpression ce)
            {
                var container = ce.Value;
                if (container == null)
                    throw new InvalidOperationException("Espressione di chiusura nulla.");

                var value = ((FieldInfo)node.Member).GetValue(container);
                var paramName = AddParameter(value);
                _sb.Append(paramName);
                return node;
            }

            var val = GetValue(node);
            var param = AddParameter(val);
            _sb.Append(param);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var paramName = AddParameter(node.Value);
            _sb.Append(paramName);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(string.Contains) && node.Object != null)
            {
                Visit(node.Object);
                _sb.Append(" LIKE ");
                var value = GetValue(node.Arguments[0]);
                var paramName = AddParameter($"%{value}%");
                _sb.Append(paramName);
                return node;
            }
            else if (node.Method.Name == nameof(string.StartsWith) && node.Object != null)
            {
                Visit(node.Object);
                _sb.Append(" LIKE ");
                var value = GetValue(node.Arguments[0]);
                var paramName = AddParameter($"{value}%");
                _sb.Append(paramName);
                return node;
            }
            else if (node.Method.Name == nameof(string.EndsWith) && node.Object != null)
            {
                Visit(node.Object);
                _sb.Append(" LIKE ");
                var value = GetValue(node.Arguments[0]);
                var paramName = AddParameter($"%{value}");
                _sb.Append(paramName);
                return node;
            }
            else if (node.Method.Name == nameof(Enumerable.Contains))
            {
                var values = GetValue(node.Arguments[0]) as IEnumerable<object?>;
                var columnExpr = node.Arguments[1] as MemberExpression;
                var columnName = GetColumnName(columnExpr!.Member);

                if (node.Object == null && node.Arguments.Count == 2)
                {
                    // Static Enumerable.Contains(collection, value)
                    var negated = node.Type == typeof(bool) && node.ToString()!.StartsWith("!Enumerable.Contains");
                    var paramNames = values!.Select(v => AddParameter(v)).ToArray();
                    _sb.Append(columnName);
                    _sb.Append(negated ? " NOT IN (" : " IN (");
                    _sb.Append(string.Join(", ", paramNames));
                    _sb.Append(")");
                    return node;
                }
            }

            throw new NotSupportedException($"Metodo non supportato: {node.Method.Name}");
        }

        private string AddParameter(object? value)
        {
            if (value is Enum)
                value = Convert.ToInt32(value);

            if (value is DateTime dt)
                value = dt;

            if (value?.GetType().IsGenericType == true && value.GetType().GetGenericTypeDefinition() == typeof(Nullable<>))
                value = Convert.ChangeType(value, Nullable.GetUnderlyingType(value.GetType())!);

            var paramName = ":p" + _paramCounter++;
            _parameters[paramName] = value;
            return paramName;
        }

        private static object? GetValue(Expression expr)
        {
            if (expr is ConstantExpression constExpr)
                return constExpr.Value;

            var lambda = Expression.Lambda(expr);
            var compiled = lambda.Compile();
            return compiled.DynamicInvoke();
        }

        private static bool IsNullConstant(Expression expr) =>
            expr is ConstantExpression ce && ce.Value == null;

        private static string GetColumnName(MemberInfo member)
        {
            var attr = member.GetCustomAttribute<ColumnNameAttribute>();
            return attr?.Name ?? member.Name;
        }
    }
}
