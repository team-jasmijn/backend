using System.Linq.Expressions;

namespace Data.Helpers;

public static class ExpressionHelper
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var secondBody = expr2.Body.Replace(expr2.Parameters[0], expr1.Parameters[0]);
        return Expression.Lambda<Func<T, bool>>
            (Expression.AndAlso(expr1.Body, secondBody), expr1.Parameters);
    }
    private static Expression Replace(this Expression expression, Expression searchEx, Expression replaceEx)
    {
        return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
    }

    class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public ReplaceVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}