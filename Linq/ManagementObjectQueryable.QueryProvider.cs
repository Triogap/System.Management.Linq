using System.Linq.Expressions;

namespace System.Management.Linq;

internal partial class ManagementObjectQueryable<T>
{
    private class QueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
            => new ManagementObjectQueryable<object?>(expression, this);

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            => new ManagementObjectQueryable<TElement>(expression, this);

        public object? Execute(Expression expression)
            => Execute<object?>(expression);

        public TResult Execute<TResult>(Expression expression)
        {
            var visitor = new ManagementObjectQueryable<T>.ExpressionVisitor();
            (var query, var clientFunc) = visitor.Translate(expression);

            using var searcher = new ManagementObjectSearcher(query);
            return (TResult)clientFunc(searcher.Get())!;

        }
    }
}
