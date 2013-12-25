using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
	public class DbQueryProvider : QueryProvider
	{
		DbConnection connection;

		public DbQueryProvider(DbConnection connection)
		{
			this.connection = connection;
		}

		public override string GetQueryText(Expression expression)
		{
			return this.Translate(expression).CommandText;
		}

		public override object Execute(Expression expression)
		{
			TranslateResult result = this.Translate(expression);
			Delegate projector = result.Projector.Compile();

			DbCommand cmd = this.connection.CreateCommand();
			cmd.CommandText = result.CommandText;
			DbDataReader reader = cmd.ExecuteReader();

			Type elementType = TypeSystem.GetElementType(expression.Type);
			return Activator.CreateInstance(
				typeof(ProjectionReader<>).MakeGenericType(elementType),
				BindingFlags.Instance | BindingFlags.NonPublic, null,
				new object[] { reader, projector },
				null
				);
		}

		internal class TranslateResult
		{
			internal string CommandText;
			internal LambdaExpression Projector;
		}

		private TranslateResult Translate(Expression expression)
		{
			expression = Evaluator.PartialEval(expression);
			ProjectionExpression proj = (ProjectionExpression)new QueryBinder().Bind(expression);
			string commandText = new QueryFormatter().Format(proj.Source);
			LambdaExpression projector = new ProjectionBuilder().Build(proj.Projector);
			return new TranslateResult { CommandText = commandText, Projector = projector };
		}
	}
}
