using System.Linq.Expressions;

namespace System.Management.Linq;

public class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _oldParameter;
    private readonly ParameterExpression _newParameter;
    private readonly Dictionary<Type, Type> _typePairs = [];

    public ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
    {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
        FillTypePairs(_oldParameter.Type, _newParameter.Type);
    }

    private void FillTypePairs(Type oldType, Type newType)
    {
        if (oldType == newType ||
            _typePairs.ContainsKey(oldType))
        {
            return;
        }

        _typePairs[oldType] = newType;
        if (oldType.IsGenericType && newType.IsGenericType)
        {
            var genericTypeDefinition = oldType.GetGenericTypeDefinition();
            if (genericTypeDefinition == newType.GetGenericTypeDefinition())
            {
                var oldTypeArguments = oldType.GetGenericArguments();
                var newTypeArguments = newType.GetGenericArguments();
                for (var i = 0; i < oldTypeArguments.Length; ++i)
                {
                    FillTypePairs(oldTypeArguments[i], newTypeArguments[i]);
                }
            }
        }
    }

    protected override Expression VisitParameter(ParameterExpression node)
        => node == _oldParameter ? _newParameter : base.VisitParameter(node);


    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (_typePairs.Count == 0 || !node.Method.IsGenericMethod)
        {
            return base.VisitMethodCall(node);
        }

        var method = node.Method.GetGenericMethodDefinition()
            .MakeGenericMethod(node.Method.GetGenericArguments().Select(t => _typePairs.TryGetValue(t , out var tNew) ? tNew : t).ToArray());
        var arguments = node.Arguments.Select(arg => Visit(arg)).ToArray();
        return Expression.Call(node.Object == null ? null : Visit(node.Object), method, arguments);
    }
}