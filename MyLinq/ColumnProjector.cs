using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
	internal class ColumnProjector : ExpressionVisitor
	{
		StringBuilder sb;
		int iColumn;
		ParameterExpression row;
		static MethodInfo miGetValue;

		internal ColumnProjector()
		{
			if (miGetValue == null)
			{
				miGetValue = typeof(ProjectionRow).GetMethod("GetValue");
			}
		}

		internal ColumnProjection ProjectColumns(Expression expression, ParameterExpression row)
		{
			this.sb = new StringBuilder();
			this.row = row;
			Expression selector = this.Visit(expression);
			return new ColumnProjection { Columns = this.sb.ToString(), Selector = selector };
		}

		protected override Expression VisitMemberAccess(MemberExpression m)
		{
			if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
			{
				if (this.sb.Length > 0)
				{
					this.sb.Append(", ");
				}
				this.sb.Append(m.Member.Name);
				return Expression.Convert(Expression.Call(this.row, miGetValue, Expression.Constant(iColumn++)), m.Type);
			}
			else
			{
				return base.VisitMemberAccess(m);
			}
		}
	}
}
