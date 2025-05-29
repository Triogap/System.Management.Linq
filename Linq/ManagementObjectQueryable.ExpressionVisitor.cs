using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Management.Linq;
using Types;

internal partial class ManagementObjectQueryable<T>
{
    private class ExpressionVisitor : System.Linq.Expressions.ExpressionVisitor
    {
        private string? _className;
        private readonly StringBuilder _conditionBuilder = new();
        private readonly HashSet<string> _selectedProperties = [];
        private Expression<Func<IEnumerable<T>, object?>>? _clientExpression;
        private ParameterExpression? _whereParameter;
        private ParameterExpression? _selectParameter;

        private IEnumerable<T> GetTypedObjects(ManagementObjectCollection collection)
            => collection.OfType<ManagementObject>().Select(InstanceFactory.CreateInstance<T>);

        public (SelectQuery query, Func<ManagementObjectCollection, object?> clientFunc) Translate(Expression expression)
        {
            try
            {
                Visit(expression);

                if (_className == null)
                {
                    throw new InvalidOperationException($"Failed to find {nameof(ManagementObjects)} in {nameof(expression)}.");
                }

                SelectQuery query;
                if (_selectedProperties.Count == 0)
                {
                    query = new SelectQuery(_className, _conditionBuilder.ToString());
                }
                else
                {
                    query = new SelectQuery(_className, _conditionBuilder.ToString(), [.._selectedProperties]);
                }

                if (_clientExpression == null)
                {
                    return (query, GetTypedObjects);
                }

                var clientFunction = _clientExpression.Compile();
                return (query, moc => clientFunction(GetTypedObjects(moc)));
            }
            finally
            {
                _conditionBuilder.Clear();
                _selectedProperties.Clear();
                _clientExpression = null;
                _whereParameter = null;
                _selectParameter = null;
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            switch (node.Method.DeclaringType?.Name)
            {
                case nameof(Queryable):
                    switch (node.Method.Name)
                    {
                        case nameof(Queryable.Where):
                            Visit(node.Arguments[0]);
                            VisitWhereExpression(node.Arguments, 1);
                            return node;
                        case nameof(Queryable.Select):
                            Visit(node.Arguments[0]);

                            var selectorExpression = (LambdaExpression)((UnaryExpression)node.Arguments[1]).Operand;
                            VisitSelectorExpression(selectorExpression);

                            var selectParameter = Expression.Parameter(typeof(IEnumerable<T>), "selectSource");

                            var enumerableSelect = Expression.Call(
                                typeof(Enumerable),
                                nameof(Enumerable.Select),
                                [typeof(T), selectorExpression.ReturnType],
                                selectParameter,
                                Expression.Constant(selectorExpression.Compile()));

                            _clientExpression = Expression.Lambda<Func<IEnumerable<T>, object?>>(enumerableSelect, selectParameter);
                            return node;
                        case nameof(Queryable.OfType):
                            Visit(node.Arguments[0]);
                            var targetType = node.Method.GetGenericArguments()[0];
                            var ofTypeparameter = Expression.Parameter(typeof(IEnumerable<T>), "ofTypeSource");
                            var enumerableOfType = Expression.Call(
                                typeof(Enumerable),
                                nameof(Enumerable.OfType),
                                [targetType],
                                ofTypeparameter);
                            ExtendOrSetClientExpression(Expression.Lambda<Func<IEnumerable<T>, object?>>(enumerableOfType, ofTypeparameter));
                            return node;
                        case nameof(Queryable.Single):
                            Visit(node.Arguments[0]);
                            VisitWhereExpression(node.Arguments, 1);
                            ExtendOrSetClientExpression(c => c.Single());
                            return node;
                        case nameof(Queryable.SingleOrDefault):
                            Visit(node.Arguments[0]);
                            VisitWhereExpression(node.Arguments, 1);
                            ExtendOrSetClientExpression(c => c.SingleOrDefault());
                            return node;
                        case nameof(Queryable.First):
                            Visit(node.Arguments[0]);
                            VisitWhereExpression(node.Arguments, 1);
                            ExtendOrSetClientExpression(c => c.First());
                            return node;
                        case nameof(Queryable.FirstOrDefault):
                            Visit(node.Arguments[0]);
                            VisitWhereExpression(node.Arguments, 1);
                            ExtendOrSetClientExpression(c => c.FirstOrDefault());
                            return node;
                        case nameof(Queryable.Any):
                            Visit(node.Arguments[0]);
                            VisitWhereExpression(node.Arguments, 1);
                            ExtendOrSetClientExpression(c => c.Any());
                            return node;
                        case nameof(Queryable.All):
                            Visit(node.Arguments[0]);
                            VisitWhereExpression(node.Arguments, 1);
                            _conditionBuilder.Insert(0, "NOT (");
                            _conditionBuilder.Append(')');
                            ExtendOrSetClientExpression(c => !c.Any());
                            return node;
                    }
                    break;
                case nameof(WMIExtensions):
                    switch (node.Method.Name)
                    {
                        case nameof(WMIExtensions.Like):
                            ThrowIfNotInWhere(node);
                            _conditionBuilder.Append('(');
                            Visit(node.Arguments[0]);
                            _conditionBuilder.Append($" LIKE '{GetValueNotNull(node.Arguments[1])}')");
                            return node;
                    }
                    break;
                case nameof(String):
                    switch (node.Method.Name)
                    {
                        case nameof(String.Contains):
                            ThrowIfNotInWhere(node);
                            _conditionBuilder.Append('(');
                            Visit(node.Object);
                            _conditionBuilder.Append($" LIKE '%{GetValueNotNull(node.Arguments[0])}%')");
                            return node;
                        case nameof(String.StartsWith):
                            ThrowIfNotInWhere(node);
                            _conditionBuilder.Append('(');
                            Visit(node.Object);
                            _conditionBuilder.Append($" LIKE '{GetValueNotNull(node.Arguments[0])}%')");
                            return node;
                        case nameof(String.EndsWith):
                            ThrowIfNotInWhere(node);
                            _conditionBuilder.Append('(');
                            Visit(node.Object);
                            _conditionBuilder.Append($" LIKE '%{GetValueNotNull(node.Arguments[0])}')");
                            return node;
                        case nameof(Equals):
                            ThrowIfNotInWhere(node);
                            _conditionBuilder.Append('(');
                            Visit(node.Object);
                            _conditionBuilder.Append(' ');
                            _conditionBuilder.Append(GetOperator(ExpressionType.Equal, node.Arguments[0]));
                            _conditionBuilder.Append(' ');
                            Visit(node.Arguments[0]);
                            _conditionBuilder.Append(')');
                            return node;
                    }
                    break;
                case nameof(Object):
                    switch (node.Method.Name)
                    {
                        case nameof(GetType):
                            ThrowIfNotInWhere(node);
                            if (_whereParameter == node.Object)
                            {
                                _conditionBuilder.Append("__CLASS");
                            }
                            else
                            {
                                throw new NotSupportedException($"The method {nameof(GetType)} is only supported on {_whereParameter!.Name}.");
                            }
                            return node;
                        case nameof(Equals):
                            ThrowIfNotInWhere(node);
                            _conditionBuilder.Append('(');
                            Visit(node.Object);
                            _conditionBuilder.Append(' ');
                            _conditionBuilder.Append(GetOperator(ExpressionType.Equal, node.Arguments[0]));
                            _conditionBuilder.Append(' ');
                            Visit(node.Arguments[0]);
                            _conditionBuilder.Append(')');
                            return node;
                    }
                    break;
                default:
                    if (_whereParameter != null)
                    {
                        WriteValue(GetValue(node));
                        return node;
                    }
                    break;
            }
            throw new NotSupportedException($"The method '{node.Method.DeclaringType?.Name}.{node.Method.Name}' is not supported");
        }

        private void ThrowIfNotInWhere(MethodCallExpression node)
        {
            if (_whereParameter == null)
            {
                throw new NotSupportedException($"The method '{node.Method.DeclaringType?.Name}.{node.Method.Name}' is only supported in Where.");
            }
        }

        private void ExtendOrSetClientExpression(Expression<Func<IEnumerable<T>, object?>> expression)
        {
            if (_clientExpression == null)
            {
                _clientExpression = expression;
                return;
            }
            ExtendClientExpression(expression); ;
        }

        private void ExtendClientExpression(LambdaExpression expression)
        {
            if (_clientExpression == null)
            {
                throw new InvalidOperationException($"{nameof(ExtendClientExpression)} should only be called after {nameof(_clientExpression)} is checked to not be null.");
            }

            var existingParameter = expression.Parameters[0];
            ParameterExpression? newParameter;
            if (_clientExpression.Body is MethodCallExpression existingMethodCall)
            {
                newParameter = Expression.Parameter(existingMethodCall.Method.ReturnType, "MethodCallResult");
            }
            else if (_clientExpression.Body is InvocationExpression existingInvocation)
            {
                var invokedLambda = (LambdaExpression)existingInvocation.Expression;
                newParameter = Expression.Parameter(invokedLambda.ReturnType, "InvocationResult");
            }
            else
            {
                throw new NotSupportedException();
            }

            Expression updatedBody;
            if (newParameter.Type == existingParameter.Type)
            {
                newParameter = existingParameter;
                updatedBody = expression.Body;
            }
            else
            {
                var replacer = new ParameterReplacer(existingParameter, newParameter);
                updatedBody = replacer.Visit(expression.Body);
            }

            var combinedBody = Expression.Invoke(Expression.Lambda(updatedBody, newParameter), _clientExpression.Body);
            _clientExpression = Expression.Lambda<Func<IEnumerable<T>, object?>>(combinedBody, _clientExpression.Parameters);
        }


        private void VisitSelectorExpression(LambdaExpression selectorExpression)
        {
            _selectParameter = selectorExpression.Parameters[0];
            Visit(selectorExpression.Body);
            _selectParameter = null;
        }

        private void VisitWhereExpression(ReadOnlyCollection<Expression> arguments, int index)
        {
            if (arguments.Count > index)
            {
                VisitWhereExpression(arguments[index]);
            }
        }

        private void VisitWhereExpression(Expression e)
        {
            var whereExpression = (LambdaExpression)StripQuotes(e);
            if (_clientExpression == null)
            {
                if (_conditionBuilder.Length != 0)
                {
                    _conditionBuilder.Append(" AND ");
                }
                _conditionBuilder.Append('(');
                _whereParameter = whereExpression.Parameters[0];
                Visit(whereExpression.Body);
                _whereParameter = null;
                _conditionBuilder.Append(')');
                return;
            }

            Type[] types = [whereExpression.Parameters[0].Type];
            var parameter = Expression.Parameter(typeof(IEnumerable<>).MakeGenericType(types), "whereSource");
            var enumerableWhere = Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Where),
                types,
                parameter,
                Expression.Constant(whereExpression.Compile()));

            ExtendClientExpression(Expression.Lambda(enumerableWhere, parameter));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (_whereParameter != null)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.UnaryPlus:
                        Visit(node.Operand);
                        return node;
                    case ExpressionType.Negate:
                        _conditionBuilder.Append('-');
                        Visit(node.Operand);
                        return node;
                    case ExpressionType.Not:
                        _conditionBuilder.Append("NOT (");
                        Visit(node.Operand);
                        _conditionBuilder.Append(')');
                        return node;
                    case ExpressionType.TypeAs:
                        throw new NotSupportedException();
                }
            }
            return base.VisitUnary(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _conditionBuilder.Append('(');
            Visit(node.Left);
            _conditionBuilder.Append(' ');
            _conditionBuilder.Append(GetOperator(node.NodeType, node.Right));
            _conditionBuilder.Append(' ');
            Visit(node.Right);
            _conditionBuilder.Append(')');
            return node;
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return base.VisitConditional(node);
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            if (_whereParameter == null)
            {
                throw new NotSupportedException("TypeBinaryExpression are only supported in Where.");
            }


            return base.VisitTypeBinary(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is ManagementObjectQueryable<T> queryable &&
                queryable.ClassName is string className)
            {
                _className = className;
            }
            else if (_whereParameter != null)
            {
                WriteValue(GetValue(node));
            }
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (_whereParameter != null)
            {
                if (node.Member.Name == nameof(Nullable<int>.HasValue) &&
                    node.Member.DeclaringType != null &&
                    node.Member.DeclaringType.IsGenericType &&
                    node.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Visit(node.Expression);
                    _conditionBuilder.Append(" IS NOT NULL");
                }
                else if (node.Expression == _whereParameter)
                {
                    _conditionBuilder.Append(node.Member.Name);
                }
                else
                {
                    WriteValue(GetValue(node));
                }
            }
            else if (_selectParameter != null &&
                node.Expression == _selectParameter)
            {
                _selectedProperties.Add(node.Member.Name);
            }
            return node;
        }

        private static object GetValueNotNull(Expression expression)
            => GetValue(expression) ?? throw new ArgumentNullException(nameof(expression), $"The expression '{expression}' resolves to null.");

        private static object? GetValue(Expression? expression)
            => expression switch
            {
                MemberExpression member => GetValue(member),
                ConstantExpression constant => GetValue(constant),
                MethodCallExpression mehtodCall => GetValue(mehtodCall),
                null => null,
                _ => new NotSupportedException($"The expression type '{expression.GetType()}' is not supported.")
            };

        private static object? GetValue(MemberExpression member)
        {
            var constant = GetValue(member.Expression);
            return member.Member switch
            {
                FieldInfo fieldInfo => fieldInfo.GetValue(constant),
                PropertyInfo propertyInfo => propertyInfo.GetValue(constant),
                _ => throw new NotSupportedException($"Member of type '{member.Member.GetType()}' is not supported.")
            };
        }

        private static object? GetValue(ConstantExpression constant)
            => constant.Value;

        private static object? GetValue(MethodCallExpression methodCall)
        {
            var arguments = methodCall.Arguments.Select(GetValue).ToArray();
            return methodCall.Method.Invoke(GetValue(methodCall.Object), arguments);
        }

        private void WriteValue(object? value)
            => _conditionBuilder.Append(value switch
            {
                Exception ex => throw ex,
                null => "NULL",
                Type type => InstanceFactory.GetClassNameOf(type),
                bool b => b ? "TRUE" : "FALSE",
                _ => Type.GetTypeCode(value.GetType()) switch
                {
                    TypeCode.String => $"'{value}'",
                    TypeCode.DateTime => $"'{value}'",
                    _ => value
                }
            });

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node;
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        private static string GetOperator(ExpressionType nodeType, Expression right)
            => nodeType switch
            {
                ExpressionType.And or ExpressionType.AndAlso => "AND",
                ExpressionType.Or or ExpressionType.OrElse => "OR",
                _ => GetValue(right) == null
                    ? nodeType switch
                    {
                        ExpressionType.Equal => "IS",
                        ExpressionType.NotEqual => "IS NOT",
                        _ => throw new NotSupportedException($"The operator '{nodeType}' is not supported when comparing to null.")
                    }
                    : nodeType switch
                    {
                        ExpressionType.Equal => "=",
                        ExpressionType.NotEqual => "<>",
                        ExpressionType.GreaterThan => ">",
                        ExpressionType.GreaterThanOrEqual => ">=",
                        ExpressionType.LessThan => "<",
                        ExpressionType.LessThanOrEqual => "<=",
                        _ => throw new NotSupportedException($"The operator '{nodeType}' is not supported")
                    }
            };
    }
}
