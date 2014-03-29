using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.DynamicProxy
{
	public static class Any
	{
		public static T Value<T>()
		{
			return default(T);
		}
	}
}
