using System.Collections;
using System.Linq.Expressions;

namespace System.Management.Linq;

internal partial class ManagementObjectQueryable<T> : IQueryable<T>
{
    public string? ClassName { get; }
    public Type ElementType => typeof(T);
    public Expression Expression { get; }
    public IQueryProvider Provider { get; }

    public ManagementObjectQueryable(string className)
    {
        ClassName = className;
        Expression = Expression.Constant(this);
        Provider = new QueryProvider();
    }

    private ManagementObjectQueryable(Expression expression, IQueryProvider provider)
    {
        Expression = expression;
        Provider = provider;
    }

    public IEnumerator<T> GetEnumerator()
        => Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}