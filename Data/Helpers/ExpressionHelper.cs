using System.Linq.Expressions;

namespace Data.Helpers;

public static class ExpressionHelper
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        if (left == null && right == null) throw new NullReferenceException("At least one argument must not be null");
        if (left == null) return right;
        if (right == null) return left;

        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        Expression combined = new ParameterReplacer(parameter).Visit(Expression.AndAlso(left.Body, right.Body));
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

    class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        internal ParameterReplacer(ParameterExpression parameter)
        {
            this._parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression _)
            => _parameter;
        
    }
}