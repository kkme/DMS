using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
	internal class ProjectionBuilder : DbExpressionVisitor
	{
		ParameterExpression row;
		private static MethodInfo miGetValue;

		internal ProjectionBuilder()
		{
			if (miGetValue == null)
			{
				miGetValue = typeof(ProjectionRow).GetMethod("GetValue");
			}
		}

		internal LambdaExpression Build(Expression expression)
		{
			this.row = Expression.Parameter(typeof(ProjectionRow), "row");
			Expression body = this.Visit(expression);
			return Expression.Lambda(body, this.row);
		}

		protected override Expression VisitColumn(ColumnExpression column)
		{
			return Expression.Convert(Expression.Call(this.row, miGetValue, Expression.Constant(column.Ordinal)), column.Type);
		}
	}
}
