using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
	internal sealed class ProjectedColumns
	{
		Expression projector;
		ReadOnlyCollection<ColumnDeclaration> columns;
		internal ProjectedColumns(Expression projector, ReadOnlyCollection<ColumnDeclaration> columns)
		{
			this.projector = projector;
			this.columns = columns;
		}
		internal Expression Projector
		{
			get { return this.projector; }
		}
		internal ReadOnlyCollection<ColumnDeclaration> Columns
		{
			get { return this.columns; }
		}
	}
}
