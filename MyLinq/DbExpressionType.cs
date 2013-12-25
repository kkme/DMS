using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
	internal enum DbExpressionType
	{
		Table = 1000, // make sure these don't overlap with ExpressionType
		Column,
		Select,
		Projection
	}
}
